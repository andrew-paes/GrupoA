
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
	public partial class UsuarioControleFH : IFilterHelper
	{
		private string _usuarioId;
		public string UsuarioId {
			get { return _usuarioId==null?String.Empty:_usuarioId; }
			set { _usuarioId=value; }
		}

		private string _dataHoraUltimaSincroniaPeriodoInicial;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataHoraUltimaSincroniaPeriodoInicial {
			get { return _dataHoraUltimaSincroniaPeriodoInicial==null?String.Empty:_dataHoraUltimaSincroniaPeriodoInicial; }
			set { _dataHoraUltimaSincroniaPeriodoInicial=value; }
		}
		private string _dataHoraUltimaSincroniaPeriodoFinal;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataHoraUltimaSincroniaPeriodoFinal {
			get { return _dataHoraUltimaSincroniaPeriodoFinal==null?String.Empty:_dataHoraUltimaSincroniaPeriodoFinal; }
			set { _dataHoraUltimaSincroniaPeriodoFinal=value; }
		}

		private string _realizarSincronizacao;
		public string RealizarSincronizacao {
			get { return _realizarSincronizacao==null?String.Empty:_realizarSincronizacao; }
			set { _realizarSincronizacao=value; }
		}

		private string _customerId;
		public string CustomerId {
			get { return _customerId==null?String.Empty:_customerId; }
			set { _customerId=value; }
		}

		private string _prospectId;
		public string ProspectId {
			get { return _prospectId==null?String.Empty:_prospectId; }
			set { _prospectId=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!UsuarioId.Equals(String.Empty)) {
				sbWhere.Append(" AND (usuarioId="+UsuarioId+")");
			}

			if( !DataHoraUltimaSincroniaPeriodoInicial.Equals(String.Empty) && !DataHoraUltimaSincroniaPeriodoFinal.Equals(String.Empty)) {
				sbWhere.Append(" AND (dataHoraUltimaSincronia >='"+DataHoraUltimaSincroniaPeriodoInicial+"'");
				sbWhere.Append(" AND dataHoraUltimaSincronia <='"+DataHoraUltimaSincroniaPeriodoFinal+"')");
			} else if (!DataHoraUltimaSincroniaPeriodoInicial.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataHoraUltimaSincronia >='"+DataHoraUltimaSincroniaPeriodoInicial+"')");
			} else if (!DataHoraUltimaSincroniaPeriodoFinal.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataHoraUltimaSincronia <='"+DataHoraUltimaSincroniaPeriodoFinal+"')");
			}

			if (!RealizarSincronizacao.Equals(String.Empty)) {
				sbWhere.Append(" AND (realizarSincronizacao LIKE '%"+RealizarSincronizacao+"%')");
			}

			if (!CustomerId.Equals(String.Empty)) {
				sbWhere.Append(" AND (customerId LIKE '%"+CustomerId+"%')");
			}

			if (!ProspectId.Equals(String.Empty)) {
				sbWhere.Append(" AND (prospectId LIKE '%"+ProspectId+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
