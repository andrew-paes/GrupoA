using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;
using GrupoA.BusinessObject.Enumerator;

namespace GrupoA.Sincronizacao
{
    public class PedidoSincronizador
    {
        #region [ Propriedades ]

        LogBLL _logBLL;
        protected LogBLL logBLL
        {
            get
            {
                if (_logBLL == null)
                {
                    _logBLL = new LogBLL();
                }
                return _logBLL;
            }
        }

        PedidoBLL _pedidoBLL;
        protected PedidoBLL pedidoBLL
        {
            get
            {
                if (_pedidoBLL == null)
                {
                    _pedidoBLL = new PedidoBLL();
                }
                return _pedidoBLL;
            }
        }

        PedidoExportacaoBLL _pedidoExportacaoBLL;
        protected PedidoExportacaoBLL pedidoExportacaoBLL
        {
            get
            {
                if (_pedidoExportacaoBLL == null)
                {
                    _pedidoExportacaoBLL = new PedidoExportacaoBLL();
                }
                return _pedidoExportacaoBLL;
            }
        }

        ServicoPortal.AM_PORTAL _wsPortal;
        protected ServicoPortal.AM_PORTAL wsPortal
        {
            get
            {
                if (_wsPortal == null)
                {
                    _wsPortal = new ServicoPortal.AM_PORTAL();
                }
                return _wsPortal;
            }
        }

        #endregion

        /// <summary>
        /// Envia os novos pedidos para o ERP com o status finalizado (pedidoStatusId = 1) e não sincronizados ( PedidoControle )
        /// </summary>
        public void SincronizarPedidos()
        {
            try
            {
                IList<Pedido> pedidoBOIList = pedidoBLL.CarregarFinalizadosNaoSincronizados(); // Carregar os pedido

                if (pedidoBOIList != null && pedidoBOIList.Any())
                {
                    foreach (Pedido pedidoBOTemp in pedidoBOIList)
                    {
                        this.SincronizarPedido(pedidoBOTemp);
                    }
                }
            }
            catch (Exception ex)
            {
                LogDados logDados = CriarLogDados(EnumLogCategoria.Sincronizacao, LogEventos.ErroEmPedido, null, "Erro Geral: " + ex.Message);
                LogOcorrencia logOcorrencia = new LogOcorrencia();
                logOcorrencia.LogEvento = new LogEvento(LogEventos.ErroEmPedido.GetHashCode());
                logOcorrencia.Dados = logDados.ToXml();
                logBLL.RegistrarOcorrenciaLog(logOcorrencia);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pedidoBO"></param>
        private void SincronizarPedido(Pedido pedidoBO)
        {
            try
            {
                bool errorFlag = false;

                if (pedidoBO.Usuario != null && pedidoBO.Usuario.UsuarioId > 0)
                {
                    pedidoBO.Usuario = new UsuarioBLL().CarregarUsuario(pedidoBO.Usuario);
                }

                if (pedidoBO.Pagamento != null && pedidoBO.Pagamento.PagamentoId > 0)
                {
                    pedidoBO.Pagamento = new PagamentoBLL().Carregar(pedidoBO);
                }

                if (
                    pedidoBO.Usuario != null && pedidoBO.Usuario.UsuarioId > 0 && !String.IsNullOrEmpty(pedidoBO.Usuario.CadastroPessoa)
                    && pedidoBO.Pagamento != null && !String.IsNullOrEmpty(pedidoBO.Pagamento.CodigoLegadoMeioPagamentoFaixa)
                    )
                {
                    float valorTotalPedido = 0;
                    float descontoValorPedido = 0;

                    #region [ Itens do Pedido ]

                    IList<ServicoPortal.ITEMPVENDAESTRU> itemPVendaEstruBOIList = new List<ServicoPortal.ITEMPVENDAESTRU>();

                    foreach (PedidoItem pedidoItemBOTemp in pedidoBO.PedidoItens)
                    {
                        try
                        {
                            ServicoPortal.ITEMPVENDAESTRU itemPVendaEstruBO = new ServicoPortal.ITEMPVENDAESTRU();

                            if (pedidoItemBOTemp.Produto != null && pedidoItemBOTemp.Produto.ProdutoId > 0)
                            {
                                pedidoItemBOTemp.Produto = new ProdutoBLL().Carregar(pedidoItemBOTemp.Produto);
                            }

                            float valorUnitario = pedidoItemBOTemp.Produto != null && pedidoItemBOTemp.Produto.ProdutoId > 0 && pedidoItemBOTemp.Produto.ValorOferta != null && pedidoItemBOTemp.Produto.ValorOferta > 0 ? (float)pedidoItemBOTemp.Produto.ValorOferta : (float)pedidoItemBOTemp.ValorUnitarioBase;

                            #region [ Valores dos Itens do Pedido com Promoção ]

                            try
                            {
                                pedidoItemBOTemp.PedidoItemPromocao = new PedidoItemPromocaoBLL().Carregar(new PedidoItemPromocao { PedidoItemPromocaoId = pedidoItemBOTemp.PedidoItemId });

                                if (pedidoItemBOTemp.PedidoItemPromocao != null && pedidoItemBOTemp.PedidoItemPromocao.PedidoItemPromocaoId > 0)
                                {
                                    if (pedidoItemBOTemp.PedidoItemPromocao.DescontoPercentual != null)
                                    {
                                        itemPVendaEstruBO.WC6_DESCONT = (float)pedidoItemBOTemp.PedidoItemPromocao.DescontoPercentual;
                                        itemPVendaEstruBO.WC6_VALDESC = ((float)pedidoItemBOTemp.PedidoItemPromocao.DescontoPercentual / 100) * valorUnitario;
                                    }
                                    else if (pedidoItemBOTemp.PedidoItemPromocao.DescontoValor != null)
                                    {
                                        descontoValorPedido += (float)pedidoItemBOTemp.PedidoItemPromocao.DescontoValor;
                                        //itemPVendaEstruBO.WC6_DESCONT = ((float)pedidoItemBOTemp.PedidoItemPromocao.DescontoValor * 100) / (valorUnitario * (float)pedidoItemBOTemp.Quantidade);
                                        //itemPVendaEstruBO.WC6_VALDESC = (float)(pedidoItemBOTemp.PedidoItemPromocao.DescontoValor / pedidoItemBOTemp.Quantidade);
                                    }
                                }
                                else
                                {
                                    PedidoCompraConjunta pedidoCompraConjuntaBO = new PedidoCompraConjuntaBLL().Carregar(new PedidoCompraConjunta { PedidoCompraConjuntaId = pedidoBO.PedidoId });

                                    if (pedidoCompraConjuntaBO != null && pedidoCompraConjuntaBO.PedidoCompraConjuntaId > 0)
                                    {
                                        valorUnitario = (float)pedidoItemBOTemp.ValorUnitarioBase;

                                        CompraConjuntaDesconto compraConjuntaDescontoBO = new CompraConjuntaDescontoBLL().Carregar(new CompraConjuntaDesconto { CompraConjuntaDescontoId = pedidoCompraConjuntaBO.CompraConjuntaDesconto.CompraConjuntaDescontoId });

                                        if (compraConjuntaDescontoBO != null && compraConjuntaDescontoBO.CompraConjuntaDescontoId > 0)
                                        {
                                            itemPVendaEstruBO.WC6_DESCONT = (float)compraConjuntaDescontoBO.PercentualDesconto;
                                            itemPVendaEstruBO.WC6_VALDESC = ((float)compraConjuntaDescontoBO.PercentualDesconto / 100) * valorUnitario;
                                        }
                                    }
                                }

                                valorUnitario = valorUnitario - itemPVendaEstruBO.WC6_VALDESC;
                            }
                            catch (Exception ex)
                            {
                                errorFlag = true;

                                LogDados logDados = CriarLogDados(EnumLogCategoria.Sincronizacao, LogEventos.ErroEmPedido, pedidoBO, "Erro na busca de pedidoItemBOTemp.PedidoItemPromocao. " + ex.Message);
                                LogOcorrencia logOcorrencia = new LogOcorrencia();
                                logOcorrencia.LogEvento = new LogEvento(LogEventos.ErroEmPedido.GetHashCode());
                                logOcorrencia.Dados = logDados.ToXml();
                                logBLL.RegistrarOcorrenciaLog(logOcorrencia);
                            }

                            #endregion

                            //i.WC6_ENTREG = "";
                            itemPVendaEstruBO.WC6_PEDCLI = "";
                            itemPVendaEstruBO.WC6_PRCVEN = valorUnitario;
                            itemPVendaEstruBO.WC6_PRODUTO = pedidoItemBOTemp.Produto.CodigoProduto;
                            itemPVendaEstruBO.WC6_QTDVEN = (float)pedidoItemBOTemp.Quantidade;

                            valorTotalPedido += valorUnitario * (float)pedidoItemBOTemp.Quantidade;

                            itemPVendaEstruBOIList.Add(itemPVendaEstruBO);
                        }
                        catch (Exception ex)
                        {
                            errorFlag = true;

                            LogDados logDados = CriarLogDados(EnumLogCategoria.Sincronizacao, LogEventos.ErroEmPedido, pedidoBO, "Erro na busca de pedidoItemBOTemp. " + ex.Message);
                            LogOcorrencia logOcorrencia = new LogOcorrencia();
                            logOcorrencia.LogEvento = new LogEvento(LogEventos.ErroEmPedido.GetHashCode());
                            logOcorrencia.Dados = logDados.ToXml();
                            logBLL.RegistrarOcorrenciaLog(logOcorrencia);
                        }
                    }

                    #endregion

                    if (itemPVendaEstruBOIList != null && itemPVendaEstruBOIList.Any() && !errorFlag)
                    {
                        ServicoPortal.ITEMPEDVENDA itemPrevidencia = new ServicoPortal.ITEMPEDVENDA();
                        itemPrevidencia.ITENSPV = itemPVendaEstruBOIList.ToArray<ServicoPortal.ITEMPVENDAESTRU>();

                        float descontoPercentalPedido = 0; // (descontoValorPedido * 100) / valorTotalPedido;

                        //String.Concat("00", pedidoBOTemp.Pagamento.MeioPagamento.MeioPagamentoId.ToString())
                        ////string resposta = wsPortal.GETPEDIDOVENDA(
                        ////                                        ConfigurationManager.AppSettings["WSEmprensa"].ToString()
                        ////                                        , ConfigurationManager.AppSettings["WSFilial"].ToString()
                        ////                                        , ConfigurationManager.AppSettings["WSLogin"].ToString()
                        ////                                        , ConfigurationManager.AppSettings["WSSenha"].ToString()
                        ////                                        , pedidoBO.Usuario.CadastroPessoa
                        ////                                        , pedidoBO.Pagamento.CodigoLegadoMeioPagamentoFaixa
                        ////                                        , "C"
                        ////                                        , (float)pedidoBO.FreteValor
                        ////                                        , itemPrevidencia
                        ////                                        );

                        string resposta = wsPortal.GETPEDIDOVENDA(
                                                                ConfigurationManager.AppSettings["WSEmprensa"].ToString()
                                                                , ConfigurationManager.AppSettings["WSFilial"].ToString()
                                                                , ConfigurationManager.AppSettings["WSLogin"].ToString()
                                                                , ConfigurationManager.AppSettings["WSSenha"].ToString()
                                                                , pedidoBO.Usuario.CadastroPessoa
                                                                , pedidoBO.Pagamento.CodigoLegadoMeioPagamentoFaixa
                                                                , "C"
                                                                , (float)pedidoBO.FreteValor
                                                                , descontoPercentalPedido
                                                                , descontoValorPedido
                                                                , itemPrevidencia
                                                                );
                        //WC5_FRETE = valor do Frete
                        //WC5_TPFRETE = C
                        //pedidoBOTemp.FreteValor

                        //string resposta = "sucesso";

                        if (resposta.Contains("sucesso"))
                        {
                            pedidoExportacaoBLL.AtualizaWebService(pedidoBO); // Pedido inserido em PedidoControle
                        }
                        else
                        {
                            LogDados logDados = CriarLogDados(EnumLogCategoria.Sincronizacao, LogEventos.ErroEmPedido, pedidoBO, resposta);
                            LogOcorrencia logOcorrencia = new LogOcorrencia();
                            logOcorrencia.LogEvento = new LogEvento(LogEventos.ErroEmPedido.GetHashCode());
                            logOcorrencia.Dados = logDados.ToXml();
                            logBLL.RegistrarOcorrenciaLog(logOcorrencia);
                        }
                    }
                    else
                    {
                        LogDados logDados = CriarLogDados(EnumLogCategoria.Sincronizacao, LogEventos.ErroEmPedido, pedidoBO, "Erro na busca de Itens do Pedido");
                        LogOcorrencia logOcorrencia = new LogOcorrencia();
                        logOcorrencia.LogEvento = new LogEvento(LogEventos.ErroEmPedido.GetHashCode());
                        logOcorrencia.Dados = logDados.ToXml();
                        logBLL.RegistrarOcorrenciaLog(logOcorrencia);
                    }
                }
                else
                {
                    LogDados logDados = CriarLogDados(EnumLogCategoria.Sincronizacao, LogEventos.ErroEmPedido, pedidoBO, "Erro na busca de Usuário ou Pagamento");
                    LogOcorrencia logOcorrencia = new LogOcorrencia();
                    logOcorrencia.LogEvento = new LogEvento(LogEventos.ErroEmPedido.GetHashCode());
                    logOcorrencia.Dados = logDados.ToXml();
                    logBLL.RegistrarOcorrenciaLog(logOcorrencia);
                }
            }
            catch (Exception ex)
            {
                LogDados logDados = CriarLogDados(EnumLogCategoria.Sincronizacao, LogEventos.ErroEmPedido, pedidoBO, ex.Message);
                LogOcorrencia logOcorrencia = new LogOcorrencia();
                logOcorrencia.LogEvento = new LogEvento(LogEventos.ErroEmPedido.GetHashCode());
                logOcorrencia.Dados = logDados.ToXml();
                logBLL.RegistrarOcorrenciaLog(logOcorrencia);
            }
        }

        public LogDados CriarLogDados(EnumLogCategoria enumLogCategoria, LogEventos logEventos, Pedido pedido, string message)
        {
            LogDados logDados = new LogDados();
            logDados.Adicionar("LogEvento", "LOG_EVENTO", logEventos.GetHashCode().ToString());
            logDados.Adicionar("LogCategoria", "LOG_CATEGORIA", enumLogCategoria.GetHashCode().ToString());
            logDados.Adicionar("DataHora", "DATA_HORA", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            logDados.Adicionar("Pedido", "PEDIDO_ID", pedido.PedidoId.ToString());
            logDados.Adicionar("MensagemErro", "MENSAGEM_ERRO", message);
            return logDados;
        }
    }
}