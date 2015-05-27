
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
    public partial interface ITituloInformacaoResumoDAL
    {	
        void Inserir(TituloInformacaoResumo entidade);
        void Atualizar(TituloInformacaoResumo entidade);
        void Excluir(TituloInformacaoResumo entidade);
        TituloInformacaoResumo Carregar(TituloInformacaoResumo entidade);
				
		TituloInformacaoResumo CarregarComDependencias(TituloInformacaoResumo entidade);	
				
        IEnumerable<TituloInformacaoResumo> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<TituloInformacaoResumo> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
