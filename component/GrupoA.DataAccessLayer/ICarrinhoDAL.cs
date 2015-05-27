
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
    public partial interface ICarrinhoDAL
    {	
        void Inserir(Carrinho entidade);
        void Atualizar(Carrinho entidade);
        void Excluir(Carrinho entidade);
        Carrinho Carregar(Carrinho entidade);
		
		IEnumerable<Carrinho> Carregar(CarrinhoItem entidade);
		
		IEnumerable<Carrinho> Carregar(Pedido entidade);
		
		IEnumerable<Carrinho> Carregar(CarrinhoStatus entidade);
		
		IEnumerable<Carrinho> Carregar(Usuario entidade);
				
        IEnumerable<Carrinho> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<Carrinho> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
