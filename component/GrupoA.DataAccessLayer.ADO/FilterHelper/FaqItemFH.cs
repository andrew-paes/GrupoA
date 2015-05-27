
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
	public partial class FaqItemFH : IFilterHelper
	{
		private string _faqItemId;
		public string FaqItemId {
			get { return _faqItemId==null?String.Empty:_faqItemId; }
			set { _faqItemId=value; }
		}

		private string _pergunta;
		public string Pergunta {
			get { return _pergunta==null?String.Empty:_pergunta; }
			set { _pergunta=value; }
		}

		private string _resposta;
		public string Resposta {
			get { return _resposta==null?String.Empty:_resposta; }
			set { _resposta=value; }
		}

		private string _ativo;
		public string Ativo {
			get { return _ativo==null?String.Empty:_ativo; }
			set { _ativo=value; }
		}

		private string _ordemItem;
		public string OrdemItem {
			get { return _ordemItem==null?String.Empty:_ordemItem; }
			set { _ordemItem=value; }
		}

		private string _faqCategoriaId;
		public string FaqCategoriaId {
			get { return _faqCategoriaId==null?String.Empty:_faqCategoriaId; }
			set { _faqCategoriaId=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!FaqItemId.Equals(String.Empty)) {
				sbWhere.Append(" AND (faqItemId="+FaqItemId+")");
			}

			if (!Pergunta.Equals(String.Empty)) {
				sbWhere.Append(" AND (pergunta LIKE '%"+Pergunta+"%')");
			}

			if (!Resposta.Equals(String.Empty)) {
				sbWhere.Append(" AND (resposta LIKE '%"+Resposta+"%')");
			}

			if (!Ativo.Equals(String.Empty)) {
				sbWhere.Append(" AND (ativo LIKE '%"+Ativo+"%')");
			}

			if (!OrdemItem.Equals(String.Empty)) {
				sbWhere.Append(" AND (ordemItem="+OrdemItem+")");
			}

			if (!FaqCategoriaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (faqCategoriaId="+FaqCategoriaId+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
