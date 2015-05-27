
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
	public partial class ProfessorDisciplinaFH : IFilterHelper
	{
		private string _professorDisciplinaId;
		public string ProfessorDisciplinaId {
			get { return _professorDisciplinaId==null?String.Empty:_professorDisciplinaId; }
			set { _professorDisciplinaId=value; }
		}

		private string _numeroAlunos;
		public string NumeroAlunos {
			get { return _numeroAlunos==null?String.Empty:_numeroAlunos; }
			set { _numeroAlunos=value; }
		}

		private string _professorCursoId;
		public string ProfessorCursoId {
			get { return _professorCursoId==null?String.Empty:_professorCursoId; }
			set { _professorCursoId=value; }
		}

		private string _disciplinaId;
		public string DisciplinaId {
			get { return _disciplinaId==null?String.Empty:_disciplinaId; }
			set { _disciplinaId=value; }
		}

		private string _indicaTitulo;
		public string IndicaTitulo {
			get { return _indicaTitulo==null?String.Empty:_indicaTitulo; }
			set { _indicaTitulo=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!ProfessorDisciplinaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (professorDisciplinaId="+ProfessorDisciplinaId+")");
			}

			if (!NumeroAlunos.Equals(String.Empty)) {
				sbWhere.Append(" AND (numeroAlunos="+NumeroAlunos+")");
			}

			if (!ProfessorCursoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (professorCursoId="+ProfessorCursoId+")");
			}

			if (!DisciplinaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (disciplinaId="+DisciplinaId+")");
			}

			if (!IndicaTitulo.Equals(String.Empty)) {
				sbWhere.Append(" AND (indicaTitulo LIKE '%"+IndicaTitulo+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
