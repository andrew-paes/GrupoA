
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
    public partial interface ICompraConjuntaDescontoDAL
    {	
        void Inserir(CompraConjuntaDesconto entidade);
        void Atualizar(CompraConjuntaDesconto entidade);
        void Excluir(CompraConjuntaDesconto entidade);
        CompraConjuntaDesconto Carregar(CompraConjuntaDesconto entidade);
		
		IEnumerable<CompraConjuntaDesconto> Carregar(PedidoCompraConjunta entidade);
		
		IEnumerable<CompraConjuntaDesconto> Carregar(CompraConjunta entidade);
				
        IEnumerable<CompraConjuntaDesconto> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<CompraConjuntaDesconto> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
