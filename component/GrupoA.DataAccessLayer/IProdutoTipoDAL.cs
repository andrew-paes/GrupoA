
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
    public partial interface IProdutoTipoDAL
    {	
        void Inserir(ProdutoTipo entidade);
        void Atualizar(ProdutoTipo entidade);
        void Excluir(ProdutoTipo entidade);
        ProdutoTipo Carregar(ProdutoTipo entidade);
		
		IEnumerable<ProdutoTipo> Carregar(Produto entidade);
		
		IEnumerable<ProdutoTipo> Carregar(Promocao entidade);
				
        IEnumerable<ProdutoTipo> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<ProdutoTipo> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
