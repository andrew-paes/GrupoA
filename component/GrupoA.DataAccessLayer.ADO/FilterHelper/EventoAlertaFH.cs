
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
	public partial class EventoAlertaFH : IFilterHelper
	{
		private string _eventoAlertaId;
		public string EventoAlertaId {
			get { return _eventoAlertaId==null?String.Empty:_eventoAlertaId; }
			set { _eventoAlertaId=value; }
		}

		private string _usuarioId;
		public string UsuarioId {
			get { return _usuarioId==null?String.Empty:_usuarioId; }
			set { _usuarioId=value; }
		}

		private string _eventoId;
		public string EventoId {
			get { return _eventoId==null?String.Empty:_eventoId; }
			set { _eventoId=value; }
		}

		private string _dias;
		public string Dias {
			get { return _dias==null?String.Empty:_dias; }
			set { _dias=value; }
		}

		private string _ativo;
		public string Ativo {
			get { return _ativo==null?String.Empty:_ativo; }
			set { _ativo=value; }
		}

		private string _dataHoraEncaminhamentoPeriodoInicial;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataHoraEncaminhamentoPeriodoInicial {
			get { return _dataHoraEncaminhamentoPeriodoInicial==null?String.Empty:_dataHoraEncaminhamentoPeriodoInicial; }
			set { _dataHoraEncaminhamentoPeriodoInicial=value; }
		}
		private string _dataHoraEncaminhamentoPeriodoFinal;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataHoraEncaminhamentoPeriodoFinal {
			get { return _dataHoraEncaminhamentoPeriodoFinal==null?String.Empty:_dataHoraEncaminhamentoPeriodoFinal; }
			set { _dataHoraEncaminhamentoPeriodoFinal=value; }
		}

		private string _dataHoraCancelamentoPeriodoInicial;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataHoraCancelamentoPeriodoInicial {
			get { return _dataHoraCancelamentoPeriodoInicial==null?String.Empty:_dataHoraCancelamentoPeriodoInicial; }
			set { _dataHoraCancelamentoPeriodoInicial=value; }
		}
		private string _dataHoraCancelamentoPeriodoFinal;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataHoraCancelamentoPeriodoFinal {
			get { return _dataHoraCancelamentoPeriodoFinal==null?String.Empty:_dataHoraCancelamentoPeriodoFinal; }
			set { _dataHoraCancelamentoPeriodoFinal=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!EventoAlertaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (eventoAlertaId="+EventoAlertaId+")");
			}

			if (!UsuarioId.Equals(String.Empty)) {
				sbWhere.Append(" AND (usuarioId="+UsuarioId+")");
			}

			if (!EventoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (eventoId="+EventoId+")");
			}

			if (!Dias.Equals(String.Empty)) {
				sbWhere.Append(" AND (dias="+Dias+")");
			}

			if (!Ativo.Equals(String.Empty)) {
				sbWhere.Append(" AND (ativo LIKE '%"+Ativo+"%')");
			}

			if( !DataHoraEncaminhamentoPeriodoInicial.Equals(String.Empty) && !DataHoraEncaminhamentoPeriodoFinal.Equals(String.Empty)) {
				sbWhere.Append(" AND (dataHoraEncaminhamento >='"+DataHoraEncaminhamentoPeriodoInicial+"'");
				sbWhere.Append(" AND dataHoraEncaminhamento <='"+DataHoraEncaminhamentoPeriodoFinal+"')");
			} else if (!DataHoraEncaminhamentoPeriodoInicial.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataHoraEncaminhamento >='"+DataHoraEncaminhamentoPeriodoInicial+"')");
			} else if (!DataHoraEncaminhamentoPeriodoFinal.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataHoraEncaminhamento <='"+DataHoraEncaminhamentoPeriodoFinal+"')");
			}

			if( !DataHoraCancelamentoPeriodoInicial.Equals(String.Empty) && !DataHoraCancelamentoPeriodoFinal.Equals(String.Empty)) {
				sbWhere.Append(" AND (dataHoraCancelamento >='"+DataHoraCancelamentoPeriodoInicial+"'");
				sbWhere.Append(" AND dataHoraCancelamento <='"+DataHoraCancelamentoPeriodoFinal+"')");
			} else if (!DataHoraCancelamentoPeriodoInicial.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataHoraCancelamento >='"+DataHoraCancelamentoPeriodoInicial+"')");
			} else if (!DataHoraCancelamentoPeriodoFinal.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataHoraCancelamento <='"+DataHoraCancelamentoPeriodoFinal+"')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
