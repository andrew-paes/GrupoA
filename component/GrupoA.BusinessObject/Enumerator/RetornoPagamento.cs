using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrupoA.BusinessObject.Enumerator
{
    public enum RetornoPagamento
    {
        Sucesso = 0,
        ErroArtmed = 1,
        ErroIPagare = 2,
        ErroCliente = 3
    }
}