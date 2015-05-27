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
	public partial class TituloInformacaoComentarioEspecialistaCategoria 
	{
		// Construtor
		public TituloInformacaoComentarioEspecialistaCategoria() {}

		// Construtor com identificador
		public TituloInformacaoComentarioEspecialistaCategoria(int tituloInformacaoComentarioEspecialistaComentarioId) {
			_tituloInformacaoComentarioEspecialistaCategoriaId = tituloInformacaoComentarioEspecialistaComentarioId;
		}

		private int _tituloInformacaoComentarioEspecialistaCategoriaId;
		private Categoria _categoria;
		private TituloInformacaoComentarioEspecialista _tituloInformacaoComentarioEspecialista;

		public int TituloInformacaoComentarioEspecialistaCategoriaId {
			get { return _tituloInformacaoComentarioEspecialistaCategoriaId; }
			set { _tituloInformacaoComentarioEspecialistaCategoriaId = value; }
		}

		[NotNullValidator]
		public Categoria Categoria {
			get { return _categoria; }
			set { _categoria = value; }
		}

		[NotNullValidator]
		public TituloInformacaoComentarioEspecialista TituloInformacaoComentarioEspecialista {
			get { return _tituloInformacaoComentarioEspecialista; }
			set { _tituloInformacaoComentarioEspecialista = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<TituloInformacaoComentarioEspecialistaCategoria>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<TituloInformacaoComentarioEspecialistaCategoria>(this);
        }
	}
	
	public struct TituloInformacaoComentarioEspecialistaCategoriaColunas
	{	
		public static string TituloInformacaoComentarioEspecialistaCategoriaId = @"tituloInformacaoComentarioEspecialistaCategoriaId";
		public static string TituloInformacaoComentarioEspecialistaId = @"tituloInformacaoComentarioEspecialistaId";
		public static string CategoriaId = @"categoriaId";
	}
}
		