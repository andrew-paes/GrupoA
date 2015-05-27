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
	public partial class OfertaCategoria 
	{
		// Construtor
		public OfertaCategoria() {}

		// Construtor com identificador
		public OfertaCategoria(int ofertaCategoriaId) {
			_ofertaCategoriaId = ofertaCategoriaId;
		}

		private int _ofertaCategoriaId;
		private Categoria _categoria;
		private Oferta _oferta;

		public int OfertaCategoriaId {
			get { return _ofertaCategoriaId; }
			set { _ofertaCategoriaId = value; }
		}

		[NotNullValidator]
		public Categoria Categoria {
			get { return _categoria; }
			set { _categoria = value; }
		}

		[NotNullValidator]
		public Oferta Oferta {
			get { return _oferta; }
			set { _oferta = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<OfertaCategoria>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<OfertaCategoria>(this);
        }
	}
	
	public struct OfertaCategoriaColunas
	{	
		public static string OfertaCategoriaId = @"ofertaCategoriaId";
		public static string OfertaId = @"ofertaId";
		public static string CategoriaId = @"categoriaId";
	}
}
		