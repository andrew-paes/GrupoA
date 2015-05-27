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
    public class TituloInformacaoResumoBLL : BaseBLL
    {
        private ITituloInformacaoResumoDAL _TituloInformacaoResumoDAL;

        private ITituloInformacaoResumoDAL TituloInformacaoResumoDAL
        {
            get
            {
                if (_TituloInformacaoResumoDAL == null)
                    _TituloInformacaoResumoDAL = new TituloInformacaoResumoADO();
                return _TituloInformacaoResumoDAL;
            }
        }

        public TituloInformacaoResumo Carregar(TituloInformacaoResumo entidade)
        {
            return TituloInformacaoResumoDAL.Carregar(entidade);
        }

        public TituloInformacaoResumo Carregar(Titulo entidade)
        {
            return TituloInformacaoResumoDAL.Carregar(entidade);
        }
    }
}