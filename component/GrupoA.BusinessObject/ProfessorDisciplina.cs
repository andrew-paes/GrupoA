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
	public partial class ProfessorDisciplina 
	{
		// Construtor
		public ProfessorDisciplina() {}

		// Construtor com identificador
		public ProfessorDisciplina(int professorDisciplinaId) {
			_professorDisciplinaId = professorDisciplinaId;
		}

		private int _professorDisciplinaId;
		private int _numeroAlunos;
		private bool _indicaTitulo;
		private Disciplina _disciplina;
		private ProfessorCurso _professorCurso;

		public int ProfessorDisciplinaId {
			get { return _professorDisciplinaId; }
			set { _professorDisciplinaId = value; }
		}

		public int NumeroAlunos {
			get { return _numeroAlunos; }
			set { _numeroAlunos = value; }
		}

		public bool IndicaTitulo {
			get { return _indicaTitulo; }
			set { _indicaTitulo = value; }
		}

		[NotNullValidator]
		public Disciplina Disciplina {
			get { return _disciplina; }
			set { _disciplina = value; }
		}

		[NotNullValidator]
		public ProfessorCurso ProfessorCurso {
			get { return _professorCurso; }
			set { _professorCurso = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<ProfessorDisciplina>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<ProfessorDisciplina>(this);
        }
	}
	
	public struct ProfessorDisciplinaColunas
	{	
		public static string ProfessorDisciplinaId = @"professorDisciplinaId";
		public static string NumeroAlunos = @"numeroAlunos";
		public static string ProfessorCursoId = @"professorCursoId";
		public static string DisciplinaId = @"disciplinaId";
		public static string IndicaTitulo = @"indicaTitulo";
	}
}
		