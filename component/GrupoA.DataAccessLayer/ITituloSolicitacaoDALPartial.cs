using System;
using System.Collections.Generic;
using GrupoA.BusinessObject;
using GrupoA.BusinessObject.ViewHelper;
using GrupoA.FilterHelper;

namespace GrupoA.DataAccess
{
    public partial interface ITituloSolicitacaoDAL
    {
        void AtualizarStatus(TituloSolicitacao entidade);
        TituloSolicitacao CarregarTituloSolicitacaoPorProfessorTitulo(int professorId, int tituloId);
        IEnumerable<TituloSolicitacaoVH> CarregarTodosCompleto(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        int ContarTodosCompleto(IFilterHelper filtro);
        Int32 TotalRegistrosPendentesPorProfessor(Int32 professorId);
        Int32 ContarAvaliacoesPendencias(Int32 professorId);
	}
}
