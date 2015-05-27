
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
	public partial class MidiaRevistaFH : IFilterHelper
	{
		private string _midiaRevistaId;
		public string MidiaRevistaId {
			get { return _midiaRevistaId==null?String.Empty:_midiaRevistaId; }
			set { _midiaRevistaId=value; }
		}

		private string _midiaId;
		public string MidiaId {
			get { return _midiaId==null?String.Empty:_midiaId; }
			set { _midiaId=value; }
		}

		private string _revistaId;
		public string RevistaId {
			get { return _revistaId==null?String.Empty:_revistaId; }
			set { _revistaId=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!MidiaRevistaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (midiaRevistaId="+MidiaRevistaId+")");
			}

			if (!MidiaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (midiaId="+MidiaId+")");
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
