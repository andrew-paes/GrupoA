
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
	public partial class PedidoCompraConjuntaFH : IFilterHelper
	{
		private string _pedidoCompraConjuntaId;
		public string PedidoCompraConjuntaId {
			get { return _pedidoCompraConjuntaId==null?String.Empty:_pedidoCompraConjuntaId; }
			set { _pedidoCompraConjuntaId=value; }
		}

		private string _compraConjuntaDescontoId;
		public string CompraConjuntaDescontoId {
			get { return _compraConjuntaDescontoId==null?String.Empty:_compraConjuntaDescontoId; }
			set { _compraConjuntaDescontoId=value; }
		}

		private string _dataHoraNotificacaoFinalizacaoPeriodoInicial;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataHoraNotificacaoFinalizacaoPeriodoInicial {
			get { return _dataHoraNotificacaoFinalizacaoPeriodoInicial==null?String.Empty:_dataHoraNotificacaoFinalizacaoPeriodoInicial; }
			set { _dataHoraNotificacaoFinalizacaoPeriodoInicial=value; }
		}
		private string _dataHoraNotificacaoFinalizacaoPeriodoFinal;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataHoraNotificacaoFinalizacaoPeriodoFinal {
			get { return _dataHoraNotificacaoFinalizacaoPeriodoFinal==null?String.Empty:_dataHoraNotificacaoFinalizacaoPeriodoFinal; }
			set { _dataHoraNotificacaoFinalizacaoPeriodoFinal=value; }
		}

		private string _compraConjuntaId;
		public string CompraConjuntaId {
			get { return _compraConjuntaId==null?String.Empty:_compraConjuntaId; }
			set { _compraConjuntaId=value; }
		}

		private string _fechamentoSincronizado;
		public string FechamentoSincronizado {
			get { return _fechamentoSincronizado==null?String.Empty:_fechamentoSincronizado; }
			set { _fechamentoSincronizado=value; }
		}

		private string _tokenCofre;
		public string TokenCofre {
			get { return _tokenCofre==null?String.Empty:_tokenCofre; }
			set { _tokenCofre=value; }
		}

		private string _numeroTentativa;
		public string NumeroTentativa {
			get { return _numeroTentativa==null?String.Empty:_numeroTentativa; }
			set { _numeroTentativa=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!PedidoCompraConjuntaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (pedidoCompraConjuntaId="+PedidoCompraConjuntaId+")");
			}

			if (!CompraConjuntaDescontoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (compraConjuntaDescontoId="+CompraConjuntaDescontoId+")");
			}

			if( !DataHoraNotificacaoFinalizacaoPeriodoInicial.Equals(String.Empty) && !DataHoraNotificacaoFinalizacaoPeriodoFinal.Equals(String.Empty)) {
				sbWhere.Append(" AND (dataHoraNotificacaoFinalizacao >='"+DataHoraNotificacaoFinalizacaoPeriodoInicial+"'");
				sbWhere.Append(" AND dataHoraNotificacaoFinalizacao <='"+DataHoraNotificacaoFinalizacaoPeriodoFinal+"')");
			} else if (!DataHoraNotificacaoFinalizacaoPeriodoInicial.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataHoraNotificacaoFinalizacao >='"+DataHoraNotificacaoFinalizacaoPeriodoInicial+"')");
			} else if (!DataHoraNotificacaoFinalizacaoPeriodoFinal.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataHoraNotificacaoFinalizacao <='"+DataHoraNotificacaoFinalizacaoPeriodoFinal+"')");
			}

			if (!CompraConjuntaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (compraConjuntaId="+CompraConjuntaId+")");
			}

			if (!FechamentoSincronizado.Equals(String.Empty)) {
				sbWhere.Append(" AND (fechamentoSincronizado LIKE '%"+FechamentoSincronizado+"%')");
			}

			if (!TokenCofre.Equals(String.Empty)) {
				sbWhere.Append(" AND (tokenCofre LIKE '%"+TokenCofre+"%')");
			}

			if (!NumeroTentativa.Equals(String.Empty)) {
				sbWhere.Append(" AND (numeroTentativa="+NumeroTentativa+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
