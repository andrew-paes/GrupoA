
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
    public partial interface IProgramaAtualizacaoPaginaDAL
    {	
        void Inserir(ProgramaAtualizacaoPagina entidade);
        void Atualizar(ProgramaAtualizacaoPagina entidade);
        void Excluir(ProgramaAtualizacaoPagina entidade);
        ProgramaAtualizacaoPagina Carregar(ProgramaAtualizacaoPagina entidade);
		
		IEnumerable<ProgramaAtualizacaoPagina> Carregar(ProgramaAtualizacaoChamada entidade);
				
        IEnumerable<ProgramaAtualizacaoPagina> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<ProgramaAtualizacaoPagina> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
