
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
    public partial interface ICursoNivelDAL
    {	
        void Inserir(CursoNivel entidade);
        void Atualizar(CursoNivel entidade);
        void Excluir(CursoNivel entidade);
        CursoNivel Carregar(CursoNivel entidade);
		
		IEnumerable<CursoNivel> Carregar(ProfessorCurso entidade);
				
        IEnumerable<CursoNivel> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<CursoNivel> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
