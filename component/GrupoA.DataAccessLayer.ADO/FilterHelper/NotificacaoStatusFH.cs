
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
	public partial class NotificacaoStatusFH : IFilterHelper
	{
		private string _notificacaoStatusId;
		public string NotificacaoStatusId {
			get { return _notificacaoStatusId==null?String.Empty:_notificacaoStatusId; }
			set { _notificacaoStatusId=value; }
		}

		private string _statusNotificacao;
		public string StatusNotificacao {
			get { return _statusNotificacao==null?String.Empty:_statusNotificacao; }
			set { _statusNotificacao=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!NotificacaoStatusId.Equals(String.Empty)) {
				sbWhere.Append(" AND (notificacaoStatusId="+NotificacaoStatusId+")");
			}

			if (!StatusNotificacao.Equals(String.Empty)) {
				sbWhere.Append(" AND (statusNotificacao LIKE '%"+StatusNotificacao+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
