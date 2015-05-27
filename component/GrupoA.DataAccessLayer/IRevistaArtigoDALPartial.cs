using System;
using System.Collections.Generic;
using GrupoA.BusinessObject;
using System.Text;

namespace GrupoA.DataAccess
{
    public partial interface IRevistaArtigoDAL
    {
        IEnumerable<RevistaArtigo> CarregarTodosArtigosPorRevistaEdicaoId(int revistaEdicaoId);
        void ExcluirRevistaArtigoImagem(int revistaArtigoId, int arquivoId, bool todosArquivos);
        void ExcluirRevistaArtigoArquivo(int arquivoId);
        void InserirRevistaArtigoGaleriaImagem(RevistaGaleriaArtigoImagem entidade);
        IEnumerable<RevistaGaleriaArtigoImagem> CarregarTodasRevistaGaleriaArtigoImagens(int revistaArtigoId);
        void ExcluirRelacionamentoRevistaArtigoIdAssociado(int revistaArtigoId);
        List<RevistaArtigo> CarregarArtigosPorRevistaEdicaoId(Int32 registrosPagina, Int32 numeroPagina, Int32 revistaEdicaoId, Int32 qtdRegistros, Boolean destaque, Int32? revistaSecaoId);
        RevistaArtigo CarregarCompleto(Int32 revistaArtigoId);
        List<RevistaArtigo> CarregarTodosArtigosSelecionados(Int32 registrosPagina, Int32 numeroPagina, String[] ordemColunas, String[] ordemSentidos, Int32 revistaId);
        Int32 ContarArtigosSelecionados(Int32 revistaId);
        RevistaArtigo CarregarArtigoCapaPorRevistaEdicaoId(Int32 revistaEdicaoId);
        Int32 ValidarArtigoPrincipalCapa(Int32 revistaEdicaoId, Int32 revistaArtigoId);
        List<RevistaArtigo> CarregarArtigosSemDestaquePorRevistaEdicaoId(Int32 revistaEdicaoId);
        List<RevistaArtigo> CarregarArtigosConteudoOnlinePorRevistaEdicaoId(Int32 registrosPagina, Int32 numeroPagina, String[] ordemColunas, String[] ordemSentidos, Int32 revistaEdicaoId, string revistaId);
        Int32 ContarArtigosConteudoOnlinePorRevistaEdicaoId(Int32 revistaEdicaoId, string revistaId);
        List<RevistaArtigo> CarregarArtigosPorRevistaEdicaoIdParaSite(Int32 revistaEdicaoId, Int32 qtdRegistros);
        int ContarArtigoBusca(String palavra, Revista entidade);
        List<RevistaArtigo> CarregarRevistaArtigoBusca(int registrosPagina, int numeroPagina, String[] ordenacao, String[] ordenacaoSentido, String palavra, Revista entidade);
        List<RevistaArtigo> CarregarArtigosAssociados(Int32 revistaArtigoId);
        Int32 ContarArtigosPorRevistaEdicaoId(Int32 revistaEdicaoId, Boolean destaque, Int32? revistaSecaoId);
        void ExcluirRevistaArtigoProduto(RevistaArtigo revistaArtigo, Produto produto);
        void IncluirRevistaArtigoProduto(RevistaArtigo revistaArtigo, Produto produto);
        StringBuilder CarregarAutoCompleteBusca(String palavra);
    }
}