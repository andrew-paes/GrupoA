
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
	public partial class ContatoAssuntoFH : IFilterHelper
	{
		private string _contatoAssuntoId;
		public string ContatoAssuntoId {
			get { return _contatoAssuntoId==null?String.Empty:_contatoAssuntoId; }
			set { _contatoAssuntoId=value; }
		}

		private string _contatoSetorId;
		public string ContatoSetorId {
			get { return _contatoSetorId==null?String.Empty:_contatoSetorId; }
			set { _contatoSetorId=value; }
		}

		private string _nomeAssunto;
		public string NomeAssunto {
			get { return _nomeAssunto==null?String.Empty:_nomeAssunto; }
			set { _nomeAssunto=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!ContatoAssuntoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (contatoAssuntoId="+ContatoAssuntoId+")");
			}

			if (!ContatoSetorId.Equals(String.Empty)) {
				sbWhere.Append(" AND (contatoSetorId="+ContatoSetorId+")");
			}

			if (!NomeAssunto.Equals(String.Empty)) {
				sbWhere.Append(" AND (nomeAssunto LIKE '%"+NomeAssunto+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
