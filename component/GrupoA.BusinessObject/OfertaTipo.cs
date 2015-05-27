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
	public partial class OfertaTipo 
	{
		// Construtor
		public OfertaTipo() {}

		// Construtor com identificador
		public OfertaTipo(int ofertaTipoId) {
			_ofertaTipoId = ofertaTipoId;
		}

		private int _ofertaTipoId;
		private string _tipoOferta;
		private List<Oferta> _ofertas;

		public int OfertaTipoId {
			get { return _ofertaTipoId; }
			set { _ofertaTipoId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 250)]
		public string TipoOferta {
			get { return _tipoOferta; }
			set { _tipoOferta = value; }
		}

		public List<Oferta> Ofertas {
			get { return _ofertas; }
			set { _ofertas = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<OfertaTipo>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<OfertaTipo>(this);
        }
	}
	
	public struct OfertaTipoColunas
	{	
		public static string OfertaTipoId = @"ofertaTipoId";
		public static string TipoOferta = @"tipoOferta";
	}
}
		