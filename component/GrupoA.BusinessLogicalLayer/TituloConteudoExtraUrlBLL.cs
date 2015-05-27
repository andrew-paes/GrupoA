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
    public class TituloConteudoExtraUrlBLL : BaseBLL
    {
        private ITituloConteudoExtraUrlDAL _TituloConteudoExtraUrlDAL;

        private ITituloConteudoExtraUrlDAL TituloConteudoExtraUrlDAL
        {
            get
            {
                if (_TituloConteudoExtraUrlDAL == null)
                    _TituloConteudoExtraUrlDAL = new TituloConteudoExtraUrlADO();
                return _TituloConteudoExtraUrlDAL;
            }
        }

        public TituloConteudoExtraUrl Carregar(TituloConteudoExtraUrl entidade)
        {
            return TituloConteudoExtraUrlDAL.Carregar(entidade);
        }

        public TituloConteudoExtraUrl Carregar(Titulo entidade)
        {
            return TituloConteudoExtraUrlDAL.Carregar(entidade);
        }

        public void Atualizar(TituloConteudoExtraUrl entidade)
        {
            TituloConteudoExtraUrlDAL.Atualizar(entidade);
        }

        public void Inserir(TituloConteudoExtraUrl entidade)
        {
            TituloConteudoExtraUrlDAL.Inserir(entidade);
        }
    }
}