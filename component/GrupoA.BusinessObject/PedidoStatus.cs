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
	public partial class PedidoStatus 
	{
		// Construtor
		public PedidoStatus() {}

		// Construtor com identificador
		public PedidoStatus(int pedidoStatusId) {
			_pedidoStatusId = pedidoStatusId;
		}

		private int _pedidoStatusId;
		private string _statusPedido;
		private List<Pedido> _pedidos;
		private List<PedidoSituacao> _pedidoSituacoes;

		public int PedidoStatusId {
			get { return _pedidoStatusId; }
			set { _pedidoStatusId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 100)]
		public string StatusPedido {
			get { return _statusPedido; }
			set { _statusPedido = value; }
		}

		public List<Pedido> Pedidos {
			get { return _pedidos; }
			set { _pedidos = value; }
		}

		public List<PedidoSituacao> PedidoSituacoes {
			get { return _pedidoSituacoes; }
			set { _pedidoSituacoes = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<PedidoStatus>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<PedidoStatus>(this);
        }
	}
	
	public struct PedidoStatusColunas
	{	
		public static string PedidoStatusId = @"pedidoStatusId";
		public static string StatusPedido = @"statusPedido";
	}
}
		