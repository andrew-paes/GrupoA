
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
    public partial interface IOfertaDAL
    {	
        void Inserir(Oferta entidade);
        void Atualizar(Oferta entidade);
        void Excluir(Oferta entidade);
        Oferta Carregar(Oferta entidade);
		
		IEnumerable<Oferta> Carregar(OfertaCategoria entidade);
		
		IEnumerable<Oferta> Carregar(OfertaProduto entidade);
		
		IEnumerable<Oferta> Carregar(OfertaTipo entidade);
				
        IEnumerable<Oferta> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<Oferta> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
