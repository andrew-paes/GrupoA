/*
'===============================================================================
'
'  Template: Gerador Código C#.csgen
'  Script versão: 0.96
'  Script criado por: Leonardo Alves Lindermann (lindermannla@ag2.com.br)
'  Gerado pelo MyGeneration versão # (???)
'
'===============================================================================
*/

using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using GrupoA.BusinessObject;
using GrupoA.FilterHelper;

namespace GrupoA.DataAccess.ADO
{
    public partial class CompraConjuntaADO : ADOSuper, ICompraConjuntaDAL
    {
        #region Métodos

        public IEnumerable<CompraConjunta> CarregarCompraConjuntaEmAberta(CompraConjunta entidade, int compraConjuntaPaginaId)
        {
            List<CompraConjunta> entidadesRetorno = new List<CompraConjunta>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * ");
            sbSQL.Append("FROM CompraConjunta ");
            sbSQL.Append("INNER JOIN CompraConjuntaStatus   ON CompraConjunta.compraConjuntaStatusId=CompraConjuntaStatus.compraConjuntaStatusId ");
            sbSQL.Append("INNER JOIN Produto                ON CompraConjunta.produtoId=Produto.produtoId ");
            sbSQL.Append("WHERE CompraConjuntaStatus.compraConjuntaStatusId = 1 ");
            sbSQL.Append("AND CompraConjunta.compraConjuntaId NOT IN ( SELECT CompraConjunta.compraConjuntaId ");
            sbSQL.Append("FROM CompraConjuntaLocalizacao ");
            sbSQL.Append("INNER JOIN CompraConjunta         ON CompraConjuntaLocalizacao.compraConjuntaId=CompraConjunta.compraConjuntaId ");
            sbSQL.Append("INNER JOIN CompraConjuntaStatus   ON CompraConjunta.compraConjuntaStatusId=CompraConjuntaStatus.compraConjuntaStatusId ");
            sbSQL.Append("WHERE CompraConjuntaStatus.compraConjuntaStatusId = 1 ");
            sbSQL.Append("AND CompraConjuntaLocalizacao.compraConjuntaPaginaId = @compraConjuntaPaginaId ) ");
            sbSQL.Append("ORDER BY Produto.nomeProduto ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@compraConjuntaPaginaId", DbType.Int32, compraConjuntaPaginaId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                CompraConjunta entidadeRetorno = new CompraConjunta();
                PopulaCompraConjunta(reader, entidadeRetorno);
                entidadeRetorno.Produto = new ProdutoADO().Carregar(entidadeRetorno.Produto);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        public IEnumerable<CompraConjunta> CarregarCompraConjuntaComPaginaRelacionada(int compraConjuntaPaginaId)
        {
            List<CompraConjunta> entidadesRetorno = new List<CompraConjunta>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * ");
            sbSQL.Append("FROM CompraConjuntaLocalizacao ");
            sbSQL.Append("INNER JOIN CompraConjunta         ON CompraConjuntaLocalizacao.compraConjuntaId=CompraConjunta.compraConjuntaId ");
            sbSQL.Append("INNER JOIN CompraConjuntaStatus   ON CompraConjunta.compraConjuntaStatusId=CompraConjuntaStatus.compraConjuntaStatusId ");
            sbSQL.Append("INNER JOIN Produto                ON CompraConjunta.produtoId=Produto.produtoId ");
            sbSQL.Append("WHERE CompraConjuntaStatus.compraConjuntaStatusId = 1 ");
            sbSQL.Append("AND CompraConjuntaLocalizacao.compraConjuntaPaginaId = @compraConjuntaPaginaId ");
            sbSQL.Append("ORDER BY Produto.nomeProduto ");
            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@compraConjuntaPaginaId", DbType.Int32, compraConjuntaPaginaId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                CompraConjunta entidadeRetorno = new CompraConjunta();
                PopulaCompraConjuntaComPaginaRelacionada(reader, entidadeRetorno);
                entidadeRetorno.Produto = new ProdutoADO().Carregar(entidadeRetorno.Produto);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        public bool PeriodoConflitante(CompraConjunta compraConjunta)
        {
            bool entidadeRetorno = false;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(@"SELECT
								COUNT(*) AS Total
							FROM
								CompraConjunta                            
							WHERE
								compraConjuntaId != @compraConjuntaId
								AND produtoId = @produtoId
								AND dataHoraFinalizacao IS NULL
								AND ativa = 1
								AND compraConjuntaStatusId = 1 
                                AND
								(
								dataInicialCompra BETWEEN @dataInicialCompra AND @dataFinalCompra
								OR dataFinalCompra BETWEEN @dataInicialCompra AND @dataFinalCompra
								)");
            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@compraConjuntaId", DbType.Int32, compraConjunta.CompraConjuntaId);
            _db.AddInParameter(command, "@produtoId", DbType.Int32, compraConjunta.Produto.ProdutoId);
            _db.AddInParameter(command, "@dataInicialCompra", DbType.DateTime, compraConjunta.DataInicialCompra);
            _db.AddInParameter(command, "@dataFinalCompra", DbType.DateTime, compraConjunta.DataFinalCompra);
            IDataReader entidades = _db.ExecuteReader(command);

            if (entidades.Read())
            {
                if (entidades["Total"] != DBNull.Value)
                {
                    if (Convert.ToInt32(entidades["Total"].ToString()) > 0)
                    {
                        entidadeRetorno = true;
                    }
                }
            }

            entidades.Close();

            return entidadeRetorno;
        }

        public bool CompraConjuntaRelacionada(CompraConjunta compraConjunta)
        {
            bool entidadeRetorno = false;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(@"SELECT
								COUNT(*) AS Total
							FROM 
								CompraConjunta
								INNER JOIN CompraConjuntaDesconto ON CompraConjuntaDesconto.compraConjuntaId = CompraConjunta.compraConjuntaId
								INNER JOIN PedidoCompraConjunta ON PedidoCompraConjunta.compraConjuntaDescontoId = CompraConjuntaDesconto.compraConjuntaDescontoId
							WHERE
								CompraConjunta.compraConjuntaId = @compraConjuntaId");
            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@compraConjuntaId", DbType.Int32, compraConjunta.CompraConjuntaId);
            IDataReader entidades = _db.ExecuteReader(command);

            if (entidades.Read())
            {
                if (entidades["Total"] != DBNull.Value)
                {
                    if (Convert.ToInt32(entidades["Total"].ToString()) > 0)
                    {
                        entidadeRetorno = true;
                    }
                }
            }

            entidades.Close();

            return entidadeRetorno;
        }

        public static void PopulaCompraConjuntaComPaginaRelacionada(IDataReader reader, CompraConjunta entidade)
        {
            if (reader["compraConjuntaId"] != DBNull.Value)
                entidade.CompraConjuntaId = Convert.ToInt32(reader["compraConjuntaId"].ToString());

            if (reader["dataInicialCompra"] != DBNull.Value)
                entidade.DataInicialCompra = Convert.ToDateTime(reader["dataInicialCompra"].ToString());

            if (reader["dataFinalCompra"] != DBNull.Value)
                entidade.DataFinalCompra = Convert.ToDateTime(reader["dataFinalCompra"].ToString());

            if (reader["estoqueSeguranca"] != DBNull.Value)
                entidade.EstoqueSeguranca = Convert.ToInt32(reader["estoqueSeguranca"].ToString());

            if (reader["dataHoraFinalizacao"] != DBNull.Value)
                entidade.DataHoraFinalizacao = Convert.ToDateTime(reader["dataHoraFinalizacao"].ToString());

            if (reader["ativa"] != DBNull.Value)
                entidade.Ativa = Convert.ToBoolean(reader["ativa"].ToString());

            if (reader["produtoId"] != DBNull.Value)
            {
                entidade.Produto = new Produto();
                entidade.Produto.ProdutoId = Convert.ToInt32(reader["produtoId"].ToString());
            }

            if (reader["compraConjuntaStatusId"] != DBNull.Value)
            {
                entidade.CompraConjuntaStatus = new CompraConjuntaStatus();
                entidade.CompraConjuntaStatus.CompraConjuntaStatusId = Convert.ToInt32(reader["compraConjuntaStatusId"].ToString());
            }


        }

        public void ExcluirCompraConjuntaLocalizacao(int compraConjuntaPaginaId)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM CompraConjuntaLocalizacao ");
            sbSQL.Append("WHERE compraConjuntaPaginaId=@compraConjuntaPaginaId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@compraConjuntaPaginaId", DbType.Int32, compraConjuntaPaginaId);

            _db.ExecuteNonQuery(command);
        }

        public void ExcluirCompraConjuntaLocalizacao(CompraConjunta compraconjunta)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM CompraConjuntaLocalizacao ");
            sbSQL.Append("WHERE compraConjuntaId = @compraConjuntaId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@compraConjuntaId", DbType.Int32, compraconjunta.CompraConjuntaId);

            _db.ExecuteNonQuery(command);
        }

        public bool InserirRelacionamentoPagina(int compraConjuntaId, int compraConjuntaPaginaId)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO CompraConjuntaLocalizacao ");
            sbSQL.Append(" ( compraConjuntaId, compraConjuntaPaginaId) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" ( @compraConjuntaId, @compraConjuntaPaginaId) ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@compraConjuntaId", DbType.Int32, compraConjuntaId);
            _db.AddInParameter(command, "@compraConjuntaPaginaId", DbType.Int32, compraConjuntaPaginaId);


            // Executa a query.
            _db.ExecuteNonQuery(command);
            return true;

        }

        public CompraConjunta CarregarCompraConjuntaEmAbertoPorProduto(Produto produto)
        {
            CompraConjunta compraConjunta = new CompraConjunta();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT cc.* ");
            sbSQL.Append("FROM CompraConjunta cc ");
            sbSQL.Append("INNER JOIN Produto p ON p.produtoId = cc.produtoId AND p.produtoId = @produtoId ");
            sbSQL.Append("WHERE cc.compraConjuntaStatusId = 1 ");
            sbSQL.Append(" AND cc.ativa = 1 ");
            sbSQL.Append(" AND cc.dataHoraFinalizacao IS NULL ");
            sbSQL.Append(" AND (cc.dataInicialCompra <= GETDATE() AND cc.dataFinalCompra >= GETDATE()) ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@produtoId", DbType.Int32, produto.ProdutoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                //CompraConjunta entidadeRetorno = new CompraConjunta();
                PopulaCompraConjunta(reader, compraConjunta);
                //entidadeRetorno.Produto = new ProdutoADO().Carregar(entidadeRetorno.Produto);
                //entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return compraConjunta;

        }

        public int TotalComprado(CompraConjunta compraconjunta)
        {
            int total = 0;

            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append(@"SELECT
	                            ISNULL(SUM(PedidoItem.quantidade),0)
                            FROM
	                            PedidoCompraConjunta
                                INNER JOIN Pedido ON Pedido.pedidoId = PedidoCompraConjunta.pedidoCompraConjuntaId
                                INNER JOIN PedidoItem ON PedidoItem.pedidoId = Pedido.pedidoId
                            WHERE
                                PedidoCompraConjunta.CompraConjuntaId = @CompraConjuntaId
                                --AND Pedido.pedidoStatusId = 5
                                ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@CompraConjuntaId", DbType.Int32, compraconjunta.CompraConjuntaId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                total = Convert.ToInt32(reader[0]);
            }

            return total;
        }

        /// <summary>
        /// Carrega compra conjunta válida
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public CompraConjunta CarregarCompraConjuntaValida(CompraConjunta entidade)
        {
            CompraConjunta compraConjunta = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT CC.compraConjuntaId, ");
            sbSQL.Append("       CC.produtoId, ");
            sbSQL.Append("       CC.dataInicialCompra, ");
            sbSQL.Append("       CC.dataFinalCompra, ");
            sbSQL.Append("       CC.estoqueSeguranca, ");
            sbSQL.Append("       CC.dataHoraFinalizacao, ");
            sbSQL.Append("       CC.ativa, ");
            sbSQL.Append("       CC.compraConjuntaStatusId, ");
            sbSQL.Append("       CC.quantidadeLimite, ");
            sbSQL.Append("       CCD.compraConjuntaDescontoId, ");
            sbSQL.Append("       CCD.quantidadeMinima, ");
            sbSQL.Append("       CCD.percentualDesconto ");
            sbSQL.Append("FROM CompraConjunta CC ");
            sbSQL.Append("INNER JOIN CompraConjuntaDesconto CCD ");
            sbSQL.Append("    ON CC.compraConjuntaId = CCD.compraConjuntaId ");
            sbSQL.Append("WHERE CC.compraConjuntaId = @compraConjuntaId ");
            //sbSQL.Append("    AND CC.dataInicialCompra <= GETDATE() ");
            sbSQL.Append("    AND CC.dataFinalCompra < GETDATE() ");
            sbSQL.Append("    AND CC.ativa = @ativa ");
            sbSQL.Append("    AND CC.compraConjuntaStatusId = 1 ");
            sbSQL.Append("ORDER BY CCD.quantidadeMinima ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@compraConjuntaId", DbType.Int32, entidade.CompraConjuntaId);
            _db.AddInParameter(command, "@ativa", DbType.Int32, 1);

            IDataReader reader = _db.ExecuteReader(command);

            Int32 compraConjuntaId = 0;

            while (reader.Read())
            {
                if (Convert.ToInt32(reader["compraConjuntaId"].ToString()) != compraConjuntaId)
                {
                    compraConjuntaId = Convert.ToInt32(reader["compraConjuntaId"].ToString());
                    compraConjunta = new CompraConjunta();
                    PopulaCompraConjunta(reader, compraConjunta);

                    compraConjunta.CompraConjuntaDescontos = new List<CompraConjuntaDesconto>();
                }

                CompraConjuntaDesconto compraConjuntaDesconto = new CompraConjuntaDesconto();
                PopulaCompraConjuntaDesconto(reader, compraConjuntaDesconto);

                compraConjunta.CompraConjuntaDescontos.Add(compraConjuntaDesconto);
            }

            reader.Close();

            return compraConjunta;
        }

        /// <summary>
        /// Método que retorna compra conjunta desconto de acordo com as quantidade já vendidas
        /// </summary>
        /// <param name="compraConjuntaId"></param>
        /// <returns></returns>
        public CompraConjuntaDesconto CarregarCompraConjuntaDesconto(Int32 compraConjuntaId)
        {
            CompraConjuntaDesconto entidadeRetorno = new CompraConjuntaDesconto();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(@"SELECT
	                            TOP 1 *
                            FROM
	                            dbo.CompraConjuntaDesconto CCC
                            WHERE
	                            CCC.quantidadeMinima <= ( CASE WHEN (
										                            SELECT
											                            ISNULL(SUM(pi.quantidade), 0) AS TotalVendido
										                            FROM
											                            dbo.Pedido P
											                            INNER JOIN dbo.PedidoItem PI ON PI.pedidoId = P.pedidoId
											                            INNER JOIN dbo.PedidoCompraConjunta PCC ON P.pedidoId = PCC.pedidoCompraConjuntaId
										                            WHERE
                                                                        PCC.compraConjuntaId = CCC.compraConjuntaId
											                            --AND P.pedidoStatusId = 5
										                            ) < (
											                            SELECT
												                            MIN(CCD.quantidadeMinima)
											                            FROM
												                            dbo.CompraConjuntaDesconto CCD
											                            WHERE
												                            CCD.compraConjuntaId = CCC.compraConjuntaId
											                            )
									                            THEN (
										                            SELECT
											                            MIN(CCD.quantidadeMinima)
										                            FROM
											                            dbo.CompraConjuntaDesconto CCD
										                            WHERE
											                            CCD.compraConjuntaId = CCC.compraConjuntaId
										                            )
									                            ELSE (
										                            SELECT
											                            ISNULL(SUM(pi.quantidade), 0) AS TotalVendido
										                            FROM
											                            dbo.Pedido P
											                            INNER JOIN dbo.PedidoItem PI ON PI.pedidoId = P.pedidoId
											                            INNER JOIN dbo.PedidoCompraConjunta PCC ON P.pedidoId = PCC.pedidoCompraConjuntaId
										                            WHERE
                                                                        PCC.compraConjuntaId = CCC.compraConjuntaId
											                            --AND P.pedidoStatusId = 5
										                            )
									                            END
							                            )
	                            AND ccc.compraConjuntaId = @compraConjuntaId
                            ORDER BY
	                            percentualDesconto DESC");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@compraConjuntaId", DbType.Int32, compraConjuntaId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                PopulaCompraConjuntaDesconto(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }

        public static void PopulaCompraConjuntaDesconto(IDataReader reader, CompraConjuntaDesconto entidade)
        {
            if (reader["compraConjuntaDescontoId"] != DBNull.Value)
                entidade.CompraConjuntaDescontoId = Convert.ToInt32(reader["compraConjuntaDescontoId"].ToString());

            if (reader["compraConjuntaId"] != DBNull.Value)
            {
                entidade.CompraConjunta = new CompraConjunta();
                entidade.CompraConjunta.CompraConjuntaId = Convert.ToInt32(reader["compraConjuntaId"].ToString());
            }

            if (reader["quantidadeMinima"] != DBNull.Value)
                entidade.QuantidadeMinima = Convert.ToInt32(reader["quantidadeMinima"].ToString());

            if (reader["percentualDesconto"] != DBNull.Value)
                entidade.PercentualDesconto = Convert.ToDecimal(reader["percentualDesconto"].ToString());
        }

        public List<CompraConjunta> CarregarTodasCompraConjuntaExpiradaNaoFinalizada()
        {
            List<CompraConjunta> entidadesRetorno = new List<CompraConjunta>();

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            sbSQL.Append("SELECT * ");
            sbSQL.Append("FROM CompraConjunta ");
            sbSQL.Append("WHERE ativa = 1 ");
            sbSQL.Append("      AND dataFinalCompra < GETDATE() ");
            sbSQL.Append("      AND compraConjuntaStatusId = 1 ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                CompraConjunta entidadeRetorno = new CompraConjunta();
                PopulaCompraConjunta(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<CompraConjunta> CarregarCompraConjuntaParaFechamento()
        {
            List<CompraConjunta> entidadesRetorno = new List<CompraConjunta>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(@"SELECT
	                            DISTINCT
	                            CompraConjunta.*
                            FROM
	                            PedidoCompraConjunta
	                            INNER JOIN CompraConjunta ON CompraConjunta.compraConjuntaId = PedidoCompraConjunta.compraConjuntaId
                            WHERE
	                            CompraConjunta.dataFinalCompra <= GETDATE()
	                            AND CompraConjunta.ativa = 1
	                            AND PedidoCompraConjunta.fechamentoSincronizado = 0");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                CompraConjunta compraConjunta = new CompraConjunta();
                PopulaCompraConjunta(reader, compraConjunta);

                compraConjunta.CompraConjuntaDescontos = new List<CompraConjuntaDesconto>();

                entidadesRetorno.Add(compraConjunta);
            }

            reader.Close();

            return entidadesRetorno;
        }

        public bool CompraConjuntaComPedidoAberto(CompraConjunta compraConjuntaBO)
        {
            bool entidadeRetorno = false;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(@"SELECT
								COUNT(*) AS Total
							FROM 
								CompraConjunta
								INNER JOIN PedidoCompraConjunta ON PedidoCompraConjunta.compraConjuntaId = CompraConjunta.compraConjuntaId
                                INNER JOIN Pedido ON Pedido.pedidoId = PedidoCompraConjunta.pedidoCompraConjuntaId
							WHERE
								CompraConjunta.compraConjuntaId = @compraConjuntaId
                                AND Pedido.pedidoStatusId = 5");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@compraConjuntaId", DbType.Int32, compraConjuntaBO.CompraConjuntaId);
            IDataReader entidades = _db.ExecuteReader(command);

            if (entidades.Read())
            {
                if (entidades["Total"] != DBNull.Value)
                {
                    if (Convert.ToInt32(entidades["Total"].ToString()) > 0)
                    {
                        entidadeRetorno = true;
                    }
                }
            }

            entidades.Close();

            return entidadeRetorno;
        }

        #endregion

    }
}