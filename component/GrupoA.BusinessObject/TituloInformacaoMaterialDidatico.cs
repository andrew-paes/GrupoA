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
	public partial class TituloInformacaoMaterialDidatico 
	{
		private int _tituloInformacaoMaterialDidaticoId;
		private string _textoMaterial;
		private Titulo _titulo;

		public int TituloInformacaoMaterialDidaticoId {
			get { return _tituloInformacaoMaterialDidaticoId; }
			set { _tituloInformacaoMaterialDidaticoId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 1073741823)]
		public string TextoMaterial {
			get { return _textoMaterial; }
			set { _textoMaterial = value; }
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
            get { return Validation.Validate<TituloInformacaoMaterialDidatico>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<TituloInformacaoMaterialDidatico>(this);
        }
	}
	
	public struct TituloInformacaoMaterialDidaticoColunas
	{	
		public static string TituloInformacaoMaterialDidaticoId = @"tituloInformacaoMaterialDidaticoId";
		public static string TextoMaterial = @"textoMaterial";
	}
}
		