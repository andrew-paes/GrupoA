using System;
using System.Collections.Generic;
using System.Text;
using GrupoA.BusinessObject;
using GrupoA.BusinessObject.ViewHelper;

namespace GrupoA.DataAccess
{
    public partial interface ITituloDAL
    {
        IEnumerable<Titulo> CarregarItensSalaDeAula(Usuario usuario, Int32 quantidadeRegistros);
        Titulo CarregarComInformacoesComplementares(Titulo entidade);
        Titulo CarregaConteudoExtraMidiaURL(Titulo entidade);
        Titulo CarregaTituloComComentarioDoEspecialista(Titulo entidade);
        List<EstanteTituloVH> CarregarOfertasPorCategoria(Categoria categoria, int numeroItensSeremExibidos);
        List<EstanteTituloVH> CarregarEBooksPorCategoria(Categoria categoria, int numeroItensSeremExibidos);
        List<EstanteTituloVH> CarregarMaisVendidosPorCategoria(Categoria categoria, int numeroItensSeremExibidos);
        List<EstanteTituloVH> CarregarLancamentosPorCategoria(Categoria categoria, int numeroItensSeremExibidos);
        List<EstanteTituloVH> CarregarCompraConjuntaPorCategoria(Categoria categoria, int numeroItensSeremExibidos);
        List<EstanteTituloVH> CarregarRecomendadosPorCategoria(Categoria categoria, Int32 qtdRegistros);
        List<EstanteTituloVH> CarregarRecomendadosPorArea(int areaDeConhecimentoId);
        List<EstanteTituloVH> CarregarTitulosRelacionadosPorArea(int produtoId, int areaDeConhecimentoId, int numeroMaximoDeRegistros);
        List<EstanteTituloVH> CarregarMaisVistos(Categoria categoria, Int32 qtdRegistros);
        List<EstanteTituloVH> CarregarMaisVistosPorArea(int areaDeConhecimentoId);
        List<EstanteTituloVH> CarregaTitulosParaEstantePorCategoria(List<Categoria> categorias, int numeroItensSeremExibidos, bool somenteEbook);
        List<EstanteTituloVH> CarregaTitulosParaEstantePorCategoria(List<Categoria> categorias, bool somenteEbook);
        List<TituloInformacaoComentarioEspecialista> CarregarTitulosPorAreaDeConhecimento(int areaDeConhecimentoId, int numeroMaximoRegistros);
        List<TituloVH> CarregarTituloComMaterialExtraArquivo(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, Usuario usuario, Categoria categoria);
        int ContarTituloComMaterialExtraArquivo(Usuario usuario, Categoria categoria);
        MenuBuscaVH CarregarMenuBusca(String palavra, Int32 categoriaId, String tipoId, Int32 seloId);
        AbasEstanteVH CarregaAbasEstante(Int32 categoriaId);
        StringBuilder CarregarAutoCompleteBusca(String palavra);
        List<EstanteTituloVH> CarregaTitulosHistoricoComprasParaEstante(Usuario usuarioBO, Categoria categoriaBO);
        void DesmarcarMaisVendidos();
        void AtualizarMaisVendidos(List<Titulo> titulosMaisVendidos);
        void AtualizarNomeSubTitulo(Titulo titulo);
        void AtualizarMenosNomeSubtitulo(Titulo entidade);
        List<EstanteTituloVH> CarregarUltimosLancamentosPorCategoria(List<EstanteTituloVH> titulosParaEstante, Categoria categoria, Int32 numeroMaximoDeRegistros);
        List<EstanteTituloVH> CarregarLivrosQueDeixaramDeSerLancamentoPorCategoria(List<EstanteTituloVH> titulosParaEstante, Categoria categoria, Int32 numeroMaximoDeRegistros);
        Int32 ContarTituloPorAutor(Autor autor);
        List<Titulo> CarregarTitulosPorAutor(Autor autor);
        List<EstanteTituloVH> CarregarRecomendadosPorCategoria(Usuario usuario, Int32 qtdRegistros);
        List<EstanteTituloVH> CarregarMaisVistos(Usuario usuario, Int32 qtdRegistros);
        List<TituloVH> CarregarBuscaTituloOrdenadaPaginada(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, String palavra, String palavraExata, Int32 categoriaId, String tipoId, Int32 seloId);
        MenuBuscaVH CarregarMenuBusca(String palavra, String palavraExata, Int32 categoriaId, String tipoId, Int32 seloId, out int totalItem);
        List<TituloVH> CarregarTitulosPorPromocaoRevista(Int32 registrosPagina, Int32 numeroPagina, String ordem, Revista revista);
        Int32 ContarTitulosPorPromocaoRevista(Revista revista);
        List<TituloVH> CarregarTodosTitulosPorPromocaoRevista(Int32 registrosPagina, Int32 numeroPagina, String ordem);
        Int32 ContarTodosTitulosPorPromocaoRevista();
        List<EstanteTituloVH> CarregarTitulosRelacionadosRevistaArtigo(RevistaArtigo revistaArtigo);
        Int32 CarregarCategoriaPorTituloId(Int32 tituloId);
    }
}