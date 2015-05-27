using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrupoA.BusinessObject;
using GrupoA.DataAccess;
using GrupoA.DataAccess.ADO;
using GrupoA.BusinessLogicalLayer.Helper;

namespace GrupoA.BusinessLogicalLayer
{
    /// <summary>
    /// Classe que abstrai as regras de negócio referentes a usuários.
    /// </summary>
    public class RevistaArtigoBLL : BaseBLL
    {
        private IRevistaArtigoDAL _revistaArtigoDAL;

        private IRevistaArtigoDAL RevistaArtigoDAL
        {
            get
            {
                if (_revistaArtigoDAL == null)
                    _revistaArtigoDAL = new RevistaArtigoADO();
                return _revistaArtigoDAL;

            }
        }

        private IProdutoDAL _produtoDAL;

        private IProdutoDAL ProdutoDal
        {
            get
            {
                if (_produtoDAL == null)
                    _produtoDAL = new ProdutoADO();
                return _produtoDAL;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public RevistaArtigo Carregar(RevistaArtigo entidade)
        {
            return RevistaArtigoDAL.Carregar(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="palavra"></param>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public int ContarArtigoBusca(String palavra, Revista entidade)
        {
            palavra = BuscaHelper.FormataPalavraFiltro(palavra);

            return RevistaArtigoDAL.ContarArtigoBusca(palavra, entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registrosPagina"></param>
        /// <param name="numeroPagina"></param>
        /// <param name="ordenacao"></param>
        /// <param name="ordenacaoSentido"></param>
        /// <param name="palavra"></param>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public List<RevistaArtigo> CarregarRevistaArtigoBusca(int registrosPagina, int numeroPagina, String[] ordenacao, String[] ordenacaoSentido, String palavra, Revista entidade)
        {
            palavra = BuscaHelper.FormataPalavraFiltro(palavra);

            return RevistaArtigoDAL.CarregarRevistaArtigoBusca(registrosPagina, numeroPagina, ordenacao, ordenacaoSentido, palavra, entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revistaArtigoId"></param>
        /// <returns></returns>
        public List<RevistaArtigo> CarregarArtigosAssociados(Int32 revistaArtigoId)
        {
            return RevistaArtigoDAL.CarregarArtigosAssociados(revistaArtigoId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revistaArtigo"></param>
        /// <returns></returns>
        public List<Produto> CarregarProdutosPorRevistaArtigo(RevistaArtigo revistaArtigo)
        {
            return ProdutoDal.CarregarPorRevistaArtigo(revistaArtigo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isbn13"></param>
        /// <param name="produtos"></param>
        /// <returns></returns>
        public List<Produto> CarregarProdutosPorIsbn13ExcetoProdutos(String isbn13, List<Produto> produtos)
        {
            return ProdutoDal.CarregarTodosPorIsbn13ExcetoProdutos(isbn13, produtos);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revistaArtigo"></param>
        /// <param name="produto"></param>
        public void ExcluirRevistaArtigoProduto(RevistaArtigo revistaArtigo, Produto produto)
        {
            RevistaArtigoDAL.ExcluirRevistaArtigoProduto(revistaArtigo, produto);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revistaArtigo"></param>
        /// <param name="produto"></param>
        public void IncluirRevistaArtigoProduto(RevistaArtigo revistaArtigo, Produto produto)
        {
            RevistaArtigoDAL.IncluirRevistaArtigoProduto(revistaArtigo, produto);
        }

        /// <summary>
        /// Método que retorna StringBuilder do auto complete da busca
        /// </summary>
        /// <param name="palavra"></param>
        /// <returns></returns>
        public StringBuilder CarregarAutoCompleteBusca(String palavra)
        {
            return RevistaArtigoDAL.CarregarAutoCompleteBusca(palavra);
        }
    }
}