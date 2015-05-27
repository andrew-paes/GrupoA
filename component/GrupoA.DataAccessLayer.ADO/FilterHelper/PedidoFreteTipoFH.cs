
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
	public partial class PedidoFreteTipoFH : IFilterHelper
	{
		private string _pedidoFreteTipoId;
		public string PedidoFreteTipoId {
			get { return _pedidoFreteTipoId==null?String.Empty:_pedidoFreteTipoId; }
			set { _pedidoFreteTipoId=value; }
		}

		private string _nomeTipo;
		public string NomeTipo {
			get { return _nomeTipo==null?String.Empty:_nomeTipo; }
			set { _nomeTipo=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!PedidoFreteTipoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (PedidoFreteTipoId LIKE '%"+PedidoFreteTipoId+"%')");
			}

			if (!NomeTipo.Equals(String.Empty)) {
				sbWhere.Append(" AND (nomeTipo LIKE '%"+NomeTipo+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
