using System;
using System.Collections.Generic;
using GrupoA.BusinessObject;

namespace GrupoA.DataAccess
{
    public partial interface IEventoAlertaDAL
    {	
        void Excluir(Evento entidade);
        void CancelarAlertas(Evento entidade);
        EventoAlerta CarregarEventoAlertaPorQtdDias(int usuarioId, int eventoId, int qtdDias);
        List<EventoAlerta> CarregarEventoAlertasPorUsuario(Int32 usuarioId, Int32 eventoId);
        List<Evento> CarregarAlertasAtivosPorUsuario(int registrosPagina, int numeroPagina, Usuario usuario);
        Int32 ContarAlertasAtivosPorUsuario(Usuario usuario);
        Int32 ValidarDiasEventoParaUsuario(Int32 usuarioId, Int32 eventoId, Int32 dias);
	}
}
