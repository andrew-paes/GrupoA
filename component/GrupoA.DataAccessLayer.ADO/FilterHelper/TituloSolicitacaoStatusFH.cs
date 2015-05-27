
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
	public partial class TituloSolicitacaoStatusFH : IFilterHelper
	{
		private string _tituloSolicitacaoStatusId;
		public string TituloSolicitacaoStatusId {
			get { return _tituloSolicitacaoStatusId==null?String.Empty:_tituloSolicitacaoStatusId; }
			set { _tituloSolicitacaoStatusId=value; }
		}

		private string _statusSolicitacao;
		public string StatusSolicitacao {
			get { return _statusSolicitacao==null?String.Empty:_statusSolicitacao; }
			set { _statusSolicitacao=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!TituloSolicitacaoStatusId.Equals(String.Empty)) {
				sbWhere.Append(" AND (tituloSolicitacaoStatusId="+TituloSolicitacaoStatusId+")");
			}

			if (!StatusSolicitacao.Equals(String.Empty)) {
				sbWhere.Append(" AND (statusSolicitacao LIKE '%"+StatusSolicitacao+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
