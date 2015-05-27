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
	public partial class TituloSolicitacaoStatus 
	{
		// Construtor
		public TituloSolicitacaoStatus() {}

		// Construtor com identificador
		public TituloSolicitacaoStatus(int tituloSolicitacaoStatusId) {
			_tituloSolicitacaoStatusId = tituloSolicitacaoStatusId;
		}

		private int _tituloSolicitacaoStatusId;
		private string _statusSolicitacao;
		private List<TituloSolicitacao> _tituloSolicitacoes;

		public int TituloSolicitacaoStatusId {
			get { return _tituloSolicitacaoStatusId; }
			set { _tituloSolicitacaoStatusId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 50)]
		public string StatusSolicitacao {
			get { return _statusSolicitacao; }
			set { _statusSolicitacao = value; }
		}

		public List<TituloSolicitacao> TituloSolicitacoes {
			get { return _tituloSolicitacoes; }
			set { _tituloSolicitacoes = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<TituloSolicitacaoStatus>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<TituloSolicitacaoStatus>(this);
        }
	}
	
	public struct TituloSolicitacaoStatusColunas
	{	
		public static string TituloSolicitacaoStatusId = @"tituloSolicitacaoStatusId";
		public static string StatusSolicitacao = @"statusSolicitacao";
	}
}
		