
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
    public partial interface IMeioPagamentoFaixaDAL
    {	
        void Inserir(MeioPagamentoFaixa entidade);
        void Atualizar(MeioPagamentoFaixa entidade);
        void Excluir(MeioPagamentoFaixa entidade);
        MeioPagamentoFaixa Carregar(MeioPagamentoFaixa entidade);
		
		IEnumerable<MeioPagamentoFaixa> Carregar(MeioPagamento entidade);
				
        IEnumerable<MeioPagamentoFaixa> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<MeioPagamentoFaixa> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
