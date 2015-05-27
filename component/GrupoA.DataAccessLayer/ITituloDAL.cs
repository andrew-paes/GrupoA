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
    public partial interface ITituloDAL
    {
        void Inserir(Titulo entidade);
        void Atualizar(Titulo entidade);
        void Excluir(Titulo entidade);
        Titulo Carregar(Titulo entidade);
        Titulo CarregarComDependencias(Titulo entidade);
        IEnumerable<Titulo> Carregar(Capitulo entidade);
        IEnumerable<Titulo> Carregar(DestaqueTituloImpresso entidade);
        IEnumerable<Titulo> Carregar(TituloConteudoExtraArquivo entidade);
        IEnumerable<Titulo> Carregar(TituloEletronico entidade);
        IEnumerable<Titulo> Carregar(TituloImagemResumo entidade);
        IEnumerable<Titulo> Carregar(TituloSolicitacao entidade);
        Titulo Carregar(TituloImpresso entidade);
        IEnumerable<Titulo> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<Titulo> CarregarTodos();
        int TotalRegistros();
        int TotalRegistros(IFilterHelper filtro);
    }
}