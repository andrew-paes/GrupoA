
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
    public partial interface ICategoriaNoticiaDAL
    {	
        void Inserir(CategoriaNoticia entidade);
        void Atualizar(CategoriaNoticia entidade);
        void Excluir(CategoriaNoticia entidade);
        CategoriaNoticia Carregar(CategoriaNoticia entidade);
		
		IEnumerable<CategoriaNoticia> Carregar(Noticia entidade);
				
        IEnumerable<CategoriaNoticia> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<CategoriaNoticia> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
