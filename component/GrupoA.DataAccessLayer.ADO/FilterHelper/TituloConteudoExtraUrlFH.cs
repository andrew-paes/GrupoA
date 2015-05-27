
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
	public partial class TituloConteudoExtraUrlFH : IFilterHelper
	{
		private string _tituloConteudoExtraUrlId;
		public string TituloConteudoExtraUrlId {
			get { return _tituloConteudoExtraUrlId==null?String.Empty:_tituloConteudoExtraUrlId; }
			set { _tituloConteudoExtraUrlId=value; }
		}

		private string _url;
		public string Url {
			get { return _url==null?String.Empty:_url; }
			set { _url=value; }
		}

		private string _targetBlank;
		public string TargetBlank {
			get { return _targetBlank==null?String.Empty:_targetBlank; }
			set { _targetBlank=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!TituloConteudoExtraUrlId.Equals(String.Empty)) {
				sbWhere.Append(" AND (tituloConteudoExtraUrlId="+TituloConteudoExtraUrlId+")");
			}

			if (!Url.Equals(String.Empty)) {
				sbWhere.Append(" AND (url LIKE '%"+Url+"%')");
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
