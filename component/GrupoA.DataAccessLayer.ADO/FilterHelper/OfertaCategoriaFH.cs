
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
	public partial class OfertaCategoriaFH : IFilterHelper
	{
		private string _ofertaCategoriaId;
		public string OfertaCategoriaId {
			get { return _ofertaCategoriaId==null?String.Empty:_ofertaCategoriaId; }
			set { _ofertaCategoriaId=value; }
		}

		private string _ofertaId;
		public string OfertaId {
			get { return _ofertaId==null?String.Empty:_ofertaId; }
			set { _ofertaId=value; }
		}

		private string _categoriaId;
		public string CategoriaId {
			get { return _categoriaId==null?String.Empty:_categoriaId; }
			set { _categoriaId=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!OfertaCategoriaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (ofertaCategoriaId="+OfertaCategoriaId+")");
			}

			if (!OfertaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (ofertaId="+OfertaId+")");
			}

			if (!CategoriaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (categoriaId="+CategoriaId+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
