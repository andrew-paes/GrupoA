
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
	public partial class ContatoSetorFH : IFilterHelper
	{
		private string _contatoSetorId;
		public string ContatoSetorId {
			get { return _contatoSetorId==null?String.Empty:_contatoSetorId; }
			set { _contatoSetorId=value; }
		}

		private string _nomeSetor;
		public string NomeSetor {
			get { return _nomeSetor==null?String.Empty:_nomeSetor; }
			set { _nomeSetor=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!ContatoSetorId.Equals(String.Empty)) {
				sbWhere.Append(" AND (contatoSetorId="+ContatoSetorId+")");
			}

			if (!NomeSetor.Equals(String.Empty)) {
				sbWhere.Append(" AND (nomeSetor LIKE '%"+NomeSetor+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
