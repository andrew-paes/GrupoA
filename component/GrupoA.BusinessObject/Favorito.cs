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
	public partial class Favorito 
	{
		// Construtor
		public Favorito() {}

		// Construtor com identificador
		public Favorito(int favoritoId) {
			_favoritoId = favoritoId;
		}

		private int _favoritoId;
		private int _usuarioId;
		private Conteudo _conteudo;

		public int FavoritoId {
			get { return _favoritoId; }
			set { _favoritoId = value; }
		}

		public int UsuarioId {
			get { return _usuarioId; }
			set { _usuarioId = value; }
		}

		[NotNullValidator]
		public Conteudo Conteudo {
			get { return _conteudo; }
			set { _conteudo = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<Favorito>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<Favorito>(this);
        }
	}
	
	public struct FavoritoColunas
	{	
		public static string FavoritoId = @"favoritoId";
		public static string ConteudoId = @"conteudoId";
		public static string UsuarioId = @"usuarioId";
	}
}
		