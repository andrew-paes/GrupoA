using System.Collections.Generic;
using GrupoA.DataAccess;
using GrupoA.BusinessObject;
using GrupoA.DataAccess.ADO;
using GrupoA.FilterHelper;
using System.Transactions;
using System;
using GrupoA.BusinessObject.Enumerator;
using System.Text;
using GrupoA.BusinessLogicalLayer.Helper;
using GrupoA.PaymentGateway;
using GrupoA.PaymentGateway.IPagare;
using System.Configuration;

namespace GrupoA.BusinessLogicalLayer
{
    public class CompraConjuntaBLL : BaseBLL
    {
        #region Declarações DAL

        private ICompraConjuntaDAL _compraConjuntaDAL;
        private ICompraConjuntaDAL CompraConjuntaDAL
        {
            get
            {
                if (_compraConjuntaDAL == null)
                    _compraConjuntaDAL = new CompraConjuntaADO();
                return _compraConjuntaDAL;
            }
        }

        private ICompraConjuntaDescontoDAL _compraConjuntaDescontoDAL;
        private ICompraConjuntaDescontoDAL CompraConjuntaDescontoDAL
        {
            get
            {
                if (_compraConjuntaDescontoDAL == null)
                    _compraConjuntaDescontoDAL = new CompraConjuntaDescontoADO();
                return _compraConjuntaDescontoDAL;
            }
        }

        #endregion

        #region Métodos: CompraConjunta

        /// <summary>
        /// 
        /// </summary>
        /// <param name="compraConjuntaPaginaId"></param>
        /// <returns></returns>
        public IEnumerable<CompraConjunta> CarregarCompraConjuntaComPaginaRelacionada(int compraConjuntaPaginaId)
        {
            return CompraConjuntaDAL.CarregarCompraConjuntaComPaginaRelacionada(compraConjuntaPaginaId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <param name="compraConjuntaPaginaId"></param>
        /// <returns></returns>
        public IEnumerable<CompraConjunta> CarregarCompraConjuntaEmAberta(CompraConjunta entidade, int compraConjuntaPaginaId)
        {
            //entidade.Produto = new ProdutoBLL().CarregarAutor(new Produto(){ ProdutoId = entidade.Produto.ProdutoId});
            return CompraConjuntaDAL.CarregarCompraConjuntaEmAberta(entidade, compraConjuntaPaginaId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public CompraConjunta Carregar(CompraConjunta entidade)
        {
            entidade = CompraConjuntaDAL.Carregar(entidade);
            return entidade;
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public CompraConjunta Inserir(CompraConjunta entidade)
        {
            CompraConjuntaDAL.Inserir(entidade);
            return entidade;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void Atualizar(CompraConjunta entidade)
        {
            CompraConjuntaDAL.Atualizar(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public IEnumerable<CompraConjunta> CarregarTodos(CompraConjunta entidade)
        {
            var compraConjuntaFH = new CompraConjuntaFH() { Ativa = entidade.Ativa.ToString() };
            IEnumerable<CompraConjunta> autores = CompraConjuntaDAL.CarregarTodos(0, 0, null, null, compraConjuntaFH);
            return autores;
        }

        /// <summary>
        /// Pesquisa se existe conflito de periodo de data em uma Compra Conjunta ativa sem data de finalização
        /// </summary>
        /// <param name="compraConjunta"></param>
        /// <param name="produto"></param>
        /// <returns></returns>
        public bool PeriodoConflitante(CompraConjunta compraConjunta)
        {
            return CompraConjuntaDAL.PeriodoConflitante(compraConjunta);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="compraConjunta"></param>
        /// <returns></returns>
        public bool CompraConjuntaRelacionada(CompraConjunta compraConjunta)
        {
            bool flag = CompraConjuntaDAL.CompraConjuntaRelacionada(compraConjunta);
            return flag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="compraConjunta"></param>
        public void ExcluirCompraConjunta(CompraConjunta compraConjunta)
        {
            bool flag = true;
            compraConjunta = this.Carregar(compraConjunta);

            flag = this.CompraConjuntaRelacionada(compraConjunta);

            if (flag) // Relacionado
            {
                // Sim
                compraConjunta.Ativa = false;

                using (TransactionScope scope = new TransactionScope())
                {
                    CompraConjuntaDAL.Atualizar(compraConjunta);
                    scope.Complete();
                }
            }
            else
            {
                // Não
                using (TransactionScope scope = new TransactionScope())
                {
                    CompraConjuntaDescontoDAL.ExcluirRelacionado(compraConjunta);
                    CompraConjuntaDAL.ExcluirCompraConjuntaLocalizacao(compraConjunta);
                    CompraConjuntaDAL.Excluir(compraConjunta);
                    scope.Complete();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="compraConjuntaPaginaId"></param>
        public void ExcluirCompraConjuntaLocalizacao(int compraConjuntaPaginaId)
        {
            CompraConjuntaDAL.ExcluirCompraConjuntaLocalizacao(compraConjuntaPaginaId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="compraConjuntaId"></param>
        /// <param name="compraConjuntaPaginaId"></param>
        /// <returns></returns>
        public bool InserirRelacionamentoPagina(int compraConjuntaId, int compraConjuntaPaginaId)
        {
            return CompraConjuntaDAL.InserirRelacionamentoPagina(compraConjuntaId, compraConjuntaPaginaId);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="produto"></param>
        /// <returns></returns>
        public CompraConjunta CarregaCompraConjuntaProduto(Produto produto)
        {
            return CompraConjuntaDAL.CarregarCompraConjuntaEmAbertoPorProduto(produto);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="compraConjunta"></param>
        /// <returns></returns>
        public int RetornaTotalCompras(CompraConjunta compraConjunta)
        {
            return CompraConjuntaDAL.TotalComprado(compraConjunta);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="compraConjuntaId"></param>
        /// <returns></returns>
        public CompraConjuntaDesconto CarregarCompraConjuntaDesconto(Int32 compraConjuntaId)
        {
            return CompraConjuntaDAL.CarregarCompraConjuntaDesconto(compraConjuntaId);
        }

        /// <summary>
        /// Busca todos os pedidos de compra conjunta
        /// </summary>
        /// <param name="compraConjunta"></param>
        private List<PedidoCompraConjunta> BuscaPedidoCompraConjunta(CompraConjunta compraConjunta)
        {
            List<PedidoCompraConjunta> pedidoCompraConjuntaBOList = new PedidoCompraConjuntaBLL().CarregarTodosPorCompraConjunta(compraConjunta);

            return pedidoCompraConjuntaBOList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="compraConjuntaBO"></param>
        /// <returns></returns>
        public bool CompraConjuntaComPedidoAberto(CompraConjunta compraConjuntaBO)
        {
            return CompraConjuntaDAL.CompraConjuntaComPedidoAberto(compraConjuntaBO);
        }


        #region Métodos de cancelamento da compra conjunta

        /// <summary>
        /// Cancela Compra Conjunta incluindo Gateway de Pagamento
        /// </summary>
        public RetornoCancelarCompraConjunta CancelarCompraConjunta(Int32 compraConjuntaId)
        {
            String caminhoTemplate = ConfigurationManager.AppSettings["CaminhoEmailCompraConjuntaCancelada"].ToString();

            CompraConjunta compraConjunta = new CompraConjunta();
            compraConjunta.CompraConjuntaId = compraConjuntaId;
            compraConjunta = this.Carregar(compraConjunta);

            this.DesativaCompraConjunta(compraConjunta);

            List<PedidoCompraConjunta> iEnumPedidoCompraConjunta = this.BuscaPedidoCompraConjunta(compraConjunta);

            if (iEnumPedidoCompraConjunta != null && iEnumPedidoCompraConjunta.Count > 0)
            {
                if (this.CancelaListaPedidoCompraConjunta(iEnumPedidoCompraConjunta, caminhoTemplate, compraConjuntaId))
                {
                    this.CancelaCompraConjunta(compraConjunta, caminhoTemplate);
                }
                else
                {
                    return RetornoCancelarCompraConjunta.DesativadoNaoCancelado;
                }
            }
            else
            {
                this.CancelaCompraConjunta(compraConjunta, caminhoTemplate);
            }

            return RetornoCancelarCompraConjunta.Cancelado;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="compraConjunta"></param>
        private void DesativaCompraConjunta(CompraConjunta compraConjunta)
        {
            compraConjunta.Ativa = false;
            this.Atualizar(compraConjunta);
        }

        /// <summary>
        /// Cancela lista de pedido de compra conjunta
        /// </summary>
        /// <param name="iEnumPedidoCompraConjunta"></param>
        private Boolean CancelaListaPedidoCompraConjunta(List<PedidoCompraConjunta> iEnumPedidoCompraConjunta, String caminhotemplate, Int32 compraConjuntaId)
        {
            bool flag = true;
            Dictionary<String, String> dicionarioDados = null;
            Configuracao configuracao = null;
            String emailEmitente;
            String caminhoImagem = ConfigurationManager.AppSettings["CaminhoImagem"].ToString();

            if (iEnumPedidoCompraConjunta != null && iEnumPedidoCompraConjunta.Count > 0)
            {
                configuracao = new Configuracao();
                configuracao.Chave = "emailGrupoA";
                configuracao = new ConfiguracaoBLL().CarregarCompleto(configuracao);
                emailEmitente = configuracao.ConfiguracaoValor.Valor;

                foreach (PedidoCompraConjunta pedidoCompraConjuntaTemp in iEnumPedidoCompraConjunta)
                {
                    PedidoCompraConjunta pedidoCompraConjunta = new PedidoCompraConjuntaBLL().Carregar(pedidoCompraConjuntaTemp);

                    if (pedidoCompraConjuntaTemp != null && pedidoCompraConjuntaTemp.PedidoCompraConjuntaId > 0)
                    {
                        if (!PedidoCompraConjuntaCancelado(pedidoCompraConjuntaTemp))
                        {
                            flag = false;
                        }

                        Pedido pedido = new Pedido();
                        pedido.PedidoId = pedidoCompraConjunta.PedidoCompraConjuntaId;
                        pedido = new PedidoBLL().CarregarComDependencias(pedido);

                        // Cancela o pedido no Ipagare
                        PedidoRecorrenteCartaoCreditoDTO pedidoRecorrenteCartaoCreditoDTO = new PedidoRecorrenteCartaoCreditoDTO();
                        pedidoRecorrenteCartaoCreditoDTO.CodigoDoPedido = pedido.PedidoId.ToString();

                        RetornoPedidoRecorrenteDTO retorno = new ServicoPagamentoCartaoCreditoRecorrente().CancelarPedido(pedidoRecorrenteCartaoCreditoDTO);

                        Usuario usuario = new Usuario();
                        usuario.UsuarioId = pedido.Usuario.UsuarioId;
                        usuario = new UsuarioBLL().CarregarUsuario(usuario);

                        if (usuario != null && usuario.UsuarioId > 0 && !String.IsNullOrEmpty(usuario.EmailUsuario))
                        {
                            // Envia email de cancelamento para os usuários.
                            dicionarioDados = new Dictionary<string, string>();
                            dicionarioDados.Add("Nome", usuario.NomeUsuario);
                            dicionarioDados.Add("CompraConjunta", compraConjuntaId.ToString());
                            dicionarioDados.Add("CaminhoSite", caminhoImagem);

                            Boolean enviouEmail = this.SendMail(usuario.EmailUsuario, emailEmitente, "Grupo A | Pedido de compra coletiva cancelado", caminhotemplate, dicionarioDados);

                            if (enviouEmail)
                            {
                                pedidoCompraConjunta.DataHoraNotificacaoFinalizacao = DateTime.Now;
                                new PedidoCompraConjuntaBLL().Atualizar(pedidoCompraConjunta);
                            }
                        }
                    }
                }
            }

            return flag;
        }

        /// <summary>
        /// Salva compra conjunta como o status cancelado
        /// </summary>
        /// <param name="compraConjunta"></param>
        private void CancelaCompraConjunta(CompraConjunta compraConjunta, String caminhotemplate)
        {
            String caminhoImagem = ConfigurationManager.AppSettings["CaminhoImagem"].ToString();
            String emailEmitente;

            compraConjunta.CompraConjuntaStatus = new CompraConjuntaStatus();
            compraConjunta.CompraConjuntaStatus.CompraConjuntaStatusId = 3;
            compraConjunta.DataHoraFinalizacao = DateTime.Now;

            this.Atualizar(compraConjunta);

            Configuracao configuracao = new Configuracao();
            configuracao.Chave = "emailGrupoA";
            configuracao = new ConfiguracaoBLL().CarregarCompleto(configuracao);
            emailEmitente = configuracao.ConfiguracaoValor.Valor;

            configuracao = new Configuracao();
            configuracao.Chave = "emailAdministradorGrupoA";
            configuracao = new ConfiguracaoBLL().CarregarCompleto(configuracao);

            Dictionary<String, String> dicionarioDados = new Dictionary<String, String>();
            dicionarioDados.Add("Nome", "Administrador");
            dicionarioDados.Add("CaminhoSite", caminhoImagem);

            this.SendMail(configuracao.ConfiguracaoValor.Valor, emailEmitente, "Grupo A | Pedido de compra coletiva cancelado", caminhotemplate, dicionarioDados);
        }

        /// <summary>
        /// Verifica se o pedido está cancelado, se não estiver, cancela o pedido. Devolve booleano se pedido estiver cancelado.
        /// </summary>
        /// <param name="pedidoCompraConjunta"></param>
        /// <returns></returns>
        private Boolean PedidoCompraConjuntaCancelado(PedidoCompraConjunta pedidoCompraConjunta)
        {
            bool flag = false;

            try
            {
                Pedido pedido = new Pedido();
                pedido.PedidoId = pedidoCompraConjunta.PedidoCompraConjuntaId;
                pedido = new PedidoBLL().CarregarComDependencias(pedido);

                if (pedido != null && pedido.PedidoId > 0)
                {
                    pedido.PedidoStatus = new PedidoStatus();
                    pedido.PedidoStatus.PedidoStatusId = Convert.ToInt32(StatusDoPedido.Cancelado);

                    new PedidoBLL().Atualizar(pedido);

                    if (pedido.PedidoStatus.PedidoStatusId == Convert.ToInt32(StatusDoPedido.Cancelado))
                    {
                        flag = true;
                    }
                    else
                    {
                        // Pedido não esta cancelado
                    }
                }
            }
            catch { }

            return flag;
        }

        #endregion

        #region Métodos de processamento da compra conjunta

        /// <summary>
        /// 
        /// </summary>
        public void FechamentoCompraConjunta()
        {
            String caminhoTemplateConcluida = ConfigurationManager.AppSettings["CaminhoEmailCompraConjuntaFinalizada"].ToString();
            String caminhoTemplateCancelada = ConfigurationManager.AppSettings["CaminhoEmailCompraConjuntaCancelada"].ToString();
            String caminhoTemplateCotaMinima = ConfigurationManager.AppSettings["CaminhoEmailCompraConjuntaCotaMinima"].ToString();

            List<CompraConjunta> comprasConjuntaBOList = CompraConjuntaDAL.CarregarCompraConjuntaParaFechamento(); // Buscar todos as compras conjunta que devem ser finalizados

            foreach (CompraConjunta compraConjuntaBOTemp in comprasConjuntaBOList) // Percorrer a lista de compras conjunta
            {
                CompraConjunta compraConjuntaBO = CompraConjuntaDAL.CarregarCompraConjuntaValida(compraConjuntaBOTemp); // Carregar compra conjunta

                if (
                    compraConjuntaBO != null 
                    && compraConjuntaBO.CompraConjuntaId > 0
                    && compraConjuntaBO.CompraConjuntaStatus != null
                    && compraConjuntaBO.CompraConjuntaStatus.CompraConjuntaStatusId == 1 // Aberta
                    )
                {
                    #region [ Buscar todos os pedidos e processá-los ]

                    compraConjuntaBO.Produto = new ProdutoBLL().CarregarComDependencias(compraConjuntaBO.Produto);

                    CompraConjuntaDesconto compraConjuntaDescontoBO = CompraConjuntaDAL.CarregarCompraConjuntaDesconto(compraConjuntaBO.CompraConjuntaId); // Carrega a faixa de desconto efetiva

                    List<PedidoCompraConjunta> pedidosCompraConjuntaBOList = new PedidoCompraConjuntaBLL().CarregarTodosPorCompraConjunta(compraConjuntaBO); // Carrega todos os pedidos da compra conjunta corrente

                    Int32 qtdVendida = CompraConjuntaDAL.TotalComprado(compraConjuntaBO);

                    if (qtdVendida <= compraConjuntaBO.EstoqueSeguranca) // Verifica se não ultrapassou o estoque
                    {
                        if (qtdVendida >= compraConjuntaBO.CompraConjuntaDescontos[0].QuantidadeMinima) // Verifica se atingiu a faixa mínima
                        {
                            #region [ Tenta Finalizar Pedidos efetuando cobrança ]

                            foreach (PedidoCompraConjunta pedidoCompraConjuntaBOTemp in pedidosCompraConjuntaBOList)  // Percorre a lista de pedidos da compra conjunta corrente
                            {
                                pedidoCompraConjuntaBOTemp.FechamentoSincronizado = true;
                                pedidoCompraConjuntaBOTemp.CompraConjuntaDesconto = compraConjuntaDescontoBO;
                                pedidoCompraConjuntaBOTemp.DataHoraNotificacaoFinalizacao = DateTime.Now;

                                Pedido pedidoBO = new PedidoBLL().CarregarComDependencias(new Pedido(pedidoCompraConjuntaBOTemp.PedidoCompraConjuntaId)); // Carregar o pedido corrente

                                pedidoBO = this.RecalcularValoresPedido(pedidoBO, compraConjuntaDescontoBO);
                                pedidoBO = this.RecalcularFrete(pedidoBO);

                                if (pedidoBO.PedidoStatus.PedidoStatusId == 5) // Status: Aguardando Compra Coletiva
                                {
                                    pedidoBO.PedidoStatus = new PedidoStatus();

                                    RetornoPedidoDTO retornoPedidoDTO = this.EfetuarPagamentoCofreIPagare(pedidoBO, pedidoCompraConjuntaBOTemp); // Processar a cobrança

                                    if (retornoPedidoDTO.CodigoDeRetorno == "0" || retornoPedidoDTO.CodigoDeRetorno == "201") // Pedido foi pago ou já foi pago na tentativa anterior
                                    {
                                        using (TransactionScope scope = new TransactionScope())
                                        {
                                            pedidoBO.PedidoStatus.PedidoStatusId = 1; // Finalizado
                                            pedidoBO.ValorPedido = pedidoBO.ValorPedido + pedidoBO.FreteValor;

                                            new PedidoBLL().Atualizar(pedidoBO);

                                            foreach (PedidoItem pedidoItemBOTemp in pedidoBO.PedidoItens) // Atualizar Pedido Item
                                            {
                                                new PedidoItemBLL().Atualizar(pedidoItemBOTemp);
                                            }

                                            new PedidoCompraConjuntaBLL().Atualizar(pedidoCompraConjuntaBOTemp);

                                            scope.Complete();
                                        }

                                        this.EmailPagamentoConcluido(caminhoTemplateConcluida, pedidoBO.Usuario, compraConjuntaBO, pedidoBO);
                                            
                                    }
                                    else // Pedido não foi cobrado no IPagare
                                    {
                                        if (pedidoBO.PedidoCompraConjunta.NumeroTentativa < 3) // Incrementa n° de tentativa
                                        {
                                            pedidoBO.PedidoCompraConjunta.NumeroTentativa++;

                                            //new PedidoBLL().Atualizar(pedidoBO);
                                            new PedidoCompraConjuntaBLL().Atualizar(pedidoBO.PedidoCompraConjunta);
                                        }
                                        else // Cancela Pedido
                                        {
                                            using (TransactionScope scope = new TransactionScope())
                                            {
                                                pedidoBO.PedidoStatus.PedidoStatusId = 2; // Cancelado
                                                pedidoBO.ValorPedido = pedidoBO.ValorPedido + pedidoBO.FreteValor;

                                                new PedidoBLL().Atualizar(pedidoBO);

                                                foreach (PedidoItem pedidoItemBOTemp in pedidoBO.PedidoItens) // Atualizar Pedido Item
                                                {
                                                    new PedidoItemBLL().Atualizar(pedidoItemBOTemp);
                                                }

                                                new PedidoCompraConjuntaBLL().Atualizar(pedidoCompraConjuntaBOTemp);

                                                scope.Complete();
                                            }

                                            this.EmailPagamentoCancelado(caminhoTemplateCancelada, pedidoBO.Usuario, compraConjuntaBO, pedidoBO);
                                        }
                                    }
                                }
                            }

                            #endregion
                        }
                        else // Quantidade vendida inferior a quantidade mínima da menor faixa.
                        {
                            #region [ Cancela Pedidos e avisa clientes disparando e-mail ]

                            foreach (PedidoCompraConjunta pedidoCompraConjuntaBOTemp in pedidosCompraConjuntaBOList)  // Percorre a lista de pedidos da compra conjunta corrente
                            {
                                Pedido pedidoBO = new PedidoBLL().CarregarComDependencias(new Pedido(pedidoCompraConjuntaBOTemp.PedidoCompraConjuntaId)); // Carregar o pedido corrente

                                if (pedidoBO.Usuario != null && pedidoBO.Usuario.UsuarioId > 0)
                                {
                                    Usuario usuarioBO = new UsuarioBLL().CarregarComDependencia(new Usuario { UsuarioId = pedidoBO.Usuario.UsuarioId }); // Carrega Usuario

                                    if (
                                        usuarioBO != null 
                                        && usuarioBO.UsuarioId > 0 
                                        && !String.IsNullOrEmpty(usuarioBO.EmailUsuario) 
                                        && pedidoBO.PedidoStatus.PedidoStatusId == 5
                                        )
                                    {
                                        pedidoBO.PedidoStatus = new PedidoStatus();
                                        pedidoBO.PedidoStatus.PedidoStatusId = 2; // Cancelado

                                        new PedidoBLL().Atualizar(pedidoBO);

                                        pedidoCompraConjuntaBOTemp.FechamentoSincronizado = true;
                                        pedidoCompraConjuntaBOTemp.CompraConjuntaDesconto = compraConjuntaDescontoBO;
                                        pedidoCompraConjuntaBOTemp.DataHoraNotificacaoFinalizacao = DateTime.Now;

                                        new PedidoCompraConjuntaBLL().Atualizar(pedidoCompraConjuntaBOTemp);

                                        this.EmailCotaMinima(caminhoTemplateCotaMinima, usuarioBO, compraConjuntaBO);
                                    }
                                }
                            }

                            #endregion
                        }
                    }
                    else // Quantidade vendida superior a quantidade permitida.
                    {

                    }

                    #endregion

                    if (!this.CompraConjuntaComPedidoAberto(compraConjuntaBO))
                    {
                        compraConjuntaBO.DataHoraFinalizacao = DateTime.Now;
                        compraConjuntaBO.Ativa = false;
                        compraConjuntaBO.CompraConjuntaStatus = new CompraConjuntaStatus();
                        compraConjuntaBO.CompraConjuntaStatus.CompraConjuntaStatusId = 2; // Finalizada

                        new CompraConjuntaBLL().Atualizar(compraConjuntaBO);
                    }
                }
                else
                {

                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="compraConjuntaId"></param>
        private Pedido RecalcularValoresPedido(Pedido pedidoBO, CompraConjuntaDesconto compraConjuntaDescontoBO)
        {
            Decimal valorAtual = 0;

            foreach (PedidoItem pedidoItemBOTemp in pedidoBO.PedidoItens)
            {
                decimal valorUnitarioFinal = (pedidoItemBOTemp.Produto.ValorUnitario * (1 - (compraConjuntaDescontoBO.PercentualDesconto / 100)));

                pedidoItemBOTemp.ValorUnitarioFinal = valorUnitarioFinal;
                valorAtual += valorUnitarioFinal * pedidoItemBOTemp.Quantidade;

                //if (pedidoItemBO.Produto.ValorOferta == null)
                //{
                //    valorAtual += (pedidoItemBO.Produto.ValorUnitario * (1 - (compraConjuntaDescontoBO.PercentualDesconto / 100))) * pedidoItemBO.Quantidade;
                //}
                //else
                //{
                //    valorAtual += (pedidoItemBO.Produto.ValorOferta.Value * (1 - (compraConjuntaDescontoBO.PercentualDesconto / 100))) * pedidoItemBO.Quantidade;
                //}
            }

            pedidoBO.ValorPedido = valorAtual;

            return pedidoBO;
        }

        /// <summary>
        /// Recalcula o frete caso o valor do pedido seja menor ou igual que ao valor limite de freteGratis
        /// </summary>
        /// <param name="pedidoBO"></param>
        /// <returns></returns>
        private Pedido RecalcularFrete(Pedido pedidoBO)
        {
            double valorFrete = 0;
            decimal peso = 0;

            if (pedidoBO.ValorPedido <= Convert.ToDecimal(ConfigurationManager.AppSettings["FreteGratisAPartirDe"]))
            {
                foreach (PedidoItem pedidoItemBOTemp in pedidoBO.PedidoItens)
                {
                    try
                    {
                        peso = peso + (pedidoItemBOTemp.Produto.Peso * pedidoItemBOTemp.Quantidade);
                    }
                    catch { }
                }

                int cepInicial = Convert.ToInt32(pedidoBO.PedidoEndereco.Cep.Substring(0, 5));
                int cepFinal = Convert.ToInt32(pedidoBO.PedidoEndereco.Cep.Substring(4, 3));

                valorFrete = new CarrinhoBLL().CalculaFrete(cepInicial, cepFinal, peso);
            }

            pedidoBO.FreteValor = Convert.ToDecimal(valorFrete);

            return pedidoBO;
        }

        /// <summary>
        /// Soma o valor do pedido com o valor do frete, e atribui o token para efetuar a cobrança do pedido
        /// </summary>
        /// <param name="pedidoBO"></param>
        /// <param name="caminhoTemplate"></param>
        private RetornoPedidoDTO EfetuarPagamentoCofreIPagare(Pedido pedidoBO, PedidoCompraConjunta pedidoCompraConjuntaBO)
        {
            PedidoCartaoCreditoDTO pedidoCartaoCreditoDTO = new PedidoCartaoCreditoDTO();

            pedidoCartaoCreditoDTO.ValorTotalDoPedido = pedidoBO.ValorPedido + pedidoBO.FreteValor;
            pedidoCartaoCreditoDTO.Token = pedidoCompraConjuntaBO.TokenCofre;
            pedidoCartaoCreditoDTO.MeioDePagamento = pedidoBO.Pagamento.MeioPagamento.CodigoGateway;
            pedidoCartaoCreditoDTO.TipoDePagamento = String.Concat("A", pedidoBO.Pagamento.NumeroParcelas.ToString().PadLeft(2, '0'));
            pedidoCartaoCreditoDTO.CodigoDoPedido = pedidoBO.PedidoId.ToString();

            return new ServicoPagamentoCofre().EfetuarCobranca(pedidoCartaoCreditoDTO);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="titulo"></param>
        /// <param name="withLink"></param>
        /// <param name="linkParaPaginaPai"></param>
        /// <returns></returns>
        public string CarregarAutores(Titulo titulo)
        {
            var autores = new TituloBLL().CarregarAutores(titulo);
            string strAutores = string.Empty;
            StringBuilder texto = new StringBuilder();

            int x = 1;

            if (autores != null && autores.Count > 0)
            {
                foreach (var item in autores)
                {
                    if (!string.IsNullOrEmpty(item.NomeAutor))
                    {
                        texto.Append(item.NomeAutor);

                        if (x < autores.Count)
                        {
                            texto.Append("; ");
                        }
                    }

                    x++;
                }
            }

            strAutores = texto.ToString();

            return strAutores;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="titulo"></param>
        /// <param name="withLink"></param>
        /// <param name="linkParaPaginaPai"></param>
        /// <returns></returns>
        public string CarregarAutores(Produto produto)
        {
            TituloImpresso tituloImpresso = new TituloImpressoBLL().CarregarPorProduto(produto.ProdutoId);
            return CarregarAutores(tituloImpresso.Titulo);

        }

        #region [ E-mail ]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="caminhoTemplate"></param>
        /// <param name="mensagem"></param>
        /// <param name="compraConjuntaBO"></param>
        private void EmailCotaMinima(String caminhoTemplate, Usuario usuarioBO, CompraConjunta compraConjuntaBO)
        {
            String caminhoImagem = ConfigurationManager.AppSettings["CaminhoImagem"].ToString();

            Configuracao configuracao = new Configuracao();
            configuracao.Chave = "emailGrupoA";
            configuracao = new ConfiguracaoBLL().CarregarCompleto(configuracao);
            String emailEmitente = configuracao.ConfiguracaoValor.Valor;

            // Envia email de cancelamento para os usuários.
            Dictionary<string, string> dicionarioDados = new Dictionary<string, string>();
            dicionarioDados.Add("CaminhoSite", caminhoImagem);

            dicionarioDados.Add("UsuarioNome", usuarioBO.NomeUsuario);
            dicionarioDados.Add("ProdutoNome", compraConjuntaBO.Produto.NomeProduto);
            dicionarioDados.Add("CompraColetivaDataIni", compraConjuntaBO.DataInicialCompra.ToString("dd/MM/yyyy HH:mm"));
            dicionarioDados.Add("CompraColetivaDataFim", compraConjuntaBO.DataFinalCompra.ToString("dd/MM/yyyy HH:mm"));

            Boolean enviouEmail = this.SendMail(usuarioBO.EmailUsuario, emailEmitente, "Grupo A | Compra coletiva cancelada", caminhoTemplate, dicionarioDados);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="caminhoTemplate"></param>
        /// <param name="usuarioBO"></param>
        /// <param name="compraConjuntaBO"></param>
        /// <param name="pedidoBO"></param>
        private void EmailPagamentoCancelado(String caminhoTemplate, Usuario usuarioBO, CompraConjunta compraConjuntaBO, Pedido pedidoBO)
        {
            String caminhoImagem = ConfigurationManager.AppSettings["CaminhoImagem"].ToString();

            Configuracao configuracao = new Configuracao();
            configuracao.Chave = "emailGrupoA";
            configuracao = new ConfiguracaoBLL().CarregarCompleto(configuracao);
            String emailEmitente = configuracao.ConfiguracaoValor.Valor;

            // Envia email de cancelamento para os usuários.
            Dictionary<string, string> dicionarioDados = new Dictionary<string, string>();
            dicionarioDados.Add("CaminhoSite", caminhoImagem);

            dicionarioDados.Add("UsuarioNome", usuarioBO.NomeUsuario);
            dicionarioDados.Add("ProdutoNome", compraConjuntaBO.Produto.NomeProduto);
            dicionarioDados.Add("PedidoId", pedidoBO.PedidoId.ToString());

            Boolean enviouEmail = this.SendMail(usuarioBO.EmailUsuario, emailEmitente, "Seu pedido de compra coletiva foi cancelado.", caminhoTemplate, dicionarioDados);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="caminhoTemplate"></param>
        /// <param name="usuarioBO"></param>
        /// <param name="compraConjuntaBO"></param>
        /// <param name="pedidoBO"></param>
        private void EmailPagamentoConcluido(String caminhoTemplate, Usuario usuarioBO, CompraConjunta compraConjuntaBO, Pedido pedidoBO)
        {
            String caminhoImagem = ConfigurationManager.AppSettings["CaminhoImagem"].ToString();

            Configuracao configuracao = new Configuracao();
            configuracao.Chave = "emailGrupoA";
            configuracao = new ConfiguracaoBLL().CarregarCompleto(configuracao);
            String emailEmitente = configuracao.ConfiguracaoValor.Valor;

            // Envia email de cancelamento para os usuários.
            Dictionary<string, string> dicionarioDados = new Dictionary<string, string>();
            dicionarioDados.Add("CaminhoSite", caminhoImagem);

            dicionarioDados.Add("UsuarioNome", usuarioBO.NomeUsuario);
            dicionarioDados.Add("CompraColetivaDataIni", compraConjuntaBO.DataInicialCompra.ToString("dd/MM/yyyy HH:mm"));
            dicionarioDados.Add("CompraColetivaDataFim", compraConjuntaBO.DataFinalCompra.ToString("dd/MM/yyyy HH:mm"));
            dicionarioDados.Add("CompraColetivaQntdVendida", CompraConjuntaDAL.TotalComprado(compraConjuntaBO).ToString());
            dicionarioDados.Add("ProdutoValorUnitario", pedidoBO.PedidoItens[0].ValorUnitarioBase.ToString("C"));
            dicionarioDados.Add("ProdutoValorCompraColetiva", pedidoBO.PedidoItens[0].ValorUnitarioFinal.ToString("C"));
            dicionarioDados.Add("PedidoId", pedidoBO.PedidoId.ToString());

            dicionarioDados.Add("ProdutoNome", compraConjuntaBO.Produto.NomeProduto);
            dicionarioDados.Add("Autor", this.CarregarAutores(pedidoBO.PedidoItens[0].Produto));
            dicionarioDados.Add("QuantidadeItem", Convert.ToInt32(pedidoBO.PedidoItens[0].Quantidade).ToString());
            dicionarioDados.Add("ProdutoValorCompraColetivaUnitario", pedidoBO.PedidoItens[0].ValorUnitarioFinal.ToString("C"));
            dicionarioDados.Add("FreteValor", pedidoBO.FreteValor.ToString("C"));
            dicionarioDados.Add("ProdutoValorCompraColetivaTotal", (pedidoBO.FreteValor + pedidoBO.ValorPedido).ToString("C"));

            dicionarioDados.Add("PedidoData", pedidoBO.DataHoraPedido.ToString("dd/MM/yyyy HH:mm"));
            dicionarioDados.Add("MeioPagamento", pedidoBO.Pagamento.MeioPagamento.Nome);

            pedidoBO.PedidoEndereco.EnderecoTipo = new EnderecoTipoBLL().Carregar(pedidoBO.PedidoEndereco.EnderecoTipo);
            dicionarioDados.Add("EnderecoTipo", pedidoBO.PedidoEndereco.EnderecoTipo.Tipo);
            dicionarioDados.Add("EnderecoLogradouro", pedidoBO.PedidoEndereco.Logradouro);
            dicionarioDados.Add("EnderecoBairro", pedidoBO.PedidoEndereco.Bairro);

            pedidoBO.PedidoEndereco.Municipio = new MunicipioBLL().Carregar(pedidoBO.PedidoEndereco.Municipio);
            dicionarioDados.Add("EnderecoMunicipio", pedidoBO.PedidoEndereco.Municipio.NomeMunicipio);

            pedidoBO.PedidoEndereco.Municipio.Regiao = new RegiaoBLL().Carregar(pedidoBO.PedidoEndereco.Municipio.Regiao);
            dicionarioDados.Add("EnderecoEstado", pedidoBO.PedidoEndereco.Municipio.Regiao.NomeRegiao);

            dicionarioDados.Add("EnderecoCEP", pedidoBO.PedidoEndereco.Cep);
            dicionarioDados.Add("UsuarioEmail", usuarioBO.EmailUsuario);
            dicionarioDados.Add("ServicoEntrega", new CarrinhoBLL().CarregarTransportadoraServico(new TransportadoraServico() { TransportadoraServicoId = 1 }).NomeServicoe);

            Boolean enviouEmail = this.SendMail(usuarioBO.EmailUsuario, emailEmitente, "Grupo A | Compra coletiva confirmada", caminhoTemplate, dicionarioDados);
        }

        #endregion

        #endregion

        /// <summary>
        /// Dispara e-mail para usuário informando cancelamento da compra conjunta.
        /// </summary>
        /// <param name="pedidoCompraConjunta"></param>
        private Boolean SendMail(String emailDestino, String emitente, String assuntoEmail, String caminhotemplate, Dictionary<String, String> dicionarioDados)
        {
            // Tenta enviar e-mail
            try
            {
                StringBuilder templateEmail = new EmailHelper().PopulaTemplateEmail(dicionarioDados, caminhotemplate);

                new EmailHelper().EnviarEmail(emitente, emailDestino, assuntoEmail, templateEmail);
            }
            catch
            {
                return false; // Mensagem NÃO enviada
            }

            return true;
        }

        #endregion
    }
}