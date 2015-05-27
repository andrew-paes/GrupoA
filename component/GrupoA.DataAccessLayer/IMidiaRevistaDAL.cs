
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
    public partial interface IMidiaRevistaDAL
    {	
        void Inserir(MidiaRevista entidade);
        void Atualizar(MidiaRevista entidade);
        void Excluir(MidiaRevista entidade);
        MidiaRevista Carregar(MidiaRevista entidade);
		
		IEnumerable<MidiaRevista> Carregar(Midia entidade);
		
		IEnumerable<MidiaRevista> Carregar(Revista entidade);
				
        IEnumerable<MidiaRevista> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<MidiaRevista> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
