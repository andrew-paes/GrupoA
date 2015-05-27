
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
    public partial interface INoticiaDAL
    {	
        void Inserir(Noticia entidade);
        void Atualizar(Noticia entidade);
        void Excluir(Noticia entidade);
        Noticia Carregar(Noticia entidade);
				
		Noticia CarregarComDependencias(Noticia entidade);	
		
		IEnumerable<Noticia> Carregar(NoticiaImagem entidade);
		
		IEnumerable<Noticia> Carregar(Arquivo entidade);
		
		IEnumerable<Noticia> Carregar(CategoriaNoticia entidade);
				
        IEnumerable<Noticia> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<Noticia> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
