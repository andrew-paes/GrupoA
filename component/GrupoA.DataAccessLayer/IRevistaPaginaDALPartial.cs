using System.Collections.Generic;
using GrupoA.BusinessObject;

namespace GrupoA.DataAccess
{
    public partial interface IRevistaPaginaDAL
    {
        List<RevistaPagina> CarregarRevistaPaginasMenu(Revista revista);
        RevistaPagina CarregarRevistaPaginaPorNome(RevistaPagina revistaPagina);
	}
}
