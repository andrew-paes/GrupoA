
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
    public partial interface IProfessorDAL
    {	
        void Inserir(Professor entidade);
        void Atualizar(Professor entidade);
        void Excluir(Professor entidade);
        Professor Carregar(Professor entidade);
				
		Professor CarregarComDependencias(Professor entidade);	
		
		IEnumerable<Professor> Carregar(ProfessorComprovanteDocencia entidade);
		
		IEnumerable<Professor> Carregar(ProfessorInstituicao entidade);
		
		IEnumerable<Professor> Carregar(TituloSolicitacao entidade);
		
		IEnumerable<Professor> Carregar(GraduacaoProfessor entidade);
				
        IEnumerable<Professor> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<Professor> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
