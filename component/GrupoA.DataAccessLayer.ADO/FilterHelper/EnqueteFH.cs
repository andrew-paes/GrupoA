
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
	public partial class EnqueteFH : IFilterHelper
	{
		private string _enqueteId;
		public string EnqueteId {
			get { return _enqueteId==null?String.Empty:_enqueteId; }
			set { _enqueteId=value; }
		}

		private string _nomeEnquete;
		public string NomeEnquete {
			get { return _nomeEnquete==null?String.Empty:_nomeEnquete; }
			set { _nomeEnquete=value; }
		}

		private string _ativo;
		public string Ativo {
			get { return _ativo==null?String.Empty:_ativo; }
			set { _ativo=value; }
		}

		private string _pergunta;
		public string Pergunta {
			get { return _pergunta==null?String.Empty:_pergunta; }
			set { _pergunta=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!EnqueteId.Equals(String.Empty)) {
				sbWhere.Append(" AND (enqueteId="+EnqueteId+")");
			}

			if (!NomeEnquete.Equals(String.Empty)) {
				sbWhere.Append(" AND (nomeEnquete LIKE '%"+NomeEnquete+"%')");
			}

			if (!Ativo.Equals(String.Empty)) {
				sbWhere.Append(" AND (ativo LIKE '%"+Ativo+"%')");
			}

			if (!Pergunta.Equals(String.Empty)) {
				sbWhere.Append(" AND (pergunta LIKE '%"+Pergunta+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
