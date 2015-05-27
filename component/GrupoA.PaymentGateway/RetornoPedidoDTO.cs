using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrupoA.PaymentGateway
{
    /// <summary>
    /// Classe para guardar retorno das transações da Braspag.
    /// </summary>
    public class RetornoPedidoDTO
    {
        /// <summary>
        /// Valor total do pedido
        /// </summary>
        public virtual decimal? ValorTotalDoPedido { get; set; }

        /// <summary>
        /// Mensagem de retorno da transação
        /// </summary>
        public virtual string MensagemDeRetorno { get; set; }

        /// <summary>
        /// Número da autorização, caso tenha sido autorizado
        /// </summary>
        public virtual string NumeroDaAutorizacao { get; set; }

        /// <summary>
        /// Código de retorno da transação
        /// </summary>
        public virtual string CodigoDeRetorno { get; set; }

        /// <summary>
        /// Status do pedido no gateway de pagamento.
        /// </summary>
        public virtual string StatusDoPedido { get; set; }

        /// <summary>
        /// Número da transação na operadora
        /// </summary>
        public virtual string NumeroDaTransacaoNaOperadora { get; set; }

    }
}
