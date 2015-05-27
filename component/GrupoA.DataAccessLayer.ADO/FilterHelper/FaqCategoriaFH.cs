
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
	public partial class FaqCategoriaFH : IFilterHelper
	{
		private string _faqCategoriaId;
		public string FaqCategoriaId {
			get { return _faqCategoriaId==null?String.Empty:_faqCategoriaId; }
			set { _faqCategoriaId=value; }
		}

		private string _nomeCategoria;
		public string NomeCategoria {
			get { return _nomeCategoria==null?String.Empty:_nomeCategoria; }
			set { _nomeCategoria=value; }
		}

		private string _ordemCategoria;
		public string OrdemCategoria {
			get { return _ordemCategoria==null?String.Empty:_ordemCategoria; }
			set { _ordemCategoria=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!FaqCategoriaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (faqCategoriaId="+FaqCategoriaId+")");
			}

			if (!NomeCategoria.Equals(String.Empty)) {
				sbWhere.Append(" AND (nomeCategoria LIKE '%"+NomeCategoria+"%')");
			}

			if (!OrdemCategoria.Equals(String.Empty)) {
				sbWhere.Append(" AND (ordemCategoria="+OrdemCategoria+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
