
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
    public partial interface IAvisoDisponibilidadeStatusDAL
    {	
        void Inserir(AvisoDisponibilidadeStatus entidade);
        void Atualizar(AvisoDisponibilidadeStatus entidade);
        void Excluir(AvisoDisponibilidadeStatus entidade);
        AvisoDisponibilidadeStatus Carregar(AvisoDisponibilidadeStatus entidade);
		
		IEnumerable<AvisoDisponibilidadeStatus> Carregar(AvisoDisponibilidade entidade);
				
        IEnumerable<AvisoDisponibilidadeStatus> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<AvisoDisponibilidadeStatus> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
