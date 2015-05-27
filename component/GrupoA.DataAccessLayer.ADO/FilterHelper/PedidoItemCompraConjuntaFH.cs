
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
	public partial class PedidoItemCompraConjuntaFH : IFilterHelper
	{
		private string _pedidoItemCompraConjuntaId;
		public string PedidoItemCompraConjuntaId {
			get { return _pedidoItemCompraConjuntaId==null?String.Empty:_pedidoItemCompraConjuntaId; }
			set { _pedidoItemCompraConjuntaId=value; }
		}

		private string _compraConjuntaId;
		public string CompraConjuntaId {
			get { return _compraConjuntaId==null?String.Empty:_compraConjuntaId; }
			set { _compraConjuntaId=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!PedidoItemCompraConjuntaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (pedidoItemCompraConjuntaId="+PedidoItemCompraConjuntaId+")");
			}

			if (!CompraConjuntaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (compraConjuntaId="+CompraConjuntaId+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
