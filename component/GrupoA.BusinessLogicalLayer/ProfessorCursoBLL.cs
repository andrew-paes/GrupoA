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
	public class ProfessorCursoBLL : BaseBLL
	{
		private IProfessorCursoDAL _professorCursoDAL;

		private IProfessorCursoDAL ProfessorCursoDAL
		{
			get
			{
				if (_professorCursoDAL == null)
					_professorCursoDAL = new ProfessorCursoADO();
				return _professorCursoDAL;
			}
		}

		public ProfessorCurso Carregar(ProfessorCurso entidade)
		{
			return ProfessorCursoDAL.Carregar(entidade);
		}

		public IEnumerable<ProfessorCurso> Carregar(ProfessorInstituicao entidade)
		{
			return ProfessorCursoDAL.Carregar(entidade);
		}

        public void Inserir(ProfessorCurso entidade)
        {
            ProfessorCursoDAL.Inserir(entidade);
        }

        public ProfessorCurso CarregarPorProfessorInstituicaoCurso(ProfessorInstituicao professorInstituicaoBO, Curso cursoBO)
        {
            return ProfessorCursoDAL.CarregarPorProfessorInstituicaoCurso(professorInstituicaoBO, cursoBO);
        }
	}
}