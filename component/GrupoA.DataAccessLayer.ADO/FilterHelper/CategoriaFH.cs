
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
	public partial class CategoriaFH : IFilterHelper
	{
		private string _categoriaId;
		public string CategoriaId {
			get { return _categoriaId==null?String.Empty:_categoriaId; }
			set { _categoriaId=value; }
		}

		private string _nomeCategoria;
		public string NomeCategoria {
			get { return _nomeCategoria==null?String.Empty:_nomeCategoria; }
			set { _nomeCategoria=value; }
		}

		private string _categoriaIdPai;
		public string CategoriaIdPai {
			get { return _categoriaIdPai==null?String.Empty:_categoriaIdPai; }
			set { _categoriaIdPai=value; }
		}

		private string _codigoCategoria;
		public string CodigoCategoria {
			get { return _codigoCategoria==null?String.Empty:_codigoCategoria; }
			set { _codigoCategoria=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!CategoriaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (categoriaId="+CategoriaId+")");
			}

			if (!NomeCategoria.Equals(String.Empty)) {
				sbWhere.Append(" AND (nomeCategoria LIKE '%"+NomeCategoria+"%')");
			}

			if (!CategoriaIdPai.Equals(String.Empty)) {
				sbWhere.Append(" AND (categoriaIdPai="+CategoriaIdPai+")");
			}

			if (!CodigoCategoria.Equals(String.Empty)) {
				sbWhere.Append(" AND (codigoCategoria LIKE '%"+CodigoCategoria+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
