
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
    public partial interface ITituloConteudoExtraUrlDAL
    {	
        void Inserir(TituloConteudoExtraUrl entidade);
        void Atualizar(TituloConteudoExtraUrl entidade);
        void Excluir(TituloConteudoExtraUrl entidade);
        TituloConteudoExtraUrl Carregar(TituloConteudoExtraUrl entidade);
				
		TituloConteudoExtraUrl CarregarComDependencias(TituloConteudoExtraUrl entidade);	
				
        IEnumerable<TituloConteudoExtraUrl> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<TituloConteudoExtraUrl> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
