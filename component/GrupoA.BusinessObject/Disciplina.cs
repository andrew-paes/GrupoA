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
	public partial class Disciplina 
	{
		// Construtor
		public Disciplina() {}

		// Construtor com identificador
		public Disciplina(int disciplinaId) {
			_disciplinaId = disciplinaId;
		}

		private int _disciplinaId;
		private string _descricao;
		private string _codigoDisciplina;
		private List<ProfessorDisciplina> _professorDisciplinas;

		public int DisciplinaId {
			get { return _disciplinaId; }
			set { _disciplinaId = value; }
		}

		public string Descricao {
			get { return _descricao; }
			set { _descricao = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 50)]
		public string CodigoDisciplina {
			get { return _codigoDisciplina; }
			set { _codigoDisciplina = value; }
		}

		public List<ProfessorDisciplina> ProfessorDisciplinas {
			get { return _professorDisciplinas; }
			set { _professorDisciplinas = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<Disciplina>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<Disciplina>(this);
        }
	}
	
	public struct DisciplinaColunas
	{	
		public static string DisciplinaId = @"disciplinaId";
		public static string Descricao = @"descricao";
		public static string CodigoDisciplina = @"codigoDisciplina";
	}
}
		