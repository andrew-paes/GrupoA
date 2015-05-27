
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
	public partial class TelefoneTipoFH : IFilterHelper
	{
		private string _telefoneTipoId;
		public string TelefoneTipoId {
			get { return _telefoneTipoId==null?String.Empty:_telefoneTipoId; }
			set { _telefoneTipoId=value; }
		}

		private string _tipoTelefone;
		public string TipoTelefone {
			get { return _tipoTelefone==null?String.Empty:_tipoTelefone; }
			set { _tipoTelefone=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!TelefoneTipoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (telefoneTipoId="+TelefoneTipoId+")");
			}

			if (!TipoTelefone.Equals(String.Empty)) {
				sbWhere.Append(" AND (tipoTelefone LIKE '%"+TipoTelefone+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
