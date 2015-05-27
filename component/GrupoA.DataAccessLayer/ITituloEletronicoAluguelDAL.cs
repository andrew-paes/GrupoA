
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
    public partial interface ITituloEletronicoAluguelDAL
    {	
        void Inserir(TituloEletronicoAluguel entidade);
        void Atualizar(TituloEletronicoAluguel entidade);
        void Excluir(TituloEletronicoAluguel entidade);
        TituloEletronicoAluguel Carregar(TituloEletronicoAluguel entidade);
				
		TituloEletronicoAluguel CarregarComDependencias(TituloEletronicoAluguel entidade);	
		
		IEnumerable<TituloEletronicoAluguel> Carregar(TituloEletronico entidade);
				
        IEnumerable<TituloEletronicoAluguel> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<TituloEletronicoAluguel> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
