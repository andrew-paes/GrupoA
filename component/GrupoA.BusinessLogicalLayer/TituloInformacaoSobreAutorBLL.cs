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
    public class TituloInformacaoSobreAutorBLL : BaseBLL
    {
        private ITituloInformacaoSobreAutorDAL _TituloInformacaoSobreAutorDAL;

        private ITituloInformacaoSobreAutorDAL TituloInformacaoSobreAutorDAL
        {
            get
            {
                if (_TituloInformacaoSobreAutorDAL == null)
                    _TituloInformacaoSobreAutorDAL = new TituloInformacaoSobreAutorADO();
                return _TituloInformacaoSobreAutorDAL;
            }
        }

        public TituloInformacaoSobreAutor Carregar(TituloInformacaoSobreAutor entidade)
        {
            return TituloInformacaoSobreAutorDAL.Carregar(entidade);
        }

        public TituloInformacaoSobreAutor Carregar(Titulo entidade)
        {
            return TituloInformacaoSobreAutorDAL.Carregar(entidade);
        }
    }
}