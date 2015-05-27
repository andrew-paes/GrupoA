
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
    public partial interface IPedidoItemDAL
    {
        void Inserir(PedidoItem entidade);
        void Atualizar(PedidoItem entidade);
        void Excluir(PedidoItem entidade);
        PedidoItem Carregar(PedidoItem entidade);
        IEnumerable<PedidoItem> Carregar(Pedido entidade);
        IEnumerable<PedidoItem> Carregar(Produto entidade);
        IEnumerable<PedidoItem> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<PedidoItem> CarregarTodos();
        int TotalRegistros();
        int TotalRegistros(IFilterHelper filtro);
    }
}