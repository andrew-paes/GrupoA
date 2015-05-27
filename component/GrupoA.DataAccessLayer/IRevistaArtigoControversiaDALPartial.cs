using System;
using GrupoA.BusinessObject;

namespace GrupoA.DataAccess
{
    public partial interface IRevistaArtigoControversiaDAL
    {	
        RevistaArtigoControversia CarregarPorArtigoIdPosicionamento(RevistaArtigoControversia entidade);
        void ExcluirTodosPorRevistaArtigoId(Int32 revistaArtigoId);
	}
}
