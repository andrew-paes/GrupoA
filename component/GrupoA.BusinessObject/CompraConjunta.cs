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
	public partial class CompraConjunta 
	{
		// Construtor
		public CompraConjunta() {}

		// Construtor com identificador
		public CompraConjunta(int compraConjuntaId) {
			_compraConjuntaId = compraConjuntaId;
		}

		private int _compraConjuntaId;
		private DateTime _dataInicialCompra;
		private DateTime _dataFinalCompra;
		private int _estoqueSeguranca;
		private DateTime? _dataHoraFinalizacao;
		private bool _ativa;
		private int? _quantidadeLimite;
		private List<CarrinhoItemCompraConjunta> _carrinhoItemCompraConjuntas;
		private List<CompraConjuntaDesconto> _compraConjuntaDescontos;
		private List<CompraConjuntaPagina> _compraConjuntaPaginas;
		private List<PedidoCompraConjunta> _pedidoCompraConjuntas;
		private List<PedidoItemCompraConjunta> _pedidoItemCompraConjuntas;
		private CompraConjuntaStatus _compraConjuntaStatus;
		private Produto _produto;

		public int CompraConjuntaId {
			get { return _compraConjuntaId; }
			set { _compraConjuntaId = value; }
		}

		[NotNullValidator]
		public DateTime DataInicialCompra {
			get { return _dataInicialCompra; }
			set { _dataInicialCompra = value; }
		}

		[NotNullValidator]
		public DateTime DataFinalCompra {
			get { return _dataFinalCompra; }
			set { _dataFinalCompra = value; }
		}

		public int EstoqueSeguranca {
			get { return _estoqueSeguranca; }
			set { _estoqueSeguranca = value; }
		}

		public DateTime? DataHoraFinalizacao {
			get { return _dataHoraFinalizacao; }
			set { _dataHoraFinalizacao = value; }
		}

		public bool Ativa {
			get { return _ativa; }
			set { _ativa = value; }
		}

		public int? QuantidadeLimite {
			get { return _quantidadeLimite; }
			set { _quantidadeLimite = value; }
		}

		public List<CarrinhoItemCompraConjunta> CarrinhoItemCompraConjuntas {
			get { return _carrinhoItemCompraConjuntas; }
			set { _carrinhoItemCompraConjuntas = value; }
		}

		public List<CompraConjuntaDesconto> CompraConjuntaDescontos {
			get { return _compraConjuntaDescontos; }
			set { _compraConjuntaDescontos = value; }
		}

		public List<CompraConjuntaPagina> CompraConjuntaPaginas {
			get { return _compraConjuntaPaginas; }
			set { _compraConjuntaPaginas = value; }
		}

		public List<PedidoCompraConjunta> PedidoCompraConjuntas {
			get { return _pedidoCompraConjuntas; }
			set { _pedidoCompraConjuntas = value; }
		}

		public List<PedidoItemCompraConjunta> PedidoItemCompraConjuntas {
			get { return _pedidoItemCompraConjuntas; }
			set { _pedidoItemCompraConjuntas = value; }
		}

		[NotNullValidator]
		public CompraConjuntaStatus CompraConjuntaStatus {
			get { return _compraConjuntaStatus; }
			set { _compraConjuntaStatus = value; }
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
            get { return Validation.Validate<CompraConjunta>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<CompraConjunta>(this);
        }
	}
	
	public struct CompraConjuntaColunas
	{	
		public static string CompraConjuntaId = @"compraConjuntaId";
		public static string ProdutoId = @"produtoId";
		public static string DataInicialCompra = @"dataInicialCompra";
		public static string DataFinalCompra = @"dataFinalCompra";
		public static string EstoqueSeguranca = @"estoqueSeguranca";
		public static string DataHoraFinalizacao = @"dataHoraFinalizacao";
		public static string Ativa = @"ativa";
		public static string CompraConjuntaStatusId = @"compraConjuntaStatusId";
		public static string QuantidadeLimite = @"quantidadeLimite";
	}
}
		