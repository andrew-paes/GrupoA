
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
    public partial interface IRevistaSecaoDAL
    {	
        void Inserir(RevistaSecao entidade);
        void Atualizar(RevistaSecao entidade);
        void Excluir(RevistaSecao entidade);
        RevistaSecao Carregar(RevistaSecao entidade);
		
		IEnumerable<RevistaSecao> Carregar(RevistaArtigo entidade);
		
		IEnumerable<RevistaSecao> Carregar(Revista entidade);
				
        IEnumerable<RevistaSecao> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<RevistaSecao> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
