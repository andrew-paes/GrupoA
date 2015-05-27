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
	public partial class ProgramaAtualizacaoChamada 
	{
		// Construtor
		public ProgramaAtualizacaoChamada() {}

		// Construtor com identificador
		public ProgramaAtualizacaoChamada(int programaAtualizacaoChamadaId) {
			_programaAtualizacaoChamadaId = programaAtualizacaoChamadaId;
		}

		private int _programaAtualizacaoChamadaId;
		private bool _ativo;
		private string _primeiraChamadaTitulo;
		private string _primeiraChamadaTexto;
		private string _primeiraChamadaUrl;
		private bool _primeiraChamadaTargetBlank;
		private string _segundaChamadaTitulo;
		private string _segundaChamadaTexto;
		private string _segundaChamadaUrl;
		private bool? _segundaChamadaTargetBlank;
		private List<ProgramaAtualizacaoPagina> _programaAtualizacaoPaginas;
		private Arquivo _arquivoImagem;

		public int ProgramaAtualizacaoChamadaId {
			get { return _programaAtualizacaoChamadaId; }
			set { _programaAtualizacaoChamadaId = value; }
		}

		public bool Ativo {
			get { return _ativo; }
			set { _ativo = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 100)]
		public string PrimeiraChamadaTitulo {
			get { return _primeiraChamadaTitulo; }
			set { _primeiraChamadaTitulo = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 500)]
		public string PrimeiraChamadaTexto {
			get { return _primeiraChamadaTexto; }
			set { _primeiraChamadaTexto = value; }
		}

		public string PrimeiraChamadaUrl {
			get { return _primeiraChamadaUrl; }
			set { _primeiraChamadaUrl = value; }
		}

		public bool PrimeiraChamadaTargetBlank {
			get { return _primeiraChamadaTargetBlank; }
			set { _primeiraChamadaTargetBlank = value; }
		}

		[StringLengthValidator(0, 100)]
		public string SegundaChamadaTitulo {
			get { return _segundaChamadaTitulo; }
			set { _segundaChamadaTitulo = value; }
		}

		[StringLengthValidator(0, 200)]
		public string SegundaChamadaTexto {
			get { return _segundaChamadaTexto; }
			set { _segundaChamadaTexto = value; }
		}

		public string SegundaChamadaUrl {
			get { return _segundaChamadaUrl; }
			set { _segundaChamadaUrl = value; }
		}

		public bool? SegundaChamadaTargetBlank {
			get { return _segundaChamadaTargetBlank; }
			set { _segundaChamadaTargetBlank = value; }
		}

		public List<ProgramaAtualizacaoPagina> ProgramaAtualizacaoPaginas {
			get { return _programaAtualizacaoPaginas; }
			set { _programaAtualizacaoPaginas = value; }
		}

		public Arquivo ArquivoImagem {
			get { return _arquivoImagem; }
			set { _arquivoImagem = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<ProgramaAtualizacaoChamada>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<ProgramaAtualizacaoChamada>(this);
        }
	}
	
	public struct ProgramaAtualizacaoChamadaColunas
	{	
		public static string ProgramaAtualizacaoChamadaId = @"programaAtualizacaoChamadaId";
		public static string Ativo = @"ativo";
		public static string PrimeiraChamadaTitulo = @"primeiraChamadaTitulo";
		public static string PrimeiraChamadaTexto = @"primeiraChamadaTexto";
		public static string PrimeiraChamadaUrl = @"primeiraChamadaUrl";
		public static string PrimeiraChamadaTargetBlank = @"primeiraChamadaTargetBlank";
		public static string SegundaChamadaTitulo = @"segundaChamadaTitulo";
		public static string SegundaChamadaTexto = @"segundaChamadaTexto";
		public static string SegundaChamadaUrl = @"segundaChamadaUrl";
		public static string SegundaChamadaTargetBlank = @"segundaChamadaTargetBlank";
		public static string ArquivoIdImagem = @"arquivoIdImagem";
	}
}
		