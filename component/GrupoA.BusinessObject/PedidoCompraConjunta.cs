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
	public partial class PedidoCompraConjunta 
	{
		private int _pedidoCompraConjuntaId;
		private DateTime? _dataHoraNotificacaoFinalizacao;
		private bool _fechamentoSincronizado;
		private string _tokenCofre;
		private int _numeroTentativa;
		private CompraConjunta _compraConjunta;
		private CompraConjuntaDesconto _compraConjuntaDesconto;
		private Pedido _pedido;

		public int PedidoCompraConjuntaId {
			get { return _pedidoCompraConjuntaId; }
			set { _pedidoCompraConjuntaId = value; }
		}

		public DateTime? DataHoraNotificacaoFinalizacao {
			get { return _dataHoraNotificacaoFinalizacao; }
			set { _dataHoraNotificacaoFinalizacao = value; }
		}

		public bool FechamentoSincronizado {
			get { return _fechamentoSincronizado; }
			set { _fechamentoSincronizado = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 24)]
		public string TokenCofre {
			get { return _tokenCofre; }
			set { _tokenCofre = value; }
		}

		public int NumeroTentativa {
			get { return _numeroTentativa; }
			set { _numeroTentativa = value; }
		}

		[NotNullValidator]
		public CompraConjunta CompraConjunta {
			get { return _compraConjunta; }
			set { _compraConjunta = value; }
		}

		public CompraConjuntaDesconto CompraConjuntaDesconto {
			get { return _compraConjuntaDesconto; }
			set { _compraConjuntaDesconto = value; }
		}

		[NotNullValidator]
		public Pedido Pedido {
			get { return _pedido; }
			set { _pedido = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<PedidoCompraConjunta>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<PedidoCompraConjunta>(this);
        }
	}
	
	public struct PedidoCompraConjuntaColunas
	{	
		public static string PedidoCompraConjuntaId = @"pedidoCompraConjuntaId";
		public static string CompraConjuntaDescontoId = @"compraConjuntaDescontoId";
		public static string DataHoraNotificacaoFinalizacao = @"dataHoraNotificacaoFinalizacao";
		public static string CompraConjuntaId = @"compraConjuntaId";
		public static string FechamentoSincronizado = @"fechamentoSincronizado";
		public static string TokenCofre = @"tokenCofre";
		public static string NumeroTentativa = @"numeroTentativa";
	}
}
		