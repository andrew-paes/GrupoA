using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Net;
using System.IO;
using System.Xml.Linq;

namespace GrupoA.PaymentGateway.IPagare
{
    public class ServicoPagamentoCartaoCreditoRecorrente : IServicoPagamentoCartaoCreditoRecorrente
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pedidoRecorrenteCartaoCreditoDto"></param>
        /// <returns></returns>
        public RetornoPedidoRecorrenteDTO CriarPedido(PedidoRecorrenteCartaoCreditoDTO pedidoRecorrenteCartaoCreditoDto)
        {
            var parametros = new Dictionary<String, String>();

            String codigoEstabelecimento = ConfigurationManager.AppSettings["IPagare.CodigoEstabelecimento.Recorrente"].ToString();
            String codigoSeguranca = ConfigurationManager.AppSettings["IPagare.CodigoSeguranca.Recorrente"].ToString();
            String valorDoPedido = pedidoRecorrenteCartaoCreditoDto.ValorTotalDoPedido.ToString("n").Replace(",", "").Replace(".", "");
            String chave = MD5Util.CreateHash(codigoEstabelecimento + MD5Util.CreateHash(codigoSeguranca) + "7" + valorDoPedido + "2").ToLower();
            String codigoPedido = pedidoRecorrenteCartaoCreditoDto.CodigoDoPedido;

            // TODO: mapear pedidoCartaoCreditoDto para parametros;
            if (!Convert.ToBoolean(ConfigurationManager.AppSettings["Producao"].ToString()))
            {
                parametros.Add("teste", "1");
                codigoPedido = String.Concat("T", codigoPedido);
            }

            parametros.Add("estabelecimento", codigoEstabelecimento);
            parametros.Add("acao", "7");
            parametros.Add("valor_total", valorDoPedido);
            parametros.Add("versao", "2");
            parametros.Add("codigo_pedido", codigoPedido);
            parametros.Add("chave", chave);
            parametros.Add("codigo_pagamento", pedidoRecorrenteCartaoCreditoDto.MeioDePagamento);
            parametros.Add("forma_pagamento", pedidoRecorrenteCartaoCreditoDto.TipoDePagamento);
            parametros.Add("numero_cartao", pedidoRecorrenteCartaoCreditoDto.CartaoDeCredito.NumeroDoCartaoDeCredito);
            parametros.Add("mes_validade_cartao", pedidoRecorrenteCartaoCreditoDto.CartaoDeCredito.Expiracao.Substring(0, 2));
            parametros.Add("ano_validade_cartao", pedidoRecorrenteCartaoCreditoDto.CartaoDeCredito.Expiracao.Substring(2, 4));
            parametros.Add("codigo_seguranca_cartao", pedidoRecorrenteCartaoCreditoDto.CartaoDeCredito.CodigoDeSegurancaDoCartao);
            parametros.Add("inicio_recorrencia", String.Format("{0:ddMMyyyy}", pedidoRecorrenteCartaoCreditoDto.DataInicialDaRecorrencia.Value));
            parametros.Add("frequencia_recorrencia", pedidoRecorrenteCartaoCreditoDto.IntervaloDaRecorrencia.ToString());
            parametros.Add("numero_recorrencias", pedidoRecorrenteCartaoCreditoDto.NumeroDeParcelas.ToString());

            // TODO: chamar o método RealizarHttpPost para realização do pedido;
            String retorno = Util.RealizarHttpPost(ConfigurationManager.AppSettings["IPagare.URLWebService"].ToString(), parametros);

            // TODO: mapear o retorno de RealizarHttpPost para RetornoPedidoDTO; Obs: utilizar Linq to XML/XDocument;
            return this.CarregarRetorno(retorno);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pedidoRecorrenteCartaoCreditoDto"></param>
        /// <returns></returns>
        public RetornoPedidoRecorrenteDTO CancelarPedido(PedidoRecorrenteCartaoCreditoDTO pedidoRecorrenteCartaoCreditoDto)
        {
            var parametros = new Dictionary<string, string>();

            string codigoEstabelecimento = ConfigurationManager.AppSettings["IPagare.CodigoEstabelecimento.Recorrente"].ToString();
            string codigoSeguranca = ConfigurationManager.AppSettings["IPagare.CodigoSeguranca.Recorrente"].ToString();
            string chave =
                MD5Util.CreateHash(codigoEstabelecimento + MD5Util.CreateHash(codigoSeguranca) + "8" +
                                   pedidoRecorrenteCartaoCreditoDto.CodigoDoPedido.ToString()).ToLower();
            String codigoPedido = pedidoRecorrenteCartaoCreditoDto.CodigoDoPedido;

            // TODO: mapear pedidoCartaoCreditoDto para parametros;
            if (!Convert.ToBoolean(ConfigurationManager.AppSettings["Producao"].ToString()))
            {
                parametros.Add("teste", "1");
                codigoPedido = String.Concat("T", codigoPedido);
            }

            parametros.Add("estabelecimento", codigoEstabelecimento);
            parametros.Add("acao", "8");
            parametros.Add("codigo_pedido", codigoPedido);
            parametros.Add("chave", chave);
            
            // TODO: chamar o método RealizarHttpPost para realização do pedido;
            String retorno = Util.RealizarHttpPost(ConfigurationManager.AppSettings["IPagare.URLWebService"].ToString(), parametros);

            // TODO: mapear o retorno de RealizarHttpPost para RetornoPedidoDTO; Obs: utilizar Linq to XML/XDocument;
            return this.CarregarRetornoCancelamento(retorno);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pedidoRecorrenteCartaoCreditoDto"></param>
        /// <returns></returns>
        public RetornoPedidoRecorrenteDTO AtualizarDadosDoCartaoDeCredito(PedidoRecorrenteCartaoCreditoDTO pedidoRecorrenteCartaoCreditoDto)
        {
            var parametros = new Dictionary<string, string>();

            string codigoEstabelecimento = ConfigurationManager.AppSettings["IPagare.CodigoEstabelecimento.Recorrente"].ToString();
            string codigoSeguranca = ConfigurationManager.AppSettings["IPagare.CodigoSeguranca.Recorrente"].ToString();
            string chave =
                MD5Util.CreateHash(codigoEstabelecimento + MD5Util.CreateHash(codigoSeguranca) + "11" +
                                   pedidoRecorrenteCartaoCreditoDto.CodigoDoPedido.ToString()).ToLower();
            String codigoPedido = pedidoRecorrenteCartaoCreditoDto.CodigoDoPedido;

            // TODO: mapear pedidoCartaoCreditoDto para parametros;
            if (!Convert.ToBoolean(ConfigurationManager.AppSettings["Producao"].ToString()))
            {
                parametros.Add("teste", "1");
                codigoPedido = String.Concat("T", codigoPedido);
            }
            parametros.Add("estabelecimento", codigoEstabelecimento);
            parametros.Add("acao", "11");
            parametros.Add("codigo_pedido", codigoPedido);
            parametros.Add("chave", chave);
            parametros.Add("codigo_pagamento", pedidoRecorrenteCartaoCreditoDto.MeioDePagamento.ToString());
            parametros.Add("forma_pagamento", pedidoRecorrenteCartaoCreditoDto.TipoDePagamento.ToString());
            parametros.Add("numero_cartao", pedidoRecorrenteCartaoCreditoDto.CartaoDeCredito.NumeroDoCartaoDeCredito.ToString());
            parametros.Add("mes_validade_cartao", pedidoRecorrenteCartaoCreditoDto.CartaoDeCredito.Expiracao.Substring(0, 2));
            parametros.Add("ano_validade_cartao", pedidoRecorrenteCartaoCreditoDto.CartaoDeCredito.Expiracao.Substring(2, 4));
            parametros.Add("codigo_seguranca_cartao", pedidoRecorrenteCartaoCreditoDto.CartaoDeCredito.CodigoDeSegurancaDoCartao.ToString());

            // TODO: chamar o método RealizarHttpPost para realização do pedido;
            String retorno = Util.RealizarHttpPost(ConfigurationManager.AppSettings["IPagare.URLWebService"].ToString(), parametros);

            // TODO: mapear o retorno de RealizarHttpPost para RetornoPedidoDTO; Obs: utilizar Linq to XML/XDocument;
            return this.CarregarRetornoAlteracao(retorno);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pedidoRecorrenteCartaoCredito"></param>
        /// <returns></returns>
        public RetornoPedidoRecorrenteDTO ReativarPedido(PedidoRecorrenteCartaoCreditoDTO pedidoRecorrenteCartaoCredito)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pedidoRecorrenteCartaoCreditoDto"></param>
        /// <returns></returns>
        public PedidoRecorrenteCartaoCreditoDTO RecuperarPedido(PedidoRecorrenteCartaoCreditoDTO pedidoRecorrenteCartaoCreditoDto)
        {
            var parametros = new Dictionary<string, string>();

            string codigoEstabelecimento = ConfigurationManager.AppSettings["IPagare.CodigoEstabelecimento.Recorrente"].ToString();
            string codigoSeguranca = ConfigurationManager.AppSettings["IPagare.CodigoSeguranca.Recorrente"].ToString();
            string chave =
                MD5Util.CreateHash(codigoEstabelecimento + MD5Util.CreateHash(codigoSeguranca) + "3" +
                                   pedidoRecorrenteCartaoCreditoDto.CodigoDoPedido.ToString()).ToLower();
            String codigoPedido = pedidoRecorrenteCartaoCreditoDto.CodigoDoPedido;

            // TODO: mapear pedidoCartaoCreditoDto para parametros;
            if (!Convert.ToBoolean(ConfigurationManager.AppSettings["Producao"].ToString()))
            {
                parametros.Add("teste", "1");
                codigoPedido = String.Concat("T", codigoPedido);
            }
            parametros.Add("estabelecimento", codigoEstabelecimento);
            parametros.Add("acao", "3");
            parametros.Add("codigo_pedido", codigoPedido);
            parametros.Add("versao", "2");
            parametros.Add("chave", chave);

            // TODO: chamar o método RealizarHttpPost para realização do pedido;
            String retorno = Util.RealizarHttpPost(ConfigurationManager.AppSettings["IPagare.URLWebService"].ToString(), parametros);

            // TODO: mapear o retorno de RealizarHttpPost para RetornoPedidoDTO; Obs: utilizar Linq to XML/XDocument;
            return this.CarregarPedido(retorno);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pedidoRecorrenteCartaoCreditoDto"></param>
        /// <returns></returns>
        public RecorrenciasDTO RecuperarRecorrenciasDePedido(PedidoRecorrenteCartaoCreditoDTO pedidoRecorrenteCartaoCreditoDto)
        {
            var parametros = new Dictionary<string, string>();

            string codigoEstabelecimento = ConfigurationManager.AppSettings["IPagare.CodigoEstabelecimento.Recorrente"].ToString();
            string codigoSeguranca = ConfigurationManager.AppSettings["IPagare.CodigoSeguranca.Recorrente"].ToString();
            string chave =
                MD5Util.CreateHash(codigoEstabelecimento + MD5Util.CreateHash(codigoSeguranca) + "10" +
                                   pedidoRecorrenteCartaoCreditoDto.CodigoDoPedido.ToString()).ToLower();
            String codigoPedido = pedidoRecorrenteCartaoCreditoDto.CodigoDoPedido;

            // TODO: mapear pedidoCartaoCreditoDto para parametros;
            if (!Convert.ToBoolean(ConfigurationManager.AppSettings["Producao"].ToString()))
            {
                parametros.Add("teste", "1");
                codigoPedido = String.Concat("T", codigoPedido);
            }
            parametros.Add("estabelecimento", codigoEstabelecimento);
            parametros.Add("acao", "10");
            parametros.Add("codigo_pedido", codigoPedido);
            parametros.Add("versao", "2");
            parametros.Add("chave", chave);

            // TODO: chamar o método RealizarHttpPost para realização do pedido;
            String retorno = Util.RealizarHttpPost(ConfigurationManager.AppSettings["IPagare.URLWebService"].ToString(), parametros);

            // TODO: mapear o retorno de RealizarHttpPost para RetornoPedidoDTO; Obs: utilizar Linq to XML/XDocument;
            return this.CarregarRecorrentes(retorno);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pedidoRecorrenteCartaoCreditoDto"></param>
        /// <returns></returns>
        public RetornoPedidoRecorrenteDTO AtualizarValorDoPedido(PedidoRecorrenteCartaoCreditoDTO pedidoRecorrenteCartaoCreditoDto)
        {
            var parametros = new Dictionary<string, string>();

            string codigoEstabelecimento = ConfigurationManager.AppSettings["IPagare.CodigoEstabelecimento.Recorrente"].ToString();
            string codigoSeguranca = ConfigurationManager.AppSettings["IPagare.CodigoSeguranca.Recorrente"].ToString();
            string chave =
                MD5Util.CreateHash(codigoEstabelecimento + MD5Util.CreateHash(codigoSeguranca) + "9" +
                                   pedidoRecorrenteCartaoCreditoDto.CodigoDoPedido.ToString()).ToLower();
            String codigoPedido = pedidoRecorrenteCartaoCreditoDto.CodigoDoPedido;

            // TODO: mapear pedidoCartaoCreditoDto para parametros;
            if (!Convert.ToBoolean(ConfigurationManager.AppSettings["Producao"].ToString()))
            {
                parametros.Add("teste", "1");
                codigoPedido = String.Concat("T", codigoPedido);
            }
            parametros.Add("estabelecimento", codigoEstabelecimento);
            parametros.Add("acao", "9");
            parametros.Add("codigo_pedido", codigoPedido);
            parametros.Add("chave", chave);
            parametros.Add("valor_total", pedidoRecorrenteCartaoCreditoDto.ValorTotalDoPedido.ToString().Replace(".", "").Replace(",", ""));

            // TODO: chamar o método RealizarHttpPost para realização do pedido;
            String retorno = Util.RealizarHttpPost(ConfigurationManager.AppSettings["IPagare.URLWebService"].ToString(), parametros);

            // TODO: mapear o retorno de RealizarHttpPost para RetornoPedidoDTO; Obs: utilizar Linq to XML/XDocument;
            return this.CarregarRetorno(retorno);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private RetornoPedidoRecorrenteDTO CarregarRetorno(String xml)
        {
            RetornoPedidoRecorrenteDTO retornoPedidoRecorrenteDTO = new RetornoPedidoRecorrenteDTO();
            XDocument xmlDoc = XDocument.Parse(@xml);

            var query = (from s in xmlDoc.Elements("recorrencia")
                         select s).FirstOrDefault();

            if (query != null)
            {
                retornoPedidoRecorrenteDTO.CodigoDeRetorno = "0";
                retornoPedidoRecorrenteDTO.MensagemDeRetorno = "Sucesso";

                Dictionary<String, String> dRetorno = new Dictionary<String, String>();
                dRetorno.Add("codigo-retorno", "0");

                //Grava log pagamentogateway
                new ServicoLogPaymentGateway().Inserir(dRetorno, xml, null);
            }
            else
            {
                query = (from s in xmlDoc.Elements("erro")
                         select s).FirstOrDefault();

                retornoPedidoRecorrenteDTO.CodigoDeRetorno = query.Element("codigo").Value.ToString();
                retornoPedidoRecorrenteDTO.MensagemDeRetorno = query.Element("descricao").Value.ToString();
            }

            return retornoPedidoRecorrenteDTO;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private RetornoPedidoRecorrenteDTO CarregarRetornoCancelamento(String xml)
        {
            RetornoPedidoRecorrenteDTO retornoPedidoRecorrenteDTO = new RetornoPedidoRecorrenteDTO();
            XDocument xmlDoc = XDocument.Parse(@xml);

            var query = (from s in xmlDoc.Elements("recorrencia")
                         select s).FirstOrDefault();

            if (query != null)
            {
                retornoPedidoRecorrenteDTO.CodigoDaRecorrencia = Convert.ToInt32(query.Element("codigo").Value.ToString());
                retornoPedidoRecorrenteDTO.Cancelado = Convert.ToInt32(query.Element("cancelado").Value.ToString());
                retornoPedidoRecorrenteDTO.MensagemDeRetorno = "Sucesso";

                Dictionary<String, String> dRetorno = new Dictionary<String, String>();
                dRetorno.Add("codigo", retornoPedidoRecorrenteDTO.CodigoDaRecorrencia.ToString());
                dRetorno.Add("cancelado", retornoPedidoRecorrenteDTO.Cancelado.ToString());

                //Grava log pagamentogateway
                new ServicoLogPaymentGateway().Inserir(dRetorno, xml, null);
            }
            else
            {
                query = (from s in xmlDoc.Elements("erro")
                         select s).FirstOrDefault();

                retornoPedidoRecorrenteDTO.CodigoDeRetorno = query.Element("codigo").Value.ToString();
                retornoPedidoRecorrenteDTO.MensagemDeRetorno = query.Element("descricao").Value.ToString();
            }

            return retornoPedidoRecorrenteDTO;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private RetornoPedidoRecorrenteDTO CarregarRetornoAlteracao(String xml)
        {
            RetornoPedidoRecorrenteDTO retornoPedidoRecorrenteDTO = new RetornoPedidoRecorrenteDTO();
            XDocument xmlDoc = XDocument.Parse(@xml);

            var query = (from s in xmlDoc.Elements("recorrencia")
                         select s).FirstOrDefault();

            if (query != null)
            {
                retornoPedidoRecorrenteDTO.CodigoDaRecorrencia = Convert.ToInt32(query.Element("codigo").Value.ToString());
                retornoPedidoRecorrenteDTO.Cancelado = Convert.ToInt32(query.Element("alterado").Value.ToString());
                retornoPedidoRecorrenteDTO.MensagemDeRetorno = "Sucesso";

                Dictionary<String, String> dRetorno = new Dictionary<String, String>();
                dRetorno.Add("codigo", retornoPedidoRecorrenteDTO.CodigoDaRecorrencia.ToString());
                dRetorno.Add("cancelado", retornoPedidoRecorrenteDTO.Cancelado.ToString());

                //Grava log pagamentogateway
                new ServicoLogPaymentGateway().Inserir(dRetorno, xml, null);
            }
            else
            {
                query = (from s in xmlDoc.Elements("erro")
                         select s).FirstOrDefault();

                retornoPedidoRecorrenteDTO.CodigoDeRetorno = query.Element("codigo").Value.ToString();
            }

            return retornoPedidoRecorrenteDTO;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private PedidoRecorrenteCartaoCreditoDTO CarregarPedido(String xml)
        {
            PedidoRecorrenteCartaoCreditoDTO pedidoRecorrenteCartaoCreditoDTO = new PedidoRecorrenteCartaoCreditoDTO();
            XDocument xmlDoc = XDocument.Parse(@xml);

            var query = (from s in xmlDoc.Elements("pedido")
                         select s).FirstOrDefault();

            if (query != null)
            {
                pedidoRecorrenteCartaoCreditoDTO.CodigoDoPedido = query.Element("codigo").Value.ToString();
                pedidoRecorrenteCartaoCreditoDTO.ValorTotalDoPedido = Convert.ToDecimal(query.Element("total").Value.ToString());
                pedidoRecorrenteCartaoCreditoDTO.MeioDePagamento = query.Element("pagamento").Element("codigo").Value.ToString();
                pedidoRecorrenteCartaoCreditoDTO.TipoDePagamento = query.Element("pagamento").Element("forma").Value.ToString();
            }

            return pedidoRecorrenteCartaoCreditoDTO;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private RecorrenciasDTO CarregarRecorrentes(String xml)
        {
            RecorrenciasDTO recorrenciasDTO = new RecorrenciasDTO();

            recorrenciasDTO.Recorrencias = new List<RetornoPedidoRecorrenteDTO>();

            XDocument xmlDoc = XDocument.Parse(@xml);

            var query = (from s in xmlDoc.Elements("pedido")
                         select s).FirstOrDefault();

            if (query != null)
            {
                recorrenciasDTO.RetornoPedido = new RetornoPedidoDTO();
                recorrenciasDTO.RetornoPedido.CodigoDeRetorno = query.Element("ocorrencias").Element("ocorrencia").Element("pedido").Element("pagamento").Element("parametros").Element("codigo-retorno").Value;
                recorrenciasDTO.RetornoPedido.MensagemDeRetorno = "Sucesso";
                recorrenciasDTO.RetornoPedido.NumeroDaAutorizacao = query.Element("ocorrencias").Element("ocorrencia").Element("pedido").Element("pagamento").Element("parametros").Element("numero-autorizacao").Value;
                recorrenciasDTO.RetornoPedido.NumeroDaTransacaoNaOperadora = query.Element("ocorrencias").Element("ocorrencia").Element("pedido").Element("pagamento").Element("parametros").Element("numero-transacao").Value;
                recorrenciasDTO.RetornoPedido.StatusDoPedido = query.Element("ocorrencias").Element("ocorrencia").Element("pedido").Element("status").Value;
                recorrenciasDTO.RetornoPedido.ValorTotalDoPedido = Convert.ToDecimal(query.Element("ocorrencias").Element("ocorrencia").Element("total").Element("status").Value);
                
                foreach (var recorrencia in query.Element("ocorrencias").Elements())
                {
                    RetornoPedidoRecorrenteDTO retornoPedidoRecorrenteDTO = new RetornoPedidoRecorrenteDTO();
                    retornoPedidoRecorrenteDTO.CodigoDaRecorrencia = Convert.ToInt32(recorrencia.Element("ocorrencia").Element("pedido").Element("codigo").Value.ToString());
                    retornoPedidoRecorrenteDTO.ValorTotalDoPedido = Convert.ToDecimal(recorrencia.Element("ocorrencia").Element("pedido").Element("total").Value.ToString());
                    retornoPedidoRecorrenteDTO.DataDaTentativa = Convert.ToDateTime(String.Format("{0:dd/MM/yyyy}", recorrencia.Element("ocorrencia").Element("data").Value.ToString()));

                    recorrenciasDTO.Recorrencias.ToList().Add(retornoPedidoRecorrenteDTO);
                }
            }

            return recorrenciasDTO;
        }
    }
}
