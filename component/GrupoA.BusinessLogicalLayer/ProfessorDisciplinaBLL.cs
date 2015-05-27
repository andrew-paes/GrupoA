using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrupoA.BusinessObject;
using GrupoA.DataAccess;
using GrupoA.DataAccess.ADO;
using GrupoA.FilterHelper;

namespace GrupoA.BusinessLogicalLayer
{
	/// <summary>
	/// Classe que abstrai as regras de negócio referentes a usuários.
	/// </summary>
	public class ProfessorDisciplinaBLL : BaseBLL
	{
		private IProfessorDisciplinaDAL _professorDisciplinaDAL;

		private IProfessorDisciplinaDAL ProfessorDisciplinaDAL
		{
			get
			{
				if (_professorDisciplinaDAL == null)
					_professorDisciplinaDAL = new ProfessorDisciplinaADO();
				return _professorDisciplinaDAL;
			}
		}

		public ProfessorDisciplina Carregar(ProfessorDisciplina entidade)
		{
			return ProfessorDisciplinaDAL.Carregar(entidade);
		}

		public IEnumerable<ProfessorDisciplina> Carregar(ProfessorCurso entidade)
		{
			return ProfessorDisciplinaDAL.Carregar(entidade);
		}

        public void Inserir(ProfessorDisciplina entidade)
        {
            ProfessorDisciplinaDAL.Inserir(entidade);
        }

        public ProfessorDisciplina CarregarPorProfessorCursoDisciplina(ProfessorCurso professorCursoBO, Disciplina disciplinaBO)
        {
            return ProfessorDisciplinaDAL.CarregarPorProfessorCursoDisciplina(professorCursoBO, disciplinaBO);
        }
	}
}