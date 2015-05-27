
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
    public partial interface ICarrinhoItemDAL
    {	
        void Inserir(CarrinhoItem entidade);
        void Atualizar(CarrinhoItem entidade);
        void Excluir(CarrinhoItem entidade);
        CarrinhoItem Carregar(CarrinhoItem entidade);
		
		IEnumerable<CarrinhoItem> Carregar(Carrinho entidade);
		
		IEnumerable<CarrinhoItem> Carregar(Produto entidade);
				
        IEnumerable<CarrinhoItem> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<CarrinhoItem> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
