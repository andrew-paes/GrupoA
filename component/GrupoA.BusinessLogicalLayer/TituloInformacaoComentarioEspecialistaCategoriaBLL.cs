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
    public class TituloInformacaoComentarioEspecialistaCategoriaBLL : BaseBLL
    {
        private ITituloInformacaoComentarioEspecialistaCategoriaDAL _tituloInformacaoComentarioEspecialistaCategoriaDAL;

        private ITituloInformacaoComentarioEspecialistaCategoriaDAL TituloInformacaoComentarioEspecialistaCategoriaDAL
        {
            get
            {
                if (_tituloInformacaoComentarioEspecialistaCategoriaDAL == null)
                    _tituloInformacaoComentarioEspecialistaCategoriaDAL = new TituloInformacaoComentarioEspecialistaCategoriaADO();
                return _tituloInformacaoComentarioEspecialistaCategoriaDAL;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public TituloInformacaoComentarioEspecialistaCategoria Inserir(TituloInformacaoComentarioEspecialistaCategoria entidade)
        {
            TituloInformacaoComentarioEspecialistaCategoriaDAL.Inserir(entidade);
            return entidade;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public TituloInformacaoComentarioEspecialistaCategoria Carregar(TituloInformacaoComentarioEspecialistaCategoria entidade)
        {
            return TituloInformacaoComentarioEspecialistaCategoriaDAL.Carregar(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void Excluir(TituloInformacaoComentarioEspecialistaCategoria entidade)
        {
            TimeSpan duracaoTransacao = new TimeSpan(0, 2, 0);

            using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required, duracaoTransacao))
            {
                TituloInformacaoComentarioEspecialistaCategoriaDAL.Excluir(entidade);

                tScope.Complete();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void ExcluirTodosPorComentarioEspecialista(TituloInformacaoComentarioEspecialista entidade)
        {
            TimeSpan duracaoTransacao = new TimeSpan(0, 2, 0);

            using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required, duracaoTransacao))
            {
                TituloInformacaoComentarioEspecialistaCategoriaDAL.ExcluirTodosPorComentarioEspecialista(entidade.TituloInformacaoComentarioEspecialistaId);

                tScope.Complete();
            }
        }
    }
}