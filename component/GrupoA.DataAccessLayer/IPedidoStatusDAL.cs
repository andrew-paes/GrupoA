
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
    public partial interface IPedidoStatusDAL
    {	
        void Inserir(PedidoStatus entidade);
        void Atualizar(PedidoStatus entidade);
        void Excluir(PedidoStatus entidade);
        PedidoStatus Carregar(PedidoStatus entidade);
		
		IEnumerable<PedidoStatus> Carregar(Pedido entidade);
		
		IEnumerable<PedidoStatus> Carregar(PedidoSituacao entidade);
				
        IEnumerable<PedidoStatus> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<PedidoStatus> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
