
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
	public partial class CapituloEletronicoFH : IFilterHelper
	{
		private string _capituloEletronicoId;
		public string CapituloEletronicoId {
			get { return _capituloEletronicoId==null?String.Empty:_capituloEletronicoId; }
			set { _capituloEletronicoId=value; }
		}

		private string _tituloEletronicoId;
		public string TituloEletronicoId {
			get { return _tituloEletronicoId==null?String.Empty:_tituloEletronicoId; }
			set { _tituloEletronicoId=value; }
		}

		private string _capituloId;
		public string CapituloId {
			get { return _capituloId==null?String.Empty:_capituloId; }
			set { _capituloId=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!CapituloEletronicoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (capituloEletronicoId="+CapituloEletronicoId+")");
			}

			if (!TituloEletronicoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (tituloEletronicoId="+TituloEletronicoId+")");
			}

			if (!CapituloId.Equals(String.Empty)) {
				sbWhere.Append(" AND (capituloId="+CapituloId+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
