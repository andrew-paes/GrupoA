using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GrupoA.ApplicacaoTest
{
    [TestClass]
    public class PromocaoBLLTest
    {
        private static int PromocaoId { get; set; }

        [TestMethod]
        public void a000_Inserir_Promocao_Automatica_Restrita_Por_Categoria()
        {
            PromocaoBLL service = new PromocaoBLL();

            Promocao promocao = new Promocao();
            promocao.AplicaAutomaticamente = true;
            promocao.Ativa = true;

            Categoria categoriaRestricao = new Categoria() {CategoriaId =  5};
            promocao.Categorias = new List<Categoria>();
            promocao.Categorias.Add(categoriaRestricao);

            PromocaoFaixa faixa1 = new PromocaoFaixa();
            faixa1.Promocao = promocao;
            faixa1.FreteGratis = true;
            faixa1.PercentualDesconto = 10.0m;
            faixa1.ValorMinimo = 50.0m;

            PromocaoFaixa faixa2 = new PromocaoFaixa();
            faixa2.Promocao = promocao;
            faixa2.FreteGratis = true;
            faixa2.PercentualDesconto = 20.0m;
            faixa2.ValorMinimo = 100.0m;

            promocao.PromocaoFaixas = new List<PromocaoFaixa>();

            promocao.PromocaoFaixas.Add(faixa1);
            promocao.PromocaoFaixas.Add(faixa2);

            promocao.CodigoPromocao = "PROMO_CAT";
            promocao.DataHoraFim = DateTime.Now.AddYears(1);
            promocao.DataHoraInicio = DateTime.Now.AddMonths(-1);
            promocao.DescricaoPromocao = "Descrição da promoção de teste com restrição por categoria";
            promocao.NomePromocao = "Promoção de teste com restrição por categoria";
            promocao.PromocaoTipo = new PromocaoTipo(1); // Por quantidade;

            service.InserirPromocao(promocao);
            service.IncluirRestricaoPorCategoria(promocao, categoriaRestricao);

            Assert.IsTrue(promocao.PromocaoId>0);

            foreach (var promocaoFaixa in promocao.PromocaoFaixas)
            {
                Assert.IsTrue(promocaoFaixa.PromocaoFaixaId > 0);
            }
        }

        [TestMethod]
        public void a001_Inserir_Promocao_Automatica_Restrita_Por_Produto()
        {
            PromocaoBLL service = new PromocaoBLL();

            Promocao promocao = new Promocao();
            promocao.AplicaAutomaticamente = true;
            promocao.Ativa = true;

            Produto produto1Restricao = new Produto() { ProdutoId = 42 };
            Produto produto2Restricao = new Produto() { ProdutoId = 43 };

            promocao.Produtos = new List<Produto>();
            promocao.Produtos.Add(produto1Restricao);
            promocao.Produtos.Add(produto2Restricao);

            PromocaoFaixa faixa1 = new PromocaoFaixa();
            faixa1.Promocao = promocao;
            faixa1.FreteGratis = true;
            faixa1.PercentualDesconto = 10.0m;
            faixa1.ValorMinimo = 50.0m;

            PromocaoFaixa faixa2 = new PromocaoFaixa();
            faixa2.Promocao = promocao;
            faixa2.FreteGratis = true;
            faixa2.PercentualDesconto = 20.0m;
            faixa2.ValorMinimo = 100.0m;

            promocao.PromocaoFaixas = new List<PromocaoFaixa>();

            promocao.PromocaoFaixas.Add(faixa1);
            promocao.PromocaoFaixas.Add(faixa2);

            promocao.CodigoPromocao = "PROMO_PROD";
            promocao.DataHoraFim = DateTime.Now.AddYears(1);
            promocao.DataHoraInicio = DateTime.Now.AddMonths(-1);
            promocao.DescricaoPromocao = "Descrição da promoção de teste com restrição por produto";
            promocao.NomePromocao = "Promoção de teste com restrição por produto";
            promocao.PromocaoTipo = new PromocaoTipo(1); // Por quantidade;

            service.InserirPromocao(promocao);
            service.IncluirRestricaoPorProduto(promocao, produto1Restricao);
            service.IncluirRestricaoPorProduto(promocao, produto2Restricao);

            Assert.IsTrue(promocao.PromocaoId > 0);

            foreach (var promocaoFaixa in promocao.PromocaoFaixas)
            {
                Assert.IsTrue(promocaoFaixa.PromocaoFaixaId > 0);
            }
        }

        [TestMethod]
        public void a010_Carregar_Promocoes_Automaticas()
        {
            PromocaoBLL service = new PromocaoBLL();

            Usuario usuario = new Usuario() { UsuarioId = 1 };
            Perfil perfil = new Perfil(1);

            Carrinho carrinho = new Carrinho();

            CarrinhoItem item1 = new CarrinhoItem();
            item1.Produto = new Produto() { ProdutoId = 41 };
            item1.Produto.Peso = 1000;
            item1.Produto.ValorUnitario = 100;
            item1.Produto.ValorOferta = 83;
            item1.Produto.ProdutoTipo = new ProdutoTipo(1);
            item1.Produto.Categorias = new List<Categoria>();
            item1.Produto.Categorias.Add(new Categoria() { CategoriaId = 5 });
            item1.Produto.Categorias.Add(new Categoria() { CategoriaId = 2 });
            item1.Quantidade = 10;

            CarrinhoItem item2 = new CarrinhoItem();
            item2.Produto = new Produto() { ProdutoId = 42 };
            item2.Produto.Peso = 500;
            item2.Produto.ValorUnitario = 50;
            item2.Produto.ProdutoTipo = new ProdutoTipo(2);
            item2.Produto.Categorias = new List<Categoria>();
            item2.Produto.Categorias.Add(new Categoria() { CategoriaId = 3 });
            item2.Quantidade = 10;

            carrinho.AdicionarItem(item1);
            carrinho.AdicionarItem(item2);

            List<Promocao> promocoes = service.CarregarPromocoesAutomaticas(usuario, perfil, carrinho);

            int expected = 1;
            int actual = promocoes.Count();
            Assert.AreEqual(expected, actual);

            expected = 2;
            actual = promocoes.SelectMany(p => p.PromocaoFaixas).Count();
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void a020_Remover_Promocao_Automatica_Restrita_Por_Categoria()
        {
            PromocaoBLL service = new PromocaoBLL();

            Promocao promocaoParaExclusao = service.CarregarPromocaoPeloCodigo("PROMO_CAT");

            service.ExcluirPromocao(promocaoParaExclusao);

            Promocao promocaoExcluida = service.CarregarPromocao(promocaoParaExclusao);

            Assert.IsTrue(promocaoExcluida==null);

        }

        [TestMethod]
        public void a030_Teste()
        {
            Carrinho CarrinhoDeTeste = new Carrinho();

            AdicionarProdutoNormalComValorUnitario(CarrinhoDeTeste);
            AdicionarProdutoNormalComValorOferta(CarrinhoDeTeste);

            #region promocao1
            Promocao promocao1 = new Promocao(1);
            promocao1.AplicaAutomaticamente = true;
            promocao1.Ativa = true;

            Categoria categoriaRestricao1 = new Categoria() { CategoriaId = 1 };
            promocao1.Categorias = new List<Categoria>();
            promocao1.Categorias.Add(categoriaRestricao1);

            PromocaoFaixa p1faixa1 = new PromocaoFaixa();
            p1faixa1.Promocao = promocao1;
            p1faixa1.FreteGratis = true;
            p1faixa1.PercentualDesconto = 10.0m;
            p1faixa1.ValorMinimo = 10;

            PromocaoFaixa p1faixa2 = new PromocaoFaixa();
            p1faixa2.Promocao = promocao1;
            p1faixa2.FreteGratis = true;
            p1faixa2.PercentualDesconto = 50.0m;
            p1faixa2.ValorMinimo = 20;

            promocao1.PromocaoFaixas = new List<PromocaoFaixa>();

            promocao1.PromocaoFaixas.Add(p1faixa1);
            promocao1.PromocaoFaixas.Add(p1faixa2);

            promocao1.CodigoPromocao = "PROMO_CAT";
            promocao1.DataHoraFim = DateTime.Now.AddYears(1);
            promocao1.DataHoraInicio = DateTime.Now.AddMonths(-1);
            promocao1.DescricaoPromocao = "Descrição da promoção de teste com restrição por categoria";
            promocao1.NomePromocao = "Promoção de teste com restrição por categoria";
            promocao1.PromocaoTipo = new PromocaoTipo(1); // Por quantidade;
            #endregion

            #region promocao2
            Promocao promocao2 = new Promocao(2);
            promocao2.AplicaAutomaticamente = true;
            promocao2.Ativa = true;

            Categoria categoriaRestricao2 = new Categoria() { CategoriaId = 2 };
            promocao2.Categorias = new List<Categoria>();
            promocao2.Categorias.Add(categoriaRestricao2);

            PromocaoFaixa p2faixa1 = new PromocaoFaixa();
            p2faixa1.Promocao = promocao2;
            p2faixa1.FreteGratis = true;
            p2faixa1.PercentualDesconto = 10;
            p2faixa1.ValorMinimo = 5;

            PromocaoFaixa p2faixa2 = new PromocaoFaixa();
            p2faixa2.Promocao = promocao2;
            p2faixa2.FreteGratis = true;
            p2faixa2.PercentualDesconto = 80;
            p2faixa2.ValorMinimo = 20;

            promocao2.PromocaoFaixas = new List<PromocaoFaixa>();

            promocao2.PromocaoFaixas.Add(p2faixa1);
            promocao2.PromocaoFaixas.Add(p2faixa2);

            promocao2.CodigoPromocao = "PROMO_CAT";
            promocao2.DataHoraFim = DateTime.Now.AddYears(1);
            promocao2.DataHoraInicio = DateTime.Now.AddMonths(-1);
            promocao2.DescricaoPromocao = "Descrição da promoção de teste com restrição por categoria";
            promocao2.NomePromocao = "Promoção de teste com restrição por categoria";
            promocao2.PromocaoTipo = new PromocaoTipo(1); // Por quantidade;
            #endregion

            #region promocao3
            Promocao promocao3 = new Promocao(3);
            promocao3.AplicaAutomaticamente = true;
            promocao3.Ativa = true;

            Categoria categoriaRestricao3 = new Categoria() { CategoriaId = 2 };
            promocao3.Categorias = new List<Categoria>();
            promocao3.Categorias.Add(categoriaRestricao3);

            PromocaoFaixa p3faixa1 = new PromocaoFaixa();
            p3faixa1.Promocao = promocao3;
            p3faixa1.FreteGratis = true;
            p3faixa1.PercentualDesconto = 50.0m;
            p3faixa1.ValorMinimo = 5;

            PromocaoFaixa p3faixa2 = new PromocaoFaixa();
            p3faixa2.Promocao = promocao3;
            p3faixa2.FreteGratis = true;
            p3faixa2.PercentualDesconto = 50.0m;
            p3faixa2.ValorMinimo = 20;

            promocao3.PromocaoFaixas = new List<PromocaoFaixa>();

            promocao3.PromocaoFaixas.Add(p3faixa1);
            promocao3.PromocaoFaixas.Add(p3faixa2);

            promocao3.CodigoPromocao = "PROMO_CAT";
            promocao3.DataHoraFim = DateTime.Now.AddYears(1);
            promocao3.DataHoraInicio = DateTime.Now.AddMonths(-1);
            promocao3.DescricaoPromocao = "Descrição da promoção de teste com restrição por categoria";
            promocao3.NomePromocao = "Promoção de teste com restrição por categoria";
            promocao3.PromocaoTipo = new PromocaoTipo(1); // Por quantidade;
            #endregion

            List<Promocao> promocoes = new List<Promocao> { promocao1, promocao2, promocao3 };

            PromocaoBLL service = new PromocaoBLL();
            List<Promocao> promocoesVantajodas = service.CalcuarConjuntoDePromocoesMaisVantajosas(CarrinhoDeTeste, promocoes);
        }

        private void AdicionarProdutoNormalComValorUnitario(Carrinho CarrinhoDeTeste)
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

            carrinhoItem.Quantidade = 10;

            CarrinhoDeTeste.AdicionarItem(carrinhoItem);
        }

        private void AdicionarProdutoNormalComValorOferta(Carrinho CarrinhoDeTeste)
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

            const decimal quantidade = 5;

            CarrinhoDeTeste.AdicionarItem(produto, quantidade);
        }

    }
}
