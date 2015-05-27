
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
	public partial class TituloAvaliacaoFH : IFilterHelper
	{
		private string _tituloAvaliacaoId;
		public string TituloAvaliacaoId {
			get { return _tituloAvaliacaoId==null?String.Empty:_tituloAvaliacaoId; }
			set { _tituloAvaliacaoId=value; }
		}

		private string _tituloSolicitacaoId;
		public string TituloSolicitacaoId {
			get { return _tituloSolicitacaoId==null?String.Empty:_tituloSolicitacaoId; }
			set { _tituloSolicitacaoId=value; }
		}

		private string _avaliacao;
		public string Avaliacao {
			get { return _avaliacao==null?String.Empty:_avaliacao; }
			set { _avaliacao=value; }
		}

		private string _finalizada;
		public string Finalizada {
			get { return _finalizada==null?String.Empty:_finalizada; }
			set { _finalizada=value; }
		}

		private string _dataRealizacaoAvaliacaoPeriodoInicial;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataRealizacaoAvaliacaoPeriodoInicial {
			get { return _dataRealizacaoAvaliacaoPeriodoInicial==null?String.Empty:_dataRealizacaoAvaliacaoPeriodoInicial; }
			set { _dataRealizacaoAvaliacaoPeriodoInicial=value; }
		}
		private string _dataRealizacaoAvaliacaoPeriodoFinal;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataRealizacaoAvaliacaoPeriodoFinal {
			get { return _dataRealizacaoAvaliacaoPeriodoFinal==null?String.Empty:_dataRealizacaoAvaliacaoPeriodoFinal; }
			set { _dataRealizacaoAvaliacaoPeriodoFinal=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!TituloAvaliacaoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (tituloAvaliacaoId="+TituloAvaliacaoId+")");
			}

			if (!TituloSolicitacaoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (tituloSolicitacaoId="+TituloSolicitacaoId+")");
			}

			if (!Avaliacao.Equals(String.Empty)) {
				sbWhere.Append(" AND (avaliacao LIKE '%"+Avaliacao+"%')");
			}

			if (!Finalizada.Equals(String.Empty)) {
				sbWhere.Append(" AND (finalizada LIKE '%"+Finalizada+"%')");
			}

			if( !DataRealizacaoAvaliacaoPeriodoInicial.Equals(String.Empty) && !DataRealizacaoAvaliacaoPeriodoFinal.Equals(String.Empty)) {
				sbWhere.Append(" AND (dataRealizacaoAvaliacao >='"+DataRealizacaoAvaliacaoPeriodoInicial+"'");
				sbWhere.Append(" AND dataRealizacaoAvaliacao <='"+DataRealizacaoAvaliacaoPeriodoFinal+"')");
			} else if (!DataRealizacaoAvaliacaoPeriodoInicial.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataRealizacaoAvaliacao >='"+DataRealizacaoAvaliacaoPeriodoInicial+"')");
			} else if (!DataRealizacaoAvaliacaoPeriodoFinal.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataRealizacaoAvaliacao <='"+DataRealizacaoAvaliacaoPeriodoFinal+"')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
