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
	public partial class AvisoDisponibilidade 
	{
		// Construtor
		public AvisoDisponibilidade() {}

		// Construtor com identificador
		public AvisoDisponibilidade(int avisoDisponibilidadeId) {
			_avisoDisponibilidadeId = avisoDisponibilidadeId;
		}

		private int _avisoDisponibilidadeId;
		private string _email;
		private DateTime? _dataSolicitacao;
		private DateTime? _dataNotificacao;
		private AvisoDisponibilidadeStatus _avisoDisponibilidadeStatus;
		private Produto _produto;

		public int AvisoDisponibilidadeId {
			get { return _avisoDisponibilidadeId; }
			set { _avisoDisponibilidadeId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 250)]
		public string Email {
			get { return _email; }
			set { _email = value; }
		}

		public DateTime? DataSolicitacao {
			get { return _dataSolicitacao; }
			set { _dataSolicitacao = value; }
		}

		public DateTime? DataNotificacao {
			get { return _dataNotificacao; }
			set { _dataNotificacao = value; }
		}

		[NotNullValidator]
		public AvisoDisponibilidadeStatus AvisoDisponibilidadeStatus {
			get { return _avisoDisponibilidadeStatus; }
			set { _avisoDisponibilidadeStatus = value; }
		}

		[NotNullValidator]
		public Produto Produto {
			get { return _produto; }
			set { _produto = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<AvisoDisponibilidade>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<AvisoDisponibilidade>(this);
        }
	}
	
	public struct AvisoDisponibilidadeColunas
	{	
		public static string AvisoDisponibilidadeId = @"avisoDisponibilidadeId";
		public static string Email = @"email";
		public static string DataSolicitacao = @"dataSolicitacao";
		public static string DataNotificacao = @"dataNotificacao";
		public static string ProdutoId = @"produtoId";
		public static string AvisoDisponibilidadeStatusId = @"avisoDisponibilidadeStatusId";
	}
}
		