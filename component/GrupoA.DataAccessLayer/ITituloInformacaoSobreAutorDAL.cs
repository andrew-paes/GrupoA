
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
    public partial interface ITituloInformacaoSobreAutorDAL
    {
        void Inserir(TituloInformacaoSobreAutor entidade);
        void Atualizar(TituloInformacaoSobreAutor entidade);
        void Excluir(TituloInformacaoSobreAutor entidade);
        TituloInformacaoSobreAutor Carregar(TituloInformacaoSobreAutor entidade);
        TituloInformacaoSobreAutor CarregarComDependencias(TituloInformacaoSobreAutor entidade);
        IEnumerable<TituloInformacaoSobreAutor> Carregar(Arquivo entidade);
        IEnumerable<TituloInformacaoSobreAutor> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<TituloInformacaoSobreAutor> CarregarTodos();
        int TotalRegistros();
        int TotalRegistros(IFilterHelper filtro);
    }
}