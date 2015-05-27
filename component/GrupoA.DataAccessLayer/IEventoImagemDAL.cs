
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
    public partial interface IEventoImagemDAL
    {	
        void Inserir(EventoImagem entidade);
        void Atualizar(EventoImagem entidade);
        void Excluir(EventoImagem entidade);
        EventoImagem Carregar(EventoImagem entidade);
		
		IEnumerable<EventoImagem> Carregar(Arquivo entidade);
		
		IEnumerable<EventoImagem> Carregar(Evento entidade);
				
        IEnumerable<EventoImagem> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<EventoImagem> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
