
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
    public partial interface ICarrinhoStatusDAL
    {	
        void Inserir(CarrinhoStatus entidade);
        void Atualizar(CarrinhoStatus entidade);
        void Excluir(CarrinhoStatus entidade);
        CarrinhoStatus Carregar(CarrinhoStatus entidade);
		
		IEnumerable<CarrinhoStatus> Carregar(Carrinho entidade);
				
        IEnumerable<CarrinhoStatus> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<CarrinhoStatus> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
