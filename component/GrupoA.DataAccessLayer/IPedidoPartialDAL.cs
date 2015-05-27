using System;
using System.Collections.Generic;
using GrupoA.BusinessObject;
using GrupoA.BusinessObject.ViewHelper;
using GrupoA.FilterHelper;

namespace GrupoA.DataAccess
{
	public partial interface IPedidoDAL
	{
		Pedido CarregarPedidoComDependencias(Pedido entidade);
		IEnumerable<Pedido> CarregarTodosComDependencias(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<ConfirmacaoPedidoVH> CarregarComDependenciasPorCarrinho(Carrinho carrinho);
        List<Pedido> CarregarPedidosPorCompraConjunta(Int32 compraConjuntaId);
        void InserirPedidoPromocaoCarrinho(Pedido entidade);
        IList<Pedido> CarregarFinalizadosNaoSincronizados();
        void AtualizarStatusPedido(Pedido pedido);
        int ContarTodosPedidos(IFilterHelper filtro);
	}
}
