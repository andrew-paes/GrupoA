using System;
using System.Collections.Generic;
using GrupoA.BusinessObject;

namespace GrupoA.DataAccess
{
    public partial interface IProfessorInstituicaoDAL
    {
        IEnumerable<ProfessorInstituicao> CarregarComDependenciasPorProfessor(Professor professor, String professorInstituicaoIdsRemovidos);
        bool ValidarProfessorInstituicaoUnico(Professor professorBO, Instituicao instituicaoBO);
        ProfessorInstituicao CarregarPorProfessorInstituicao(Professor professorBO, Instituicao instituicaoBO);
        IEnumerable<ProfessorInstituicao> CarregarComDependenciasPorProfessorInstituicoesIds(Professor entidade, String professorInstituicaoIdsRemovidos);
    }
}