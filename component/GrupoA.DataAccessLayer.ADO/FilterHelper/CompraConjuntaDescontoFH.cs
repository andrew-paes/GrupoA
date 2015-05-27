
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
	public partial class CompraConjuntaDescontoFH : IFilterHelper
	{
		private string _compraConjuntaDescontoId;
		public string CompraConjuntaDescontoId {
			get { return _compraConjuntaDescontoId==null?String.Empty:_compraConjuntaDescontoId; }
			set { _compraConjuntaDescontoId=value; }
		}

		private string _quantidadeMinima;
		public string QuantidadeMinima {
			get { return _quantidadeMinima==null?String.Empty:_quantidadeMinima; }
			set { _quantidadeMinima=value; }
		}

		private string _percentualDesconto;
		public string PercentualDesconto {
			get { return _percentualDesconto==null?String.Empty:_percentualDesconto; }
			set { _percentualDesconto=value; }
		}

		private string _compraConjuntaId;
		public string CompraConjuntaId {
			get { return _compraConjuntaId==null?String.Empty:_compraConjuntaId; }
			set { _compraConjuntaId=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!CompraConjuntaDescontoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (compraConjuntaDescontoId="+CompraConjuntaDescontoId+")");
			}

			if (!QuantidadeMinima.Equals(String.Empty)) {
				sbWhere.Append(" AND (quantidadeMinima="+QuantidadeMinima+")");
			}

			if (!PercentualDesconto.Equals(String.Empty)) {
				sbWhere.Append(" AND (percentualDesconto="+PercentualDesconto+")");
			}

			if (!CompraConjuntaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (compraConjuntaId="+CompraConjuntaId+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
