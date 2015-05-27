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
	public partial class TituloInformacaoSumario 
	{
		private int _tituloInformacaoSumarioId;
		private string _textoSumario;
		private Arquivo _arquivoSumario;
		private Titulo _titulo;

		public int TituloInformacaoSumarioId {
			get { return _tituloInformacaoSumarioId; }
			set { _tituloInformacaoSumarioId = value; }
		}

		public string TextoSumario {
			get { return _textoSumario; }
			set { _textoSumario = value; }
		}

		public Arquivo ArquivoSumario {
			get { return _arquivoSumario; }
			set { _arquivoSumario = value; }
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
            get { return Validation.Validate<TituloInformacaoSumario>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<TituloInformacaoSumario>(this);
        }
	}
	
	public struct TituloInformacaoSumarioColunas
	{	
		public static string TituloInformacaoSumarioId = @"tituloInformacaoSumarioId";
		public static string ArquivoIdSumario = @"arquivoIdSumario";
		public static string TextoSumario = @"textoSumario";
	}
}
		