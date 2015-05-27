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
	public partial class PromocaoCupomPedido 
	{
		// Construtor
		public PromocaoCupomPedido() {}

		// Construtor com identificador
		public PromocaoCupomPedido(int promocaoCupomPedidoId) {
			_promocaoCupomPedidoId = promocaoCupomPedidoId;
		}

		private int _promocaoCupomPedidoId;
		private Pedido _pedido;
		private PromocaoCupom _promocaoCupom;

		public int PromocaoCupomPedidoId {
			get { return _promocaoCupomPedidoId; }
			set { _promocaoCupomPedidoId = value; }
		}

		public Pedido Pedido {
			get { return _pedido; }
			set { _pedido = value; }
		}

		[NotNullValidator]
		public PromocaoCupom PromocaoCupom {
			get { return _promocaoCupom; }
			set { _promocaoCupom = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<PromocaoCupomPedido>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<PromocaoCupomPedido>(this);
        }
	}
	
	public struct PromocaoCupomPedidoColunas
	{	
		public static string PromocaoCupomPedidoId = @"promocaoCupomPedidoId";
		public static string PromocaoCupomId = @"promocaoCupomId";
		public static string PedidoId = @"pedidoId";
	}
}
		