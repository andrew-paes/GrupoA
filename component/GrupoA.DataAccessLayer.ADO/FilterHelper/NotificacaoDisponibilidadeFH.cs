
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
	public partial class NotificacaoDisponibilidadeFH : IFilterHelper
	{
		private string _notificacaoDisponibilidadeId;
		public string NotificacaoDisponibilidadeId {
			get { return _notificacaoDisponibilidadeId==null?String.Empty:_notificacaoDisponibilidadeId; }
			set { _notificacaoDisponibilidadeId=value; }
		}

		private string _produtoId;
		public string ProdutoId {
			get { return _produtoId==null?String.Empty:_produtoId; }
			set { _produtoId=value; }
		}

		private string _usuarioId;
		public string UsuarioId {
			get { return _usuarioId==null?String.Empty:_usuarioId; }
			set { _usuarioId=value; }
		}

		private string _dataHoraSolicitacaoPeriodoInicial;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataHoraSolicitacaoPeriodoInicial {
			get { return _dataHoraSolicitacaoPeriodoInicial==null?String.Empty:_dataHoraSolicitacaoPeriodoInicial; }
			set { _dataHoraSolicitacaoPeriodoInicial=value; }
		}
		private string _dataHoraSolicitacaoPeriodoFinal;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataHoraSolicitacaoPeriodoFinal {
			get { return _dataHoraSolicitacaoPeriodoFinal==null?String.Empty:_dataHoraSolicitacaoPeriodoFinal; }
			set { _dataHoraSolicitacaoPeriodoFinal=value; }
		}

		private string _notificacaoStatusId;
		public string NotificacaoStatusId {
			get { return _notificacaoStatusId==null?String.Empty:_notificacaoStatusId; }
			set { _notificacaoStatusId=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!NotificacaoDisponibilidadeId.Equals(String.Empty)) {
				sbWhere.Append(" AND (notificacaoDisponibilidadeId="+NotificacaoDisponibilidadeId+")");
			}

			if (!ProdutoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (produtoId="+ProdutoId+")");
			}

			if (!UsuarioId.Equals(String.Empty)) {
				sbWhere.Append(" AND (usuarioId="+UsuarioId+")");
			}

			if( !DataHoraSolicitacaoPeriodoInicial.Equals(String.Empty) && !DataHoraSolicitacaoPeriodoFinal.Equals(String.Empty)) {
				sbWhere.Append(" AND (dataHoraSolicitacao >='"+DataHoraSolicitacaoPeriodoInicial+"'");
				sbWhere.Append(" AND dataHoraSolicitacao <='"+DataHoraSolicitacaoPeriodoFinal+"')");
			} else if (!DataHoraSolicitacaoPeriodoInicial.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataHoraSolicitacao >='"+DataHoraSolicitacaoPeriodoInicial+"')");
			} else if (!DataHoraSolicitacaoPeriodoFinal.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataHoraSolicitacao <='"+DataHoraSolicitacaoPeriodoFinal+"')");
			}

			if (!NotificacaoStatusId.Equals(String.Empty)) {
				sbWhere.Append(" AND (notificacaoStatusId="+NotificacaoStatusId+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
