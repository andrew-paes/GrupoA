using System;
using System.Collections.Generic;
using GrupoA.BusinessObject;

namespace GrupoA.DataAccess
{
    public partial interface IRevistaSecaoDAL
    {
        IEnumerable<RevistaSecao> CarregarTodasSecoesPorRevistaId(int revistaId);
        List<RevistaSecao> CarregarTodasSecoesPorRevistaIdEdicaoId(Int32 revistaId, Int32 revistaEdicaoId);
	}
}
