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
	public partial class Autor 
	{
		// Construtor
		public Autor() {}

		// Construtor com identificador
		public Autor(int autorId) {
			_autorId = autorId;
		}

		private int _autorId;
		private string _url;
		private string _email;
		private string _blog;
		private string _nomeAutor;
		private string _codigoLegado;
		private string _biografia;
		private List<Capitulo> _capitulos;
		private List<TituloAutor> _tituloAutores;
		private Arquivo _arquivoImagem;

		public int AutorId {
			get { return _autorId; }
			set { _autorId = value; }
		}

		public string Url {
			get { return _url; }
			set { _url = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 256)]
		public string Email {
			get { return _email; }
			set { _email = value; }
		}

		public string Blog {
			get { return _blog; }
			set { _blog = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 100)]
		public string NomeAutor {
			get { return _nomeAutor; }
			set { _nomeAutor = value; }
		}

		public string CodigoLegado {
			get { return _codigoLegado; }
			set { _codigoLegado = value; }
		}

		public string Biografia {
			get { return _biografia; }
			set { _biografia = value; }
		}

		public List<Capitulo> Capitulos {
			get { return _capitulos; }
			set { _capitulos = value; }
		}

		public List<TituloAutor> TituloAutores {
			get { return _tituloAutores; }
			set { _tituloAutores = value; }
		}

		public Arquivo ArquivoImagem {
			get { return _arquivoImagem; }
			set { _arquivoImagem = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<Autor>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<Autor>(this);
        }
	}
	
	public struct AutorColunas
	{	
		public static string AutorId = @"autorId";
		public static string Url = @"url";
		public static string Email = @"email";
		public static string Blog = @"blog";
		public static string NomeAutor = @"nomeAutor";
		public static string CodigoLegado = @"codigoLegado";
		public static string Biografia = @"biografia";
		public static string ArquivoIdImagem = @"arquivoIdImagem";
	}
}
		