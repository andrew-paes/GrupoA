
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
	public partial class PerfilFH : IFilterHelper
	{
		private string _perfilId;
		public string PerfilId {
			get { return _perfilId==null?String.Empty:_perfilId; }
			set { _perfilId=value; }
		}

		private string _perfilNome;
		public string PerfilNome {
			get { return _perfilNome==null?String.Empty:_perfilNome; }
			set { _perfilNome=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!PerfilId.Equals(String.Empty)) {
				sbWhere.Append(" AND (perfilId="+PerfilId+")");
			}

			if (!PerfilNome.Equals(String.Empty)) {
				sbWhere.Append(" AND (perfilNome LIKE '%"+PerfilNome+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
