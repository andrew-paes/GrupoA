
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
	public partial class AvisoDisponibilidadeStatusFH : IFilterHelper
	{
		private string _avisoDisponibilidadeStatusId;
		public string AvisoDisponibilidadeStatusId {
			get { return _avisoDisponibilidadeStatusId==null?String.Empty:_avisoDisponibilidadeStatusId; }
			set { _avisoDisponibilidadeStatusId=value; }
		}

		private string _statusAviso;
		public string StatusAviso {
			get { return _statusAviso==null?String.Empty:_statusAviso; }
			set { _statusAviso=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!AvisoDisponibilidadeStatusId.Equals(String.Empty)) {
				sbWhere.Append(" AND (avisoDisponibilidadeStatusId="+AvisoDisponibilidadeStatusId+")");
			}

			if (!StatusAviso.Equals(String.Empty)) {
				sbWhere.Append(" AND (statusAviso LIKE '%"+StatusAviso+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
