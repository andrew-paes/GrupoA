
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
    public partial interface IOfertaCategoriaDAL
    {	
        void Inserir(OfertaCategoria entidade);
        void Atualizar(OfertaCategoria entidade);
        void Excluir(OfertaCategoria entidade);
        OfertaCategoria Carregar(OfertaCategoria entidade);
		
		IEnumerable<OfertaCategoria> Carregar(Categoria entidade);
		
		IEnumerable<OfertaCategoria> Carregar(Oferta entidade);
				
        IEnumerable<OfertaCategoria> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<OfertaCategoria> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
