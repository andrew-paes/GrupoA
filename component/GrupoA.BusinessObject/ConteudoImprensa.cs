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
	public partial class ConteudoImprensa 
	{
		private int _conteudoImprensaId;
		private string _fonte;
		private string _fonteUrl;
		private bool _ativo;
		private DateTime? _dataExibicaoInicio;
		private DateTime? _dataExibicaoFim;
		private string _resumo;
		private string _texto;
		private bool _destaque;
		private string _titulo;
		private Clipping _clipping;
		private Evento _evento;
		private Noticia _noticia;
		private Conteudo _conteudo;

		public int ConteudoImprensaId {
			get { return _conteudoImprensaId; }
			set { _conteudoImprensaId = value; }
		}

		public string Fonte {
			get { return _fonte; }
			set { _fonte = value; }
		}

		public string FonteUrl {
			get { return _fonteUrl; }
			set { _fonteUrl = value; }
		}

		public bool Ativo {
			get { return _ativo; }
			set { _ativo = value; }
		}

		public DateTime? DataExibicaoInicio {
			get { return _dataExibicaoInicio; }
			set { _dataExibicaoInicio = value; }
		}

		public DateTime? DataExibicaoFim {
			get { return _dataExibicaoFim; }
			set { _dataExibicaoFim = value; }
		}

		public string Resumo {
			get { return _resumo; }
			set { _resumo = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 1073741823)]
		public string Texto {
			get { return _texto; }
			set { _texto = value; }
		}

		public bool Destaque {
			get { return _destaque; }
			set { _destaque = value; }
		}

		public string Titulo {
			get { return _titulo; }
			set { _titulo = value; }
		}

		[NotNullValidator]
		public Clipping Clipping {
			get { return _clipping; }
			set { _clipping = value; }
		}

		[NotNullValidator]
		public Evento Evento {
			get { return _evento; }
			set { _evento = value; }
		}

		[NotNullValidator]
		public Noticia Noticia {
			get { return _noticia; }
			set { _noticia = value; }
		}

		[NotNullValidator]
		public Conteudo Conteudo {
			get { return _conteudo; }
			set { _conteudo = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<ConteudoImprensa>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<ConteudoImprensa>(this);
        }
	}
	
	public struct ConteudoImprensaColunas
	{	
		public static string ConteudoImprensaId = @"conteudoImprensaId";
		public static string Fonte = @"fonte";
		public static string FonteUrl = @"fonteUrl";
		public static string Ativo = @"ativo";
		public static string DataExibicaoInicio = @"dataExibicaoInicio";
		public static string DataExibicaoFim = @"dataExibicaoFim";
		public static string Resumo = @"resumo";
		public static string Texto = @"texto";
		public static string Destaque = @"destaque";
		public static string Titulo = @"titulo";
	}
}
		