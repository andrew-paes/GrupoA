using System;
using System.Collections.Generic;
using System.Text;
using GrupoA.BusinessObject;

namespace GrupoA.DataAccess
{
    public partial interface IPromocaoDAL
    {
        void InserirPromocaoUsuario(Promocao promocao, Usuario usuario);
        void ExcluirPromocaoUsuario(Promocao promocao, Usuario usuario);
        void ExcluirUsuariosPorPromocao(Promocao promocao);

        void InserirPromocaoPerfil(Promocao promocao, Perfil perfil);
        void ExcluirPromocaoPerfil(Promocao promocao, Perfil perfil);
        void ExcluirPerfisPorPromocao(Promocao promocao);

        void InserirPromocaoRevista(Promocao promocao, Revista revista);
        void ExcluirPromocaoRevista(Promocao promocao, Revista revista);
        void ExcluirRevistasPorPromocao(Promocao promocao);

        void InserirPromocaoProduto(Promocao promocao, Produto produto);
        void ExcluirPromocaoProduto(Promocao promocao, Produto produto);
        void ExcluirProdutosPorPromocao(Promocao promocao);

        void InserirPromocaoProdutoTipo(Promocao promocao, ProdutoTipo produtoTipo);
        void ExcluirPromocaoProdutoTipo(Promocao promocao, ProdutoTipo produtoTipo);
        void ExcluirProdutoTiposPorPromocao(Promocao promocao);

        void InserirPromocaoCategoria(Promocao promocao, Categoria categoria);
        void ExcluirCategoriasPorPromocao(Promocao promocao);

        void ExcluirCuponsPorPromocao(Promocao promocao);

        IEnumerable<Promocao> CarregarPromocoesAutomaticasValidas(Usuario usuario, Perfil perfil,
                                                                         List<Categoria> categorias,
                                                                         List<Produto> produtos,
                                                                         List<ProdutoTipo> tiposDeProdutos,
                                                                         String codigoPromocao,
                                                                         Boolean valida);
        Promocao Carregar(string codigoDaPromocao);
        Promocao CarregarPromocaoPorCupom(String codigoCupom);
        List<Usuario> CarregarPromocaoUsuarioPorPromocao(Int32 promocaoId);
        List<Categoria> CarregarPromocaoCategoriaPorPromocao(Int32 promocaoId);
        List<Perfil> CarregarPromocaoPerfilPorPromocao(Int32 promocaoId);
        List<Revista> CarregarPromocaoRevistaPorPromocao(Int32 promocaoId);
        List<Produto> CarregarPromocaoProdutoPorPromocao(Int32 promocaoId);
        List<ProdutoTipo> CarregarPromocaoProdutoTipoPorPromocao(Int32 promocaoId);
        StringBuilder CarregarCategoriasSelecionadasPorPromocao(Int32 promocaoId);
        Int32 ContarPromocaoRevistaSemProdutoPorRevista(Revista revista);
    }
}
