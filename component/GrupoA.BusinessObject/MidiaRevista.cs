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
	public partial class MidiaRevista 
	{
		// Construtor
		public MidiaRevista() {}

		// Construtor com identificador
		public MidiaRevista(int midiaRevistaId) {
			_midiaRevistaId = midiaRevistaId;
		}

		private int _midiaRevistaId;
		private Midia _midia;
		private Revista _revista;

		public int MidiaRevistaId {
			get { return _midiaRevistaId; }
			set { _midiaRevistaId = value; }
		}

		[NotNullValidator]
		public Midia Midia {
			get { return _midia; }
			set { _midia = value; }
		}

		[NotNullValidator]
		public Revista Revista {
			get { return _revista; }
			set { _revista = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<MidiaRevista>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<MidiaRevista>(this);
        }
	}
	
	public struct MidiaRevistaColunas
	{	
		public static string MidiaRevistaId = @"midiaRevistaId";
		public static string MidiaId = @"midiaId";
		public static string RevistaId = @"revistaId";
	}
}
		