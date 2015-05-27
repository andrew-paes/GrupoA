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
	public partial class ProfessorCurso 
	{
		// Construtor
		public ProfessorCurso() {}

		// Construtor com identificador
		public ProfessorCurso(int professorCursoId) {
			_professorCursoId = professorCursoId;
		}

		private int _professorCursoId;
		private bool _coordenadorCurso;
		private string _cargo;
		private List<ProfessorDisciplina> _professorDisciplinas;
		private Curso _curso;
		private CursoNivel _cursoNivel;
		private ProfessorInstituicao _professorInstituicao;

		public int ProfessorCursoId {
			get { return _professorCursoId; }
			set { _professorCursoId = value; }
		}

		public bool CoordenadorCurso {
			get { return _coordenadorCurso; }
			set { _coordenadorCurso = value; }
		}

		public string Cargo {
			get { return _cargo; }
			set { _cargo = value; }
		}

		public List<ProfessorDisciplina> ProfessorDisciplinas {
			get { return _professorDisciplinas; }
			set { _professorDisciplinas = value; }
		}

		[NotNullValidator]
		public Curso Curso {
			get { return _curso; }
			set { _curso = value; }
		}

		[NotNullValidator]
		public CursoNivel CursoNivel {
			get { return _cursoNivel; }
			set { _cursoNivel = value; }
		}

		[NotNullValidator]
		public ProfessorInstituicao ProfessorInstituicao {
			get { return _professorInstituicao; }
			set { _professorInstituicao = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<ProfessorCurso>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<ProfessorCurso>(this);
        }
	}
	
	public struct ProfessorCursoColunas
	{	
		public static string ProfessorCursoId = @"professorCursoId";
		public static string ProfessorInstituicaoId = @"professorInstituicaoId";
		public static string CursoNivelId = @"cursoNivelId";
		public static string CoordenadorCurso = @"coordenadorCurso";
		public static string CursoId = @"cursoId";
		public static string Cargo = @"cargo";
	}
}
		