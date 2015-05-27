
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
	public partial class DadosFH : IFilterHelper
	{
		private string _produtoId;
		public string ProdutoId {
			get { return _produtoId==null?String.Empty:_produtoId; }
			set { _produtoId=value; }
		}

		private string _categoriaId;
		public string CategoriaId {
			get { return _categoriaId==null?String.Empty:_categoriaId; }
			set { _categoriaId=value; }
		}

		private string _seloId;
		public string SeloId {
			get { return _seloId==null?String.Empty:_seloId; }
			set { _seloId=value; }
		}

		private string _lancamento;
		public string Lancamento {
			get { return _lancamento==null?String.Empty:_lancamento; }
			set { _lancamento=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!ProdutoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (ProdutoId="+ProdutoId+")");
			}

			if (!CategoriaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (CategoriaId="+CategoriaId+")");
			}

			if (!SeloId.Equals(String.Empty)) {
				sbWhere.Append(" AND (SeloId="+SeloId+")");
			}

			if (!Lancamento.Equals(String.Empty)) {
				sbWhere.Append(" AND (Lancamento="+Lancamento+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
