
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
	public partial class PedidoSituacaoFH : IFilterHelper
	{
		private string _pedidoSituacaoId;
		public string PedidoSituacaoId {
			get { return _pedidoSituacaoId==null?String.Empty:_pedidoSituacaoId; }
			set { _pedidoSituacaoId=value; }
		}

		private string _pedidoId;
		public string PedidoId {
			get { return _pedidoId==null?String.Empty:_pedidoId; }
			set { _pedidoId=value; }
		}

		private string _pedidoStatusId;
		public string PedidoStatusId {
			get { return _pedidoStatusId==null?String.Empty:_pedidoStatusId; }
			set { _pedidoStatusId=value; }
		}

		private string _dataHoraAlteracaoPeriodoInicial;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataHoraAlteracaoPeriodoInicial {
			get { return _dataHoraAlteracaoPeriodoInicial==null?String.Empty:_dataHoraAlteracaoPeriodoInicial; }
			set { _dataHoraAlteracaoPeriodoInicial=value; }
		}
		private string _dataHoraAlteracaoPeriodoFinal;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataHoraAlteracaoPeriodoFinal {
			get { return _dataHoraAlteracaoPeriodoFinal==null?String.Empty:_dataHoraAlteracaoPeriodoFinal; }
			set { _dataHoraAlteracaoPeriodoFinal=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!PedidoSituacaoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (pedidoSituacaoId="+PedidoSituacaoId+")");
			}

			if (!PedidoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (pedidoId="+PedidoId+")");
			}

			if (!PedidoStatusId.Equals(String.Empty)) {
				sbWhere.Append(" AND (pedidoStatusId="+PedidoStatusId+")");
			}

			if( !DataHoraAlteracaoPeriodoInicial.Equals(String.Empty) && !DataHoraAlteracaoPeriodoFinal.Equals(String.Empty)) {
				sbWhere.Append(" AND (dataHoraAlteracao >='"+DataHoraAlteracaoPeriodoInicial+"'");
				sbWhere.Append(" AND dataHoraAlteracao <='"+DataHoraAlteracaoPeriodoFinal+"')");
			} else if (!DataHoraAlteracaoPeriodoInicial.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataHoraAlteracao >='"+DataHoraAlteracaoPeriodoInicial+"')");
			} else if (!DataHoraAlteracaoPeriodoFinal.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataHoraAlteracao <='"+DataHoraAlteracaoPeriodoFinal+"')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
