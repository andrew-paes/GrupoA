
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
	public partial class DestaqueTituloImpressoFH : IFilterHelper
	{
		private string _destaqueTituloImpressoId;
		public string DestaqueTituloImpressoId {
			get { return _destaqueTituloImpressoId==null?String.Empty:_destaqueTituloImpressoId; }
			set { _destaqueTituloImpressoId=value; }
		}

		private string _nomeArea;
		public string NomeArea {
			get { return _nomeArea==null?String.Empty:_nomeArea; }
			set { _nomeArea=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!DestaqueTituloImpressoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (destaqueTituloImpressoId="+DestaqueTituloImpressoId+")");
			}

			if (!NomeArea.Equals(String.Empty)) {
				sbWhere.Append(" AND (nomeArea LIKE '%"+NomeArea+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
