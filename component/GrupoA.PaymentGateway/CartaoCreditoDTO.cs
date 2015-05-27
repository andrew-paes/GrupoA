using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrupoA.PaymentGateway
{
    /// <summary>
    /// Classe que abstrai os dados do cartão de crédito.
    /// </summary>
    public class CartaoCreditoDTO
    {
        /// <summary>
        /// Nome do cliente no cartão de crédito
        /// </summary>
        public virtual string NomeDoClienteNoCartaoDeCredito { get; set; }

        /// <summary>
        /// Número do cartão de crédito
        /// </summary>
        public virtual string NumeroDoCartaoDeCredito { get; set; }

        /// <summary>
        /// Código de segurança do cartão
        /// </summary>
        public virtual string CodigoDeSegurancaDoCartao { get; set; }

        /// <summary>
        /// Data de validade do cartão.
        /// </summary>
        public virtual string Expiracao { get; set; }
    }
}
