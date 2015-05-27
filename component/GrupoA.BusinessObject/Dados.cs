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
	public partial class Dados 
	{
		private int? _produtoId;
		private int? _categoriaId;
		private int? _seloId;
		private int? _lancamento;

		public int? ProdutoId {
			get { return _produtoId; }
			set { _produtoId = value; }
		}

		public int? CategoriaId {
			get { return _categoriaId; }
			set { _categoriaId = value; }
		}

		public int? SeloId {
			get { return _seloId; }
			set { _seloId = value; }
		}

		public int? Lancamento {
			get { return _lancamento; }
			set { _lancamento = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<Dados>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<Dados>(this);
        }
	}
	
	public struct DadosColunas
	{	
		public static string ProdutoId = @"ProdutoId";
		public static string CategoriaId = @"CategoriaId";
		public static string SeloId = @"SeloId";
		public static string Lancamento = @"Lancamento";
	}
}
		