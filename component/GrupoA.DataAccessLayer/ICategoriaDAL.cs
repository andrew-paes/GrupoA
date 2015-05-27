
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
    public partial interface ICategoriaDAL
    {
        void Inserir(Categoria entidade);
        void Atualizar(Categoria entidade);
        void Excluir(Categoria entidade);
        Categoria Carregar(Categoria entidade);
        IEnumerable<Categoria> Carregar(Conteudo entidade);
        IEnumerable<Categoria> Carregar(CursoPanamericano entidade);
        IEnumerable<Categoria> Carregar(Produto entidade);
        IEnumerable<Categoria> Carregar(Promocao entidade);
        IEnumerable<Categoria> Carregar(Revista entidade);
        IEnumerable<Categoria> Carregar(Usuario entidade);
        IEnumerable<Categoria> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<Categoria> CarregarTodos();
        int TotalRegistros();
        int TotalRegistros(IFilterHelper filtro);
    }
}