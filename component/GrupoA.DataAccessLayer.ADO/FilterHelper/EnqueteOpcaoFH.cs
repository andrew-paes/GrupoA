
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
	public partial class EnqueteOpcaoFH : IFilterHelper
	{
		private string _enqueteOpcaoId;
		public string EnqueteOpcaoId {
			get { return _enqueteOpcaoId==null?String.Empty:_enqueteOpcaoId; }
			set { _enqueteOpcaoId=value; }
		}

		private string _enqueteId;
		public string EnqueteId {
			get { return _enqueteId==null?String.Empty:_enqueteId; }
			set { _enqueteId=value; }
		}

		private string _descricao;
		public string Descricao {
			get { return _descricao==null?String.Empty:_descricao; }
			set { _descricao=value; }
		}

		private string _contador;
		public string Contador {
			get { return _contador==null?String.Empty:_contador; }
			set { _contador=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!EnqueteOpcaoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (enqueteOpcaoId="+EnqueteOpcaoId+")");
			}

			if (!EnqueteId.Equals(String.Empty)) {
				sbWhere.Append(" AND (enqueteId="+EnqueteId+")");
			}

			if (!Descricao.Equals(String.Empty)) {
				sbWhere.Append(" AND (descricao LIKE '%"+Descricao+"%')");
			}

			if (!Contador.Equals(String.Empty)) {
				sbWhere.Append(" AND (contador="+Contador+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
