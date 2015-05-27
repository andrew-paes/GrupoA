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
	public partial class CategoriaNoticia 
	{
		// Construtor
		public CategoriaNoticia() {}

		// Construtor com identificador
		public CategoriaNoticia(int categoriaNoticiaId) {
			_categoriaNoticiaId = categoriaNoticiaId;
		}

		private int _categoriaNoticiaId;
		private string _nomeCategoriaNoticia;
		private List<Noticia> _noticias;

		public int CategoriaNoticiaId {
			get { return _categoriaNoticiaId; }
			set { _categoriaNoticiaId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 50)]
		public string NomeCategoriaNoticia {
			get { return _nomeCategoriaNoticia; }
			set { _nomeCategoriaNoticia = value; }
		}

		public List<Noticia> Noticias {
			get { return _noticias; }
			set { _noticias = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<CategoriaNoticia>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<CategoriaNoticia>(this);
        }
	}
	
	public struct CategoriaNoticiaColunas
	{	
		public static string CategoriaNoticiaId = @"categoriaNoticiaId";
		public static string NomeCategoriaNoticia = @"nomeCategoriaNoticia";
	}
}
		