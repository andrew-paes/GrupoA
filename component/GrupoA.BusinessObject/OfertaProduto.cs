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
	public partial class OfertaProduto 
	{
		// Construtor
		public OfertaProduto() {}

		// Construtor com identificador
		public OfertaProduto(int ofertaProdutoId) {
			_ofertaProdutoId = ofertaProdutoId;
		}

		private int _ofertaProdutoId;
		private Oferta _oferta;
		private Produto _produto;

		public int OfertaProdutoId {
			get { return _ofertaProdutoId; }
			set { _ofertaProdutoId = value; }
		}

		[NotNullValidator]
		public Oferta Oferta {
			get { return _oferta; }
			set { _oferta = value; }
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
            get { return Validation.Validate<OfertaProduto>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<OfertaProduto>(this);
        }
	}
	
	public struct OfertaProdutoColunas
	{	
		public static string OfertaProdutoId = @"ofertaProdutoId";
		public static string OfertaId = @"ofertaId";
		public static string ProdutoId = @"produtoId";
	}
}
		