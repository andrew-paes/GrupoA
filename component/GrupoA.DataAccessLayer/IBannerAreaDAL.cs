
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
    public partial interface IBannerAreaDAL
    {	
        void Inserir(BannerArea entidade);
        void Atualizar(BannerArea entidade);
        void Excluir(BannerArea entidade);
        BannerArea Carregar(BannerArea entidade);
		
		IEnumerable<BannerArea> Carregar(Banner entidade);
				
        IEnumerable<BannerArea> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<BannerArea> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
