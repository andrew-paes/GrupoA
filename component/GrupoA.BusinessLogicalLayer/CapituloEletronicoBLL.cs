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
    public class CapituloEletronicoBLL : BaseBLL
    {
        #region Declarações DAL

        private ICapituloDAL _capituloDal;

        private ICapituloDAL CapituloDal
        {
            get { return _capituloDal ?? (_capituloDal = new CapituloADO()); }
        }

        private ICapituloEletronicoDAL _capituloEletronicoDal;

        private ICapituloEletronicoDAL CapituloEletronicoDal
        {
            get { return _capituloEletronicoDal ?? (_capituloEletronicoDal = new CapituloEletronicoADO()); }
        }

        #endregion

        #region Métodos: TituloEletronico

        /// <summary>
        /// 
        /// </summary>
        /// <param name="capituloEletronico"></param>
        /// <returns></returns>
        public CapituloEletronico CarregarComDependencias(CapituloEletronico capituloEletronico)
        {
            capituloEletronico = CapituloEletronicoDal.CarregarComDependencias(capituloEletronico);
            //Carrega informações do Capitulo Eletronico
            capituloEletronico.Produto = new ProdutoBLL().Carregar(new Produto() { ProdutoId = capituloEletronico.Produto.ProdutoId });

            //Carrega informações do Capitulo
            capituloEletronico.Capitulo = CapituloDal.Carregar(capituloEletronico.Capitulo);

            //Carrega informações do Livro Relacionado ao Capitulo Eletronico
            capituloEletronico.TituloEletronico = new TituloEletronicoBLL().Carregar(new TituloEletronico() { TituloEletronicoId = capituloEletronico.TituloEletronico.TituloEletronicoId });
            capituloEletronico.TituloEletronico.Produto = new ProdutoBLL().Carregar(new Produto() { ProdutoId = capituloEletronico.TituloEletronico.TituloEletronicoId });
            capituloEletronico.TituloEletronico.Titulo = new TituloBLL().Carregar(new Titulo() { TituloId = capituloEletronico.TituloEletronico.Titulo.TituloId });

            //Carrega Autores do Capitulo Eletrônico
            capituloEletronico.Capitulo.Autores = CapituloDal.CarregarAutoresDoCapitulo(capituloEletronico.Capitulo);
            return capituloEletronico;
        }

        public IEnumerable<CapituloEletronico> Carregar(TituloEletronico entidade)
        {
            return CapituloEletronicoDal.Carregar(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="capituloBO"></param>
        /// <param name="tituloEletronicoBO"></param>
        /// <returns></returns>
        public CapituloEletronico CarregarCapituloEletronicoRelacionado(Capitulo capituloBO, TituloEletronico tituloEletronicoBO)
        {
            return CapituloEletronicoDal.CarregarCapituloEletronicoRelacionado(capituloBO, tituloEletronicoBO);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void Inserir(CapituloEletronico entidade)
        {
            CapituloEletronicoDal.Inserir(entidade);
        }
        public CapituloEletronico Carregar(CapituloEletronico entidade)
        {
            return CapituloEletronicoDal.Carregar(entidade);
        }

        #endregion

    }
}