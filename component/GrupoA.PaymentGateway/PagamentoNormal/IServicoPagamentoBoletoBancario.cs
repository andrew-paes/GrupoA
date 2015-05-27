using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrupoA.PaymentGateway
{
    public interface IServicoPagamentoBoletoBancario
	{
        /// <summary>
        /// Capturar boleto bancário do pedido.
        /// </summary>
        /// <param name="codigoDoPedido"></param>
        /// <returns></returns>
        String CapturarBoletoBancario(String codigoDoPedido);

        /// <summary>
        /// Consulta boleto bancário do pedido.
        /// </summary>
        /// <param name="codigoDoPedido"></param>
        /// <returns></returns>
        PedidoBoletoBancarioDTO ConsultarBoletoBancario(String codigoDoPedido);
	}
}