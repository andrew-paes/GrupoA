
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
	public partial class ProdutoImagemFH : IFilterHelper
	{
		private string _produtoImagemId;
		public string ProdutoImagemId {
			get { return _produtoImagemId==null?String.Empty:_produtoImagemId; }
			set { _produtoImagemId=value; }
		}

		private string _arquivoId;
		public string ArquivoId {
			get { return _arquivoId==null?String.Empty:_arquivoId; }
			set { _arquivoId=value; }
		}

		private string _produtoId;
		public string ProdutoId {
			get { return _produtoId==null?String.Empty:_produtoId; }
			set { _produtoId=value; }
		}

		private string _produtoImagemTipoId;
		public string ProdutoImagemTipoId {
			get { return _produtoImagemTipoId==null?String.Empty:_produtoImagemTipoId; }
			set { _produtoImagemTipoId=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!ProdutoImagemId.Equals(String.Empty)) {
				sbWhere.Append(" AND (produtoImagemId="+ProdutoImagemId+")");
			}

			if (!ArquivoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (arquivoId="+ArquivoId+")");
			}

			if (!ProdutoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (produtoId="+ProdutoId+")");
			}

			if (!ProdutoImagemTipoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (produtoImagemTipoId="+ProdutoImagemTipoId+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
