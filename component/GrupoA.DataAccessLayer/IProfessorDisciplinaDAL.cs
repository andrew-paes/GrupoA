
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
    public partial interface IProfessorDisciplinaDAL
    {	
        void Inserir(ProfessorDisciplina entidade);
        void Atualizar(ProfessorDisciplina entidade);
        void Excluir(ProfessorDisciplina entidade);
        ProfessorDisciplina Carregar(ProfessorDisciplina entidade);
		
		IEnumerable<ProfessorDisciplina> Carregar(Disciplina entidade);
		
		IEnumerable<ProfessorDisciplina> Carregar(ProfessorCurso entidade);
				
        IEnumerable<ProfessorDisciplina> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<ProfessorDisciplina> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
