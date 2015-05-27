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
	public partial class Instituicao 
	{
		// Construtor
		public Instituicao() {}

		// Construtor com identificador
		public Instituicao(int instituicaoId) {
			_instituicaoId = instituicaoId;
		}

		private int _instituicaoId;
		private string _nomeInstituicao;
		private string _cnpj;
		private string _telefoneNumero;
		private string _emailInstituicao;
		private string _urlSiteInstituicao;
		private string _codigoInstituicao;
		private List<ProfessorComprovanteDocencia> _professorComprovanteDocencias;
		private List<ProfessorInstituicao> _professorInstituicoes;

		public int InstituicaoId {
			get { return _instituicaoId; }
			set { _instituicaoId = value; }
		}

		public string NomeInstituicao {
			get { return _nomeInstituicao; }
			set { _nomeInstituicao = value; }
		}

		public string Cnpj {
			get { return _cnpj; }
			set { _cnpj = value; }
		}

		public string TelefoneNumero {
			get { return _telefoneNumero; }
			set { _telefoneNumero = value; }
		}

		public string EmailInstituicao {
			get { return _emailInstituicao; }
			set { _emailInstituicao = value; }
		}

		public string UrlSiteInstituicao {
			get { return _urlSiteInstituicao; }
			set { _urlSiteInstituicao = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 50)]
		public string CodigoInstituicao {
			get { return _codigoInstituicao; }
			set { _codigoInstituicao = value; }
		}

		public List<ProfessorComprovanteDocencia> ProfessorComprovanteDocencias {
			get { return _professorComprovanteDocencias; }
			set { _professorComprovanteDocencias = value; }
		}

		public List<ProfessorInstituicao> ProfessorInstituicoes {
			get { return _professorInstituicoes; }
			set { _professorInstituicoes = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<Instituicao>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<Instituicao>(this);
        }
	}
	
	public struct InstituicaoColunas
	{	
		public static string InstituicaoId = @"instituicaoId";
		public static string NomeInstituicao = @"nomeInstituicao";
		public static string Cnpj = @"cnpj";
		public static string TelefoneNumero = @"telefoneNumero";
		public static string EmailInstituicao = @"emailInstituicao";
		public static string UrlSiteInstituicao = @"urlSiteInstituicao";
		public static string CodigoInstituicao = @"codigoInstituicao";
	}
}
		