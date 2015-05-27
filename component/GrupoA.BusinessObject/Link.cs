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
	public partial class Link 
	{
		// Construtor
		public Link() {}

		// Construtor com identificador
		public Link(int linkId) {
			_linkId = linkId;
		}

		private int _linkId;
		private string _nomeLink;
		private string _urlLink;
		private bool _ativo;
		private bool _targetBlank;

		public int LinkId {
			get { return _linkId; }
			set { _linkId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 100)]
		public string NomeLink {
			get { return _nomeLink; }
			set { _nomeLink = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 250)]
		public string UrlLink {
			get { return _urlLink; }
			set { _urlLink = value; }
		}

		public bool Ativo {
			get { return _ativo; }
			set { _ativo = value; }
		}

		public bool TargetBlank {
			get { return _targetBlank; }
			set { _targetBlank = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<Link>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<Link>(this);
        }
	}
	
	public struct LinkColunas
	{	
		public static string LinkId = @"linkId";
		public static string NomeLink = @"nomeLink";
		public static string UrlLink = @"urlLink";
		public static string Ativo = @"ativo";
		public static string TargetBlank = @"targetBlank";
	}
}
		