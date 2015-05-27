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
	public partial class ProfessorInstituicao 
	{
		// Construtor
		public ProfessorInstituicao() {}

		// Construtor com identificador
		public ProfessorInstituicao(int professorInstituicaoId) {
			_professorInstituicaoId = professorInstituicaoId;
		}

		private int _professorInstituicaoId;
		private string _campus;
		private string _departamento;
		private List<ProfessorCurso> _professorCursos;
		private Instituicao _instituicao;
		private Professor _professor;
		private Telefone _telefone;

		public int ProfessorInstituicaoId {
			get { return _professorInstituicaoId; }
			set { _professorInstituicaoId = value; }
		}

		public string Campus {
			get { return _campus; }
			set { _campus = value; }
		}

		public string Departamento {
			get { return _departamento; }
			set { _departamento = value; }
		}

		public List<ProfessorCurso> ProfessorCursos {
			get { return _professorCursos; }
			set { _professorCursos = value; }
		}

		[NotNullValidator]
		public Instituicao Instituicao {
			get { return _instituicao; }
			set { _instituicao = value; }
		}

		[NotNullValidator]
		public Professor Professor {
			get { return _professor; }
			set { _professor = value; }
		}

		public Telefone Telefone {
			get { return _telefone; }
			set { _telefone = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<ProfessorInstituicao>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<ProfessorInstituicao>(this);
        }
	}
	
	public struct ProfessorInstituicaoColunas
	{	
		public static string ProfessorInstituicaoId = @"professorInstituicaoId";
		public static string InstituicaoId = @"instituicaoId";
		public static string Campus = @"campus";
		public static string Departamento = @"departamento";
		public static string TelefoneId = @"telefoneId";
		public static string ProfessorId = @"professorId";
	}
}
		