
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
	public partial class PedidoControleFH : IFilterHelper
	{
		private string _pedidoId;
		public string PedidoId {
			get { return _pedidoId==null?String.Empty:_pedidoId; }
			set { _pedidoId=value; }
		}

		private string _dataHoraExportacaoPeriodoInicial;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataHoraExportacaoPeriodoInicial {
			get { return _dataHoraExportacaoPeriodoInicial==null?String.Empty:_dataHoraExportacaoPeriodoInicial; }
			set { _dataHoraExportacaoPeriodoInicial=value; }
		}
		private string _dataHoraExportacaoPeriodoFinal;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataHoraExportacaoPeriodoFinal {
			get { return _dataHoraExportacaoPeriodoFinal==null?String.Empty:_dataHoraExportacaoPeriodoFinal; }
			set { _dataHoraExportacaoPeriodoFinal=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!PedidoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (pedidoId="+PedidoId+")");
			}

			if( !DataHoraExportacaoPeriodoInicial.Equals(String.Empty) && !DataHoraExportacaoPeriodoFinal.Equals(String.Empty)) {
				sbWhere.Append(" AND (dataHoraExportacao >='"+DataHoraExportacaoPeriodoInicial+"'");
				sbWhere.Append(" AND dataHoraExportacao <='"+DataHoraExportacaoPeriodoFinal+"')");
			} else if (!DataHoraExportacaoPeriodoInicial.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataHoraExportacao >='"+DataHoraExportacaoPeriodoInicial+"')");
			} else if (!DataHoraExportacaoPeriodoFinal.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataHoraExportacao <='"+DataHoraExportacaoPeriodoFinal+"')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
