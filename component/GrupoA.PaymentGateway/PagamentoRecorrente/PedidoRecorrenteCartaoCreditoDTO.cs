using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrupoA.PaymentGateway
{

    /// <summary>
    /// Classe que abstrai os dados de transferência para envio de pedido recorrente.
    /// </summary>
	public class PedidoRecorrenteCartaoCreditoDTO : PedidoCartaoCreditoDTO
    {
		/// <summary>
		/// Intervalo de recorrência (ex: 1 = mensal; 3 = trimestral; 6 = semestral; 12 = anual).
		/// </summary>
		public int IntervaloDaRecorrencia { get; set; }

		/// <summary>
		/// Data do primeiro débito (dd/mm/aaaa) (hoje, se vazio)
		/// </summary>
		public DateTime? DataInicialDaRecorrencia { get; set; }

		/// <summary>
		/// Data do último débito (dd/mm/aaaa) (para sempre, se vazio)
		/// </summary>
		public DateTime? DataFinalDaRecorrencia { get; set; }

		/// <summary>
		/// Dia de pagamento
		/// </summary>
		public int DiaDoPagamento { get; set; }

		/// <summary>
		/// Data do Pedido.
		/// </summary>
		public string DataDoPedido { get; set; }

    }
}
