
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
	public partial class MeioPagamentoFaixaFH : IFilterHelper
	{
		private string _meioPagamentoFaixaId;
		public string MeioPagamentoFaixaId {
			get { return _meioPagamentoFaixaId==null?String.Empty:_meioPagamentoFaixaId; }
			set { _meioPagamentoFaixaId=value; }
		}

		private string _meioPagamentoId;
		public string MeioPagamentoId {
			get { return _meioPagamentoId==null?String.Empty:_meioPagamentoId; }
			set { _meioPagamentoId=value; }
		}

		private string _valorMinimo;
		public string ValorMinimo {
			get { return _valorMinimo==null?String.Empty:_valorMinimo; }
			set { _valorMinimo=value; }
		}

		private string _numeroParcelas;
		public string NumeroParcelas {
			get { return _numeroParcelas==null?String.Empty:_numeroParcelas; }
			set { _numeroParcelas=value; }
		}

		private string _codigoGatewayFaixa;
		public string CodigoGatewayFaixa {
			get { return _codigoGatewayFaixa==null?String.Empty:_codigoGatewayFaixa; }
			set { _codigoGatewayFaixa=value; }
		}

		private string _codigoLegado;
		public string CodigoLegado {
			get { return _codigoLegado==null?String.Empty:_codigoLegado; }
			set { _codigoLegado=value; }
		}

		private string _taxaJuros;
		public string TaxaJuros {
			get { return _taxaJuros==null?String.Empty:_taxaJuros; }
			set { _taxaJuros=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!MeioPagamentoFaixaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (meioPagamentoFaixaId="+MeioPagamentoFaixaId+")");
			}

			if (!MeioPagamentoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (meioPagamentoId="+MeioPagamentoId+")");
			}

			if (!ValorMinimo.Equals(String.Empty)) {
				sbWhere.Append(" AND (valorMinimo="+ValorMinimo+")");
			}

			if (!NumeroParcelas.Equals(String.Empty)) {
				sbWhere.Append(" AND (numeroParcelas="+NumeroParcelas+")");
			}

			if (!CodigoGatewayFaixa.Equals(String.Empty)) {
				sbWhere.Append(" AND (codigoGatewayFaixa LIKE '%"+CodigoGatewayFaixa+"%')");
			}

			if (!CodigoLegado.Equals(String.Empty)) {
				sbWhere.Append(" AND (codigoLegado LIKE '%"+CodigoLegado+"%')");
			}

			if (!TaxaJuros.Equals(String.Empty)) {
				sbWhere.Append(" AND (taxaJuros="+TaxaJuros+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
