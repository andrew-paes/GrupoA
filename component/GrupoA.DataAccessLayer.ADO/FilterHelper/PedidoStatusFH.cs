
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
using System;
using System.Text;

namespace GrupoA.FilterHelper
{
	public partial class PedidoStatusFH : IFilterHelper
	{
		private string _pedidoStatusId;
		public string PedidoStatusId {
			get { return _pedidoStatusId==null?String.Empty:_pedidoStatusId; }
			set { _pedidoStatusId=value; }
		}

		private string _statusPedido;
		public string StatusPedido {
			get { return _statusPedido==null?String.Empty:_statusPedido; }
			set { _statusPedido=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!PedidoStatusId.Equals(String.Empty)) {
				sbWhere.Append(" AND (pedidoStatusId="+PedidoStatusId+")");
			}

			if (!StatusPedido.Equals(String.Empty)) {
				sbWhere.Append(" AND (statusPedido LIKE '%"+StatusPedido+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
