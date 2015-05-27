
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
	public partial class TituloEletronicoFH : IFilterHelper
	{
		private string _tituloEletronicoId;
		public string TituloEletronicoId {
			get { return _tituloEletronicoId==null?String.Empty:_tituloEletronicoId; }
			set { _tituloEletronicoId=value; }
		}

		private string _isbn13;
		public string Isbn13 {
			get { return _isbn13==null?String.Empty:_isbn13; }
			set { _isbn13=value; }
		}

		private string _tituloId;
		public string TituloId {
			get { return _tituloId==null?String.Empty:_tituloId; }
			set { _tituloId=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!TituloEletronicoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (tituloEletronicoId="+TituloEletronicoId+")");
			}

			if (!Isbn13.Equals(String.Empty)) {
				sbWhere.Append(" AND (isbn13 LIKE '%"+Isbn13+"%')");
			}

			if (!TituloId.Equals(String.Empty)) {
				sbWhere.Append(" AND (tituloId="+TituloId+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
