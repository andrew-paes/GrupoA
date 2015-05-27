
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
    public partial interface ICapituloImpressoDAL
    {	
        void Inserir(CapituloImpresso entidade);
        void Atualizar(CapituloImpresso entidade);
        void Excluir(CapituloImpresso entidade);
        CapituloImpresso Carregar(CapituloImpresso entidade);
				
		CapituloImpresso CarregarComDependencias(CapituloImpresso entidade);	
		
		IEnumerable<CapituloImpresso> Carregar(TituloImpresso entidade);
		
		CapituloImpresso Carregar(Capitulo entidade);
				
        IEnumerable<CapituloImpresso> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<CapituloImpresso> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
