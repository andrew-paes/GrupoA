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
    public partial interface ITituloConteudoExtraArquivoDAL
    {
        void Inserir(TituloConteudoExtraArquivo entidade);
        void Atualizar(TituloConteudoExtraArquivo entidade);
        void Excluir(TituloConteudoExtraArquivo entidade);
        TituloConteudoExtraArquivo Carregar(TituloConteudoExtraArquivo entidade);
        IEnumerable<TituloConteudoExtraArquivo> Carregar(Arquivo entidade);
        IEnumerable<TituloConteudoExtraArquivo> Carregar(Titulo entidade);
        IEnumerable<TituloConteudoExtraArquivo> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<TituloConteudoExtraArquivo> CarregarTodos();
        int TotalRegistros();
        int TotalRegistros(IFilterHelper filtro);
    }
}