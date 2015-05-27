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
    public partial interface IUsuarioDAL
    {
        void Inserir(Usuario entidade, String chave);
        void Atualizar(Usuario entidade, String chave);
        void Excluir(Usuario entidade);
        Usuario Carregar(Usuario entidade);
        IEnumerable<Usuario> Carregar(Carrinho entidade);
        IEnumerable<Usuario> Carregar(Endereco entidade);
        IEnumerable<Usuario> Carregar(Enquete entidade);
        IEnumerable<Usuario> Carregar(EventoAlerta entidade);
        IEnumerable<Usuario> Carregar(LogOcorrencia entidade);
        IEnumerable<Usuario> Carregar(NotificacaoDisponibilidade entidade);
        IEnumerable<Usuario> Carregar(Pedido entidade);
        IEnumerable<Usuario> Carregar(Promocao entidade);
        IEnumerable<Usuario> Carregar(Telefone entidade);
        IEnumerable<Usuario> Carregar(Categoria entidade);
        IEnumerable<Usuario> Carregar(Perfil entidade);
        IEnumerable<Usuario> Carregar(ProfissionalOcupacao entidade);
        IEnumerable<Usuario> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<Usuario> CarregarTodos();
        int TotalRegistros();
        int TotalRegistros(IFilterHelper filtro);
    }
}