
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
    public partial interface IFaqItemDAL
    {	
        void Inserir(FaqItem entidade);
        void Atualizar(FaqItem entidade);
        void Excluir(FaqItem entidade);
        FaqItem Carregar(FaqItem entidade);
		
		IEnumerable<FaqItem> Carregar(FaqCategoria entidade);
				
        IEnumerable<FaqItem> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<FaqItem> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
