
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
	public partial class TransportadoraFH : IFilterHelper
	{
		private string _transportadoraId;
		public string TransportadoraId {
			get { return _transportadoraId==null?String.Empty:_transportadoraId; }
			set { _transportadoraId=value; }
		}

		private string _nomeTransportadora;
		public string NomeTransportadora {
			get { return _nomeTransportadora==null?String.Empty:_nomeTransportadora; }
			set { _nomeTransportadora=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!TransportadoraId.Equals(String.Empty)) {
				sbWhere.Append(" AND (transportadoraId="+TransportadoraId+")");
			}

			if (!NomeTransportadora.Equals(String.Empty)) {
				sbWhere.Append(" AND (nomeTransportadora LIKE '%"+NomeTransportadora+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
