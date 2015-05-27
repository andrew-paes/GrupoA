
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
    public partial interface IContatoSetorDAL
    {	
        void Inserir(ContatoSetor entidade);
        void Atualizar(ContatoSetor entidade);
        void Excluir(ContatoSetor entidade);
        ContatoSetor Carregar(ContatoSetor entidade);
		
		IEnumerable<ContatoSetor> Carregar(ContatoAssunto entidade);
				
        IEnumerable<ContatoSetor> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<ContatoSetor> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
