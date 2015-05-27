
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
    public partial interface IEventoAlertaDAL
    {	
        void Inserir(EventoAlerta entidade);
        void Atualizar(EventoAlerta entidade);
        void Excluir(EventoAlerta entidade);
        EventoAlerta Carregar(EventoAlerta entidade);
		
		IEnumerable<EventoAlerta> Carregar(Evento entidade);
		
		IEnumerable<EventoAlerta> Carregar(Usuario entidade);
				
        IEnumerable<EventoAlerta> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<EventoAlerta> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
