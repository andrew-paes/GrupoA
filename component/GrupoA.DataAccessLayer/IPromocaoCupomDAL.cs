
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
    public partial interface IPromocaoCupomDAL
    {	
        void Inserir(PromocaoCupom entidade);
        void Atualizar(PromocaoCupom entidade);
        void Excluir(PromocaoCupom entidade);
        PromocaoCupom Carregar(PromocaoCupom entidade);
		
		IEnumerable<PromocaoCupom> Carregar(PromocaoCupomPedido entidade);
		
		IEnumerable<PromocaoCupom> Carregar(Promocao entidade);
				
        IEnumerable<PromocaoCupom> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<PromocaoCupom> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
