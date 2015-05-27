using System;
using System.Collections.Generic;
using GrupoA.BusinessObject;
using GrupoA.DataAccess;
using GrupoA.DataAccess.ADO;

namespace GrupoA.BusinessLogicalLayer
{
    /// <summary>
    /// Classe que abstrai as regras de negócio referentes a usuários.
    /// </summary>
    public class RevistaAssinaturaBLL : BaseBLL
    {
        private IRevistaAssinaturaDAL _revistaAssinaturaDAL;

        private IRevistaAssinaturaDAL RevistaAssinaturaDAL
        {
            get
            {
                if (_revistaAssinaturaDAL == null)
                    _revistaAssinaturaDAL = new RevistaAssinaturaADO();
                return _revistaAssinaturaDAL;
            }
        }

        public RevistaAssinatura Carregar(RevistaAssinatura entidade)
        {
            return RevistaAssinaturaDAL.Carregar(entidade);
        }

        public IEnumerable<RevistaAssinatura> Carregar(Revista entidade)
        {
            return RevistaAssinaturaDAL.Carregar(entidade);
        }

        public void Atualizar(RevistaAssinatura entidade)
        {
            RevistaAssinaturaDAL.Atualizar(entidade);
        }

        public void Inserir(RevistaAssinatura entidade)
        {
            RevistaAssinaturaDAL.Inserir(entidade);
        }

        public RevistaAssinatura CarregarPorRevistaNumExemplares(RevistaAssinatura entidade)
        {
            return RevistaAssinaturaDAL.CarregarPorRevistaNumExemplares(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registrosPagina"></param>
        /// <param name="numeroPagina"></param>
        /// <param name="ordemColunas"></param>
        /// <param name="ordemSentidos"></param>
        /// <param name="revistaId"></param>
        /// <returns></returns>
        public List<RevistaAssinatura> CarregarTodosAssinaturasPorRevistaId(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, Int32 revistaId)
        {
            return RevistaAssinaturaDAL.CarregarTodosAssinaturasPorRevistaId(registrosPagina, numeroPagina, ordemColunas, ordemSentidos, revistaId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revistaId"></param>
        /// <returns></returns>
        public Int32 ContarTodasAssinaturasPorRevistaId(Int32 revistaId)
        {
            return RevistaAssinaturaDAL.ContarTodasAssinaturasPorRevistaId(revistaId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revistaAssinatura"></param>
        /// <returns></returns>
        public RevistaAssinatura CarregarComDependencias(RevistaAssinatura revistaAssinatura)
        {
            return RevistaAssinaturaDAL.CarregarComDependencias(revistaAssinatura);
        }
    }
}