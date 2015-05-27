using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrupoA.PaymentGateway
{
    public class PedidoCartaoCreditoDTO
    {
		/// <summary>
		/// Identificador da loja no gateway
		/// </summary>
		public virtual string IdentificadorDaLojaNoGateway { get; set; }

		/// <summary>
		/// Identificador único do pedido.
		/// </summary>
        public virtual string CodigoDoPedido { get; set; }

		/// <summary>
		/// Valor total do pedido.
		/// </summary>
        public virtual decimal ValorTotalDoPedido { get; set; }

		/// <summary>
		/// Meio de pagamento.
		/// </summary>
        public virtual string MeioDePagamento { get; set; }

        /// <summary>
        /// Cartão de crédito relacionado ao pagamento.
        /// </summary>
        public virtual CartaoCreditoDTO CartaoDeCredito { get; set; }

        /// <summary>
        /// Cliente relacionado ao pedido.
        /// </summary>
        public virtual ClienteDTO Cliente { get; set; }

		/// <summary>
		/// Número de parcelas
		/// </summary>
        public virtual int NumeroDeParcelas { get; set; }

		/// <summary>
		/// Tipo de pagamento
		/// </summary>
        public virtual string TipoDePagamento { get; set; }

		/// <summary>
		/// Valor da Taxa de Serviço (ou taxa de Embarque no caso de pagamento com IATA) no formato brasileiro: 0.000,00
		/// </summary>
        public virtual string TaxaDeServico { get; set; }

        /// <summary>
        /// Token usado pelo cofre
        /// </summary>
        public virtual string Token { get; set; }
    }
}
