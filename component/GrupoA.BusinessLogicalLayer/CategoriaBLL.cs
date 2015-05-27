using System;
using System.Collections.Generic;
using System.Linq;
using GrupoA.BusinessObject;
using GrupoA.DataAccess;
using GrupoA.DataAccess.ADO;
using GrupoA.FilterHelper;

namespace GrupoA.BusinessLogicalLayer
{
    public class CategoriaBLL : BaseBLL
    {
        private ICategoriaDAL _categoriaDal;
        public ICategoriaDAL CategoriaDal
        {
            get { return _categoriaDal ?? (_categoriaDal = new CategoriaADO()); }
        }

        public List<CategoriaVH> CarregarSubMenu(int categoriaId)
        {
            return CategoriaDal.CarregarSubMenuPorIdentificador(categoriaId);
        }

        /// <summary>
        /// Carrega Categoria
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public Categoria Carregar(Categoria entidade)
        {
            return CategoriaDal.Carregar(entidade);
        }

        /// <summary>
        /// Carrega Categoria
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public IEnumerable<Categoria> Carregar(Produto entidade)
        {
            return CategoriaDal.Carregar(entidade);
        }

        /// <summary>
        /// Carrega Categoria por produto
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public List<Categoria> CarregarCategoriaPorProduto(int produtoId)
        {
            return CategoriaDal.CarregarCategoriaPorProduto(produtoId);
        }

        /// <summary>
        /// Método que retorna todas as Categorias que sejam Áreas de Conhecimento (Categorias Base).
        /// </summary>
        /// <returns>Coleção de categorias base.</returns>
        public List<Categoria> CarregarCategorias()
        {
            return CategoriaDal.CarregarTodosBase().ToList();
        }

        /// <summary>
        /// Método que retorna todas as Categorias que estejam ligadas Áreas de Conhecimento (Categorias Base).
        /// ATENÇÃO: Não carrega as categorias base.
        /// </summary>
        /// <returns>Coleção de categorias filhas.</returns>
        public List<Categoria> CarregarCategoriasFilhas(Categoria categoria)
        {
            CategoriaFH fh = new CategoriaFH { CategoriaIdPai = categoria.CategoriaId.ToString() };
            return CategoriaDal.CarregarTodos(0, 0, null, null, fh).ToList();
        }

        public List<Categoria> CarregarAreaDeInteresseUsuario(Usuario usuario)
        {
            return CategoriaDal.CarregarAreaDeInteresseUsuario(usuario).ToList();
        }

        public Categoria CarregarAreaPorCategoria(Categoria categoria)
        {
            return CategoriaDal.CarregarCategoriaMestre(categoria);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public Categoria CarregarPorCodigoLegado(Categoria entidade)
        {
            return CategoriaDal.CarregarPorCodigoLegado(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void Inserir(Categoria entidade)
        {
            CategoriaDal.Inserir(entidade);
        }

        public List<Categoria> CarregarCategoriasComFilhos()
        {
            IEnumerable<Categoria> categoriasPai = CategoriaDal.CarregarTodosBase();
            List<Categoria> categorias = CategoriaDal.CarregarTodasCategorias();

            List<Categoria> novaLista = new List<Categoria>();

            foreach (var c in categoriasPai)
            {
                c.Categorias = new List<Categoria>();
                c.Categorias = (from cat in categorias.Where(o => o.CategoriaPai != null && o.CategoriaPai.CategoriaId == c.CategoriaId)
                                select cat).OrderBy(p => p.NomeCategoria).ToList<Categoria>();

                foreach (var cFilho in c.Categorias)
                {
                    cFilho.Categorias = new List<Categoria>();
                    cFilho.Categorias = (from cat in categorias.Where(o => o.CategoriaPai != null && o.CategoriaPai.CategoriaId == cFilho.CategoriaId)
                                         select cat).OrderBy(p => p.NomeCategoria).ToList<Categoria>();
                }

                novaLista.Add(c);
            }

            return novaLista;
        }

        public Categoria CarregarCategoriaMestre(Int64 produtoId)
        {
            return CategoriaDal.CarregarCategoriaMestre(produtoId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public List<Categoria> CarregarTodasSubCategorias(Categoria entidade)
        {
            List<Categoria> categoriaBOList = new List<Categoria>();

            List<CategoriaVH> categoriaVHList = CategoriaDal.CarregarTodasSubCategorias(entidade);
            List<CategoriaVH> categoriaVHListTemp = new List<CategoriaVH>();            

            categoriaVHListTemp = (from CategoriaVH categoriaVH in categoriaVHList
                                    where categoriaVH.CategoriaPai.CategoriaId == entidade.CategoriaId
                                    select categoriaVH).ToList();

            if (categoriaVHListTemp != null && categoriaVHListTemp.Any())
            {
                foreach (CategoriaVH categoriaVHTemp in categoriaVHListTemp)
                {
                    Categoria categoriaBO = new Categoria();
                    categoriaBO.CategoriaId = categoriaVHTemp.CategoriaId;
                    categoriaBO.NomeCategoria = categoriaVHTemp.NomeCategoria;
                    categoriaBO.CategoriaPai = categoriaVHTemp.CategoriaPai;
                    categoriaBO.CategoriaId = categoriaVHTemp.CategoriaId;

                    categoriaBOList.Add(categoriaBO);
                }
            }

            if (categoriaBOList != null && categoriaBOList.Any())
            {
                int i = 0;

                foreach (Categoria categoriaBOTemp in categoriaBOList)
                {
                    categoriaBOList[i].Categorias = new List<Categoria>();

                    categoriaVHListTemp = (from CategoriaVH categoriaVH in categoriaVHList
                                           where categoriaVH.CategoriaPai.CategoriaId == categoriaBOTemp.CategoriaId
                                           select categoriaVH).ToList();

                    if (categoriaVHListTemp != null && categoriaVHListTemp.Any())
                    {
                        foreach (CategoriaVH categoriaVHTemp in categoriaVHListTemp)
                        {
                            Categoria categoriaBO = new Categoria();
                            categoriaBO.CategoriaId = categoriaVHTemp.CategoriaId;
                            categoriaBO.NomeCategoria = categoriaVHTemp.NomeCategoria;
                            categoriaBO.CategoriaPai = categoriaVHTemp.CategoriaPai;
                            categoriaBO.CategoriaId = categoriaVHTemp.CategoriaId;

                            categoriaBOList[i].Categorias.Add(categoriaBO);
                        }
                    }

                    i++;
                }
            }

            return categoriaBOList;
        }
    }
}