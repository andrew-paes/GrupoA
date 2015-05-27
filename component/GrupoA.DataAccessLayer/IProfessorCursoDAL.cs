
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
    public partial interface IProfessorCursoDAL
    {	
        void Inserir(ProfessorCurso entidade);
        void Atualizar(ProfessorCurso entidade);
        void Excluir(ProfessorCurso entidade);
        ProfessorCurso Carregar(ProfessorCurso entidade);
		
		IEnumerable<ProfessorCurso> Carregar(ProfessorDisciplina entidade);
		
		IEnumerable<ProfessorCurso> Carregar(Curso entidade);
		
		IEnumerable<ProfessorCurso> Carregar(CursoNivel entidade);
		
		IEnumerable<ProfessorCurso> Carregar(ProfessorInstituicao entidade);
				
        IEnumerable<ProfessorCurso> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<ProfessorCurso> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
