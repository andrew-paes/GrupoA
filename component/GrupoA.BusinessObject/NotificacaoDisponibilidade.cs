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
	public partial class NotificacaoDisponibilidade 
	{
		// Construtor
		public NotificacaoDisponibilidade() {}

		// Construtor com identificador
		public NotificacaoDisponibilidade(int notificacaoDisponibilidadeId) {
			_notificacaoDisponibilidadeId = notificacaoDisponibilidadeId;
		}

		private int _notificacaoDisponibilidadeId;
		private DateTime _dataHoraSolicitacao;
		private NotificacaoStatus _notificacaoStatus;
		private Produto _produto;
		private Usuario _usuario;

		public int NotificacaoDisponibilidadeId {
			get { return _notificacaoDisponibilidadeId; }
			set { _notificacaoDisponibilidadeId = value; }
		}

		[NotNullValidator]
		public DateTime DataHoraSolicitacao {
			get { return _dataHoraSolicitacao; }
			set { _dataHoraSolicitacao = value; }
		}

		[NotNullValidator]
		public NotificacaoStatus NotificacaoStatus {
			get { return _notificacaoStatus; }
			set { _notificacaoStatus = value; }
		}

		[NotNullValidator]
		public Produto Produto {
			get { return _produto; }
			set { _produto = value; }
		}

		[NotNullValidator]
		public Usuario Usuario {
			get { return _usuario; }
			set { _usuario = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<NotificacaoDisponibilidade>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<NotificacaoDisponibilidade>(this);
        }
	}
	
	public struct NotificacaoDisponibilidadeColunas
	{	
		public static string NotificacaoDisponibilidadeId = @"notificacaoDisponibilidadeId";
		public static string ProdutoId = @"produtoId";
		public static string UsuarioId = @"usuarioId";
		public static string DataHoraSolicitacao = @"dataHoraSolicitacao";
		public static string NotificacaoStatusId = @"notificacaoStatusId";
	}
}
		