using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

namespace GrupoA.PaymentGateway.IPagare
{
    public class ServicoPagamentoCartaoCredito : IServicoPagamentoCartaoCredito
    {
        public RetornoPedidoDTO CriarPedido(PedidoCartaoCreditoDTO pedidoCartaoCreditoDto)
        {
            var parametros = new Dictionary<String, String>();

            String codigoEstabelecimento = ConfigurationManager.AppSettings["IPagare.CodigoEstabelecimento"].ToString();
            String codigoSeguranca = ConfigurationManager.AppSettings["IPagare.CodigoSeguranca"].ToString();
            String valorDoPedido = pedidoCartaoCreditoDto.ValorTotalDoPedido.ToString("n").Replace(",", "").Replace(".", "");
            String chave = MD5Util.CreateHash(String.Concat(codigoEstabelecimento, MD5Util.CreateHash(codigoSeguranca), "2", valorDoPedido, "2")).ToLower();
            String codigoPedido = pedidoCartaoCreditoDto.CodigoDoPedido;

            // mapear pedidoCartaoCreditoDto para parametros;
            if (!Convert.ToBoolean(ConfigurationManager.AppSettings["Producao"].ToString()))
            {
                parametros.Add("teste", "1");
                codigoPedido = String.Concat("T", codigoPedido);
            }

            parametros.Add("estabelecimento", codigoEstabelecimento);
            parametros.Add("acao", "2");
            parametros.Add("valor_total", valorDoPedido);
            parametros.Add("versao", "2");
            parametros.Add("chave", chave);
            parametros.Add("codigo_pedido", codigoPedido);
            parametros.Add("codigo_pagamento", pedidoCartaoCreditoDto.MeioDePagamento);
            parametros.Add("forma_pagamento", pedidoCartaoCreditoDto.TipoDePagamento.ToString());
            parametros.Add("numero_cartao", pedidoCartaoCreditoDto.CartaoDeCredito.NumeroDoCartaoDeCredito);
            parametros.Add("mes_validade_cartao", pedidoCartaoCreditoDto.CartaoDeCredito.Expiracao.Substring(0, 2));
            parametros.Add("ano_validade_cartao", pedidoCartaoCreditoDto.CartaoDeCredito.Expiracao.Substring(2, 4));
            parametros.Add("codigo_seguranca_cartao", pedidoCartaoCreditoDto.CartaoDeCredito.CodigoDeSegurancaDoCartao);

            parametros.Add("tipo_cliente", pedidoCartaoCreditoDto.Cliente.TipoCliente);
            parametros.Add("nome_cliente", pedidoCartaoCreditoDto.Cliente.NomeDoCliente);
            parametros.Add("email_cliente", pedidoCartaoCreditoDto.Cliente.Email);
            parametros.Add("cpf_cnpj_cliente", pedidoCartaoCreditoDto.Cliente.CadastroNacional);
            if (!String.IsNullOrEmpty(pedidoCartaoCreditoDto.Cliente.TelefoneDoClienteDdd) && !String.IsNullOrEmpty(pedidoCartaoCreditoDto.Cliente.TelefoneDoClienteNumero))
            {
                parametros.Add("ddd_telefone_1", pedidoCartaoCreditoDto.Cliente.TelefoneDoClienteDdd);
                parametros.Add("numero_telefone_1", pedidoCartaoCreditoDto.Cliente.TelefoneDoClienteNumero);
            }

            // chamar o método RealizarHttpPost para realização do pedido;
            String retorno = Util.RealizarHttpPost(ConfigurationManager.AppSettings["IPagare.URLWebService"].ToString(), parametros);

            if (!String.IsNullOrEmpty(retorno))
            {
                // mapear o retorno de RealizarHttpPost para RetornoPedidoDTO; Obs: utilizar Linq to XML/XDocument;
                return this.CarregarRetorno(retorno, Convert.ToInt32(pedidoCartaoCreditoDto.CodigoDoPedido));
            }
            else
            {
                RetornoPedidoDTO retornoPedidoDTO = new RetornoPedidoDTO();
                retornoPedidoDTO.CodigoDeRetorno = "204";

                return retornoPedidoDTO;
            }
        }

        public RetornoPedidoDTO CapturarPedido(PedidoCartaoCreditoDTO pedidoCartaoCreditoDto)
        {
            throw new NotImplementedException();
        }

        public RetornoPedidoDTO CancelarPedido(PedidoCartaoCreditoDTO pedidoCartaoCreditoDto)
        {
            throw new NotImplementedException();
        }

        public RetornoPedidoDTO AtualizarStatusPedido(String xml)
        {
            RetornoPedidoDTO retornoPedidoDTO = new RetornoPedidoDTO();

            return retornoPedidoDTO;
        }

        private RetornoPedidoDTO CarregarRetorno(String xml, Int32 pedidoId)
        {
            try
            {
                RetornoPedidoDTO retornoPedidoDto = new RetornoPedidoDTO();
                XDocument xmlDoc = XDocument.Parse(@xml);

                var query = (from s in xmlDoc.Elements("pedido")
                             select s).FirstOrDefault();

                if (query != null)
                {
                    retornoPedidoDto.CodigoDeRetorno = query.Element("pagamento").Element("parametros").Element("codigo-retorno").Value.ToString();
                    retornoPedidoDto.NumeroDaAutorizacao = query.Element("pagamento").Element("parametros").Element("numero-autorizacao").Value.ToString();
                    retornoPedidoDto.NumeroDaTransacaoNaOperadora = query.Element("pagamento").Element("parametros").Element("numero-transacao").Value.ToString();
                    retornoPedidoDto.StatusDoPedido = query.Element("status").Value.ToString();
                    retornoPedidoDto.ValorTotalDoPedido = Convert.ToDecimal(query.Element("pagamento").Element("total").Value.ToString());
                    retornoPedidoDto.MensagemDeRetorno = "Sucesso";

                    Dictionary<String, String> dRetorno = new Dictionary<String, String>();
                    dRetorno.Add("codigo-retorno", retornoPedidoDto.CodigoDeRetorno);
                    dRetorno.Add("numero-autorizacao", retornoPedidoDto.NumeroDaAutorizacao);
                    dRetorno.Add("numero-transacao", retornoPedidoDto.NumeroDaTransacaoNaOperadora);
                    dRetorno.Add("status", retornoPedidoDto.StatusDoPedido);
                    dRetorno.Add("total", retornoPedidoDto.ValorTotalDoPedido.ToString());



                    //Grava log pagamentogateway
                    new ServicoLogPaymentGateway().Inserir(dRetorno, xml, pedidoId);
                }
                else
                {
                    query = (from s in xmlDoc.Elements("erro")
                             select s).FirstOrDefault();

                    retornoPedidoDto.CodigoDeRetorno = query.Element("codigo").Value.ToString();
                    retornoPedidoDto.MensagemDeRetorno = query.Element("descricao").Value.ToString();

                    Dictionary<String, String> dRetorno = new Dictionary<String, String>();
                    dRetorno.Add("codigo", retornoPedidoDto.CodigoDeRetorno);
                    dRetorno.Add("descricao", retornoPedidoDto.MensagemDeRetorno);

                    //Grava log pagamentogateway
                    new ServicoLogPaymentGateway().Inserir(dRetorno, xml, null);
                }

                return retornoPedidoDto;
            }
            catch (Exception ex)
            {
                RetornoPedidoDTO retornoPedidoDTO = new RetornoPedidoDTO();
                retornoPedidoDTO.CodigoDeRetorno = "204";

                return retornoPedidoDTO;
            }
        }
    }
}

