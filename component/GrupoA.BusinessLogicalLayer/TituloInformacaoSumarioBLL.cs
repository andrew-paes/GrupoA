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
    public class TituloInformacaoSumarioBLL : BaseBLL
    {
        private ITituloInformacaoSumarioDAL _TituloInformacaoSumarioDAL;

        private ITituloInformacaoSumarioDAL TituloInformacaoSumarioDAL
        {
            get
            {
                if (_TituloInformacaoSumarioDAL == null)
                    _TituloInformacaoSumarioDAL = new TituloInformacaoSumarioADO();
                return _TituloInformacaoSumarioDAL;
            }
        }

        public TituloInformacaoSumario Carregar(TituloInformacaoSumario entidade)
        {
            return TituloInformacaoSumarioDAL.Carregar(entidade);
        }

        public TituloInformacaoSumario Carregar(Titulo entidade)
        {
            return TituloInformacaoSumarioDAL.Carregar(entidade);
        }
    }
}