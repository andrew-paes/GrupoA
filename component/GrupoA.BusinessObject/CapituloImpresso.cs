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
	public partial class CapituloImpresso 
	{
		private int _capituloImpressoId;
		private Capitulo _capitulo;
		private Produto _produto;
		private TituloImpresso _tituloImpresso;

		public int CapituloImpressoId {
			get { return _capituloImpressoId; }
			set { _capituloImpressoId = value; }
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
		public TituloImpresso TituloImpresso {
			get { return _tituloImpresso; }
			set { _tituloImpresso = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<CapituloImpresso>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<CapituloImpresso>(this);
        }
	}
	
	public struct CapituloImpressoColunas
	{	
		public static string CapituloImpressoId = @"capituloImpressoId";
		public static string CapituloId = @"capituloId";
		public static string TituloImpressoId = @"tituloImpressoId";
	}
}
		