
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
    public partial interface IMeioPagamentoDAL
    {	
        void Inserir(MeioPagamento entidade);
        void Atualizar(MeioPagamento entidade);
        void Excluir(MeioPagamento entidade);
        MeioPagamento Carregar(MeioPagamento entidade);
		
		IEnumerable<MeioPagamento> Carregar(MeioPagamentoFaixa entidade);
		
		IEnumerable<MeioPagamento> Carregar(Pagamento entidade);
				
        IEnumerable<MeioPagamento> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<MeioPagamento> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
