
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
	public partial class RegiaoFH : IFilterHelper
	{
		private string _regiaoId;
		public string RegiaoId {
			get { return _regiaoId==null?String.Empty:_regiaoId; }
			set { _regiaoId=value; }
		}

		private string _nomeRegiao;
		public string NomeRegiao {
			get { return _nomeRegiao==null?String.Empty:_nomeRegiao; }
			set { _nomeRegiao=value; }
		}

		private string _uf;
		public string Uf {
			get { return _uf==null?String.Empty:_uf; }
			set { _uf=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!RegiaoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (regiaoId="+RegiaoId+")");
			}

			if (!NomeRegiao.Equals(String.Empty)) {
				sbWhere.Append(" AND (nomeRegiao LIKE '%"+NomeRegiao+"%')");
			}

			if (!Uf.Equals(String.Empty)) {
				sbWhere.Append(" AND (uf LIKE '%"+Uf+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
