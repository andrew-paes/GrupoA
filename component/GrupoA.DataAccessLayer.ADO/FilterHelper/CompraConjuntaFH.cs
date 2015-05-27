
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
	public partial class CompraConjuntaFH : IFilterHelper
	{
		private string _compraConjuntaId;
		public string CompraConjuntaId {
			get { return _compraConjuntaId==null?String.Empty:_compraConjuntaId; }
			set { _compraConjuntaId=value; }
		}

		private string _produtoId;
		public string ProdutoId {
			get { return _produtoId==null?String.Empty:_produtoId; }
			set { _produtoId=value; }
		}

		private string _dataInicialCompraPeriodoInicial;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataInicialCompraPeriodoInicial {
			get { return _dataInicialCompraPeriodoInicial==null?String.Empty:_dataInicialCompraPeriodoInicial; }
			set { _dataInicialCompraPeriodoInicial=value; }
		}
		private string _dataInicialCompraPeriodoFinal;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataInicialCompraPeriodoFinal {
			get { return _dataInicialCompraPeriodoFinal==null?String.Empty:_dataInicialCompraPeriodoFinal; }
			set { _dataInicialCompraPeriodoFinal=value; }
		}

		private string _dataFinalCompraPeriodoInicial;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataFinalCompraPeriodoInicial {
			get { return _dataFinalCompraPeriodoInicial==null?String.Empty:_dataFinalCompraPeriodoInicial; }
			set { _dataFinalCompraPeriodoInicial=value; }
		}
		private string _dataFinalCompraPeriodoFinal;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataFinalCompraPeriodoFinal {
			get { return _dataFinalCompraPeriodoFinal==null?String.Empty:_dataFinalCompraPeriodoFinal; }
			set { _dataFinalCompraPeriodoFinal=value; }
		}

		private string _estoqueSeguranca;
		public string EstoqueSeguranca {
			get { return _estoqueSeguranca==null?String.Empty:_estoqueSeguranca; }
			set { _estoqueSeguranca=value; }
		}

		private string _dataHoraFinalizacaoPeriodoInicial;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataHoraFinalizacaoPeriodoInicial {
			get { return _dataHoraFinalizacaoPeriodoInicial==null?String.Empty:_dataHoraFinalizacaoPeriodoInicial; }
			set { _dataHoraFinalizacaoPeriodoInicial=value; }
		}
		private string _dataHoraFinalizacaoPeriodoFinal;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataHoraFinalizacaoPeriodoFinal {
			get { return _dataHoraFinalizacaoPeriodoFinal==null?String.Empty:_dataHoraFinalizacaoPeriodoFinal; }
			set { _dataHoraFinalizacaoPeriodoFinal=value; }
		}

		private string _ativa;
		public string Ativa {
			get { return _ativa==null?String.Empty:_ativa; }
			set { _ativa=value; }
		}

		private string _compraConjuntaStatusId;
		public string CompraConjuntaStatusId {
			get { return _compraConjuntaStatusId==null?String.Empty:_compraConjuntaStatusId; }
			set { _compraConjuntaStatusId=value; }
		}

		private string _quantidadeLimite;
		public string QuantidadeLimite {
			get { return _quantidadeLimite==null?String.Empty:_quantidadeLimite; }
			set { _quantidadeLimite=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!CompraConjuntaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (compraConjuntaId="+CompraConjuntaId+")");
			}

			if (!ProdutoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (produtoId="+ProdutoId+")");
			}

			if( !DataInicialCompraPeriodoInicial.Equals(String.Empty) && !DataInicialCompraPeriodoFinal.Equals(String.Empty)) {
				sbWhere.Append(" AND (dataInicialCompra >='"+DataInicialCompraPeriodoInicial+"'");
				sbWhere.Append(" AND dataInicialCompra <='"+DataInicialCompraPeriodoFinal+"')");
			} else if (!DataInicialCompraPeriodoInicial.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataInicialCompra >='"+DataInicialCompraPeriodoInicial+"')");
			} else if (!DataInicialCompraPeriodoFinal.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataInicialCompra <='"+DataInicialCompraPeriodoFinal+"')");
			}

			if( !DataFinalCompraPeriodoInicial.Equals(String.Empty) && !DataFinalCompraPeriodoFinal.Equals(String.Empty)) {
				sbWhere.Append(" AND (dataFinalCompra >='"+DataFinalCompraPeriodoInicial+"'");
				sbWhere.Append(" AND dataFinalCompra <='"+DataFinalCompraPeriodoFinal+"')");
			} else if (!DataFinalCompraPeriodoInicial.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataFinalCompra >='"+DataFinalCompraPeriodoInicial+"')");
			} else if (!DataFinalCompraPeriodoFinal.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataFinalCompra <='"+DataFinalCompraPeriodoFinal+"')");
			}

			if (!EstoqueSeguranca.Equals(String.Empty)) {
				sbWhere.Append(" AND (estoqueSeguranca="+EstoqueSeguranca+")");
			}

			if( !DataHoraFinalizacaoPeriodoInicial.Equals(String.Empty) && !DataHoraFinalizacaoPeriodoFinal.Equals(String.Empty)) {
				sbWhere.Append(" AND (dataHoraFinalizacao >='"+DataHoraFinalizacaoPeriodoInicial+"'");
				sbWhere.Append(" AND dataHoraFinalizacao <='"+DataHoraFinalizacaoPeriodoFinal+"')");
			} else if (!DataHoraFinalizacaoPeriodoInicial.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataHoraFinalizacao >='"+DataHoraFinalizacaoPeriodoInicial+"')");
			} else if (!DataHoraFinalizacaoPeriodoFinal.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataHoraFinalizacao <='"+DataHoraFinalizacaoPeriodoFinal+"')");
			}

			if (!Ativa.Equals(String.Empty)) {
				sbWhere.Append(" AND (ativa LIKE '%"+Ativa+"%')");
			}

			if (!CompraConjuntaStatusId.Equals(String.Empty)) {
				sbWhere.Append(" AND (compraConjuntaStatusId="+CompraConjuntaStatusId+")");
			}

			if (!QuantidadeLimite.Equals(String.Empty)) {
				sbWhere.Append(" AND (quantidadeLimite="+QuantidadeLimite+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
