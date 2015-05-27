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
	public partial class Configuracao 
	{
		// Construtor
		public Configuracao() {}

		// Construtor com identificador
		public Configuracao(int configuracaoId) {
			_configuracaoId = configuracaoId;
		}

		private int _configuracaoId;
		private string _chave;
		private string _descricaoConfiguracao;
		private ConfiguracaoValor _configuracaoValor;

		public int ConfiguracaoId {
			get { return _configuracaoId; }
			set { _configuracaoId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 50)]
		public string Chave {
			get { return _chave; }
			set { _chave = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 250)]
		public string DescricaoConfiguracao {
			get { return _descricaoConfiguracao; }
			set { _descricaoConfiguracao = value; }
		}

		[NotNullValidator]
		public ConfiguracaoValor ConfiguracaoValor {
			get { return _configuracaoValor; }
			set { _configuracaoValor = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<Configuracao>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<Configuracao>(this);
        }
	}
	
	public struct ConfiguracaoColunas
	{	
		public static string ConfiguracaoId = @"configuracaoId";
		public static string Chave = @"chave";
		public static string DescricaoConfiguracao = @"descricaoConfiguracao";
	}
}
		