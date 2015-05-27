
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
	public partial class TituloAutorFH : IFilterHelper
	{
		private string _tituloId;
		public string TituloId {
			get { return _tituloId==null?String.Empty:_tituloId; }
			set { _tituloId=value; }
		}

		private string _autorId;
		public string AutorId {
			get { return _autorId==null?String.Empty:_autorId; }
			set { _autorId=value; }
		}

		private string _principal;
		public string Principal {
			get { return _principal==null?String.Empty:_principal; }
			set { _principal=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!TituloId.Equals(String.Empty)) {
				sbWhere.Append(" AND (tituloId="+TituloId+")");
			}

			if (!AutorId.Equals(String.Empty)) {
				sbWhere.Append(" AND (autorId="+AutorId+")");
			}

			if (!Principal.Equals(String.Empty)) {
				sbWhere.Append(" AND (principal LIKE '%"+Principal+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
