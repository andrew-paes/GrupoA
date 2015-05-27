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
	public partial class NewsletterDestinatario 
	{
		// Construtor
		public NewsletterDestinatario() {}

		// Construtor com identificador
		public NewsletterDestinatario(int newsletterDestinatarioId) {
			_newsletterDestinatarioId = newsletterDestinatarioId;
		}

		private int _newsletterDestinatarioId;
		private string _emailDestinatario;
		private string _nomeDestinatario;
		private DateTime _dataHoraCadastro;

		public int NewsletterDestinatarioId {
			get { return _newsletterDestinatarioId; }
			set { _newsletterDestinatarioId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 200)]
		public string EmailDestinatario {
			get { return _emailDestinatario; }
			set { _emailDestinatario = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 200)]
		public string NomeDestinatario {
			get { return _nomeDestinatario; }
			set { _nomeDestinatario = value; }
		}

		[NotNullValidator]
		public DateTime DataHoraCadastro {
			get { return _dataHoraCadastro; }
			set { _dataHoraCadastro = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<NewsletterDestinatario>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<NewsletterDestinatario>(this);
        }
	}
	
	public struct NewsletterDestinatarioColunas
	{	
		public static string NewsletterDestinatarioId = @"newsletterDestinatarioId";
		public static string EmailDestinatario = @"emailDestinatario";
		public static string NomeDestinatario = @"nomeDestinatario";
		public static string DataHoraCadastro = @"dataHoraCadastro";
	}
}
		