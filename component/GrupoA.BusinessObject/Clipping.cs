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
	public partial class Clipping 
	{
		private int _clippingId;
		private string _autor;
		private DateTime? _dataPublicacao;
		private List<ClippingImagem> _clippingImagens;
		private ConteudoImprensa _conteudoImprensa;

		public int ClippingId {
			get { return _clippingId; }
			set { _clippingId = value; }
		}

		public string Autor {
			get { return _autor; }
			set { _autor = value; }
		}

		public DateTime? DataPublicacao {
			get { return _dataPublicacao; }
			set { _dataPublicacao = value; }
		}

		public List<ClippingImagem> ClippingImagens {
			get { return _clippingImagens; }
			set { _clippingImagens = value; }
		}

		[NotNullValidator]
		public ConteudoImprensa ConteudoImprensa {
			get { return _conteudoImprensa; }
			set { _conteudoImprensa = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<Clipping>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<Clipping>(this);
        }
	}
	
	public struct ClippingColunas
	{	
		public static string ClippingId = @"clippingId";
		public static string Autor = @"autor";
		public static string DataPublicacao = @"dataPublicacao";
	}
}
		