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
	public partial class Professor 
	{
		private int _professorId;
		private bool _autorGrupoa;
		private bool _colaboradorGrupoa;
		private bool _possuiPublicacao;
		private List<ProfessorComprovanteDocencia> _professorComprovanteDocencias;
		private List<ProfessorInstituicao> _professorInstituicoes;
		private List<TituloSolicitacao> _tituloSolicitacoes;
		private GraduacaoProfessor _graduacaoProfessor;
		private Usuario _usuario;

		public int ProfessorId {
			get { return _professorId; }
			set { _professorId = value; }
		}

		public bool AutorGrupoa {
			get { return _autorGrupoa; }
			set { _autorGrupoa = value; }
		}

		public bool ColaboradorGrupoa {
			get { return _colaboradorGrupoa; }
			set { _colaboradorGrupoa = value; }
		}

		public bool PossuiPublicacao {
			get { return _possuiPublicacao; }
			set { _possuiPublicacao = value; }
		}

		public List<ProfessorComprovanteDocencia> ProfessorComprovanteDocencias {
			get { return _professorComprovanteDocencias; }
			set { _professorComprovanteDocencias = value; }
		}

		public List<ProfessorInstituicao> ProfessorInstituicoes {
			get { return _professorInstituicoes; }
			set { _professorInstituicoes = value; }
		}

		public List<TituloSolicitacao> TituloSolicitacoes {
			get { return _tituloSolicitacoes; }
			set { _tituloSolicitacoes = value; }
		}

		[NotNullValidator]
		public GraduacaoProfessor GraduacaoProfessor {
			get { return _graduacaoProfessor; }
			set { _graduacaoProfessor = value; }
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
            get { return Validation.Validate<Professor>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<Professor>(this);
        }
	}
	
	public struct ProfessorColunas
	{	
		public static string ProfessorId = @"professorId";
		public static string GraduacaoProfessorId = @"graduacaoProfessorId";
		public static string AutorGrupoa = @"autorGrupoa";
		public static string ColaboradorGrupoa = @"colaboradorGrupoa";
		public static string PossuiPublicacao = @"possuiPublicacao";
	}
}
		