using System.Collections.Generic;
using GrupoA.DataAccess;
using GrupoA.BusinessObject;
using GrupoA.DataAccess.ADO;
using GrupoA.FilterHelper;
using System.Transactions;
using System.Text;

namespace GrupoA.BusinessLogicalLayer
{
    public class OfertaBLL : BaseBLL
    {
        #region Declarações DAL

        private IOfertaDAL _ofertaDal;
        private IOfertaDAL OfertaDal
        {
            get { return _ofertaDal ?? (_ofertaDal = new OfertaADO()); }
        }

        private IOfertaTipoDAL _ofertaTipoDal;
        private IOfertaTipoDAL OfertaTipoDal
        {
            get { return _ofertaTipoDal ?? (_ofertaTipoDal = new OfertaTipoADO()); }
        }

        private IOfertaCategoriaDAL _ofertaCategoriaDal;
        private IOfertaCategoriaDAL OfertaCategoriaDal
        {
            get { return _ofertaCategoriaDal ?? (_ofertaCategoriaDal = new OfertaCategoriaADO()); }
        }

        private IOfertaProdutoDAL _ofertaProdutoDal;
        private IOfertaProdutoDAL OfertaProdutoDal
        {
            get { return _ofertaProdutoDal ?? (_ofertaProdutoDal = new OfertaProdutoADO()); }
        }

        private IProdutoDAL _produtoDal;
        private IProdutoDAL ProdutoDal
        {
            get { return _produtoDal ?? (_produtoDal = new ProdutoADO()); }
        }

        #endregion

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="oferta"></param>
        /// <returns></returns>
        public Oferta Carregar(Oferta oferta)
        {
            return OfertaDal.Carregar(oferta);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oferta"></param>
        /// <param name="categorias"></param>
        /// <param name="produtos"></param>
        /// <returns></returns>
        public Oferta Inserir(Oferta oferta, List<Categoria> categorias, List<Produto> produtos)
        {
            if (OfertaDal.ValidarOferta(oferta, categorias, produtos) > 0)
            {
                return null;
            }

            using (TransactionScope scope = new TransactionScope())
            {
                OfertaDal.Inserir(oferta);

                if (oferta.OfertaTipo.OfertaTipoId == 3)
                {
                    // Inserção de Categorias
                    foreach (Categoria categoria in categorias)
                    {
                        OfertaCategoria ofertaCategoria = new OfertaCategoria();
                        ofertaCategoria.Oferta = oferta;
                        ofertaCategoria.Categoria = categoria;
                        OfertaCategoriaDal.Inserir(ofertaCategoria);
                    }
                }
                else if (oferta.OfertaTipo.OfertaTipoId == 1)
                {
                    foreach (Produto produto in produtos)
                    {
                        OfertaProduto ofertaProduto = new OfertaProduto();
                        ofertaProduto.Produto = produto;
                        ofertaProduto.Oferta = new Oferta();
                        ofertaProduto.Oferta.OfertaId = oferta.OfertaId;

                        OfertaProdutoDal.Inserir(ofertaProduto);
                    }
                }

                scope.Complete();
            }

            return oferta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oferta"></param>
        /// <param name="categorias"></param>
        /// <param name="produtos"></param>
        public Oferta Atualizar(Oferta oferta, List<Categoria> categorias, List<Produto> produtos)
        {
            if (OfertaDal.ValidarOferta(oferta, categorias, produtos) > 0)
            {
                return null;
            }

            using (TransactionScope scope = new TransactionScope())
            {
                if (oferta.OfertaTipo.OfertaTipoId == 3)
                {
                    // Atualização de Categorias
                    // a. Exclui todos os relacionamentos com áreas de conhecimento
                    OfertaCategoriaDal.ExcluirTodosPorOferta(oferta);
                    // b. Inclui os novos relacionamentos
                    foreach (Categoria categoria in categorias)
                    {
                        OfertaCategoria ofertaCategoria = new OfertaCategoria();
                        ofertaCategoria.Oferta = oferta;
                        ofertaCategoria.Categoria = categoria;
                        OfertaCategoriaDal.Inserir(ofertaCategoria);
                    }
                }
                else if (oferta.OfertaTipo.OfertaTipoId == 1)
                {
                    OfertaProdutoDal.ExcluirTodosPorOferta(oferta);

                    foreach (Produto produto in produtos)
                    {
                        OfertaProduto ofertaProduto = new OfertaProduto();
                        ofertaProduto.Produto = produto;
                        ofertaProduto.Oferta = new Oferta();
                        ofertaProduto.Oferta.OfertaId = oferta.OfertaId;

                        OfertaProdutoDal.Inserir(ofertaProduto);
                    }
                }

                OfertaDal.Atualizar(oferta);
                scope.Complete();
            }

            return oferta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oferta"></param>
        public void Excluir(Oferta oferta)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                OfertaCategoriaDal.ExcluirTodosPorOferta(oferta);
                OfertaProdutoDal.ExcluirTodosPorOferta(oferta);

                OfertaDal.Excluir(oferta);

                scope.Complete();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OfertaTipo> CarregarTodasOfertaTipos()
        {
            return OfertaTipoDal.CarregarTodos();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oferta"></param>
        /// <returns></returns>
        public StringBuilder CarregarTodosOfertaCategoriaPorOferta(Oferta oferta)
        {
            return OfertaCategoriaDal.CarregarTodosPorOferta(oferta);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public void AplicarOfertas()
        {
            List<Produto> produtosValorOfertaAlterado = new List<Produto>();

            //Carrega a(s) oferta(s)
            List<Oferta> ofertasAplicaveis = OfertaDal.CarregarOfertasAplicaveis();

            foreach (Oferta oferta in ofertasAplicaveis)
            {
                //Carregar os produto da oferta
                List<Produto> produtos = ProdutoDal.CarregarProdutosPorOferta(oferta);

                foreach (Produto produto in produtos)
                {
                    if (oferta.PrecoOferta != null)
                    {
                        produto.ValorOferta = oferta.PrecoOferta.Value;
                    }
                    else
                    {
                        produto.ValorOferta = produto.ValorUnitario * (1 - (oferta.Percentual.Value / 100));
                    }

                    ProdutoDal.AtualizarValorOferta(produto);

                    produtosValorOfertaAlterado.Add(produto);
                }
            }

            //Atualiza para NULL o valor oferta dos produtos que não foi aplicado algum oferta
            ProdutoDal.AtualizarValorOfertaParaNull(produtosValorOfertaAlterado);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oferta"></param>
        /// <returns></returns>
        public List<OfertaProduto> CarregarProdutosPorOferta(Oferta oferta)
        {
            return OfertaProdutoDal.CarregarProdutosPorOferta(oferta);
        }
    }
}