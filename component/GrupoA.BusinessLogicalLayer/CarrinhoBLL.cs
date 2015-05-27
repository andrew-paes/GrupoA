using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrupoA.BusinessObject;
using GrupoA.DataAccess;
using GrupoA.DataAccess.ADO;
using GrupoA.BusinessObject.ViewHelper;
using GrupoA.FilterHelper;
using GrupoA.BusinessObject.Enumerator;
using System.Transactions;
using GrupoA.PaymentGateway;
using GrupoA.PaymentGateway.IPagare;
using GrupoA.BusinessLogicalLayer.Helper;
using System.Web;
using System.Configuration;

namespace GrupoA.BusinessLogicalLayer
{
    /// <summary>
    /// Classe que abstrai as regras de negócio referentes ao carrinho.
    /// </summary>
    public class CarrinhoBLL : BaseBLL
    {
        #region Propriedades

        private ICarrinhoDAL _carrinhoDal;
        private ICarrinhoItemDAL _carrinhoItemDal;
        private ICarrinhoItemCompraConjuntaDAL _carrinhoItemCompraConjuntaDal;
        private IProdutoDAL _produtoDal;
        private IMeioPagamentoDAL _meioPagamentoDal;
        private ITransportadoraServicoDAL _transportadoraServicoDal;
        private ICompraConjuntaDAL _compraConjuntaDal;
        private IPedidoDAL _pedidoDal;
        private IPedidoItemDAL _pedidoItemDal;
        private IPedidoItemPromocaoDAL _pedidoItemPromocaoDal;
        private IPedidoItemCompraConjuntaDAL _pedidoItemCompraConjuntaDal;
        private IPedidoEnderecoDAL _pedidoEnderecoDal;
        private IPagamentoDAL _pagamentoDal;
        private IPromocaoCupomDAL _promocaoCupomDal;
        private IPromocaoCupomPedidoDAL _promocaoCupomPedidoDal;
        private IUsuarioControleDAL _usuarioControleDal;

        private ITransportadoraServicoDAL TransportadoraServicoDal
        {
            get { return _transportadoraServicoDal ?? (_transportadoraServicoDal = new TransportadoraServicoADO()); }
        }

        private IMeioPagamentoDAL MeioPagamentoDal
        {
            get { return _meioPagamentoDal ?? (_meioPagamentoDal = new MeioPagamentoADO()); }
        }

        private ICarrinhoItemDAL CarrinhoItemDal
        {
            get { return _carrinhoItemDal ?? (_carrinhoItemDal = new CarrinhoItemADO()); }
        }

        private ICarrinhoDAL CarrinhoDal
        {
            get { return _carrinhoDal ?? (_carrinhoDal = new CarrinhoADO()); }
        }

        private IProdutoDAL ProdutoDal
        {
            get { return _produtoDal ?? (_produtoDal = new ProdutoADO()); }
        }

        private IAutorDAL _autorDal;
        private IAutorDAL AutorDal
        {
            get { return _autorDal ?? (_autorDal = new AutorADO()); }
        }

        private IEnderecoDAL _enderecoDal;

        private IEnderecoDAL EnderecoDal
        {
            get
            {
                if (_enderecoDal == null)
                    _enderecoDal = new EnderecoADO();
                return _enderecoDal;

            }
        }

        private ICarrinhoItemCompraConjuntaDAL CarrinhoItemCompraConjuntaDal
        {
            get { return _carrinhoItemCompraConjuntaDal ?? (_carrinhoItemCompraConjuntaDal = new CarrinhoItemCompraConjuntaADO()); }
        }

        private ICompraConjuntaDAL CompraConjuntaDal
        {
            get { return _compraConjuntaDal ?? (_compraConjuntaDal = new CompraConjuntaADO()); }
        }

        private IPedidoDAL PedidoDal
        {
            get { return _pedidoDal ?? (_pedidoDal = new PedidoADO()); }
        }

        private IPedidoItemDAL PedidoItemDal
        {
            get { return _pedidoItemDal ?? (_pedidoItemDal = new PedidoItemADO()); }
        }

        private IPedidoItemPromocaoDAL PedidoItemPromocaoDal
        {
            get { return _pedidoItemPromocaoDal ?? (_pedidoItemPromocaoDal = new PedidoItemPromocaoADO()); }
        }

        private IPedidoItemCompraConjuntaDAL PedidoItemCompraConjuntaDal
        {
            get { return _pedidoItemCompraConjuntaDal ?? (_pedidoItemCompraConjuntaDal = new PedidoItemCompraConjuntaADO()); }
        }

        private IPedidoEnderecoDAL PedidoEnderecoDal
        {
            get { return _pedidoEnderecoDal ?? (_pedidoEnderecoDal = new PedidoEnderecoADO()); }
        }

        private IPagamentoDAL PagamentoDal
        {
            get { return _pagamentoDal ?? (_pagamentoDal = new PagamentoADO()); }
        }

        private IPromocaoCupomDAL PromocaoCupomDAL
        {
            get { return _promocaoCupomDal ?? (_promocaoCupomDal = new PromocaoCupomADO()); }
        }

        private IPromocaoCupomPedidoDAL PromocaoCupomPedidoDAL
        {
            get { return _promocaoCupomPedidoDal ?? (_promocaoCupomPedidoDal = new PromocaoCupomPedidoADO()); }
        }

        private IUsuarioControleDAL UsuarioControleDal
        {
            get { return _usuarioControleDal ?? (_usuarioControleDal = new UsuarioControleADO()); }
        }

        #endregion

        /// <summary>
        /// Carrega o carrinho aberto
        /// </summary>
        /// <param name="carrinho">Carrinho (deve conter o código identificador)</param>
        /// <param name="totalDeItensDoCarrinho">Total (máximo) de itens por listagem</param>
        /// <returns>Listagem de itens (simplificados) do carrinho</returns>
        public List<CarrinhoItemVH> CarregarCarrinhoSimplificadoAbertoPorUsuario(Carrinho carrinho, int totalDeItensDoCarrinho)
        {
            List<CarrinhoItemVH> carrinhoItens = null;
            // Remove os itens desativados ou inativos
            this.ExcluirDoCarrinhoProdutosDesativadosOuIndisponiveis(carrinho);
            // CarregarAutor
            carrinhoItens = this.CarregarProdutosSimplificadosCarrinho(carrinho, totalDeItensDoCarrinho);
            //}
            return carrinhoItens;
        }

        /// <summary>
        /// Carrega o carrinho aberto 
        /// </summary>
        /// <param name="carrinho">Carrinho (deve conter o código identificador)</param>
        /// <param name="totalDeItensDoCarrinho">Total (máximo) de itens por listagem</param>
        /// <returns>Listagem de itens (simplificados) do carrinho</returns>
        public List<CarrinhoItemVH> CarregarProdutosSimplificadosCarrinho(Carrinho carrinho, int totalDeItensDoCarrinho)
        {
            return ProdutoDal.CarregarSimplificadoPorCarrinho(carrinho, totalDeItensDoCarrinho);
        }

        /// <summary>
        /// Carrega um carrinho do usuário ou insere um novo carrinho se não existir
        /// </summary>
        /// <param name="usuario">Usuário (somente o id é necessário)</param>
        /// <returns>Carrinho de Compras</returns>
        public Carrinho CarregarCarrinhoAbertoPorUsuario(Usuario usuario)
        {
            // Busca somente carrinhos abertos 
            CarrinhoStatus carrinhoStatus = new CarrinhoStatus() { CarrinhoStatusId = (int)StatusDoCarrinho.Aberto, };
            Carrinho carrinho = CarrinhoDal.CarregarAbertoPorUsuario(usuario, carrinhoStatus);

            if (carrinho != null)
            {
                // Antes de carregar os itens do carrinho ele faz a limpeza para remover todos os produtos que foram desativados
                // juntamente com produtos que pertencem à compras conjunto que não são mais válidas
                this.ExcluirDoCarrinhoProdutosDesativadosOuIndisponiveis(carrinho);

                // Carrega carrinho item
                carrinho.CarrinhoItens = CarrinhoItemDal.Carregar(carrinho).ToList<CarrinhoItem>();

                this.CarregarProdutosCarrinho(carrinho);
            }
            return carrinho;
        }

        /// <summary>
        /// Carrega todos os produtos do carrinho
        /// </summary>
        /// <param name="carrinho">Carrinho a ser carregado com produtos</param>
        /// <returns></returns>
        public List<CarrinhoItemVH> CarregarProdutosCarrinho(Carrinho carrinho)
        {
            List<CarrinhoItemVH> produtos = null;
            if (carrinho.CarrinhoId > 0)
            {
                produtos = CarrinhoDal.CarregarPorCarrinho(carrinho);
            }
            else
            {
                produtos = CarrinhoDal.CarregarPorProduto(carrinho);
            }

            foreach (CarrinhoItemVH produto in produtos)
            {
                produto.Autores = EstanteTituloVH.CarregaListaDeAutoresEmStringUnica(AutorDal.CarregarAutores(new Produto() { ProdutoId = produto.ProdutoId }));
            }

            return produtos;
        }

        /// <summary>
        /// Se um produto ainda não existir no carrinho ele é inserido, senão, sua quantidade é incrementada
        /// </summary>
        /// <param name="carrinhoItem">Item do carrinho a receber a operação</param>
        public void InserirOuIncrementarProdutoCarrinho(CarrinhoItem carrinhoItem)
        {
            int totalProdutoCarrinho = 0;
            totalProdutoCarrinho = CarrinhoItemDal.TotalRegistrosComCompraConjunta(carrinhoItem);
            if (totalProdutoCarrinho > 0)
            {
                CarrinhoItemDal.IncrementarQuantidade(carrinhoItem);
            }
            else
            {
                if (carrinhoItem.Quantidade == 0)
                {
                    carrinhoItem.Quantidade = 1;
                }

                CarrinhoItemDal.Inserir(carrinhoItem);
                if (carrinhoItem.CarrinhoItemCompraConjunta != null)
                {
                    CarrinhoItemCompraConjunta carrinhoItemCompraConjunta = new CarrinhoItemCompraConjunta();
                    carrinhoItemCompraConjunta.CarrinhoItemCompraConjuntaId = carrinhoItem.CarrinhoItemId;
                    CompraConjunta compraConjunta = CompraConjuntaDal.CarregarCompraConjuntaEmAbertoPorProduto(carrinhoItem.Produto);
                    if (compraConjunta != null)
                    {
                        carrinhoItemCompraConjunta.CompraConjunta = compraConjunta;
                        CarrinhoItemCompraConjuntaDal.Inserir(carrinhoItemCompraConjunta);
                    }
                }
            }
        }

        /// <summary>
        /// Atualiza as quantidades do itens no carrinho
        /// </summary>
        /// <param name="itens">Itens a terem quantidades atualizadas</param>
        public void AtualizarQuantidadesItensCarrinho(List<CarrinhoItem> itens)
        {
            if (itens != null && itens.Count > 0)
            {
                foreach (CarrinhoItem item in itens)
                {
                    CarrinhoItemDal.AtualizarQuantidade(item);
                }
            }
        }

        /// <summary>
        /// Exclui um item do carrinho
        /// </summary>
        /// <param name="carrinhoItem">Item do carrinho a ser excluído</param>
        public void ExcluirItemCarrinho(CarrinhoItem carrinhoItem)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                CarrinhoItemCompraConjuntaDal.Excluir(new CarrinhoItemCompraConjunta() { CarrinhoItemCompraConjuntaId = carrinhoItem.CarrinhoItemId });
                CarrinhoItemDal.Excluir(carrinhoItem);
                scope.Complete();
            }
        }

        /// <summary>
        /// Efetua o pagamento.
        /// </summary>
        /// <param name="pedidos"></param>
        public RetornoCarrinho EfetuarPagamento(List<Pedido> pedidos, PedidoCartaoCreditoDTO pedidoCartaoCreditoDTO, PedidoRecorrenteCartaoCreditoDTO pedidoRecorrenteCartaoCreditoDTO)
        {
            RetornoCarrinho retornoCarrinho = new RetornoCarrinho();

            // Grava o pedido
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    foreach (Pedido pedido in pedidos)
                    {
                        PagamentoDal.Inserir(pedido.Pagamento);
                        PedidoDal.Inserir(pedido);

                        // Insere pedido compra conjunta
                        if (pedido.PedidoCompraConjunta != null)
                        {
                            pedido.PedidoCompraConjunta.PedidoCompraConjuntaId = pedido.PedidoId;

                            new PedidoCompraConjuntaBLL().Inserir(pedido.PedidoCompraConjunta);
                        }

                        // Insere endereço
                        pedido.PedidoEndereco.Pedido = pedido;
                        //pedido.PedidoEnderecos[0].Pedido.PedidoId = pedido.PedidoId;
                        PedidoEnderecoDal.Inserir(pedido.PedidoEndereco);

                        if (pedido.Promocoes != null)
                        {
                            // Insere promoções do pedido
                            foreach (Promocao promocao in pedido.Promocoes)
                            {
                                if (promocao.PromocaoCupons != null && promocao.PromocaoCupons.Count() > 0)
                                {
                                    PromocaoCupomPedido promocaoCupomPedido = new PromocaoCupomPedido();
                                    promocaoCupomPedido.Pedido = new Pedido(pedido.PedidoId);
                                    promocaoCupomPedido.PromocaoCupom = new PromocaoCupom();
                                    promocaoCupomPedido.PromocaoCupom.PromocaoCupomId = PromocaoCupomDAL.CarregarPorCodigoCupom(promocao.PromocaoCupons[0].CodigoCupom.ToString()).PromocaoCupomId;

                                    PromocaoCupomPedidoDAL.Inserir(promocaoCupomPedido);
                                }
                                else
                                {
                                    Pedido pedidoPromocao = new Pedido();
                                    pedidoPromocao.PedidoId = pedido.PedidoId;
                                    pedidoPromocao.Promocoes = new List<Promocao>();
                                    pedidoPromocao.Promocoes.Add(new Promocao(promocao.PromocaoId));
                                    PedidoDal.InserirPedidoPromocaoCarrinho(pedidoPromocao);
                                }
                            }
                        }

                        // Insere itens do pedido
                        foreach (PedidoItem pedidoItem in pedido.PedidoItens)
                        {
                            pedidoItem.Pedido = pedido;
                            // Insere os itens do pedido em PedidoItem sem compra conjunta
                            PedidoItemDal.Inserir(pedidoItem);
                            // Insere compra conjunta
                            if (pedidoItem.PedidoItemCompraConjunta != null)
                            {
                                pedidoItem.PedidoItemCompraConjunta.PedidoItemCompraConjuntaId = pedidoItem.PedidoItemId;
                                PedidoItemCompraConjuntaDal.Inserir(pedidoItem.PedidoItemCompraConjunta);
                            }
                            else
                            {
                                if (pedidoItem.PedidoItemPromocao != null)
                                {
                                    pedidoItem.PedidoItemPromocao.PedidoItemPromocaoId = pedidoItem.PedidoItemId;

                                    PedidoItemPromocaoDal.Inserir(pedidoItem.PedidoItemPromocao);
                                }
                            }
                        }

                        Carrinho carrinhoAtualizacaoStatus = pedido.Carrinho;
                        carrinhoAtualizacaoStatus.CarrinhoStatus = new CarrinhoStatus() { CarrinhoStatusId = (int)StatusDoCarrinho.Finalizado };
                        CarrinhoDal.AtualizarStatus(carrinhoAtualizacaoStatus);

                        // Cria o pedido no cartão de crédito.
                        if (pedidoCartaoCreditoDTO != null)
                        {
                            pedidoCartaoCreditoDTO.CodigoDoPedido = pedido.PedidoId.ToString();
                            pedidoCartaoCreditoDTO.ValorTotalDoPedido = pedido.ValorPedido;

                            RetornoPedidoDTO retornoPedidoDTO = new ServicoPagamentoCartaoCredito().CriarPedido(pedidoCartaoCreditoDTO);

                            if (Convert.ToInt32(retornoPedidoDTO.CodigoDeRetorno) == 0)
                            {
                                pedido.Pagamento.CodigoTransacao = retornoPedidoDTO.NumeroDaTransacaoNaOperadora;
                                PagamentoDal.Atualizar(pedido.Pagamento);
                            }
                            else
                            {
                                scope.Dispose();
                                PopulaRetornoCarrinho(retornoCarrinho, Convert.ToInt32(retornoPedidoDTO.CodigoDeRetorno));
                                return retornoCarrinho;
                            }
                        }
                        else if (pedidoRecorrenteCartaoCreditoDTO != null)
                        {
                            pedidoRecorrenteCartaoCreditoDTO.CodigoDoPedido = pedido.PedidoId.ToString();
                            pedidoRecorrenteCartaoCreditoDTO.ValorTotalDoPedido = pedido.ValorPedido;

                            RetornoPedidoRecorrenteDTO retornoPedidoRecorrenteDTO = new ServicoPagamentoCartaoCreditoRecorrente().CriarPedido(pedidoRecorrenteCartaoCreditoDTO);

                            if (Convert.ToInt32(retornoPedidoRecorrenteDTO.CodigoDeRetorno) == 0)
                            {
                                pedido.PedidoStatus = new PedidoStatus();
                                pedido.PedidoStatus.PedidoStatusId = 4;
                                PedidoDal.Atualizar(pedido);
                            }
                            else
                            {
                                scope.Dispose();
                                PopulaRetornoCarrinho(retornoCarrinho, Convert.ToInt32(retornoPedidoRecorrenteDTO.CodigoDeRetorno));
                                return retornoCarrinho;
                            }
                        }
                        else if (pedidoCartaoCreditoDTO == null && pedidoRecorrenteCartaoCreditoDTO == null)
                        {
                            pedido.PedidoStatus = new PedidoStatus();
                            pedido.PedidoStatus.PedidoStatusId = 4;
                            PedidoDal.Atualizar(pedido);
                        }

                        // Forçar Sincronização sempre que o Usuário realizar um Pedido
                        UsuarioControle usuarioControle = new UsuarioControle();
                        usuarioControle.Usuario = pedido.Usuario;
                        usuarioControle.RealizarSincronizacao = true;
                        usuarioControle.UsuarioId = pedido.Usuario.UsuarioId;
                        UsuarioControleDal.AtualizarStatusSincronizacao(usuarioControle);
                    }
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    retornoCarrinho.CodigoRetorno = -1;
                    retornoCarrinho.MensagemRetorno = ex.Message + "<br/>" + ex.StackTrace;

                    return retornoCarrinho;
                }

                scope.Complete();
            }

            retornoCarrinho.CodigoRetorno = 0;
            retornoCarrinho.MensagemRetorno = "Sucesso";

            return retornoCarrinho;
        }

        /// <summary>
        /// Efetua o pagamento de compra coletiva.
        /// </summary>
        /// <param name="pedidoBO"></param>
        /// <param name="pedidoCartaoCreditoDTO"></param>
        /// <returns></returns>
        public Pedido EfetuarPagamento(Pedido pedidoBO, PedidoCartaoCreditoDTO pedidoCartaoCreditoDTO)
        {
            string toKenCofre = String.Empty;
            
            using (TransactionScope scope = new TransactionScope()) // Grava o pedido
            {
                try
                {
                    PagamentoDal.Inserir(pedidoBO.Pagamento);

                    if (pedidoBO.Pagamento != null && pedidoBO.Pagamento.PagamentoId > 0)
                    {
                        PedidoDal.Inserir(pedidoBO);

                        if (pedidoBO != null && pedidoBO.PedidoId > 0)
                        {
                            if (pedidoCartaoCreditoDTO != null) // Cria o token no cofre.
                            {
                                pedidoCartaoCreditoDTO.CodigoDoPedido = pedidoBO.PedidoId.ToString(); // Usado no Log dentro do serviço de cofre

                                RetornoCofreDTO retornoCofreDTO = null;

                                using (TransactionScope scope2 = new TransactionScope(TransactionScopeOption.Suppress))
                                {
                                    retornoCofreDTO = new ServicoPagamentoCofre().CriarToken(pedidoCartaoCreditoDTO);
                                }

                                if (
                                    retornoCofreDTO != null
                                    && !String.IsNullOrEmpty(retornoCofreDTO.MensagemDeRetorno)
                                    && retornoCofreDTO.MensagemDeRetorno == "Sucesso"
                                    && !String.IsNullOrEmpty(retornoCofreDTO.Token)
                                    )
                                {
                                    pedidoBO.PedidoCompraConjunta.PedidoCompraConjuntaId = pedidoBO.PedidoId;
                                    pedidoBO.PedidoCompraConjunta.TokenCofre = retornoCofreDTO.Token;

                                    new PedidoCompraConjuntaBLL().Inserir(pedidoBO.PedidoCompraConjunta); // Insere pedido compra conjunta

                                    pedidoBO.PedidoEndereco.Pedido = pedidoBO;
                                    PedidoEnderecoDal.Inserir(pedidoBO.PedidoEndereco); // Insere endereço

                                    if (pedidoBO.PedidoItens != null && pedidoBO.PedidoItens.Any())
                                    {
                                        foreach (PedidoItem pedidoItemBOTemp in pedidoBO.PedidoItens) // Insere itens do pedido
                                        {
                                            pedidoItemBOTemp.Pedido = pedidoBO;

                                            PedidoItemDal.Inserir(pedidoItemBOTemp); // Insere os itens do pedido em PedidoItem sem compra conjunta

                                            pedidoItemBOTemp.PedidoItemCompraConjunta = new PedidoItemCompraConjunta();
                                            pedidoItemBOTemp.PedidoItemCompraConjunta.PedidoItemCompraConjuntaId = pedidoItemBOTemp.PedidoItemId;
                                            pedidoItemBOTemp.PedidoItemCompraConjunta.CompraConjunta = pedidoBO.PedidoCompraConjunta.CompraConjunta;
                                            PedidoItemCompraConjuntaDal.Inserir(pedidoItemBOTemp.PedidoItemCompraConjunta); // Insere os itens de compra conjunta
                                        }
                                    }

                                    scope.Complete();
                                }
                                else
                                {
                                    throw new Exception(retornoCofreDTO.MensagemDeRetorno);
                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    //scope.Complete();
                }
            }

            return pedidoBO;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="retornoCarrinho"></param>
        /// <param name="codigoDeRetorno"></param>
        private static void PopulaRetornoCarrinho(RetornoCarrinho retornoCarrinho, Int32 codigoDeRetorno)
        {
            switch (codigoDeRetorno)
            {
                case 101:
                case 102:
                case 106:
                case 107:
                case 112:
                case 113:
                case 114:
                case 115:
                case 116:
                case 117:
                case 118:
                case 119:
                case 206:
                case 208:
                    retornoCarrinho.CodigoRetorno = codigoDeRetorno;
                    retornoCarrinho.MensagemRetorno = GrupoA.GlobalResources.GrupoA_MensagensIPagare.ErroSistema;
                    break;
                case 201:
                    retornoCarrinho.CodigoRetorno = codigoDeRetorno;
                    retornoCarrinho.MensagemRetorno = GrupoA.GlobalResources.GrupoA_MensagensIPagare.PagamentoJaProcessado;
                    break;
                case 202:
                    retornoCarrinho.CodigoRetorno = codigoDeRetorno;
                    retornoCarrinho.MensagemRetorno = GrupoA.GlobalResources.GrupoA_MensagensIPagare.PedidoCancelaNaoPodeSerPago;
                    break;
                case 203:
                    retornoCarrinho.CodigoRetorno = codigoDeRetorno;
                    retornoCarrinho.MensagemRetorno = GrupoA.GlobalResources.GrupoA_MensagensIPagare.LimiteValorExcedido;
                    break;
                case 204:
                    retornoCarrinho.CodigoRetorno = codigoDeRetorno;
                    retornoCarrinho.MensagemRetorno = GrupoA.GlobalResources.GrupoA_MensagensIPagare.ErroProcessarPedido;
                    break;
                case 205:
                    retornoCarrinho.CodigoRetorno = codigoDeRetorno;
                    retornoCarrinho.MensagemRetorno = GrupoA.GlobalResources.GrupoA_MensagensIPagare.OpcaoPagamentoIndisponivel;
                    break;
                case 207:
                    retornoCarrinho.CodigoRetorno = codigoDeRetorno;
                    retornoCarrinho.MensagemRetorno = GrupoA.GlobalResources.GrupoA_MensagensIPagare.OpcaoPagamentoIndisponivelTenteNovamente;
                    break;
                case 209:
                    retornoCarrinho.CodigoRetorno = codigoDeRetorno;
                    retornoCarrinho.MensagemRetorno = GrupoA.GlobalResources.GrupoA_MensagensIPagare.PagamentoNaoFoiCapturadoNaoPodeSerCancelado;
                    break;
                case 210:
                    retornoCarrinho.CodigoRetorno = codigoDeRetorno;
                    retornoCarrinho.MensagemRetorno = GrupoA.GlobalResources.GrupoA_MensagensIPagare.EstabelecimentoNaoPodeCancelarSoOperadoraCartao;
                    break;
                case 211:
                    retornoCarrinho.CodigoRetorno = codigoDeRetorno;
                    retornoCarrinho.MensagemRetorno = GrupoA.GlobalResources.GrupoA_MensagensIPagare.FuncionalidadeNaoEstaHabilitada;
                    break;
                case 212:
                    retornoCarrinho.CodigoRetorno = codigoDeRetorno;
                    retornoCarrinho.MensagemRetorno = GrupoA.GlobalResources.GrupoA_MensagensIPagare.NaoFoiPossivelValidarCartaoCredito;
                    break;
                case 213:
                    retornoCarrinho.CodigoRetorno = codigoDeRetorno;
                    retornoCarrinho.MensagemRetorno = GrupoA.GlobalResources.GrupoA_MensagensIPagare.PedidoNaoPossuiPagamentoPorCartaoCreditoNaoPodeSerCancelado;
                    break;
                case 214:
                    retornoCarrinho.CodigoRetorno = codigoDeRetorno;
                    retornoCarrinho.MensagemRetorno = GrupoA.GlobalResources.GrupoA_MensagensIPagare.PedidoJaEstaCancelado;
                    break;
                case 215:
                    retornoCarrinho.CodigoRetorno = codigoDeRetorno;
                    retornoCarrinho.MensagemRetorno = GrupoA.GlobalResources.GrupoA_MensagensIPagare.NaoFoiPossivelCancelarPagamento;
                    break;
                case 216:
                    retornoCarrinho.CodigoRetorno = codigoDeRetorno;
                    retornoCarrinho.MensagemRetorno = GrupoA.GlobalResources.GrupoA_MensagensIPagare.ValorCanceladoNaoPodeSerMaiorQueValorTotal;
                    break;
                case 217:
                    retornoCarrinho.CodigoRetorno = codigoDeRetorno;
                    retornoCarrinho.MensagemRetorno = GrupoA.GlobalResources.GrupoA_MensagensIPagare.ValorCanceladoNaoPodeSerMaiorQueValorRestante;
                    break;
                case 218:
                    retornoCarrinho.CodigoRetorno = codigoDeRetorno;
                    retornoCarrinho.MensagemRetorno = GrupoA.GlobalResources.GrupoA_MensagensIPagare.ObrigatorioEnvioValorTotalRecorrencia;
                    break;
                case 219:
                    retornoCarrinho.CodigoRetorno = codigoDeRetorno;
                    retornoCarrinho.MensagemRetorno = GrupoA.GlobalResources.GrupoA_MensagensIPagare.RecorrenciasNaoEstaHabilitado;
                    break;
                case 220:
                    retornoCarrinho.CodigoRetorno = codigoDeRetorno;
                    retornoCarrinho.MensagemRetorno = GrupoA.GlobalResources.GrupoA_MensagensIPagare.ErroCapturaPagamento;
                    break;
                case 221:
                    retornoCarrinho.CodigoRetorno = codigoDeRetorno;
                    retornoCarrinho.MensagemRetorno = GrupoA.GlobalResources.GrupoA_MensagensIPagare.PagamentoJaFoiCapturado;
                    break;
                case 222:
                    retornoCarrinho.CodigoRetorno = codigoDeRetorno;
                    retornoCarrinho.MensagemRetorno = GrupoA.GlobalResources.GrupoA_MensagensIPagare.PedidoNaoPossuiPagamentoPorCartaoCreditoNaoPodeSerCapturado;
                    break;
                case 223:
                    retornoCarrinho.CodigoRetorno = codigoDeRetorno;
                    retornoCarrinho.MensagemRetorno = GrupoA.GlobalResources.GrupoA_MensagensIPagare.PedidoNaoPossuiPagamento;
                    break;
                case 224:
                    retornoCarrinho.CodigoRetorno = codigoDeRetorno;
                    retornoCarrinho.MensagemRetorno = GrupoA.GlobalResources.GrupoA_MensagensIPagare.ModuloCofreNaoEstaHabilitado;
                    break;
                case 225:
                    retornoCarrinho.CodigoRetorno = codigoDeRetorno;
                    retornoCarrinho.MensagemRetorno = GrupoA.GlobalResources.GrupoA_MensagensIPagare.MeioPagamentoNaoWebservice;
                    break;
                case 226:
                    retornoCarrinho.CodigoRetorno = codigoDeRetorno;
                    retornoCarrinho.MensagemRetorno = GrupoA.GlobalResources.GrupoA_MensagensIPagare.TokenExpirado;
                    break;
                case 227:
                    retornoCarrinho.CodigoRetorno = codigoDeRetorno;
                    retornoCarrinho.MensagemRetorno = GrupoA.GlobalResources.GrupoA_MensagensIPagare.TokenNaoPodeSerAlterado;
                    break;
                case 228:
                    retornoCarrinho.CodigoRetorno = codigoDeRetorno;
                    retornoCarrinho.MensagemRetorno = GrupoA.GlobalResources.GrupoA_MensagensIPagare.SessaoExpirada;
                    break;
                case 231:
                    retornoCarrinho.CodigoRetorno = codigoDeRetorno;
                    retornoCarrinho.MensagemRetorno = GrupoA.GlobalResources.GrupoA_MensagensIPagare.AcessoNegadoAmbienteExclusivoContasGratis;
                    break;
                case 232:
                    retornoCarrinho.CodigoRetorno = codigoDeRetorno;
                    retornoCarrinho.MensagemRetorno = GrupoA.GlobalResources.GrupoA_MensagensIPagare.PedidoExpiradoNaoPodeSerPago;
                    break;
                case 301:
                    retornoCarrinho.CodigoRetorno = codigoDeRetorno;
                    retornoCarrinho.MensagemRetorno = GrupoA.GlobalResources.GrupoA_MensagensIPagare.PedidoNaoAutorizadoContateAdmCartaoCredito;
                    break;
                case 302:
                    retornoCarrinho.CodigoRetorno = codigoDeRetorno;
                    retornoCarrinho.MensagemRetorno = GrupoA.GlobalResources.GrupoA_MensagensIPagare.OpcaoPagamentoIndisponivelTenteNovamente;
                    break;
                case 303:
                    retornoCarrinho.CodigoRetorno = codigoDeRetorno;
                    retornoCarrinho.MensagemRetorno = GrupoA.GlobalResources.GrupoA_MensagensIPagare.CartaoCreditoInvalido;
                    break;
                case 304:
                    retornoCarrinho.CodigoRetorno = codigoDeRetorno;
                    retornoCarrinho.MensagemRetorno = GrupoA.GlobalResources.GrupoA_MensagensIPagare.SomenteRecorrenciasComOcorrenciaAbertaPodemSerAlteradas;
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void TesteEmail()
        {
            try
            {
                Dictionary<string, string> dicionarioDados = new Dictionary<string, string>();
                string emailDestino = string.Empty;
                string assuntoEmail = "Teste";
                string caminhotemplate = string.Empty;

                //dicionarioDados.Add("NomeAmigo", txtNomeDoAmigo.Value);
                //dicionarioDados.Add("Nome", txtSeuNome.Value);

                // Carrega o caminho do template
                caminhotemplate = HttpContext.Current.Server.MapPath("~/_emails/teste.aspx?id=123");

                StringBuilder templateEmail = new EmailHelper().PopulaTemplateEmail(dicionarioDados, caminhotemplate);

                new EmailHelper().EnviarEmail("teste@ag2.com.br", "silvaap@ag2.combr", assuntoEmail, templateEmail);
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Persiste um novo carrinho de compra.
        /// </summary>
        /// <param name="carrinho">Carrinho de compra a ser persistido.</param>
        public void InserirNovoCarrinho(Carrinho carrinho)
        {
            carrinho.CarrinhoStatus = new CarrinhoStatus((int)StatusDoCarrinho.Aberto);
            carrinho.DataHoraCriacao = DateTime.Now;

            // Cria carrinho
            CarrinhoDal.Inserir(carrinho);
        }

        /// <summary>
        /// Atualiza um carrinho de compras
        /// </summary>
        /// <param name="carrinho">Carrinho que será atualizado</param>
        public void AtualizarCarrinho(Carrinho carrinho)
        {
            // Exclui os itens do carrinho com compra conjunta
            CarrinhoItemCompraConjuntaDal.ExcluirPorCarrinho(carrinho);

            // Exclui os itens do carrinho sem compra conjunta
            CarrinhoItemDal.ExcluirPorCarrinho(carrinho);

            CarrinhoDal.Atualizar(carrinho);
            foreach (CarrinhoItem carrinhoItem in carrinho.CarrinhoItens)
            {
                carrinhoItem.Carrinho = carrinho;
                CarrinhoItemDal.Inserir(carrinhoItem);
                if (carrinhoItem.CarrinhoItemCompraConjunta != null)
                {
                    // Deve buscar a compra conjunta ativa
                    CarrinhoItemCompraConjunta carrinhoItemCompraConjunta = new CarrinhoItemCompraConjunta();
                    carrinhoItemCompraConjunta.CarrinhoItemCompraConjuntaId = carrinhoItem.CarrinhoItemId;
                    carrinhoItemCompraConjunta.CompraConjunta = CompraConjuntaDal.CarregarCompraConjuntaEmAbertoPorProduto(carrinhoItem.Produto);
                    CarrinhoItemCompraConjuntaDal.Inserir(carrinhoItemCompraConjunta);
                }
            }
        }

        /// <summary>
        /// Calcula o frete do carrinho de compras
        /// </summary>
        /// <param name="cepInicial">Cep Inicial do cliente 90000 - 99999</param>
        /// <param name="cepFinal">Cep Final 000 - 999</param>
        /// <param name="peso">peso da soma dos produtos</param>
        /// <returns></returns>
        public double CalculaFrete(int cepInicial, int cepFinal, decimal peso)
        {
            return CarrinhoDal.CalculaFrete(cepInicial, cepFinal, peso);
        }

        /// <summary>
        /// Carrega todos os enderecos do usuário
        /// </summary>
        /// <param name="usuario">Usuário que possui os endereços(somente o id é necessário)</param>
        /// <returns>Listagem de endereços do usuário</returns>
        public List<Endereco> RetornaEnderecosUsuario(Usuario usuario)
        {
            return this.EnderecoDal.CarregarEnderecosUsuario(usuario);
        }

        /// <summary>
        /// Carrega o endereco de entrega
        /// </summary>
        /// <param name="endereco">Endereco (somente o id é necessário)</param>
        /// <returns>Endereço populado</returns>
        public Endereco CarregarEnderecoEntrega(Endereco endereco)
        {
            return EnderecoDal.CarregarEnderecoComDependencias(endereco);
        }

        /// <summary>
        /// Verifica se um cep é atendido
        /// </summary>
        /// <param name="cep1">Início do CEP</param>
        /// <param name="cep2">Final do CEP</param>
        /// <returns>True/False indicando se o CEP é ou não atendido</returns>
        public bool CepAtendido(int cep1, int cep2)
        {
            return this.CalculaFrete(cep1, cep2, 1) > 0;
        }

        /// <summary>
        /// Carrega todos os meios de pagamento maiores que o valor mínimo
        /// </summary>
        /// <param name="valorMinimo">Valor mínimo</param>
        /// <returns>Listagem dos meios de pagamentos válidos</returns>
        public List<MeioPagamento> CarregarMeiosPagamentoMaioresQueValor(double valorMinimo)
        {
            return MeioPagamentoDal.CarregarMeiosPagamentoMaioresQueValor(valorMinimo);
        }

        /// <summary>
        /// Carrega o serviço de transporte
        /// </summary>
        /// <param name="transportadoraServico">serviço de transporte</param>
        /// <returns>serviço de transporte populado</returns>
        public TransportadoraServico CarregarTransportadoraServico(TransportadoraServico transportadoraServico)
        {
            return TransportadoraServicoDal.Carregar(transportadoraServico);
        }

        /// <summary>
        /// Exclui do carrinho de compras todos os produtos que foram desativados ou estão indisponíveis
        /// </summary>
        /// <param name="carrinho">Carrinho (somente o id é necessário)</param>
        public void ExcluirDoCarrinhoProdutosDesativadosOuIndisponiveis(Carrinho carrinho)
        {
            // Busca os itens do carrinho de compra conjunta
            List<CarrinhoItemCompraConjunta> carrinhoitensCompraConjunta = CarrinhoItemCompraConjuntaDal.CarregarPorCompraConjuntaIndisponivel(carrinho);
            using (TransactionScope scope = new TransactionScope())
            {
                string itensExcluidosCompraConjunta = string.Empty;
                // Exclui todos os itens de compra conjunta do carrinho
                if (carrinhoitensCompraConjunta.Count > 0)
                {
                    itensExcluidosCompraConjunta = CarrinhoItemCompraConjuntaDal.ExcluirMultiplos(carrinhoitensCompraConjunta);
                }
                // Exclui os demais itens
                CarrinhoItemDal.ExcluirDesativadosPorCarrinho(carrinho, itensExcluidosCompraConjunta);
                scope.Complete();
            }
        }

        /// <summary>
        /// Calcula o frete conforme o cep e peso
        /// </summary>
        /// <param name="endereco">endereco que contém o cep a ser pesquisado</param>
        /// <param name="pesoTotal">peso</param>
        /// <returns>Valor do frete</returns>
        public double CalcularFrete(Endereco endereco, decimal pesoTotal)
        {
            if (string.IsNullOrEmpty(endereco.Cep))
            {
                return 0.0;
            }
            int cepInicial = Convert.ToInt32(endereco.Cep.Substring(0, 5));
            int cepFinal = Convert.ToInt32(endereco.Cep.Substring(4, 3));
            double valorFrete = CalculaFrete(cepInicial, cepFinal, pesoTotal);
            //ltrValorFrete.Text = valorFrete.ToString("c");
            return valorFrete;
            //hddValorFrete.Value = valorFrete.ToString();
        }

        /// <summary>
        /// Carrega todos os pedidos por carrinho
        /// </summary>
        /// <param name="carrinho">Carrinho a serem carregados os pedidos (somente o id é necessário)</param>
        /// <returns>Listagem (viewhelper) que contém todos os itens de confirmação do carrinho</returns>
        public List<ConfirmacaoPedidoVH> CarregarPedidosPorCarrinho(Carrinho carrinho)
        {
            return PedidoDal.CarregarComDependenciasPorCarrinho(carrinho).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="confirmacaoPedido"></param>
        /// <returns></returns>
        public Boolean EnviarEmailPedido(List<ConfirmacaoPedidoVH> confirmacaoPedidos, String caminhotemplate)
        {
            try
            {
                Usuario usuario = new UsuarioBLL().CarregarComDependencia(confirmacaoPedidos[0].Usuario);

                foreach (ConfirmacaoPedidoVH confirmacaoPedidoVH in confirmacaoPedidos)
                {
                    Dictionary<string, string> dicionarioDados = new Dictionary<string, string>();
                    StringBuilder sbTitulos = new StringBuilder();
                    Decimal valorSubtotal = 0;
                    Decimal valorDescontoTotal = 0;

                    foreach (PedidoItemVH pedidoItemVH in confirmacaoPedidoVH.PedidoItensVH)
                    {
                        Decimal valorDesconto = 0;

                        try
                        {
                            valorDesconto = this.CalcularCupomDesconto(pedidoItemVH, confirmacaoPedidoVH.PedidoId);
                        }
                        catch { }

                        sbTitulos.Append("<tr>");
                        sbTitulos.Append("<td width='510'>");
                        sbTitulos.Append("<table width='510' border='0' cellpadding='4'>");
                        sbTitulos.Append("<tr>");
                        sbTitulos.Append("<td width='10'><img style='display:block;' src='" + ConfigurationManager.AppSettings["sitePath"].ToString() + "_email/img/vazio.gif' width='1' height='1'></td>");
                        sbTitulos.Append("<td style='padding:10px 0;'>");
                        sbTitulos.Append("<font face='Arial' size='2' color='#ac1220'>");
                        sbTitulos.Append(pedidoItemVH.NomeProduto);
                        sbTitulos.Append("</font>");
                        sbTitulos.Append("</td>");
                        sbTitulos.Append(String.Concat("<td width='30' align='center'><font face='Arial' size='2' color='#222222'>", Convert.ToInt32(pedidoItemVH.Quantidade), "</font></td>"));
                        sbTitulos.Append(String.Concat("<td width='70' align='right'><font face='Arial' size='2' color='#222222'>", valorDesconto.ToString("c"), "</font></td>"));
                        sbTitulos.Append(String.Concat("<td width='70' align='right'><font face='Arial' size='2' color='#222222'>", pedidoItemVH.ValorUnitario.ToString("C"), "</font></td>"));
                        sbTitulos.Append(String.Concat("<td width='84' align='right'><font face='Arial' size='2' color='#222222'>", (pedidoItemVH.ValorFinal * pedidoItemVH.Quantidade).ToString("C"), "</font></td>"));
                        sbTitulos.Append("</tr>");
                        sbTitulos.Append("</table>");
                        sbTitulos.Append("</td>");
                        sbTitulos.Append("</tr>");
                        sbTitulos.Append("<tr>");
                        sbTitulos.Append("<td width='510' height='1' colspan='4' bgcolor='#d9d9d9'><img style='display:block;' src='" + ConfigurationManager.AppSettings["sitePath"].ToString() + "_email/img/vazio.gif' width='1' height='1'></td>");
                        sbTitulos.Append("</tr>");

                        valorDescontoTotal += valorDesconto;
                        valorSubtotal += (pedidoItemVH.ValorFinal * pedidoItemVH.Quantidade);
                    }

                    dicionarioDados.Add("Usuario", usuario.NomeUsuario);
                    dicionarioDados.Add("PedidoId", confirmacaoPedidoVH.PedidoId.ToString());
                    dicionarioDados.Add("Itens", sbTitulos.ToString());
                    dicionarioDados.Add("SubTotal", valorSubtotal.ToString("c"));
                    dicionarioDados.Add("Descontos", valorDescontoTotal.ToString("c"));
                    dicionarioDados.Add("Frete", confirmacaoPedidoVH.FreteValor.ToString("c"));
                    dicionarioDados.Add("Total", confirmacaoPedidoVH.ValorPedido.ToString("c"));
                    dicionarioDados.Add("DataCompra", confirmacaoPedidoVH.DataHoraPedido.ToShortDateString());
                    dicionarioDados.Add("FormaPagamento", confirmacaoPedidoVH.Pagamento.MeioPagamento.Nome);

                    if (confirmacaoPedidoVH.Pagamento.MeioPagamento.Nome.Trim() == "Boleto Bancário")
                    {
                        dicionarioDados.Add("BoletoBancario", String.Format("<tr><td width='510' align='right'><a href='{0}carrinho/ConsultarBoletoBancario.aspx?pedidoId={1}'><img style='display:block;' src='{0}_emails/confirmacao-pedido/img/img_04.jpg' width='116' height='32' alt='Imprimir boleto' border='0'></a></td></tr>", ConfigurationManager.AppSettings["sitePath"].ToString(), confirmacaoPedidoVH.PedidoId));
                    }
                    else
                    {
                        dicionarioDados.Add("BoletoBancario", String.Empty);
                    }


                    dicionarioDados.Add("TipoEndereco", confirmacaoPedidoVH.PedidoEndereco.EnderecoTipo.Tipo);
                    dicionarioDados.Add("Endereco", string.Format("{0}, {1} {2}", confirmacaoPedidoVH.PedidoEndereco.Logradouro, confirmacaoPedidoVH.PedidoEndereco.Numero, (confirmacaoPedidoVH.PedidoEndereco.Complemento != null ? string.Concat(" - ", confirmacaoPedidoVH.PedidoEndereco.Complemento) : string.Empty)));
                    dicionarioDados.Add("Bairro", confirmacaoPedidoVH.PedidoEndereco.Bairro);
                    dicionarioDados.Add("Cidade", confirmacaoPedidoVH.PedidoEndereco.Municipio.NomeMunicipio);
                    dicionarioDados.Add("Estado", confirmacaoPedidoVH.PedidoEndereco.Municipio.Regiao.NomeRegiao);
                    dicionarioDados.Add("Cep", confirmacaoPedidoVH.PedidoEndereco.Cep);
                    dicionarioDados.Add("Email", usuario.EmailUsuario);
                    dicionarioDados.Add("ServicoEntrega", this.CarregarTransportadoraServico(confirmacaoPedidoVH.TransportadoraServico).NomeServicoe);

                    dicionarioDados.Add("UrlSite", ConfigurationManager.AppSettings["sitePath"].ToString());

                    StringBuilder templateEmail = new EmailHelper().PopulaTemplateEmail(dicionarioDados, caminhotemplate);

                    new EmailHelper().EnviarEmail(ConfigurationManager.AppSettings["emailContato"].ToString(), usuario.EmailUsuario, "Pedido processado", templateEmail);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pedidoItemVH"></param>
        /// <returns></returns>
        private decimal CalcularCupomDesconto(PedidoItemVH pedidoItemVH, int pedidoId)
        {
            decimal valorDesconto = 0;

            if (pedidoId != null && pedidoId > 0 && pedidoItemVH != null && pedidoItemVH.ProdutoId > 0)
            {
                try
                {
                    PedidoItem pedidoItemBO = new PedidoItem();

                    Pedido pedidoBO = new Pedido();
                    pedidoBO.PedidoId = pedidoId;

                    Produto produtoBO = new Produto();
                    produtoBO.ProdutoId = pedidoItemVH.ProdutoId;

                    pedidoItemBO = new PedidoItemBLL().Carregar(pedidoBO, produtoBO);

                    if (pedidoItemBO != null && pedidoItemBO.PedidoItemId > 0 && pedidoItemBO.ValorUnitarioBase > 0)
                    {
                        PedidoItemPromocao pedidoItemPromocaoBO = new PedidoItemPromocao();
                        pedidoItemPromocaoBO = new PedidoItemPromocaoBLL().Carregar(new PedidoItemPromocao { PedidoItemPromocaoId = pedidoItemBO.PedidoItemId });

                        if (pedidoItemPromocaoBO != null && pedidoItemPromocaoBO.PedidoItemPromocaoId > 0)
                        {
                            if (pedidoItemPromocaoBO.DescontoValor != null && pedidoItemPromocaoBO.DescontoValor.Value > 0)
                            {
                                valorDesconto = pedidoItemPromocaoBO.DescontoValor.Value; // *pedidoItemBO.Quantidade;
                            }
                            else if (pedidoItemPromocaoBO.DescontoPercentual != null && pedidoItemPromocaoBO.DescontoPercentual.Value > 0)
                            {
                                valorDesconto = ((pedidoItemPromocaoBO.DescontoPercentual.Value / 100) * pedidoItemBO.ValorUnitarioBase) * pedidoItemBO.Quantidade;
                            }
                        }
                    }
                }
                catch { }
            }

            return valorDesconto;
        }
    }
}