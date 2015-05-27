
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
    public partial interface IRevistaPacoteBrindeRegraDAL
    {	
        void Inserir(RevistaPacoteBrindeRegra entidade);
        void Atualizar(RevistaPacoteBrindeRegra entidade);
        void Excluir(RevistaPacoteBrindeRegra entidade);
        RevistaPacoteBrindeRegra Carregar(RevistaPacoteBrindeRegra entidade);
		
		IEnumerable<RevistaPacoteBrindeRegra> Carregar(RevistaPacote entidade);
				
        IEnumerable<RevistaPacoteBrindeRegra> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<RevistaPacoteBrindeRegra> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
