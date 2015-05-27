
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
    public partial interface ITransportadoraDAL
    {	
        void Inserir(Transportadora entidade);
        void Atualizar(Transportadora entidade);
        void Excluir(Transportadora entidade);
        Transportadora Carregar(Transportadora entidade);
		
		IEnumerable<Transportadora> Carregar(TransportadoraServico entidade);
				
        IEnumerable<Transportadora> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<Transportadora> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
