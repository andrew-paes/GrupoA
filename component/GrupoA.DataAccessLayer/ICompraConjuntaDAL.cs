
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
    public partial interface ICompraConjuntaDAL
    {	
        void Inserir(CompraConjunta entidade);
        void Atualizar(CompraConjunta entidade);
        void Excluir(CompraConjunta entidade);
        CompraConjunta Carregar(CompraConjunta entidade);
		
		IEnumerable<CompraConjunta> Carregar(CarrinhoItemCompraConjunta entidade);
		
		IEnumerable<CompraConjunta> Carregar(CompraConjuntaDesconto entidade);
		
		IEnumerable<CompraConjunta> Carregar(CompraConjuntaPagina entidade);
		
		IEnumerable<CompraConjunta> Carregar(PedidoCompraConjunta entidade);
		
		IEnumerable<CompraConjunta> Carregar(PedidoItemCompraConjunta entidade);
		
		IEnumerable<CompraConjunta> Carregar(CompraConjuntaStatus entidade);
		
		IEnumerable<CompraConjunta> Carregar(Produto entidade);
				
        IEnumerable<CompraConjunta> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<CompraConjunta> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
