
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
    public partial interface ITransportadoraServicoDAL
    {	
        void Inserir(TransportadoraServico entidade);
        void Atualizar(TransportadoraServico entidade);
        void Excluir(TransportadoraServico entidade);
        TransportadoraServico Carregar(TransportadoraServico entidade);
		
		IEnumerable<TransportadoraServico> Carregar(Pedido entidade);
		
		IEnumerable<TransportadoraServico> Carregar(Transportadora entidade);
				
        IEnumerable<TransportadoraServico> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<TransportadoraServico> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
