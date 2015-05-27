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
	public partial class ContatoResponsavel 
	{
		// Construtor
		public ContatoResponsavel() {}

		// Construtor com identificador
		public ContatoResponsavel(int contatoResponsavelId) {
			_contatoResponsavelId = contatoResponsavelId;
		}

		private int _contatoResponsavelId;
		private string _nomeResponsavel;
		private string _emailResonsavel;
		private ContatoAssunto _contatoAssunto;

		public int ContatoResponsavelId {
			get { return _contatoResponsavelId; }
			set { _contatoResponsavelId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 100)]
		public string NomeResponsavel {
			get { return _nomeResponsavel; }
			set { _nomeResponsavel = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 200)]
		public string EmailResonsavel {
			get { return _emailResonsavel; }
			set { _emailResonsavel = value; }
		}

		[NotNullValidator]
		public ContatoAssunto ContatoAssunto {
			get { return _contatoAssunto; }
			set { _contatoAssunto = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<ContatoResponsavel>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<ContatoResponsavel>(this);
        }
	}
	
	public struct ContatoResponsavelColunas
	{	
		public static string ContatoResponsavelId = @"contatoResponsavelId";
		public static string ContatoAssuntoId = @"contatoAssuntoId";
		public static string NomeResponsavel = @"nomeResponsavel";
		public static string EmailResonsavel = @"emailResonsavel";
	}
}
		