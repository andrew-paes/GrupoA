
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
    public partial interface IRevistaArtigoDAL
    {	
        void Inserir(RevistaArtigo entidade);
        void Atualizar(RevistaArtigo entidade);
        void Excluir(RevistaArtigo entidade);
        RevistaArtigo Carregar(RevistaArtigo entidade);
				
		RevistaArtigo CarregarComDependencias(RevistaArtigo entidade);	
		
		IEnumerable<RevistaArtigo> Carregar(RevistaArtigoControversia entidade);
		
		IEnumerable<RevistaArtigo> Carregar(Arquivo entidade);
		
		IEnumerable<RevistaArtigo> Carregar(Produto entidade);
		
		IEnumerable<RevistaArtigo> Carregar(RevistaArtigoPermissao entidade);
		
		IEnumerable<RevistaArtigo> Carregar(RevistaEdicao entidade);
		
		IEnumerable<RevistaArtigo> Carregar(RevistaSecao entidade);
				
        IEnumerable<RevistaArtigo> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<RevistaArtigo> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
