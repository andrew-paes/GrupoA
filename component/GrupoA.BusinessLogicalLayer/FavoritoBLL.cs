using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrupoA.DataAccess;
using GrupoA.BusinessObject;
using GrupoA.FilterHelper;
using GrupoA.DataAccess.ADO;
using GrupoA.BusinessObject.Enumerator;

namespace GrupoA.BusinessLogicalLayer
{
    public class FavoritoBLL : BaseBLL
    {
        #region Declarações DAL

        private IFavoritoDAL _favoritoDAL;

        private IFavoritoDAL FavoritoDAL
        {
            get
            {
                if (_favoritoDAL == null)
                    _favoritoDAL = new FavoritoADO();
                return _favoritoDAL;
            }
        }

        #endregion

        #region Métodos: Favorito

        /// <summary>
        /// Método que persiste um Favorito.
        /// </summary>
        /// <param name="entidade"></param>
        public void Inserir(Favorito entidade)
        {
            FavoritoDAL.Inserir(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public Favorito Carregar(Favorito entidade)
        {
            entidade = FavoritoDAL.Carregar(entidade);
            return entidade;
        }

        /// <summary>
        /// Verifica se este conteúdo já está relacionado com o usuário em "Favorito"
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="conteudo"></param>
        /// <returns></returns>
        public bool FavoritoRelacionado(Usuario usuario, Conteudo conteudo)
        {
            return FavoritoDAL.FavoritoRelacionado(usuario, conteudo);
        }

        #endregion
    }
}