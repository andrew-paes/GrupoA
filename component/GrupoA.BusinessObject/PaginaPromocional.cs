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
	public partial class PaginaPromocional 
	{
		// Construtor
		public PaginaPromocional() {}

		// Construtor com identificador
		public PaginaPromocional(int paginaPromocionalId) {
			_paginaPromocionalId = paginaPromocionalId;
		}

		private int _paginaPromocionalId;
		private string _nomePagina;
        private string _tituloPagina;
		private string _subtituloPagina;
		private string _resumo;
		private string _linkMidia;
		private Arquivo _arquivo;
        private int? _larguraArquivo;
        private int? _alturaArquivo;
        private bool _ativo;
        private bool _targetBlank;

		public int PaginaPromocionalId {
			get { return _paginaPromocionalId; }
			set { _paginaPromocionalId = value; }
		}

        [NotNullValidator]
        [StringLengthValidator(0, 100)]
        public string NomePagina
        {
            get { return _nomePagina; }
            set { _nomePagina = value; }
        }

		[NotNullValidator]
		[StringLengthValidator(0, 1073741823)]
		public string TituloPagina {
			get { return _tituloPagina; }
			set { _tituloPagina = value; }
		}

		public string SubtituloPagina {
			get { return _subtituloPagina; }
			set { _subtituloPagina = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 1073741823)]
		public string Resumo {
			get { return _resumo; }
			set { _resumo = value; }
		}

		public string LinkMidia {
			get { return _linkMidia; }
			set { _linkMidia = value; }
		}

		public Arquivo Arquivo {
			get { return _arquivo; }
			set { _arquivo = value; }
		}

        public int? LarguraArquivo
        {
            get { return _larguraArquivo; }
            set { _larguraArquivo = value; }
        }

        public int? AlturaArquivo
        {
            get { return _alturaArquivo; }
            set { _alturaArquivo = value; }
        }

        public bool Ativo
        {
            get { return _ativo; }
            set { _ativo = value; }
        }

        public bool TargetBlank
        {
            get { return _targetBlank; }
            set { _targetBlank = value; }
        }

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<PaginaPromocional>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<PaginaPromocional>(this);
        }
	}
	
	public struct PaginaPromocionalColunas
	{	
		public static string PaginaPromocionalId = @"paginaPromocionalId";
		public static string NomePagina = @"nomePagina";
        public static string TituloPagina = @"tituloPagina";
		public static string SubtituloPagina = @"subtituloPagina";
		public static string Resumo = @"resumo";
		public static string LinkMidia = @"linkMidia";
		public static string ArquivoId = @"arquivoId";
        public static string LarguraArquivo = @"larguraArquivo";
        public static string AlturaArquivo = @"alturaArquivo";
        public static string Ativo = @"ativo";
        public static string TargetBlank = @"targetBlank";
	}
}
		