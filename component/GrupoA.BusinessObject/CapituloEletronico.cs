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
	public partial class CapituloEletronico 
	{
		private int _capituloEletronicoId;
		private Capitulo _capitulo;
		private Produto _produto;
		private TituloEletronico _tituloEletronico;

		public int CapituloEletronicoId {
			get { return _capituloEletronicoId; }
			set { _capituloEletronicoId = value; }
		}

		[NotNullValidator]
		public Capitulo Capitulo {
			get { return _capitulo; }
			set { _capitulo = value; }
		}

		[NotNullValidator]
		public Produto Produto {
			get { return _produto; }
			set { _produto = value; }
		}

		[NotNullValidator]
		public TituloEletronico TituloEletronico {
			get { return _tituloEletronico; }
			set { _tituloEletronico = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<CapituloEletronico>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<CapituloEletronico>(this);
        }
	}
	
	public struct CapituloEletronicoColunas
	{	
		public static string CapituloEletronicoId = @"capituloEletronicoId";
		public static string TituloEletronicoId = @"tituloEletronicoId";
		public static string CapituloId = @"capituloId";
	}
}
		