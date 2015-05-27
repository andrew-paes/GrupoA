using System;
using System.Collections.Generic;
using GrupoA.BusinessObject;

namespace GrupoA.DataAccess
{
    public partial interface IUsuarioDAL
    {
        List<Usuario> CarregarTodosPorCadastroPessoaUsuariosExcetoUsuarios(Usuario usuario, List<Usuario> usuarios);
        List<Usuario> CarregarUsuariosPorPromocao(Promocao promocao);
        int CarregarIdUsuarioByAvaliacao(int tituloAvaliacaoId);
        void ExcluiAreaInteressePorUsuarioId(int usuarioId);
        void InsereUsuarioInteresse(int usuarioId, int categoriaId);
        int ValidaUsuarioPorEmailCadastroPessoa(string emailUsuario, string cadastroPessoa, int usuarioId);
        List<Categoria> CarregarUsuarioInteresse(int usuarioId);
        void InsereUsuarioPerfil(int usuarioId, int perfilId);
        void ExcluiUsuarioPerfil(int usuarioId);
        Usuario LoginUsuario(Usuario usuario, String chave);
        List<Usuario> CarregarUsuariosComSincronizacaoPendente();
        bool ValidarCadastroPessoaUnico(Usuario usuario);
        bool ValidarEmailUnico(Usuario usuario);
        Usuario CarregarUsuarioEsqueciMinhaSenha(Usuario usuario, String chave);
        List<Usuario> CarregarUsuarioPromocaoAniversariantes();
        Usuario CarregarPorCadastroPessoa(Usuario entidade);
        void AtualizarStatus(Usuario entidade);
        Usuario CarregarPorCadastroPessoaEmail(Usuario entidade);
        List<Categoria> CarregarUsuarioAreaInteresse(Usuario entidade);
        List<Usuario> CarregarUsuarioParaAssinatura(String cadastroPessoa, String nomeUsuario);
        List<Usuario> Carregar(CompraConjunta entidade);
    }
}