
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
	public partial class RevistaPacoteFH : IFilterHelper
	{
		private string _revistaPacoteId;
		public string RevistaPacoteId {
			get { return _revistaPacoteId==null?String.Empty:_revistaPacoteId; }
			set { _revistaPacoteId=value; }
		}

		private string _nome;
		public string Nome {
			get { return _nome==null?String.Empty:_nome; }
			set { _nome=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!RevistaPacoteId.Equals(String.Empty)) {
				sbWhere.Append(" AND (revistaPacoteId="+RevistaPacoteId+")");
			}

			if (!Nome.Equals(String.Empty)) {
				sbWhere.Append(" AND (nome LIKE '%"+Nome+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
