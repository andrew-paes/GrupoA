
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
	public partial class PedidoFH : IFilterHelper
	{
		private string _pedidoId;
		public string PedidoId {
			get { return _pedidoId==null?String.Empty:_pedidoId; }
			set { _pedidoId=value; }
		}

		private string _usuarioId;
		public string UsuarioId {
			get { return _usuarioId==null?String.Empty:_usuarioId; }
			set { _usuarioId=value; }
		}

		private string _dataHoraPedidoPeriodoInicial;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataHoraPedidoPeriodoInicial {
			get { return _dataHoraPedidoPeriodoInicial==null?String.Empty:_dataHoraPedidoPeriodoInicial; }
			set { _dataHoraPedidoPeriodoInicial=value; }
		}
		private string _dataHoraPedidoPeriodoFinal;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataHoraPedidoPeriodoFinal {
			get { return _dataHoraPedidoPeriodoFinal==null?String.Empty:_dataHoraPedidoPeriodoFinal; }
			set { _dataHoraPedidoPeriodoFinal=value; }
		}

		private string _carrinhoId;
		public string CarrinhoId {
			get { return _carrinhoId==null?String.Empty:_carrinhoId; }
			set { _carrinhoId=value; }
		}

		private string _pedidoStatusId;
		public string PedidoStatusId {
			get { return _pedidoStatusId==null?String.Empty:_pedidoStatusId; }
			set { _pedidoStatusId=value; }
		}

		private string _freteValor;
		public string FreteValor {
			get { return _freteValor==null?String.Empty:_freteValor; }
			set { _freteValor=value; }
		}

		private string _valorPedido;
		public string ValorPedido {
			get { return _valorPedido==null?String.Empty:_valorPedido; }
			set { _valorPedido=value; }
		}

		private string _pagamentoId;
		public string PagamentoId {
			get { return _pagamentoId==null?String.Empty:_pagamentoId; }
			set { _pagamentoId=value; }
		}

		private string _transportadoraServicoId;
		public string TransportadoraServicoId {
			get { return _transportadoraServicoId==null?String.Empty:_transportadoraServicoId; }
			set { _transportadoraServicoId=value; }
		}

		private string _pedidoCodigo;
		public string PedidoCodigo {
			get { return _pedidoCodigo==null?String.Empty:_pedidoCodigo; }
			set { _pedidoCodigo=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!PedidoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (pedidoId="+PedidoId+")");
			}

			if (!UsuarioId.Equals(String.Empty)) {
				sbWhere.Append(" AND (usuarioId="+UsuarioId+")");
			}

			if( !DataHoraPedidoPeriodoInicial.Equals(String.Empty) && !DataHoraPedidoPeriodoFinal.Equals(String.Empty)) {
				sbWhere.Append(" AND (dataHoraPedido >='"+DataHoraPedidoPeriodoInicial+"'");
				sbWhere.Append(" AND dataHoraPedido <='"+DataHoraPedidoPeriodoFinal+"')");
			} else if (!DataHoraPedidoPeriodoInicial.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataHoraPedido >='"+DataHoraPedidoPeriodoInicial+"')");
			} else if (!DataHoraPedidoPeriodoFinal.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataHoraPedido <='"+DataHoraPedidoPeriodoFinal+"')");
			}

			if (!CarrinhoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (carrinhoId="+CarrinhoId+")");
			}

			if (!PedidoStatusId.Equals(String.Empty)) {
				sbWhere.Append(" AND (pedidoStatusId="+PedidoStatusId+")");
			}

			if (!FreteValor.Equals(String.Empty)) {
				sbWhere.Append(" AND (freteValor="+FreteValor+")");
			}

			if (!ValorPedido.Equals(String.Empty)) {
				sbWhere.Append(" AND (valorPedido="+ValorPedido+")");
			}

			if (!PagamentoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (pagamentoId="+PagamentoId+")");
			}

			if (!TransportadoraServicoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (transportadoraServicoId="+TransportadoraServicoId+")");
			}

			if (!PedidoCodigo.Equals(String.Empty)) {
				sbWhere.Append(" AND (pedidoCodigo="+PedidoCodigo+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
