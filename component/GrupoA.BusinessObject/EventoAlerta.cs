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
	public partial class EventoAlerta 
	{
		// Construtor
		public EventoAlerta() {}

		// Construtor com identificador
		public EventoAlerta(int eventoAlertaId) {
			_eventoAlertaId = eventoAlertaId;
		}

		private int _eventoAlertaId;
		private int _dias;
		private bool _ativo;
		private DateTime? _dataHoraEncaminhamento;
		private DateTime? _dataHoraCancelamento;
		private Evento _evento;
		private Usuario _usuario;

		public int EventoAlertaId {
			get { return _eventoAlertaId; }
			set { _eventoAlertaId = value; }
		}

		public int Dias {
			get { return _dias; }
			set { _dias = value; }
		}

		public bool Ativo {
			get { return _ativo; }
			set { _ativo = value; }
		}

		public DateTime? DataHoraEncaminhamento {
			get { return _dataHoraEncaminhamento; }
			set { _dataHoraEncaminhamento = value; }
		}

		public DateTime? DataHoraCancelamento {
			get { return _dataHoraCancelamento; }
			set { _dataHoraCancelamento = value; }
		}

		[NotNullValidator]
		public Evento Evento {
			get { return _evento; }
			set { _evento = value; }
		}

		[NotNullValidator]
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
            get { return Validation.Validate<EventoAlerta>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<EventoAlerta>(this);
        }
	}
	
	public struct EventoAlertaColunas
	{	
		public static string EventoAlertaId = @"eventoAlertaId";
		public static string UsuarioId = @"usuarioId";
		public static string EventoId = @"eventoId";
		public static string Dias = @"dias";
		public static string Ativo = @"ativo";
		public static string DataHoraEncaminhamento = @"dataHoraEncaminhamento";
		public static string DataHoraCancelamento = @"dataHoraCancelamento";
	}
}
		