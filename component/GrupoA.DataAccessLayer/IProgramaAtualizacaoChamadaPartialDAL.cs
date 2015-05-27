
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
    public partial interface IProgramaAtualizacaoChamadaDAL
    {
        IEnumerable<ProgramaAtualizacaoPagina> CarregarLocalizacoesPorProgramaAtualizacao(ProgramaAtualizacaoChamada programaAtualizacaoChamada);
        void ExcluirLocalizacoesPorProgramaAtualizacao(ProgramaAtualizacaoChamada programaAtualizacaoChamada);
        void InserirLocalizacao(ProgramaAtualizacaoChamada programaAtualizacaoChamada, ProgramaAtualizacaoPagina programaAtualizacaoPagina);
        List<ProgramaAtualizacaoChamada> CarregarChamadasRandomicas(int numeroMaximoRegistros);
        ProgramaAtualizacaoChamada CarregarProgramaAtualizacaoChamadaPorPagina(ProgramaAtualizacaoPagina programaAtualizacaoPagina);
	}
}
