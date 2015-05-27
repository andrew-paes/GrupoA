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
	public partial class LogOcorrencia 
	{
		// Construtor
		public LogOcorrencia() {}

		// Construtor com identificador
		public LogOcorrencia(int logOcorrenciaId) {
			_logOcorrenciaId = logOcorrenciaId;
		}

		private int _logOcorrenciaId;
		private DateTime _dataHoraOcorrencia;
		private XDocument _dados;
		private LogEvento _logEvento;
		private Usuario _usuario;

		public int LogOcorrenciaId {
			get { return _logOcorrenciaId; }
			set { _logOcorrenciaId = value; }
		}

		[NotNullValidator]
		public DateTime DataHoraOcorrencia {
			get { return _dataHoraOcorrencia; }
			set { _dataHoraOcorrencia = value; }
		}

		[NotNullValidator]
		public XDocument Dados {
			get { return _dados; }
			set { _dados = value; }
		}

		[NotNullValidator]
		public LogEvento LogEvento {
			get { return _logEvento; }
			set { _logEvento = value; }
		}

		public Usuario Usuario {
			get { return _usuario; }
			set { _usuario = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<LogOcorrencia>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<LogOcorrencia>(this);
        }
	}
	
	public struct LogOcorrenciaColunas
	{	
		public static string LogOcorrenciaId = @"logOcorrenciaId";
		public static string LogEventoId = @"logEventoId";
		public static string DataHoraOcorrencia = @"dataHoraOcorrencia";
		public static string UsuarioId = @"usuarioId";
		public static string Dados = @"dados";
	}
}
		