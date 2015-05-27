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
	public partial class CarrinhoItem 
	{
		// Construtor
		public CarrinhoItem() {}

		// Construtor com identificador
		public CarrinhoItem(int carrinhoItemId) {
			_carrinhoItemId = carrinhoItemId;
		}

		private int _carrinhoItemId;
		private decimal _quantidade;
		private CarrinhoItemCompraConjunta _carrinhoItemCompraConjunta;
		private Carrinho _carrinho;
		private Produto _produto;

		public int CarrinhoItemId {
			get { return _carrinhoItemId; }
			set { _carrinhoItemId = value; }
		}

		public decimal Quantidade {
			get { return _quantidade; }
			set { _quantidade = value; }
		}

		[NotNullValidator]
		public CarrinhoItemCompraConjunta CarrinhoItemCompraConjunta {
			get { return _carrinhoItemCompraConjunta; }
			set { _carrinhoItemCompraConjunta = value; }
		}

		[NotNullValidator]
		public Carrinho Carrinho {
			get { return _carrinho; }
			set { _carrinho = value; }
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
            get { return Validation.Validate<CarrinhoItem>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<CarrinhoItem>(this);
        }
	}
	
	public struct CarrinhoItemColunas
	{	
		public static string CarrinhoItemId = @"carrinhoItemId";
		public static string CarrinhoId = @"carrinhoId";
		public static string ProdutoId = @"produtoId";
		public static string Quantidade = @"quantidade";
	}
}
		