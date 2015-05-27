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
	public partial class TituloImpresso 
	{
		private int _tituloImpressoId;
		private string _isbn10;
		private string _isbn13;
		private List<CapituloImpresso> _capituloImpressos;
		private Produto _produto;
		private Titulo _titulo;

		public int TituloImpressoId {
			get { return _tituloImpressoId; }
			set { _tituloImpressoId = value; }
		}

		public string Isbn10 {
			get { return _isbn10; }
			set { _isbn10 = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 20)]
		public string Isbn13 {
			get { return _isbn13; }
			set { _isbn13 = value; }
		}

		public List<CapituloImpresso> CapituloImpressos {
			get { return _capituloImpressos; }
			set { _capituloImpressos = value; }
		}

		[NotNullValidator]
		public Produto Produto {
			get { return _produto; }
			set { _produto = value; }
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
            get { return Validation.Validate<TituloImpresso>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<TituloImpresso>(this);
        }
	}
	
	public struct TituloImpressoColunas
	{	
		public static string TituloImpressoId = @"tituloImpressoId";
		public static string Isbn10 = @"isbn10";
		public static string Isbn13 = @"isbn13";
		public static string TituloId = @"tituloId";
	}
}
		