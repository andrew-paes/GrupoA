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
	public partial class NotificacaoStatus 
	{
		// Construtor
		public NotificacaoStatus() {}

		// Construtor com identificador
		public NotificacaoStatus(int notificacaoStatusId) {
			_notificacaoStatusId = notificacaoStatusId;
		}

		private int _notificacaoStatusId;
		private string _statusNotificacao;
		private List<NotificacaoDisponibilidade> _notificacaoDisponibilidades;

		public int NotificacaoStatusId {
			get { return _notificacaoStatusId; }
			set { _notificacaoStatusId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 50)]
		public string StatusNotificacao {
			get { return _statusNotificacao; }
			set { _statusNotificacao = value; }
		}

		public List<NotificacaoDisponibilidade> NotificacaoDisponibilidades {
			get { return _notificacaoDisponibilidades; }
			set { _notificacaoDisponibilidades = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<NotificacaoStatus>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<NotificacaoStatus>(this);
        }
	}
	
	public struct NotificacaoStatusColunas
	{	
		public static string NotificacaoStatusId = @"notificacaoStatusId";
		public static string StatusNotificacao = @"statusNotificacao";
	}
}
		