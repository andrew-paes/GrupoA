
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
	public partial class ComentarioFormatoFH : IFilterHelper
	{
		private string _comentarioFormatoId;
		public string ComentarioFormatoId {
			get { return _comentarioFormatoId==null?String.Empty:_comentarioFormatoId; }
			set { _comentarioFormatoId=value; }
		}

		private string _formato;
		public string Formato {
			get { return _formato==null?String.Empty:_formato; }
			set { _formato=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!ComentarioFormatoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (comentarioFormatoId="+ComentarioFormatoId+")");
			}

			if (!Formato.Equals(String.Empty)) {
				sbWhere.Append(" AND (formato LIKE '%"+Formato+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
