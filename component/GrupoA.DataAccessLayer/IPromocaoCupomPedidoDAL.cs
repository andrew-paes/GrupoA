
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
    public partial interface IPromocaoCupomPedidoDAL
    {	
        void Inserir(PromocaoCupomPedido entidade);
        void Atualizar(PromocaoCupomPedido entidade);
        void Excluir(PromocaoCupomPedido entidade);
        PromocaoCupomPedido Carregar(PromocaoCupomPedido entidade);
		
		IEnumerable<PromocaoCupomPedido> Carregar(Pedido entidade);
		
		IEnumerable<PromocaoCupomPedido> Carregar(PromocaoCupom entidade);
				
        IEnumerable<PromocaoCupomPedido> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<PromocaoCupomPedido> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
