
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
	public partial class TituloSolicitacaoFH : IFilterHelper
	{
		private string _tituloSolicitacaoId;
		public string TituloSolicitacaoId {
			get { return _tituloSolicitacaoId==null?String.Empty:_tituloSolicitacaoId; }
			set { _tituloSolicitacaoId=value; }
		}

		private string _professorId;
		public string ProfessorId {
			get { return _professorId==null?String.Empty:_professorId; }
			set { _professorId=value; }
		}

		private string _tituloId;
		public string TituloId {
			get { return _tituloId==null?String.Empty:_tituloId; }
			set { _tituloId=value; }
		}

		private string _tituloSolicitacaoStatusId;
		public string TituloSolicitacaoStatusId {
			get { return _tituloSolicitacaoStatusId==null?String.Empty:_tituloSolicitacaoStatusId; }
			set { _tituloSolicitacaoStatusId=value; }
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

		private string _justificativaProfessor;
		public string JustificativaProfessor {
			get { return _justificativaProfessor==null?String.Empty:_justificativaProfessor; }
			set { _justificativaProfessor=value; }
		}

		private string _exportada;
		public string Exportada {
			get { return _exportada==null?String.Empty:_exportada; }
			set { _exportada=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!TituloSolicitacaoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (tituloSolicitacaoId="+TituloSolicitacaoId+")");
			}

			if (!ProfessorId.Equals(String.Empty)) {
				sbWhere.Append(" AND (professorId="+ProfessorId+")");
			}

			if (!TituloId.Equals(String.Empty)) {
				sbWhere.Append(" AND (tituloId="+TituloId+")");
			}

			if (!TituloSolicitacaoStatusId.Equals(String.Empty)) {
				sbWhere.Append(" AND (tituloSolicitacaoStatusId="+TituloSolicitacaoStatusId+")");
			}

			if( !DataSolicitacaoPeriodoInicial.Equals(String.Empty) && !DataSolicitacaoPeriodoFinal.Equals(String.Empty)) {
				sbWhere.Append(" AND (dataSolicitacao >='"+DataSolicitacaoPeriodoInicial+"'");
				sbWhere.Append(" AND dataSolicitacao <='"+DataSolicitacaoPeriodoFinal+"')");
			} else if (!DataSolicitacaoPeriodoInicial.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataSolicitacao >='"+DataSolicitacaoPeriodoInicial+"')");
			} else if (!DataSolicitacaoPeriodoFinal.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataSolicitacao <='"+DataSolicitacaoPeriodoFinal+"')");
			}

			if (!JustificativaProfessor.Equals(String.Empty)) {
				sbWhere.Append(" AND (justificativaProfessor LIKE '%"+JustificativaProfessor+"%')");
			}

			if (!Exportada.Equals(String.Empty)) {
				sbWhere.Append(" AND (exportada LIKE '%"+Exportada+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
