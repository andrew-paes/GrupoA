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
    public partial interface IPedidoControleDAL
    {
        void Inserir(PedidoControle entidade);
        void Atualizar(PedidoControle entidade);
        void Excluir(PedidoControle entidade);
        PedidoControle Carregar(PedidoControle entidade);
        PedidoControle CarregarComDependencias(PedidoControle entidade);
        IEnumerable<PedidoControle> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<PedidoControle> CarregarTodos();
        int TotalRegistros();
        int TotalRegistros(IFilterHelper filtro);
    }
}