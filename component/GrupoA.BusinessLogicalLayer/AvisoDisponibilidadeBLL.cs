using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrupoA.BusinessObject;
using GrupoA.DataAccess;
using GrupoA.DataAccess.ADO;
using System.Configuration;
using GrupoA.GlobalResources;
using GrupoA.BusinessLogicalLayer.Helper;
using GrupoA.BusinessObject.Enumerator;
using System.Text.RegularExpressions;

namespace GrupoA.BusinessLogicalLayer
{
    /// <summary>
    /// Classe que abstrai as regras de negócio referentes a usuários.
    /// </summary>
    public class AvisoDisponibilidadeBLL : BaseBLL
    {
        /// <summary>
        /// 
        /// </summary>
        private IAvisoDisponibilidadeDAL _avisoDisponibilidadeDAL;

        /// <summary>
        /// 
        /// </summary>
        private IAvisoDisponibilidadeDAL AvisoDisponibilidadeDAL
        {
            get
            {
                if (_avisoDisponibilidadeDAL == null)
                    _avisoDisponibilidadeDAL = new AvisoDisponibilidadeADO();
                return _avisoDisponibilidadeDAL;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public AvisoDisponibilidade Inserir(AvisoDisponibilidade entidade)
        {
            AvisoDisponibilidadeDAL.Inserir(entidade);
            return entidade;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void Atualizar(AvisoDisponibilidade entidade)
        {
            AvisoDisponibilidadeDAL.Atualizar(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public AvisoDisponibilidade Carregar(AvisoDisponibilidade entidade)
        {
            return AvisoDisponibilidadeDAL.Carregar(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public List<AvisoDisponibilidade> Carregar(Produto entidade)
        {
            return AvisoDisponibilidadeDAL.Carregar(entidade).ToList();
        }

        /// <summary>
        /// Dispara serviço de atualização em "avisoDisponibilidade", atualizando as solicitações expiradas
        /// para "Cancelado", e enviando e-mail para as solicitações válidas que estão aguardando por um produto
        /// disponível
        /// </summary>
        public void DispararAvisoDisponibilidade()
        {
            try
            {
                int avisoDisponibilidadePrazo = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["AvisoDisponibilidadePrazo"].ToString());
                String caminhoTemplate = System.Configuration.ConfigurationManager.AppSettings["CaminhoEmailAvisoDisponibilidade"].ToString();

                //DateTime dataLimiteSolicitacao = DateTime.Now.AddDays(-avisoDisponibilidadePrazo);

                //AvisoDisponibilidadeDAL.AtualizarSolicitacaoExpirada(dataLimiteSolicitacao);

                IEnumerable<AvisoDisponibilidade> avisoDisponibilidadeBOList = new List<AvisoDisponibilidade>();

                avisoDisponibilidadeBOList = AvisoDisponibilidadeDAL.CarregarAvisoDisponibilidadePendente();

                if (avisoDisponibilidadeBOList != null && avisoDisponibilidadeBOList.Any())
                {
                    foreach (AvisoDisponibilidade avisoDisponibilidadeBOTemp in avisoDisponibilidadeBOList)
                    {
                        try
                        {
                            bool flagEmail = false;

                            if (!String.IsNullOrEmpty(avisoDisponibilidadeBOTemp.Email))
                            {
                                flagEmail = this.EnviarEmail(avisoDisponibilidadeBOTemp, avisoDisponibilidadeBOTemp.Email, caminhoTemplate);
                            }

                            if (flagEmail) // Se e-mail enviado, atualiza status de "avisoDisponibilidade"
                            {
                                avisoDisponibilidadeBOTemp.DataNotificacao = DateTime.Now;
                                avisoDisponibilidadeBOTemp.AvisoDisponibilidadeStatus = new AvisoDisponibilidadeStatus();
                                avisoDisponibilidadeBOTemp.AvisoDisponibilidadeStatus.AvisoDisponibilidadeStatusId = StatusDeAvisoDisponibilidade.Enviado.GetHashCode();

                                new AvisoDisponibilidadeBLL().Atualizar(avisoDisponibilidadeBOTemp);
                            }
                        }
                        catch(Exception ex) 
                        {
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailDestino"></param>
        /// <param name="caminhoTemplate"></param>
        /// <returns></returns>
        private Boolean EnviarEmail(AvisoDisponibilidade avisoDisponibilidade, String emailDestino, String caminhoTemplate)
        {
            String emailEmitente = String.Empty;
            String assuntoEmail = "Grupo A | O produto que você quer já está disponível no nosso site!";
            String isbn13 = String.Empty;

            Configuracao configuracao = new Configuracao();
            configuracao.Chave = "emailGrupoA";
            configuracao = new ConfiguracaoBLL().CarregarCompleto(configuracao);
            emailEmitente = configuracao.ConfiguracaoValor.Valor;

            TituloImpresso tituloImpressoBO = new TituloImpressoBLL().CarregarPorProduto(avisoDisponibilidade.Produto.ProdutoId);

            if (tituloImpressoBO != null && tituloImpressoBO.TituloImpressoId > 0)
            {
                isbn13 = tituloImpressoBO.Isbn13;
            }
            else
            {
                TituloEletronico tituloEletronicoBO = new TituloEletronicoBLL().Carregar(new TituloEletronico { TituloEletronicoId = avisoDisponibilidade.Produto.ProdutoId });

                if (tituloEletronicoBO != null && tituloEletronicoBO.TituloEletronicoId > 0)
                {
                    isbn13 = tituloEletronicoBO.Isbn13;
                }
            }

            avisoDisponibilidade.Titulo.TituloInformacaoResumo = new TituloInformacaoResumoBLL().Carregar(avisoDisponibilidade.Titulo);

            string urlDetalhe = String.Concat(
                                            ConfigurationManager.AppSettings["CaminhoImagem"].ToString()
                                            , "busca/DetalheAux.aspx?categoriaId="
                                            , avisoDisponibilidade.Produto.Categorias[0].CategoriaId
                                            , "&produtoId="
                                            , avisoDisponibilidade.Produto.ProdutoId
                                            , "&tituloId="
                                            , avisoDisponibilidade.Titulo.TituloId
                                            , "&nomeTitulo="
                                            , avisoDisponibilidade.Titulo.NomeTitulo
                                            );

            Dictionary<string, string> dicionarioDados = new Dictionary<string, string>();
            dicionarioDados.Add("Titulo", avisoDisponibilidade.Titulo.NomeTitulo);
            dicionarioDados.Add("Subtitulo", avisoDisponibilidade.Titulo.SubtituloLivro);
            dicionarioDados.Add("Edicao", avisoDisponibilidade.Titulo.Edicao != null ? String.Concat(avisoDisponibilidade.Titulo.Edicao.ToString(), "ª edição") : String.Empty);
            dicionarioDados.Add("Autores", this.CarregarAutores(avisoDisponibilidade.Titulo));
            dicionarioDados.Add("ISBN", isbn13);
            dicionarioDados.Add("Resumo", avisoDisponibilidade.Titulo.TituloInformacaoResumo != null ? System.Web.HttpUtility.HtmlDecode(avisoDisponibilidade.Titulo.TituloInformacaoResumo.TextoResumo) : String.Empty);
            dicionarioDados.Add("Tipo", avisoDisponibilidade.Produto.ProdutoTipo.Tipo);
            dicionarioDados.Add("UrlImagemTitulo", this.CarregarImagemTitulo(avisoDisponibilidade.Produto));
            dicionarioDados.Add("UrlTitulo", urlDetalhe);
            dicionarioDados.Add("CaminhoSite", ConfigurationManager.AppSettings["CaminhoImagem"].ToString());

            try // Tenta enviar e-mail
            {
                StringBuilder templateEmail = new EmailHelper().PopulaTemplateEmail(dicionarioDados, caminhoTemplate);

                new EmailHelper().EnviarEmail(emailEmitente, emailDestino, assuntoEmail, templateEmail);
            }
            catch
            {
                return false; // Mensagem NÃO enviada
            }

            return true;
        }

        private string CarregarAutores(Titulo titulo)
        {
            List<Autor> autorBOList = new TituloBLL().CarregarAutores(titulo);
            StringBuilder texto = new StringBuilder();

            int x = 1;

            if (autorBOList != null && autorBOList.Count > 0)
            {
                foreach (Autor autorBOTemp in autorBOList)
                {
                    if (!string.IsNullOrEmpty(autorBOTemp.NomeAutor))
                    {
                        texto.Append(autorBOTemp.NomeAutor);

                        if (x < autorBOList.Count)
                        {
                            texto.Append("; ");
                        }
                    }

                    x++;
                }
            }

            return texto.ToString();
        }

        private string CarregarImagemTitulo(Produto produto)
        {
            string path = String.Concat(ConfigurationManager.AppSettings["sitePath"].ToString(), GrupoA_Resource.PastaRaizUploads, GrupoA_Resource.PastaImagensTitulo);

            List<ProdutoImagem> produtoImagemBOList = new ProdutoImagemBLL().Carregar(produto);

            produtoImagemBOList = (from ProdutoImagem produtoImagem in produtoImagemBOList
                                   where produtoImagem.ProdutoImagemTipo != null && produtoImagem.ProdutoImagemTipo.ProdutoImagemTipoId == 2
                                   select produtoImagem).ToList();

            if (produtoImagemBOList != null && produtoImagemBOList.Any() && produtoImagemBOList[0] != null && produtoImagemBOList[0].Arquivo != null && produtoImagemBOList[0].Arquivo.ArquivoId > 0)
            {
                Arquivo arquivoBO = new ArquivoBLL().CarregarArquivo(new Arquivo { ArquivoId = produtoImagemBOList[0].Arquivo.ArquivoId });

                if (arquivoBO != null && arquivoBO.ArquivoId > 0 && !String.IsNullOrEmpty(arquivoBO.NomeArquivo))
                {
                    return String.Concat(path, arquivoBO.NomeArquivo);
                }
            }

            return String.Concat(path, GrupoA_Resource.ImagemTituloInexistenteM);
        }
    }
}