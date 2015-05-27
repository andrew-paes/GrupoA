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
	public partial class RevistaPagina 
	{
		// Construtor
		public RevistaPagina() {}

		// Construtor com identificador
		public RevistaPagina(int revistaPaginaId) {
			_revistaPaginaId = revistaPaginaId;
		}

		private int _revistaPaginaId;
		private string _nomePagina;
		private string _tituloPagina;
		private string _textoPagina;
		private bool _ativo;
		private int _ordem;
		private bool _exibirMenu;
		private Revista _revista;

		public int RevistaPaginaId {
			get { return _revistaPaginaId; }
			set { _revistaPaginaId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 100)]
		public string NomePagina {
			get { return _nomePagina; }
			set { _nomePagina = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 100)]
		public string TituloPagina {
			get { return _tituloPagina; }
			set { _tituloPagina = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 1073741823)]
		public string TextoPagina {
			get { return _textoPagina; }
			set { _textoPagina = value; }
		}

		public bool Ativo {
			get { return _ativo; }
			set { _ativo = value; }
		}

		public int Ordem {
			get { return _ordem; }
			set { _ordem = value; }
		}

		public bool ExibirMenu {
			get { return _exibirMenu; }
			set { _exibirMenu = value; }
		}

		[NotNullValidator]
		public Revista Revista {
			get { return _revista; }
			set { _revista = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<RevistaPagina>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<RevistaPagina>(this);
        }
	}
	
	public struct RevistaPaginaColunas
	{	
		public static string RevistaPaginaId = @"revistaPaginaId";
		public static string RevistaId = @"revistaId";
		public static string NomePagina = @"nomePagina";
		public static string TituloPagina = @"tituloPagina";
		public static string TextoPagina = @"textoPagina";
		public static string Ativo = @"ativo";
		public static string Ordem = @"ordem";
		public static string ExibirMenu = @"exibirMenu";
	}
}
		