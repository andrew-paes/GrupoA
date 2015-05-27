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
	public partial class ProdutoImagemTipo 
	{
		// Construtor
		public ProdutoImagemTipo() {}

		// Construtor com identificador
		public ProdutoImagemTipo(int produtoImagemTipoId) {
			_produtoImagemTipoId = produtoImagemTipoId;
		}

		private int _produtoImagemTipoId;
		private string _tipoImagem;
		private List<ProdutoImagem> _produtoImagens;

		public int ProdutoImagemTipoId {
			get { return _produtoImagemTipoId; }
			set { _produtoImagemTipoId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 100)]
		public string TipoImagem {
			get { return _tipoImagem; }
			set { _tipoImagem = value; }
		}

		public List<ProdutoImagem> ProdutoImagens {
			get { return _produtoImagens; }
			set { _produtoImagens = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<ProdutoImagemTipo>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<ProdutoImagemTipo>(this);
        }
	}
	
	public struct ProdutoImagemTipoColunas
	{	
		public static string ProdutoImagemTipoId = @"produtoImagemTipoId";
		public static string TipoImagem = @"tipoImagem";
	}
}
		