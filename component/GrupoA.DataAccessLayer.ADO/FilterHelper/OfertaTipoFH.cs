
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
	public partial class OfertaTipoFH : IFilterHelper
	{
		private string _ofertaTipoId;
		public string OfertaTipoId {
			get { return _ofertaTipoId==null?String.Empty:_ofertaTipoId; }
			set { _ofertaTipoId=value; }
		}

		private string _tipoOferta;
		public string TipoOferta {
			get { return _tipoOferta==null?String.Empty:_tipoOferta; }
			set { _tipoOferta=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!OfertaTipoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (ofertaTipoId="+OfertaTipoId+")");
			}

			if (!TipoOferta.Equals(String.Empty)) {
				sbWhere.Append(" AND (tipoOferta LIKE '%"+TipoOferta+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
