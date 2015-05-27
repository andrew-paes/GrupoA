
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
    public partial interface IRevistaArtigoPermissaoDAL
    {	
        void Inserir(RevistaArtigoPermissao entidade);
        void Atualizar(RevistaArtigoPermissao entidade);
        void Excluir(RevistaArtigoPermissao entidade);
        RevistaArtigoPermissao Carregar(RevistaArtigoPermissao entidade);
		
		IEnumerable<RevistaArtigoPermissao> Carregar(RevistaArtigo entidade);
				
        IEnumerable<RevistaArtigoPermissao> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<RevistaArtigoPermissao> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
