
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
    public partial interface IPedidoFretePrecoDAL
    {	
        void Inserir(PedidoFretePreco entidade);
        void Atualizar(PedidoFretePreco entidade);
        void Excluir(PedidoFretePreco entidade);
        PedidoFretePreco Carregar(PedidoFretePreco entidade);
		
		IEnumerable<PedidoFretePreco> Carregar(PedidoFreteGrupo entidade);
				
        IEnumerable<PedidoFretePreco> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<PedidoFretePreco> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
