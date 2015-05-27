using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrupoA.BusinessObject;
using GrupoA.DataAccess;
using GrupoA.DataAccess.ADO;
using GrupoA.BusinessObject.ViewHelper;

namespace GrupoA.BusinessLogicalLayer
{
    /// <summary>
    /// Classe que abstrai as regras de negócio referentes a usuários.
    /// </summary>
    public class ProdutoBLL : BaseBLL
    {
        private IProdutoDAL _produtoDAL;
        private IProdutoImagemDAL _produtoImagemDAL;
        private IProdutoImagemTipoDAL _produtoImagemTipoDAL;

        private IArquivoDAL _arquivoDAL;

        private IArquivoDAL ArquivoDAL
        {
            get
            {
                if (_arquivoDAL == null)
                    _arquivoDAL = new ArquivoADO();
                return _arquivoDAL;
            }
        }

        private IProdutoDAL ProdutoDAL
        {
            get
            {
                if (_produtoDAL == null)
                    _produtoDAL = new ProdutoADO();
                return _produtoDAL;

            }
        }

        private IProdutoImagemDAL ProdutoImagemDAL
        {
            get
            {
                if (_produtoImagemDAL == null)
                    _produtoImagemDAL = new ProdutoImagemADO();
                return _produtoImagemDAL;

            }
        }

        private IProdutoImagemTipoDAL ProdutoImagemTipoDAL
        {
            get
            {
                if (_produtoImagemTipoDAL == null)
                    _produtoImagemTipoDAL = new ProdutoImagemTipoADO();
                return _produtoImagemTipoDAL;

            }
        }

        /// <summary>
        /// Método que carrega um produto
        /// </summary>
        /// <param name="entidade">Produto a ser carregado (somente o identificador é necessário).</param>
        /// <returns>Produto</returns>
        public Produto Carregar(Produto entidade)
        {
            return ProdutoDAL.Carregar(entidade);
        }

        /// <summary>
        /// Método que carrega um produto imagem
        /// </summary>
        /// <param name="entidade">Produto a ser carregado (somente o identificador é necessário).</param>
        /// <returns>Produto</returns>
        public IList<ProdutoImagem> CarregarProdutoImagem(ProdutoImagem entidade)
        {
            return ProdutoImagemDAL.CarregarProdutoImagens(entidade);
        }

        /// <summary>
        /// Método que carrega produtos por nome
        /// </summary>
        /// <param name="entidade">Produtos a serem carregados filtrando por nome.</param>
        /// <param name="qtdLinhas">Quantidade de linhas desejada para retorno.</param>
        /// <returns>IList<AutoCompleteVH></returns>
        public StringBuilder CarregarPorNome(Produto entidade, int qtdLinhas)
        {
            return ProdutoDAL.CarregarPorNome(entidade, qtdLinhas);
        }

        /// <summary>
        /// Método que carrega uma Lista do tipo ProdutoListaVH
        /// </summary>
        /// <param name="entidade">Id da Categoria</param>
        /// <returns>ProdutoListaVH</returns>
        public IList<ProdutoListaVH> CarregarPorCategoria(Categoria entidade)
        {
            // Implementação da Cache
            IList<ProdutoListaVH> produtos;
            string storageKey = string.Format("Carrega_Produtos_Por_Categoria_{0}", entidade.CategoriaId.ToString());
            produtos = Cache.Retrieve<IList<ProdutoListaVH>>(storageKey);
            if (produtos == null)
            {
                produtos = ProdutoDAL.CarregarPorCategoria(entidade);
                Cache.Store(storageKey, produtos, DateTime.Now.AddMinutes(10));
            }
            return produtos;
        }

        /// <summary>
        /// Método que carrega um objeto do tipo ProdutoListaVH
        /// </summary>
        /// <param name="entidade">Id do Produto</param>
        /// <returns>ProdutoListaVH</returns>
        public ProdutoListaVH CarregarProdutoDetalhe(ProdutoListaVH entidade)
        {
            return ProdutoDAL.CarregarProdutoDetalhe(entidade);
        }

        /// <summary>
        /// Método que carrega um objeto do tipo Produto
        /// </summary>
        /// <param name="entidade">Id do Titulo</param>
        /// <returns>ListProduto</returns>
        public List<Produto> CarregarProdutosOutrosFormatos(Titulo entidade)
        {
            return ProdutoDAL.CarregaFormatosDisponiveis(entidade);
        }

        /// <summary>
        /// Método que insere uma lista de produto imagem
        /// </summary>
        /// <param name="entidade">Produto a ser carregado (somente o identificador é necessário).</param>
        public void InserirProdutoImagem(List<ProdutoImagem> entidades)
        {
            foreach (var entidade in entidades)
            {
                ArquivoDAL.Inserir(entidade.Arquivo);
                ProdutoImagemDAL.Inserir(entidade);
            }
        }

        /// <summary>
        /// Método que insere um produto imagem
        /// </summary>
        /// <param name="entidade">Produto a ser carregado (somente o identificador é necessário).</param>
        public ProdutoImagem InserirProdutoImagemArquivo(ProdutoImagem entidade)
        {
            ArquivoDAL.Inserir(entidade.Arquivo);
            ProdutoImagemDAL.Inserir(entidade);
            return entidade;
        }

        /// <summary>
        /// Método que exclui os arquivos relacionados a um determinado produto.
        /// </summary>
        /// <param name="entidade">ProdutoImagem com o id configurado.</param>
        public void ExcluirProdutoImagem(ProdutoImagem entidade)
        {
            ProdutoImagemDAL.Excluir(entidade);
        }

        /// <summary>
        /// Método que exclui um produto imagem
        /// </summary>
        /// <param name="entidade">Produto a ser carregado (somente o identificador é necessário).</param>
        public void ExcluirArquivosProdutoImagem(IList<ProdutoImagem> entidades)
        {
            foreach (var entidade in entidades)
            {
                ProdutoImagemDAL.Excluir(entidade);
                ArquivoDAL.Excluir(entidade.Arquivo);
            }
        }

        public void Atualizar(Produto entidade)
        {
            ProdutoDAL.Atualizar(entidade);
        }


        /// <summary>
        /// Carrega eventos ativos e não expirados com suas dependências.
        /// </summary>
        public List<TituloVH> ConsultaPublicacoesUsuario(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, int usuarioId)
        {
            return ProdutoDAL.ConsultaTodasPublicacoesUsuario(registrosPagina, numeroPagina, ordemColunas, ordemSentidos, null, usuarioId);
        }

        /// <summary>
        /// Carrega eventos ativos e não expirados com suas dependências.
        /// </summary>
        public int ContaPublicacaoUsuario(Int32 usuarioId)
        {
            return ProdutoDAL.ContaPublicacaoUsuario(usuarioId);
        }

        /// <summary>
        /// Carrega eventos ativos e não expirados com suas dependências.
        /// </summary>
        public List<TituloVH> ConsultaTodasBibliotecasUsuario(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, int usuarioId)
        {
            return ProdutoDAL.ConsultaTodasBibliotecasUsuario(registrosPagina, numeroPagina, ordemColunas, ordemSentidos, null, usuarioId);
        }

        /// <summary>
        /// Carrega eventos ativos e não expirados com suas dependências.
        /// </summary>
        public int ContaBibliotecaUsuario(Int32 usuarioId)
        {
            return ProdutoDAL.ContaBibliotecaUsuario(usuarioId);
        }

        /// <summary>
        /// Carrega eventos ativos e não expirados com suas dependências.
        /// </summary>
        public List<TituloVH> ConsultaTodasFavoritosUsuario(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, int usuarioId)
        {
            return ProdutoDAL.ConsultaTodasFavoritosUsuario(registrosPagina, numeroPagina, ordemColunas, ordemSentidos, null, usuarioId);
        }

        /// <summary>
        /// Carrega eventos ativos e não expirados com suas dependências.
        /// </summary>
        public int ContaFavoritosUsuario(Int32 usuarioId)
        {
            return ProdutoDAL.ContaFavoritosUsuario(usuarioId);
        }

        public Produto CarregarComDependencias(Produto entidade)
        {
            return ProdutoDAL.CarregarComDependencias(entidade);
        }

        public void AtualizaHomologado(Produto entidade)
        {
            ProdutoDAL.AtualizaHomologado(entidade);
        }

        public void Inserir(Produto entidade)
        {
            ProdutoDAL.Inserir(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="produtoBO"></param>
        /// <param name="seloBO"></param>
        public void RelacionarProdutoSelo(Produto produtoBO, Selo seloBO)
        {
            ProdutoDAL.RelacionarProdutoSelo(produtoBO, seloBO);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="produtoBO"></param>
        /// <param name="categoriaBO"></param>
        /// <returns></returns>
        public bool ProdutoCategoriaRelacionado(Produto produtoBO, Categoria categoriaBO)
        {
            return ProdutoDAL.ProdutoCategoriaRelacionado(produtoBO, categoriaBO);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="produtoBO"></param>
        /// <param name="categoriaBO"></param>
        public void RelacionarProdutoCategoria(Produto produtoBO, Categoria categoriaBO)
        {
            ProdutoDAL.RelacionarProdutoCategoria(produtoBO, categoriaBO);
        }

        /// <summary>
        /// Atualiza tudo, menos o nome do produto
        /// </summary>
        /// <param name="entidade"></param>
        public void AtualizarMenosNome(Produto entidade)
        {
            ProdutoDAL.AtualizarMenosNome(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public List<Produto> CarregarProdutosPorNome(Produto entidade, Oferta oferta)
        {
            return ProdutoDAL.CarregarProdutosPorNome(entidade, oferta);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="autorId"></param>
        /// <returns></returns>
        public List<TituloVH> CarregarUltimosProdutosPublicadosPorAutor(int autorId)
        {
            return ProdutoDAL.CarregarUltimosProdutosPublicadosPorAutor(autorId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="produtoBO"></param>
        /// <param name="arquivoBO"></param>
        /// <returns></returns>
        public bool ProdutoArquivoRelacionado(Produto produtoBO, Arquivo arquivoBO)
        {
            return ProdutoDAL.ProdutoArquivoRelacionado(produtoBO, arquivoBO);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="produtoId"></param>
        /// <returns></returns>
        public List<TituloVH> CarregarProdutosSugeridos(Int32 produtoId)
        {
            Produto produtoBO = new Produto() { ProdutoId = produtoId };

            List<Produto> produtoBOList = new List<Produto>();
            produtoBOList.Add(produtoBO);

            return this.CarregarProdutosSugeridos(produtoBOList, 3);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="produtoBOList"></param>
        /// <returns></returns>
        public List<TituloVH> CarregarProdutosSugeridos(List<Produto> produtoBOList, int quantidade)
        {
            return ProdutoDAL.CarregarProdutosSugeridos(produtoBOList, quantidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public Produto CarregarDetalheLivro(Produto entidade)
        {
            return ProdutoDAL.CarregarDetalheLivro(entidade);
        }

        public List<Selo> CarregarSelos(Produto entidade)
        {
            return ProdutoDAL.CarregarSelos(entidade);
        }

        public void ExcluirProdutoCategoria(Produto entidade)
        {
            ProdutoDAL.ExcluirProdutoCategoria(entidade);
        }

        /// <summary>
        /// Exclui os relacionamentos de produto com o selo
        /// </summary>
        /// <param name="entidade"></param>
        public void ExcluirProdutoSelo(Produto entidade)
        {
            ProdutoDAL.ExcluirProdutoSelo(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public List<Produto> CarregarProdutosPorNome(Produto entidade)
        {
            return ProdutoDAL.CarregarProdutosPorNome(entidade);
        }

        /// <summary>
        /// Método que carrega uma coleção de produtos conforme o código ISBN13 recebido excluindo a coleção de produtos recebida como produto.
        /// </summary>
        /// <param name="isbn13">Código ISBN13 do título.</param>
        /// <param name="produtos">Coleção de produtos que não deve ser retornada.</param>
        /// <returns>Coleção de produtos conforme o código ISBN13 recebido.</returns>
        public List<Produto> CarregarProdutosPorIsbn13ExcetoProdutos(string isbn13, List<Produto> produtos)
        {
            return ProdutoDAL.CarregarTodosPorIsbn13ExcetoProdutos(isbn13, produtos);
        }

        public List<Produto> Carregar(RevistaPacote entidade)
        {
            return ProdutoDAL.Carregar(entidade);
        }
    }
}