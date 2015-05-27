
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
	public partial class CategoriaNoticiaFH : IFilterHelper
	{
		private string _categoriaNoticiaId;
		public string CategoriaNoticiaId {
			get { return _categoriaNoticiaId==null?String.Empty:_categoriaNoticiaId; }
			set { _categoriaNoticiaId=value; }
		}

		private string _nomeCategoriaNoticia;
		public string NomeCategoriaNoticia {
			get { return _nomeCategoriaNoticia==null?String.Empty:_nomeCategoriaNoticia; }
			set { _nomeCategoriaNoticia=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!CategoriaNoticiaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (categoriaNoticiaId="+CategoriaNoticiaId+")");
			}

			if (!NomeCategoriaNoticia.Equals(String.Empty)) {
				sbWhere.Append(" AND (nomeCategoriaNoticia LIKE '%"+NomeCategoriaNoticia+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
