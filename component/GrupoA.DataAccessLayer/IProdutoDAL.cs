
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
    public partial interface IProdutoDAL
    {	
        void Inserir(Produto entidade);
        void Atualizar(Produto entidade);
        void Excluir(Produto entidade);
        Produto Carregar(Produto entidade);
				
		Produto CarregarComDependencias(Produto entidade);	
		
		IEnumerable<Produto> Carregar(AvisoDisponibilidade entidade);
		
		IEnumerable<Produto> Carregar(CarrinhoItem entidade);
		
		IEnumerable<Produto> Carregar(CompraConjunta entidade);
		
		IEnumerable<Produto> Carregar(NotificacaoDisponibilidade entidade);
		
		IEnumerable<Produto> Carregar(PedidoItem entidade);
		
		IEnumerable<Produto> Carregar(Categoria entidade);
		
		IEnumerable<Produto> Carregar(ProdutoImagem entidade);
		
		IEnumerable<Produto> Carregar(Selo entidade);
		
		IEnumerable<Produto> Carregar(Promocao entidade);
		
		IEnumerable<Produto> Carregar(Fabricante entidade);
		
		IEnumerable<Produto> Carregar(ProdutoTipo entidade);

        List<Selo> CarregarSelos(Produto entidade);

        void AtualizaHomologado(Produto entidade);
				
        IEnumerable<Produto> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<Produto> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
