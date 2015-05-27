
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
	public partial class PromocaoCupomPedidoFH : IFilterHelper
	{
		private string _promocaoCupomPedidoId;
		public string PromocaoCupomPedidoId {
			get { return _promocaoCupomPedidoId==null?String.Empty:_promocaoCupomPedidoId; }
			set { _promocaoCupomPedidoId=value; }
		}

		private string _promocaoCupomId;
		public string PromocaoCupomId {
			get { return _promocaoCupomId==null?String.Empty:_promocaoCupomId; }
			set { _promocaoCupomId=value; }
		}

		private string _pedidoId;
		public string PedidoId {
			get { return _pedidoId==null?String.Empty:_pedidoId; }
			set { _pedidoId=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!PromocaoCupomPedidoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (promocaoCupomPedidoId="+PromocaoCupomPedidoId+")");
			}

			if (!PromocaoCupomId.Equals(String.Empty)) {
				sbWhere.Append(" AND (promocaoCupomId="+PromocaoCupomId+")");
			}

			if (!PedidoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (pedidoId="+PedidoId+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
