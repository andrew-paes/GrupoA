
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
    public partial interface IProfessorComprovanteDocenciaDAL
    {	
        void Inserir(ProfessorComprovanteDocencia entidade);
        void Atualizar(ProfessorComprovanteDocencia entidade);
        void Excluir(ProfessorComprovanteDocencia entidade);
        ProfessorComprovanteDocencia Carregar(ProfessorComprovanteDocencia entidade);
		
		IEnumerable<ProfessorComprovanteDocencia> Carregar(Arquivo entidade);
		
		IEnumerable<ProfessorComprovanteDocencia> Carregar(Instituicao entidade);
		
		IEnumerable<ProfessorComprovanteDocencia> Carregar(Professor entidade);
				
        IEnumerable<ProfessorComprovanteDocencia> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<ProfessorComprovanteDocencia> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);

        void ExcluirPorProfessorEInstituicao(Int64 professorId, Int64 instituicaoId);
	}
}
