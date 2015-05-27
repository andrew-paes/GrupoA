
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
    public partial interface IPedidoItemPromocaoDAL
    {	
        void Inserir(PedidoItemPromocao entidade);
        void Atualizar(PedidoItemPromocao entidade);
        void Excluir(PedidoItemPromocao entidade);
        PedidoItemPromocao Carregar(PedidoItemPromocao entidade);
				
		PedidoItemPromocao CarregarComDependencias(PedidoItemPromocao entidade);	
		
		IEnumerable<PedidoItemPromocao> Carregar(Promocao entidade);
				
        IEnumerable<PedidoItemPromocao> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<PedidoItemPromocao> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
