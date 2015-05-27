
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
	public partial class LinkFH : IFilterHelper
	{
		private string _linkId;
		public string LinkId {
			get { return _linkId==null?String.Empty:_linkId; }
			set { _linkId=value; }
		}

		private string _nomeLink;
		public string NomeLink {
			get { return _nomeLink==null?String.Empty:_nomeLink; }
			set { _nomeLink=value; }
		}

		private string _urlLink;
		public string UrlLink {
			get { return _urlLink==null?String.Empty:_urlLink; }
			set { _urlLink=value; }
		}

		private string _ativo;
		public string Ativo {
			get { return _ativo==null?String.Empty:_ativo; }
			set { _ativo=value; }
		}

		private string _targetBlank;
		public string TargetBlank {
			get { return _targetBlank==null?String.Empty:_targetBlank; }
			set { _targetBlank=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!LinkId.Equals(String.Empty)) {
				sbWhere.Append(" AND (linkId="+LinkId+")");
			}

			if (!NomeLink.Equals(String.Empty)) {
				sbWhere.Append(" AND (nomeLink LIKE '%"+NomeLink+"%')");
			}

			if (!UrlLink.Equals(String.Empty)) {
				sbWhere.Append(" AND (urlLink LIKE '%"+UrlLink+"%')");
			}

			if (!Ativo.Equals(String.Empty)) {
				sbWhere.Append(" AND (ativo LIKE '%"+Ativo+"%')");
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
