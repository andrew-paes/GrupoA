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
	public partial class ConfiguracaoValor 
	{
		private int _configuracaoId;
		private string _valor;
		private Configuracao _configuracao;

		public int ConfiguracaoId {
			get { return _configuracaoId; }
			set { _configuracaoId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 250)]
		public string Valor {
			get { return _valor; }
			set { _valor = value; }
		}

		[NotNullValidator]
		public Configuracao Configuracao {
			get { return _configuracao; }
			set { _configuracao = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<ConfiguracaoValor>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<ConfiguracaoValor>(this);
        }
	}
	
	public struct ConfiguracaoValorColunas
	{	
		public static string ConfiguracaoId = @"configuracaoId";
		public static string Valor = @"valor";
	}
}
		