using System;
using System.Collections.Generic;
using System.Linq;
using GrupoA.BusinessObject;
using GrupoA.DataAccess;
using GrupoA.DataAccess.ADO;

namespace GrupoA.BusinessLogicalLayer
{
    /// <summary>
    /// Classe que abstrai as regras de negócio referentes a usuários.
    /// </summary>
    public class RevistaBLL : BaseBLL
    {
        private IRevistaDAL _revistaDAL;

        private IRevistaDAL RevistaDAL
        {
            get
            {
                if (_revistaDAL == null)
                    _revistaDAL = new RevistaADO();
                return _revistaDAL;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public Revista Carregar(Revista entidade)
        {
            return RevistaDAL.Carregar(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Revista> CarregarTodos()
        {
            return RevistaDAL.CarregarTodos().ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public Revista CarregarRevistaPorIssn(Revista entidade)
        {
            return RevistaDAL.CarregarRevistaPorIssn(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void Inserir(Revista entidade)
        {
            RevistaDAL.Inserir(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qtdRegistros"></param>
        /// <returns></returns>
        public List<Revista> CarregarRevistasPatio(Int32 qtdRegistros)
        {
            return RevistaDAL.CarregarRevistasPatio(qtdRegistros);
        }
    }
}