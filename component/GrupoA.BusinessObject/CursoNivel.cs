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
	public partial class CursoNivel 
	{
		// Construtor
		public CursoNivel() {}

		// Construtor com identificador
		public CursoNivel(int cursoNivelId) {
			_cursoNivelId = cursoNivelId;
		}

		private int _cursoNivelId;
		private string _nivel;
		private List<ProfessorCurso> _professorCursos;

		public int CursoNivelId {
			get { return _cursoNivelId; }
			set { _cursoNivelId = value; }
		}

		public string Nivel {
			get { return _nivel; }
			set { _nivel = value; }
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
            get { return Validation.Validate<CursoNivel>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<CursoNivel>(this);
        }
	}
	
	public struct CursoNivelColunas
	{	
		public static string CursoNivelId = @"cursoNivelId";
		public static string Nivel = @"nivel";
	}
}
		