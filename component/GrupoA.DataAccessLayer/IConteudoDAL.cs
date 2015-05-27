
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
    public partial interface IConteudoDAL
    {	
        void Inserir(Conteudo entidade);
        void Atualizar(Conteudo entidade);
        void Excluir(Conteudo entidade);
        Conteudo Carregar(Conteudo entidade);
		
		IEnumerable<Conteudo> Carregar(Categoria entidade);
		
		IEnumerable<Conteudo> Carregar(Favorito entidade);
		
		IEnumerable<Conteudo> Carregar(ConteudoTipo entidade);
				
        IEnumerable<Conteudo> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<Conteudo> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
