
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
    public partial interface ITituloSolicitacaoDAL
    {	
        void Inserir(TituloSolicitacao entidade);
        void Atualizar(TituloSolicitacao entidade);
        void Excluir(TituloSolicitacao entidade);
        TituloSolicitacao Carregar(TituloSolicitacao entidade);
		
		IEnumerable<TituloSolicitacao> Carregar(TituloAvaliacao entidade);
		
		IEnumerable<TituloSolicitacao> Carregar(Professor entidade);
		
		IEnumerable<TituloSolicitacao> Carregar(Titulo entidade);
		
		IEnumerable<TituloSolicitacao> Carregar(TituloSolicitacaoStatus entidade);
				
        IEnumerable<TituloSolicitacao> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<TituloSolicitacao> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
