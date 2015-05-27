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
	public partial class TituloInformacaoSobreAutor 
	{
		private int _tituloInformacaoSobreAutorId;
		private string _textoAutor;
		private string _urlMidia;
		private Arquivo _arquivoImagem;
		private Titulo _titulo;

		public int TituloInformacaoSobreAutorId {
			get { return _tituloInformacaoSobreAutorId; }
			set { _tituloInformacaoSobreAutorId = value; }
		}

		public string TextoAutor {
			get { return _textoAutor; }
			set { _textoAutor = value; }
		}

		public string UrlMidia {
			get { return _urlMidia; }
			set { _urlMidia = value; }
		}

		public Arquivo ArquivoImagem {
			get { return _arquivoImagem; }
			set { _arquivoImagem = value; }
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
            get { return Validation.Validate<TituloInformacaoSobreAutor>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<TituloInformacaoSobreAutor>(this);
        }
	}
	
	public struct TituloInformacaoSobreAutorColunas
	{	
		public static string TituloInformacaoSobreAutorId = @"tituloInformacaoSobreAutorId";
		public static string TextoAutor = @"textoAutor";
		public static string UrlMidia = @"urlMidia";
		public static string ArquivoIdImagem = @"arquivoIdImagem";
	}
}
		