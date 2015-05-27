
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
	public partial class LogCategoriaFH : IFilterHelper
	{
		private string _logCategoriaId;
		public string LogCategoriaId {
			get { return _logCategoriaId==null?String.Empty:_logCategoriaId; }
			set { _logCategoriaId=value; }
		}

		private string _categoria;
		public string Categoria {
			get { return _categoria==null?String.Empty:_categoria; }
			set { _categoria=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!LogCategoriaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (logCategoriaId="+LogCategoriaId+")");
			}

			if (!Categoria.Equals(String.Empty)) {
				sbWhere.Append(" AND (categoria LIKE '%"+Categoria+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
