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
    public class CapituloImpressoBLL : BaseBLL
    {
        #region Declarações DAL

        private ICapituloDAL _capituloDal;

        private ICapituloDAL CapituloDal
        {
            get { return _capituloDal ?? (_capituloDal = new CapituloADO()); }
        }

        private ICapituloImpressoDAL _capituloImpressoDal;

        private ICapituloImpressoDAL CapituloImpressoDal
        {
            get { return _capituloImpressoDal ?? (_capituloImpressoDal = new CapituloImpressoADO()); }
        }

        #endregion

        #region Métodos: TituloImpresso

        /// <summary>
        /// 
        /// </summary>
        /// <param name="capituloImpresso"></param>
        /// <returns></returns>
        public CapituloImpresso CarregarComDependencias(CapituloImpresso capituloImpresso)
        {
            capituloImpresso = CapituloImpressoDal.CarregarComDependencias(capituloImpresso);
            //Carrega informações do Capitulo Impresso
            capituloImpresso.Produto = new ProdutoBLL().Carregar(new Produto() { ProdutoId = capituloImpresso.Produto.ProdutoId });

            //Carrega informações do Capitulo
            capituloImpresso.Capitulo = CapituloDal.Carregar(capituloImpresso.Capitulo);

            //Carrega informações do Livro Relacionado ao Capitulo Impresso
            capituloImpresso.TituloImpresso = new TituloBLL().CarregarTituloImpresso(new TituloImpresso() { TituloImpressoId = capituloImpresso.TituloImpresso.TituloImpressoId });
            capituloImpresso.TituloImpresso.Produto = new ProdutoBLL().Carregar(new Produto() { ProdutoId = capituloImpresso.TituloImpresso.TituloImpressoId });
            capituloImpresso.TituloImpresso.Titulo = new TituloBLL().Carregar(new Titulo() { TituloId = capituloImpresso.TituloImpresso.Titulo.TituloId });

            //Carrega Autores do Capitulo Impresso
            capituloImpresso.Capitulo.Autores = CapituloDal.CarregarAutoresDoCapitulo(capituloImpresso.Capitulo);
            return capituloImpresso;
        }

        public IEnumerable<CapituloImpresso> Carregar(TituloImpresso entidade)
        {
            return CapituloImpressoDal.Carregar(entidade);
        }

        /// <summary>
        /// Verifica se o Capitulo e TituloImpresso estão relacionados
        /// </summary>
        /// <param name="capituloBO"></param>
        /// <param name="tituloImpressoBO"></param>
        /// <returns></returns>
        public bool CapituloImpressoRelacionado(Capitulo capituloBO, TituloImpresso tituloImpressoBO)
        {
            return CapituloImpressoDal.CapituloImpressoRelacionado(capituloBO, tituloImpressoBO);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="capituloBO"></param>
        /// <param name="tituloImpressoBO"></param>
        /// <returns></returns>
        public CapituloImpresso CarregarCapituloImpressoRelacionado(Capitulo capituloBO, TituloImpresso tituloImpressoBO)
        {
            return CapituloImpressoDal.CarregarCapituloImpressoRelacionado(capituloBO, tituloImpressoBO);
        }

        public void Inserir(CapituloImpresso entidade)
        {
            CapituloImpressoDal.Inserir(entidade);
        }

        public CapituloImpresso Carregar(CapituloImpresso entidade)
        {
            return CapituloImpressoDal.Carregar(entidade);
        }

        #endregion
    }
}