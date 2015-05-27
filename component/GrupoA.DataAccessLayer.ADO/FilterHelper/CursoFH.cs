
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
	public partial class CursoFH : IFilterHelper
	{
		private string _cursoId;
		public string CursoId {
			get { return _cursoId==null?String.Empty:_cursoId; }
			set { _cursoId=value; }
		}

		private string _nome;
		public string Nome {
			get { return _nome==null?String.Empty:_nome; }
			set { _nome=value; }
		}

		private string _codigoCurso;
		public string CodigoCurso {
			get { return _codigoCurso==null?String.Empty:_codigoCurso; }
			set { _codigoCurso=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!CursoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (cursoId="+CursoId+")");
			}

			if (!Nome.Equals(String.Empty)) {
				sbWhere.Append(" AND (nome LIKE '%"+Nome+"%')");
			}

			if (!CodigoCurso.Equals(String.Empty)) {
				sbWhere.Append(" AND (codigoCurso LIKE '%"+CodigoCurso+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
