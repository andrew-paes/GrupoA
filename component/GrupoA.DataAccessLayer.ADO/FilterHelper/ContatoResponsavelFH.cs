
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
	public partial class ContatoResponsavelFH : IFilterHelper
	{
		private string _contatoResponsavelId;
		public string ContatoResponsavelId {
			get { return _contatoResponsavelId==null?String.Empty:_contatoResponsavelId; }
			set { _contatoResponsavelId=value; }
		}

		private string _contatoAssuntoId;
		public string ContatoAssuntoId {
			get { return _contatoAssuntoId==null?String.Empty:_contatoAssuntoId; }
			set { _contatoAssuntoId=value; }
		}

		private string _nomeResponsavel;
		public string NomeResponsavel {
			get { return _nomeResponsavel==null?String.Empty:_nomeResponsavel; }
			set { _nomeResponsavel=value; }
		}

		private string _emailResonsavel;
		public string EmailResonsavel {
			get { return _emailResonsavel==null?String.Empty:_emailResonsavel; }
			set { _emailResonsavel=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!ContatoResponsavelId.Equals(String.Empty)) {
				sbWhere.Append(" AND (contatoResponsavelId="+ContatoResponsavelId+")");
			}

			if (!ContatoAssuntoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (contatoAssuntoId="+ContatoAssuntoId+")");
			}

			if (!NomeResponsavel.Equals(String.Empty)) {
				sbWhere.Append(" AND (nomeResponsavel LIKE '%"+NomeResponsavel+"%')");
			}

			if (!EmailResonsavel.Equals(String.Empty)) {
				sbWhere.Append(" AND (emailResonsavel LIKE '%"+EmailResonsavel+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
