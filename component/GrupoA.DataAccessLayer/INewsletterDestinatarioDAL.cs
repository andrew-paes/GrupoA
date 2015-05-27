
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
    public partial interface INewsletterDestinatarioDAL
    {	
        void Inserir(NewsletterDestinatario entidade);
        void Atualizar(NewsletterDestinatario entidade);
        void Excluir(NewsletterDestinatario entidade);
        NewsletterDestinatario Carregar(NewsletterDestinatario entidade);
				
        IEnumerable<NewsletterDestinatario> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<NewsletterDestinatario> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
