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
	public partial class TituloConteudoExtraUrl 
	{
		private int _tituloConteudoExtraUrlId;
		private string _url;
		private bool _targetBlank;
		private Titulo _titulo;

		public int TituloConteudoExtraUrlId {
			get { return _tituloConteudoExtraUrlId; }
			set { _tituloConteudoExtraUrlId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 250)]
		public string Url {
			get { return _url; }
			set { _url = value; }
		}

		public bool TargetBlank {
			get { return _targetBlank; }
			set { _targetBlank = value; }
		}

		[NotNullValidator]
		public Titulo Titulo {
			get { return _titulo; }
			set { _titulo = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<TituloConteudoExtraUrl>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<TituloConteudoExtraUrl>(this);
        }
	}
	
	public struct TituloConteudoExtraUrlColunas
	{	
		public static string TituloConteudoExtraUrlId = @"tituloConteudoExtraUrlId";
		public static string Url = @"url";
		public static string TargetBlank = @"targetBlank";
	}
}
		