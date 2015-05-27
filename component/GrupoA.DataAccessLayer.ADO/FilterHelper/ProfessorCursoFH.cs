
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
	public partial class ProfessorCursoFH : IFilterHelper
	{
		private string _professorCursoId;
		public string ProfessorCursoId {
			get { return _professorCursoId==null?String.Empty:_professorCursoId; }
			set { _professorCursoId=value; }
		}

		private string _professorInstituicaoId;
		public string ProfessorInstituicaoId {
			get { return _professorInstituicaoId==null?String.Empty:_professorInstituicaoId; }
			set { _professorInstituicaoId=value; }
		}

		private string _cursoNivelId;
		public string CursoNivelId {
			get { return _cursoNivelId==null?String.Empty:_cursoNivelId; }
			set { _cursoNivelId=value; }
		}

		private string _coordenadorCurso;
		public string CoordenadorCurso {
			get { return _coordenadorCurso==null?String.Empty:_coordenadorCurso; }
			set { _coordenadorCurso=value; }
		}

		private string _cursoId;
		public string CursoId {
			get { return _cursoId==null?String.Empty:_cursoId; }
			set { _cursoId=value; }
		}

		private string _cargo;
		public string Cargo {
			get { return _cargo==null?String.Empty:_cargo; }
			set { _cargo=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!ProfessorCursoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (professorCursoId="+ProfessorCursoId+")");
			}

			if (!ProfessorInstituicaoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (professorInstituicaoId="+ProfessorInstituicaoId+")");
			}

			if (!CursoNivelId.Equals(String.Empty)) {
				sbWhere.Append(" AND (cursoNivelId="+CursoNivelId+")");
			}

			if (!CoordenadorCurso.Equals(String.Empty)) {
				sbWhere.Append(" AND (coordenadorCurso LIKE '%"+CoordenadorCurso+"%')");
			}

			if (!CursoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (cursoId="+CursoId+")");
			}

			if (!Cargo.Equals(String.Empty)) {
				sbWhere.Append(" AND (cargo LIKE '%"+Cargo+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
