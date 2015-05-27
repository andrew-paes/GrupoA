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
	public partial class ProdutoImagem 
	{
		// Construtor
		public ProdutoImagem() {}

		// Construtor com identificador
		public ProdutoImagem(int produtoImagemId) {
			_produtoImagemId = produtoImagemId;
		}

		private int _produtoImagemId;
		private Arquivo _arquivo;
		private Produto _produto;
		private ProdutoImagemTipo _produtoImagemTipo;

		public int ProdutoImagemId {
			get { return _produtoImagemId; }
			set { _produtoImagemId = value; }
		}

		[NotNullValidator]
		public Arquivo Arquivo {
			get { return _arquivo; }
			set { _arquivo = value; }
		}

		[NotNullValidator]
		public Produto Produto {
			get { return _produto; }
			set { _produto = value; }
		}

		[NotNullValidator]
		public ProdutoImagemTipo ProdutoImagemTipo {
			get { return _produtoImagemTipo; }
			set { _produtoImagemTipo = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<ProdutoImagem>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<ProdutoImagem>(this);
        }
	}
	
	public struct ProdutoImagemColunas
	{	
		public static string ProdutoImagemId = @"produtoImagemId";
		public static string ArquivoId = @"arquivoId";
		public static string ProdutoId = @"produtoId";
		public static string ProdutoImagemTipoId = @"produtoImagemTipoId";
	}
}
		