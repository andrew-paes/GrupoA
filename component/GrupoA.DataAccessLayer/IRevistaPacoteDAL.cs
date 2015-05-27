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
    public partial interface IRevistaPacoteDAL
    {
        void Inserir(RevistaPacote entidade);
        void Atualizar(RevistaPacote entidade);
        void Excluir(RevistaPacote entidade);
        RevistaPacote Carregar(RevistaPacote entidade);
        IEnumerable<RevistaPacote> Carregar(Produto entidade);
        IEnumerable<RevistaPacote> Carregar(RevistaPacoteBrindeRegra entidade);
        IEnumerable<RevistaPacote> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<RevistaPacote> CarregarTodos();
        int TotalRegistros();
        int TotalRegistros(IFilterHelper filtro);
    }
}