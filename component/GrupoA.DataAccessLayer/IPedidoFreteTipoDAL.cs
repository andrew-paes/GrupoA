
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
    public partial interface IPedidoFreteTipoDAL
    {	
        void Inserir(PedidoFreteTipo entidade);
        void Atualizar(PedidoFreteTipo entidade);
        void Excluir(PedidoFreteTipo entidade);
        PedidoFreteTipo Carregar(PedidoFreteTipo entidade);
		
		IEnumerable<PedidoFreteTipo> Carregar(PedidoFreteGrupo entidade);
				
        IEnumerable<PedidoFreteTipo> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<PedidoFreteTipo> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
