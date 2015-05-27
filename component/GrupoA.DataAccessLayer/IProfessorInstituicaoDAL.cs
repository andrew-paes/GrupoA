
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
    public partial interface IProfessorInstituicaoDAL
    {	
        void Inserir(ProfessorInstituicao entidade);
        void Atualizar(ProfessorInstituicao entidade);
        void Excluir(ProfessorInstituicao entidade);
        ProfessorInstituicao Carregar(ProfessorInstituicao entidade);
		
		IEnumerable<ProfessorInstituicao> Carregar(ProfessorCurso entidade);
		
		IEnumerable<ProfessorInstituicao> Carregar(Instituicao entidade);
		
		IEnumerable<ProfessorInstituicao> Carregar(Professor entidade);
		
		ProfessorInstituicao Carregar(Telefone entidade);
				
        IEnumerable<ProfessorInstituicao> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<ProfessorInstituicao> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
