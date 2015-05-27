
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
	public partial class PromocaoTipoFH : IFilterHelper
	{
		private string _promocaoTipoId;
		public string PromocaoTipoId {
			get { return _promocaoTipoId==null?String.Empty:_promocaoTipoId; }
			set { _promocaoTipoId=value; }
		}

		private string _tipoPromocao;
		public string TipoPromocao {
			get { return _tipoPromocao==null?String.Empty:_tipoPromocao; }
			set { _tipoPromocao=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!PromocaoTipoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (promocaoTipoId="+PromocaoTipoId+")");
			}

			if (!TipoPromocao.Equals(String.Empty)) {
				sbWhere.Append(" AND (tipoPromocao LIKE '%"+TipoPromocao+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
