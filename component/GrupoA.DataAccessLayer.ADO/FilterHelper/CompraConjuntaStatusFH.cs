
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
	public partial class CompraConjuntaStatusFH : IFilterHelper
	{
		private string _compraConjuntaStatusId;
		public string CompraConjuntaStatusId {
			get { return _compraConjuntaStatusId==null?String.Empty:_compraConjuntaStatusId; }
			set { _compraConjuntaStatusId=value; }
		}

		private string _statusCompra;
		public string StatusCompra {
			get { return _statusCompra==null?String.Empty:_statusCompra; }
			set { _statusCompra=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!CompraConjuntaStatusId.Equals(String.Empty)) {
				sbWhere.Append(" AND (compraConjuntaStatusId="+CompraConjuntaStatusId+")");
			}

			if (!StatusCompra.Equals(String.Empty)) {
				sbWhere.Append(" AND (statusCompra LIKE '%"+StatusCompra+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
