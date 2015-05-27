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
    public class TituloImpressoBLL : BaseBLL
    {
        #region Declarações DAL

        private ITituloImpressoDAL _tituloImpressoDal;
        private ITituloImpressoDAL TituloImpressoDal
        {
            get { return _tituloImpressoDal ?? (_tituloImpressoDal = new TituloImpressoADO()); }
        }

        private ITituloDAL _tituloDal;
        private ITituloDAL TituloDal
        {
            get { return _tituloDal ?? (_tituloDal = new TituloADO()); }
        }

        #endregion

        #region Métodos: TituloImpresso

        public TituloImpresso Carregar(TituloImpresso entidade)
        {
            entidade = TituloImpressoDal.Carregar(entidade);
            return entidade;
        }

        public IEnumerable<TituloImpresso> CarregarTodos(TituloImpresso entidade)
        {
            var tituloImpressoFH = new TituloImpressoFH() { Isbn13 = entidade.Isbn13 };
            IEnumerable<TituloImpresso> tituloImpressos = TituloImpressoDal.CarregarTodos(0, 0, null, null, tituloImpressoFH);
            return tituloImpressos;
        }

        public TituloImpresso CarregarPorProduto(Int32 produtoId)
        {
            return TituloImpressoDal.CarregarPorProduto(produtoId);
        }

        public TituloImpresso CarregarPorProduto(Produto entidade)
        {
            TituloImpresso retorno = TituloImpressoDal.CarregarPorProduto(entidade);
            return retorno;
        }

        public TituloImpresso CarregarPorTitulo(Int32 tituloId)
        {
            return TituloImpressoDal.CarregarPorTitulo(tituloId);
        }

        public TituloImpresso Carregar(Titulo entidade)
        {
            return TituloImpressoDal.Carregar(entidade);
        }

        /// <summary>
        /// Método que carrega um Título Impressocom a dependências de Titulo, Produto e Conteudo já configuradas.
        /// </summary>
        /// <param name="tituloImpresso">TituloImpresso com identificador a ser carregado.</param>
        /// <returns>Título com os dados configurados.</returns>
        public TituloImpresso CarregarTituloImpressoComDependencias(TituloImpresso tituloImpresso)
        {
            tituloImpresso = TituloImpressoDal.Carregar(new TituloImpresso() { TituloImpressoId = tituloImpresso.TituloImpressoId });
            tituloImpresso = TituloImpressoDal.CarregarComDependencias(tituloImpresso);
            tituloImpresso.Titulo = TituloDal.Carregar(tituloImpresso.Titulo);
            return tituloImpresso;
        }

        /// <summary>
        /// Método que carrega um Título Impressocom a dependências de Titulo, Produto e Conteudo já configuradas.
        /// </summary>
        /// <param name="tituloImpresso">TituloImpresso com identificador a ser carregado.</param>
        /// <returns>Título com os dados configurados.</returns>
        public TituloImpresso CarregarTituloImpressoComDependenciasPorTitulo(Titulo titulo)
        {
            TituloImpresso tituloImpresso = TituloImpressoDal.Carregar(new Titulo() { TituloId = titulo.TituloId });
            tituloImpresso = TituloImpressoDal.CarregarComDependencias(tituloImpresso);
            tituloImpresso.Titulo = TituloDal.Carregar(tituloImpresso.Titulo);
            return tituloImpresso;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Isbn13"></param>
        /// <returns></returns>
        public TituloImpresso CarregarPorIsbn13(String Isbn13)
        {
            return TituloImpressoDal.CarregarPorIsbn13(Isbn13);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void Inserir(TituloImpresso entidade)
        {
            TituloImpressoDal.Inserir(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void AtualizarISBN(TituloImpresso entidade)
        {
            TituloImpressoDal.AtualizarISBN(entidade);
        }
        #endregion
    }
}