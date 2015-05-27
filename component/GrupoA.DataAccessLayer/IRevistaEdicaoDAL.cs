
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
    public partial interface IRevistaEdicaoDAL
    {	
        void Inserir(RevistaEdicao entidade);
        void Atualizar(RevistaEdicao entidade);
        void Excluir(RevistaEdicao entidade);
        RevistaEdicao Carregar(RevistaEdicao entidade);
				
		RevistaEdicao CarregarComDependencias(RevistaEdicao entidade);	
		
		IEnumerable<RevistaEdicao> Carregar(RevistaArtigo entidade);
		
		IEnumerable<RevistaEdicao> Carregar(Revista entidade);
				
        IEnumerable<RevistaEdicao> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<RevistaEdicao> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
