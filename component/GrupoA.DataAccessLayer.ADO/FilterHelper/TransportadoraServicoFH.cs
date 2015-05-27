
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
	public partial class TransportadoraServicoFH : IFilterHelper
	{
		private string _transportadoraServicoId;
		public string TransportadoraServicoId {
			get { return _transportadoraServicoId==null?String.Empty:_transportadoraServicoId; }
			set { _transportadoraServicoId=value; }
		}

		private string _transportadoraId;
		public string TransportadoraId {
			get { return _transportadoraId==null?String.Empty:_transportadoraId; }
			set { _transportadoraId=value; }
		}

		private string _nomeServicoe;
		public string NomeServicoe {
			get { return _nomeServicoe==null?String.Empty:_nomeServicoe; }
			set { _nomeServicoe=value; }
		}

		private string _ativo;
		public string Ativo {
			get { return _ativo==null?String.Empty:_ativo; }
			set { _ativo=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!TransportadoraServicoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (transportadoraServicoId="+TransportadoraServicoId+")");
			}

			if (!TransportadoraId.Equals(String.Empty)) {
				sbWhere.Append(" AND (transportadoraId="+TransportadoraId+")");
			}

			if (!NomeServicoe.Equals(String.Empty)) {
				sbWhere.Append(" AND (nomeServicoe LIKE '%"+NomeServicoe+"%')");
			}

			if (!Ativo.Equals(String.Empty)) {
				sbWhere.Append(" AND (ativo LIKE '%"+Ativo+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
