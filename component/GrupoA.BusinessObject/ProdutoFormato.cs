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
	public partial class ProdutoFormato 
	{
		private int _produtoId;
		private string _formato;
		private Produto _produto;

		public int ProdutoId {
			get { return _produtoId; }
			set { _produtoId = value; }
		}

		public string Formato {
			get { return _formato; }
			set { _formato = value; }
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
            get { return Validation.Validate<ProdutoFormato>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<ProdutoFormato>(this);
        }
	}
	
	public struct ProdutoFormatoColunas
	{	
		public static string ProdutoId = @"produtoId";
		public static string Formato = @"formato";
	}
}
		