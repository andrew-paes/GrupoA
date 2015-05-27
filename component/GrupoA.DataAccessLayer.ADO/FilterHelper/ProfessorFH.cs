
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
	public partial class ProfessorFH : IFilterHelper
	{
		private string _professorId;
		public string ProfessorId {
			get { return _professorId==null?String.Empty:_professorId; }
			set { _professorId=value; }
		}

		private string _graduacaoProfessorId;
		public string GraduacaoProfessorId {
			get { return _graduacaoProfessorId==null?String.Empty:_graduacaoProfessorId; }
			set { _graduacaoProfessorId=value; }
		}

		private string _autorGrupoa;
		public string AutorGrupoa {
			get { return _autorGrupoa==null?String.Empty:_autorGrupoa; }
			set { _autorGrupoa=value; }
		}

		private string _colaboradorGrupoa;
		public string ColaboradorGrupoa {
			get { return _colaboradorGrupoa==null?String.Empty:_colaboradorGrupoa; }
			set { _colaboradorGrupoa=value; }
		}

		private string _possuiPublicacao;
		public string PossuiPublicacao {
			get { return _possuiPublicacao==null?String.Empty:_possuiPublicacao; }
			set { _possuiPublicacao=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!ProfessorId.Equals(String.Empty)) {
				sbWhere.Append(" AND (professorId="+ProfessorId+")");
			}

			if (!GraduacaoProfessorId.Equals(String.Empty)) {
				sbWhere.Append(" AND (graduacaoProfessorId="+GraduacaoProfessorId+")");
			}

			if (!AutorGrupoa.Equals(String.Empty)) {
				sbWhere.Append(" AND (autorGrupoa LIKE '%"+AutorGrupoa+"%')");
			}

			if (!ColaboradorGrupoa.Equals(String.Empty)) {
				sbWhere.Append(" AND (colaboradorGrupoa LIKE '%"+ColaboradorGrupoa+"%')");
			}

			if (!PossuiPublicacao.Equals(String.Empty)) {
				sbWhere.Append(" AND (possuiPublicacao LIKE '%"+PossuiPublicacao+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
