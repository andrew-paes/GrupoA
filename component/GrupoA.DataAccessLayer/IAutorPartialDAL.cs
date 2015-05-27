using System;
using System.Text;
using System.Collections.Generic;
using GrupoA.BusinessObject;
using GrupoA.FilterHelper;

namespace GrupoA.DataAccess
{
    public partial interface IAutorDAL
    {
        List<Autor> CarregarAutores(Titulo entidade);
        List<Autor> CarregarAutores(Titulo entidade, Int32 principal);
        List<Autor> CarregarAutores(Titulo entidade, Autor autor);
        List<Autor> CarregarAutores(Produto produto);
        Autor CarregarAutorCodigoLegado(Autor entidade);
        Autor CarregarAutorComDependencia(Autor autor);
        List<Autor> CarregarAutoresPorNome(Autor entidade, Titulo titulo);
        Autor CarregarAutorPorNome(Autor entidade);
    }
}
