
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
using GrupoA.BusinessObject.ViewHelper;

namespace GrupoA.DataAccess
{
    public partial interface IDisciplinaDAL
    {
		List<DisciplinaCursoVH> CarregarDisciplinasComCursosPorInstituicoesProfessor(ProfessorInstituicao entidade);
        Disciplina Carregar(String nomeDisciplina);
	}
}
