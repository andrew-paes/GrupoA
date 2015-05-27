
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
    public partial interface INotificacaoStatusDAL
    {	
        void Inserir(NotificacaoStatus entidade);
        void Atualizar(NotificacaoStatus entidade);
        void Excluir(NotificacaoStatus entidade);
        NotificacaoStatus Carregar(NotificacaoStatus entidade);
		
		IEnumerable<NotificacaoStatus> Carregar(NotificacaoDisponibilidade entidade);
				
        IEnumerable<NotificacaoStatus> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<NotificacaoStatus> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
