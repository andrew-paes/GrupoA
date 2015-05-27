
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
	public partial class AvisoDisponibilidadeFH : IFilterHelper
	{
		private string _avisoDisponibilidadeId;
		public string AvisoDisponibilidadeId {
			get { return _avisoDisponibilidadeId==null?String.Empty:_avisoDisponibilidadeId; }
			set { _avisoDisponibilidadeId=value; }
		}

		private string _email;
		public string Email {
			get { return _email==null?String.Empty:_email; }
			set { _email=value; }
		}

		private string _dataSolicitacaoPeriodoInicial;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataSolicitacaoPeriodoInicial {
			get { return _dataSolicitacaoPeriodoInicial==null?String.Empty:_dataSolicitacaoPeriodoInicial; }
			set { _dataSolicitacaoPeriodoInicial=value; }
		}
		private string _dataSolicitacaoPeriodoFinal;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataSolicitacaoPeriodoFinal {
			get { return _dataSolicitacaoPeriodoFinal==null?String.Empty:_dataSolicitacaoPeriodoFinal; }
			set { _dataSolicitacaoPeriodoFinal=value; }
		}

		private string _dataNotificacaoPeriodoInicial;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataNotificacaoPeriodoInicial {
			get { return _dataNotificacaoPeriodoInicial==null?String.Empty:_dataNotificacaoPeriodoInicial; }
			set { _dataNotificacaoPeriodoInicial=value; }
		}
		private string _dataNotificacaoPeriodoFinal;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataNotificacaoPeriodoFinal {
			get { return _dataNotificacaoPeriodoFinal==null?String.Empty:_dataNotificacaoPeriodoFinal; }
			set { _dataNotificacaoPeriodoFinal=value; }
		}

		private string _produtoId;
		public string ProdutoId {
			get { return _produtoId==null?String.Empty:_produtoId; }
			set { _produtoId=value; }
		}

		private string _avisoDisponibilidadeStatusId;
		public string AvisoDisponibilidadeStatusId {
			get { return _avisoDisponibilidadeStatusId==null?String.Empty:_avisoDisponibilidadeStatusId; }
			set { _avisoDisponibilidadeStatusId=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!AvisoDisponibilidadeId.Equals(String.Empty)) {
				sbWhere.Append(" AND (avisoDisponibilidadeId="+AvisoDisponibilidadeId+")");
			}

			if (!Email.Equals(String.Empty)) {
				sbWhere.Append(" AND (email LIKE '%"+Email+"%')");
			}

			if( !DataSolicitacaoPeriodoInicial.Equals(String.Empty) && !DataSolicitacaoPeriodoFinal.Equals(String.Empty)) {
				sbWhere.Append(" AND (dataSolicitacao >='"+DataSolicitacaoPeriodoInicial+"'");
				sbWhere.Append(" AND dataSolicitacao <='"+DataSolicitacaoPeriodoFinal+"')");
			} else if (!DataSolicitacaoPeriodoInicial.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataSolicitacao >='"+DataSolicitacaoPeriodoInicial+"')");
			} else if (!DataSolicitacaoPeriodoFinal.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataSolicitacao <='"+DataSolicitacaoPeriodoFinal+"')");
			}

			if( !DataNotificacaoPeriodoInicial.Equals(String.Empty) && !DataNotificacaoPeriodoFinal.Equals(String.Empty)) {
				sbWhere.Append(" AND (dataNotificacao >='"+DataNotificacaoPeriodoInicial+"'");
				sbWhere.Append(" AND dataNotificacao <='"+DataNotificacaoPeriodoFinal+"')");
			} else if (!DataNotificacaoPeriodoInicial.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataNotificacao >='"+DataNotificacaoPeriodoInicial+"')");
			} else if (!DataNotificacaoPeriodoFinal.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataNotificacao <='"+DataNotificacaoPeriodoFinal+"')");
			}

			if (!ProdutoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (produtoId="+ProdutoId+")");
			}

			if (!AvisoDisponibilidadeStatusId.Equals(String.Empty)) {
				sbWhere.Append(" AND (avisoDisponibilidadeStatusId="+AvisoDisponibilidadeStatusId+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
