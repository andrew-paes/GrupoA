
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
    public partial interface ICarrinhoItemCompraConjuntaDAL
    {	
        void Inserir(CarrinhoItemCompraConjunta entidade);
        void Atualizar(CarrinhoItemCompraConjunta entidade);
        void Excluir(CarrinhoItemCompraConjunta entidade);
        CarrinhoItemCompraConjunta Carregar(CarrinhoItemCompraConjunta entidade);
				
		CarrinhoItemCompraConjunta CarregarComDependencias(CarrinhoItemCompraConjunta entidade);	
		
		IEnumerable<CarrinhoItemCompraConjunta> Carregar(CompraConjunta entidade);
				
        IEnumerable<CarrinhoItemCompraConjunta> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<CarrinhoItemCompraConjunta> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
