using System;
using System.Configuration;
using System.Linq;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;
using GrupoA.BusinessObject.Enumerator;
using GrupoA.Sincronizacao.DTO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace GrupoA.Sincronizacao
{
    public class UsuarioSincronizador
    {
        #region [ Propriedades ]

        LogBLL _logBLL;
        protected LogBLL logBLL
        {
            get
            {
                if (_logBLL == null)
                {
                    _logBLL = new LogBLL();
                }
                return _logBLL;
            }
        }

        UsuarioBLL _usuarioBLL;
        protected UsuarioBLL usuarioBLL
        {
            get
            {
                if (_usuarioBLL == null)
                {
                    _usuarioBLL = new UsuarioBLL();
                }
                return _usuarioBLL;
            }
        }

        #endregion

        /// <summary>
        /// Sincroniza todos os usuários que estão com integração pendente conforme regras (fluxograma)
        /// </summary>
        public void SincronizarClientesComIntegracaoPendente()
        {
            try
            {
                List<Usuario> usuarioBOList = usuarioBLL.CarregarUsuariosComSincronizacaoPendente();

                if (usuarioBOList != null && usuarioBOList.Any())
                {
                    foreach (Usuario usuario in usuarioBOList)
                    {
                        try
                        {
                            if (usuario.Pedidos != null && usuario.Pedidos.Count > 0) // Se possui pedido já é para ser Customer.
                            {
                                SincronizarCustomer(usuario); // Sincroniza o Cliente
                            }
                            else
                            {
                                SincronizarProspect(usuario); // Sincroniza Prospecção
                            }
                        }
                        catch (Exception ex)
                        {
                            LogDados logDados = CriarLogDados(EnumLogCategoria.Sincronizacao, LogEventos.ErroEmClient, null, "Erro Sincronismo: " + ex.Message);
                            LogOcorrencia logOcorrencia = new LogOcorrencia();
                            logOcorrencia.LogEvento = new LogEvento(LogEventos.ErroEmClient.GetHashCode());
                            logOcorrencia.Dados = logDados.ToXml();
                            logBLL.RegistrarOcorrenciaLog(logOcorrencia);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogDados logDados = CriarLogDados(EnumLogCategoria.Sincronizacao, LogEventos.ErroEmClient, null, "Erro Geral: " + ex.Message);
                LogOcorrencia logOcorrencia = new LogOcorrencia();
                logOcorrencia.LogEvento = new LogEvento(LogEventos.ErroEmClient.GetHashCode());
                logOcorrencia.Dados = logDados.ToXml();
                logBLL.RegistrarOcorrenciaLog(logOcorrencia);
            }
        }

        #region Customer

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public void SincronizarCustomer(Usuario usuario)
        {
            string xmlRetorno = string.Empty;

            try
            {
                using (ServicoPortal.AM_PORTAL proxy = new ServicoPortal.AM_PORTAL()) // Faz a chamada ao WS
                {
                    CustomerDTO customerDTO = PopularUsuarioCustomer(usuario);

                    if (customerDTO != null && !String.IsNullOrEmpty(customerDTO.CGC))
                    {
                        xmlRetorno = proxy.GETCLIENTES(
                                                        customerDTO.WSEmprensa
                                                        , customerDTO.WSFilial
                                                        , customerDTO.WSLogin
                                                        , customerDTO.WSSenha
                                                        , customerDTO.CGC
                                                        , "F" //sempre F a pedido do Carlos Grupo A
                                                        , "F" //sempre F a pedido do Carlos Grupo A
                                                        , customerDTO.Nome
                                                        , customerDTO.NomeReduzido
                                                        , customerDTO.Inscricao
                                                        , customerDTO.InscricaoMunicipal
                                                        , customerDTO.Endereco
                                                        , customerDTO.Bairro
                                                        , customerDTO.CodigoMunicipio
                                                        , customerDTO.Municipio
                                                        , customerDTO.Estado
                                                        , customerDTO.CEP
                                                        , customerDTO.Email
                                                        , customerDTO.DDD
                                                        , customerDTO.Telefone
                                                        , customerDTO.HomePage
                                                        );

                        // Atribui os campos ao controle e atualiza as informações
                        //XmlDocument doc = new XmlDocument();
                        //doc.InnerXml = xmlRetorno;
                        //string id = doc.SelectSingleNode("//CUSTOMERCODE").InnerText;

                        //usuario.UsuarioControle.CustomerId = id;
                        usuario.UsuarioControle.DataHoraUltimaSincronia = DateTime.Now;
                        usuario.UsuarioControle.RealizarSincronizacao = false;
                        usuarioBLL.AtualizarDadosControle(usuario);
                    }
                }
            }
            catch (Exception exception)
            {
                LogDados logDados = CriarLogDados(EnumLogCategoria.Sincronizacao, LogEventos.ErroEmClient, usuario, exception.Message);
                LogOcorrencia logOcorrencia = new LogOcorrencia();
                logOcorrencia.LogEvento = new LogEvento(LogEventos.ErroEmClient.GetHashCode());
                logOcorrencia.Dados = logDados.ToXml();
                logBLL.RegistrarOcorrenciaLog(logOcorrencia);
            }

            //return xmlRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public CustomerDTO PopularUsuarioCustomer(Usuario usuario)
        {
            CustomerDTO customerDTO = new CustomerDTO();

            customerDTO.WSEmprensa = ConfigurationManager.AppSettings["WSEmprensa"].ToString();
            customerDTO.WSFilial = ConfigurationManager.AppSettings["WSFilial"].ToString();
            customerDTO.WSLogin = ConfigurationManager.AppSettings["WSLogin"].ToString();
            customerDTO.WSSenha = ConfigurationManager.AppSettings["WSSenha"].ToString();
            customerDTO.CGC = usuario.CadastroPessoa;
            customerDTO.Tipo = usuario.TipoPessoa == 0 ? "PF" : "PJ";
            customerDTO.Pessoa = "";
            customerDTO.Nome = this.FormataPalavra(usuario.NomeUsuario);
            customerDTO.NomeReduzido = "";
            customerDTO.Inscricao = "";
            customerDTO.InscricaoMunicipal = "";

            // Endereco
            if (usuario.Enderecos != null && usuario.Enderecos.Any() && usuario.Enderecos[0] != null)
            {
                string endereco = string.Concat(
                                                this.FormataPalavra(usuario.Enderecos[0].Logradouro)
                                                , ", "
                                                , this.FormataPalavra(usuario.Enderecos[0].Numero)
                                                , (usuario.Enderecos[0].Complemento != null && usuario.Enderecos[0].Complemento.Length > 0 ? string.Concat(" - ", this.FormataPalavra(usuario.Enderecos[0].Complemento)) : ""));

                customerDTO.Endereco = endereco;
                customerDTO.Bairro = this.FormataPalavra(usuario.Enderecos[0].Bairro);
                customerDTO.CEP = usuario.Enderecos[0].Cep;
                customerDTO.Municipio = "";
                customerDTO.Estado = "";

                string codMunicipio = "";

                if (usuario.Enderecos[0].Municipio != null)
                {
                    customerDTO.Municipio = this.FormataPalavra(usuario.Enderecos[0].Municipio.NomeMunicipio);

                    if (usuario.Enderecos[0].Municipio.Regiao != null)
                    {
                        customerDTO.Estado = this.FormataPalavra(usuario.Enderecos[0].Municipio.Regiao.Uf);
                    }

                    if (usuario.Enderecos[0].Municipio.CodigoIbge != null)
                    {
                        codMunicipio = usuario.Enderecos[0].Municipio.CodigoIbge.Value.ToString().PadLeft(5, '0');
                    }
                }

                customerDTO.CodigoMunicipio = codMunicipio;
            }
            else
            {
                customerDTO.Endereco = "";
                customerDTO.Bairro = "";
                customerDTO.CodigoMunicipio = "";
                customerDTO.Municipio = "";
                customerDTO.Estado = "";
                customerDTO.CEP = "";
            }

            customerDTO.Email = usuario.EmailUsuario.ToUpper();

            if (usuario.Telefones != null && usuario.Telefones.Any() && usuario.Telefones[0] != null)
            {
                customerDTO.DDD = usuario.Telefones[0].DddTelefone != null ? usuario.Telefones[0].DddTelefone : "00";
                customerDTO.Telefone = usuario.Telefones[0].NumeroTelefone != null ? usuario.Telefones[0].NumeroTelefone : "00000000";
            }
            else
            {
                customerDTO.DDD = "00";
                customerDTO.Telefone = "00000000";
            }

            customerDTO.HomePage = "";

            return customerDTO;
        }

        /*
        /// <summary>
        /// Popula Usuário Customer
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public Customer.CUSTOMERVIEW PopularUsuarioEmCustomer(Usuario usuario)
        {
            Customer.CUSTOMERVIEW customerView = new Customer.CUSTOMERVIEW();

            customerView.NAME = usuario.NomeUsuario;
            //Prospect.PROSPECTVIEW.NICKNAME = usuario.
            customerView.FEDERALID = usuario.CadastroPessoa;
            customerView.EMAIL = usuario.EmailUsuario;
            // Prospect.PROSPECTVIEW.HOMEPAGE
            //Prospect.PROSPECTVIEW.CUSTOMERCODE
            //Prospect.PROSPECTVIEW.UNITPROSPECTCODE
            //Prospect.PROSPECTVIEW.UNITCUSTOMERCODE
            //Prospect.PROSPECTVIEW.SELLERCODE
            //TODO: fazer um enum       

            // Endereços
            PopularEnderecosUsuarioCustomer(usuario, customerView);

            // Telefones
            PopularTelefonesUsuarioCustomer(usuario, customerView);

            return customerView;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="customerView"></param>
        public void PopularEnderecosUsuarioCustomer(Usuario usuario, Customer.CUSTOMERVIEW customerView)
        {
            List<Customer.ADDRESSVIEW> enderecos = new List<Customer.ADDRESSVIEW>();
            foreach (Endereco endereco in usuario.Enderecos)
            {
                Customer.ADDRESSVIEW addressView = new Customer.ADDRESSVIEW();
                addressView.ADDRESS = endereco.Logradouro; //string.Concat( endereco.Logradouro, ", ", endereco.Numero, ( endereco.Complemento.Length > 0 ? string.Concat( " - ", endereco.Complemento ) ) );
                addressView.ADDRESSNUMBER = endereco.Numero.ToString();
                addressView.TYPEOFADDRESS = "1";
                addressView.DISTRICT = endereco.Municipio.NomeMunicipio;
                addressView.STATE = endereco.Municipio.Regiao.Uf;
                addressView.COUNTRY = "BR";
                addressView.ZONE = endereco.Bairro;
                addressView.ZIPCODE = endereco.Cep;
                enderecos.Add(addressView);

            }
            customerView.ADDRESSES = enderecos.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="customerView"></param>
        public void PopularTelefonesUsuarioCustomer(Usuario usuario, Customer.CUSTOMERVIEW customerView)
        {
            List<Customer.PHONEVIEW> telefones = new List<Customer.PHONEVIEW>();
            foreach (Telefone telefone in usuario.Telefones)
            {
                Customer.PHONEVIEW phoneview = new Customer.PHONEVIEW();
                // Ver como fica
                phoneview.TYPEOFPHONE = "1";
                //Prospect.PHONEVIEW.COUNTRYAREACODE 
                phoneview.LOCALAREACODE = telefone.DddTelefone;
                phoneview.PHONENUMBER = telefone.NumeroTelefone;
                telefones.Add(phoneview);
            }
            customerView.PHONES = telefones.ToArray();
        }
        */

        #endregion

        #region Prospect

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        public void SincronizarProspect(Usuario usuario)
        {
            string xmlRetorno = string.Empty;

            try
            {
                using (ServicoPortal.AM_PORTAL proxy = new ServicoPortal.AM_PORTAL()) // Faz a chamada ao WS
                {
                    ProspectDTO prospectDTO = PopularUsuarioProspect(usuario);

                    if (prospectDTO != null && !String.IsNullOrEmpty(prospectDTO.CGC))
                    {
                        xmlRetorno = proxy.GETPROSPECT(
                                                        prospectDTO.WSEmprensa
                                                        , prospectDTO.WSFilial
                                                        , prospectDTO.WSLogin
                                                        , prospectDTO.WSSenha
                                                        , prospectDTO.CGC
                                                        , prospectDTO.Nome
                                                        , prospectDTO.NomeReduzido
                                                        , "F" //sempre F a pedido do Carlos Grupo A
                                                        , prospectDTO.Endereco
                                                        , prospectDTO.Bairro
                                                        , prospectDTO.Municipio
                                                        , prospectDTO.Estado
                                                        , prospectDTO.CEP
                                                        , prospectDTO.Email
                                                        , prospectDTO.DDD
                                                        , prospectDTO.Telefone
                                                        , prospectDTO.URL
                                                        , prospectDTO.CodigoMunicipio
                                                        , prospectDTO.Pais
                                                        );

                        //XmlDocument doc = new XmlDocument();
                        //doc.InnerXml = xmlRetorno;
                        //string id = doc.SelectSingleNode("//PROSPECTID").InnerText;

                        // Atribui os campos ao controle e atualiza as informações
                        //usuario.UsuarioControle.ProspectId = id;
                        usuario.UsuarioControle.DataHoraUltimaSincronia = DateTime.Now;
                        usuario.UsuarioControle.RealizarSincronizacao = false;
                        usuarioBLL.AtualizarDadosControle(usuario);
                    }
                }
            }
            catch (Exception exception)
            {
                LogDados logDados = CriarLogDados(EnumLogCategoria.Sincronizacao, LogEventos.ErroEmProspect, usuario, exception.Message);
                LogOcorrencia logOcorrencia = new LogOcorrencia();
                logOcorrencia.LogEvento = new LogEvento(LogEventos.ErroEmProspect.GetHashCode());
                logOcorrencia.Dados = logDados.ToXml();
                logBLL.RegistrarOcorrenciaLog(logOcorrencia);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public ProspectDTO PopularUsuarioProspect(Usuario usuario)
        {
            ProspectDTO prospectDTO = new ProspectDTO();

            prospectDTO.WSEmprensa = ConfigurationManager.AppSettings["WSEmprensa"].ToString();
            prospectDTO.WSFilial = ConfigurationManager.AppSettings["WSFilial"].ToString();
            prospectDTO.WSLogin = ConfigurationManager.AppSettings["WSLogin"].ToString();
            prospectDTO.WSSenha = ConfigurationManager.AppSettings["WSSenha"].ToString();
            prospectDTO.CGC = usuario.CadastroPessoa;
            prospectDTO.Nome = this.FormataPalavra(usuario.NomeUsuario);
            prospectDTO.NomeReduzido = "";
            prospectDTO.Pessoa = "";

            // Endereco
            if (usuario.Enderecos != null && usuario.Enderecos.Any() && usuario.Enderecos[0] != null)
            {
                string endereco = string.Concat(
                                                this.FormataPalavra(usuario.Enderecos[0].Logradouro)
                                                , ", "
                                                , this.FormataPalavra(usuario.Enderecos[0].Numero)
                                                , (usuario.Enderecos[0].Complemento != null && usuario.Enderecos[0].Complemento.Length > 0 ? string.Concat(" - ", this.FormataPalavra(usuario.Enderecos[0].Complemento)) : ""));


                prospectDTO.Endereco = endereco;
                prospectDTO.Bairro = this.FormataPalavra(usuario.Enderecos[0].Bairro);
                prospectDTO.CEP = usuario.Enderecos[0].Cep;
                prospectDTO.Municipio = "";
                prospectDTO.Estado = "";

                string codMunicipio = "";

                if (usuario.Enderecos[0].Municipio != null)
                {
                    prospectDTO.Municipio = this.FormataPalavra(usuario.Enderecos[0].Municipio.NomeMunicipio);

                    if (usuario.Enderecos[0].Municipio.Regiao != null)
                    {
                        prospectDTO.Estado = this.FormataPalavra(usuario.Enderecos[0].Municipio.Regiao.Uf);
                    }

                    if (usuario.Enderecos[0].Municipio.CodigoIbge != null)
                    {
                        codMunicipio = usuario.Enderecos[0].Municipio.CodigoIbge.Value.ToString().PadLeft(5, '0');
                    }
                }

                prospectDTO.CodigoMunicipio = codMunicipio;
            }
            else
            {
                prospectDTO.Endereco = "";
                prospectDTO.Bairro = "";
                prospectDTO.Municipio = "";
                prospectDTO.Estado = "";
                prospectDTO.CEP = "";
                prospectDTO.CodigoMunicipio = "";
            }

            prospectDTO.Pais = "BR";
            prospectDTO.Email = usuario.EmailUsuario.ToUpper();

            if (usuario.Telefones != null && usuario.Telefones.Any() && usuario.Telefones[0] != null)
            {
                prospectDTO.DDD = usuario.Telefones[0].DddTelefone != null ? usuario.Telefones[0].DddTelefone : "00";
                prospectDTO.Telefone = usuario.Telefones[0].NumeroTelefone != null ? usuario.Telefones[0].NumeroTelefone : "00000000";
            }
            else
            {
                prospectDTO.DDD = "00";
                prospectDTO.Telefone = "00000000";
            }

            prospectDTO.URL = "";

            return prospectDTO;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="palavra"></param>
        /// <returns></returns>
        private String FormataPalavra(String palavra)
        {
            if (!String.IsNullOrEmpty(palavra))
            {
                palavra = palavra.RemoveAccents();
                palavra = Regex.Replace(palavra, @"[^a-zA-Z0-9\s#?!&$@]", " "); // invalid chars
                palavra = Regex.Replace(palavra, @"\s+", " ").Trim(); // convert multiple spaces into one space
                palavra = palavra.ToUpper();

                return palavra;
            }
            else
            {
                return palavra;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumLogCategoria"></param>
        /// <param name="logEventos"></param>
        /// <param name="usuario"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public LogDados CriarLogDados(EnumLogCategoria enumLogCategoria, LogEventos logEventos, Usuario usuario, string message)
        {
            LogDados logDados = new LogDados();
            logDados.Adicionar("LogEvento", "LOG_EVENTO", logEventos.GetHashCode().ToString());
            logDados.Adicionar("LogCategoria", "LOG_CATEGORIA", enumLogCategoria.GetHashCode().ToString());
            logDados.Adicionar("DataHora", "DATA_HORA", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            logDados.Adicionar("Usuario", "USUARIO_ID", usuario.UsuarioId.ToString());
            logDados.Adicionar("MensagemErro", "MENSAGEM_ERRO", message);
            return logDados;
        }
    }
}