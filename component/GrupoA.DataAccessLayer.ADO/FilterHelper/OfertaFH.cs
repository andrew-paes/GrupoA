
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
	public partial class OfertaFH : IFilterHelper
	{
		private string _ofertaId;
		public string OfertaId {
			get { return _ofertaId==null?String.Empty:_ofertaId; }
			set { _ofertaId=value; }
		}

		private string _ofertaTipoId;
		public string OfertaTipoId {
			get { return _ofertaTipoId==null?String.Empty:_ofertaTipoId; }
			set { _ofertaTipoId=value; }
		}

		private string _percentual;
		public string Percentual {
			get { return _percentual==null?String.Empty:_percentual; }
			set { _percentual=value; }
		}

		private string _precoOferta;
		public string PrecoOferta {
			get { return _precoOferta==null?String.Empty:_precoOferta; }
			set { _precoOferta=value; }
		}

		private string _nomeOferta;
		public string NomeOferta {
			get { return _nomeOferta==null?String.Empty:_nomeOferta; }
			set { _nomeOferta=value; }
		}

		private string _dataHoraInicioPeriodoInicial;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataHoraInicioPeriodoInicial {
			get { return _dataHoraInicioPeriodoInicial==null?String.Empty:_dataHoraInicioPeriodoInicial; }
			set { _dataHoraInicioPeriodoInicial=value; }
		}
		private string _dataHoraInicioPeriodoFinal;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataHoraInicioPeriodoFinal {
			get { return _dataHoraInicioPeriodoFinal==null?String.Empty:_dataHoraInicioPeriodoFinal; }
			set { _dataHoraInicioPeriodoFinal=value; }
		}

		private string _dataHoraTermino;
		public string DataHoraTermino {
			get { return _dataHoraTermino==null?String.Empty:_dataHoraTermino; }
			set { _dataHoraTermino=value; }
		}

		private string _ofertaAplicada;
		public string OfertaAplicada {
			get { return _ofertaAplicada==null?String.Empty:_ofertaAplicada; }
			set { _ofertaAplicada=value; }
		}

		private string _ofertaExpirada;
		public string OfertaExpirada {
			get { return _ofertaExpirada==null?String.Empty:_ofertaExpirada; }
			set { _ofertaExpirada=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!OfertaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (ofertaId="+OfertaId+")");
			}

			if (!OfertaTipoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (ofertaTipoId="+OfertaTipoId+")");
			}

			if (!Percentual.Equals(String.Empty)) {
				sbWhere.Append(" AND (percentual="+Percentual+")");
			}

			if (!PrecoOferta.Equals(String.Empty)) {
				sbWhere.Append(" AND (precoOferta="+PrecoOferta+")");
			}

			if (!NomeOferta.Equals(String.Empty)) {
				sbWhere.Append(" AND (nomeOferta LIKE '%"+NomeOferta+"%')");
			}

			if( !DataHoraInicioPeriodoInicial.Equals(String.Empty) && !DataHoraInicioPeriodoFinal.Equals(String.Empty)) {
				sbWhere.Append(" AND (dataHoraInicio >='"+DataHoraInicioPeriodoInicial+"'");
				sbWhere.Append(" AND dataHoraInicio <='"+DataHoraInicioPeriodoFinal+"')");
			} else if (!DataHoraInicioPeriodoInicial.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataHoraInicio >='"+DataHoraInicioPeriodoInicial+"')");
			} else if (!DataHoraInicioPeriodoFinal.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataHoraInicio <='"+DataHoraInicioPeriodoFinal+"')");
			}

			if (!DataHoraTermino.Equals(String.Empty)) {
				sbWhere.Append(" AND (dataHoraTermino="+DataHoraTermino+")");
			}

			if (!OfertaAplicada.Equals(String.Empty)) {
				sbWhere.Append(" AND (ofertaAplicada LIKE '%"+OfertaAplicada+"%')");
			}

			if (!OfertaExpirada.Equals(String.Empty)) {
				sbWhere.Append(" AND (ofertaExpirada LIKE '%"+OfertaExpirada+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
