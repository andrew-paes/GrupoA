
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
	public partial class OfertaProdutoFH : IFilterHelper
	{
		private string _ofertaProdutoId;
		public string OfertaProdutoId {
			get { return _ofertaProdutoId==null?String.Empty:_ofertaProdutoId; }
			set { _ofertaProdutoId=value; }
		}

		private string _ofertaId;
		public string OfertaId {
			get { return _ofertaId==null?String.Empty:_ofertaId; }
			set { _ofertaId=value; }
		}

		private string _produtoId;
		public string ProdutoId {
			get { return _produtoId==null?String.Empty:_produtoId; }
			set { _produtoId=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!OfertaProdutoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (ofertaProdutoId="+OfertaProdutoId+")");
			}

			if (!OfertaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (ofertaId="+OfertaId+")");
			}

			if (!ProdutoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (produtoId="+ProdutoId+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
