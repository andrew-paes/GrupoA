using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrupoA.BusinessObject;
using GrupoA.DataAccess;
using GrupoA.DataAccess.ADO;

namespace GrupoA.BusinessLogicalLayer
{
    /// <summary>
    /// Classe que abstrai as regras de negócio referentes a usuários.
    /// </summary>
    public class ProfissionalOcupacaoBLL : BaseBLL
    {
        private IProfissionalOcupacaoDAL _profissionalOcupacaoDAL;

        private IProfissionalOcupacaoDAL ProfissionalOcupacaoDAL
        {
            get
            {
                if (_profissionalOcupacaoDAL == null)
                    _profissionalOcupacaoDAL = new ProfissionalOcupacaoADO();
                return _profissionalOcupacaoDAL;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public ProfissionalOcupacao Carregar(ProfissionalOcupacao entidade)
        {
            return ProfissionalOcupacaoDAL.Carregar(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public ProfissionalOcupacao CarregarPorCodigoOcupacao(ProfissionalOcupacao entidade)
        {
            return ProfissionalOcupacaoDAL.CarregarPorCodigoOcupacao(entidade);
        }
    }
}