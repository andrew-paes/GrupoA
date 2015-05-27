
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
    public partial interface INotificacaoDisponibilidadeDAL
    {	
        void Inserir(NotificacaoDisponibilidade entidade);
        void Atualizar(NotificacaoDisponibilidade entidade);
        void Excluir(NotificacaoDisponibilidade entidade);
        NotificacaoDisponibilidade Carregar(NotificacaoDisponibilidade entidade);
		
		IEnumerable<NotificacaoDisponibilidade> Carregar(NotificacaoStatus entidade);
		
		IEnumerable<NotificacaoDisponibilidade> Carregar(Produto entidade);
		
		IEnumerable<NotificacaoDisponibilidade> Carregar(Usuario entidade);
				
        IEnumerable<NotificacaoDisponibilidade> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<NotificacaoDisponibilidade> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
