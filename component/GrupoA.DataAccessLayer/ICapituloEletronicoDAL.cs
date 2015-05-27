
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
    public partial interface ICapituloEletronicoDAL
    {	
        void Inserir(CapituloEletronico entidade);
        void Atualizar(CapituloEletronico entidade);
        void Excluir(CapituloEletronico entidade);
        CapituloEletronico Carregar(CapituloEletronico entidade);
				
		CapituloEletronico CarregarComDependencias(CapituloEletronico entidade);	
		
		IEnumerable<CapituloEletronico> Carregar(TituloEletronico entidade);
		
		CapituloEletronico Carregar(Capitulo entidade);
				
        IEnumerable<CapituloEletronico> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<CapituloEletronico> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
