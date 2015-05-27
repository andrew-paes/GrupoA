/*
'===============================================================================
'
'  Template: Gerador Código C#.csgen
'  Script versão: 0.96
'  Script criado por: Leonardo Alves Lindermann (lindermannla@ag2.com.br)
'  Gerado pelo MyGeneration versão # (???)
'
'===============================================================================
*/
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Text;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace GrupoA.BusinessObject
{	
	
	[Serializable]
	public partial class Pedido 
	{
		// Construtor
		public Pedido() {}

		// Construtor com identificador
		public Pedido(int pedidoId) {
			_pedidoId = pedidoId;
		}

		private int _pedidoId;
		private DateTime _dataHoraPedido;
		private decimal _freteValor;
		private decimal _valorPedido;
		private int? _pedidoCodigo;
		private PedidoCompraConjunta _pedidoCompraConjunta;
		private PedidoControle _pedidoControle;
		private PedidoEndereco _pedidoEndereco;
		private List<PedidoItem> _pedidoItens;
		private List<Promocao> _promocoes;
		private List<PedidoSituacao> _pedidoSituacoes;
		private Carrinho _carrinho;
		private Pagamento _pagamento;
		private PedidoStatus _pedidoStatus;
		private TransportadoraServico _transportadoraServico;
		private Usuario _usuario;

		public int PedidoId {
			get { return _pedidoId; }
			set { _pedidoId = value; }
		}

		[NotNullValidator]
		public DateTime DataHoraPedido {
			get { return _dataHoraPedido; }
			set { _dataHoraPedido = value; }
		}

		public decimal FreteValor {
			get { return _freteValor; }
			set { _freteValor = value; }
		}

		public decimal ValorPedido {
			get { return _valorPedido; }
			set { _valorPedido = value; }
		}

		public int? PedidoCodigo {
			get { return _pedidoCodigo; }
			set { _pedidoCodigo = value; }
		}

		[NotNullValidator]
		public PedidoCompraConjunta PedidoCompraConjunta {
			get { return _pedidoCompraConjunta; }
			set { _pedidoCompraConjunta = value; }
		}

		[NotNullValidator]
		public PedidoControle PedidoControle {
			get { return _pedidoControle; }
			set { _pedidoControle = value; }
		}

		[NotNullValidator]
		public PedidoEndereco PedidoEndereco {
			get { return _pedidoEndereco; }
			set { _pedidoEndereco = value; }
		}

		public List<PedidoItem> PedidoItens {
			get { return _pedidoItens; }
			set { _pedidoItens = value; }
		}

		public List<Promocao> Promocoes {
			get { return _promocoes; }
			set { _promocoes = value; }
		}

		public List<PedidoSituacao> PedidoSituacoes {
			get { return _pedidoSituacoes; }
			set { _pedidoSituacoes = value; }
		}

		public Carrinho Carrinho {
			get { return _carrinho; }
			set { _carrinho = value; }
		}

		[NotNullValidator]
		public Pagamento Pagamento {
			get { return _pagamento; }
			set { _pagamento = value; }
		}

		[NotNullValidator]
		public PedidoStatus PedidoStatus {
			get { return _pedidoStatus; }
			set { _pedidoStatus = value; }
		}

		[NotNullValidator]
		public TransportadoraServico TransportadoraServico {
			get { return _transportadoraServico; }
			set { _transportadoraServico = value; }
		}

		[NotNullValidator]
		public Usuario Usuario {
			get { return _usuario; }
			set { _usuario = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<Pedido>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<Pedido>(this);
        }
	}
	
	public struct PedidoColunas
	{	
		public static string PedidoId = @"pedidoId";
		public static string UsuarioId = @"usuarioId";
		public static string DataHoraPedido = @"dataHoraPedido";
		public static string CarrinhoId = @"carrinhoId";
		public static string PedidoStatusId = @"pedidoStatusId";
		public static string FreteValor = @"freteValor";
		public static string ValorPedido = @"valorPedido";
		public static string PagamentoId = @"pagamentoId";
		public static string TransportadoraServicoId = @"transportadoraServicoId";
		public static string PedidoCodigo = @"pedidoCodigo";
	}
}
		