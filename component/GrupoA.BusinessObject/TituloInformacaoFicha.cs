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
	public partial class TituloInformacaoFicha 
	{
		private int _tituloInformacaoFichaId;
		private string _textoFichaTecnica;
		private Titulo _titulo;

		public int TituloInformacaoFichaId {
			get { return _tituloInformacaoFichaId; }
			set { _tituloInformacaoFichaId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 1073741823)]
		public string TextoFichaTecnica {
			get { return _textoFichaTecnica; }
			set { _textoFichaTecnica = value; }
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
            get { return Validation.Validate<TituloInformacaoFicha>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<TituloInformacaoFicha>(this);
        }
	}
	
	public struct TituloInformacaoFichaColunas
	{	
		public static string TituloInformacaoFichaId = @"tituloInformacaoFichaId";
		public static string TextoFichaTecnica = @"textoFichaTecnica";
	}
}
		