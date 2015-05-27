using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrupoA.PaymentGateway
{
    /// <summary>
    /// Classe para guardar retorno das transações da Braspag.
    /// </summary>
    public class RetornoCofreDTO
    {
        /// <summary>
        /// Mensagem de retorno da transação
        /// </summary>
        public virtual string MensagemDeRetorno { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Token { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string DataExpiracao { get; set; }
    }
}