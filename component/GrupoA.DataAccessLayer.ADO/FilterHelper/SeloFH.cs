
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
	public partial class SeloFH : IFilterHelper
	{
		private string _seloId;
		public string SeloId {
			get { return _seloId==null?String.Empty:_seloId; }
			set { _seloId=value; }
		}

		private string _nomeSelo;
		public string NomeSelo {
			get { return _nomeSelo==null?String.Empty:_nomeSelo; }
			set { _nomeSelo=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!SeloId.Equals(String.Empty)) {
				sbWhere.Append(" AND (seloId="+SeloId+")");
			}

			if (!NomeSelo.Equals(String.Empty)) {
				sbWhere.Append(" AND (nomeSelo LIKE '%"+NomeSelo+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
