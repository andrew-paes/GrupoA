
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
	public partial class CursoNivelFH : IFilterHelper
	{
		private string _cursoNivelId;
		public string CursoNivelId {
			get { return _cursoNivelId==null?String.Empty:_cursoNivelId; }
			set { _cursoNivelId=value; }
		}

		private string _nivel;
		public string Nivel {
			get { return _nivel==null?String.Empty:_nivel; }
			set { _nivel=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!CursoNivelId.Equals(String.Empty)) {
				sbWhere.Append(" AND (cursoNivelId="+CursoNivelId+")");
			}

			if (!Nivel.Equals(String.Empty)) {
				sbWhere.Append(" AND (nivel LIKE '%"+Nivel+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
