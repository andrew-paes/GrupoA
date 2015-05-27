
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
	public partial class EventoFH : IFilterHelper
	{
		private string _eventoId;
		public string EventoId {
			get { return _eventoId==null?String.Empty:_eventoId; }
			set { _eventoId=value; }
		}

		private string _dataEventoInicioPeriodoInicial;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataEventoInicioPeriodoInicial {
			get { return _dataEventoInicioPeriodoInicial==null?String.Empty:_dataEventoInicioPeriodoInicial; }
			set { _dataEventoInicioPeriodoInicial=value; }
		}
		private string _dataEventoInicioPeriodoFinal;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataEventoInicioPeriodoFinal {
			get { return _dataEventoInicioPeriodoFinal==null?String.Empty:_dataEventoInicioPeriodoFinal; }
			set { _dataEventoInicioPeriodoFinal=value; }
		}

		private string _dataEventoFimPeriodoInicial;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataEventoFimPeriodoInicial {
			get { return _dataEventoFimPeriodoInicial==null?String.Empty:_dataEventoFimPeriodoInicial; }
			set { _dataEventoFimPeriodoInicial=value; }
		}
		private string _dataEventoFimPeriodoFinal;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataEventoFimPeriodoFinal {
			get { return _dataEventoFimPeriodoFinal==null?String.Empty:_dataEventoFimPeriodoFinal; }
			set { _dataEventoFimPeriodoFinal=value; }
		}

		private string _local;
		public string Local {
			get { return _local==null?String.Empty:_local; }
			set { _local=value; }
		}

		private string _arquivoIdThumb;
		public string ArquivoIdThumb {
			get { return _arquivoIdThumb==null?String.Empty:_arquivoIdThumb; }
			set { _arquivoIdThumb=value; }
		}

		private string _exibeFormularioContato;
		public string ExibeFormularioContato {
			get { return _exibeFormularioContato==null?String.Empty:_exibeFormularioContato; }
			set { _exibeFormularioContato=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!EventoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (eventoId="+EventoId+")");
			}

			if( !DataEventoInicioPeriodoInicial.Equals(String.Empty) && !DataEventoInicioPeriodoFinal.Equals(String.Empty)) {
				sbWhere.Append(" AND (dataEventoInicio >='"+DataEventoInicioPeriodoInicial+"'");
				sbWhere.Append(" AND dataEventoInicio <='"+DataEventoInicioPeriodoFinal+"')");
			} else if (!DataEventoInicioPeriodoInicial.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataEventoInicio >='"+DataEventoInicioPeriodoInicial+"')");
			} else if (!DataEventoInicioPeriodoFinal.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataEventoInicio <='"+DataEventoInicioPeriodoFinal+"')");
			}

			if( !DataEventoFimPeriodoInicial.Equals(String.Empty) && !DataEventoFimPeriodoFinal.Equals(String.Empty)) {
				sbWhere.Append(" AND (dataEventoFim >='"+DataEventoFimPeriodoInicial+"'");
				sbWhere.Append(" AND dataEventoFim <='"+DataEventoFimPeriodoFinal+"')");
			} else if (!DataEventoFimPeriodoInicial.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataEventoFim >='"+DataEventoFimPeriodoInicial+"')");
			} else if (!DataEventoFimPeriodoFinal.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataEventoFim <='"+DataEventoFimPeriodoFinal+"')");
			}

			if (!Local.Equals(String.Empty)) {
				sbWhere.Append(" AND (local LIKE '%"+Local+"%')");
			}

			if (!ArquivoIdThumb.Equals(String.Empty)) {
				sbWhere.Append(" AND (arquivoIdThumb="+ArquivoIdThumb+")");
			}

			if (!ExibeFormularioContato.Equals(String.Empty)) {
				sbWhere.Append(" AND (exibeFormularioContato LIKE '%"+ExibeFormularioContato+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
