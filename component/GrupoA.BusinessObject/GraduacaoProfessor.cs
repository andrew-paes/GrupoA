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
	public partial class GraduacaoProfessor 
	{
		// Construtor
		public GraduacaoProfessor() {}

		// Construtor com identificador
		public GraduacaoProfessor(int graduacaoProfessorId) {
			_graduacaoProfessorId = graduacaoProfessorId;
		}

		private int _graduacaoProfessorId;
		private string _graduacao;
		private List<Professor> _professores;

		public int GraduacaoProfessorId {
			get { return _graduacaoProfessorId; }
			set { _graduacaoProfessorId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 100)]
		public string Graduacao {
			get { return _graduacao; }
			set { _graduacao = value; }
		}

		public List<Professor> Professores {
			get { return _professores; }
			set { _professores = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<GraduacaoProfessor>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<GraduacaoProfessor>(this);
        }
	}
	
	public struct GraduacaoProfessorColunas
	{	
		public static string GraduacaoProfessorId = @"graduacaoProfessorId";
		public static string Graduacao = @"graduacao";
	}
}
		