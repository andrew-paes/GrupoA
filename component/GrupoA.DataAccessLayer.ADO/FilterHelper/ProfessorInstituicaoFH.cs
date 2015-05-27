
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
	public partial class ProfessorInstituicaoFH : IFilterHelper
	{
		private string _professorInstituicaoId;
		public string ProfessorInstituicaoId {
			get { return _professorInstituicaoId==null?String.Empty:_professorInstituicaoId; }
			set { _professorInstituicaoId=value; }
		}

		private string _instituicaoId;
		public string InstituicaoId {
			get { return _instituicaoId==null?String.Empty:_instituicaoId; }
			set { _instituicaoId=value; }
		}

		private string _campus;
		public string Campus {
			get { return _campus==null?String.Empty:_campus; }
			set { _campus=value; }
		}

		private string _departamento;
		public string Departamento {
			get { return _departamento==null?String.Empty:_departamento; }
			set { _departamento=value; }
		}

		private string _telefoneId;
		public string TelefoneId {
			get { return _telefoneId==null?String.Empty:_telefoneId; }
			set { _telefoneId=value; }
		}

		private string _professorId;
		public string ProfessorId {
			get { return _professorId==null?String.Empty:_professorId; }
			set { _professorId=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!ProfessorInstituicaoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (professorInstituicaoId="+ProfessorInstituicaoId+")");
			}

			if (!InstituicaoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (instituicaoId="+InstituicaoId+")");
			}

			if (!Campus.Equals(String.Empty)) {
				sbWhere.Append(" AND (campus LIKE '%"+Campus+"%')");
			}

			if (!Departamento.Equals(String.Empty)) {
				sbWhere.Append(" AND (departamento LIKE '%"+Departamento+"%')");
			}

			if (!TelefoneId.Equals(String.Empty)) {
				sbWhere.Append(" AND (telefoneId="+TelefoneId+")");
			}

			if (!ProfessorId.Equals(String.Empty)) {
				sbWhere.Append(" AND (professorId="+ProfessorId+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
