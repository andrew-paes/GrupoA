
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
	public partial class CarrinhoStatusFH : IFilterHelper
	{
		private string _carrinhoStatusId;
		public string CarrinhoStatusId {
			get { return _carrinhoStatusId==null?String.Empty:_carrinhoStatusId; }
			set { _carrinhoStatusId=value; }
		}

		private string _statusCarrinho;
		public string StatusCarrinho {
			get { return _statusCarrinho==null?String.Empty:_statusCarrinho; }
			set { _statusCarrinho=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!CarrinhoStatusId.Equals(String.Empty)) {
				sbWhere.Append(" AND (carrinhoStatusId="+CarrinhoStatusId+")");
			}

			if (!StatusCarrinho.Equals(String.Empty)) {
				sbWhere.Append(" AND (statusCarrinho LIKE '%"+StatusCarrinho+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
