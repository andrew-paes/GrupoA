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
	public partial class ProfessorComprovanteDocencia 
	{
		// Construtor
		public ProfessorComprovanteDocencia() {}

		// Construtor com identificador
		public ProfessorComprovanteDocencia(int professorComprovanteDocenciaId) {
			_professorComprovanteDocenciaId = professorComprovanteDocenciaId;
		}

		private int _professorComprovanteDocenciaId;
		private Arquivo _arquivo;
		private Instituicao _instituicao;
		private Professor _professor;

		public int ProfessorComprovanteDocenciaId {
			get { return _professorComprovanteDocenciaId; }
			set { _professorComprovanteDocenciaId = value; }
		}

		[NotNullValidator]
		public Arquivo Arquivo {
			get { return _arquivo; }
			set { _arquivo = value; }
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

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<ProfessorComprovanteDocencia>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<ProfessorComprovanteDocencia>(this);
        }
	}
	
	public struct ProfessorComprovanteDocenciaColunas
	{	
		public static string ProfessorComprovanteDocenciaId = @"professorComprovanteDocenciaId";
		public static string ProfessorId = @"professorId";
		public static string ArquivoId = @"arquivoId";
		public static string InstituicaoId = @"instituicaoId";
	}
}
		