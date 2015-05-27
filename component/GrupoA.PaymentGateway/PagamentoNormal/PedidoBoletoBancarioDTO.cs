using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrupoA.PaymentGateway
{
    public class PedidoBoletoBancarioDTO : PedidoCartaoCreditoDTO
    {
		/// <summary>
		/// Data de vencimento do boleto.
		/// </summary>
		public virtual DateTime DataDeVencimento { get; set; }

        /// <summary>
        /// Número do boleto.
        /// </summary>
        public virtual String NumeroDoBoleto { get; set; }

        /// <summary>
        /// Status do boleto.
        /// </summary>
        public virtual Int32 Status { get; set; }
    }
}
