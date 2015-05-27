
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
using System.Collections.Generic;
using GrupoA.BusinessObject;
using GrupoA.FilterHelper;

namespace GrupoA.DataAccess
{
    public partial interface IPedidoDAL
    {	
        void Inserir(Pedido entidade);
        void Atualizar(Pedido entidade);
        void Excluir(Pedido entidade);
        Pedido Carregar(Pedido entidade);
		
		IEnumerable<Pedido> Carregar(PedidoItem entidade);
		
		IEnumerable<Pedido> Carregar(Promocao entidade);
		
		IEnumerable<Pedido> Carregar(PedidoSituacao entidade);
		
		IEnumerable<Pedido> Carregar(Carrinho entidade);
		
		IEnumerable<Pedido> Carregar(PedidoStatus entidade);
		
		IEnumerable<Pedido> Carregar(TransportadoraServico entidade);
		
		IEnumerable<Pedido> Carregar(Usuario entidade);
		
		Pedido Carregar(PedidoEndereco entidade);
		
		Pedido Carregar(Pagamento entidade);
				
        IEnumerable<Pedido> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<Pedido> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);

        
    }
}
