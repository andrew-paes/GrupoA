
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
        void Inserir(ProgramaAtualizacaoChamada entidade);
        void Atualizar(ProgramaAtualizacaoChamada entidade);
        void Excluir(ProgramaAtualizacaoChamada entidade);
        ProgramaAtualizacaoChamada Carregar(ProgramaAtualizacaoChamada entidade);
		
		IEnumerable<ProgramaAtualizacaoChamada> Carregar(ProgramaAtualizacaoPagina entidade);
		
		IEnumerable<ProgramaAtualizacaoChamada> Carregar(Arquivo entidade);
				
        IEnumerable<ProgramaAtualizacaoChamada> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<ProgramaAtualizacaoChamada> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
