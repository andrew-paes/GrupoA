using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using GrupoA.DataAccess;
using GrupoA.BusinessObject;
using GrupoA.FilterHelper;
using GrupoA.DataAccess.ADO;
using GrupoA.BusinessObject.Enumerator;

namespace GrupoA.BusinessLogicalLayer
{
    public class TituloEletronicoAluguelBLL : BaseBLL
    {
        #region Declarações DAL

        private ITituloEletronicoAluguelDAL _tituloEletronicoAluguelDAL;

        private ITituloEletronicoAluguelDAL TituloEletronicoAluguelDAL
        {
            get { return _tituloEletronicoAluguelDAL ?? (_tituloEletronicoAluguelDAL = new TituloEletronicoAluguelADO()); }
        }


        #endregion

        #region Métodos

        public TituloEletronicoAluguel Carregar(TituloEletronicoAluguel entidade)
        {
            entidade = TituloEletronicoAluguelDAL.Carregar(entidade);
            return entidade;
        }

        public void Inserir(TituloEletronicoAluguel entidade)
        {
            TituloEletronicoAluguelDAL.Inserir(entidade);
        }

        /// <summary>
        /// Verifica se já existe um TituloEletronico cadastrado com o tempo de aluguel informado
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public bool TituloEletronicoTempoAluguelRelacionado(TituloEletronicoAluguel entidade)
        {
            return TituloEletronicoAluguelDAL.TituloEletronicoTempoAluguelRelacionado(entidade);
        }

        #endregion
    }
}