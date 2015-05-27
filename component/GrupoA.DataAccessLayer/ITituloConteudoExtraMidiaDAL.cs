
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
    public partial interface ITituloConteudoExtraMidiaDAL
    {	
        void Inserir(TituloConteudoExtraMidia entidade);
        void Atualizar(TituloConteudoExtraMidia entidade);
        void Excluir(TituloConteudoExtraMidia entidade);
        TituloConteudoExtraMidia Carregar(TituloConteudoExtraMidia entidade);
				
		TituloConteudoExtraMidia CarregarComDependencias(TituloConteudoExtraMidia entidade);	
				
        IEnumerable<TituloConteudoExtraMidia> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<TituloConteudoExtraMidia> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
