
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
    public partial interface IInstituicaoDAL
    {	
        void Inserir(Instituicao entidade);
        void Atualizar(Instituicao entidade);
        void Excluir(Instituicao entidade);
        Instituicao Carregar(Instituicao entidade);
		
		IEnumerable<Instituicao> Carregar(ProfessorComprovanteDocencia entidade);
		
		IEnumerable<Instituicao> Carregar(ProfessorInstituicao entidade);
				
        IEnumerable<Instituicao> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<Instituicao> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
