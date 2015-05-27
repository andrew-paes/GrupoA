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
	public partial class MidiaCategoria 
	{
		// Construtor
		public MidiaCategoria() {}

		// Construtor com identificador
		public MidiaCategoria(int midiaCategoriaId) {
			_midiaCategoriaId = midiaCategoriaId;
		}

		private int _midiaCategoriaId;
		private Categoria _categoria;
		private Midia _midia;

		public int MidiaCategoriaId {
			get { return _midiaCategoriaId; }
			set { _midiaCategoriaId = value; }
		}

		[NotNullValidator]
		public Categoria Categoria {
			get { return _categoria; }
			set { _categoria = value; }
		}

		[NotNullValidator]
		public Midia Midia {
			get { return _midia; }
			set { _midia = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<MidiaCategoria>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<MidiaCategoria>(this);
        }
	}
	
	public struct MidiaCategoriaColunas
	{	
		public static string MidiaCategoriaId = @"midiaCategoriaId";
		public static string MidiaId = @"midiaId";
		public static string CategoriaId = @"categoriaId";
	}
}
		