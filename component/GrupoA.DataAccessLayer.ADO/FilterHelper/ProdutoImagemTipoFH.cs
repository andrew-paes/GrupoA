
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
	public partial class ProdutoImagemTipoFH : IFilterHelper
	{
		private string _produtoImagemTipoId;
		public string ProdutoImagemTipoId {
			get { return _produtoImagemTipoId==null?String.Empty:_produtoImagemTipoId; }
			set { _produtoImagemTipoId=value; }
		}

		private string _tipoImagem;
		public string TipoImagem {
			get { return _tipoImagem==null?String.Empty:_tipoImagem; }
			set { _tipoImagem=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!ProdutoImagemTipoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (produtoImagemTipoId="+ProdutoImagemTipoId+")");
			}

			if (!TipoImagem.Equals(String.Empty)) {
				sbWhere.Append(" AND (tipoImagem LIKE '%"+TipoImagem+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
