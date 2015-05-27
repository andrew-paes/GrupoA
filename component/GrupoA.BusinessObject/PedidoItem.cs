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
	public partial class PedidoItem 
	{
		// Construtor
		public PedidoItem() {}

		// Construtor com identificador
		public PedidoItem(int pedidoItemId) {
			_pedidoItemId = pedidoItemId;
		}

		private int _pedidoItemId;
		private decimal _quantidade;
		private decimal _valorUnitarioBase;
		private decimal _valorUnitarioFinal;
		private PedidoItemCompraConjunta _pedidoItemCompraConjunta;
		private PedidoItemPromocao _pedidoItemPromocao;
		private Pedido _pedido;
		private Produto _produto;

		public int PedidoItemId {
			get { return _pedidoItemId; }
			set { _pedidoItemId = value; }
		}

		public decimal Quantidade {
			get { return _quantidade; }
			set { _quantidade = value; }
		}

		public decimal ValorUnitarioBase {
			get { return _valorUnitarioBase; }
			set { _valorUnitarioBase = value; }
		}

		public decimal ValorUnitarioFinal {
			get { return _valorUnitarioFinal; }
			set { _valorUnitarioFinal = value; }
		}

		[NotNullValidator]
		public PedidoItemCompraConjunta PedidoItemCompraConjunta {
			get { return _pedidoItemCompraConjunta; }
			set { _pedidoItemCompraConjunta = value; }
		}

		[NotNullValidator]
		public PedidoItemPromocao PedidoItemPromocao {
			get { return _pedidoItemPromocao; }
			set { _pedidoItemPromocao = value; }
		}

		[NotNullValidator]
		public Pedido Pedido {
			get { return _pedido; }
			set { _pedido = value; }
		}

		[NotNullValidator]
		public Produto Produto {
			get { return _produto; }
			set { _produto = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<PedidoItem>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<PedidoItem>(this);
        }
	}
	
	public struct PedidoItemColunas
	{	
		public static string PedidoItemId = @"pedidoItemId";
		public static string ProdutoId = @"produtoId";
		public static string PedidoId = @"pedidoId";
		public static string Quantidade = @"quantidade";
		public static string ValorUnitarioBase = @"valorUnitarioBase";
		public static string ValorUnitarioFinal = @"valorUnitarioFinal";
	}
}
		