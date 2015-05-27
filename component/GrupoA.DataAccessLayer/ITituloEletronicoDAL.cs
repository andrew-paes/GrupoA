
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
    public partial interface ITituloEletronicoDAL
    {	
        void Inserir(TituloEletronico entidade);
        void Atualizar(TituloEletronico entidade);
        void Excluir(TituloEletronico entidade);
        TituloEletronico Carregar(TituloEletronico entidade);
				
		TituloEletronico CarregarComDependencias(TituloEletronico entidade);	
		
		IEnumerable<TituloEletronico> Carregar(CapituloEletronico entidade);
		
		IEnumerable<TituloEletronico> Carregar(TituloEletronicoAluguel entidade);
		
		TituloEletronico Carregar(Titulo entidade);
				
        IEnumerable<TituloEletronico> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<TituloEletronico> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
