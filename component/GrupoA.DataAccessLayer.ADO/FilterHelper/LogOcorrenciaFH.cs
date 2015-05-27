
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
	public partial class LogOcorrenciaFH : IFilterHelper
	{
		private string _logOcorrenciaId;
		public string LogOcorrenciaId {
			get { return _logOcorrenciaId==null?String.Empty:_logOcorrenciaId; }
			set { _logOcorrenciaId=value; }
		}

		private string _logEventoId;
		public string LogEventoId {
			get { return _logEventoId==null?String.Empty:_logEventoId; }
			set { _logEventoId=value; }
		}

		private string _dataHoraOcorrenciaPeriodoInicial;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataHoraOcorrenciaPeriodoInicial {
			get { return _dataHoraOcorrenciaPeriodoInicial==null?String.Empty:_dataHoraOcorrenciaPeriodoInicial; }
			set { _dataHoraOcorrenciaPeriodoInicial=value; }
		}
		private string _dataHoraOcorrenciaPeriodoFinal;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataHoraOcorrenciaPeriodoFinal {
			get { return _dataHoraOcorrenciaPeriodoFinal==null?String.Empty:_dataHoraOcorrenciaPeriodoFinal; }
			set { _dataHoraOcorrenciaPeriodoFinal=value; }
		}

		private string _usuarioId;
		public string UsuarioId {
			get { return _usuarioId==null?String.Empty:_usuarioId; }
			set { _usuarioId=value; }
		}

		private string _dados;
		public string Dados {
			get { return _dados==null?String.Empty:_dados; }
			set { _dados=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!LogOcorrenciaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (logOcorrenciaId="+LogOcorrenciaId+")");
			}

			if (!LogEventoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (logEventoId="+LogEventoId+")");
			}

			if( !DataHoraOcorrenciaPeriodoInicial.Equals(String.Empty) && !DataHoraOcorrenciaPeriodoFinal.Equals(String.Empty)) {
				sbWhere.Append(" AND (dataHoraOcorrencia >='"+DataHoraOcorrenciaPeriodoInicial+"'");
				sbWhere.Append(" AND dataHoraOcorrencia <='"+DataHoraOcorrenciaPeriodoFinal+"')");
			} else if (!DataHoraOcorrenciaPeriodoInicial.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataHoraOcorrencia >='"+DataHoraOcorrenciaPeriodoInicial+"')");
			} else if (!DataHoraOcorrenciaPeriodoFinal.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataHoraOcorrencia <='"+DataHoraOcorrenciaPeriodoFinal+"')");
			}

			if (!UsuarioId.Equals(String.Empty)) {
				sbWhere.Append(" AND (usuarioId="+UsuarioId+")");
			}

			if (!Dados.Equals(String.Empty)) {
				sbWhere.Append(" AND (dados LIKE '%"+Dados+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
