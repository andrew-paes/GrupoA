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
	public partial class ComentarioFormato 
	{
		// Construtor
		public ComentarioFormato() {}

		// Construtor com identificador
		public ComentarioFormato(int comentarioFormatoId) {
			_comentarioFormatoId = comentarioFormatoId;
		}

		private int _comentarioFormatoId;
		private string _formato;
		private List<TituloInformacaoComentarioEspecialista> _tituloInformacaoComentarioEspecialistas;

		public int ComentarioFormatoId {
			get { return _comentarioFormatoId; }
			set { _comentarioFormatoId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 100)]
		public string Formato {
			get { return _formato; }
			set { _formato = value; }
		}

		public List<TituloInformacaoComentarioEspecialista> TituloInformacaoComentarioEspecialistas {
			get { return _tituloInformacaoComentarioEspecialistas; }
			set { _tituloInformacaoComentarioEspecialistas = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<ComentarioFormato>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<ComentarioFormato>(this);
        }
	}
	
	public struct ComentarioFormatoColunas
	{	
		public static string ComentarioFormatoId = @"comentarioFormatoId";
		public static string Formato = @"formato";
	}
}
		