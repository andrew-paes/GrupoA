
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
    public partial interface IGraduacaoProfessorDAL
    {	
        void Inserir(GraduacaoProfessor entidade);
        void Atualizar(GraduacaoProfessor entidade);
        void Excluir(GraduacaoProfessor entidade);
        GraduacaoProfessor Carregar(GraduacaoProfessor entidade);
		
		IEnumerable<GraduacaoProfessor> Carregar(Professor entidade);
				
        IEnumerable<GraduacaoProfessor> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<GraduacaoProfessor> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
