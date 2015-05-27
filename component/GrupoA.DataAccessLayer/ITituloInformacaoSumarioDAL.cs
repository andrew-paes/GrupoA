
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
    public partial interface ITituloInformacaoSumarioDAL
    {	
        void Inserir(TituloInformacaoSumario entidade);
        void Atualizar(TituloInformacaoSumario entidade);
        void Excluir(TituloInformacaoSumario entidade);
        TituloInformacaoSumario Carregar(TituloInformacaoSumario entidade);
				
		TituloInformacaoSumario CarregarComDependencias(TituloInformacaoSumario entidade);	
		
		IEnumerable<TituloInformacaoSumario> Carregar(Arquivo entidade);
				
        IEnumerable<TituloInformacaoSumario> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<TituloInformacaoSumario> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
