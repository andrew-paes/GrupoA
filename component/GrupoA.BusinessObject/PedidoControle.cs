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
	public partial class PedidoControle 
	{
		private int _pedidoId;
		private DateTime? _dataHoraExportacao;
		private Pedido _pedido;

		public int PedidoId {
			get { return _pedidoId; }
			set { _pedidoId = value; }
		}

		public DateTime? DataHoraExportacao {
			get { return _dataHoraExportacao; }
			set { _dataHoraExportacao = value; }
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
            get { return Validation.Validate<PedidoControle>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<PedidoControle>(this);
        }
	}
	
	public struct PedidoControleColunas
	{	
		public static string PedidoId = @"pedidoId";
		public static string DataHoraExportacao = @"dataHoraExportacao";
	}
}
		