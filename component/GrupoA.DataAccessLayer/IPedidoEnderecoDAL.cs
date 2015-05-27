
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
    public partial interface IPedidoEnderecoDAL
    {	
        void Inserir(PedidoEndereco entidade);
        void Atualizar(PedidoEndereco entidade);
        void Excluir(PedidoEndereco entidade);
        PedidoEndereco Carregar(PedidoEndereco entidade);
		
		IEnumerable<PedidoEndereco> Carregar(EnderecoTipo entidade);
		
		IEnumerable<PedidoEndereco> Carregar(Municipio entidade);
		
		PedidoEndereco Carregar(Pedido entidade);
				
        IEnumerable<PedidoEndereco> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<PedidoEndereco> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
