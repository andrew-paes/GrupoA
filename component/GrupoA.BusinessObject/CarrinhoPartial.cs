using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrupoA.BusinessObject
{
    public partial class Carrinho : ICloneable
    {
        private List<PromocaoFaixa> _descontosDoCarrinho;
        private List<Promocao> _promocoes;
        private decimal _valorDoFrete;

        public List<PromocaoFaixa> DescontosDoCarrinho
        {
            get { return _descontosDoCarrinho; }
            set { _descontosDoCarrinho = value; }
        }

        public List<Promocao> Promocoes
        {
            get { return _promocoes; }
            set { _promocoes = value; }
        }

        public decimal ValorDoFrete
        {
            get { return _valorDoFrete; }
            set { _valorDoFrete = value; }
        }

        /// <summary>
        /// Total de itens no carrinho.
        /// </summary>
        public int TotalDeItensNoCarrinho
        {
            get
            {
                if (CarrinhoItens != null)
                {
                    return CarrinhoItens.Count();
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private List<PromocaoFaixa> PromocoesFaixasDoCarrinho
        {
            get
            {
                if (DescontosDoCarrinho == null)
                {
                    DescontosDoCarrinho = new List<PromocaoFaixa>();
                }
                return DescontosDoCarrinho;
            }
            set { DescontosDoCarrinho = value; }
        }

        /// <summary>
        /// Adiciona uma faixa da promoção ao carrinho de compras. 
        /// A promoção só é aplicada aos itens que ainda não posseum nenhuma promoção relacionada e que não são compra conjunta.
        /// </summary>
        /// <param name="promocao">Promoção com suas faixas de desconto.</param>
        public virtual void AdicionarPromocao(Promocao promocao)
        {
            if (promocao == null || promocao.PromocaoFaixas.Count < 1)
            {
                throw new ArgumentException("Promoção inválida ou sem faixas informadas!");
            }

            this.RemoverPromocao(promocao);

            // Somente os itens que não possuem promoção já associada e não são compra conjunta
            IEnumerable<CarrinhoItem> itensPassiveisDaPromocao = CarrinhoItens.Where(item => item.PromocaoFaixaAplicada == null && item.CompraConjuntaDesconto == null).OrderBy(item => item.ValorUnitarioDoItem * item.Quantidade);

            itensPassiveisDaPromocao = AplicarRestricoesDaPromocao(itensPassiveisDaPromocao, promocao); // Verifica se a promoção possui restrição.

            if (itensPassiveisDaPromocao.Count() > 0) // Verifica se a promoção é aplicável
            {
                decimal valorMinimo;

                if (promocao.PromocaoTipo.PromocaoTipoId == (int)TipoDePromocao.PorQuantidade)
                {
                    valorMinimo = itensPassiveisDaPromocao.Sum(i => i.Quantidade);
                }
                else
                {
                    valorMinimo = itensPassiveisDaPromocao.Sum(i => i.ValorUnitarioDoItem * i.Quantidade);
                }

                IEnumerable<PromocaoFaixa> faixasValidas = promocao.PromocaoFaixas.Where(pf => pf.ValorMinimo <= valorMinimo).OrderBy(pf => pf.ValorMinimo).ToList();

                if (faixasValidas.Count() > 0)
                {
                    AplicarFaixaDePromocao(itensPassiveisDaPromocao, faixasValidas.Last(), PromocoesFaixasDoCarrinho);

                    if (Promocoes == null)
                    {
                        Promocoes = new List<Promocao>();
                    }

                    Promocoes.Add(promocao);
                }
            }
        }

        /// <summary>
        /// Aplicar as restrições da promoção aos conjuntos de itens do carrinho.
        /// </summary>
        /// <param name="itensPassiveisDaPromocao">Coleção de itens do carrinho.</param>
        /// <param name="promocao">Promoção a ser aplicada ao carrinho.</param>
        /// <returns></returns>
        private static IEnumerable<CarrinhoItem> AplicarRestricoesDaPromocao(IEnumerable<CarrinhoItem> itensPassiveisDaPromocao, Promocao promocao)
        {
            if (promocao.Produtos != null && promocao.Produtos.Count > 0) // por produto.
            {
                itensPassiveisDaPromocao = itensPassiveisDaPromocao.Where(carrinhoItem => promocao.Produtos.Select(p => p.ProdutoId).Contains(carrinhoItem.Produto.ProdutoId));
            }
            else if (promocao.ProdutoTipos != null && promocao.ProdutoTipos.Count > 0) // por tipo.
            {
                itensPassiveisDaPromocao = itensPassiveisDaPromocao.Where(carrinhoItem => promocao.ProdutoTipos.Select(t => t.ProdutoTipoId).Contains(carrinhoItem.Produto.ProdutoTipo.ProdutoTipoId));
            }
            else if (promocao.Categorias != null && promocao.Categorias.Count > 0) // por tipo.
            {
                itensPassiveisDaPromocao =
                    itensPassiveisDaPromocao.Where(item => (item.Produto.Categorias.Select(c => c.CategoriaId).Intersect(promocao.Categorias.Select(cc => cc.CategoriaId)).Count() > 0));
            }

            return itensPassiveisDaPromocao;
        }

        /// <summary>
        /// Aplica uma faixa de promoção ao carrinho ou para os seus itens.
        /// </summary>
        /// <param name="itensPassiveisDaPromocao">Itens do carrinho passiveis da promoção.</param>
        /// <param name="promocaoFaixa">Faixa da promoção.</param>
        /// <param name="descontosDoCarrinho"></param>
        private static void AplicarFaixaDePromocao(IEnumerable<CarrinhoItem> itensPassiveisDaPromocao, PromocaoFaixa promocaoFaixa, List<PromocaoFaixa> descontosDoCarrinho)
        {
            if (promocaoFaixa != null)
            {
                if (promocaoFaixa.PercentualDesconto.HasValue || promocaoFaixa.ValorDesconto.HasValue || promocaoFaixa.FreteGratis)
                {
                    foreach (CarrinhoItem carrinhoItem in itensPassiveisDaPromocao)
                    {
                        if (promocaoFaixa.ValorDesconto.HasValue)
                        {
                            carrinhoItem.PromocaoFaixaAplicada = promocaoFaixa;
                            break;
                        }
                        else
                        {
                            carrinhoItem.PromocaoFaixaAplicada = promocaoFaixa;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Remove uma promoção do carrinho de compras e seus respectivos descontos dos itens (valor, percentual e frete grátis).
        /// </summary>
        /// <param name="promocao">Promoção a ser removida do carrinho de compras.</param>
        public virtual void RemoverPromocao(Promocao promocao)
        {
            var itensComPromocao = CarrinhoItens.Where(item => item.PromocaoFaixaAplicada != null && item.PromocaoFaixaAplicada.Promocao.PromocaoId == promocao.PromocaoId);

            // Remove a promoção por percentual ou frete grátis dos itens do carrinho.
            foreach (CarrinhoItem carrinhoItem in itensComPromocao)
            {
                carrinhoItem.PromocaoFaixaAplicada = null;
            }

            // Remove a faixa da promoção por valor do carrinho.
            PromocoesFaixasDoCarrinho.RemoveAll(faixa => faixa.Promocao.PromocaoId == promocao.PromocaoId);

            // Remove a promoção do carrinho. 
            if (Promocoes != null)
            {
                Promocoes.RemoveAll(p => p.PromocaoId == promocao.PromocaoId);
            }

        }

        /// <summary>
        /// Remove todas as promoções do carrinho.
        /// </summary>
        public virtual void RemoverTodasPromocoes()
        {
            List<Promocao> promocoesAtuais = Promocoes.ToList();

            foreach (Promocao promocao in promocoesAtuais)
            {
                RemoverPromocao(promocao);
            }
        }

        /// <summary>
        /// Calcula o peso total do pedido.
        /// </summary>
        /// <returns>Peso total do pedido.</returns>
        public virtual decimal PesoTotalDoPedido()
        {
            //return CarrinhoItens.Where(carrinhoItem => (!carrinhoItem.FreteGratis && carrinhoItem.Produto.UtilizaFrete)).Sum(carrinhoItem => carrinhoItem.Produto.Peso * carrinhoItem.Quantidade);

            decimal pesoCarrinho = 0;

            List<CarrinhoItem> carrinhoItemBOList = new List<CarrinhoItem>();
            carrinhoItemBOList = CarrinhoItens.Where(carrinhoItem => (!carrinhoItem.FreteGratis && carrinhoItem.Produto.UtilizaFrete)).ToList();

            foreach (CarrinhoItem carrinhoItemBOTemp in carrinhoItemBOList)
            {
                try
                {
                    pesoCarrinho = pesoCarrinho + (carrinhoItemBOTemp.Produto.Peso * carrinhoItemBOTemp.Quantidade);
                }
                catch { }
            }

            return pesoCarrinho;
        }

        /// <summary>
        /// Totaliza o valor do carrinho.
        /// </summary>
        /// <returns></returns>
        public virtual decimal TotalDoCarrinho()
        {
            // Verifa se todos itens estão com seus produtos configurados.
            bool carrinhoComTodosProdutosValidos = CarrinhoItens.All(p => p.Produto != null && p.Produto.ValorUnitario > 0);
            if (!carrinhoComTodosProdutosValidos)
            {
                throw new Exception("Um ou mais produtos do carrinho são inválidos!");
            }

            decimal totalDoCarrinho = 0m;

            if (CarrinhoItens != null)
            {
                foreach (var carrinhoItem in CarrinhoItens)
                {
                    if (carrinhoItem.CompraConjuntaDesconto != null) // Se for compra conjunta.
                    {
                        totalDoCarrinho += (carrinhoItem.ValorUnitarioDoItem * carrinhoItem.Quantidade);
                    }
                    else
                    {
                        totalDoCarrinho += (carrinhoItem.ValorUnitarioDoItem * carrinhoItem.Quantidade);
                    }
                }
            }

            // Aplica o valor do frete.
            totalDoCarrinho += ValorDoFrete;

            return totalDoCarrinho;
        }

        /// <summary>
        /// Remove um item do carrinho baseado no produto.
        /// </summary>
        /// <param name="carrinhoItem">Item a ser removido do carrinho.</param>
        public void RemoverItem(CarrinhoItem carrinhoItem)
        {
            if (carrinhoItem.CompraConjuntaDesconto != null && carrinhoItem.CompraConjuntaDesconto.CompraConjunta == null)
            {
                throw new ArgumentException("O item contém um desconto de compra conjunta sem compra conjunta relacionada!");
            }

            if (carrinhoItem.Produto == null)
            {
                throw new ArgumentException("Produto não informado!");
            }

            if (carrinhoItem.CompraConjuntaDesconto != null)
            {
                CarrinhoItens.RemoveAll(
                    item => item.CompraConjuntaDesconto != null
                        && (item.CompraConjuntaDesconto.CompraConjunta.CompraConjuntaId == carrinhoItem.CompraConjuntaDesconto.CompraConjunta.CompraConjuntaId
                        && item.Produto.ProdutoId == carrinhoItem.Produto.ProdutoId));
            }
            else
            {
                CarrinhoItens.RemoveAll(item => item.CompraConjuntaDesconto == null && item.Produto.ProdutoId == carrinhoItem.Produto.ProdutoId);
            }

            ReaplicarPromocoes();
        }

        /// <summary>
        /// Adiciona um item ao carrinho. Se ja existir sua quatidade será somada.
        /// </summary>
        /// <param name="produto">Produto a ser adicionado.</param>
        /// <param name="quantidade">Quantidade do produto.</param>
        public void AdicionarItem(Produto produto, decimal quantidade)
        {
            CarrinhoItem novoItem = new CarrinhoItem() { Produto = produto, Quantidade = quantidade };
            AdicionarItem(novoItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="carrinhoItem"></param>
        public void AdicionarItem(CarrinhoItem carrinhoItem)
        {
            this.AdicionarItem(carrinhoItem, true);
        }

        /// <summary>
        /// Adiciona um item ao carrinho. Se ja existir sua quatidade será somada.
        /// </summary>
        /// <param name="carrinhoItem">Item a ser adicionado ao carrinho.</param>
        public void AdicionarItem(CarrinhoItem carrinhoItem, Boolean acrescentar)
        {
            if (CarrinhoItens == null)
            {
                CarrinhoItens = new List<CarrinhoItem>();
            }

            if (carrinhoItem.Quantidade <= 0)
            {
                throw new ArgumentException("Quantidade não informada ou inválida!");
            }

            if (carrinhoItem.Produto == null)
            {
                throw new ArgumentException("Produto não informado!");
            }

            if (carrinhoItem.Produto.ProdutoTipo == null)
            {
                throw new ArgumentException("Tipo do produto não informado!");
            }

            if (carrinhoItem.Produto.Categorias == null || carrinhoItem.Produto.Categorias.Count < 1)
            {
                throw new ArgumentException("O produto deverá conter ao menos uma categoria associada!");
            }

            if (carrinhoItem.CompraConjuntaDesconto != null && carrinhoItem.CompraConjuntaDesconto.CompraConjunta == null)
            {
                throw new ArgumentException("O item contém um desconto de compra conjunta sem compra conjunta relacionada!");
            }

            bool produtoJaExistenteNoCarrinho = false;

            if (carrinhoItem.CompraConjuntaDesconto != null || carrinhoItem.CarrinhoItemCompraConjunta != null) // Verifica se é por compra conjunta.
            {
                // verifica se o item já existe no carrinho.
                //produtoJaExistenteNoCarrinho = CarrinhoItens.Where(item => item.CompraConjuntaDesconto != null).Any(item => (item.Produto.ProdutoId == carrinhoItem.Produto.ProdutoId)
                //                                                                                                    && item.CompraConjuntaDesconto.CompraConjuntaDescontoId == carrinhoItem.CompraConjuntaDesconto.CompraConjuntaDescontoId);
                if (carrinhoItem.CarrinhoItemId > 0)
                {
                    //Carrinho cadastrado
                    produtoJaExistenteNoCarrinho = CarrinhoItens.Any(item => item.CarrinhoItemId == carrinhoItem.CarrinhoItemId);
                }
                else
                {
                    //Carrinhos cookie
                    produtoJaExistenteNoCarrinho = CarrinhoItens.Where(item => item.CompraConjuntaDesconto != null).Any(item => item.Produto.ProdutoId == carrinhoItem.Produto.ProdutoId && (item.CarrinhoItemCompraConjunta == null || item.CompraConjuntaDesconto == null));
                }

                if (produtoJaExistenteNoCarrinho) // se já existe adiciona a quantidade a já existente.
                {
                    //CarrinhoItens.Where(item => (item.Produto.ProdutoId == carrinhoItem.Produto.ProdutoId) && item.CompraConjuntaDesconto.CompraConjuntaDescontoId == carrinhoItem.CompraConjuntaDesconto.CompraConjuntaDescontoId).First().Produto = carrinhoItem.Produto;
                    CarrinhoItens.Where(item => item.CarrinhoItemId == carrinhoItem.CarrinhoItemId).First().Produto = carrinhoItem.Produto;
                    CarrinhoItens.Where(item => item.CarrinhoItemId == carrinhoItem.CarrinhoItemId).First().CarrinhoItemCompraConjunta = carrinhoItem.CarrinhoItemCompraConjunta;
                    CarrinhoItens.Where(item => item.CarrinhoItemId == carrinhoItem.CarrinhoItemId).First().CompraConjuntaDesconto = carrinhoItem.CompraConjuntaDesconto;
                }
                else
                {
                    CarrinhoItens.Add(carrinhoItem);
                }
            }
            else
            {
                // verifica se o item já existe no carrinho.
                //produtoJaExistenteNoCarrinho = CarrinhoItens.Any(item => item.Produto.ProdutoId == carrinhoItem.Produto.ProdutoId);
                if (carrinhoItem.CarrinhoItemId > 0)
                {
                    //Carrinho cadastrado
                    produtoJaExistenteNoCarrinho = CarrinhoItens.Any(item => item.CarrinhoItemId == carrinhoItem.CarrinhoItemId);
                }
                else
                {
                    //Carrinhos cookie
                    produtoJaExistenteNoCarrinho = CarrinhoItens.Any(item => item.Produto.ProdutoId == carrinhoItem.Produto.ProdutoId && item.CarrinhoItemCompraConjunta == null && item.CompraConjuntaDesconto == null);
                }

                if (produtoJaExistenteNoCarrinho) // se já existe adiciona a quantidade a já existente.
                {
                    if (carrinhoItem.CarrinhoItemId > 0)
                    {
                        CarrinhoItens.Where(item => item.CarrinhoItemId == carrinhoItem.CarrinhoItemId).First().Produto = carrinhoItem.Produto;
                    }
                    else
                    {
                        CarrinhoItens.Where(item => item.Produto.ProdutoId == carrinhoItem.Produto.ProdutoId && item.CarrinhoItemCompraConjunta == null && item.CompraConjuntaDesconto == null).First().Produto = carrinhoItem.Produto;
                    }
                }
                else
                {
                    CarrinhoItens.Add(carrinhoItem);
                }
            }

            this.ReaplicarPromocoes();
        }

        /// <summary>
        /// Aplica novamente as promoções no carrinho.
        /// </summary>
        private void ReaplicarPromocoes()
        {
            if (Promocoes != null)
            {
                List<Promocao> promocoesAtuais = Promocoes.ToList();
                RemoverTodasPromocoes();
                foreach (Promocao promocao in promocoesAtuais)
                {
                    AdicionarPromocao(promocao);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            Carrinho novoCarrinho = new Carrinho
                                        {
                                            CarrinhoId = CarrinhoId,
                                            DataHoraCriacao = DataHoraCriacao,
                                            CarrinhoStatus = CarrinhoStatus,
                                            Usuario = Usuario,
                                            ValorDoFrete = ValorDoFrete,
                                            Pedidos = Pedidos,
                                            CarrinhoItens = new List<CarrinhoItem>()
                                        };

            if (CarrinhoItens != null)
            {
                CarrinhoItens.ForEach(item => novoCarrinho.CarrinhoItens.Add(item.Clone() as CarrinhoItem));
            }

            novoCarrinho.Promocoes = Promocoes;
            novoCarrinho.DescontosDoCarrinho = DescontosDoCarrinho;

            return novoCarrinho;
        }
    }
}