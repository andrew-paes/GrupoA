
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
	public partial class BannerFH : IFilterHelper
	{
		private string _bannerId;
		public string BannerId {
			get { return _bannerId==null?String.Empty:_bannerId; }
			set { _bannerId=value; }
		}

		private string _nomeBanner;
		public string NomeBanner {
			get { return _nomeBanner==null?String.Empty:_nomeBanner; }
			set { _nomeBanner=value; }
		}

		private string _ativo;
		public string Ativo {
			get { return _ativo==null?String.Empty:_ativo; }
			set { _ativo=value; }
		}

		private string _dataExibicaoInicioPeriodoInicial;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataExibicaoInicioPeriodoInicial {
			get { return _dataExibicaoInicioPeriodoInicial==null?String.Empty:_dataExibicaoInicioPeriodoInicial; }
			set { _dataExibicaoInicioPeriodoInicial=value; }
		}
		private string _dataExibicaoInicioPeriodoFinal;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataExibicaoInicioPeriodoFinal {
			get { return _dataExibicaoInicioPeriodoFinal==null?String.Empty:_dataExibicaoInicioPeriodoFinal; }
			set { _dataExibicaoInicioPeriodoFinal=value; }
		}

		private string _dataExibicaoFimPeriodoInicial;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataExibicaoFimPeriodoInicial {
			get { return _dataExibicaoFimPeriodoInicial==null?String.Empty:_dataExibicaoFimPeriodoInicial; }
			set { _dataExibicaoFimPeriodoInicial=value; }
		}
		private string _dataExibicaoFimPeriodoFinal;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataExibicaoFimPeriodoFinal {
			get { return _dataExibicaoFimPeriodoFinal==null?String.Empty:_dataExibicaoFimPeriodoFinal; }
			set { _dataExibicaoFimPeriodoFinal=value; }
		}

		private string _url;
		public string Url {
			get { return _url==null?String.Empty:_url; }
			set { _url=value; }
		}

		private string _arquivoId;
		public string ArquivoId {
			get { return _arquivoId==null?String.Empty:_arquivoId; }
			set { _arquivoId=value; }
		}

		private string _targetBlank;
		public string TargetBlank {
			get { return _targetBlank==null?String.Empty:_targetBlank; }
			set { _targetBlank=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!BannerId.Equals(String.Empty)) {
				sbWhere.Append(" AND (bannerId="+BannerId+")");
			}

			if (!NomeBanner.Equals(String.Empty)) {
				sbWhere.Append(" AND (nomeBanner LIKE '%"+NomeBanner+"%')");
			}

			if (!Ativo.Equals(String.Empty)) {
				sbWhere.Append(" AND (ativo LIKE '%"+Ativo+"%')");
			}

			if( !DataExibicaoInicioPeriodoInicial.Equals(String.Empty) && !DataExibicaoInicioPeriodoFinal.Equals(String.Empty)) {
				sbWhere.Append(" AND (dataExibicaoInicio >='"+DataExibicaoInicioPeriodoInicial+"'");
				sbWhere.Append(" AND dataExibicaoInicio <='"+DataExibicaoInicioPeriodoFinal+"')");
			} else if (!DataExibicaoInicioPeriodoInicial.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataExibicaoInicio >='"+DataExibicaoInicioPeriodoInicial+"')");
			} else if (!DataExibicaoInicioPeriodoFinal.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataExibicaoInicio <='"+DataExibicaoInicioPeriodoFinal+"')");
			}

			if( !DataExibicaoFimPeriodoInicial.Equals(String.Empty) && !DataExibicaoFimPeriodoFinal.Equals(String.Empty)) {
				sbWhere.Append(" AND (dataExibicaoFim >='"+DataExibicaoFimPeriodoInicial+"'");
				sbWhere.Append(" AND dataExibicaoFim <='"+DataExibicaoFimPeriodoFinal+"')");
			} else if (!DataExibicaoFimPeriodoInicial.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataExibicaoFim >='"+DataExibicaoFimPeriodoInicial+"')");
			} else if (!DataExibicaoFimPeriodoFinal.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataExibicaoFim <='"+DataExibicaoFimPeriodoFinal+"')");
			}

			if (!Url.Equals(String.Empty)) {
				sbWhere.Append(" AND (url LIKE '%"+Url+"%')");
			}

			if (!ArquivoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (arquivoId="+ArquivoId+")");
			}

			if (!TargetBlank.Equals(String.Empty)) {
				sbWhere.Append(" AND (targetBlank LIKE '%"+TargetBlank+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
