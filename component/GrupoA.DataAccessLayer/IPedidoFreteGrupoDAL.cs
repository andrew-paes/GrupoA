
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
    public partial interface IPedidoFreteGrupoDAL
    {	
        void Inserir(PedidoFreteGrupo entidade);
        void Atualizar(PedidoFreteGrupo entidade);
        void Excluir(PedidoFreteGrupo entidade);
        PedidoFreteGrupo Carregar(PedidoFreteGrupo entidade);
		
		IEnumerable<PedidoFreteGrupo> Carregar(PedidoFretePreco entidade);
		
		IEnumerable<PedidoFreteGrupo> Carregar(PedidoFreteTipo entidade);
				
        IEnumerable<PedidoFreteGrupo> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<PedidoFreteGrupo> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
