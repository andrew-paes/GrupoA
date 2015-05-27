using System;
using System.Text;
using System.Collections.Generic;
using GrupoA.BusinessObject;
using GrupoA.FilterHelper;

namespace GrupoA.DataAccess
{
    public partial interface IBlogDAL
    {
        List<Blog> CarregarBlogsPorCategoria(Categoria categoria, Int32 qtdRegistros);
	}
}
