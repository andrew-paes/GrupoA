
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
	public partial class RevistaArtigoPermissaoFH : IFilterHelper
	{
		private string _revistaArtigoPermissaoId;
		public string RevistaArtigoPermissaoId {
			get { return _revistaArtigoPermissaoId==null?String.Empty:_revistaArtigoPermissaoId; }
			set { _revistaArtigoPermissaoId=value; }
		}

		private string _permissao;
		public string Permissao {
			get { return _permissao==null?String.Empty:_permissao; }
			set { _permissao=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!RevistaArtigoPermissaoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (revistaArtigoPermissaoId="+RevistaArtigoPermissaoId+")");
			}

			if (!Permissao.Equals(String.Empty)) {
				sbWhere.Append(" AND (permissao LIKE '%"+Permissao+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
