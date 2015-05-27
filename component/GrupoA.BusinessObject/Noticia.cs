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
	public partial class Noticia 
	{
		private int _noticiaId;
		private string _autor;
		private DateTime? _dataPublicacao;
		private List<NoticiaImagem> _noticiaImagens;
		private Arquivo _arquivoThumb;
		private CategoriaNoticia _categoriaNoticia;
		private ConteudoImprensa _conteudoImprensa;

		public int NoticiaId {
			get { return _noticiaId; }
			set { _noticiaId = value; }
		}

		public string Autor {
			get { return _autor; }
			set { _autor = value; }
		}

		public DateTime? DataPublicacao {
			get { return _dataPublicacao; }
			set { _dataPublicacao = value; }
		}

		public List<NoticiaImagem> NoticiaImagens {
			get { return _noticiaImagens; }
			set { _noticiaImagens = value; }
		}

		public Arquivo ArquivoThumb {
			get { return _arquivoThumb; }
			set { _arquivoThumb = value; }
		}

		[NotNullValidator]
		public CategoriaNoticia CategoriaNoticia {
			get { return _categoriaNoticia; }
			set { _categoriaNoticia = value; }
		}

		[NotNullValidator]
		public ConteudoImprensa ConteudoImprensa {
			get { return _conteudoImprensa; }
			set { _conteudoImprensa = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<Noticia>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<Noticia>(this);
        }
	}
	
	public struct NoticiaColunas
	{	
		public static string NoticiaId = @"noticiaId";
		public static string Autor = @"autor";
		public static string DataPublicacao = @"dataPublicacao";
		public static string CategoriaNoticiaId = @"categoriaNoticiaId";
		public static string ArquivoIdThumb = @"arquivoIdThumb";
	}
}
		