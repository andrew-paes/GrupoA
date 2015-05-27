
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
    public partial interface IFaqCategoriaDAL
    {	
        void Inserir(FaqCategoria entidade);
        void Atualizar(FaqCategoria entidade);
        void Excluir(FaqCategoria entidade);
        FaqCategoria Carregar(FaqCategoria entidade);
		
		IEnumerable<FaqCategoria> Carregar(FaqItem entidade);
				
        IEnumerable<FaqCategoria> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<FaqCategoria> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
