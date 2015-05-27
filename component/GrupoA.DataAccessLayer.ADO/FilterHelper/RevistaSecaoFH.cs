
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
	public partial class RevistaSecaoFH : IFilterHelper
	{
		private string _revistaSecaoId;
		public string RevistaSecaoId {
			get { return _revistaSecaoId==null?String.Empty:_revistaSecaoId; }
			set { _revistaSecaoId=value; }
		}

		private string _nomeSecao;
		public string NomeSecao {
			get { return _nomeSecao==null?String.Empty:_nomeSecao; }
			set { _nomeSecao=value; }
		}

		private string _revistaId;
		public string RevistaId {
			get { return _revistaId==null?String.Empty:_revistaId; }
			set { _revistaId=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!RevistaSecaoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (revistaSecaoId="+RevistaSecaoId+")");
			}

			if (!NomeSecao.Equals(String.Empty)) {
				sbWhere.Append(" AND (nomeSecao LIKE '%"+NomeSecao+"%')");
			}

			if (!RevistaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (revistaId="+RevistaId+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
