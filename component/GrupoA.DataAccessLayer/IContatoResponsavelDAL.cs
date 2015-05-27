
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
    public partial interface IContatoResponsavelDAL
    {	
        void Inserir(ContatoResponsavel entidade);
        void Atualizar(ContatoResponsavel entidade);
        void Excluir(ContatoResponsavel entidade);
        ContatoResponsavel Carregar(ContatoResponsavel entidade);
		
		IEnumerable<ContatoResponsavel> Carregar(ContatoAssunto entidade);
				
        IEnumerable<ContatoResponsavel> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<ContatoResponsavel> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
