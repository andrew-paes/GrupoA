
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
    public partial interface IPedidoCompraConjuntaDAL
    {	
        void Inserir(PedidoCompraConjunta entidade);
        void Atualizar(PedidoCompraConjunta entidade);
        void Excluir(PedidoCompraConjunta entidade);
        PedidoCompraConjunta Carregar(PedidoCompraConjunta entidade);
				
		PedidoCompraConjunta CarregarComDependencias(PedidoCompraConjunta entidade);	
		
		IEnumerable<PedidoCompraConjunta> Carregar(CompraConjunta entidade);
		
		IEnumerable<PedidoCompraConjunta> Carregar(CompraConjuntaDesconto entidade);
				
        IEnumerable<PedidoCompraConjunta> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<PedidoCompraConjunta> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
