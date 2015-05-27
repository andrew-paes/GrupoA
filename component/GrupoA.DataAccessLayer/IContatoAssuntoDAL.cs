
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
    public partial interface IContatoAssuntoDAL
    {	
        void Inserir(ContatoAssunto entidade);
        void Atualizar(ContatoAssunto entidade);
        void Excluir(ContatoAssunto entidade);
        ContatoAssunto Carregar(ContatoAssunto entidade);
		
		IEnumerable<ContatoAssunto> Carregar(ContatoResponsavel entidade);
		
		IEnumerable<ContatoAssunto> Carregar(ContatoSetor entidade);
				
        IEnumerable<ContatoAssunto> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<ContatoAssunto> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
