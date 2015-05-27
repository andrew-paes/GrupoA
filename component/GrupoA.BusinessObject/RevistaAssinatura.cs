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
	public partial class RevistaAssinatura 
	{
		private int _revistaAssinaturaId;
		private int? _numeroExemplares;
		private string _descricaoAssinatura;
		private Produto _produto;
		private Revista _revista;

		public int RevistaAssinaturaId {
			get { return _revistaAssinaturaId; }
			set { _revistaAssinaturaId = value; }
		}

		public int? NumeroExemplares {
			get { return _numeroExemplares; }
			set { _numeroExemplares = value; }
		}

		public string DescricaoAssinatura {
			get { return _descricaoAssinatura; }
			set { _descricaoAssinatura = value; }
		}

		[NotNullValidator]
		public Produto Produto {
			get { return _produto; }
			set { _produto = value; }
		}

		[NotNullValidator]
		public Revista Revista {
			get { return _revista; }
			set { _revista = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<RevistaAssinatura>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<RevistaAssinatura>(this);
        }
	}
	
	public struct RevistaAssinaturaColunas
	{	
		public static string RevistaAssinaturaId = @"revistaAssinaturaId";
		public static string RevistaId = @"revistaId";
		public static string NumeroExemplares = @"numeroExemplares";
		public static string DescricaoAssinatura = @"descricaoAssinatura";
	}
}
		