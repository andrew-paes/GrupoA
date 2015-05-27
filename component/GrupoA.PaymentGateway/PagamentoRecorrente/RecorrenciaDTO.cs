using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrupoA.PaymentGateway
{
    /// <summary>
    /// Classe que abstrai a recorrência.
    /// </summary>
    public class RetornoRecorrenciaDTO
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
        /// Número sequêncial da recorrência.
        /// </summary>
        public virtual int NumeroDeRecorrencias { get; set; }

        /// <summary>
        /// Número de tentativas.
        /// </summary>
        public virtual int NumeroDeTentativas { get; set; }

        /// <summary>
        /// Meio de pagamento.
        /// </summary>
        public virtual MeioPagamentoRecorrente MeioDePagamento { get; set; }

        /// <summary>
        /// Meio de pagamento.
        /// </summary>
        public virtual string TipoPagamento { get; set; }

        /// <summary>
        /// Cartão de crédito relacionado ao pagamento.
        /// </summary>
        public virtual CartaoCreditoDTO CartaoDeCredito { get; set; }

        /// <summary>
        /// Valor total.
        /// </summary>
        public virtual decimal ValorTotal { get; set; }

        /// <summary>
        /// Data em que a tentativa da recorrência foi realizada.
        /// </summary>
        public virtual DateTime DataDeRecorrencia { get; set; }

        /// <summary>
        /// Mensagem recebida do adquirente ao processar o pagamento.
        /// </summary>
        public virtual string MensagemDeRetornoDoProcessamento { get; set; }

        /// <summary>
        /// Status da recorrência.
        /// </summary>
        public virtual string StatusDaRecorrencia { get; set; }

    }
}
