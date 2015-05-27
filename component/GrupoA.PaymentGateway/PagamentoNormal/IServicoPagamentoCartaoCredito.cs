using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrupoA.PaymentGateway
{
	public interface IServicoPagamentoCartaoCredito
	{
        /// <summary>
        /// Realiza o pedido.
        /// </summary>
        /// <param name="pedidoCartaoCreditoDto"></param>
        /// <returns></returns>
		RetornoPedidoDTO CriarPedido(PedidoCartaoCreditoDTO pedidoCartaoCreditoDto);

        /// <summary>
        /// Captura o processamento do pedido.
        /// </summary>
        /// <param name="pedidoCartaoCreditoDto"></param>
        /// <returns></returns>
		RetornoPedidoDTO CapturarPedido(PedidoCartaoCreditoDTO pedidoCartaoCreditoDto);

        /// <summary>
        /// Cancela um pedido.
        /// </summary>
        /// <param name="pedidoCartaoCreditoDto"></param>
        /// <returns></returns>
		RetornoPedidoDTO CancelarPedido(PedidoCartaoCreditoDTO pedidoCartaoCreditoDto);

        /// <summary>
        /// Recebe do IPagare novo status do pedido.
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        RetornoPedidoDTO AtualizarStatusPedido(String xml);
	}
}