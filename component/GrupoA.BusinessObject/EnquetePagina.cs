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
	public partial class EnquetePagina 
	{
		// Construtor
		public EnquetePagina() {}

		// Construtor com identificador
		public EnquetePagina(int enquetePaginaId) {
			_enquetePaginaId = enquetePaginaId;
		}

		private int _enquetePaginaId;
		private string _nomePagina;
		private List<Enquete> _enquetes;

		public int EnquetePaginaId {
			get { return _enquetePaginaId; }
			set { _enquetePaginaId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 100)]
		public string NomePagina {
			get { return _nomePagina; }
			set { _nomePagina = value; }
		}

		public List<Enquete> Enquetes {
			get { return _enquetes; }
			set { _enquetes = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<EnquetePagina>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<EnquetePagina>(this);
        }
	}
	
	public struct EnquetePaginaColunas
	{	
		public static string EnquetePaginaId = @"enquetePaginaId";
		public static string NomePagina = @"nomePagina";
	}
}
		