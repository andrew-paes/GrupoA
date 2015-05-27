
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
	public partial class PedidoItemPromocaoFH : IFilterHelper
	{
		private string _pedidoItemPromocaoId;
		public string PedidoItemPromocaoId {
			get { return _pedidoItemPromocaoId==null?String.Empty:_pedidoItemPromocaoId; }
			set { _pedidoItemPromocaoId=value; }
		}

		private string _freteGratis;
		public string FreteGratis {
			get { return _freteGratis==null?String.Empty:_freteGratis; }
			set { _freteGratis=value; }
		}

		private string _descontoPercentual;
		public string DescontoPercentual {
			get { return _descontoPercentual==null?String.Empty:_descontoPercentual; }
			set { _descontoPercentual=value; }
		}

		private string _descontoValor;
		public string DescontoValor {
			get { return _descontoValor==null?String.Empty:_descontoValor; }
			set { _descontoValor=value; }
		}

		private string _promocaoId;
		public string PromocaoId {
			get { return _promocaoId==null?String.Empty:_promocaoId; }
			set { _promocaoId=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!PedidoItemPromocaoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (pedidoItemPromocaoId="+PedidoItemPromocaoId+")");
			}

			if (!FreteGratis.Equals(String.Empty)) {
				sbWhere.Append(" AND (freteGratis LIKE '%"+FreteGratis+"%')");
			}

			if (!DescontoPercentual.Equals(String.Empty)) {
				sbWhere.Append(" AND (descontoPercentual="+DescontoPercentual+")");
			}

			if (!DescontoValor.Equals(String.Empty)) {
				sbWhere.Append(" AND (descontoValor="+DescontoValor+")");
			}

			if (!PromocaoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (promocaoId="+PromocaoId+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
