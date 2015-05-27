using System;
using GrupoA.BusinessObject;
using GrupoA.DataAccess;
using GrupoA.DataAccess.ADO;

namespace GrupoA.BusinessLogicalLayer
{
    /// <summary>
    /// Classe que abstrai as regras de negócio referentes a usuários.
    /// </summary>
    public class RevistaArtigoControversiaBLL : BaseBLL
    {
        private IRevistaArtigoControversiaDAL _revistaArtigoControversiaDAL;

        private IRevistaArtigoControversiaDAL RevistaArtigoControversiaDAL
        {
            get
            {
                if (_revistaArtigoControversiaDAL == null)
                    _revistaArtigoControversiaDAL = new RevistaArtigoControversiaADO();
                return _revistaArtigoControversiaDAL;

            }
        }

        public RevistaArtigoControversia CarregarPorArtigoIdPosicionamento(RevistaArtigoControversia entidade)
        {
            return RevistaArtigoControversiaDAL.CarregarPorArtigoIdPosicionamento(entidade);
        }

        public void Atualizar(RevistaArtigoControversia revistaArtigoControversia)
        {
            RevistaArtigoControversiaDAL.Atualizar(revistaArtigoControversia);
        }

        public void Inserir(RevistaArtigoControversia revistaArtigoControversia)
        {
            RevistaArtigoControversiaDAL.Inserir(revistaArtigoControversia);
        }

        public void ExcluirTodosPorRevistaArtigoId(Int32 revistaArtigoId)
        {
            RevistaArtigoControversiaDAL.ExcluirTodosPorRevistaArtigoId(revistaArtigoId);
        }
    }
}