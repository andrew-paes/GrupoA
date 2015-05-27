
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
    public partial interface ITituloInformacaoFichaDAL
    {	
        void Inserir(TituloInformacaoFicha entidade);
        void Atualizar(TituloInformacaoFicha entidade);
        void Excluir(TituloInformacaoFicha entidade);
        TituloInformacaoFicha Carregar(TituloInformacaoFicha entidade);
				
		TituloInformacaoFicha CarregarComDependencias(TituloInformacaoFicha entidade);	
				
        IEnumerable<TituloInformacaoFicha> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<TituloInformacaoFicha> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
