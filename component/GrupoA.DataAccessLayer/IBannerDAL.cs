
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
    public partial interface IBannerDAL
    {	
        void Inserir(Banner entidade);
        void Atualizar(Banner entidade);
        void Excluir(Banner entidade);
        Banner Carregar(Banner entidade);
		
		IEnumerable<Banner> Carregar(BannerArea entidade);
		
		IEnumerable<Banner> Carregar(Arquivo entidade);
				
        IEnumerable<Banner> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<Banner> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
