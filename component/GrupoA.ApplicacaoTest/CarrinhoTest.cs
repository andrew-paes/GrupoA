using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using GrupoA.BusinessObject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GrupoA.ApplicacaoTest
{
    /// <summary>
    /// Summary description for CarrinhoTest
    /// </summary>
    [TestClass]
    public class CarrinhoTest
    {
        #region propriedades
        public CarrinhoTest()
        {
           CarrinhoDeTeste = new Carrinho();
        }

        private TestContext testContextInstance;
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        private List<Produto> _produtosParaCarrinho;
        private List<Promocao> _promocoesParaCarrinho;
        private List<Promocao> _promocoesParaItensDoCarrinho;

        public List<Produto> ProdutosParaCarrinho
        {
            get { return _produtosParaCarrinho; }
            set { _produtosParaCarrinho = value; }
        }

        public List<Promocao> PromocoesParaCarrinho
        {
            get { return _promocoesParaCarrinho; }
            set { _promocoesParaCarrinho = value; }
        }

        public List<Promocao> PromocoesParaItensDoCarrinho
        {
            get { return _promocoesParaItensDoCarrinho; }
            set { _promocoesParaItensDoCarrinho = value; }
        }

        public TestContext TestContextInstance
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        public Carrinho CarrinhoDeTeste { get; set; }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
        }

        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void MyTestInitialize()
        {

        }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        #endregion

        [TestMethod]
        public void Adicionar_Produto_Normal_Com_Valor_Unitario()
        {
            AdicionarProdutoNormalComValorUnitario();

            int expectedTotalItens = 1;
            int actualTotalItens = CarrinhoDeTeste.TotalDeItensNoCarrinho;

            Assert.AreEqual(expectedTotalItens, actualTotalItens);

            decimal expectedTotalCarrinho = 40.0m;
            decimal actualTotalCarrinho = CarrinhoDeTeste.TotalDoCarrinho();

            Assert.AreEqual(expectedTotalCarrinho, actualTotalCarrinho);

        }

        private void AdicionarProdutoNormalComValorUnitario()
        {
            CarrinhoItem carrinhoItem = new CarrinhoItem();

            carrinhoItem.Produto = new Produto
            {
                ProdutoId = 1,
                ValorUnitario = 20.0m,
                Peso = 1000,
                ProdutoTipo = new ProdutoTipo(1)
                ,
                Categorias = new List<Categoria>() { new Categoria() { CategoriaId = 1 } }
            };

            carrinhoItem.Quantidade = 2;

            CarrinhoDeTeste.AdicionarItem(carrinhoItem);
        }

        [TestMethod]
        public void Adicionar_Produto_Normal_Com_Valor_Oferta()
        {
            AdicionarProdutoNormalComValorUnitario();
            AdicionarProdutoNormalComValorOferta();

            int expectedTotalItens = 2;
            int actualTotalItens = CarrinhoDeTeste.TotalDeItensNoCarrinho;

            Assert.AreEqual(expectedTotalItens, actualTotalItens);

            decimal expectedTotalCarrinho = 70.0m;
            decimal actualTotalCarrinho = CarrinhoDeTeste.TotalDoCarrinho();

            Assert.AreEqual(expectedTotalCarrinho, actualTotalCarrinho);
        }

        private void AdicionarProdutoNormalComValorOferta()
        {
            Produto produto = new Produto
            {
                ProdutoId = 2,
                ValorUnitario = 20.0m,
                ValorOferta = 15.0m,
                Peso = 1000,
                ProdutoTipo = new ProdutoTipo(1),
                Categorias = new List<Categoria>() { new Categoria() { CategoriaId = 2 } }
            };

            const decimal quantidade = 2.0m;

            CarrinhoDeTeste.AdicionarItem(produto, quantidade);
        }

        [TestMethod]
        public void Adicionar_Mesmo_Produto_Por_Compra_Conjunta_Com_Valor_Unitario()
        {
            AdicionarProdutoNormalComValorUnitario();
            AdicionarProdutoNormalComValorOferta();
            AdicionarMesmoProdutoPorCompraConjuntaComValorUnitario();

            int expectedTotalItens = 3;
            int actualTotalItens = CarrinhoDeTeste.TotalDeItensNoCarrinho;

            Assert.AreEqual(expectedTotalItens, actualTotalItens);

            decimal expectedTotalCarrinho = 150.0m;
            decimal actualTotalCarrinho = CarrinhoDeTeste.TotalDoCarrinho();

            Assert.AreEqual(expectedTotalCarrinho, actualTotalCarrinho);
        }

        private void AdicionarMesmoProdutoPorCompraConjuntaComValorUnitario()
        {
            CarrinhoItem carrinhoItem = new CarrinhoItem
                                            {
                                                Produto = new Produto
                                                              {
                                                                  ProdutoId = 1,
                                                                  ValorUnitario = 20.0m,
                                                                  Peso = 1000,
                                                                  ProdutoTipo = new ProdutoTipo(1),
                                                                  Categorias = new List<Categoria>() { new Categoria() { CategoriaId = 1 } }
                                                              }
                                            };
            carrinhoItem.Quantidade = 5;

            carrinhoItem.CompraConjuntaDesconto = new CompraConjuntaDesconto
                                                      {
                                                          CompraConjuntaDescontoId = 1,
                                                          PercentualDesconto = 20.0m,
                                                          CompraConjunta = new CompraConjunta(1)
                                                      };


            CarrinhoDeTeste.AdicionarItem(carrinhoItem);
        }

        [TestMethod]
        public void Adicionar_Mesmo_Produto_Por_Compra_Conjunta_Com_Valor_Oferta()
        {
            AdicionarProdutoNormalComValorUnitario();
            AdicionarProdutoNormalComValorOferta();
            AdicionarMesmoProdutoPorCompraConjuntaComValorUnitario();
            AdicionarMesmoProdutoPorCompraConjuntaComValorOferta();

            int expectedTotalItens = 4;
            int actualTotalItens = CarrinhoDeTeste.TotalDeItensNoCarrinho;

            Assert.AreEqual(expectedTotalItens, actualTotalItens);

            decimal expectedTotalCarrinho = 150.0m + 27.0m;
            decimal actualTotalCarrinho = CarrinhoDeTeste.TotalDoCarrinho();

            Assert.AreEqual(expectedTotalCarrinho, actualTotalCarrinho);
        }

        private void AdicionarMesmoProdutoPorCompraConjuntaComValorOferta()
        {
            CarrinhoItem carrinhoItem = new CarrinhoItem();

            carrinhoItem.Produto = new Produto
                                       {
                                           ProdutoId = 2,
                                           ValorUnitario = 20.0m,
                                           ValorOferta = 15.0m,
                                           Peso = 1000,
                                           ProdutoTipo = new ProdutoTipo(1),
                                           Categorias = new List<Categoria>() { new Categoria() { CategoriaId = 2 } }
                                       };
            carrinhoItem.Quantidade = 2;

            carrinhoItem.CompraConjuntaDesconto = new CompraConjuntaDesconto
                                                      {
                                                          CompraConjuntaDescontoId = 2,
                                                          PercentualDesconto = 10.0m,
                                                          CompraConjunta = new CompraConjunta(2)
                                                      };

            CarrinhoDeTeste.AdicionarItem(carrinhoItem);
        }

        [TestMethod]
        public void Adicionar_Promocao_Por_Quantidade_Com_Restricao_De_Tipo_De_Produto_Nao_Atingivel()
        {
            AdicionarProdutoNormalComValorUnitario();
            AdicionarProdutoNormalComValorOferta();
            AdicionarMesmoProdutoPorCompraConjuntaComValorUnitario();
            AdicionarMesmoProdutoPorCompraConjuntaComValorOferta();
            AdicionarPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoNaoAtingivel();

            decimal expectedTotalCarrinho = 177.0m;
            decimal actualTotalCarrinho = CarrinhoDeTeste.TotalDoCarrinho();

            Assert.AreEqual(expectedTotalCarrinho, actualTotalCarrinho);
        }

        private void AdicionarPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoNaoAtingivel()
        {
            Promocao promocao = new Promocao();
            promocao.PromocaoId = 1;
            promocao.PromocaoTipo = new PromocaoTipo((int)TipoDePromocao.PorQuantidade);

            PromocaoFaixa faixa1 = new PromocaoFaixa();
            faixa1.ValorMinimo = 5;
            faixa1.PercentualDesconto = 10.0m;
            faixa1.Promocao = promocao;

            PromocaoFaixa faixa2 = new PromocaoFaixa();
            faixa2.ValorMinimo = 10;
            faixa2.PercentualDesconto = 20.0m;
            faixa2.Promocao = promocao;

            promocao.PromocaoFaixas = new List<PromocaoFaixa>();
            promocao.PromocaoFaixas.Add(faixa1);
            promocao.PromocaoFaixas.Add(faixa2);

            promocao.ProdutoTipos = new List<ProdutoTipo> { new ProdutoTipo(1) };

            CarrinhoDeTeste.AdicionarPromocao(promocao);
        }

        [TestMethod]
        public void Adicionar_Promocao_Por_Quantidade_Com_Restricao_De_Tipo_De_Produto_Atingivel()
        {
            AdicionarProdutoNormalComValorUnitario();
            AdicionarProdutoNormalComValorOferta();
            AdicionarMesmoProdutoPorCompraConjuntaComValorUnitario();
            AdicionarMesmoProdutoPorCompraConjuntaComValorOferta();
            AdicionarPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoNaoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoAtingivel();

            decimal expectedTotalCarrinho = 170.0m;
            decimal actualTotalCarrinho = CarrinhoDeTeste.TotalDoCarrinho();

            Assert.AreEqual(expectedTotalCarrinho, actualTotalCarrinho);
        }

        private void AdicionarPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoAtingivel()
        {
            Promocao promocao = new Promocao();
            promocao.PromocaoId = 2;
            promocao.PromocaoTipo = new PromocaoTipo((int)TipoDePromocao.PorQuantidade);

            PromocaoFaixa faixa1 = new PromocaoFaixa();
            faixa1.ValorMinimo = 4;
            faixa1.PercentualDesconto = 10.0m;
            faixa1.Promocao = promocao;

            PromocaoFaixa faixa2 = new PromocaoFaixa();
            faixa2.ValorMinimo = 8;
            faixa2.PercentualDesconto = 20.0m;
            faixa2.Promocao = promocao;

            promocao.PromocaoFaixas = new List<PromocaoFaixa>();
            promocao.PromocaoFaixas.Add(faixa1);
            promocao.PromocaoFaixas.Add(faixa2);

            promocao.ProdutoTipos = new List<ProdutoTipo> { new ProdutoTipo(1) };

            CarrinhoDeTeste.AdicionarPromocao(promocao);
        }

        [TestMethod]
        public void Remover_Promocao_Por_Quantidade_Com_Restricao_De_Tipo_De_Produto_Atingivel()
        {
            AdicionarProdutoNormalComValorUnitario();
            AdicionarProdutoNormalComValorOferta();
            AdicionarMesmoProdutoPorCompraConjuntaComValorUnitario();
            AdicionarMesmoProdutoPorCompraConjuntaComValorOferta();
            AdicionarPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoNaoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoAtingivel();
            RemoverPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoAtingivel();

            decimal expectedTotalCarrinho = 177.0m;
            decimal actualTotalCarrinho = CarrinhoDeTeste.TotalDoCarrinho();

            Assert.AreEqual(expectedTotalCarrinho, actualTotalCarrinho);
        }

        private void RemoverPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoAtingivel()
        {
            Promocao promocao = new Promocao();
            promocao.PromocaoId = 2;
            promocao.PromocaoTipo = new PromocaoTipo((int)TipoDePromocao.PorQuantidade);

            CarrinhoDeTeste.RemoverPromocao(promocao);
        }

        [TestMethod]
        public void Adicionar_Promocao_Por_Quantidade_Com_Restricao_De_Produto_Atingivel()
        {
            AdicionarProdutoNormalComValorUnitario();
            AdicionarProdutoNormalComValorOferta();
            AdicionarMesmoProdutoPorCompraConjuntaComValorUnitario();
            AdicionarMesmoProdutoPorCompraConjuntaComValorOferta();
            AdicionarPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoNaoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoAtingivel();
            RemoverPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeProdutoAtingivel();

            decimal expectedTotalCarrinho = 173.0m;
            decimal actualTotalCarrinho = CarrinhoDeTeste.TotalDoCarrinho();

            Assert.AreEqual(expectedTotalCarrinho, actualTotalCarrinho);
        }

        private void AdicionarPromocaoPorQuantidadeComRestricaoDeProdutoAtingivel()
        {
            Promocao promocao = new Promocao();
            promocao.PromocaoId = 3;
            promocao.PromocaoTipo = new PromocaoTipo((int)TipoDePromocao.PorQuantidade);

            PromocaoFaixa faixa1 = new PromocaoFaixa();
            faixa1.ValorMinimo = 2;
            faixa1.PercentualDesconto = 10.0m;
            faixa1.Promocao = promocao;

            promocao.PromocaoFaixas = new List<PromocaoFaixa>();
            promocao.PromocaoFaixas.Add(faixa1);

            promocao.Produtos = new List<Produto>() { new Produto() { ProdutoId = 1 } };

            CarrinhoDeTeste.AdicionarPromocao(promocao);
        }

        [TestMethod]
        public void Remover_Promocao_Por_Quantidade_Com_Restricao_De_Produto_Atingivel()
        {
            AdicionarProdutoNormalComValorUnitario();
            AdicionarProdutoNormalComValorOferta();
            AdicionarMesmoProdutoPorCompraConjuntaComValorUnitario();
            AdicionarMesmoProdutoPorCompraConjuntaComValorOferta();
            AdicionarPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoNaoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoAtingivel();
            RemoverPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeProdutoAtingivel();
            RemoverPromocaoPorQuantidadeComRestricaoDeProdutoAtingivel();

            decimal expectedTotalCarrinho = 177.0m;
            decimal actualTotalCarrinho = CarrinhoDeTeste.TotalDoCarrinho();

            Assert.AreEqual(expectedTotalCarrinho, actualTotalCarrinho);
        }

        private void RemoverPromocaoPorQuantidadeComRestricaoDeProdutoAtingivel()
        {
            Promocao promocao = new Promocao();
            promocao.PromocaoId = 3;
            promocao.PromocaoTipo = new PromocaoTipo((int)TipoDePromocao.PorQuantidade);

            CarrinhoDeTeste.RemoverPromocao(promocao);
        }

        [TestMethod]
        public void Adicionar_Promocao_Por_Quantidade_Com_Restricao_De_Categoria_Atingivel()
        {
            AdicionarProdutoNormalComValorUnitario();
            AdicionarProdutoNormalComValorOferta();
            AdicionarMesmoProdutoPorCompraConjuntaComValorUnitario();
            AdicionarMesmoProdutoPorCompraConjuntaComValorOferta();
            AdicionarPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoNaoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoAtingivel();
            RemoverPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeProdutoAtingivel();
            RemoverPromocaoPorQuantidadeComRestricaoDeProdutoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeCategoriaAtingivel();

            decimal expectedTotalCarrinho = 157.0m;
            decimal actualTotalCarrinho = CarrinhoDeTeste.TotalDoCarrinho();

            Assert.AreEqual(expectedTotalCarrinho, actualTotalCarrinho);
        }

        private void AdicionarPromocaoPorQuantidadeComRestricaoDeCategoriaAtingivel()
        {
            Promocao promocao = new Promocao();
            promocao.PromocaoId = 4;
            promocao.PromocaoTipo = new PromocaoTipo((int)TipoDePromocao.PorQuantidade);

            PromocaoFaixa faixa1 = new PromocaoFaixa();
            faixa1.ValorMinimo = 2;
            faixa1.PercentualDesconto = 50.0m;
            faixa1.Promocao = promocao;

            promocao.PromocaoFaixas = new List<PromocaoFaixa>();
            promocao.PromocaoFaixas.Add(faixa1);

            promocao.Categorias = new List<Categoria>() { new Categoria() { CategoriaId = 1 } };

            CarrinhoDeTeste.AdicionarPromocao(promocao);
        }

        [TestMethod]
        public void Remover_Promocao_Por_Quantidade_Com_Restricao_De_Categoria_Atingivel()
        {
            AdicionarProdutoNormalComValorUnitario();
            AdicionarProdutoNormalComValorOferta();
            AdicionarMesmoProdutoPorCompraConjuntaComValorUnitario();
            AdicionarMesmoProdutoPorCompraConjuntaComValorOferta();
            AdicionarPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoNaoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoAtingivel();
            RemoverPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeProdutoAtingivel();
            RemoverPromocaoPorQuantidadeComRestricaoDeProdutoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeCategoriaAtingivel();
            RemoverPromocaoPorQuantidadeComRestricaoDeCategoriaAtingivel();

            decimal expectedTotalCarrinho = 177.0m;
            decimal actualTotalCarrinho = CarrinhoDeTeste.TotalDoCarrinho();

            Assert.AreEqual(expectedTotalCarrinho, actualTotalCarrinho);
        }

        private void RemoverPromocaoPorQuantidadeComRestricaoDeCategoriaAtingivel()
        {
            Promocao promocao = new Promocao();
            promocao.PromocaoId = 4;
            promocao.PromocaoTipo = new PromocaoTipo((int)TipoDePromocao.PorQuantidade);

            CarrinhoDeTeste.RemoverPromocao(promocao);
        }

        [TestMethod]
        public void Adicionar_Promocao_Por_Valor_Com_Restricao_De_Tipo_De_Produto_Atingivel()
        {
            AdicionarProdutoNormalComValorUnitario();
            AdicionarProdutoNormalComValorOferta();
            AdicionarMesmoProdutoPorCompraConjuntaComValorUnitario();
            AdicionarMesmoProdutoPorCompraConjuntaComValorOferta();
            AdicionarPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoNaoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoAtingivel();
            RemoverPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeProdutoAtingivel();
            RemoverPromocaoPorQuantidadeComRestricaoDeProdutoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeCategoriaAtingivel();
            RemoverPromocaoPorQuantidadeComRestricaoDeCategoriaAtingivel();
            AdicionarPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivel();

            decimal expectedTotalCarrinho = 170.0m;
            decimal actualTotalCarrinho = CarrinhoDeTeste.TotalDoCarrinho();

            Assert.AreEqual(expectedTotalCarrinho, actualTotalCarrinho);
        }

        private void AdicionarPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivel()
        {
            Promocao promocao = new Promocao();
            promocao.PromocaoId = 10;
            promocao.PromocaoTipo = new PromocaoTipo((int)TipoDePromocao.PorValor);

            PromocaoFaixa faixa1 = new PromocaoFaixa();
            faixa1.ValorMinimo = 50;
            faixa1.PercentualDesconto = 10.0m;
            faixa1.Promocao = promocao;

            PromocaoFaixa faixa2 = new PromocaoFaixa();
            faixa2.ValorMinimo = 100;
            faixa2.PercentualDesconto = 20.0m;
            faixa2.Promocao = promocao;

            promocao.PromocaoFaixas = new List<PromocaoFaixa>();
            promocao.PromocaoFaixas.Add(faixa1);
            promocao.PromocaoFaixas.Add(faixa2);

            promocao.ProdutoTipos = new List<ProdutoTipo> { new ProdutoTipo(1) };

            CarrinhoDeTeste.AdicionarPromocao(promocao);
        }

        [TestMethod]
        public void Remover_Promocao_Por_Valor_Com_Restricao_De_Tipo_De_Produto_Atingivel()
        {
            AdicionarProdutoNormalComValorUnitario();
            AdicionarProdutoNormalComValorOferta();
            AdicionarMesmoProdutoPorCompraConjuntaComValorUnitario();
            AdicionarMesmoProdutoPorCompraConjuntaComValorOferta();
            AdicionarPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoNaoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoAtingivel();
            RemoverPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeProdutoAtingivel();
            RemoverPromocaoPorQuantidadeComRestricaoDeProdutoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeCategoriaAtingivel();
            RemoverPromocaoPorQuantidadeComRestricaoDeCategoriaAtingivel();
            AdicionarPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivel();
            RemoverPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivel();

            decimal expectedTotalCarrinho = 177.0m;
            decimal actualTotalCarrinho = CarrinhoDeTeste.TotalDoCarrinho();

            Assert.AreEqual(expectedTotalCarrinho, actualTotalCarrinho);
        }

        private void RemoverPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivel()
        {
            Promocao promocao = new Promocao();
            promocao.PromocaoId = 10;
            promocao.PromocaoTipo = new PromocaoTipo((int)TipoDePromocao.PorQuantidade);

            CarrinhoDeTeste.RemoverPromocao(promocao);
        }

        [TestMethod]
        public void Adicionar_Promocao_Por_Valor_Com_Restricao_De_Tipo_De_Produto_Atingivel_Com_Frete_Gratis()
        {
            AdicionarProdutoNormalComValorUnitario();
            AdicionarProdutoNormalComValorOferta();
            AdicionarMesmoProdutoPorCompraConjuntaComValorUnitario();
            AdicionarMesmoProdutoPorCompraConjuntaComValorOferta();
            AdicionarPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoNaoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoAtingivel();
            RemoverPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeProdutoAtingivel();
            RemoverPromocaoPorQuantidadeComRestricaoDeProdutoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeCategoriaAtingivel();
            RemoverPromocaoPorQuantidadeComRestricaoDeCategoriaAtingivel();
            AdicionarPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivel();
            RemoverPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivel();
            AdicionarPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivelComFreteGratis();

            decimal expectedTotalCarrinho = 170.0m;
            decimal actualTotalCarrinho = CarrinhoDeTeste.TotalDoCarrinho();

            Assert.AreEqual(expectedTotalCarrinho, actualTotalCarrinho);
        }

        private void AdicionarPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivelComFreteGratis()
        {
            Promocao promocao = new Promocao();
            promocao.PromocaoId = 11;
            promocao.PromocaoTipo = new PromocaoTipo((int)TipoDePromocao.PorValor);

            PromocaoFaixa faixa1 = new PromocaoFaixa();
            faixa1.ValorMinimo = 50;
            faixa1.PercentualDesconto = 10.0m;
            faixa1.FreteGratis = true;
            faixa1.Promocao = promocao;

            PromocaoFaixa faixa2 = new PromocaoFaixa();
            faixa2.ValorMinimo = 50;
            faixa2.PercentualDesconto = 10.0m;
            faixa2.FreteGratis = true;
            faixa2.Promocao = promocao;

            promocao.PromocaoFaixas = new List<PromocaoFaixa>();
            promocao.PromocaoFaixas.Add(faixa1);
            promocao.PromocaoFaixas.Add(faixa2);

            promocao.ProdutoTipos = new List<ProdutoTipo> { new ProdutoTipo(1) };

            CarrinhoDeTeste.AdicionarPromocao(promocao);
        }

        [TestMethod]
        public void Calcula_Peso_Com_Promocao_Por_Frete_Gratis()
        {
            AdicionarProdutoNormalComValorUnitario();
            AdicionarProdutoNormalComValorOferta();
            AdicionarMesmoProdutoPorCompraConjuntaComValorUnitario();
            AdicionarMesmoProdutoPorCompraConjuntaComValorOferta();
            AdicionarPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoNaoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoAtingivel();
            RemoverPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeProdutoAtingivel();
            RemoverPromocaoPorQuantidadeComRestricaoDeProdutoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeCategoriaAtingivel();
            RemoverPromocaoPorQuantidadeComRestricaoDeCategoriaAtingivel();
            AdicionarPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivel();
            RemoverPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivel();
            AdicionarPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivelComFreteGratis();

            decimal expected = 7000.0m;
            decimal actual = CarrinhoDeTeste.PesoTotalDoPedido(); ;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Remover_Promocao_Por_Valor_Com_Restricao_De_Tipo_De_Produto_Atingivel_Com_Frete_Gratis()
        {
            AdicionarProdutoNormalComValorUnitario();
            AdicionarProdutoNormalComValorOferta();
            AdicionarMesmoProdutoPorCompraConjuntaComValorUnitario();
            AdicionarMesmoProdutoPorCompraConjuntaComValorOferta();
            AdicionarPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoNaoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoAtingivel();
            RemoverPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeProdutoAtingivel();
            RemoverPromocaoPorQuantidadeComRestricaoDeProdutoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeCategoriaAtingivel();
            RemoverPromocaoPorQuantidadeComRestricaoDeCategoriaAtingivel();
            AdicionarPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivel();
            RemoverPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivel();
            AdicionarPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivelComFreteGratis();
            RemoverPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivelComFreteGratis();

            decimal expectedTotalCarrinho = 177.0m;
            decimal actualTotalCarrinho = CarrinhoDeTeste.TotalDoCarrinho();

            Assert.AreEqual(expectedTotalCarrinho, actualTotalCarrinho);
        }

        private void RemoverPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivelComFreteGratis()
        {
            Promocao promocao = new Promocao();
            promocao.PromocaoId = 11;
            promocao.PromocaoTipo = new PromocaoTipo((int)TipoDePromocao.PorQuantidade);

            CarrinhoDeTeste.RemoverPromocao(promocao);
        }

        [TestMethod]
        public void Calcula_Peso_Sem_Promocao_Por_Frete_Gratis()
        {
            AdicionarProdutoNormalComValorUnitario();
            AdicionarProdutoNormalComValorOferta();
            AdicionarMesmoProdutoPorCompraConjuntaComValorUnitario();
            AdicionarMesmoProdutoPorCompraConjuntaComValorOferta();
            AdicionarPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoNaoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoAtingivel();
            RemoverPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeProdutoAtingivel();
            RemoverPromocaoPorQuantidadeComRestricaoDeProdutoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeCategoriaAtingivel();
            RemoverPromocaoPorQuantidadeComRestricaoDeCategoriaAtingivel();
            AdicionarPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivel();
            RemoverPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivel();
            AdicionarPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivelComFreteGratis();
            RemoverPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivelComFreteGratis();

            decimal expected = 11000.0m;
            decimal actual = CarrinhoDeTeste.PesoTotalDoPedido(); ;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Adicionar_Promocao_Por_Valor_Com_Restricao_De_Produto_Atingivel()
        {
            AdicionarProdutoNormalComValorUnitario();
            AdicionarProdutoNormalComValorOferta();
            AdicionarMesmoProdutoPorCompraConjuntaComValorUnitario();
            AdicionarMesmoProdutoPorCompraConjuntaComValorOferta();
            AdicionarPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoNaoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoAtingivel();
            RemoverPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeProdutoAtingivel();
            RemoverPromocaoPorQuantidadeComRestricaoDeProdutoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeCategoriaAtingivel();
            RemoverPromocaoPorQuantidadeComRestricaoDeCategoriaAtingivel();
            AdicionarPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivel();
            RemoverPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivel();
            AdicionarPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivelComFreteGratis();
            RemoverPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivelComFreteGratis();
            AdicionarPromocaoPorValorComRestricaoDeProdutoAtingivel();

            decimal expectedTotalCarrinho = 169.0m;
            decimal actualTotalCarrinho = CarrinhoDeTeste.TotalDoCarrinho();

            Assert.AreEqual(expectedTotalCarrinho, actualTotalCarrinho);
        }

        private void AdicionarPromocaoPorValorComRestricaoDeProdutoAtingivel()
        {
            Promocao promocao = new Promocao();
            promocao.PromocaoId = 10;
            promocao.PromocaoTipo = new PromocaoTipo((int)TipoDePromocao.PorValor);

            PromocaoFaixa faixa1 = new PromocaoFaixa();
            faixa1.ValorMinimo = 20;
            faixa1.PercentualDesconto = 10.0m;
            faixa1.Promocao = promocao;

            PromocaoFaixa faixa2 = new PromocaoFaixa();
            faixa2.ValorMinimo = 40;
            faixa2.PercentualDesconto = 20.0m;
            faixa2.Promocao = promocao;

            promocao.PromocaoFaixas = new List<PromocaoFaixa>();
            promocao.PromocaoFaixas.Add(faixa1);
            promocao.PromocaoFaixas.Add(faixa2);

            promocao.Produtos = new List<Produto> { new Produto() { ProdutoId = 1 } };

            CarrinhoDeTeste.AdicionarPromocao(promocao);
        }

        [TestMethod]
        public void Adicionar_Promocao_Por_Valor_Com_Restricao_De_Tipo_De_Produto_Atingivel_Novamente()
        {
            AdicionarProdutoNormalComValorUnitario();
            AdicionarProdutoNormalComValorOferta();
            AdicionarMesmoProdutoPorCompraConjuntaComValorUnitario();
            AdicionarMesmoProdutoPorCompraConjuntaComValorOferta();
            AdicionarPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoNaoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoAtingivel();
            RemoverPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeProdutoAtingivel();
            RemoverPromocaoPorQuantidadeComRestricaoDeProdutoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeCategoriaAtingivel();
            RemoverPromocaoPorQuantidadeComRestricaoDeCategoriaAtingivel();
            AdicionarPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivel();
            RemoverPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivel();
            AdicionarPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivelComFreteGratis();
            RemoverPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivelComFreteGratis();
            AdicionarPromocaoPorValorComRestricaoDeProdutoAtingivel();
            AdicionarPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivelNovamente();

            decimal expectedTotalCarrinho = 163.0m;
            decimal actualTotalCarrinho = CarrinhoDeTeste.TotalDoCarrinho();

            Assert.AreEqual(expectedTotalCarrinho, actualTotalCarrinho);
        }

        private void AdicionarPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivelNovamente()
        {

            Promocao promocao = new Promocao();
            promocao.PromocaoId = 20;
            promocao.PromocaoTipo = new PromocaoTipo((int)TipoDePromocao.PorValor);

            PromocaoFaixa faixa1 = new PromocaoFaixa();
            faixa1.ValorMinimo = 15;
            faixa1.PercentualDesconto = 10.0m;
            faixa1.Promocao = promocao;

            PromocaoFaixa faixa2 = new PromocaoFaixa();
            faixa2.ValorMinimo = 30;
            faixa2.PercentualDesconto = 20.0m;
            faixa2.Promocao = promocao;

            promocao.PromocaoFaixas = new List<PromocaoFaixa>();
            promocao.PromocaoFaixas.Add(faixa1);
            promocao.PromocaoFaixas.Add(faixa2);

            promocao.ProdutoTipos = new List<ProdutoTipo>() { new ProdutoTipo(1) };

            CarrinhoDeTeste.AdicionarPromocao(promocao);
        }

        [TestMethod]
        public void Remover_Produto_Normal_Com_Valor_Unitario()
        {
            AdicionarProdutoNormalComValorUnitario();
            AdicionarProdutoNormalComValorOferta();
            AdicionarMesmoProdutoPorCompraConjuntaComValorUnitario();
            AdicionarMesmoProdutoPorCompraConjuntaComValorOferta();
            AdicionarPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoNaoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoAtingivel();
            RemoverPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeProdutoAtingivel();
            RemoverPromocaoPorQuantidadeComRestricaoDeProdutoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeCategoriaAtingivel();
            RemoverPromocaoPorQuantidadeComRestricaoDeCategoriaAtingivel();
            AdicionarPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivel();
            RemoverPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivel();
            AdicionarPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivelComFreteGratis();
            RemoverPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivelComFreteGratis();
            AdicionarPromocaoPorValorComRestricaoDeProdutoAtingivel();
            AdicionarPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivelNovamente();

            int expectedTotalItens = CarrinhoDeTeste.TotalDeItensNoCarrinho - 1;

            RemoverProdutoNormalComValorUnitario();

            int actualTotalItens = CarrinhoDeTeste.TotalDeItensNoCarrinho;

            Assert.AreEqual(expectedTotalItens, actualTotalItens);

            const decimal expectedTotalCarrinho = 131.0m;
            decimal actualTotalCarrinho = CarrinhoDeTeste.TotalDoCarrinho();

            Assert.AreEqual(expectedTotalCarrinho, actualTotalCarrinho);

        }

        private void RemoverProdutoNormalComValorUnitario()
        {
            Produto produto = new Produto
                                  {
                                      ProdutoId = 1
                                  };

            CarrinhoDeTeste.RemoverItem(new CarrinhoItem() { Produto = produto });
        }

        [TestMethod]
        public void Clonar_Carrinho()
        {
            AdicionarProdutoNormalComValorUnitario();
            AdicionarProdutoNormalComValorOferta();
            AdicionarMesmoProdutoPorCompraConjuntaComValorUnitario();
            AdicionarMesmoProdutoPorCompraConjuntaComValorOferta();
            AdicionarPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoNaoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoAtingivel();
            RemoverPromocaoPorQuantidadeComRestricaoDeTipoDeProdutoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeProdutoAtingivel();
            RemoverPromocaoPorQuantidadeComRestricaoDeProdutoAtingivel();
            AdicionarPromocaoPorQuantidadeComRestricaoDeCategoriaAtingivel();
            RemoverPromocaoPorQuantidadeComRestricaoDeCategoriaAtingivel();
            AdicionarPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivel();
            RemoverPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivel();
            AdicionarPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivelComFreteGratis();
            RemoverPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivelComFreteGratis();
            AdicionarPromocaoPorValorComRestricaoDeProdutoAtingivel();
            AdicionarPromocaoPorValorComRestricaoDeTipoDeProdutoAtingivelNovamente();
            RemoverProdutoNormalComValorUnitario();

            Carrinho carrinhoClonado = CarrinhoDeTeste.Clone() as Carrinho;
            Assert.AreNotSame(carrinhoClonado, CarrinhoDeTeste);

            decimal expected = carrinhoClonado.TotalDoCarrinho();
            decimal actual = CarrinhoDeTeste.TotalDoCarrinho();
            Assert.AreEqual(expected, actual);

        }

    }
}
