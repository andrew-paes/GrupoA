using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Transactions;
using GrupoA.BusinessLogicalLayer.Helper;
using GrupoA.BusinessObject;
using GrupoA.DataAccess;
using GrupoA.DataAccess.ADO;
using GrupoA.FilterHelper;
using Perfil = GrupoA.BusinessObject.Perfil;
using GrupoA.BusinessObject.Enumerator;

namespace GrupoA.BusinessLogicalLayer
{
    /// <summary>
    /// Classe que contém os métodos de negócio para utilização de Eventos e suas dependências.
    /// </summary>
    public class PromocaoBLL : BaseBLL
    {
        #region Declarações DAL

        private IPromocaoDAL _promocaoDal;
        private IPromocaoDAL PromocaoDal
        {
            get { return _promocaoDal ?? (_promocaoDal = new PromocaoADO()); }
        }

        private IPromocaoCupomDAL _promocaoCupomDal;
        private IPromocaoCupomDAL PromocaoCupomDal
        {
            get { return _promocaoCupomDal ?? (_promocaoCupomDal = new PromocaoCupomADO()); }
        }

        private IPromocaoCupomPedidoDAL _promocaoCupomPedidoDal;
        private IPromocaoCupomPedidoDAL PromocaoCupomPedidoDal
        {
            get { return _promocaoCupomPedidoDal ?? (_promocaoCupomPedidoDal = new PromocaoCupomPedidoADO()); }
        }

        private IPromocaoTipoDAL _promocaoTipoDal;
        private IPromocaoTipoDAL PromocaoTipoDal
        {
            get { return _promocaoTipoDal ?? (_promocaoTipoDal = new PromocaoTipoADO()); }
        }

        private IUsuarioDAL _usuarioDal;
        private IUsuarioDAL UsuarioDal
        {
            get { return _usuarioDal ?? (_usuarioDal = new UsuarioADO()); }
        }

        private IPerfilDAL _perfilDal;
        private IPerfilDAL PerfilDal
        {
            get { return _perfilDal ?? (_perfilDal = new PerfilADO()); }
        }

        private IRevistaDAL _revistaDal;
        private IRevistaDAL RevistaDal
        {
            get { return _revistaDal ?? (_revistaDal = new RevistaADO()); }
        }

        private IProdutoDAL _produtoDal;
        private IProdutoDAL ProdutoDal
        {
            get { return _produtoDal ?? (_produtoDal = new ProdutoADO()); }
        }

        private IProdutoTipoDAL _produtoTipoDal;
        private IProdutoTipoDAL ProdutoTipoDal
        {
            get { return _produtoTipoDal ?? (_produtoTipoDal = new ProdutoTipoADO()); }
        }

        private ICategoriaDAL _categoriaDal;
        private ICategoriaDAL CategoriaDal
        {
            get { return _categoriaDal ?? (_categoriaDal = new CategoriaADO()); }
        }

        private IPromocaoFaixaDAL _promocaoFaixaDal;
        private IPromocaoFaixaDAL PromocaoFaixaDal
        {
            get { return _promocaoFaixaDal ?? (_promocaoFaixaDal = new PromocaoFaixaADO()); }
        }

        #endregion

        /// <summary>
        /// Insere uma nova Promoção.
        /// </summary>
        /// <param name="promocao">Objeto Promocao a ser inserido.</param>
        public void InserirPromocao(Promocao promocao)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                PromocaoDal.Inserir(promocao);

                PromocaoCupomDal.Inserir(promocao);

                if (promocao.PromocaoFaixas != null)
                {
                    promocao.PromocaoFaixas.ForEach(InserirFaixaDaPromocao);
                }
                scope.Complete();
            }
        }

        /// <summary>
        /// Carrega uma Promoção e seu Tipo
        /// </summary>
        /// <param name="promocao">Objeto Promocao a ser carregada (somente o código identificador é utilizado).</param>
        /// <returns>Objeto Promocao populado com os dados da promoção e o tipo de promoção.</returns>
        public Promocao CarregarPromocaoComTipo(Promocao promocao)
        {
            promocao = PromocaoDal.Carregar(promocao);

            PromocaoCupom promocaoCupom = PromocaoCupomDal.CarregarPorPromocao(promocao);

            if (promocaoCupom != null)
            {
                promocao.Reutilizavel = promocaoCupom.Reutilizavel;
            }
            else
            {
                promocao.Reutilizavel = false;
            }

            promocao.PromocaoTipo = PromocaoTipoDal.Carregar(new PromocaoTipo() { PromocaoTipoId = promocao.PromocaoTipo.PromocaoTipoId });
            return promocao;
        }

        /// <summary>
        /// Carrega uma Promoção.
        /// </summary>
        /// <param name="promocao">Promoção a ser carregada com identificador configurado .</param>
        /// <returns>Promoção com seus dados.</returns>
        public Promocao CarregarPromocao(Promocao promocao)
        {
            promocao = PromocaoDal.Carregar(promocao);
            return promocao;
        }

        /// <summary>
        /// Atualiza as informações da Promoção.
        /// </summary>
        /// <param name="promocao">Objeto Promocao contendo as informações a serem atualizadas.</param>
        public void AtualizarPromocao(Promocao promocao)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (promocao.NumeroMaximoCupomDif > 0)
                {
                    PromocaoCupomDal.Inserir(promocao);
                }

                PromocaoDal.Atualizar(promocao);
                scope.Complete();
            }
        }

        /// <summary>
        /// Insere uma nova faixa de promoção
        /// </summary>
        /// <param name="faixa">Objeto PromocaoFaixa a ser inserido.</param>
        public void InserirFaixaDaPromocao(PromocaoFaixa faixa)
        {
            PromocaoFaixaDal.Inserir(faixa);
        }

        /// <summary>
        /// Exclui todas faixas uma promoção.
        /// </summary>
        /// <param name="entidade">Objeto Promocao a ser excluído.</param>
        public void ExcluirTodasFaixasDaPromocao(Promocao entidade)
        {
            PromocaoFaixaDal.ExcluirTodasFaixasDaPromocao(entidade);
        }

        /// <summary>
        /// Exclui uma promoção completa.
        /// </summary>
        /// <param name="entidade">Promoção a ser excluída.</param>
        public void ExcluirPromocao(Promocao entidade)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                PromocaoFaixaDal.ExcluirTodasFaixasDaPromocao(entidade);

                PromocaoDal.ExcluirCategoriasPorPromocao(entidade);

                PromocaoDal.ExcluirPerfisPorPromocao(entidade);

                PromocaoDal.ExcluirProdutosPorPromocao(entidade);

                PromocaoDal.ExcluirProdutoTiposPorPromocao(entidade);

                PromocaoDal.ExcluirUsuariosPorPromocao(entidade);

                PromocaoDal.ExcluirCuponsPorPromocao(entidade);

                PromocaoDal.Excluir(entidade);

                transactionScope.Complete();
            }
        }

        /// <summary>
        /// Exclui uma faixa de promoção.
        /// </summary>
        /// <param name="faixa">Faixa de promoção a ser excluída.</param>
        public void ExcluirFaixaDaPromocao(PromocaoFaixa faixa)
        {
            PromocaoFaixaDal.Excluir(faixa);
        }

        /// <summary>
        /// Carrega todas as faixas de uma determinada Promoção.
        /// </summary>
        /// <param name="promocao">Promoção com identificador configurado.</param>
        /// <returns>Coleção de Faixas de uma Promoção</returns>
        public List<PromocaoFaixa> CarregarFaixasDaPromocao(Promocao promocao)
        {
            PromocaoFaixaFH fh = new PromocaoFaixaFH { PromocaoId = promocao.PromocaoId.ToString() };
            return PromocaoFaixaDal.CarregarTodos(0, 0, null, null, fh).ToList();
        }

        /// <summary>
        /// Verifica a existência de uma faixa de valor contendo um mesmo valor mínimo ou percentual de desconto.
        /// </summary>
        /// <param name="promocaoFaixa">Objeto PromocaoFaixa que contém os valores a serem validados.</param>
        /// <returns>Existência ou não de objetos contendo as mesmas informações.</returns>
        public bool ExistePromocaoFaixa(PromocaoFaixa promocaoFaixa)
        {
            bool testeValorMinimo = true;
            bool testePercentualDesconto = true;

            // Teste do Valor Mínimo
            PromocaoFaixaFH fh = new PromocaoFaixaFH();
            fh.ValorMinimo = promocaoFaixa.ValorMinimo.ToString();
            fh.PromocaoId = promocaoFaixa.Promocao.PromocaoId.ToString();
            int total = PromocaoFaixaDal.TotalRegistros();
            if (PromocaoFaixaDal.TotalRegistros(fh) > 0)
                testeValorMinimo = false;

            // Teste do Percentual de Desconto
            if (promocaoFaixa.PercentualDesconto != null)
            {
                fh = new PromocaoFaixaFH();
                fh.PercentualDesconto = promocaoFaixa.PercentualDesconto.ToString();
                fh.PromocaoId = promocaoFaixa.Promocao.PromocaoId.ToString();
                total = PromocaoFaixaDal.TotalRegistros(fh);
                if (PromocaoFaixaDal.TotalRegistros(fh) > 0)
                    testePercentualDesconto = false;
            }

            // Decisão de mensagens
            if (testeValorMinimo && testePercentualDesconto)
                return true;
            else if (!testeValorMinimo && testePercentualDesconto)
                throw new Exception(string.Format("Já existe uma faixa com o valor mínimo ({0}) informado.", promocaoFaixa.ValorMinimo.ToString()));
            else if (testeValorMinimo && !testePercentualDesconto)
                throw new Exception(string.Format("Já existe uma faixa com o percentual de desconto ({0}) informado.", promocaoFaixa.PercentualDesconto.ToString()));
            else
                throw new Exception(string.Format("Já existe uma faixa com o valor mínimo ({0}) e percentual de desconto ({1}) informados.", promocaoFaixa.ValorMinimo.ToString(), promocaoFaixa.PercentualDesconto.ToString()));
        }

        /// <summary>
        /// Carrega os Tipos de Promoção existentes.
        /// </summary>
        /// <returns>Coleção de Tipos de Promoção</returns>
        public List<PromocaoTipo> CarregarTiposDePromocoes()
        {
            return PromocaoTipoDal.CarregarTodos().ToList();
        }

        /// <summary>
        /// Carrega uma Promoção a partir do seu código.
        /// </summary>
        /// <returns>Promoção com suas informações configuradas.</returns>
        public Promocao CarregarPromocaoPeloCodigo(string codigoDaPromocao)
        {
            List<Promocao> promocoes = this.CarregarPromocoesAutomaticas(null, null, null, null, null, codigoDaPromocao, false);

            if (promocoes.Count() > 0)
            {
                return promocoes[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Insere um novo registro ligando uma Promoção a um Usuário
        /// </summary>
        /// <param name="promocao">Promoção que deverá ser ligada ao Usuário (somente o identificador é utilizado usuarioId)</param>
        /// <param name="usuario">Usuário que deverá ser ligado a Promoção (somente o identificador é utilizado promocaoId)</param>
        public void IncluirRestricaoPorUsuario(Promocao promocao, Usuario usuario)
        {
            PromocaoDal.InserirPromocaoUsuario(promocao, usuario);
        }

        /// <summary>
        /// Carrega os Usuários de uma Promoção.
        /// </summary>
        /// <param name="promocao">Objeto Promoção a serem carregados os Usuários (somente o código identificador é utilizado)</param>
        /// <returns>Coleção de Usuários ligado à uma Promoção.</returns>
        public List<Usuario> CarregarUsuariosPorPromocao(Promocao promocao)
        {
            return UsuarioDal.CarregarUsuariosPorPromocao(promocao);
        }

        /// <summary>
        /// Exclui uma restrição de Usuário de uma Promoção.
        /// </summary>
        /// <param name="promocao">Promoção a ser removida da ligação com Usuários.</param>
        /// <param name="usuario">Usuário a ser removido da ligação com Promoção.</param>
        public void ExcluirRestricaoPorUsuario(Promocao promocao, Usuario usuario)
        {
            PromocaoDal.ExcluirPromocaoUsuario(promocao, usuario);
        }

        /// <summary>
        /// Método que carrega uma colecao de usuarios por cadastro pessoa (CPF/CNPJ) com exceção da listagem de usuários recebida por parâmetro.
        /// </summary>
        /// <param name="usuario">Objeto Usuario que contém o campo tipo pessoa a ser pesquisado.</param>
        /// <param name="usuarios">Listagem de Usuários (somente o identificador é necessário) que não deverão ser buscados.</param>
        /// <returns>Coleção de Usuários conforme o campo cadastro pessoa</returns>
        public List<Usuario> CarregarTodosUsuariosPorCadastroPessoaExcetoUsuarios(Usuario usuario, List<Usuario> usuarios)
        {
            return UsuarioDal.CarregarTodosPorCadastroPessoaUsuariosExcetoUsuarios(usuario, usuarios);
        }

        /// <summary>
        /// Exclui todos os relacionamentos entre Usuários e Promoção através do código identificador de uma Promoção
        /// </summary>
        /// <param name="promocao">Promoção que deverá ser ligada ao Usuário (somente o identificador é utilizado usuarioId)</param>
        public void ExcluirTodasRestricoesPorUsuario(Promocao promocao)
        {
            PromocaoDal.ExcluirUsuariosPorPromocao(promocao);
        }

        /// <summary>
        /// Insere um novo relacionamento entre uma Promoção e um Perfil
        /// </summary>
        /// <param name="promocao">Promoção a ser relacionada com Peril (somente o identificador é utilizado promocaoId)</param>
        /// <param name="perfil">Perfil a ser relacionado com a Promoção (somente o identificador é utilizado perfilId)</param>
        public void IncluirRestricaoPorPerfil(Promocao promocao, Perfil perfil)
        {
            PromocaoDal.InserirPromocaoPerfil(promocao, perfil);
        }

        /// <summary>
        /// Carrega todos os Perfis filtrados pelo código identificador da Promoção
        /// </summary>
        /// <param name="promocao">Objeto Promoção que contém o identificador promocaoId</param>
        /// <returns>Listagem de Perfis da Promoção</returns>
        public List<Perfil> CarregarPerfisPorPromocao(Promocao promocao)
        {
            return PerfilDal.CarregarTodosPorPromocao(promocao);
        }

        /// <summary>
        /// Exclui um relacionamento entre uma Promoção e um Perfil.
        /// </summary>
        /// <param name="promocao">Promoção a ser excluído o relacionamento com Peril (somente o identificador é utilizado promocaoId)</param>
        /// <param name="perfil">Perfil a ser excluído o relacionamento com a Promoção (somente o identificador é utilizado perfilId)</param>
        public void ExcluirRestricaoPorPerfil(Promocao promocao, Perfil perfil)
        {
            PromocaoDal.ExcluirPromocaoPerfil(promocao, perfil);
        }

        /// <summary>
        /// Método que carrega todos os Perfis exceto os Perfis recebidos por parâmetro.
        /// </summary>
        /// <param name="perfis">Coleção de Perfis que NÃO deve ser carregado.</param>
        /// <returns>Coleção de Perfis que deve ser carregado.</returns>
        public List<Perfil> CarregarTodosPerfisExcetoPerfis(List<Perfil> perfis)
        {
            return PerfilDal.CarregarTodosExcetoPerfis(perfis);
        }

        /// <summary>
        /// Exclui todos os relacionamentos de uma Promoção
        /// </summary>
        /// <param name="promocao">Promoção a ser excluído o relacionamento com Peril (somente o identificador é utilizado promocaoId)</param>
        public void ExcluirTodasRestricoesPorPerfil(Promocao promocao)
        {
            PromocaoDal.ExcluirPerfisPorPromocao(promocao);
        }

        /// <summary>
        /// Insere um novo relacionamento entre uma Promoção e um Produto
        /// </summary>
        /// <param name="promocao">Promoção a ser relacionada com Produto (somente o identificador é utilizado promocaoId)</param>
        /// <param name="produto">Produto a ser relacionado com a Promoção (somente o identificador é utilizado produtoId)</param>
        public void IncluirRestricaoPorProduto(Promocao promocao, Produto produto)
        {
            PromocaoDal.InserirPromocaoProduto(promocao, produto);
        }

        /// <summary>
        /// Carrega todos os Produtos de uma Promoção conforme o código identificador da Promoção recebido.
        /// </summary>
        /// <param name="promocao">Objeto Promocao que contém o código identificador da Promoção (promocaoId)</param>
        /// <returns>Coleção de Produtos da Promoção</returns>
        public List<Produto> CarregarProdutosPorPromocao(Promocao promocao)
        {
            return ProdutoDal.CarregarPorPromocao(promocao);
        }

        /// <summary>
        /// Exclui o relacionamento entre uma promoção e um Produto
        /// </summary>
        /// <param name="promocao">Promoção a ser excluído o relacionamento com Produto (somente o identificador é utilizado promocaoId)</param>
        /// <param name="produto">Produto a ser excluído o relacionamento com Promoção (somente o identificador é utilizado produtoId)</param>
        public void ExcluirRestricaoPorProduto(Promocao promocao, Produto produto)
        {
            PromocaoDal.ExcluirPromocaoProduto(promocao, produto);
        }

        /// <summary>
        /// Método que carrega uma coleção de produtos conforme o código EAN13 recebido excluindo a coleção de produtos recebida como produto.
        /// </summary>
        /// <param name="produto">Produto que contém o código EAN13 desejado.</param>
        /// <param name="produtos">Coleção de produtos que não deve ser retornada.</param>
        /// <returns>Coleção de produtos conforme o código EAN13 recebido.</returns>
        public List<Produto> CarregarTodosProdutosPorCadastroPessoaExcetoProdutos(Produto produto, List<Produto> produtos)
        {
            return ProdutoDal.CarregarTodosPorEANExcetoProdutos(produto, produtos);
        }

        /// <summary>
        /// Método que carrega uma coleção de produtos conforme o código ISBN13 recebido excluindo a coleção de produtos recebida como produto.
        /// </summary>
        /// <param name="isbn13">Código ISBN13 do título.</param>
        /// <param name="produtos">Coleção de produtos que não deve ser retornada.</param>
        /// <returns>Coleção de produtos conforme o código ISBN13 recebido.</returns>
        public List<Produto> CarregarProdutosPorIsbn13ExcetoProdutos(string isbn13, List<Produto> produtos)
        {
            return ProdutoDal.CarregarTodosPorIsbn13ExcetoProdutos(isbn13, produtos);
        }

        /// <summary>
        /// Exclui todos os relacionamentos de uma Promoção
        /// </summary>
        /// <param name="promocao">Promoção a ser excluído o relacionamento com Produto (somente o identificador é utilizado promocaoId)</param>
        public void ExcluirTodasRestricoesPorProduto(Promocao promocao)
        {
            PromocaoDal.ExcluirProdutosPorPromocao(promocao);
        }

        /// <summary>
        /// Insere um relacionamento entre uma Promoção e um Tipo de Produto
        /// </summary>
        /// <param name="promocao">Promoção a ser relacionada com Tipo de Produto (somente o identificador é utilizado promocaoId)</param>
        /// <param name="produtoTipo">Tipo de Produto a ser relacionado com Promoção (somente o identificador é utilizado produtoTipoId)</param>
        public void IncluirRestricaoPorTipoDeProduto(Promocao promocao, ProdutoTipo produtoTipo)
        {
            PromocaoDal.InserirPromocaoProdutoTipo(promocao, produtoTipo);
        }

        /// <summary>
        /// Carrega uma coleção de Tipos de Produto por Promoção conforme o código identificador recebido.
        /// </summary>
        /// <param name="promocao">Objeto Promocao que deverá ser carregado os Tipos de Produto (somente o código identificador é utilizado).</param>
        /// <returns>Coleção de Tipos de Produto da Promoção</returns>
        public List<ProdutoTipo> CarregarTiposDeProdutoPorPromocao(Promocao promocao)
        {
            return ProdutoTipoDal.CarregarPorPromocao(promocao);
        }

        /// <summary>
        /// Exclui um relacionamento entre uma Promoção e um Tipo de Produto.
        /// </summary>
        /// <param name="promocao">Promoção a ser excluído o relacionamento com Tipo de Produto (somente o identificador é utilizado promocaoId)</param>
        /// <param name="produtoTipo">Tipo de Produto a ser excluído o relacionamento com Promoção (somente o identificador é utilizado produtoTipoId)</param>
        public void ExcluirRestricaoPorTipoDeProduto(Promocao promocao, ProdutoTipo produtoTipo)
        {
            PromocaoDal.ExcluirPromocaoProdutoTipo(promocao, produtoTipo);
        }

        /// <summary>
        /// Método que carrega os Tipos de Produto excluindo os tipos de produto recebidos por parâmetro.
        /// </summary>
        /// <param name="produtoTipos">Tipos de Produto que não devem ser buscados (somente o identificador é necessário).</param>
        /// <returns>Coleção de Tipos de Produto</returns>
        public List<ProdutoTipo> CarregarTodosProdutoTiposExcetoProdutoTipos(List<ProdutoTipo> produtoTipos)
        {
            return ProdutoTipoDal.CarregarTodosExcetoProdutoTipos(produtoTipos);
        }

        /// <summary>
        /// Exclui todos os relacionamentos de uma Promoção com os Tipos de Produto
        /// </summary>
        /// <param name="promocao">Promoção a ser excluído o relacionamento com Tipo de Produto (somente o identificador é utilizado promocaoId)</param>
        public void ExcluirRestricoesPorTipoDeProduto(Promocao promocao)
        {
            PromocaoDal.ExcluirProdutoTiposPorPromocao(promocao);
        }

        /// <summary>
        /// Insere um novo relacionamento entre uma Promoção e uma Categoria
        /// </summary>
        /// <param name="promocao">Promoção a ser relacionada com Categoria (somente o identificador é utilizado promocaoId)</param>
        /// <param name="categoria">Categoria a ser relacionada com Promoção (somente o identificador é utilizado categoriaId)</param>
        public void IncluirRestricaoPorCategoria(Promocao promocao, Categoria categoria)
        {
            PromocaoDal.InserirPromocaoCategoria(promocao, categoria);
        }

        /// <summary>
        /// Carrega todas as Categorias de uma Promoção
        /// </summary>
        /// <param name="promocao">Objeto Promoção a serem carregadas as Categorias.</param>
        /// <returns>Coleção de Categorias que fazem parte de uma Promoção.</returns>
        public List<Categoria> CarregarCategoriasPorPromocao(Promocao promocao)
        {
            return CategoriaDal.CarregarPorPromocao(promocao);
        }

        /// <summary>
        /// Exclui todos os relacionamentos entre Categorias e Promoção conforme o código identificador da Promoção
        /// </summary>
        /// <param name="promocao">Promoção a ser relacionada com Categoria (somente o identificador é utilizado promocaoId)</param>
        public void ExcluirTodasRestricoesPorCategoria(Promocao promocao)
        {
            PromocaoDal.ExcluirCategoriasPorPromocao(promocao);
        }

        /// <summary>
        /// Método que recebe um carrinho e valida as promoções automáticas.
        /// </summary>
        /// <param name="carrinhoDeCompras">Carrinho com seus itens.</param>
        /// <param name="perfilDoUsuario">Perfil com identificador do usuário logado, se existir.</param>
        /// <param name="usuario">Usuario com identificador configurado, se existir.</param>
        /// <returns>Carrinho com itens contendo as promoções automáticas.</returns>
        public Carrinho AplicarPromocaoAutomaticaAoCarrinho(Usuario usuario, Perfil perfilDoUsuario, Carrinho carrinhoDeCompras)
        {
            List<Produto> produtos = carrinhoDeCompras.CarrinhoItens.Select(item => item.Produto).ToList();
            List<Categoria> categorias = produtos.SelectMany(produto => produto.Categorias, (produto, categoria) => categoria).Distinct().ToList();
            List<ProdutoTipo> tiposDeProdutos = produtos.Select(produto => produto.ProdutoTipo).Distinct().ToList();

            List<Promocao> promocoes = CarregarPromocoesAutomaticas(usuario,
                                                       perfilDoUsuario,
                                                       categorias, produtos,
                                                       tiposDeProdutos);

            // TODO: aplicar a promoção mais vantajosa.

            return null;
        }

        /// <summary>
        /// Carrega as promoções automáticas conforme as restrições passadas.
        /// </summary>
        /// <param name="usuario">Usuario para validação da restrição de promoção.</param>
        /// <param name="perfilDoUsuario">Perfil do usuário para validação da restrição de promoção.</param>
        /// <param name="categorias">Categorias para validação da restrição de promoção.</param>
        /// <param name="produtos">Produtos para validação da restrição de promoção.</param>
        /// <param name="tiposDeProdutos">Tipos de produtso para validação da restrição de promoção.</param>
        /// <returns></returns>
        public List<Promocao> CarregarPromocoesAutomaticas(Usuario usuario, Perfil perfilDoUsuario, List<Categoria> categorias, List<Produto> produtos, List<ProdutoTipo> tiposDeProdutos)
        {
            return this.CarregarPromocoesAutomaticas(usuario, perfilDoUsuario, categorias, produtos, tiposDeProdutos, null, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="perfilDoUsuario"></param>
        /// <param name="categorias"></param>
        /// <param name="produtos"></param>
        /// <param name="tiposDeProdutos"></param>
        /// <param name="codigoPromocao"></param>
        /// <param name="valida"></param>
        /// <returns></returns>
        public List<Promocao> CarregarPromocoesAutomaticas(Usuario usuario, Perfil perfilDoUsuario, List<Categoria> categorias, List<Produto> produtos, List<ProdutoTipo> tiposDeProdutos, String codigoPromocao, Boolean valida)
        {
            List<Promocao> promocoesAutomaticas = PromocaoDal.CarregarPromocoesAutomaticasValidas(usuario, perfilDoUsuario, categorias,
                                                                                                  produtos, tiposDeProdutos, codigoPromocao, valida).ToList();
            return promocoesAutomaticas;
        }

        /// <summary>
        /// Carrega as promoções automáticas conforme as restrições passadas.
        /// </summary>
        /// <param name="usuario">Usuario para validação da restrição de promoção.</param>
        /// <param name="perfilDoUsuario">Perfil do usuário para validação da restrição de promoção.</param>
        /// <param name="carrinho">Carrinho para validação da restrição de promoção pelos seus produtos e seu tipo e categorias respectivamente.</param>
        /// <returns></returns>
        public List<Promocao> CarregarPromocoesAutomaticas(Usuario usuario, Perfil perfilDoUsuario, Carrinho carrinho)
        {
            var categorias = new List<Categoria>();
            var produtos = new List<Produto>();
            var tiposDeProdutos = new List<ProdutoTipo>();

            if (carrinho != null && carrinho.CarrinhoItens != null)
            {
                carrinho.CarrinhoItens = (from c in carrinho.CarrinhoItens where c.Produto.Disponivel && c.Produto.ExibirSite select c).ToList();

                categorias = carrinho.CarrinhoItens.Select(i => i.Produto).SelectMany(p => p.Categorias).ToList();
                produtos = carrinho.CarrinhoItens.Select(i => i.Produto).ToList();
                tiposDeProdutos = carrinho.CarrinhoItens.Select(i => i.Produto).Select(p => p.ProdutoTipo).ToList();
            }

            List<Promocao> promocoesAutomaticas = CarregarPromocoesAutomaticas(usuario, perfilDoUsuario, categorias,
                                                                               produtos, tiposDeProdutos);

            return promocoesAutomaticas;
        }

        /// <summary>
        /// Calcula a combinação válida de promoções mais vantajosas para o carrinho informado.
        /// </summary>
        /// <param name="carrinho"></param>
        /// <param name="promocoes"></param>
        /// <returns></returns>
        public List<Promocao> CalcuarConjuntoDePromocoesMaisVantajosas(Carrinho carrinho, List<Promocao> promocoes)
        {
            List<Promocao> promocoesMaisVantajosas = new List<Promocao>();

            var combinacoesDePromocoesPossiveis = new List<List<Promocao>>();

            RealizaCombinacoesPossiveis(promocoes, combinacoesDePromocoesPossiveis, null);

            decimal valorDoFrete = carrinho.ValorDoFrete;
            decimal valorDoCarrinho = carrinho.TotalDoCarrinho();
            decimal melhorValor = valorDoCarrinho;
            int indiceDaMelhorCombinacao = -1;

            for (int i = 0; i < combinacoesDePromocoesPossiveis.Count; i++)
            {
                List<Promocao> promocoesPossiveis = combinacoesDePromocoesPossiveis[i];

                // Adiciona as promoções no carrinho;
                promocoesPossiveis.ForEach(carrinho.AdicionarPromocao);

                Int32 qtdFreteGratis = carrinho.CarrinhoItens.Where(c => c.PromocaoFaixaAplicada != null && c.PromocaoFaixaAplicada.FreteGratis).Count();

                valorDoCarrinho = carrinho.TotalDoCarrinho();

                if (valorDoCarrinho < melhorValor)
                {
                    melhorValor = valorDoCarrinho;
                    indiceDaMelhorCombinacao = i;
                }
                else if (valorDoCarrinho == melhorValor && qtdFreteGratis > 0)
                {
                    melhorValor = valorDoCarrinho;
                    indiceDaMelhorCombinacao = i;
                }

                // Remove as promoções no carrinho;
                promocoesPossiveis.ForEach(carrinho.RemoverPromocao);

                //if (valorDoFrete != carrinho.ValorDoFrete || valorDoCarrinho != carrinho.TotalDoCarrinho())
                //{
                //    throw new Exception("Erro ao adicionar e remover as promoções do carrinho!");
                //}
            }

            // Pega a melhor combinação encontrada e obtém somente as que são aplicáveis ao carrinho.
            if (indiceDaMelhorCombinacao >= 0)
            {
                List<Promocao> possiveisMelhoresPromocoes = combinacoesDePromocoesPossiveis[indiceDaMelhorCombinacao];
                possiveisMelhoresPromocoes.ForEach(carrinho.AdicionarPromocao);
                if (carrinho.Promocoes.Count > 0) // O carrinho só armazena as promocções aplicáveis.
                {
                    promocoesMaisVantajosas = carrinho.Promocoes.ToList(); // Clona as promoções.
                }
                possiveisMelhoresPromocoes.ForEach(carrinho.RemoverPromocao);
            }

            return promocoesMaisVantajosas;
        }

        /// <summary>
        /// Retorna todas as combinações possíveis dentro de um grupo de promoções.
        /// </summary>
        /// <param name="promocoes"></param>
        /// <param name="combinacoes"></param>
        /// <param name="combinacaoLinha"></param>
        private void RealizaCombinacoesPossiveis(List<Promocao> promocoes, List<List<Promocao>> combinacoes, List<Promocao> combinacaoLinha)
        {
            if (promocoes.Count > 0)
            {
                for (int i = 0; i < promocoes.Count; i++)
                {
                    var promocao = promocoes[i];
                    List<Promocao> promocoesClone = promocoes.ToList();
                    promocoesClone.RemoveAt(i);
                    List<Promocao> combinacaoLinhaClone = combinacaoLinha == null ? new List<Promocao>() : combinacaoLinha.ToList();
                    combinacaoLinhaClone.Add(promocao);
                    RealizaCombinacoesPossiveis(promocoesClone, combinacoes, combinacaoLinhaClone);
                }
            }
            else
            {
                combinacoes.Add(combinacaoLinha);
            }
        }

        /// <summary>
        /// Verifica todos os usuários válidos oara a promoção de aniversariantes, cadastra os mesmo e dispara os e-mails de aviso
        /// </summary>
        public void DispararPromocaoAniversariantes()
        {
            String caminhoTemplate = ConfigurationManager.AppSettings["CaminhoEmailPromoAniversariante"].ToString();
            List<Usuario> aniversariantes = UsuarioDal.CarregarUsuarioPromocaoAniversariantes();

            if (aniversariantes != null && aniversariantes.Any())
            {
                foreach (Usuario aniversariante in aniversariantes)
                {
                    bool flagEmail = false;

                    try
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            Configuracao configuracaoBO = new Configuracao();

                            // Insere Promocao
                            Promocao promocaoBO = new Promocao();
                            promocaoBO.NomePromocao = String.Concat("PROMOÇÃO ANIVERSÁRIO ", aniversariante.CadastroPessoa, " ANO ", DateTime.Now.Year);
                            promocaoBO.DescricaoPromocao = String.Concat("Feliz aniversário, ", aniversariante.NomeUsuario, "! Aproveite o presente do Grupo A para você!");
                            promocaoBO.DataHoraInicio = DateTime.Now;
                            promocaoBO.DataHoraFim = DateTime.Now.AddDays(30);
                            promocaoBO.AplicaAutomaticamente = false;
                            promocaoBO.PromocaoTipo = new PromocaoTipo();
                            promocaoBO.PromocaoTipo.PromocaoTipoId = 1; // Por quantidade
                            promocaoBO.Ativa = true;
                            promocaoBO.OrigemSistema = true;
                            promocaoBO.Reutilizavel = false;
                            promocaoBO.NumeroMaximoCupom = 1;
                            promocaoBO.NumeroMaximoCupomDif = 1;

                            PromocaoDal.Inserir(promocaoBO);

                            if (promocaoBO != null && promocaoBO.PromocaoId > 0)
                            {
                                // Insere PromocaoFaixa
                                PromocaoFaixa promocaoFaixaBO = new PromocaoFaixa();
                                promocaoFaixaBO.Promocao = new Promocao();
                                promocaoFaixaBO.Promocao = promocaoBO;
                                promocaoFaixaBO.ValorMinimo = 1;
                                promocaoFaixaBO.ValorDesconto = 10;
                                promocaoFaixaBO.FreteGratis = false;

                                new PromocaoBLL().InserirFaixaDaPromocao(promocaoFaixaBO);

                                // Insere PromocaoUsuario
                                PromocaoDal.InserirPromocaoUsuario(promocaoBO, aniversariante);

                                PromocaoCupom promocaoCupom = new PromocaoCupom();
                                promocaoCupom.Promocao = promocaoBO;
                                promocaoCupom.Reutilizavel = false;

                                // Insere PrmoçãoCupom
                                PromocaoCupomDal.Inserir(promocaoBO);

                                promocaoCupom = PromocaoCupomDal.CarregarPorPromocao(promocaoBO);

                                if (promocaoCupom != null)
                                {
                                    flagEmail = this.EnviarEmail(aniversariante.EmailUsuario, caminhoTemplate, promocaoCupom.CodigoCupom.ToString());
                                }
                            }

                            scope.Complete();
                        }
                    }
                    catch(Exception ex) 
                    {
                        throw ex;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailDestino"></param>
        /// <param name="caminhoTemplate"></param>
        /// <returns></returns>
        private Boolean EnviarEmail(String emailDestino, String caminhoTemplate, string codigoPromocao)
        {
            String emailEmitente = String.Empty;
            String assuntoEmail = "Grupo A | Presente de Aniversário do Grupo A";

            emailEmitente = GrupoA.GlobalResources.GrupoA_Resource.EmailSAC.ToString();

            Dictionary<string, string> dicionarioDados = new Dictionary<string, string>();
            dicionarioDados.Add("CodigoPromocao", codigoPromocao);
            //dicionarioDados.Add("Assunto", "Você ganhou a Promoção de Aniversariante");
            //dicionarioDados.Add("Mensagem", "Este é um e-mail da Artmed Informando a Promoção de Aniversariante.");
            //dicionarioDados.Add("DataEnvio", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            dicionarioDados.Add("CaminhoSite", ConfigurationManager.AppSettings["CaminhoImagem"].ToString());

            try // Tenta enviar e-mail
            {
                StringBuilder templateEmail = new EmailHelper().PopulaTemplateEmail(dicionarioDados, caminhoTemplate);

                new EmailHelper().EnviarEmail(emailEmitente, emailDestino, assuntoEmail, templateEmail);
            }
            catch
            {
                return false; // Mensagem NÃO enviada
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="promocao"></param>
        /// <returns></returns>
        public List<PromocaoCupomPedido> CarregarPromocaoCupomPedidoPorPromocao(Promocao promocao)
        {
            return PromocaoCupomPedidoDal.CarregarPromocaoCupomPedidoPorPromocao(promocao);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="promocao"></param>
        /// <returns></returns>
        public Boolean ValidarPromocaoCupom(Promocao promocao)
        {
            if (promocao.PromocaoCupons.Count == 0 || promocao.PromocaoCupons[0] == null)
            {
                return false;
            }
            else if (promocao.PromocaoCupons[0].Reutilizavel)
            {
                return true;
            }
            else
            {
                if (PromocaoCupomPedidoDal.TotalRegistrosPorCodigoCupom(promocao.PromocaoCupons[0].CodigoCupom.ToString()) > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="promocao"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public Boolean ValidarPromocaoUsuario(Promocao promocao, Usuario usuario)
        {
            if (promocao.Usuarios.Count > 0)
            {
                usuario = promocao.Usuarios.Where(c => c.UsuarioId == usuario.UsuarioId).FirstOrDefault();

                if (usuario != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="promocao"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public Boolean ValidarPromocaoPerfil(Promocao promocao, Usuario usuario)
        {
            if (promocao.Perfis.Count > 0)
            {
                Perfil perfil = usuario.Perfis != null && usuario.Perfis.Count() > 0 ? usuario.Perfis[0] : null;

                perfil = promocao.Perfis.Where(c => c.PerfilId == perfil.PerfilId).FirstOrDefault();

                if (perfil != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="promocao"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public Boolean ValidarPromocaoRevista(Promocao promocao, Usuario usuario)
        {
            if (promocao.Revistas.Count > 0)
            {
                Revista revista = promocao.Revistas.Where(c => c.RevistaId == AreaDeRevista.Bmj.GetHashCode()).FirstOrDefault();

                if (revista != null && usuario.AssinanteRevistaBmj)
                {
                    return true;
                }
                else
                {
                    revista = promocao.Revistas.Where(c => c.RevistaId == AreaDeRevista.PatioEnsinoMedio.GetHashCode()).FirstOrDefault();
                    
                    if (revista != null && usuario.AssinanteRevistaPatioEnsMedio)
                    {
                        return true;
                    }
                    else
                    {
                        revista = promocao.Revistas.Where(c => c.RevistaId == AreaDeRevista.PatioFundamental.GetHashCode()).FirstOrDefault();

                        if (revista != null && usuario.AssinanteRevistaPatioEnsFundamental)
                        {
                            return true;
                        }
                        else
                        {
                            revista = promocao.Revistas.Where(c => c.RevistaId == AreaDeRevista.PatioPedagogica.GetHashCode()).FirstOrDefault();

                            if (revista != null && usuario.AssinanteRevistaPatioPedagogica)
                            {
                                return true;
                            }
                        }
                    }

                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="promocao"></param>
        /// <param name="carrinho"></param>
        /// <returns></returns>
        public Boolean ValidarPromocaoProdutos(Promocao promocao, Carrinho carrinho)
        {
            var categorias = new List<Categoria>();
            var produtos = new List<Produto>();
            var tiposDeProdutos = new List<ProdutoTipo>();

            if (carrinho != null && carrinho.CarrinhoItens != null)
            {
                carrinho.CarrinhoItens = (from c in carrinho.CarrinhoItens where c.Produto.Disponivel && c.Produto.ExibirSite select c).ToList();

                categorias = carrinho.CarrinhoItens.Select(i => i.Produto).SelectMany(p => p.Categorias).ToList();
                produtos = carrinho.CarrinhoItens.Select(i => i.Produto).ToList();
                tiposDeProdutos = carrinho.CarrinhoItens.Select(i => i.Produto).Select(p => p.ProdutoTipo).ToList();

                //Valida produtos
                if (promocao.Produtos.Count > 0)
                {
                    Int32 qtdProdutosAplicaveis = 0;

                    foreach (Produto produtoAux in produtos)
                    {
                        Produto produto = promocao.Produtos.Where(c => c.ProdutoId == produtoAux.ProdutoId).FirstOrDefault();

                        if (produto != null)
                        {
                            qtdProdutosAplicaveis++;
                        }
                    }

                    if (qtdProdutosAplicaveis == 0)
                    {
                        return false;
                    }
                }

                //Valida categorias
                if (promocao.Categorias.Count > 0)
                {
                    Int32 qtdCategoriasAplicaveis = 0;

                    foreach (Categoria categoriaAux in categorias)
                    {
                        Categoria categoria = promocao.Categorias.Where(c => c.CategoriaId == categoriaAux.CategoriaId).FirstOrDefault();

                        if (categoria != null)
                        {
                            qtdCategoriasAplicaveis++;
                        }
                    }

                    if (qtdCategoriasAplicaveis == 0)
                    {
                        return false;
                    }
                }

                //Valida tipos de produto
                if (promocao.ProdutoTipos.Count > 0)
                {
                    Int32 qtdProdutoTiposAplicaveis = 0;

                    foreach (ProdutoTipo produtoTipoAux in tiposDeProdutos)
                    {
                        ProdutoTipo produtoTipo = promocao.ProdutoTipos.Where(c => c.ProdutoTipoId == produtoTipoAux.ProdutoTipoId).FirstOrDefault();

                        if (produtoTipo != null)
                        {
                            qtdProdutoTiposAplicaveis++;
                        }
                    }

                    if (qtdProdutoTiposAplicaveis == 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="codigoCupom"></param>
        /// <returns></returns>
        public Promocao CarregarPromocaoPorCupom(String codigoCupom)
        {
            Promocao promocao = PromocaoDal.CarregarPromocaoPorCupom(codigoCupom);

            if (promocao != null)
            {
                promocao.Usuarios = PromocaoDal.CarregarPromocaoUsuarioPorPromocao(promocao.PromocaoId);
                promocao.Categorias = PromocaoDal.CarregarPromocaoCategoriaPorPromocao(promocao.PromocaoId);
                promocao.Perfis = PromocaoDal.CarregarPromocaoPerfilPorPromocao(promocao.PromocaoId);
                promocao.Revistas = PromocaoDal.CarregarPromocaoRevistaPorPromocao(promocao.PromocaoId);
                promocao.Produtos = PromocaoDal.CarregarPromocaoProdutoPorPromocao(promocao.PromocaoId);
                promocao.ProdutoTipos = PromocaoDal.CarregarPromocaoProdutoTipoPorPromocao(promocao.PromocaoId);
            }

            return promocao;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="promocaoId"></param>
        /// <returns></returns>
        public StringBuilder CarregarCategoriasSelecionadasPorPromocao(Int32 promocaoId)
        {
            return PromocaoDal.CarregarCategoriasSelecionadasPorPromocao(promocaoId);
        }

        public Promocao Carregar(Promocao entidade)
        {
            return PromocaoDal.Carregar(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="promocaoCupom"></param>
        /// <returns></returns>
        public PromocaoCupom CarregarPromocaoCupom(PromocaoCupom promocaoCupom)
        {
            return PromocaoCupomDal.Carregar(promocaoCupom);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="promocaoCupom"></param>
        public void AtualizarPromocaoCupom(PromocaoCupom promocaoCupom)
        {
            PromocaoCupomDal.Atualizar(promocaoCupom);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="codigoAmigavel"></param>
        /// <returns></returns>
        public PromocaoCupom CarregarPorCodigoAmigavel(String codigoAmigavel)
        {
            return this.CarregarPorCodigoAmigavel(null, codigoAmigavel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="promocaoCupomId"></param>
        /// <param name="codigoAmigavel"></param>
        /// <returns></returns>
        public PromocaoCupom CarregarPorCodigoAmigavel(Int32? promocaoCupomId, String codigoAmigavel)
        {
            return PromocaoCupomDal.CarregarPorCodigoAmigavel(promocaoCupomId, codigoAmigavel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="promocao"></param>
        /// <param name="revista"></param>
        public void InserirPromocaoRevista(Promocao promocao, Revista revista)
        {
            PromocaoDal.InserirPromocaoRevista(promocao, revista);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="promocao"></param>
        /// <param name="revista"></param>
        public void ExcluirPromocaoRevista(Promocao promocao, Revista revista)
        {
            PromocaoDal.ExcluirPromocaoRevista(promocao, revista);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="promocao"></param>
        public void ExcluirRevistasPorPromocao(Promocao promocao)
        {
            PromocaoDal.ExcluirRevistasPorPromocao(promocao);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revistas"></param>
        /// <returns></returns>
        public List<Revista> CarregarTodasRevistasExcetoRevistas(List<Revista> revistas)
        {
            return RevistaDal.CarregarTodasExcetoRevistas(revistas);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="promocao"></param>
        /// <returns></returns>
        public List<Revista> CarregarRevistasPorPromocao(Promocao promocao)
        {
            return RevistaDal.CarregarTodosPorPromocao(promocao);
        }
    }
}