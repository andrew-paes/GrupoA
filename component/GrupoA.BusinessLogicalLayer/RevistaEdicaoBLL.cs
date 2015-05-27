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
    public class RevistaEdicaoBLL : BaseBLL
    {
        private IRevistaEdicaoDAL _revistaEdicaoDAL;

        private IRevistaEdicaoDAL RevistaEdicaoDAL
        {
            get
            {
                if (_revistaEdicaoDAL == null)
                    _revistaEdicaoDAL = new RevistaEdicaoADO();
                return _revistaEdicaoDAL;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public RevistaEdicao CarregarComDependencia(RevistaEdicao entidade)
        {
            return RevistaEdicaoDAL.CarregarComDependencia(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public RevistaEdicao CarregarComDependencias(RevistaEdicao entidade)
        {
            return RevistaEdicaoDAL.CarregarComDependencias(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void Atualizar(RevistaEdicao entidade)
        {
            RevistaEdicaoDAL.Atualizar(entidade);
        }

        /// <summary>
        /// Carrega pesquisando por revistaId e numeroEdicao
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public RevistaEdicao CarregarPorRevistaNumEdicao(RevistaEdicao entidade)
        {
            return RevistaEdicaoDAL.CarregarPorRevistaNumEdicao(entidade);
        }

        public void Inserir(RevistaEdicao entidade)
        {
            RevistaEdicaoDAL.Inserir(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registrosPagina"></param>
        /// <param name="numeroPagina"></param>
        /// <param name="ordemColunas"></param>
        /// <param name="ordemSentidos"></param>
        /// <param name="revistaId"></param>
        /// <param name="revistaEdicaoId"></param>
        /// <returns></returns>
        public List<RevistaEdicao> CarregarTodasEdicoes(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, Int32 revistaId)
        {
            return RevistaEdicaoDAL.CarregarTodasEdicoes(registrosPagina, numeroPagina, ordemColunas, ordemSentidos, revistaId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revistaEdicaoId"></param>
        /// <returns></returns>
        public Int32 ContarEdicoesDiferentesDaEdicaoEntrada(Int32 revistaId)
        {
            return RevistaEdicaoDAL.ContarEdicoesDiferentesDaEdicaoEntrada(revistaId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revistaEdicaoId"></param>
        /// <returns></returns>
        public RevistaEdicao CarregarEdicaoComProduto(Int32 revistaEdicaoId)
        {
            return RevistaEdicaoDAL.CarregarEdicaoComProduto(revistaEdicaoId);
        }

        public RevistaEdicao Carregar(RevistaEdicao entidade)
        {
            return RevistaEdicaoDAL.Carregar(entidade);
        }
    }
}