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
	public partial class Evento 
	{
		private int _eventoId;
		private DateTime? _dataEventoInicio;
		private DateTime? _dataEventoFim;
		private string _local;
		private bool _exibeFormularioContato;
		private List<EventoAlerta> _eventoAlertas;
		private List<EventoImagem> _eventoImagens;
		private Arquivo _arquivoThumb;
		private ConteudoImprensa _conteudoImprensa;

		public int EventoId {
			get { return _eventoId; }
			set { _eventoId = value; }
		}

		public DateTime? DataEventoInicio {
			get { return _dataEventoInicio; }
			set { _dataEventoInicio = value; }
		}

		public DateTime? DataEventoFim {
			get { return _dataEventoFim; }
			set { _dataEventoFim = value; }
		}

		public string Local {
			get { return _local; }
			set { _local = value; }
		}

		public bool ExibeFormularioContato {
			get { return _exibeFormularioContato; }
			set { _exibeFormularioContato = value; }
		}

		public List<EventoAlerta> EventoAlertas {
			get { return _eventoAlertas; }
			set { _eventoAlertas = value; }
		}

		public List<EventoImagem> EventoImagens {
			get { return _eventoImagens; }
			set { _eventoImagens = value; }
		}

		public Arquivo ArquivoThumb {
			get { return _arquivoThumb; }
			set { _arquivoThumb = value; }
		}

		[NotNullValidator]
		public ConteudoImprensa ConteudoImprensa {
			get { return _conteudoImprensa; }
			set { _conteudoImprensa = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<Evento>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<Evento>(this);
        }
	}
	
	public struct EventoColunas
	{	
		public static string EventoId = @"eventoId";
		public static string DataEventoInicio = @"dataEventoInicio";
		public static string DataEventoFim = @"dataEventoFim";
		public static string Local = @"local";
		public static string ArquivoIdThumb = @"arquivoIdThumb";
		public static string ExibeFormularioContato = @"exibeFormularioContato";
	}
}
		