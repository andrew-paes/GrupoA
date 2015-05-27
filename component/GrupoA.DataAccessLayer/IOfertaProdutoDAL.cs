
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
    public partial interface IOfertaProdutoDAL
    {	
        void Inserir(OfertaProduto entidade);
        void Atualizar(OfertaProduto entidade);
        void Excluir(OfertaProduto entidade);
        OfertaProduto Carregar(OfertaProduto entidade);
		
		IEnumerable<OfertaProduto> Carregar(Oferta entidade);
		
		IEnumerable<OfertaProduto> Carregar(Produto entidade);
				
        IEnumerable<OfertaProduto> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<OfertaProduto> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
