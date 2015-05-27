
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
	public partial class CarrinhoItemFH : IFilterHelper
	{
		private string _carrinhoItemId;
		public string CarrinhoItemId {
			get { return _carrinhoItemId==null?String.Empty:_carrinhoItemId; }
			set { _carrinhoItemId=value; }
		}

		private string _carrinhoId;
		public string CarrinhoId {
			get { return _carrinhoId==null?String.Empty:_carrinhoId; }
			set { _carrinhoId=value; }
		}

		private string _produtoId;
		public string ProdutoId {
			get { return _produtoId==null?String.Empty:_produtoId; }
			set { _produtoId=value; }
		}

		private string _quantidade;
		public string Quantidade {
			get { return _quantidade==null?String.Empty:_quantidade; }
			set { _quantidade=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!CarrinhoItemId.Equals(String.Empty)) {
				sbWhere.Append(" AND (carrinhoItemId="+CarrinhoItemId+")");
			}

			if (!CarrinhoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (carrinhoId="+CarrinhoId+")");
			}

			if (!ProdutoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (produtoId="+ProdutoId+")");
			}

			if (!Quantidade.Equals(String.Empty)) {
				sbWhere.Append(" AND (quantidade="+Quantidade+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
