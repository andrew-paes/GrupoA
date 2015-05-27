using System.Collections.Generic;
using GrupoA.BusinessObject;

namespace GrupoA.DataAccess
{
    public partial interface IUsuarioRevistaDAL
    {
        List<UsuarioRevista> CarregarAssinaturasValidasPorUsuario(Usuario entidade);
    }
}
