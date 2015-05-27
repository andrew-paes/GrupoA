using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using GrupoA.BusinessObject;
using GrupoA.BusinessObject.Enumerator;
using GrupoA.BusinessObject.ViewHelper;
using GrupoA.FilterHelper;

namespace GrupoA.DataAccess.ADO
{
    public partial class PedidoADO : ADOSuper
    {
        /// <summary>
        /// Método que persiste um Pedido.
        /// </summary>
        /// <param name="entidade">Pedido contendo os dados a serem persistidos.</param>	
        public void InserirPedidoPromocaoCarrinho(Pedido entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO PedidoPromocaoCarrinho ");
            sbSQL.Append(" (pedidoId, promocaoId) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@pedidoId, @promocaoId) ");

            //sbSQL.Append(" ; SET @pedidoId = SCOPE_IDENTITY(); ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@pedidoId", DbType.Int32, entidade.PedidoId);

            _db.AddInParameter(command, "@promocaoId", DbType.Int32, entidade.Promocoes[0].PromocaoId);

            // Executa a query.
            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que carrega um Produto com suas dependências.
        /// </summary>
        /// <param name="entidade">Produto a ser carregado (somente o identificador é necessário).</param>
        /// <returns>Produto</returns>
        public Pedido CarregarPedidoComDependencias(Pedido entidade)
        {

            Pedido entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("SELECT                ");
            sbSQL.Append("    pedido.*         ");
            sbSQL.Append("FROM pedido ");

            sbSQL.Append(" WHERE pedido.pedidoId=@pedidoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@pedidoId", DbType.Int32, entidade.PedidoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Pedido();
                PopulaPedidoComDependencias(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// Método que retorna popula um Pedido baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Pedido a ser populado(.</param>
        public static void PopulaPedidoComDependencias(IDataReader reader, Pedido entidade)
        {
            if (reader["pedidoId"] != DBNull.Value)
                entidade.PedidoId = Convert.ToInt32(reader["pedidoId"].ToString());

            if (reader["dataHoraPedido"] != DBNull.Value)
                entidade.DataHoraPedido = Convert.ToDateTime(reader["dataHoraPedido"].ToString());

            if (reader["freteValor"] != DBNull.Value)
                entidade.FreteValor = Convert.ToDecimal(reader["freteValor"].ToString());

            if (reader["valorPedido"] != DBNull.Value)
                entidade.ValorPedido = Convert.ToDecimal(reader["valorPedido"].ToString());

            if (reader["usuarioId"] != DBNull.Value)
            {
                entidade.Usuario = new Usuario();
                entidade.Usuario.UsuarioId = Convert.ToInt32(reader["usuarioId"].ToString());
                entidade.Usuario = new UsuarioADO().Carregar(entidade.Usuario);

                //Carrega Lista de Enderecos
                entidade.Usuario.Telefones = new List<Telefone>();
                var telefoneFH = new TelefoneFH() { UsuarioId = entidade.Usuario.UsuarioId.ToString() };
                var telefones = new TelefoneADO().CarregarTodos(0, 0, null, null, telefoneFH);
                foreach (var item in telefones)
                {
                    entidade.Usuario.Telefones.Add(item);
                }
            }

            if (reader["carrinhoId"] != DBNull.Value)
            {
                entidade.Carrinho = new Carrinho();
                entidade.Carrinho.CarrinhoId = Convert.ToInt32(reader["carrinhoId"].ToString());
            }

            if (reader["pedidoStatusId"] != DBNull.Value)
            {
                entidade.PedidoStatus = new PedidoStatus();
                entidade.PedidoStatus.PedidoStatusId = Convert.ToInt32(reader["pedidoStatusId"].ToString());
                entidade.PedidoStatus = new PedidoStatusADO().Carregar(entidade.PedidoStatus);
            }

            if (reader["pagamentoId"] != DBNull.Value)
            {
                entidade.Pagamento = new PagamentoADO().Carregar(Convert.ToInt32(reader["pagamentoId"].ToString()));
                entidade.Pagamento.MeioPagamento = new MeioPagamentoADO().Carregar(entidade.Pagamento.MeioPagamento);
            }

            if (reader["transportadoraServicoId"] != DBNull.Value)
            {
                entidade.TransportadoraServico = new TransportadoraServico();
                entidade.TransportadoraServico.TransportadoraServicoId = Convert.ToInt32(reader["transportadoraServicoId"].ToString());
            }

            //Carrega os endereços do pedido.
            var endereco = new PedidoEnderecoADO().Carregar(entidade);
            if (endereco != null)
            {
                entidade.PedidoEndereco = endereco;
                entidade.PedidoEndereco.Municipio = new MunicipioADO().Carregar(entidade.PedidoEndereco.Municipio.MunicipioId);
                entidade.PedidoEndereco.Municipio.Regiao = new RegiaoADO().Carregar(entidade.PedidoEndereco.Municipio.Regiao.RegiaoId);
            }

            //Carrega Lista de Pedidos
            entidade.PedidoItens = new List<PedidoItem>();
            var pedidoItemFH = new PedidoItemFH() { PedidoId = entidade.PedidoId.ToString() };
            var itens = new PedidoItemADO().CarregarTodos(0, 0, null, null, pedidoItemFH);
            foreach (var item in itens)
            {
                item.Produto = new ProdutoADO().Carregar(item.Produto);
                item.PedidoItemPromocao = new PedidoItemPromocao();
                item.PedidoItemPromocao.PedidoItemPromocaoId = item.PedidoItemId;
                item.PedidoItemPromocao = new PedidoItemPromocaoADO().Carregar(item.PedidoItemPromocao);
                if (item.PedidoItemPromocao != null && item.PedidoItemPromocao.Promocao != null && item.PedidoItemPromocao.Promocao.PromocaoId > 0)
                {
                    item.PedidoItemPromocao.Promocao = new PromocaoADO().Carregar(item.PedidoItemPromocao.Promocao);
                }
                entidade.PedidoItens.Add(item);
            }

            //Carrega Compra Conjunta
            entidade.PedidoCompraConjunta = new PedidoCompraConjunta();
            entidade.PedidoCompraConjunta.PedidoCompraConjuntaId = entidade.PedidoId;
            entidade.PedidoCompraConjunta = new PedidoCompraConjuntaADO().Carregar(entidade.PedidoCompraConjunta);

            if (entidade.PedidoCompraConjunta != null)
            {
                if (entidade.PedidoCompraConjunta.CompraConjuntaDesconto != null && entidade.PedidoCompraConjunta.CompraConjuntaDesconto.CompraConjuntaDescontoId > 0)
                {
                    entidade.PedidoCompraConjunta.CompraConjuntaDesconto = new CompraConjuntaDescontoADO().Carregar(entidade.PedidoCompraConjunta.CompraConjuntaDesconto);

                    if (entidade.PedidoCompraConjunta.CompraConjuntaDesconto != null && entidade.PedidoCompraConjunta.CompraConjuntaDesconto.CompraConjunta != null && entidade.PedidoCompraConjunta.CompraConjuntaDesconto.CompraConjunta.CompraConjuntaId > 0)
                    {
                        entidade.PedidoCompraConjunta.CompraConjuntaDesconto.CompraConjunta = new CompraConjuntaADO().Carregar(entidade.PedidoCompraConjunta.CompraConjuntaDesconto.CompraConjunta);

                        if (entidade.PedidoCompraConjunta.CompraConjuntaDesconto.CompraConjunta != null && entidade.PedidoCompraConjunta.CompraConjuntaDesconto.CompraConjunta.CompraConjuntaStatus != null && entidade.PedidoCompraConjunta.CompraConjuntaDesconto.CompraConjunta.CompraConjuntaStatus.CompraConjuntaStatusId > 0)
                            entidade.PedidoCompraConjunta.CompraConjuntaDesconto.CompraConjunta.CompraConjuntaStatus = new CompraConjuntaStatusADO().Carregar(entidade.PedidoCompraConjunta.CompraConjuntaDesconto.CompraConjunta.CompraConjuntaStatus);
                    }
                }
            }
        }

        /// <summary>
        /// Método que retorna uma coleção de Pedido com Endereco.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos Pedidos.</returns>
        public IEnumerable<Pedido> CarregarTodosComDependencias(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {
            List<Pedido> entidadesRetorno = new List<Pedido>();

            StringBuilder sbSQL = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();
            StringBuilder sbOrder = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            // Monta o "OrderBy"
            if (ordemColunas != null)
            {
                for (int i = 0; i < ordemColunas.Length; i++)
                {
                    if (sbOrder.Length > 0) { sbOrder.Append(", "); }
                    sbOrder.Append(ordemColunas[i] + " " + ordemSentidos[i]);
                }
                if (sbOrder.Length > 0) { sbOrder.Insert(0, " ORDER BY "); }
            }
            else
            {
                sbOrder.Append(" ORDER BY pedidoId");
            }


            if (registrosPagina > 0)
            {

                sbSQL.Append("SELECT *  ");
                sbSQL.Append(", PedidoItem.pedidoItemId ");
                sbSQL.Append(", PedidoItem.produtoId ");
                sbSQL.Append(", PedidoItem.quantidade ");
                sbSQL.Append(", PedidoItem.valorUnitarioBase ");
                sbSQL.Append(", PedidoItem.valorUnitarioFinal ");
                sbSQL.Append(", PedidoStatus.statusPedido ");
                sbSQL.Append(", Produto.codigoEAN13 ");
                sbSQL.Append(", Produto.codigoProduto ");
                sbSQL.Append(", Produto.disponivel ");
                sbSQL.Append(", Produto.exibirSite ");
                sbSQL.Append(", Produto.fabricanteId ");
                sbSQL.Append(", Produto.homologado ");
                sbSQL.Append(", Produto.nomeProduto ");
                sbSQL.Append(", Produto.peso ");
                sbSQL.Append(", Produto.produtoTipoId ");
                sbSQL.Append(", Produto.utilizaFrete ");
                sbSQL.Append(", Produto.valorOferta ");
                sbSQL.Append(", Produto.valorUnitario ");
                sbSQL.Append(", PedidoEndereco.bairro ");
                sbSQL.Append(", PedidoEndereco.cep ");
                sbSQL.Append(", PedidoEndereco.complemento ");
                sbSQL.Append(", PedidoEndereco.enderecoTipoId ");
                sbSQL.Append(", PedidoEndereco.logradouro ");
                sbSQL.Append(", PedidoEndereco.municipioId ");
                sbSQL.Append(", PedidoEndereco.numero ");
                sbSQL.Append(", PedidoEndereco.pedidoEnderecoId ");
                sbSQL.Append(", EnderecoTipo.tipo ");
                sbSQL.Append(", Municipio.codigoIbge ");
                sbSQL.Append(", Municipio.nomeMunicipio ");
                sbSQL.Append(", Municipio.regiaoId ");
                sbSQL.Append(", Regiao.nomeRegiao ");
                sbSQL.Append(", Regiao.uf ");
                sbSQL.Append(", Pagamento.codigoLegadoMeioPagamentoFaixa ");
                sbSQL.Append(", Pagamento.codigoTransacao ");
                sbSQL.Append(", Pagamento.meioPagamentoId ");
                sbSQL.Append(", Pagamento.numeroParcelas ");
                sbSQL.Append(", MeioPagamento.ativo ");
                sbSQL.Append(", MeioPagamento.codigoGateway ");
                sbSQL.Append(", MeioPagamento.codigoLegado ");
                sbSQL.Append(", MeioPagamento.nome ");
                sbSQL.Append("FROM ( ");
                sbSQL.Append("SELECT Pedido.* ");
                sbSQL.Append(", ROW_NUMBER() OVER (ORDER BY dataHoraPedido DESC) R ");
                sbSQL.Append(" FROM Pedido ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                {
                    sbSQL.Append(string.Concat(" WHERE ", filtro.GetWhereString()));
                }
                sbSQL.Append(") #Q ");
                sbSQL.Append("INNER JOIN PedidoItem ON #Q.pedidoId=PedidoItem.pedidoId ");
                sbSQL.Append(" INNER JOIN PedidoStatus ON #Q.pedidoStatusId=PedidoStatus.pedidoStatusId ");
                sbSQL.Append(" INNER JOIN Produto ON PedidoItem.produtoId=Produto.produtoId ");
                sbSQL.Append(" INNER JOIN pedidoEndereco ON #Q.pedidoId=pedidoEndereco.pedidoId ");
                sbSQL.Append(" INNER JOIN EnderecoTipo ON PedidoEndereco.enderecoTipoId=enderecoTipo.enderecoTipoId ");
                sbSQL.Append(" INNER JOIN Municipio ON PedidoEndereco.municipioId=Municipio.municipioId ");
                sbSQL.Append(" INNER JOIN Regiao ON Municipio.regiaoId=Regiao.regiaoId ");
                sbSQL.Append(" INNER JOIN Pagamento ON #Q.pagamentoId=Pagamento.pagamentoId ");
                sbSQL.Append(" INNER JOIN MeioPagamento ON Pagamento.meioPagamentoId=MeioPagamento.meioPagamentoId ");
                sbSQL.Append("WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());
                sbSQL.Append(" ORDER BY dataHoraPedido DESC ");
            }
            else
            {
                sbSQL.Append("SELECT Pedido.* ");
                sbSQL.Append(", PedidoItem.* ");
                sbSQL.Append(", PedidoStatus.* ");
                sbSQL.Append(", Produto.* ");
                sbSQL.Append(", PedidoEndereco.* ");
                sbSQL.Append(", EnderecoTipo.* ");
                sbSQL.Append(", Municipio.* ");
                sbSQL.Append(", Regiao.* ");
                sbSQL.Append(", Pagamento.* ");
                sbSQL.Append(", MeioPagamento.* ");

                sbSQL.Append(" FROM Pedido");
                sbSQL.Append(" INNER JOIN PedidoItem ON Pedido.pedidoId=PedidoItem.pedidoId");
                sbSQL.Append(" INNER JOIN PedidoStatus ON Pedido.pedidoStatusId=PedidoStatus.pedidoStatusId");
                sbSQL.Append(" INNER JOIN Produto ON PedidoItem.produtoId=Produto.produtoId");
                sbSQL.Append(" INNER JOIN pedidoEndereco ON Pedido.pedidoId=pedidoEndereco.pedidoId");
                sbSQL.Append(" INNER JOIN EnderecoTipo ON PedidoEndereco.enderecoTipoId=enderecoTipo.enderecoTipoId");
                sbSQL.Append(" INNER JOIN Municipio ON PedidoEndereco.municipioId=Municipio.municipioId");
                sbSQL.Append(" INNER JOIN Regiao ON Municipio.regiaoId=Regiao.regiaoId");
                sbSQL.Append(" INNER JOIN Pagamento ON Pedido.pagamentoId=Pagamento.pagamentoId");
                sbSQL.Append(" INNER JOIN MeioPagamento ON Pagamento.meioPagamentoId=MeioPagamento.meioPagamentoId");

                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                {
                    sbSQL.Append(string.Concat(" WHERE ", filtro.GetWhereString()));
                }

                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            Pedido pedido = new Pedido();
            while (reader.Read())
            {
                if (pedido.PedidoId != Convert.ToInt32(reader["PedidoId"].ToString()))
                {
                    pedido = new Pedido();
                    PopulaPedido(reader, pedido);
                    pedido.PedidoItens = new List<PedidoItem>();
                    entidadesRetorno.Add(pedido);

                    PagamentoADO.PopulaPagamento(reader, pedido.Pagamento);

                    PedidoEndereco enderecoDoPedido = new PedidoEndereco();
                    PedidoEnderecoADO.PopulaPedidoEndereco(reader, enderecoDoPedido);

                    enderecoDoPedido.Municipio = new Municipio();
                    MunicipioADO.PopulaMunicipio(reader, enderecoDoPedido.Municipio);

                    enderecoDoPedido.Municipio.Regiao = new Regiao();
                    RegiaoADO.PopulaRegiao(reader, enderecoDoPedido.Municipio.Regiao);

                    // pedido.PedidoEndereco = new PedidoEndereco();
                    pedido.PedidoEndereco = enderecoDoPedido;
                    //PedidoEnderecoADO.PopulaPedidoEndereco(reader, pedido.PedidoEndereco);
                    EnderecoTipoADO.PopulaEnderecoTipo(reader, pedido.PedidoEndereco.EnderecoTipo);

                    pedido.Pagamento.MeioPagamento = new MeioPagamento();
                    MeioPagamentoADO.PopulaMeioPagamento(reader, pedido.Pagamento.MeioPagamento);

                    pedido.PedidoStatus = new PedidoStatus();
                    PedidoStatusADO.PopulaPedidoStatus(reader, pedido.PedidoStatus);
                }

                PedidoItem itemDoPedido = new PedidoItem();
                PedidoItemADO.PopulaPedidoItem(reader, itemDoPedido);

                itemDoPedido.Produto = new Produto();
                ProdutoADO.PopulaProduto(reader, itemDoPedido.Produto);

                pedido.PedidoItens.Add(itemDoPedido);
            }

            reader.Close();

            return entidadesRetorno;

        }

        public IEnumerable<ConfirmacaoPedidoVH> CarregarComDependenciasPorCarrinho(Carrinho carrinho)
        {
            //List<ConfirmacaoPedidoVH> p
            List<ConfirmacaoPedidoVH> pedidos = new List<ConfirmacaoPedidoVH>();

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            sbSQL.Append("SELECT Pedido.* ");
            sbSQL.Append(", PedidoItem.* ");
            sbSQL.Append(", PedidoStatus.* ");
            sbSQL.Append(", p.* ");
            sbSQL.Append(", PedidoEndereco.* ");
            sbSQL.Append(", EnderecoTipo.* ");
            sbSQL.Append(", Municipio.* ");
            sbSQL.Append(", Regiao.* ");
            sbSQL.Append(", Pagamento.* ");
            sbSQL.Append(", MeioPagamento.* ");
            sbSQL.Append(", TransportadoraServico.* ");
            sbSQL.Append(", Transportadora.* ");
            sbSQL.Append(", PedidoItemCompraConjunta.* ");
            sbSQL.Append(" ,'#TIPO#' Tipo ");
            sbSQL.Append(" ,P.ProdutoId ");
            sbSQL.Append(" ,P.NomeProduto ");
            sbSQL.Append(" ,PedidoItem.ValorUnitarioBase ");
            sbSQL.Append(" ,PedidoItem.ValorUnitarioFinal ");
            sbSQL.Append(" ,PedidoItem.Quantidade ");
            sbSQL.Append(" ,P.ProdutoTipoId ");
            sbSQL.Append(" ,A.NomeArquivo ");
            //sbSQL.Append(" ,#CAMPO_PESO# ")
            //sbSQL.Append(" ,CI.quantidade ");;
            //sbSQL.Append(" ,CI.carrinhoItemId ");
            //sbSQL.Append(" ,cicc.* ");

            //sbSQL.Append(" FROM Produto P   ");


            sbSQL.Append(" FROM Pedido");
            sbSQL.Append(" INNER JOIN PedidoItem ON Pedido.pedidoId=PedidoItem.pedidoId");
            sbSQL.Append(" INNER JOIN PedidoStatus ON Pedido.pedidoStatusId=PedidoStatus.pedidoStatusId");
            sbSQL.Append(" INNER JOIN Produto P ON PedidoItem.produtoId=P.produtoId");
            sbSQL.Append(" INNER JOIN pedidoEndereco ON Pedido.pedidoId=pedidoEndereco.pedidoId");
            sbSQL.Append(" INNER JOIN EnderecoTipo ON PedidoEndereco.enderecoTipoId=enderecoTipo.enderecoTipoId");
            sbSQL.Append(" INNER JOIN Municipio ON PedidoEndereco.municipioId=Municipio.municipioId");
            sbSQL.Append(" INNER JOIN Regiao ON Municipio.regiaoId=Regiao.regiaoId");
            sbSQL.Append(" INNER JOIN Pagamento ON Pedido.pagamentoId=Pagamento.pagamentoId");
            sbSQL.Append(" INNER JOIN MeioPagamento ON Pagamento.meioPagamentoId=MeioPagamento.meioPagamentoId");
            sbSQL.Append(" JOIN TransportadoraServico ON TransportadoraServico.TransportadoraServicoId = Pedido.TransportadoraServicoId");
            sbSQL.Append(" JOIN Transportadora ON Transportadora.transportadoraId = TransportadoraServico.transportadoraId");
            sbSQL.Append(" LEFT JOIN PedidoItemCompraConjunta ON PedidoItemCompraConjunta.pedidoItemCompraConjuntaId = PedidoItem.pedidoItemId");
            sbSQL.Append(" #RELACIONAMENTO# ");
            //sbSQL.Append(" INNER JOIN Titulo T ON t.tituloId = relacionamentoTitulo.tituloId  ");
            sbSQL.Append(" LEFT JOIN dbo.ProdutoImagem TIMG ON TIMG.produtoId = P.produtoId AND TIMG.produtoImagemTipoId = 1  ");
            sbSQL.Append(" LEFT JOIN Arquivo A ON A.ArquivoId = TIMG.ArquivoId  ");
            sbSQL.Append(" WHERE Pedido.carrinhoId = @carrinhoId");
            // Faz a replicação e a substituição conforme o relacionamento
            // TítuloImpresso
            string relacionamentoTituloImpresso = string.Concat("INNER JOIN TituloImpresso relacionamentoTitulo ON p.produtoId = relacionamentoTitulo.tituloImpressoId AND P.ProdutoTipoId = ", Convert.ToInt32(TipoDeProduto.TituloImpresso).ToString()
                                                              , " INNER JOIN Titulo T ON t.tituloId = relacionamentoTitulo.tituloId  ");
            string sqlTituloImpresso = sbSQL.ToString().Replace("#RELACIONAMENTO#", relacionamentoTituloImpresso)
                                                       .Replace("#TIPO#", "Livro Impresso");
            // Título Eletrônico
            string relacionamentoTituloEletronico = string.Concat("INNER JOIN TituloEletronico relacionamentoTitulo ON p.produtoId = relacionamentoTitulo.tituloEletronicoId AND P.ProdutoTipoId = ", Convert.ToInt32(TipoDeProduto.TituloEletronico).ToString()
                                                                , " INNER JOIN Titulo T ON t.tituloId = relacionamentoTitulo.tituloId  ");
            string sqlTituloEletronico = sbSQL.ToString().Replace("#RELACIONAMENTO#", relacionamentoTituloEletronico)
                                                       .Replace("#TIPO#", "eBook");
            // Capítulo Impresso
            StringBuilder relacionamentoCapituloImpresso = new StringBuilder();
            relacionamentoCapituloImpresso.AppendFormat(" JOIN capituloImpresso ON capituloImpresso.capituloImpressoId = P.ProdutoId  AND P.ProdutoTipoId = {0}", Convert.ToInt32(TipoDeProduto.TituloEletronico).ToString());
            relacionamentoCapituloImpresso.Append(" JOIN TituloImpresso relacionamentoTitulo ON relacionamentoTitulo.tituloImpressoId = capituloImpresso.tituloImpressoId ");
            relacionamentoCapituloImpresso.Append(" INNER JOIN Titulo T ON t.tituloId = relacionamentoTitulo.tituloId  ");
            string sqlCapituloImpresso = sbSQL.ToString().Replace("#RELACIONAMENTO#", relacionamentoCapituloImpresso.ToString())
                                                       .Replace("#TIPO#", "Capítulo Impresso");
            // Capítulo Eletrônico
            StringBuilder relacionamentoCapituloEletronico = new StringBuilder();
            relacionamentoCapituloEletronico.AppendFormat(" JOIN capituloEletronico ON capituloEletronico.capituloEletronicoId = P.ProdutoId  AND P.ProdutoTipoId = {0}", Convert.ToInt32(TipoDeProduto.TituloEletronico).ToString());
            relacionamentoCapituloEletronico.Append(" JOIN TituloEletronico relacionamentoTitulo ON relacionamentoTitulo.tituloEletronicoId = capituloEletronico.tituloEletronicoId ");
            relacionamentoCapituloEletronico.Append(" INNER JOIN Titulo T ON t.tituloId = relacionamentoTitulo.tituloId  ");
            string sqlCapituloEletronico = sbSQL.ToString().Replace("#RELACIONAMENTO#", relacionamentoCapituloEletronico.ToString())
                                                       .Replace("#TIPO#", "Capítulo eBook");
            // Revista Edição
            string relacionamentoRevistaEdicao = string.Concat(" JOIN RevistaEdicao ON RevistaEdicao.RevistaEdicaoId = P.ProdutoId  AND P.ProdutoTipoId = ", Convert.ToInt32(TipoDeProduto.Revista).ToString());
            string sqlRevistaEdicao = sbSQL.ToString().Replace("#RELACIONAMENTO#", relacionamentoRevistaEdicao)
                                                       .Replace("#TIPO#", "Revista");
            // Revista Assinatura
            string relacionamentoRevistaAssinatura = string.Concat(" JOIN RevistaAssinatura ON RevistaAssinatura.RevistaAssinaturaId = P.ProdutoId  AND P.ProdutoTipoId = ", Convert.ToInt32(TipoDeProduto.RevistaAssinatura).ToString());
            string sqlRevistaAssinatura = sbSQL.ToString().Replace("#RELACIONAMENTO#", relacionamentoRevistaAssinatura)
                                                       .Replace("#TIPO#", "Revista Assinatura");


            sbSQL = new StringBuilder();
            sbSQL.Append(string.Concat("(", sqlTituloImpresso));
            sbSQL.Append(") UNION ALL (");
            sbSQL.Append(sqlTituloEletronico);
            sbSQL.Append(") UNION ALL (");
            sbSQL.Append(sqlCapituloImpresso);
            sbSQL.Append(") UNION ALL (");
            sbSQL.Append(sqlCapituloEletronico);
            sbSQL.Append(") UNION ALL (");
            sbSQL.Append(sqlRevistaEdicao);
            sbSQL.Append(") UNION ALL (");
            sbSQL.Append(string.Concat(sqlRevistaAssinatura, ")"));

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@carrinhoId", DbType.Int32, carrinho.CarrinhoId);

            reader = _db.ExecuteReader(command);

            ConfirmacaoPedidoVH pedidoAnterior = null; // new Pedido();
            ConfirmacaoPedidoVH pedido = new ConfirmacaoPedidoVH();
            int totalPedidos = 0;
            while (reader.Read())
            {
                totalPedidos++;
                //PedidoADO.PopulaPedido(reader, pedido);
                if ((pedidoAnterior == null) || (Convert.ToInt32(reader["PedidoId"].ToString()) != pedidoAnterior.PedidoId))
                {
                    totalPedidos++;
                    if (pedidoAnterior != null)
                    {
                        pedidos.Add(pedidoAnterior);
                    }
                    pedido = new ConfirmacaoPedidoVH();
                    pedido.PedidoItens = new List<PedidoItem>();
                    pedido.PedidoId = Convert.ToInt32(reader["PedidoId"].ToString());
                    PopulaPedido(reader, pedido);

                    pedido.PedidoStatus = new PedidoStatus();
                    PedidoStatusADO.PopulaPedidoStatus(reader, pedido.PedidoStatus);

                    PedidoEndereco enderecoDoPedido = new PedidoEndereco();
                    PedidoEnderecoADO.PopulaPedidoEndereco(reader, enderecoDoPedido);

                    enderecoDoPedido.Municipio = new Municipio();
                    MunicipioADO.PopulaMunicipio(reader, enderecoDoPedido.Municipio);

                    enderecoDoPedido.Municipio.Regiao = new Regiao();
                    RegiaoADO.PopulaRegiao(reader, enderecoDoPedido.Municipio.Regiao);

                    //pedido.PedidoEndereco = new PedidoEndereco();
                    //PedidoEnderecoADO.PopulaPedidoEndereco(reader, pedido.PedidoEndereco);
                    enderecoDoPedido.EnderecoTipo = new EnderecoTipo();
                    EnderecoTipoADO.PopulaEnderecoTipo(reader, enderecoDoPedido.EnderecoTipo);

                    pedido.PedidoEndereco = enderecoDoPedido;

                    pedido.TransportadoraServico = new TransportadoraServico();
                    TransportadoraServicoADO.PopulaTransportadoraServico(reader, pedido.TransportadoraServico);
                    pedido.TransportadoraServico.Transportadora = new Transportadora();
                    TransportadoraADO.PopulaTransportadora(reader, pedido.TransportadoraServico.Transportadora);

                    pedido.Pagamento = new Pagamento();
                    PagamentoADO.PopulaPagamento(reader, pedido.Pagamento);
                    pedido.Pagamento.MeioPagamento = new MeioPagamento();
                    MeioPagamentoADO.PopulaMeioPagamento(reader, pedido.Pagamento.MeioPagamento);

                    pedido.Carrinho = carrinho;
                }

                PedidoItem itemDoPedido = new PedidoItem();
                PedidoItemADO.PopulaPedidoItem(reader, itemDoPedido);

                PedidoItemVH pedidoItemVH = new PedidoItemVH();
                PopulaItemPedido(reader, pedidoItemVH);
                if (pedido.PedidoItensVH == null)
                {
                    pedido.PedidoItensVH = new List<PedidoItemVH>();
                }
                pedido.PedidoItensVH.Add(pedidoItemVH);

                itemDoPedido.Produto = new Produto();
                ProdutoADO.PopulaProduto(reader, itemDoPedido.Produto);
                //if (reader["ProdutoImagemId"] != null)
                //{
                //    itemDoPedido.Produto.ProdutoImagens = new List<ProdutoImagem>();
                //    itemDoPedido.Produto.ProdutoImagens.Add(new ProdutoImagem(Convert.ToInt32(reader["ProdutoImagemId"] )));
                //    ProdutoImagemADO.PopulaProdutoImagem(reader, itemDoPedido.Produto.ProdutoImagens.First());
                //}

                if (reader["PedidoItemCompraConjuntaId"] != null)
                {
                    itemDoPedido.PedidoItemCompraConjunta = new PedidoItemCompraConjunta();
                    PedidoItemCompraConjuntaADO.PopulaPedidoItemCompraConjunta(reader, itemDoPedido.PedidoItemCompraConjunta);
                }

                pedido.PedidoItens.Add(itemDoPedido);

                pedidoAnterior = pedido;

            }
            //Insere o último pedido
            pedidos.Add(pedidoAnterior);

            reader.Close();

            return pedidos;

        }

        private void PopulaItemPedido(IDataReader reader, PedidoItemVH entidade)
        {
            if (reader["produtoId"] != DBNull.Value)
                entidade.ProdutoId = Convert.ToInt32(reader["produtoId"].ToString());
            //if (reader["carrinhoItemId"] != DBNull.Value)
            //    entidade.CarrinhoItemId = Convert.ToInt32(reader["carrinhoItemId"].ToString());
            //if (reader["CarrinhoItemCompraConjuntaId"] != DBNull.Value)
            //    entidade.CarrinhoItemCompraConjuntaId = Convert.ToInt32(reader["CarrinhoItemCompraConjuntaId"].ToString());
            //if (reader["CompraConjuntaId"] != DBNull.Value)
            //    entidade.CompraConjuntaId = Convert.ToInt32(reader["CompraConjuntaId"].ToString());
            //if (reader["TituloId"] != DBNull.Value)
            //    entidade.TituloId = Convert.ToInt32(reader["TituloId"].ToString());
            if (reader["NomeProduto"] != DBNull.Value)
                entidade.NomeProduto = reader["NomeProduto"].ToString();
            if (reader["Tipo"] != DBNull.Value)
                entidade.Tipo = reader["Tipo"].ToString();
            if (reader["ValorUnitarioBase"] != DBNull.Value)
                entidade.ValorUnitario = Convert.ToDecimal(reader["ValorUnitarioBase"].ToString());
            //if (reader["ValorOferta"] != DBNull.Value)
            //    entidade.Valor = Convert.ToDecimal(reader["ValorOferta"].ToString());
            if (reader["ValorUnitarioFinal"] != DBNull.Value)
                entidade.ValorFinal = Convert.ToDecimal(reader["ValorUnitarioFinal"].ToString());
            //if (reader["DataLancamento"] != DBNull.Value)
            //    entidade.DataLancamento = Convert.ToDateTime(reader["DataLancamento"].ToString());
            if (reader["NomeArquivo"] != DBNull.Value)
                entidade.NomeArquivo = reader["NomeArquivo"].ToString();
            //if (reader["Parcelas"] != DBNull.Value)
            //    entidade.Parcelas = Convert.ToInt32(reader["Parcelas"].ToString());
            //if (reader["exibirSite"] != DBNull.Value)
            //    entidade.ExibirSite = Boolean.Parse(reader["exibirSite"].ToString());
            //if (reader["disponivel"] != DBNull.Value)
            //    entidade.Disponivel = Boolean.Parse(reader["disponivel"].ToString());
            if (reader["quantidade"] != DBNull.Value)
                entidade.Quantidade = Convert.ToDecimal(reader["quantidade"].ToString()); ;
            //if (reader["Peso"] != DBNull.Value)
            //    entidade.Peso = Convert.ToDecimal(reader["Peso"].ToString()); ;
            //if (reader["utilizaFrete"] != DBNull.Value)
            //    entidade.UtilizaFrete = Boolean.Parse(reader["utilizaFrete"].ToString());
        }

        /// <summary>
        /// Carrega lista de pedidos para ser enviado ao web service.
        /// </summary>
        /// <returns>Lista de pedidos para ser exportado</returns>
        public IEnumerable<Pedido> CarregaPedidoParaExportacao()
        {
            List<Pedido> entidadesRetorno = new List<Pedido>();

            StringBuilder sbSQL = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();

            DbCommand command;
            IDataReader reader;

            sbSQL.Append("SELECT Pedido.* ");
            sbSQL.Append(", PedidoItem.* ");
            sbSQL.Append(", PedidoStatus.* ");
            sbSQL.Append(", Produto.* ");
            sbSQL.Append(", PedidoEndereco.* ");
            sbSQL.Append(", EnderecoTipo.* ");
            sbSQL.Append(", Municipio.* ");
            sbSQL.Append(", Regiao.* ");
            sbSQL.Append(", Pagamento.* ");
            sbSQL.Append(", MeioPagamento.* ");
            sbSQL.Append(" FROM Pedido");
            sbSQL.Append(" INNER JOIN PedidoItem ON Pedido.pedidoId=PedidoItem.pedidoId");
            sbSQL.Append(" INNER JOIN PedidoStatus ON Pedido.pedidoStatusId=PedidoStatus.pedidoStatusId");
            sbSQL.Append(" INNER JOIN Produto ON PedidoItem.produtoId=Produto.produtoId");
            sbSQL.Append(" INNER JOIN pedidoEndereco ON Pedido.pedidoId=pedidoEndereco.pedidoId");
            sbSQL.Append(" INNER JOIN EnderecoTipo ON PedidoEndereco.enderecoTipoId=enderecoTipo.enderecoTipoId");
            sbSQL.Append(" INNER JOIN Municipio ON PedidoEndereco.municipioId=Municipio.municipioId");
            sbSQL.Append(" INNER JOIN Regiao ON Municipio.regiaoId=Regiao.regiaoId");
            sbSQL.Append(" INNER JOIN Pagamento ON Pedido.pagamentoId=Pagamento.pagamentoId");
            sbSQL.Append(" INNER JOIN MeioPagamento ON Pagamento.meioPagamentoId=MeioPagamento.meioPagamentoId");
            sbSQL.Append(" WHERE NOT EXISTS (SELECT PC.PedidoId FROM PedidoControle PC WHERE PC.PedidoId=Pedido.PedidoId)");


            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            Pedido pedido = new Pedido();
            while (reader.Read())
            {
                if (pedido.PedidoId != Convert.ToInt32(reader["PedidoId"].ToString()))
                {
                    pedido = new Pedido();
                    PopulaPedido(reader, pedido);
                    pedido.PedidoItens = new List<PedidoItem>();
                    entidadesRetorno.Add(pedido);

                    PedidoEndereco enderecoDoPedido = new PedidoEndereco();
                    PedidoEnderecoADO.PopulaPedidoEndereco(reader, enderecoDoPedido);

                    enderecoDoPedido.Municipio = new Municipio();
                    MunicipioADO.PopulaMunicipio(reader, enderecoDoPedido.Municipio);

                    enderecoDoPedido.Municipio.Regiao = new Regiao();
                    RegiaoADO.PopulaRegiao(reader, enderecoDoPedido.Municipio.Regiao);

                    pedido.PedidoEndereco = enderecoDoPedido;
                    EnderecoTipoADO.PopulaEnderecoTipo(reader, pedido.PedidoEndereco.EnderecoTipo);

                    pedido.Pagamento.MeioPagamento = new MeioPagamento();
                    MeioPagamentoADO.PopulaMeioPagamento(reader, pedido.Pagamento.MeioPagamento);

                    pedido.PedidoStatus = new PedidoStatus();
                    PedidoStatusADO.PopulaPedidoStatus(reader, pedido.PedidoStatus);
                }

                PedidoItem itemDoPedido = new PedidoItem();
                PedidoItemADO.PopulaPedidoItem(reader, itemDoPedido);

                itemDoPedido.Produto = new Produto();
                ProdutoADO.PopulaProduto(reader, itemDoPedido.Produto);

                pedido.PedidoItens.Add(itemDoPedido);
            }

            reader.Close();

            return entidadesRetorno;
        }

        public List<Pedido> CarregarPedidosPorCompraConjunta(Int32 compraConjuntaId)
        {
            List<Pedido> entidadesRetorno = new List<Pedido>();

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            sbSQL.Append("SELECT P.pedidoId, ");
            sbSQL.Append("       P.usuarioId, ");
            sbSQL.Append("       P.dataHoraPedido, ");
            sbSQL.Append("       P.carrinhoId, ");
            sbSQL.Append("       P.pedidoStatusId, ");
            sbSQL.Append("       P.freteValor, ");
            sbSQL.Append("       P.valorPedido, ");
            sbSQL.Append("       P.pagamentoId, ");
            sbSQL.Append("       P.transportadoraServicoId, ");
            sbSQL.Append("       P.PedidoCodigo, ");
            sbSQL.Append("       PEI.pedidoItemId, ");
            sbSQL.Append("       PEI.produtoId, ");
            sbSQL.Append("       PEI.quantidade, ");
            sbSQL.Append("       PEI.valorUnitarioBase, ");
            sbSQL.Append("       PEI.valorUnitarioFinal, ");
            sbSQL.Append("       U.nomeUsuario, ");
            sbSQL.Append("       U.cadastroPessoa, ");
            sbSQL.Append("       U.emailUsuario ");
            sbSQL.Append("FROM Pedido P ");
            sbSQL.Append("INNER JOIN PedidoItem PEI ");
            sbSQL.Append("    ON P.pedidoId = PEI.pedidoId ");
            sbSQL.Append("INNER JOIN PedidoCompraConjunta PCC ");
            sbSQL.Append("    ON P.pedidoId = PCC.pedidoCompraConjuntaId ");
            sbSQL.Append("INNER JOIN Usuario U ");
            sbSQL.Append("    ON P.usuarioId = U.usuarioId ");
            sbSQL.Append("WHERE P.pedidoStatusId = 4 ");
            sbSQL.Append("      AND PCC.compraConjuntaId = @compraConjuntaId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@compraConjuntaId", DbType.Int32, compraConjuntaId);

            reader = _db.ExecuteReader(command);

            Pedido pedido = new Pedido();
            while (reader.Read())
            {
                if (pedido.PedidoId != Convert.ToInt32(reader["pedidoId"].ToString()))
                {
                    pedido = new Pedido();
                    PopulaPedido(reader, pedido);
                    pedido.PedidoItens = new List<PedidoItem>();
                    entidadesRetorno.Add(pedido);

                    PopulaUsuario(reader, pedido);
                }

                PedidoItem itemDoPedido = new PedidoItem();
                this.PopulaItemPedido(reader, itemDoPedido);

                pedido.PedidoItens.Add(itemDoPedido);
            }

            reader.Close();

            return entidadesRetorno;
        }

        private void PopulaItemPedido(IDataReader reader, PedidoItem entidade)
        {
            if (reader["pedidoItemId"] != DBNull.Value)
            {
                entidade.PedidoItemId = Convert.ToInt32(reader["pedidoItemId"].ToString());
            }

            if (reader["produtoId"] != DBNull.Value)
            {
                entidade.Produto = new Produto();
                entidade.Produto.ProdutoId = Convert.ToInt32(reader["produtoId"].ToString());
            }

            if (reader["quantidade"] != DBNull.Value)
            {
                entidade.Quantidade = Convert.ToInt32(reader["quantidade"].ToString());
            }

            if (reader["valorUnitarioBase"] != DBNull.Value)
            {
                entidade.ValorUnitarioBase = Convert.ToDecimal(reader["valorUnitarioBase"].ToString());
            }

            if (reader["valorUnitarioFinal"] != DBNull.Value)
            {
                entidade.ValorUnitarioFinal = Convert.ToDecimal(reader["valorUnitarioFinal"].ToString());
            }
        }

        private void PopulaUsuario(IDataReader reader, Pedido pedido)
        {
            pedido.Usuario = new Usuario();

            if (reader["usuarioId"] != DBNull.Value)
            {
                pedido.Usuario.UsuarioId = Convert.ToInt32(reader["usuarioId"].ToString());
            }

            if (reader["nomeUsuario"] != DBNull.Value)
            {
                pedido.Usuario.NomeUsuario = reader["nomeUsuario"].ToString();
            }

            if (reader["cadastroPessoa"] != DBNull.Value)
            {
                pedido.Usuario.CadastroPessoa = reader["cadastroPessoa"].ToString();
            }

            if (reader["emailUsuario"] != DBNull.Value)
            {
                pedido.Usuario.EmailUsuario = reader["emailUsuario"].ToString();
            }
        }

        /// <summary>
        /// Carrega Apenas os Pedidos que foram finalizados (STATUS =1 ) e que não foram sincronizados
        /// </summary>
        /// <returns>Retorna uma List de pedidos</returns>
        public IList<Pedido> CarregarFinalizadosNaoSincronizados()
        {
            IList<Pedido> entidadesRetorno = new List<Pedido>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(@"SELECT 
								Pedido.* 
							FROM 
								Pedido
							WHERE 
								Pedido.pedidoStatusId = 1 
								AND Pedido.pedidoId 
								NOT IN (
										SELECT 
											pedidoId 
										FROM 
											pedidoControle
										)");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Pedido entidadeRetorno = new Pedido();
                PopulaPedido(reader, entidadeRetorno);
                entidadeRetorno.PedidoItens = new PedidoItemADO().CarregarItensDoPedido(entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }

            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="promocaoId"></param>
        /// <returns></returns>
        public Int32 ContarPedidosPorPromocao(Int32 promocaoId)
        {
            Int32 retorno = 0;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(DISTINCT R.pedidoId) FROM ( ");
            sbSQL.Append("    SELECT P.pedidoId FROM Pedido P INNER JOIN PedidoPromocaoCarrinho PPC ON P.pedidoId = PPC.pedidoId ");
            sbSQL.Append("    WHERE PPC.promocaoId = @promocaoId ");
            sbSQL.Append("    UNION ");
            sbSQL.Append("    SELECT P.pedidoId FROM Pedido P ");
            sbSQL.Append("    INNER JOIN PedidoItem PI ON P.pedidoId = PI.pedidoId ");
            sbSQL.Append("    INNER JOIN PedidoItemPromocao PIP ON PI.pedidoItemId = PIP.pedidoItemPromocaoId ");
            sbSQL.Append("    WHERE PIP.promocaoId = @promocaoId ");
            sbSQL.Append(") AS R ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@promocaoId", DbType.Int32, promocaoId);

            IDataReader reader = _db.ExecuteReader(command);

            if ((reader.Read()) && ((reader["Qtd"] != DBNull.Value)))
            {
                retorno = (int)reader["Qtd"];
            }
            reader.Close();

            return retorno;
        }

        /// <summary>
        /// Método que atualiza o status do pedido.
        /// </summary>
        /// <param name="pedido">Pedido que será atualizado.</param>
        public void AtualizarStatusPedido(Pedido pedido)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE Pedido SET ");
            sbSQL.Append(" pedidoStatusId = @pedidoStatusId");
            sbSQL.Append(" WHERE pedidoId = @pedidoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@pedidoId", DbType.Int32, pedido.PedidoId);
            _db.AddInParameter(command, "@pedidoStatusId", DbType.Int32, pedido.PedidoStatus.PedidoStatusId);

            // Executa a query.
            _db.ExecuteNonQuery(command);
        }

        public int ContarTodosPedidos(IFilterHelper filtro)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(Pedido.pedidoId) AS Total ");
            sbSQL.Append(" FROM Pedido ");
            sbSQL.Append(" INNER JOIN PedidoItem ON Pedido.pedidoId=PedidoItem.pedidoId ");
            sbSQL.Append(" INNER JOIN PedidoStatus ON Pedido.pedidoStatusId=PedidoStatus.pedidoStatusId ");
            sbSQL.Append(" INNER JOIN Produto ON PedidoItem.produtoId=Produto.produtoId ");
            sbSQL.Append(" INNER JOIN pedidoEndereco ON Pedido.pedidoId=pedidoEndereco.pedidoId ");
            sbSQL.Append(" INNER JOIN EnderecoTipo ON PedidoEndereco.enderecoTipoId=enderecoTipo.enderecoTipoId ");
            sbSQL.Append(" INNER JOIN Municipio ON PedidoEndereco.municipioId=Municipio.municipioId ");
            sbSQL.Append(" INNER JOIN Regiao ON Municipio.regiaoId=Regiao.regiaoId ");
            sbSQL.Append(" INNER JOIN Pagamento ON Pedido.pagamentoId=Pagamento.pagamentoId ");
            sbSQL.Append(" INNER JOIN MeioPagamento ON Pagamento.meioPagamentoId=MeioPagamento.meioPagamentoId ");

            if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Executa a query.

            int resultado = (int)_db.ExecuteScalar(command);


            return resultado;
        }
    }
}
