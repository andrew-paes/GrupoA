using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrupoA.PaymentGateway
{
    /// <summary>
    /// Classe para manter coleção de Recorrencias e RetornoPedido das transações da Braspag.
    /// </summary>
    public class RecorrenciasDTO
    {
        /// <summary>
        /// Coleção de Recorrencias
        /// </summary>
        public virtual IEnumerable<RetornoPedidoRecorrenteDTO> Recorrencias{ get; set; }

        /// <summary>
        /// RetornoPedido das transacoes Braspag.
        /// </summary>
        public virtual RetornoPedidoDTO RetornoPedido { get; set; }

    }
}
