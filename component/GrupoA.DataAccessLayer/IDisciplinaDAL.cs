
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
    public partial interface IDisciplinaDAL
    {	
        void Inserir(Disciplina entidade);
        void Atualizar(Disciplina entidade);
        void Excluir(Disciplina entidade);
        Disciplina Carregar(Disciplina entidade);
		
		IEnumerable<Disciplina> Carregar(ProfessorDisciplina entidade);
				
        IEnumerable<Disciplina> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<Disciplina> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
