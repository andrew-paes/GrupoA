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
    public class TituloConteudoExtraArquivoBLL : BaseBLL
    {
        #region Propriedades

        private ITituloConteudoExtraArquivoDAL _tituloConteudoExtraArquivoDAL;

        private ITituloConteudoExtraArquivoDAL TituloConteudoExtraArquivoDAL
        {
            get
            {
                if (_tituloConteudoExtraArquivoDAL == null)
                    _tituloConteudoExtraArquivoDAL = new TituloConteudoExtraArquivoADO();
                return _tituloConteudoExtraArquivoDAL;

            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void Inserir(TituloConteudoExtraArquivo entidade)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                entidade.Arquivo.TamanhoArquivo = 0;
                new ArquivoBLL().InserirNovoArquivo(entidade.Arquivo);

                TituloConteudoExtraArquivoDAL.Inserir(entidade);

                scope.Complete();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void Atualizar(TituloConteudoExtraArquivo entidade)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                entidade.Arquivo.TamanhoArquivo = 0;
                new ArquivoBLL().Atualizar(entidade.Arquivo);

                TituloConteudoExtraArquivoDAL.Atualizar(entidade);

                scope.Complete();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void Excluir(TituloConteudoExtraArquivo entidade)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                TituloConteudoExtraArquivo tituloConteudoExtraArquivo = this.CarregarComDependencia(new TituloConteudoExtraArquivo(entidade.TituloConteudoExtraArquivoId));

                TituloConteudoExtraArquivoDAL.Excluir(tituloConteudoExtraArquivo);

                new ArquivoBLL().ExcluirArquivo(tituloConteudoExtraArquivo.Arquivo);

                scope.Complete();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public TituloConteudoExtraArquivo Carregar(TituloConteudoExtraArquivo entidade)
        {
            return TituloConteudoExtraArquivoDAL.Carregar(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns></returns>
        public List<Titulo> CarregarSalaAulaPorCategoria(Usuario usuario)
        {
            return TituloConteudoExtraArquivoDAL.CarregarSalaAulaPorCategoria(usuario);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public TituloConteudoExtraArquivo CarregarComDependencia(TituloConteudoExtraArquivo entidade)
        {
            return TituloConteudoExtraArquivoDAL.CarregarComDependencia(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tituloId"></param>
        /// <returns></returns>
        public List<TituloConteudoExtraArquivo> CarregarTodosComDependenciaPorTitulo(Int32 tituloId)
        {
            return TituloConteudoExtraArquivoDAL.CarregarTodosComDependenciaPorTitulo(tituloId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void InserirTituloConteudoExtraArquivo(TituloConteudoExtraArquivo entidade)
        {
            TituloConteudoExtraArquivoDAL.Inserir(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void AtualizarNomeConteudo(TituloConteudoExtraArquivo entidade)
        {
            TituloConteudoExtraArquivoDAL.AtualizarNomeConteudo(entidade);
        }

        #endregion
    }
}