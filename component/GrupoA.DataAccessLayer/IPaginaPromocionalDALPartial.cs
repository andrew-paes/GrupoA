using System.Collections.Generic;
using GrupoA.BusinessObject;
using GrupoA.FilterHelper;

namespace GrupoA.DataAccess
{
    public partial interface IPaginaPromocionalDAL
    {
        PaginaPromocional CarregarPorNomePagina(PaginaPromocional entidade);
    }
}
