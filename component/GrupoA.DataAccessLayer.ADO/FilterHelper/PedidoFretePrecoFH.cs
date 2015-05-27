
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
	public partial class PedidoFretePrecoFH : IFilterHelper
	{
		private string _pedidoFretePrecoId;
		public string PedidoFretePrecoId {
			get { return _pedidoFretePrecoId==null?String.Empty:_pedidoFretePrecoId; }
			set { _pedidoFretePrecoId=value; }
		}

		private string _pedidoFreteGrupoId;
		public string PedidoFreteGrupoId {
			get { return _pedidoFreteGrupoId==null?String.Empty:_pedidoFreteGrupoId; }
			set { _pedidoFreteGrupoId=value; }
		}

		private string _peso;
		public string Peso {
			get { return _peso==null?String.Empty:_peso; }
			set { _peso=value; }
		}

		private string _preco;
		public string Preco {
			get { return _preco==null?String.Empty:_preco; }
			set { _preco=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!PedidoFretePrecoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (pedidoFretePrecoId="+PedidoFretePrecoId+")");
			}

			if (!PedidoFreteGrupoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (pedidoFreteGrupoId="+PedidoFreteGrupoId+")");
			}

			if (!Peso.Equals(String.Empty)) {
				sbWhere.Append(" AND (peso="+Peso+")");
			}

			if (!Preco.Equals(String.Empty)) {
				sbWhere.Append(" AND (preco="+Preco+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
