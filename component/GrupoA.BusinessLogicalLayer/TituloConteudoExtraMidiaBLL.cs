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
    public class TituloConteudoExtraMidiaBLL : BaseBLL
    {
        private ITituloConteudoExtraMidiaDAL _TituloConteudoExtraMidiaDAL;

        private ITituloConteudoExtraMidiaDAL TituloConteudoExtraMidiaDAL
        {
            get
            {
                if (_TituloConteudoExtraMidiaDAL == null)
                    _TituloConteudoExtraMidiaDAL = new TituloConteudoExtraMidiaADO();
                return _TituloConteudoExtraMidiaDAL;
            }
        }

        public TituloConteudoExtraMidia Carregar(TituloConteudoExtraMidia entidade)
        {
            return TituloConteudoExtraMidiaDAL.Carregar(entidade);
        }

        public TituloConteudoExtraMidia Carregar(Titulo entidade)
        {
            return TituloConteudoExtraMidiaDAL.Carregar(entidade);
        }

        public void Atualizar(TituloConteudoExtraMidia entidade)
        {
            TituloConteudoExtraMidiaDAL.Atualizar(entidade);
        }

        public void Inserir(TituloConteudoExtraMidia entidade)
        {
            TituloConteudoExtraMidiaDAL.Inserir(entidade);
        }
    }
}