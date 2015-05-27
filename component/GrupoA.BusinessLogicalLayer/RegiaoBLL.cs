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
    public class RegiaoBLL : BaseBLL
    {
        private IRegiaoDAL _regiaoDAL;

        private IRegiaoDAL RegiaoDAL
        {
            get
            {
                if (_regiaoDAL == null)
                    _regiaoDAL = new RegiaoADO();
                return _regiaoDAL;

            }
        }

        public Regiao Carregar(Regiao entidade)
        {
            return RegiaoDAL.Carregar(entidade);
        }
    }
}