
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
    public partial interface IPedidoSituacaoDAL
    {	
        void Inserir(PedidoSituacao entidade);
        void Atualizar(PedidoSituacao entidade);
        void Excluir(PedidoSituacao entidade);
        PedidoSituacao Carregar(PedidoSituacao entidade);
		
		IEnumerable<PedidoSituacao> Carregar(Pedido entidade);
		
		IEnumerable<PedidoSituacao> Carregar(PedidoStatus entidade);
				
        IEnumerable<PedidoSituacao> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<PedidoSituacao> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
