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
	public partial class LogEvento 
	{
		// Construtor
		public LogEvento() {}

		// Construtor com identificador
		public LogEvento(int logEventoId) {
			_logEventoId = logEventoId;
		}

		private int _logEventoId;
		private string _evento;
		private string _descricaoEvento;
		private List<LogOcorrencia> _logOcorrencias;
		private LogCategoria _logCategoria;

		public int LogEventoId {
			get { return _logEventoId; }
			set { _logEventoId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 100)]
		public string Evento {
			get { return _evento; }
			set { _evento = value; }
		}

		public string DescricaoEvento {
			get { return _descricaoEvento; }
			set { _descricaoEvento = value; }
		}

		public List<LogOcorrencia> LogOcorrencias {
			get { return _logOcorrencias; }
			set { _logOcorrencias = value; }
		}

		[NotNullValidator]
		public LogCategoria LogCategoria {
			get { return _logCategoria; }
			set { _logCategoria = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<LogEvento>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<LogEvento>(this);
        }
	}
	
	public struct LogEventoColunas
	{	
		public static string LogEventoId = @"logEventoId";
		public static string LogCategoriaId = @"logCategoriaId";
		public static string Evento = @"evento";
		public static string DescricaoEvento = @"descricaoEvento";
	}
}
		