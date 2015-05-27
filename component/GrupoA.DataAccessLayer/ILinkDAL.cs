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
    public partial interface ILinkDAL
    {
        void Inserir(Link entidade);
        void Atualizar(Link entidade);
        void Excluir(Link entidade);
        Link Carregar(Link entidade);
        IEnumerable<Link> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<Link> CarregarTodos();
        int TotalRegistros();
        int TotalRegistros(IFilterHelper filtro);
    }
}