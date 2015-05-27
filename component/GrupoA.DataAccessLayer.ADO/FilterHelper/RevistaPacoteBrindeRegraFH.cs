
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
	public partial class RevistaPacoteBrindeRegraFH : IFilterHelper
	{
		private string _revistaPacoteBrindeRegraId;
		public string RevistaPacoteBrindeRegraId {
			get { return _revistaPacoteBrindeRegraId==null?String.Empty:_revistaPacoteBrindeRegraId; }
			set { _revistaPacoteBrindeRegraId=value; }
		}

		private string _revistaPacoteId;
		public string RevistaPacoteId {
			get { return _revistaPacoteId==null?String.Empty:_revistaPacoteId; }
			set { _revistaPacoteId=value; }
		}

		private string _codigosProdutos;
		public string CodigosProdutos {
			get { return _codigosProdutos==null?String.Empty:_codigosProdutos; }
			set { _codigosProdutos=value; }
		}

		private string _quantidade;
		public string Quantidade {
			get { return _quantidade==null?String.Empty:_quantidade; }
			set { _quantidade=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!RevistaPacoteBrindeRegraId.Equals(String.Empty)) {
				sbWhere.Append(" AND (revistaPacoteBrindeRegraId="+RevistaPacoteBrindeRegraId+")");
			}

			if (!RevistaPacoteId.Equals(String.Empty)) {
				sbWhere.Append(" AND (revistaPacoteId="+RevistaPacoteId+")");
			}

			if (!CodigosProdutos.Equals(String.Empty)) {
				sbWhere.Append(" AND (codigosProdutos LIKE '%"+CodigosProdutos+"%')");
			}

			if (!Quantidade.Equals(String.Empty)) {
				sbWhere.Append(" AND (quantidade="+Quantidade+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
