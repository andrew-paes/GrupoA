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
	public class ProfessorComprovanteDocenciaBLL : BaseBLL
	{
		private IProfessorComprovanteDocenciaDAL _professorComprovanteDocenciaDAL;

		private IProfessorComprovanteDocenciaDAL ProfessorComprovanteDocenciaDAL
		{
			get
			{
				if (_professorComprovanteDocenciaDAL == null)
					_professorComprovanteDocenciaDAL = new ProfessorComprovanteDocenciaADO();
				return _professorComprovanteDocenciaDAL;
			}
		}

		public ProfessorComprovanteDocencia Carregar(ProfessorComprovanteDocencia entidade)
		{
			return ProfessorComprovanteDocenciaDAL.Carregar(entidade);
		}

		public IEnumerable<ProfessorComprovanteDocencia> Carregar(Professor entidade)
		{
			return ProfessorComprovanteDocenciaDAL.Carregar(entidade);
		}

        public void ExcluirPorProfessorEInstituicao(Int64 professorId, Int64 instituicaoId)
        {
            ProfessorComprovanteDocenciaDAL.ExcluirPorProfessorEInstituicao(professorId, instituicaoId);
        }
	}
}
