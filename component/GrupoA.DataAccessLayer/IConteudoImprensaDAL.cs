
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
    public partial interface IConteudoImprensaDAL
    {	
        void Inserir(ConteudoImprensa entidade);
        void Atualizar(ConteudoImprensa entidade);
        void Excluir(ConteudoImprensa entidade);
        ConteudoImprensa Carregar(ConteudoImprensa entidade);
				
		ConteudoImprensa CarregarComDependencias(ConteudoImprensa entidade);	
				
        IEnumerable<ConteudoImprensa> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<ConteudoImprensa> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
