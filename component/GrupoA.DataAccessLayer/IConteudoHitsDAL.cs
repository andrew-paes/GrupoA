
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
    public partial interface IConteudoHitsDAL
    {	
        void Inserir(ConteudoHits entidade);
        void Atualizar(ConteudoHits entidade);
        void Excluir(ConteudoHits entidade);
        ConteudoHits Carregar(ConteudoHits entidade);
				
		ConteudoHits CarregarComDependencias(ConteudoHits entidade);	
				
        IEnumerable<ConteudoHits> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<ConteudoHits> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
