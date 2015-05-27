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
	public partial class PedidoSituacao 
	{
		// Construtor
		public PedidoSituacao() {}

		// Construtor com identificador
		public PedidoSituacao(int pedidoSituacaoId) {
			_pedidoSituacaoId = pedidoSituacaoId;
		}

		private int _pedidoSituacaoId;
		private DateTime _dataHoraAlteracao;
		private Pedido _pedido;
		private PedidoStatus _pedidoStatus;

		public int PedidoSituacaoId {
			get { return _pedidoSituacaoId; }
			set { _pedidoSituacaoId = value; }
		}

		[NotNullValidator]
		public DateTime DataHoraAlteracao {
			get { return _dataHoraAlteracao; }
			set { _dataHoraAlteracao = value; }
		}

		[NotNullValidator]
		public Pedido Pedido {
			get { return _pedido; }
			set { _pedido = value; }
		}

		[NotNullValidator]
		public PedidoStatus PedidoStatus {
			get { return _pedidoStatus; }
			set { _pedidoStatus = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<PedidoSituacao>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<PedidoSituacao>(this);
        }
	}
	
	public struct PedidoSituacaoColunas
	{	
		public static string PedidoSituacaoId = @"pedidoSituacaoId";
		public static string PedidoId = @"pedidoId";
		public static string PedidoStatusId = @"pedidoStatusId";
		public static string DataHoraAlteracao = @"dataHoraAlteracao";
	}
}
		