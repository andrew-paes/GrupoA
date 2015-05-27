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
    public partial interface IProdutoImagemDAL
    {
        void Inserir(ProdutoImagem entidade);
        void Atualizar(ProdutoImagem entidade);
        void Excluir(ProdutoImagem entidade);
        ProdutoImagem Carregar(ProdutoImagem entidade);
        IEnumerable<ProdutoImagem> Carregar(Arquivo entidade);
        IEnumerable<ProdutoImagem> Carregar(Produto entidade);
        IEnumerable<ProdutoImagem> Carregar(ProdutoImagemTipo entidade);
        IEnumerable<ProdutoImagem> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<ProdutoImagem> CarregarTodos();
        int TotalRegistros();
        int TotalRegistros(IFilterHelper filtro);
    }
}