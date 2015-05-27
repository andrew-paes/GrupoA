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
    public class TelefoneTipoBLL : BaseBLL
    {
        private ITelefoneTipoDAL _telefoneTipoDAL;

        private ITelefoneTipoDAL TelefoneTipoDAL
        {
            get
            {
                if (_telefoneTipoDAL == null)
                    _telefoneTipoDAL = new TelefoneTipoADO();
                return _telefoneTipoDAL;

            }
        }

        public TelefoneTipo Carregar(TelefoneTipo entidade)
        {
            return TelefoneTipoDAL.Carregar(entidade);
        }

    }
}
