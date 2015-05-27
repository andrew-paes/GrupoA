
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
	public partial class CarrinhoItemCompraConjuntaFH : IFilterHelper
	{
		private string _carrinhoItemCompraConjuntaId;
		public string CarrinhoItemCompraConjuntaId {
			get { return _carrinhoItemCompraConjuntaId==null?String.Empty:_carrinhoItemCompraConjuntaId; }
			set { _carrinhoItemCompraConjuntaId=value; }
		}

		private string _compraConjuntaId;
		public string CompraConjuntaId {
			get { return _compraConjuntaId==null?String.Empty:_compraConjuntaId; }
			set { _compraConjuntaId=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!CarrinhoItemCompraConjuntaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (carrinhoItemCompraConjuntaId="+CarrinhoItemCompraConjuntaId+")");
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
