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
	public partial class TituloImagemResumo 
	{
		// Construtor
		public TituloImagemResumo() {}

		// Construtor com identificador
		public TituloImagemResumo(int tituloImagemResumoId) {
			_tituloImagemResumoId = tituloImagemResumoId;
		}

		private int _tituloImagemResumoId;
		private Arquivo _arquivo;
		private Titulo _titulo;

		public int TituloImagemResumoId {
			get { return _tituloImagemResumoId; }
			set { _tituloImagemResumoId = value; }
		}

		[NotNullValidator]
		public Arquivo Arquivo {
			get { return _arquivo; }
			set { _arquivo = value; }
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
            get { return Validation.Validate<TituloImagemResumo>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<TituloImagemResumo>(this);
        }
	}
	
	public struct TituloImagemResumoColunas
	{	
		public static string TituloImagemResumoId = @"tituloImagemResumoId";
		public static string ArquivoId = @"arquivoId";
		public static string TituloId = @"tituloId";
	}
}
		