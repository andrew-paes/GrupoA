
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
    public partial interface IEnderecoDAL
    {
        void Inserir(Endereco entidade);
        void Atualizar(Endereco entidade);
        void Excluir(Endereco entidade);
        Endereco Carregar(Endereco entidade);

        IEnumerable<Endereco> Carregar(EnderecoTipo entidade);

        IEnumerable<Endereco> Carregar(Municipio entidade);

        IEnumerable<Endereco> Carregar(Usuario entidade);

        IEnumerable<Endereco> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<Endereco> CarregarTodos();
        int TotalRegistros();
        int TotalRegistros(IFilterHelper filtro);
    }
}