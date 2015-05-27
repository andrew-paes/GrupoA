using System;
using System.Collections.Generic;
using System.Transactions;
using GrupoA.BusinessObject;
using GrupoA.DataAccess;
using GrupoA.DataAccess.ADO;
using GrupoA.FilterHelper;

namespace GrupoA.BusinessLogicalLayer
{
    public class EnqueteBLL : BaseBLL
    {
        #region Propriedades

        #region enqueteDAL
        private IEnqueteDAL _enqueteDAL;
        private IEnqueteDAL enqueteDAL
        {
            get
            {
                if (_enqueteDAL == null)
                    _enqueteDAL = new EnqueteADO();
                return _enqueteDAL;

            }
        }
        #endregion

        #region enquetePaginaDAL
        private IEnquetePaginaDAL _enquetePaginaDAL;
        private IEnquetePaginaDAL enquetePaginaDAL
        {
            get
            {
                if (_enquetePaginaDAL == null)
                    _enquetePaginaDAL = new EnquetePaginaADO();
                return _enquetePaginaDAL;

            }
        }
        #endregion

        #region enqueteOpcaoDAL
        private IEnqueteOpcaoDAL _enqueteOpcaoDAL;
        private IEnqueteOpcaoDAL enqueteOpcaoDAL
        {
            get
            {
                if (_enqueteOpcaoDAL == null)
                    _enqueteOpcaoDAL = new EnqueteOpcaoADO();
                return _enqueteOpcaoDAL;

            }
        }
        #endregion

        #endregion

        #region Métodos

        #region Métodos referentes a Enquete

        #region public void InserirEnquete(Enquete enquete, List<EnquetePagina> enquetesPaginas)
        /// <summary>
        /// Insere uma nova Enquete no sistema.
        /// </summary>
        /// <param name="enquete">Objeto Enquete que contém as informações a serem inseridas.</param>
        /// <param name="enquetesPaginas">Objeto que contém as páginas que devem ser relacionadas a Enquete</param>
        public void InserirEnquete(Enquete enquete, List<EnquetePagina> enquetesPaginas)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // 1. Inserção da Enquete
                enqueteDAL.Inserir(enquete);
                foreach (EnquetePagina enquetePagina in enquetesPaginas)
                {
                    enqueteDAL.InserirLocalizacao(enquete, enquetePagina);
                }
                scope.Complete();
            }

        }
        #endregion

        #region public void AtualizarEnquete(Enquete enquete)
        /// <summary>
        /// Atualiza informações da Enquete.
        /// </summary>
        /// <param name="enquete">Enquete contendo os dados a serem atualizados.</param>
        public void AtualizarEnquete(Enquete enquete)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                enqueteDAL.ExcluirLocalizacoesPorEnquete(enquete);
                foreach (EnquetePagina enquetePagina in enquete.EnquetePaginas)
                    enqueteDAL.InserirLocalizacao(enquete, enquetePagina);
                enqueteDAL.Atualizar(enquete);
                scope.Complete();
            }
        }
        #endregion

        #region public Enquete CarregarEnquete(Enquete enquete)
        /// <summary>
        /// Carrega as informações referentes à Enquete e as páginas em que será inserida.
        /// </summary>
        /// <param name="enquete">Enquete que deverá ser carregada através do identificador "enqueteId".</param>
        /// <returns>Objeto Enquete contendo as informações populadas.</returns>
        public Enquete CarregarEnquete(Enquete enquete)
        {
            enquete = enqueteDAL.Carregar(enquete);
            enquete.EnquetePaginas = CarregarPaginasEnquete(enquete);
            return enquete;
        }
        #endregion

        #region public void ExcluirEnquete( Enquete enquete )
        /// <summary>
        /// Exclui uma enquete conforme a Enquete recebida. ATENÇÃO: também serão excluídas as suas localizações (páginas) e opções.
        /// </summary>
        /// <param name="enquete">Enquete contendo as informações a serem excluídas (Somente o Id é necessário).</param>
        public void ExcluirEnquete( Enquete enquete )
        {
            enquete = this.CarregarEnquete(enquete);
            using (TransactionScope scope = new TransactionScope())
            {
                enqueteDAL.ExcluirLocalizacoesPorEnquete(enquete);
                enqueteOpcaoDAL.Excluir(enquete);
                enqueteDAL.Excluir(enquete);
                scope.Complete();
            }
        }

        #endregion

        #endregion

        #region Métodos referentes a Página

        #region public IEnumerable<EnquetePagina> CarregarPaginas()
        /// <summary>
        /// Carrega as páginas referentes todas as páginas.
        /// </summary>
        /// <returns>Coleção de objetos EnquetePagina (páginas).</returns>
        public IEnumerable<EnquetePagina> CarregarPaginas()
        {
            return enquetePaginaDAL.CarregarTodos();
        }
        #endregion

        #region public List<EnquetePagina> CarregarPaginasEnquete(Enquete enquete)
        /// <summary>
        /// Carrega todas as páginas de uma Enquete conforme o código identificador fornecido.
        /// </summary>
        /// <param name="enquete">Objeto Enquete que contém o código identificador a ser carregado "enqueteId".</param>
        /// <returns>Coleção de páginas (Objeto EnquetePagina) da Enquete.</returns>
        public List<EnquetePagina> CarregarPaginasEnquete(Enquete enquete)
        {
            List<EnquetePagina> retorno = new List<EnquetePagina>();
            foreach (EnquetePagina enquetePagina in enqueteDAL.CarregarLocalizacoesPorEnquete(enquete))
            {
                retorno.Add(enquetePagina);
            }
            return retorno;
        }
        #endregion

        #endregion

        #region Métodos referentes a Opção

        #region public void InserirOpcao(EnqueteOpcao enqueteOpcao)
        /// <summary>
        /// Insere uma nova Opção para Enquete.
        /// </summary>
        /// <param name="enqueteOpcao">Objeto de Opção que contém as informações a serem inseridas.</param>
        public void InserirOpcao(EnqueteOpcao enqueteOpcao)
        {
            enqueteOpcaoDAL.Inserir(enqueteOpcao);
        }
        #endregion

        #region public List<EnqueteOpcao> CarregarOpcoesEnquetePorEnquete(Enquete enquete)
        /// <summary>
        /// Carrega todas as opções de uma Enquete conforme o código identificador recebido.
        /// </summary>
        /// <param name="enquete">Objeto Enquete que contém o código identificador "enqueteId".</param>
        /// <returns>Coleção de opções da Enquete.</returns>
        public List<EnqueteOpcao> CarregarOpcoesEnquetePorEnquete(Enquete enquete)
        {
            return enqueteOpcaoDAL.CarregarTodosPorEnquete(enquete);
        }
        #endregion

        #region public void ExcluirOpcaoPorEnquete(EnqueteOpcao enqueteOpcao)
        /// <summary>
        /// Exclui uma Opção de Enquete. Caso o número de opções restantes para seja menor que 2, a Enquete será desativada.
        /// </summary>
        /// <param name="enqueteOpcao">Objeto EnqueteOpcao a ser excluído.</param>
        public void ExcluirOpcaoPorEnquete(EnqueteOpcao enqueteOpcao)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                EnqueteOpcao temp = enqueteOpcaoDAL.Carregar(enqueteOpcao);
                enqueteOpcaoDAL.Excluir(enqueteOpcao);
                EnqueteOpcaoFH fh = new FilterHelper.EnqueteOpcaoFH();
                fh.EnqueteId = temp.Enquete.EnqueteId.ToString();
                int opcoes = enqueteOpcaoDAL.TotalRegistros(fh);
                if (opcoes < 2)
                {
                    Enquete enquete = new Enquete();
                    enquete.EnqueteId = temp.Enquete.EnqueteId;
                    enquete.Ativo = false;
                    enqueteDAL.AtualizaStatus(enquete);
                }
                scope.Complete();
            }
        }
        #endregion

        #endregion

        public List<Enquete> CarregarEnquetes(Int32 enquetePaginaId, Usuario usuario)
        {
            List<Enquete> enquete = enqueteDAL.CarregarEnquetePorAreas(enquetePaginaId, usuario);

            return enquete;
        }

        public Enquete VotarNaEnquete(Enquete enquete)
        {
            return enqueteDAL.VotarNaEnquete(enquete);
        }

        #endregion

    }
}
