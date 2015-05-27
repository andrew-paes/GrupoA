
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
    public partial interface IPedidoItemCompraConjuntaDAL
    {	
        void Inserir(PedidoItemCompraConjunta entidade);
        void Atualizar(PedidoItemCompraConjunta entidade);
        void Excluir(PedidoItemCompraConjunta entidade);
        PedidoItemCompraConjunta Carregar(PedidoItemCompraConjunta entidade);
				
		PedidoItemCompraConjunta CarregarComDependencias(PedidoItemCompraConjunta entidade);	
		
		IEnumerable<PedidoItemCompraConjunta> Carregar(CompraConjunta entidade);
				
        IEnumerable<PedidoItemCompraConjunta> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<PedidoItemCompraConjunta> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
