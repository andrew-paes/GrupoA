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
	public partial class Telefone 
	{
		// Construtor
		public Telefone() {}

		// Construtor com identificador
		public Telefone(int telefoneId) {
			_telefoneId = telefoneId;
		}

		private int _telefoneId;
		private string _numeroTelefone;
		private string _dddTelefone;
		private string _ramal;
		private ProfessorInstituicao _professorInstituicao;
		private TelefoneTipo _telefoneTipo;
		private Usuario _usuario;

		public int TelefoneId {
			get { return _telefoneId; }
			set { _telefoneId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 50)]
		public string NumeroTelefone {
			get { return _numeroTelefone; }
			set { _numeroTelefone = value; }
		}

		public string DddTelefone {
			get { return _dddTelefone; }
			set { _dddTelefone = value; }
		}

		public string Ramal {
			get { return _ramal; }
			set { _ramal = value; }
		}

		[NotNullValidator]
		public ProfessorInstituicao ProfessorInstituicao {
			get { return _professorInstituicao; }
			set { _professorInstituicao = value; }
		}

		[NotNullValidator]
		public TelefoneTipo TelefoneTipo {
			get { return _telefoneTipo; }
			set { _telefoneTipo = value; }
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
            get { return Validation.Validate<Telefone>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<Telefone>(this);
        }
	}
	
	public struct TelefoneColunas
	{	
		public static string TelefoneId = @"telefoneId";
		public static string NumeroTelefone = @"numeroTelefone";
		public static string DddTelefone = @"dddTelefone";
		public static string TelefoneTipoId = @"telefoneTipoId";
		public static string UsuarioId = @"usuarioId";
		public static string Ramal = @"ramal";
	}
}
		