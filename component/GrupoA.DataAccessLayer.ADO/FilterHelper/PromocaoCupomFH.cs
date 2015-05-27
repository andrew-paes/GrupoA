
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
	public partial class PromocaoCupomFH : IFilterHelper
	{
		private string _promocaoCupomId;
		public string PromocaoCupomId {
			get { return _promocaoCupomId==null?String.Empty:_promocaoCupomId; }
			set { _promocaoCupomId=value; }
		}

		private string _promocaoId;
		public string PromocaoId {
			get { return _promocaoId==null?String.Empty:_promocaoId; }
			set { _promocaoId=value; }
		}

		private string _codigoCupom;
		public string CodigoCupom {
			get { return _codigoCupom==null?String.Empty:_codigoCupom; }
			set { _codigoCupom=value; }
		}

		private string _reutilizavel;
		public string Reutilizavel {
			get { return _reutilizavel==null?String.Empty:_reutilizavel; }
			set { _reutilizavel=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!PromocaoCupomId.Equals(String.Empty)) {
				sbWhere.Append(" AND (promocaoCupomId="+PromocaoCupomId+")");
			}

			if (!PromocaoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (promocaoId="+PromocaoId+")");
			}

			if (!CodigoCupom.Equals(String.Empty)) {
				sbWhere.Append(" AND (codigoCupom LIKE '%"+CodigoCupom+"%')");
			}

			if (!Reutilizavel.Equals(String.Empty)) {
				sbWhere.Append(" AND (reutilizavel LIKE '%"+Reutilizavel+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
