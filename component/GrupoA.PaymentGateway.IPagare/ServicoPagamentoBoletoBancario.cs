using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Configuration;

namespace GrupoA.PaymentGateway.IPagare
{
    class ServicoPagamentoBoletoBancario : IServicoPagamentoBoletoBancario
    {
        public string CapturarBoletoBancario(string codigoDoPedido)
        {
            throw new NotImplementedException();
        }

        public PedidoBoletoBancarioDTO ConsultarBoletoBancario(string codigoDoPedido)
        {
            var parametros = new Dictionary<string, string>();

            string codigoEstabelecimento = ConfigurationManager.AppSettings["IPagare.CodigoEstabelecimento"].ToString();
            string codigoSeguranca = ConfigurationManager.AppSettings["IPagare.CodigoSeguranca"].ToString();
            string chave = MD5Util.CreateHash(String.Concat(codigoEstabelecimento, MD5Util.CreateHash(codigoSeguranca), "3", codigoDoPedido)).ToLower();

            // mapear pedidoCartaoCreditoDto para parametros;
            if (!Convert.ToBoolean(ConfigurationManager.AppSettings["Producao"].ToString()))
            {
                parametros.Add("teste", "1");
            }
            parametros.Add("estabelecimento", codigoEstabelecimento);
            parametros.Add("acao", "3");
            parametros.Add("codigo_pedido", codigoDoPedido);
            parametros.Add("versao", "2");
            parametros.Add("chave", chave);

            // chamar o método RealizarHttpPost para realização do pedido;
            String retorno = Util.RealizarHttpPost(ConfigurationManager.AppSettings["IPagare.URLWebService"].ToString(), parametros);

            // mapear o retorno de RealizarHttpPost para RetornoPedidoDTO; Obs: utilizar Linq to XML/XDocument;
            return this.CarregarBoletoBancario(retorno);
        }

        private PedidoBoletoBancarioDTO CarregarBoletoBancario(String xml)
        {
            PedidoBoletoBancarioDTO pedidoBoletoBancarioDTO = new PedidoBoletoBancarioDTO();
            XDocument xmlDoc = XDocument.Parse(@xml);

            var query = (from s in xmlDoc.Elements("pedido")
                         select s).FirstOrDefault();

            if (query != null)
            {
                pedidoBoletoBancarioDTO.CodigoDoPedido = query.Element("codigo").Value.ToString();
                pedidoBoletoBancarioDTO.ValorTotalDoPedido = Convert.ToDecimal(query.Element("total").Value.ToString());
                pedidoBoletoBancarioDTO.NumeroDoBoleto = query.Element("pagamento").Element("boleto").Element("nosso-numero").Value.ToString();
                pedidoBoletoBancarioDTO.DataDeVencimento = Convert.ToDateTime(String.Format("{0:dd/MM/yyyy}", query.Element("pagamento").Element("boleto").Element("vencimento").Value.ToString()));
                pedidoBoletoBancarioDTO.Status = Convert.ToInt32(query.Element("pagamento").Element("boleto").Element("pago").Value.ToString());
            }

            return pedidoBoletoBancarioDTO;
        }
    }
}
