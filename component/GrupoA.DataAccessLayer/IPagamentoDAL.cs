
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
    public partial interface IPagamentoDAL
    {	
        void Inserir(Pagamento entidade);
        void Atualizar(Pagamento entidade);
        void Excluir(Pagamento entidade);
        Pagamento Carregar(Pagamento entidade);
		
		IEnumerable<Pagamento> Carregar(MeioPagamento entidade);
		
		Pagamento Carregar(Pedido entidade);
				
        IEnumerable<Pagamento> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<Pagamento> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
