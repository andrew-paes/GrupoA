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
	public partial class LogCategoria 
	{
		// Construtor
		public LogCategoria() {}

		// Construtor com identificador
		public LogCategoria(int logCategoriaId) {
			_logCategoriaId = logCategoriaId;
		}

		private int _logCategoriaId;
		private string _categoria;
		private List<LogEvento> _logEventos;

		public int LogCategoriaId {
			get { return _logCategoriaId; }
			set { _logCategoriaId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 100)]
		public string Categoria {
			get { return _categoria; }
			set { _categoria = value; }
		}

		public List<LogEvento> LogEventos {
			get { return _logEventos; }
			set { _logEventos = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<LogCategoria>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<LogCategoria>(this);
        }
	}
	
	public struct LogCategoriaColunas
	{	
		public static string LogCategoriaId = @"logCategoriaId";
		public static string Categoria = @"categoria";
	}
}
		