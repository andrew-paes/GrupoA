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
    public partial interface ITituloImpressoDAL
    {
        void Inserir(TituloImpresso entidade);
        void Atualizar(TituloImpresso entidade);
        void Excluir(TituloImpresso entidade);
        TituloImpresso Carregar(TituloImpresso entidade);
        TituloImpresso CarregarComDependencias(TituloImpresso entidade);
        IEnumerable<TituloImpresso> Carregar(CapituloImpresso entidade);
        TituloImpresso Carregar(Titulo entidade);
        IEnumerable<TituloImpresso> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<TituloImpresso> CarregarTodos();
        int TotalRegistros();
        int TotalRegistros(IFilterHelper filtro);
    }
}