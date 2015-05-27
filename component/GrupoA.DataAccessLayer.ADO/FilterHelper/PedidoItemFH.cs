
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
	public partial class PedidoItemFH : IFilterHelper
	{
		private string _pedidoItemId;
		public string PedidoItemId {
			get { return _pedidoItemId==null?String.Empty:_pedidoItemId; }
			set { _pedidoItemId=value; }
		}

		private string _produtoId;
		public string ProdutoId {
			get { return _produtoId==null?String.Empty:_produtoId; }
			set { _produtoId=value; }
		}

		private string _pedidoId;
		public string PedidoId {
			get { return _pedidoId==null?String.Empty:_pedidoId; }
			set { _pedidoId=value; }
		}

		private string _quantidade;
		public string Quantidade {
			get { return _quantidade==null?String.Empty:_quantidade; }
			set { _quantidade=value; }
		}

		private string _valorUnitarioBase;
		public string ValorUnitarioBase {
			get { return _valorUnitarioBase==null?String.Empty:_valorUnitarioBase; }
			set { _valorUnitarioBase=value; }
		}

		private string _valorUnitarioFinal;
		public string ValorUnitarioFinal {
			get { return _valorUnitarioFinal==null?String.Empty:_valorUnitarioFinal; }
			set { _valorUnitarioFinal=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!PedidoItemId.Equals(String.Empty)) {
				sbWhere.Append(" AND (pedidoItemId="+PedidoItemId+")");
			}

			if (!ProdutoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (produtoId="+ProdutoId+")");
			}

			if (!PedidoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (pedidoId="+PedidoId+")");
			}

			if (!Quantidade.Equals(String.Empty)) {
				sbWhere.Append(" AND (quantidade="+Quantidade+")");
			}

			if (!ValorUnitarioBase.Equals(String.Empty)) {
				sbWhere.Append(" AND (valorUnitarioBase="+ValorUnitarioBase+")");
			}

			if (!ValorUnitarioFinal.Equals(String.Empty)) {
				sbWhere.Append(" AND (valorUnitarioFinal="+ValorUnitarioFinal+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
