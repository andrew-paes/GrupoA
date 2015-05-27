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
	public partial class EventoImagem 
	{
		// Construtor
		public EventoImagem() {}

		// Construtor com identificador
		public EventoImagem(int eventoImagemId) {
			_eventoImagemId = eventoImagemId;
		}

		private int _eventoImagemId;
		private int _ordemApresentacao;
		private Arquivo _arquivo;
		private Evento _evento;

		public int EventoImagemId {
			get { return _eventoImagemId; }
			set { _eventoImagemId = value; }
		}

		public int OrdemApresentacao {
			get { return _ordemApresentacao; }
			set { _ordemApresentacao = value; }
		}

		[NotNullValidator]
		public Arquivo Arquivo {
			get { return _arquivo; }
			set { _arquivo = value; }
		}

		[NotNullValidator]
		public Evento Evento {
			get { return _evento; }
			set { _evento = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<EventoImagem>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<EventoImagem>(this);
        }
	}
	
	public struct EventoImagemColunas
	{	
		public static string EventoImagemId = @"eventoImagemId";
		public static string EventoId = @"eventoId";
		public static string ArquivoId = @"arquivoId";
		public static string OrdemApresentacao = @"ordemApresentacao";
	}
}
		