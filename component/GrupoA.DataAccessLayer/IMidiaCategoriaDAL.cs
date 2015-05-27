
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
    public partial interface IMidiaCategoriaDAL
    {	
        void Inserir(MidiaCategoria entidade);
        void Atualizar(MidiaCategoria entidade);
        void Excluir(MidiaCategoria entidade);
        MidiaCategoria Carregar(MidiaCategoria entidade);
		
		IEnumerable<MidiaCategoria> Carregar(Categoria entidade);
		
		IEnumerable<MidiaCategoria> Carregar(Midia entidade);
				
        IEnumerable<MidiaCategoria> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<MidiaCategoria> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
