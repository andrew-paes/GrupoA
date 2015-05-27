
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
using GrupoA.BusinessObject.ViewHelper;

namespace GrupoA.DataAccess
{
    public partial interface IProdutoDAL
    {
        List<Produto> CarregarTodosPorEANExcetoProdutos(Produto produto, List<Produto> produtos);
        List<Produto> CarregarTodosPorIsbn13ExcetoProdutos(string isbn13, List<Produto> produtos);
        List<Produto> CarregarPorPromocao(Promocao promocao);
        List<Produto> CarregarPorRevistaArtigo(RevistaArtigo revistaArtigo);
        List<ProdutoListaVH> CarregarPorCategoria(Categoria entidade);
        List<CarrinhoItemVH> CarregarPorCarrinho(Carrinho carrinho);
        ProdutoListaVH CarregarProdutoDetalhe(ProdutoListaVH entidade);
        List<CarrinhoItemVH> CarregarSimplificadoPorCarrinho(Carrinho carrinho, int totalItensCarrinho);
        List<Produto> CarregaFormatosDisponiveis(Titulo titulo);
        List<TituloVH> ConsultaTodasPublicacoesUsuario(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro, int usuarioId);
        int ContaPublicacaoUsuario(Int32 usuarioId);
        StringBuilder CarregarPorNome(Produto entidade, int qtdLinhas);
        List<TituloVH> ConsultaTodasBibliotecasUsuario(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro, int usuarioId);
        int ContaBibliotecaUsuario(Int32 usuarioId);
        List<TituloVH> ConsultaTodasFavoritosUsuario(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro, int usuarioId);
        int ContaFavoritosUsuario(Int32 usuarioId);
        void RelacionarProdutoSelo(Produto produtoBO, Selo seloBO);
        bool ProdutoCategoriaRelacionado(Produto produtoBO, Categoria categoriaBO);
        void RelacionarProdutoCategoria(Produto produtoBO, Categoria categoriaBO);
        void AtualizarNome(Produto produto);
        void AtualizarMenosNome(Produto entidade);
        void AtualizarValorOfertaParaNull(List<Produto> produtos);
        void AtualizarValorOferta(Produto produto);
        List<Produto> CarregarProdutosPorNome(Produto entidade, Oferta oferta);
        List<Produto> CarregarProdutosPorOferta(Oferta oferta);
        List<TituloVH> CarregarUltimosProdutosPublicadosPorAutor(int autorId);
        bool ProdutoArquivoRelacionado(Produto produtoBO, Arquivo arquivoBO);
        List<TituloVH> CarregarProdutosSugeridos(Int32 produtoId);
        List<TituloVH> CarregarProdutosSugeridos(List<Produto> produtoBOList, int quantidade);
        Produto CarregarDetalheLivro(Produto entidade);
        void ExcluirProdutoCategoria(Produto entidade);
        void ExcluirProdutoSelo(Produto entidade);
        List<Produto> CarregarProdutosPorNome(Produto entidade);
        List<Produto> Carregar(RevistaPacote entidade);
    }
}