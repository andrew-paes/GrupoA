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
    public class TituloInformacaoMaterialDidaticoBLL : BaseBLL
    {
        private ITituloInformacaoMaterialDidaticoDAL _TituloInformacaoMaterialDidaticoDAL;

        private ITituloInformacaoMaterialDidaticoDAL TituloInformacaoMaterialDidaticoDAL
        {
            get
            {
                if (_TituloInformacaoMaterialDidaticoDAL == null)
                    _TituloInformacaoMaterialDidaticoDAL = new TituloInformacaoMaterialDidaticoADO();
                return _TituloInformacaoMaterialDidaticoDAL;
            }
        }

        public TituloInformacaoMaterialDidatico Carregar(TituloInformacaoMaterialDidatico entidade)
        {
            return TituloInformacaoMaterialDidaticoDAL.Carregar(entidade);
        }

        public TituloInformacaoMaterialDidatico Carregar(Titulo entidade)
        {
            return TituloInformacaoMaterialDidaticoDAL.Carregar(entidade);
        }
    }
}