
/*
'===============================================================================
'
'  Template: Gerador Código C#.csgen
'  Script versão: 0.96
'  Script criado por: Leonardo Alves Lindermann (lindermannla@ag2.com.br)
'  Gerado pelo MyGeneration versão # (???)
'
'===============================================================================
*/
using System;
using System.Text;
using System.Collections.Generic;
using GrupoA.BusinessObject;
using GrupoA.FilterHelper;

namespace GrupoA.DataAccess
{
    public partial interface IEventoDAL
    {	
        void AtualizarStatus(Evento entidade);
        IEnumerable<Evento> CarregarTodosValidosComDependencias(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        int ContarTodosValidosComDependencias();
        List<Evento> CarregarEventosPorCategoria(Categoria categoria);
        List<Evento> CarregarEventosPorCategoria(List<Categoria> categoriaBOList);
        int ContarTodosEventosValidosComDependencias(int areaId);
	}
}
