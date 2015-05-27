using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

namespace GrupoA.PaymentGateway.IPagare
{
    public class ServicoPagamentoCofre : IServicoPagamentoCofre
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pedidoCartaoCreditoDTO"></param>
        /// <returns></returns>
        public RetornoPedidoDTO EfetuarCobranca(PedidoCartaoCreditoDTO pedidoCartaoCreditoDTO)
        {
            RetornoPedidoDTO retornoPedidoDTO = new RetornoPedidoDTO();

            try
            {
                var parametros = new Dictionary<String, String>();

                String codigoEstabelecimento = ConfigurationManager.AppSettings["IPagare.CodigoEstabelecimento"].ToString();
                String codigoSeguranca = ConfigurationManager.AppSettings["IPagare.CodigoSeguranca"].ToString();
                String valorDoPedido = pedidoCartaoCreditoDTO.ValorTotalDoPedido.ToString("n").Replace(",", "").Replace(".", "");
                String chave = MD5Util.CreateHash(String.Concat(codigoEstabelecimento, MD5Util.CreateHash(codigoSeguranca), "2", valorDoPedido, "2")).ToLower();

                parametros.Add("acao", "2");
                parametros.Add("versao", "2");
                parametros.Add("estabelecimento", codigoEstabelecimento);
                parametros.Add("chave", chave);
                parametros.Add("valor_total", valorDoPedido);                

                Boolean IsProducao = Convert.ToBoolean(Convert.ToString(ConfigurationManager.AppSettings["Producao"]));

                if (!IsProducao)
                {
                    pedidoCartaoCreditoDTO.CodigoDoPedido = String.Concat("T", pedidoCartaoCreditoDTO.CodigoDoPedido);
                    parametros.Add("teste", "1");
                }

                parametros.Add("codigo_pedido", pedidoCartaoCreditoDTO.CodigoDoPedido);
                //parametros.Add("codigo_pagamento", pedidoCartaoCreditoDTO.MeioDePagamento);
                parametros.Add("forma_pagamento", pedidoCartaoCreditoDTO.TipoDePagamento);
                parametros.Add("token", pedidoCartaoCreditoDTO.Token);

                // Chamar o método RealizarHttpPost para realização do pedido;
                String retorno = Util.RealizarHttpPost(ConfigurationManager.AppSettings["IPagare.URLWebService"].ToString(), parametros);

                if (!String.IsNullOrEmpty(retorno))
                {
                    //return this.CarregarRetorno(retorno, Convert.ToInt32(pedidoCartaoCreditoDTO.CodigoDoPedido));

                    String xml = retorno;
                    Int32 pedidoId = 0;
                    
                    Int32.TryParse(pedidoCartaoCreditoDTO.CodigoDoPedido.Replace("T", ""), out pedidoId);

                    XDocument xmlDoc = XDocument.Parse(@xml);

                    var query = (from s in xmlDoc.Elements("pedido")
                                 select s).FirstOrDefault();

                    if (query != null)
                    {
                        retornoPedidoDTO.CodigoDeRetorno = query.Element("pagamento").Element("parametros").Element("codigo-retorno").Value.ToString();
                        retornoPedidoDTO.NumeroDaAutorizacao = query.Element("pagamento").Element("parametros").Element("numero-autorizacao").Value.ToString();
                        retornoPedidoDTO.NumeroDaTransacaoNaOperadora = query.Element("pagamento").Element("parametros").Element("numero-transacao").Value.ToString();
                        retornoPedidoDTO.StatusDoPedido = query.Element("status").Value.ToString();
                        retornoPedidoDTO.ValorTotalDoPedido = Convert.ToDecimal(query.Element("pagamento").Element("total").Value.ToString());
                        retornoPedidoDTO.MensagemDeRetorno = "Sucesso";

                        Dictionary<String, String> dRetorno = new Dictionary<String, String>();
                        dRetorno.Add("codigo-retorno", retornoPedidoDTO.CodigoDeRetorno);
                        dRetorno.Add("numero-autorizacao", retornoPedidoDTO.NumeroDaAutorizacao);
                        dRetorno.Add("numero-transacao", retornoPedidoDTO.NumeroDaTransacaoNaOperadora);
                        dRetorno.Add("status", retornoPedidoDTO.StatusDoPedido);
                        dRetorno.Add("total", retornoPedidoDTO.ValorTotalDoPedido.ToString());

                        new ServicoLogPaymentGateway().Inserir(dRetorno, xml, pedidoId); // Grava log pagamentogateway
                    }
                    else
                    {
                        query = (from s in xmlDoc.Elements("erro")
                                 select s).FirstOrDefault();

                        retornoPedidoDTO.CodigoDeRetorno = query.Element("codigo").Value.ToString();
                        retornoPedidoDTO.MensagemDeRetorno = query.Element("descricao").Value.ToString();

                        Dictionary<String, String> dRetorno = new Dictionary<String, String>();
                        dRetorno.Add("codigo", retornoPedidoDTO.CodigoDeRetorno);
                        dRetorno.Add("descricao", retornoPedidoDTO.MensagemDeRetorno);

                        new ServicoLogPaymentGateway().Inserir(dRetorno, xml, pedidoId); // Grava log pagamentogateway
                    }
                }
                else
                {
                    retornoPedidoDTO.CodigoDeRetorno = "204";
                }
            }
            catch (Exception ex)
            {
                retornoPedidoDTO.CodigoDeRetorno = "204";
            }

            return retornoPedidoDTO;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pedidoCartaoCreditoDTO"></param>
        /// <returns></returns>
        public RetornoCofreDTO CriarToken(PedidoCartaoCreditoDTO pedidoCartaoCreditoDTO)
        {
            RetornoCofreDTO retornoCofreDTO = new RetornoCofreDTO();

            try
            {
                var parametros = new Dictionary<String, String>();

                String codigoEstabelecimento = ConfigurationManager.AppSettings["IPagare.CodigoEstabelecimento"].ToString();
                String codigoSeguranca = ConfigurationManager.AppSettings["IPagare.CodigoSeguranca"].ToString();
                String chave = MD5Util.CreateHash(String.Concat(
                                                                codigoEstabelecimento
                                                                , MD5Util.CreateHash(codigoSeguranca)
                                                                , "15" // acao
                                                                , pedidoCartaoCreditoDTO.MeioDePagamento // codigo_pagamento
                                                                , pedidoCartaoCreditoDTO.CartaoDeCredito.NumeroDoCartaoDeCredito
                                                                , pedidoCartaoCreditoDTO.CartaoDeCredito.Expiracao.Substring(0, 2) // mes_validade_cartao
                                                                , pedidoCartaoCreditoDTO.CartaoDeCredito.Expiracao.Substring(2, 4) // ano_validade_cartao
                                                                , pedidoCartaoCreditoDTO.CartaoDeCredito.CodigoDeSegurancaDoCartao
                                                                , "12" // validade_token
                                                                )
                                                    ).ToLower();

                parametros.Add("estabelecimento", codigoEstabelecimento);
                parametros.Add("acao", "15");
                parametros.Add("chave", chave);
                parametros.Add("codigo_pagamento", pedidoCartaoCreditoDTO.MeioDePagamento);
                parametros.Add("numero_cartao", pedidoCartaoCreditoDTO.CartaoDeCredito.NumeroDoCartaoDeCredito);
                parametros.Add("mes_validade_cartao", pedidoCartaoCreditoDTO.CartaoDeCredito.Expiracao.Substring(0, 2));
                parametros.Add("ano_validade_cartao", pedidoCartaoCreditoDTO.CartaoDeCredito.Expiracao.Substring(2, 4));
                parametros.Add("codigo_seguranca_cartao", pedidoCartaoCreditoDTO.CartaoDeCredito.CodigoDeSegurancaDoCartao);
                parametros.Add("validade_token", "12"); // meses

                // Chamar o método RealizarHttpPost para realização do pedido;
                String retorno = Util.RealizarHttpPost(ConfigurationManager.AppSettings["IPagare.URLWebService"].ToString(), parametros);

                if (!String.IsNullOrEmpty(retorno))
                {
                    String xml = retorno;
                    Dictionary<String, String> dRetorno = new Dictionary<String, String>();
                    Int32 pedidoId = Convert.ToInt32(pedidoCartaoCreditoDTO.CodigoDoPedido);

                    XDocument xmlDoc = XDocument.Parse(@xml);

                    if (xmlDoc != null)
                    {
                        var query = (from s in xmlDoc.Elements("cofre")
                                     select s).FirstOrDefault();

                        if (query != null)
                        {
                            if (!String.IsNullOrEmpty(query.Element("token").Value.ToString()))
                            {
                                retornoCofreDTO.Token = query.Element("token").Value.ToString();
                                retornoCofreDTO.DataExpiracao = query.Element("data-expiracao").Value.ToString();
                                retornoCofreDTO.MensagemDeRetorno = "Sucesso";

                                dRetorno = new Dictionary<String, String>();
                                dRetorno.Add("token", retornoCofreDTO.Token);
                                dRetorno.Add("data-expiracao", retornoCofreDTO.DataExpiracao);
                            }
                            else
                            {
                                dRetorno = new Dictionary<String, String>();
                                dRetorno.Add("descricao", "");
                            }
                        }
                        else
                        {
                            query = (from s in xmlDoc.Elements("erro")
                                     select s).FirstOrDefault();

                            //retornoCofreDTO.CodigoDeRetorno = query.Element("codigo").Value.ToString();
                            retornoCofreDTO.MensagemDeRetorno = query.Element("descricao").Value.ToString();

                            dRetorno = new Dictionary<String, String>();
                            dRetorno.Add("descricao", retornoCofreDTO.MensagemDeRetorno);
                        }
                    }
                    else
                    {
                        dRetorno = new Dictionary<String, String>();
                        dRetorno.Add("descricao", "");
                    }

                    new ServicoLogPaymentGateway().Inserir(dRetorno, xml, pedidoId); // Grava log pagamentogateway
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return retornoCofreDTO;
        }

        /// <summary>
        /// Mapear o retorno de RealizarHttpPost para RetornoPedidoDTO
        /// Utilizar Linq to XML/XDocument
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="pedidoId"></param>
        /// <returns></returns>
        private RetornoPedidoDTO CarregarRetorno(String xml, Int32 pedidoId)
        {
            RetornoPedidoDTO retornoPedidoDTO = new RetornoPedidoDTO();
            XDocument xmlDoc = XDocument.Parse(@xml);

            var query = (from s in xmlDoc.Elements("pedido")
                         select s).FirstOrDefault();

            if (query != null)
            {
                retornoPedidoDTO.CodigoDeRetorno = query.Element("pagamento").Element("parametros").Element("codigo-retorno").Value.ToString();
                retornoPedidoDTO.NumeroDaAutorizacao = query.Element("pagamento").Element("parametros").Element("numero-autorizacao").Value.ToString();
                retornoPedidoDTO.NumeroDaTransacaoNaOperadora = query.Element("pagamento").Element("parametros").Element("numero-transacao").Value.ToString();
                retornoPedidoDTO.StatusDoPedido = query.Element("status").Value.ToString();
                retornoPedidoDTO.ValorTotalDoPedido = Convert.ToDecimal(query.Element("pagamento").Element("total").Value.ToString());
                retornoPedidoDTO.MensagemDeRetorno = "Sucesso";

                Dictionary<String, String> dRetorno = new Dictionary<String, String>();
                dRetorno.Add("codigo-retorno", retornoPedidoDTO.CodigoDeRetorno);
                dRetorno.Add("numero-autorizacao", retornoPedidoDTO.NumeroDaAutorizacao);
                dRetorno.Add("numero-transacao", retornoPedidoDTO.NumeroDaTransacaoNaOperadora);
                dRetorno.Add("status", retornoPedidoDTO.StatusDoPedido);
                dRetorno.Add("total", retornoPedidoDTO.ValorTotalDoPedido.ToString());

                new ServicoLogPaymentGateway().Inserir(dRetorno, xml, pedidoId); // Grava log pagamentogateway
            }
            else
            {
                query = (from s in xmlDoc.Elements("erro")
                         select s).FirstOrDefault();

                retornoPedidoDTO.CodigoDeRetorno = query.Element("codigo").Value.ToString();
                retornoPedidoDTO.MensagemDeRetorno = query.Element("tentativa-pagamento").Element("mensagem-financeira").Value.ToString();

                Dictionary<String, String> dRetorno = new Dictionary<String, String>();
                dRetorno.Add("codigo", retornoPedidoDTO.CodigoDeRetorno);
                dRetorno.Add("descricao", retornoPedidoDTO.MensagemDeRetorno);

                new ServicoLogPaymentGateway().Inserir(dRetorno, xml, null); // Grava log pagamentogateway
            }

            return retornoPedidoDTO;
        }
    }
}