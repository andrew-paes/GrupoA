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
	public partial class Curso 
	{
		// Construtor
		public Curso() {}

		// Construtor com identificador
		public Curso(int cursoId) {
			_cursoId = cursoId;
		}

		private int _cursoId;
		private string _nome;
		private string _codigoCurso;
		private List<ProfessorCurso> _professorCursos;

		public int CursoId {
			get { return _cursoId; }
			set { _cursoId = value; }
		}

		public string Nome {
			get { return _nome; }
			set { _nome = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 50)]
		public string CodigoCurso {
			get { return _codigoCurso; }
			set { _codigoCurso = value; }
		}

		public List<ProfessorCurso> ProfessorCursos {
			get { return _professorCursos; }
			set { _professorCursos = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<Curso>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<Curso>(this);
        }
	}
	
	public struct CursoColunas
	{	
		public static string CursoId = @"cursoId";
		public static string Nome = @"nome";
		public static string CodigoCurso = @"codigoCurso";
	}
}
		