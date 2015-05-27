using System;
using System.Collections.Generic;
using GrupoA.BusinessObject;

namespace GrupoA.DataAccess
{
    public partial interface IRevistaEdicaoDAL
    {
        IEnumerable<RevistaEdicao> CarregarUltimasEdicoesPorAreaInteresse(List<Categoria> categoriasDasAreasDeInteresse, Int32 quantidadeRegistros);
        IEnumerable<RevistaEdicao> CarregarTodasEdicoesPorRevistaId(int revistaId);
        RevistaEdicao CarregarComDependencia(RevistaEdicao entidade);
        RevistaEdicao CarregarPorRevistaNumEdicao(RevistaEdicao entidade);
        RevistaEdicao CarregarMaiorEdicaoPorRevistaId(Int32 revistaId);
        List<RevistaEdicao> CarregarTodasEdicoes(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, Int32 revistaId);
        Int32 ContarEdicoesDiferentesDaEdicaoEntrada(Int32 revistaId);
        RevistaEdicao CarregarEdicaoComProduto(Int32 revistaEdicaoId);
        Int32 CarregarRevistaIdUltimaEdicaoCadastrada();
    }
}
