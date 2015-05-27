
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
    public partial interface IAvisoDisponibilidadeDAL
    {	
        void Inserir(AvisoDisponibilidade entidade);
        void Atualizar(AvisoDisponibilidade entidade);
        void Excluir(AvisoDisponibilidade entidade);
        AvisoDisponibilidade Carregar(AvisoDisponibilidade entidade);
		
		IEnumerable<AvisoDisponibilidade> Carregar(AvisoDisponibilidadeStatus entidade);
		
		IEnumerable<AvisoDisponibilidade> Carregar(Produto entidade);
				
        IEnumerable<AvisoDisponibilidade> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<AvisoDisponibilidade> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
