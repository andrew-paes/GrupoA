
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
    public partial interface IEventoDAL
    {	
        void Inserir(Evento entidade);
        void Atualizar(Evento entidade);
        void Excluir(Evento entidade);
        Evento Carregar(Evento entidade);
				
		Evento CarregarComDependencias(Evento entidade);	
		
		IEnumerable<Evento> Carregar(EventoAlerta entidade);
		
		IEnumerable<Evento> Carregar(EventoImagem entidade);
		
		IEnumerable<Evento> Carregar(Arquivo entidade);
				
        IEnumerable<Evento> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<Evento> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
