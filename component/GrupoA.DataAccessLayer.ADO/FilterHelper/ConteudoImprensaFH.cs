
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
	public partial class ConteudoImprensaFH : IFilterHelper
	{
		private string _conteudoImprensaId;
		public string ConteudoImprensaId {
			get { return _conteudoImprensaId==null?String.Empty:_conteudoImprensaId; }
			set { _conteudoImprensaId=value; }
		}

		private string _fonte;
		public string Fonte {
			get { return _fonte==null?String.Empty:_fonte; }
			set { _fonte=value; }
		}

		private string _fonteUrl;
		public string FonteUrl {
			get { return _fonteUrl==null?String.Empty:_fonteUrl; }
			set { _fonteUrl=value; }
		}

		private string _ativo;
		public string Ativo {
			get { return _ativo==null?String.Empty:_ativo; }
			set { _ativo=value; }
		}

		private string _dataExibicaoInicioPeriodoInicial;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataExibicaoInicioPeriodoInicial {
			get { return _dataExibicaoInicioPeriodoInicial==null?String.Empty:_dataExibicaoInicioPeriodoInicial; }
			set { _dataExibicaoInicioPeriodoInicial=value; }
		}
		private string _dataExibicaoInicioPeriodoFinal;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataExibicaoInicioPeriodoFinal {
			get { return _dataExibicaoInicioPeriodoFinal==null?String.Empty:_dataExibicaoInicioPeriodoFinal; }
			set { _dataExibicaoInicioPeriodoFinal=value; }
		}

		private string _dataExibicaoFimPeriodoInicial;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataExibicaoFimPeriodoInicial {
			get { return _dataExibicaoFimPeriodoInicial==null?String.Empty:_dataExibicaoFimPeriodoInicial; }
			set { _dataExibicaoFimPeriodoInicial=value; }
		}
		private string _dataExibicaoFimPeriodoFinal;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataExibicaoFimPeriodoFinal {
			get { return _dataExibicaoFimPeriodoFinal==null?String.Empty:_dataExibicaoFimPeriodoFinal; }
			set { _dataExibicaoFimPeriodoFinal=value; }
		}

		private string _resumo;
		public string Resumo {
			get { return _resumo==null?String.Empty:_resumo; }
			set { _resumo=value; }
		}

		private string _texto;
		public string Texto {
			get { return _texto==null?String.Empty:_texto; }
			set { _texto=value; }
		}

		private string _destaque;
		public string Destaque {
			get { return _destaque==null?String.Empty:_destaque; }
			set { _destaque=value; }
		}

		private string _titulo;
		public string Titulo {
			get { return _titulo==null?String.Empty:_titulo; }
			set { _titulo=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!ConteudoImprensaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (conteudoImprensaId="+ConteudoImprensaId+")");
			}

			if (!Fonte.Equals(String.Empty)) {
				sbWhere.Append(" AND (fonte LIKE '%"+Fonte+"%')");
			}

			if (!FonteUrl.Equals(String.Empty)) {
				sbWhere.Append(" AND (fonteUrl LIKE '%"+FonteUrl+"%')");
			}

			if (!Ativo.Equals(String.Empty)) {
				sbWhere.Append(" AND (ativo LIKE '%"+Ativo+"%')");
			}

			if( !DataExibicaoInicioPeriodoInicial.Equals(String.Empty) && !DataExibicaoInicioPeriodoFinal.Equals(String.Empty)) {
				sbWhere.Append(" AND (dataExibicaoInicio >='"+DataExibicaoInicioPeriodoInicial+"'");
				sbWhere.Append(" AND dataExibicaoInicio <='"+DataExibicaoInicioPeriodoFinal+"')");
			} else if (!DataExibicaoInicioPeriodoInicial.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataExibicaoInicio >='"+DataExibicaoInicioPeriodoInicial+"')");
			} else if (!DataExibicaoInicioPeriodoFinal.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataExibicaoInicio <='"+DataExibicaoInicioPeriodoFinal+"')");
			}

			if( !DataExibicaoFimPeriodoInicial.Equals(String.Empty) && !DataExibicaoFimPeriodoFinal.Equals(String.Empty)) {
				sbWhere.Append(" AND (dataExibicaoFim >='"+DataExibicaoFimPeriodoInicial+"'");
				sbWhere.Append(" AND dataExibicaoFim <='"+DataExibicaoFimPeriodoFinal+"')");
			} else if (!DataExibicaoFimPeriodoInicial.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataExibicaoFim >='"+DataExibicaoFimPeriodoInicial+"')");
			} else if (!DataExibicaoFimPeriodoFinal.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataExibicaoFim <='"+DataExibicaoFimPeriodoFinal+"')");
			}

			if (!Resumo.Equals(String.Empty)) {
				sbWhere.Append(" AND (resumo LIKE '%"+Resumo+"%')");
			}

			if (!Texto.Equals(String.Empty)) {
				sbWhere.Append(" AND (texto LIKE '%"+Texto+"%')");
			}

			if (!Destaque.Equals(String.Empty)) {
				sbWhere.Append(" AND (destaque LIKE '%"+Destaque+"%')");
			}

			if (!Titulo.Equals(String.Empty)) {
				sbWhere.Append(" AND (titulo LIKE '%"+Titulo+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
