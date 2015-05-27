using System;
using System.Collections.Generic;
using GrupoA.BusinessObject;

namespace GrupoA.DataAccess
{
    public partial interface IRevistaAssinaturaDAL
    {
        RevistaAssinatura CarregarPorRevistaNumExemplares(RevistaAssinatura entidade);
        List<RevistaAssinatura> CarregarTodosAssinaturasPorRevistaId(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, Int32 revistaId);
        Int32 ContarTodasAssinaturasPorRevistaId(Int32 revistaId);
    }
}