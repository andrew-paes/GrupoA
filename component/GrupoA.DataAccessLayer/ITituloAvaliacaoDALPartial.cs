using System;
using System.Collections.Generic;
using GrupoA.BusinessObject;

namespace GrupoA.DataAccess
{
    public partial interface ITituloAvaliacaoDAL
    {
        TituloAvaliacao CarregarPorSolicitacao(TituloSolicitacao entidade);
        List<TituloAvaliacao> CarregarAvaliacoesPublicacao(Int32 usuarioId);
	}
}
