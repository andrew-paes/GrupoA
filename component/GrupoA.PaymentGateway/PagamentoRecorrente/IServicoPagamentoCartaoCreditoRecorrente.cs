using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrupoA.PaymentGateway
{
	public interface IServicoPagamentoCartaoCreditoRecorrente
	{
		/// <summary>
		/// Cria um novo pedido, em que o método de pagamento é cartão de crédito.
		/// </summary>
		/// <param name="pedidoRecorrenteCartaoCreditoDto"></param>
		/// <returns></returns>
        RetornoPedidoRecorrenteDTO CriarPedido(PedidoRecorrenteCartaoCreditoDTO pedidoRecorrenteCartaoCreditoDto);

		/// <summary>
		/// Método que executa a transação para cancelar um pedido.
		/// Popular lojaId e pedidoId
		/// </summary>
		/// <param name="pedidoRecorrenteCartaoCredito"></param>
		/// <returns>RetornoPedidoDTO</returns>
        RetornoPedidoRecorrenteDTO CancelarPedido(PedidoRecorrenteCartaoCreditoDTO pedidoRecorrenteCartaoCredito);

		/// <summary>
		/// Método que executa a transação para atualizar os dados do cartão de créido de um pedido recorrente.
		/// </summary>
		/// <param name="pedidoRecorrenteCartaoCredito"></param>
		/// <returns>RetornoPedidoDTO</returns>
        RetornoPedidoRecorrenteDTO AtualizarDadosDoCartaoDeCredito(PedidoRecorrenteCartaoCreditoDTO pedidoRecorrenteCartaoCredito);

		/// <summary>
		/// Método que reativa um pedido e atualiza os dados do cartão de crédito.
		/// </summary>
		/// <param name="pedidoRecorrenteCartaoCredito"></param>
		/// <returns></returns>
        RetornoPedidoRecorrenteDTO ReativarPedido(PedidoRecorrenteCartaoCreditoDTO pedidoRecorrenteCartaoCredito);

		/// <summary>
		/// Método para buscar um pedido recorrente.
		/// Popular lojaId e pedidoId
		/// </summary>
		/// <param name="pedidoRecorrenteCartaoCredito"></param>
		/// <returns>PedidoRecorrenteCartaoCreditoDTO</returns>
		PedidoRecorrenteCartaoCreditoDTO RecuperarPedido(PedidoRecorrenteCartaoCreditoDTO pedidoRecorrenteCartaoCredito);

		/// <summary>
		/// Método para buscar todas as recorrencias feitas de um pedido.
		/// Popular lojaId e pedidoId
		/// </summary>
		/// <param name="pedidoRecorrenteCartaoCredito"></param>
		/// <returns>IEnumerable<RecorrenciaDTO/></returns>
		RecorrenciasDTO RecuperarRecorrenciasDePedido(PedidoRecorrenteCartaoCreditoDTO pedidoRecorrenteCartaoCredito);
	}
}