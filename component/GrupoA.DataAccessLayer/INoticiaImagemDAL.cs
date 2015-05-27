
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
    public partial interface INoticiaImagemDAL
    {	
        void Inserir(NoticiaImagem entidade);
        void Atualizar(NoticiaImagem entidade);
        void Excluir(NoticiaImagem entidade);
        NoticiaImagem Carregar(NoticiaImagem entidade);
		
		IEnumerable<NoticiaImagem> Carregar(Arquivo entidade);
		
		IEnumerable<NoticiaImagem> Carregar(Noticia entidade);
				
        IEnumerable<NoticiaImagem> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<NoticiaImagem> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
