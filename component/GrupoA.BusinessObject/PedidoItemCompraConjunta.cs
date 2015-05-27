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
	public partial class PedidoItemCompraConjunta 
	{
		private int _pedidoItemCompraConjuntaId;
		private CompraConjunta _compraConjunta;
		private PedidoItem _pedidoItem;

		public int PedidoItemCompraConjuntaId {
			get { return _pedidoItemCompraConjuntaId; }
			set { _pedidoItemCompraConjuntaId = value; }
		}

		[NotNullValidator]
		public CompraConjunta CompraConjunta {
			get { return _compraConjunta; }
			set { _compraConjunta = value; }
		}

		[NotNullValidator]
		public PedidoItem PedidoItem {
			get { return _pedidoItem; }
			set { _pedidoItem = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<PedidoItemCompraConjunta>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<PedidoItemCompraConjunta>(this);
        }
	}
	
	public struct PedidoItemCompraConjuntaColunas
	{	
		public static string PedidoItemCompraConjuntaId = @"pedidoItemCompraConjuntaId";
		public static string CompraConjuntaId = @"compraConjuntaId";
	}
}
		