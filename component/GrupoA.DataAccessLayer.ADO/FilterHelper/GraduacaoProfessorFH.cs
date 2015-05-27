
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
	public partial class GraduacaoProfessorFH : IFilterHelper
	{
		private string _graduacaoProfessorId;
		public string GraduacaoProfessorId {
			get { return _graduacaoProfessorId==null?String.Empty:_graduacaoProfessorId; }
			set { _graduacaoProfessorId=value; }
		}

		private string _graduacao;
		public string Graduacao {
			get { return _graduacao==null?String.Empty:_graduacao; }
			set { _graduacao=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!GraduacaoProfessorId.Equals(String.Empty)) {
				sbWhere.Append(" AND (graduacaoProfessorId="+GraduacaoProfessorId+")");
			}

			if (!Graduacao.Equals(String.Empty)) {
				sbWhere.Append(" AND (graduacao LIKE '%"+Graduacao+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
