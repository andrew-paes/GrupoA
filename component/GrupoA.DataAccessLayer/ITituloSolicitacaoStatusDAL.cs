
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
    public partial interface ITituloSolicitacaoStatusDAL
    {	
        void Inserir(TituloSolicitacaoStatus entidade);
        void Atualizar(TituloSolicitacaoStatus entidade);
        void Excluir(TituloSolicitacaoStatus entidade);
        TituloSolicitacaoStatus Carregar(TituloSolicitacaoStatus entidade);
		
		IEnumerable<TituloSolicitacaoStatus> Carregar(TituloSolicitacao entidade);
				
        IEnumerable<TituloSolicitacaoStatus> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<TituloSolicitacaoStatus> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
