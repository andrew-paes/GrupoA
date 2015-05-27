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
    public class ProfessorInstituicaoBLL : BaseBLL
    {
        private IProfessorInstituicaoDAL _professorInstituicaoDAL;

        private IProfessorInstituicaoDAL ProfessorInstituicaoDAL
        {
            get
            {
                if (_professorInstituicaoDAL == null)
                    _professorInstituicaoDAL = new ProfessorInstituicaoADO();
                return _professorInstituicaoDAL;
            }
        }

        public ProfessorInstituicao Carregar(ProfessorInstituicao entidade)
        {
            return ProfessorInstituicaoDAL.Carregar(entidade);
        }

        public IEnumerable<ProfessorInstituicao> Carregar(Professor entidade)
        {
            return ProfessorInstituicaoDAL.Carregar(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void Inserir(ProfessorInstituicao entidade)
        {
            ProfessorInstituicaoDAL.Inserir(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="professorBO"></param>
        /// <param name="instituicaoBO"></param>
        /// <returns></returns>
        public bool ValidarProfessorInstituicaoUnico(Professor professorBO, Instituicao instituicaoBO)
        {
            return ProfessorInstituicaoDAL.ValidarProfessorInstituicaoUnico(professorBO, instituicaoBO);
        }

        public ProfessorInstituicao CarregarPorProfessorInstituicao(Professor professorBO, Instituicao instituicaoBO)
        {
            return ProfessorInstituicaoDAL.CarregarPorProfessorInstituicao(professorBO, instituicaoBO);
        }
    }
}