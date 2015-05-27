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
	public partial class ProdutoTipo 
	{
		// Construtor
		public ProdutoTipo() {}

		// Construtor com identificador
		public ProdutoTipo(int produtoTipoId) {
			_produtoTipoId = produtoTipoId;
		}

		private int _produtoTipoId;
		private string _tipo;
		private List<Produto> _produtos;
		private List<Promocao> _promocoes;

		public int ProdutoTipoId {
			get { return _produtoTipoId; }
			set { _produtoTipoId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 100)]
		public string Tipo {
			get { return _tipo; }
			set { _tipo = value; }
		}

		public List<Produto> Produtos {
			get { return _produtos; }
			set { _produtos = value; }
		}

		public List<Promocao> Promocoes {
			get { return _promocoes; }
			set { _promocoes = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<ProdutoTipo>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<ProdutoTipo>(this);
        }
	}
	
	public struct ProdutoTipoColunas
	{	
		public static string ProdutoTipoId = @"produtoTipoId";
		public static string Tipo = @"tipo";
	}
}
		