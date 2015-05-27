using System;
using System.Text;
using System.Collections.Generic;
using GrupoA.BusinessObject;
using GrupoA.FilterHelper;

namespace GrupoA.DataAccess
{
    public partial interface ICategoriaDAL
    {
        IEnumerable<Categoria> CarregarTodosBase();
        void ExcluirPorConteudo(Conteudo entidade);
        List<Categoria> CarregarPorPromocao(Promocao promocao);
        List<CategoriaVH> CarregarSubMenuPorIdentificador(int categoriaId);
        IEnumerable<Categoria> CarregarAreaDeInteresseUsuario(Usuario entidade);

        /// <summary>
        /// Metodo que retorna a categoria (mestre) de nível mais alto de uma determinada categoria.
        /// </summary>
        /// <param name="categoria">Categoria a ser pesquisada.</param>
        /// <returns></returns>
        Categoria CarregarCategoriaMestre(Categoria categoria);
        Categoria CarregarCategoriaMestre(Int64 produtoId);

        List<Categoria> CarregarCategoriasDoSegundoNivel();
        List<Categoria> CarregarCategoriaPorProduto(int produtoId);
        List<Categoria> CarregarCategoriasFilhas(Categoria categoria, bool incluirCategoriaInformada);
        Categoria CarregarPorCodigoLegado(Categoria entidade);
        List<Categoria> CarregarTodasCategorias();
        List<CategoriaVH> CarregarTodasSubCategorias(Categoria entidade);
    }
}