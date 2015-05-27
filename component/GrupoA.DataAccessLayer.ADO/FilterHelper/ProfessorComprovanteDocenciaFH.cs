
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
	public partial class ProfessorComprovanteDocenciaFH : IFilterHelper
	{
		private string _professorComprovanteDocenciaId;
		public string ProfessorComprovanteDocenciaId {
			get { return _professorComprovanteDocenciaId==null?String.Empty:_professorComprovanteDocenciaId; }
			set { _professorComprovanteDocenciaId=value; }
		}

		private string _professorId;
		public string ProfessorId {
			get { return _professorId==null?String.Empty:_professorId; }
			set { _professorId=value; }
		}

		private string _arquivoId;
		public string ArquivoId {
			get { return _arquivoId==null?String.Empty:_arquivoId; }
			set { _arquivoId=value; }
		}

		private string _instituicaoId;
		public string InstituicaoId {
			get { return _instituicaoId==null?String.Empty:_instituicaoId; }
			set { _instituicaoId=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!ProfessorComprovanteDocenciaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (professorComprovanteDocenciaId="+ProfessorComprovanteDocenciaId+")");
			}

			if (!ProfessorId.Equals(String.Empty)) {
				sbWhere.Append(" AND (professorId="+ProfessorId+")");
			}

			if (!ArquivoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (arquivoId="+ArquivoId+")");
			}

			if (!InstituicaoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (instituicaoId="+InstituicaoId+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
