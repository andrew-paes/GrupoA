using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrupoA.BusinessObject;
using GrupoA.DataAccess;
using GrupoA.DataAccess.ADO;
using System.Transactions;

namespace GrupoA.BusinessLogicalLayer
{
    /// <summary>
    /// Classe que abstrai as regras de negócio referentes a usuários.
    /// </summary>
    public class ProdutoFormatoBLL : BaseBLL
    {
        private IProdutoFormatoDAL _produtoFormatoDAL;

        private IProdutoFormatoDAL ProdutoFormatoDAL
        {
            get
            {
                if (_produtoFormatoDAL == null)
                    _produtoFormatoDAL = new ProdutoFormatoADO();
                return _produtoFormatoDAL;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public ProdutoFormato Inserir(ProdutoFormato entidade)
        {
            ProdutoFormatoDAL.Inserir(entidade);
            return entidade;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public ProdutoFormato Carregar(ProdutoFormato entidade)
        {
            return ProdutoFormatoDAL.Carregar(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void Excluir(ProdutoFormato entidade)
        {
            TimeSpan duracaoTransacao = new TimeSpan(0, 2, 0);

            using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required, duracaoTransacao))
            {
                ProdutoFormatoDAL.Excluir(entidade);

                tScope.Complete();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formato"></param>
        /// <returns></returns>
        public ProdutoFormato Carregar(string formato)
        {
            return ProdutoFormatoDAL.Carregar(formato);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void Atualizar(ProdutoFormato entidade)
        {
            ProdutoFormatoDAL.Atualizar(entidade);
        }
    }
}