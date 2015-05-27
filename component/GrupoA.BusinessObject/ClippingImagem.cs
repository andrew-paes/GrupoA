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
	public partial class ClippingImagem 
	{
		// Construtor
		public ClippingImagem() {}

		// Construtor com identificador
		public ClippingImagem(int clippingImagemId) {
			_clippingImagemId = clippingImagemId;
		}

		private int _clippingImagemId;
		private int _ordemApresentacao;
		private Arquivo _arquivo;
		private Clipping _clipping;

		public int ClippingImagemId {
			get { return _clippingImagemId; }
			set { _clippingImagemId = value; }
		}

		public int OrdemApresentacao {
			get { return _ordemApresentacao; }
			set { _ordemApresentacao = value; }
		}

		[NotNullValidator]
		public Arquivo Arquivo {
			get { return _arquivo; }
			set { _arquivo = value; }
		}

		[NotNullValidator]
		public Clipping Clipping {
			get { return _clipping; }
			set { _clipping = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<ClippingImagem>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<ClippingImagem>(this);
        }
	}
	
	public struct ClippingImagemColunas
	{	
		public static string ClippingImagemId = @"clippingImagemId";
		public static string ArquivoId = @"arquivoId";
		public static string ClippingId = @"clippingId";
		public static string OrdemApresentacao = @"ordemApresentacao";
	}
}
		