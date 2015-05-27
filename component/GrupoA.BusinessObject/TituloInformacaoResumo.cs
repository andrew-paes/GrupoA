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
	public partial class TituloInformacaoResumo 
	{
		private int _tituloInformacaoResumoId;
		private string _textoResumo;
		private Titulo _titulo;

		public int TituloInformacaoResumoId {
			get { return _tituloInformacaoResumoId; }
			set { _tituloInformacaoResumoId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 1073741823)]
		public string TextoResumo {
			get { return _textoResumo; }
			set { _textoResumo = value; }
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
            get { return Validation.Validate<TituloInformacaoResumo>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<TituloInformacaoResumo>(this);
        }
	}
	
	public struct TituloInformacaoResumoColunas
	{	
		public static string TituloInformacaoResumoId = @"tituloInformacaoResumoId";
		public static string TextoResumo = @"textoResumo";
	}
}
		