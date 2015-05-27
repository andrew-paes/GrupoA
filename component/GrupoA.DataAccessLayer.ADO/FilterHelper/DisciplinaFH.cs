
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
	public partial class DisciplinaFH : IFilterHelper
	{
		private string _disciplinaId;
		public string DisciplinaId {
			get { return _disciplinaId==null?String.Empty:_disciplinaId; }
			set { _disciplinaId=value; }
		}

		private string _descricao;
		public string Descricao {
			get { return _descricao==null?String.Empty:_descricao; }
			set { _descricao=value; }
		}

		private string _codigoDisciplina;
		public string CodigoDisciplina {
			get { return _codigoDisciplina==null?String.Empty:_codigoDisciplina; }
			set { _codigoDisciplina=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!DisciplinaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (disciplinaId="+DisciplinaId+")");
			}

			if (!Descricao.Equals(String.Empty)) {
				sbWhere.Append(" AND (descricao LIKE '%"+Descricao+"%')");
			}

			if (!CodigoDisciplina.Equals(String.Empty)) {
				sbWhere.Append(" AND (codigoDisciplina LIKE '%"+CodigoDisciplina+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
