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
	public partial class CompraConjuntaDesconto 
	{
		// Construtor
		public CompraConjuntaDesconto() {}

		// Construtor com identificador
		public CompraConjuntaDesconto(int compraConjuntaDescontoId) {
			_compraConjuntaDescontoId = compraConjuntaDescontoId;
		}

		private int _compraConjuntaDescontoId;
		private int _quantidadeMinima;
		private decimal _percentualDesconto;
		private List<PedidoCompraConjunta> _pedidoCompraConjuntas;
		private CompraConjunta _compraConjunta;

		public int CompraConjuntaDescontoId {
			get { return _compraConjuntaDescontoId; }
			set { _compraConjuntaDescontoId = value; }
		}

		public int QuantidadeMinima {
			get { return _quantidadeMinima; }
			set { _quantidadeMinima = value; }
		}

		public decimal PercentualDesconto {
			get { return _percentualDesconto; }
			set { _percentualDesconto = value; }
		}

		public List<PedidoCompraConjunta> PedidoCompraConjuntas {
			get { return _pedidoCompraConjuntas; }
			set { _pedidoCompraConjuntas = value; }
		}

		[NotNullValidator]
		public CompraConjunta CompraConjunta {
			get { return _compraConjunta; }
			set { _compraConjunta = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<CompraConjuntaDesconto>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<CompraConjuntaDesconto>(this);
        }
	}
	
	public struct CompraConjuntaDescontoColunas
	{	
		public static string CompraConjuntaDescontoId = @"compraConjuntaDescontoId";
		public static string QuantidadeMinima = @"quantidadeMinima";
		public static string PercentualDesconto = @"percentualDesconto";
		public static string CompraConjuntaId = @"compraConjuntaId";
	}
}
		