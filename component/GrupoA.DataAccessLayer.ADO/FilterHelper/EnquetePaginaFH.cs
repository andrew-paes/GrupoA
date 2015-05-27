
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
	public partial class EnquetePaginaFH : IFilterHelper
	{
		private string _enquetePaginaId;
		public string EnquetePaginaId {
			get { return _enquetePaginaId==null?String.Empty:_enquetePaginaId; }
			set { _enquetePaginaId=value; }
		}

		private string _nomePagina;
		public string NomePagina {
			get { return _nomePagina==null?String.Empty:_nomePagina; }
			set { _nomePagina=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!EnquetePaginaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (enquetePaginaId="+EnquetePaginaId+")");
			}

			if (!NomePagina.Equals(String.Empty)) {
				sbWhere.Append(" AND (nomePagina LIKE '%"+NomePagina+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
