using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrupoA.PaymentGateway
{
    /// <summary>
    /// Classe que abstrai a recorrência.
    /// </summary>
    public class RetornoPedidoRecorrenteDTO : RetornoPedidoDTO
    {
        /// <summary>
        /// Número sequêncial da recorrência.
        /// </summary>
        public virtual int CodigoDaRecorrencia { get; set; }

        /// <summary>
        /// Número que confirma o cancelamento da recorrência.
        /// </summary>
        public virtual int Cancelado { get; set; }

        /// <summary>
        /// Número que confirma a alteração dos dados de cartão.
        /// </summary>
        public virtual int Alterado { get; set; }

        /// <summary>
        /// Número de tentativas.
        /// </summary>
        public virtual int NumeroDeTentativas { get; set; }

        /// <summary>
        /// Data em que a tentativa da recorrência foi realizada.
        /// </summary>
		public virtual DateTime DataDaTentativa { get; set; }
    }
}
