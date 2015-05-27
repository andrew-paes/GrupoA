using System;
using System.Collections.Generic;
using GrupoA.BusinessObject;

namespace GrupoA.DataAccess
{
    public partial interface IRevistaDAL
    {
        Revista CarregarRevistaPorIssn(Revista entidade);
        List<Revista> CarregarRevistasPatio(Int32 qtdRegistros);
        List<Revista> CarregarTodasExcetoRevistas(List<Revista> revistas);
        List<Revista> CarregarTodosPorPromocao(Promocao promocao);
    }
}