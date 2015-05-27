using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GrupoA.DataAccess.ADO;
using GrupoA.BusinessObject;
using System.Transactions;

namespace GrupoA.BusinessLogicalLayer
{
	public class PedidoExportacaoBLL : BaseBLL
	{
		/// <summary>
		/// Metodo que exporta dados do pedido para web service.
		/// </summary>
		public void ExportarPedidos()
		{
			PedidoADO pedidoADO = new PedidoADO();
			IEnumerable<Pedido> pedidos = pedidoADO.CarregaPedidoParaExportacao();

			ComumicacaoComWebService(pedidos);
		}

		/// <summary>
		/// Metodo que faz a comunicação com o web service do cliente
		/// </summary>
		/// <param name="pedidos">Lista de pedidos a ser enviada para o cliente</param>
		private void ComumicacaoComWebService(IEnumerable<Pedido> pedidos)
		{
			foreach (Pedido pedido in pedidos)
			{
				using (TransactionScope scope = new TransactionScope())
				{
					// TODO: Chamar Web Service
					pedido.PedidoCodigo = 1234;
					AtualizaPedidoWebService(pedido);

					scope.Complete();
				}
			}
		}

		/// <summary>
		/// Metodo que atualiza dados de resposta do web service do cliente
		/// </summary>
		/// <param name="pedido">Pedido para ser atualizado no banco</param>
		private void AtualizaPedidoWebService(Pedido pedido)
		{
			PedidoADO pedidoADO = new PedidoADO();
			pedidoADO.Atualizar(pedido);

			PedidoControleADO pedidoControleADO = new PedidoControleADO();

			PedidoControle pedidoControle = new PedidoControle();
			pedidoControle.PedidoId = pedido.PedidoId;
			pedidoControle.DataHoraExportacao = DateTime.Now;

			pedidoControleADO.Inserir(pedidoControle);
		}

		/// <summary>
		/// Metodo que atualiza dados de resposta do web service do cliente
		/// </summary>
		/// <param name="pedido">Pedido para ser atualizado no banco</param>
		public void AtualizaWebService(Pedido pedido)
		{
			PedidoControleADO pedidoControleADO = new PedidoControleADO();

			PedidoControle pedidoControle = new PedidoControle();
			pedidoControle.PedidoId = pedido.PedidoId;
			pedidoControle.DataHoraExportacao = DateTime.Now;

			pedidoControleADO.Inserir(pedidoControle);
		}
	}
}