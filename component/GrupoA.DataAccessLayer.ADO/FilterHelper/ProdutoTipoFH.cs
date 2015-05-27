
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
	public partial class ProdutoTipoFH : IFilterHelper
	{
		private string _produtoTipoId;
		public string ProdutoTipoId {
			get { return _produtoTipoId==null?String.Empty:_produtoTipoId; }
			set { _produtoTipoId=value; }
		}

		private string _tipo;
		public string Tipo {
			get { return _tipo==null?String.Empty:_tipo; }
			set { _tipo=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!ProdutoTipoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (produtoTipoId="+ProdutoTipoId+")");
			}

			if (!Tipo.Equals(String.Empty)) {
				sbWhere.Append(" AND (tipo LIKE '%"+Tipo+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
