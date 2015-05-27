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
    public class LinkBLL : BaseBLL
    {
        private ILinkDAL _linkDAL;

        private ILinkDAL LinkDAL
        {
            get
            {
                if (_linkDAL == null)
                    _linkDAL = new LinkADO();
                return _linkDAL;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public Link Carregar(Link entidade)
        {
            return LinkDAL.Carregar(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void Inserir(Link entidade)
        {
            LinkDAL.Inserir(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void Atualizar(Link entidade)
        {
            LinkDAL.Atualizar(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Link> CarregarTodos()
        {
            return LinkDAL.CarregarTodos().ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registrosPagina"></param>
        /// <param name="numeroPagina"></param>
        /// <param name="ordemColunas"></param>
        /// <param name="ordemSentidos"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<Link> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {
            return LinkDAL.CarregarTodos(registrosPagina, numeroPagina, ordemColunas, ordemSentidos, filtro).ToList();
        }
    }
}