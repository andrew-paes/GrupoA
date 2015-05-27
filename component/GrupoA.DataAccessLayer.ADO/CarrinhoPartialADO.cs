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
using GrupoA.BusinessObject.ViewHelper;

namespace GrupoA.DataAccess.ADO
{
    public partial class CarrinhoADO : ADOSuper, ICarrinhoDAL
    {
        /// <summary>
        /// Método que carrega um Carrinho.
        /// </summary>
        /// <param name="entidade">Carrinho a ser carregado (somente o identificador é necessário).</param>
        /// <returns>Carrinho</returns>
        public Carrinho CarregarAbertoPorUsuario(Usuario usuario, CarrinhoStatus carrinhoStatus)
        {

            Carrinho entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM Carrinho WHERE usuarioId=@usuarioId AND carrinhoStatusId=@carrinhoStatusId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@usuarioId", DbType.Int32, usuario.UsuarioId);
            _db.AddInParameter(command, "@carrinhoStatusId", DbType.Int32, carrinhoStatus.CarrinhoStatusId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Carrinho();
                PopulaCarrinho(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cepInicial"></param>
        /// <param name="cepFinal"></param>
        /// <param name="peso"></param>
        /// <returns></returns>
        public double CalculaFrete(int cepInicial, int cepFinal, decimal peso)
        {
            if (peso == 0)
            {
                return 0.0;
            }

            double freteValor = 0.0;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(@"SELECT
	                            *
	                            , (SELECT 
			                            TOP 1 PedidoFretePreco.preco
		                            FROM 
			                            PedidoFretePreco 
			                            INNER JOIN PedidoFreteGrupo PFG2 ON PFG2.pedidoFreteGrupoId = PedidoFretePreco.pedidoFreteGrupoId
		                            WHERE
			                            PFG2.pedidoFreteGrupoId = PFG1.pedidoFreteGrupoId
			                            AND PedidoFretePreco.peso > @peso) AS Preco1
	                            , (SELECT 
			                            TOP 1 PedidoFretePreco.preco
		                            FROM 
			                            PedidoFretePreco 
			                            INNER JOIN PedidoFreteGrupo PFG2 ON PFG2.pedidoFreteGrupoId = PedidoFretePreco.pedidoFreteGrupoId
		                            WHERE
			                            PFG2.pedidoFreteGrupoId = PFG1.pedidoFreteGrupoId
			                            AND PedidoFretePreco.peso < @peso
		                            ORDER BY
			                            PedidoFretePreco.peso DESC) AS Preco2
                            FROM 
	                            PedidoFreteGrupo PFG1
                            WHERE
	                            PFG1.PedidoFreteTipoId = 'B'
	                            AND @cepInicial BETWEEN PFG1.cepInicial1 AND PFG1.cepFinal1
	                            AND @cepFinal BETWEEN PFG1.cepInicial2 AND PFG1.cepFinal2");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@cepInicial", DbType.Int32, cepInicial);
            _db.AddInParameter(command, "@cepFinal", DbType.Int32, cepFinal);
            _db.AddInParameter(command, "@peso", DbType.Double, Convert.ToDouble(peso));

            DataTable dt = _db.ExecuteDataSet(command).Tables[0];

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row["Preco1"] != DBNull.Value && !String.IsNullOrEmpty(row["Preco1"].ToString()))
                    {
                        freteValor = Convert.ToDouble(row["Preco1"]);
                    }
                    else
                    {
                        if (row["Preco2"] != DBNull.Value && !String.IsNullOrEmpty(row["Preco2"].ToString()))
                        {
                            freteValor = Convert.ToDouble(row["Preco2"]);
                        }
                    }
                }
            }

            return freteValor;
        }

        public void AtualizarStatus(Carrinho carrinho)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE Carrinho SET ");
            sbSQL.Append(" carrinhoStatusId=@carrinhoStatusId ");
            sbSQL.Append(" WHERE carrinhoId=@carrinhoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@carrinhoId", DbType.Int32, carrinho.CarrinhoId);
            _db.AddInParameter(command, "@carrinhoStatusId", DbType.Int32, carrinho.CarrinhoStatus.CarrinhoStatusId);

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="carrinho"></param>
        /// <returns></returns>
        public List<CarrinhoItemVH> CarregarPorCarrinho(Carrinho carrinho)
        {
            List<CarrinhoItemVH> entidadeRetorno = new List<CarrinhoItemVH>();
            // Teste para retornar vazio
            // Somente é possível pesquisar quando há um código de carrinho 
            // ou carrinho com diversos itens
            if ((carrinho.CarrinhoId == 0) && ((carrinho.CarrinhoItens == null) || (carrinho.CarrinhoItens.Count == 0)))
            {
                return entidadeRetorno;
            }
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT  CASE WHEN TI.tituloImpressoId IS NOT NULL THEN TI.tituloImpressoId ");
            sbSQL.Append("        WHEN TE.tituloEletronicoId IS NOT NULL THEN TE.tituloEletronicoId ");
            sbSQL.Append("        WHEN CIT.capituloImpressoId IS NOT NULL ");
            sbSQL.Append("        THEN TI_CIT.tituloImpressoId ");
            sbSQL.Append("        WHEN CET.capituloEletronicoId IS NOT NULL ");
            sbSQL.Append("        THEN TE_CET.tituloEletronicoId ");
            sbSQL.Append("        WHEN TEA.tituloEletronicoAluguelId IS NOT NULL ");
            sbSQL.Append("        THEN TE_TEA.tituloEletronicoId ");
            sbSQL.Append("END produtoIdPai , ");
            sbSQL.Append("CASE WHEN TI.tituloImpressoId IS NOT NULL THEN PC_TI.categoriaId ");
            sbSQL.Append("        WHEN TE.tituloEletronicoId IS NOT NULL THEN PC_TE.categoriaId ");
            sbSQL.Append("        WHEN CIT.capituloImpressoId IS NOT NULL THEN PC_CIT.categoriaId ");
            sbSQL.Append("        WHEN CET.capituloEletronicoId IS NOT NULL THEN PC_CET.categoriaId ");
            sbSQL.Append("        WHEN TEA.tituloEletronicoAluguelId IS NOT NULL THEN PC_TEA.categoriaId ");
            sbSQL.Append("END categoriaIdPai , ");
            sbSQL.Append("dbo.AreaDeConhecimentoDaCategoria(CASE WHEN TI.tituloImpressoId IS NOT NULL ");
            sbSQL.Append("                                        THEN PC_TI.categoriaId ");
            sbSQL.Append("                                        WHEN TE.tituloEletronicoId IS NOT NULL ");
            sbSQL.Append("                                        THEN PC_TE.categoriaId ");
            sbSQL.Append("                                        WHEN CIT.capituloImpressoId IS NOT NULL ");
            sbSQL.Append("                                        THEN PC_CIT.categoriaId ");
            sbSQL.Append("                                        WHEN CET.capituloEletronicoId IS NOT NULL ");
            sbSQL.Append("                                        THEN PC_CET.categoriaId ");
            sbSQL.Append("                                        WHEN TEA.tituloEletronicoAluguelId IS NOT NULL ");
            sbSQL.Append("                                        THEN PC_TEA.categoriaId");
            sbSQL.Append("                                    END) AreaIdPai , ");
            sbSQL.Append("PT.produtoTipoId , ");
            sbSQL.Append("PT.tipo , ");
            sbSQL.Append("P.produtoId , ");
            sbSQL.Append("P.nomeProduto , ");
            sbSQL.Append("P.ValorUnitario , ");
            sbSQL.Append("P.ValorOferta , ");
            sbSQL.Append("P.ProdutoTipoId , ");
            sbSQL.Append("P.disponivel , ");
            sbSQL.Append("P.peso , ");
            sbSQL.Append("P.utilizaFrete , ");
            sbSQL.Append("P.exibirSite , ");
            sbSQL.Append("CICC.* , ");
            sbSQL.Append("CI.quantidade , ");
            sbSQL.Append("CI.carrinhoItemId , ");
            sbSQL.Append("A.*, ");
            sbSQL.Append("CASE WHEN p.valorOferta > 0 THEN p.valorOferta ");
            sbSQL.Append("        ELSE p.valorUnitario ");
            sbSQL.Append("END valor ");
            sbSQL.Append("FROM Carrinho C ");
            sbSQL.Append("INNER JOIN dbo.CarrinhoItem CI ON C.carrinhoId = CI.carrinhoId ");
            sbSQL.Append("INNER JOIN dbo.Produto P ON CI.produtoId = P.produtoId ");
            sbSQL.Append("INNER JOIN dbo.ProdutoTipo PT ON pt.produtoTipoId = p.produtoTipoId ");
            sbSQL.Append("LEFT JOIN dbo.CarrinhoItemCompraConjunta CICC ON CICC.carrinhoItemCompraConjuntaId = CI.carrinhoItemId ");
            sbSQL.Append("LEFT JOIN dbo.TituloImpresso TI ON TI.tituloImpressoId = P.produtoId ");
            sbSQL.Append("LEFT JOIN dbo.TituloEletronico TE ON TE.tituloEletronicoId = P.produtoId ");
            sbSQL.Append("LEFT JOIN dbo.CapituloImpresso CIT ON CIT.capituloImpressoId = P.produtoId ");
            sbSQL.Append("LEFT JOIN dbo.CapituloEletronico CET ON CET.capituloEletronicoId = P.produtoId ");
            sbSQL.Append("LEFT JOIN dbo.TituloEletronicoAluguel TEA ON TEA.tituloEletronicoAluguelId = P.produtoId ");
            sbSQL.Append("LEFT JOIN dbo.ProdutoCategoria PC_TI ON PC_TI.produtoId = TI.tituloImpressoId ");
            sbSQL.Append("LEFT JOIN dbo.ProdutoCategoria PC_TE ON PC_TE.produtoId = TE.tituloEletronicoId ");
            sbSQL.Append("LEFT JOIN dbo.ProdutoCategoria PC_CIT ON PC_CIT.produtoId = CIT.capituloImpressoId ");
            sbSQL.Append("LEFT JOIN dbo.ProdutoCategoria PC_CET ON PC_CET.produtoId = CET.capituloEletronicoId ");
            sbSQL.Append("LEFT JOIN dbo.ProdutoCategoria PC_TEA ON PC_TEA.produtoId = TEA.tituloEletronicoAluguelId ");
            sbSQL.Append("LEFT JOIN dbo.TituloImpresso TI_CIT ON TI_CIT.tituloImpressoId = CIT.tituloImpressoId ");
            sbSQL.Append("LEFT JOIN dbo.TituloEletronico TE_CET ON TE_CET.tituloEletronicoId = CET.tituloEletronicoId ");
            sbSQL.Append("LEFT JOIN dbo.TituloEletronico TE_TEA ON TE_TEA.tituloEletronicoId = TEA.tituloEletronicoId ");
            sbSQL.Append("LEFT JOIN dbo.ProdutoImagem PIMG ON PIMG.produtoId = P.produtoId AND PIMG.produtoImagemTipoId = 1 ");
            sbSQL.Append("LEFT JOIN dbo.Arquivo A ON A.arquivoId = PIMG.arquivoId ");
            sbSQL.Append("WHERE P.disponivel = 1 AND P.homologado = 1 AND C.carrinhoId = @carrinhoid ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@carrinhoid", DbType.Int32, carrinho.CarrinhoId);

            IDataReader reader = _db.ExecuteReader(command);
            while (reader.Read())
            {
                CarrinhoItemVH carrinhoItem = new CarrinhoItemVH();
                this.PopulaItemCarrinho(reader, carrinhoItem);
                if (carrinho.CarrinhoItens != null)
                {
                    bool eCompraConjunta = (carrinhoItem.CarrinhoItemCompraConjuntaId > 0 ? true : false);
                    foreach (CarrinhoItem _carrinhoItem in carrinho.CarrinhoItens)
                    {
                        // Do item do foreach 
                        // (sendo populado)
                        if ((_carrinhoItem.Produto.ProdutoId == carrinhoItem.ProdutoId) &&
                            ((!eCompraConjunta && _carrinhoItem.CarrinhoItemCompraConjunta == null) ||
                                (eCompraConjunta && _carrinhoItem.CarrinhoItemCompraConjunta != null)))
                        {
                            carrinhoItem.Quantidade = (decimal)_carrinhoItem.Quantidade;

                            if (carrinhoItem.CarrinhoItemCompraConjuntaId > 0)
                            {
                                _carrinhoItem.CarrinhoItemCompraConjunta = new CarrinhoItemCompraConjunta();
                                _carrinhoItem.CarrinhoItemCompraConjunta.CarrinhoItemCompraConjuntaId = carrinhoItem.CarrinhoItemCompraConjuntaId;
                            }

                            break;
                        }

                        if (_carrinhoItem.CarrinhoItemId == carrinhoItem.CarrinhoItemId)
                        {
                            if (carrinhoItem.CarrinhoItemCompraConjuntaId > 0)
                            {
                                _carrinhoItem.CarrinhoItemCompraConjunta = new CarrinhoItemCompraConjunta();
                                _carrinhoItem.CarrinhoItemCompraConjunta.CarrinhoItemCompraConjuntaId = carrinhoItem.CarrinhoItemCompraConjuntaId;
                            }
                        }
                    }

                }
                entidadeRetorno.Add(carrinhoItem);
            }
            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="carrinho"></param>
        /// <returns></returns>
        public List<CarrinhoItemVH> CarregarPorProduto(Carrinho carrinho)
        {
            List<CarrinhoItemVH> entidadeRetorno = new List<CarrinhoItemVH>();
            // Teste para retornar vazio
            // Somente é possível pesquisar quando há um código de carrinho 
            // ou carrinho com diversos itens
            if ((carrinho.CarrinhoId == 0) && ((carrinho.CarrinhoItens == null) || (carrinho.CarrinhoItens.Count == 0)))
            {
                return entidadeRetorno;
            }

            StringBuilder sbProduto = new StringBuilder();
            StringBuilder sbProdutoCompraConjunta = new StringBuilder();

            foreach (CarrinhoItem carrinhoItem in carrinho.CarrinhoItens)
            {
                if (carrinhoItem.CarrinhoItemCompraConjunta == null)
                {
                    sbProduto.Append(string.Concat((sbProduto.ToString().Length > 0 ? "," : string.Empty), carrinhoItem.Produto.ProdutoId.ToString()));
                }
                else
                {
                    sbProdutoCompraConjunta.Append(string.Concat((sbProdutoCompraConjunta.ToString().Length > 0 ? "," : string.Empty), carrinhoItem.Produto.ProdutoId.ToString()));
                }
            }

            StringBuilder sbSQL = new StringBuilder();

            if (sbProduto.Length > 0)
            {
                sbSQL.Append("SELECT  CASE WHEN TI.tituloImpressoId IS NOT NULL THEN TI.tituloImpressoId");
                sbSQL.Append("             WHEN TE.tituloEletronicoId IS NOT NULL THEN TE.tituloEletronicoId");
                sbSQL.Append("             WHEN CIT.capituloImpressoId IS NOT NULL");
                sbSQL.Append("             THEN TI_CIT.tituloImpressoId");
                sbSQL.Append("             WHEN CET.capituloEletronicoId IS NOT NULL");
                sbSQL.Append("             THEN TE_CET.tituloEletronicoId");
                sbSQL.Append("             WHEN TEA.tituloEletronicoAluguelId IS NOT NULL ");
                sbSQL.Append("             THEN TE_TEA.tituloEletronicoId ");
                sbSQL.Append("        END produtoIdPai ,");
                sbSQL.Append("        CASE WHEN TI.tituloImpressoId IS NOT NULL THEN PC_TI.categoriaId");
                sbSQL.Append("             WHEN TE.tituloEletronicoId IS NOT NULL THEN PC_TE.categoriaId");
                sbSQL.Append("             WHEN CIT.capituloImpressoId IS NOT NULL THEN PC_CIT.categoriaId");
                sbSQL.Append("             WHEN CET.capituloEletronicoId IS NOT NULL THEN PC_CET.categoriaId");
                sbSQL.Append("             WHEN TEA.tituloEletronicoAluguelId IS NOT NULL THEN PC_TEA.categoriaId ");
                sbSQL.Append("        END categoriaIdPai ,");
                sbSQL.Append("        dbo.AreaDeConhecimentoDaCategoria(CASE WHEN TI.tituloImpressoId IS NOT NULL");
                sbSQL.Append("                                               THEN PC_TI.categoriaId");
                sbSQL.Append("                                               WHEN TE.tituloEletronicoId IS NOT NULL");
                sbSQL.Append("                                               THEN PC_TE.categoriaId");
                sbSQL.Append("                                               WHEN CIT.capituloImpressoId IS NOT NULL");
                sbSQL.Append("                                               THEN PC_CIT.categoriaId");
                sbSQL.Append("                                               WHEN CET.capituloEletronicoId IS NOT NULL");
                sbSQL.Append("                                               THEN PC_CET.categoriaId");
                sbSQL.Append("                                               WHEN TEA.tituloEletronicoAluguelId IS NOT NULL ");
                sbSQL.Append("                                               THEN PC_TEA.categoriaId");
                sbSQL.Append("                                          END) AreaIdPai ,");
                sbSQL.Append("        PT.produtoTipoId ,");
                sbSQL.Append("        PT.tipo ,");
                sbSQL.Append("        P.produtoId ,");
                sbSQL.Append("        P.nomeProduto ,");
                sbSQL.Append("        P.ValorUnitario ,");
                sbSQL.Append("        P.ValorOferta ,");
                sbSQL.Append("        P.ProdutoTipoId ,");
                sbSQL.Append("        P.disponivel ,");
                sbSQL.Append("        P.peso ,");
                sbSQL.Append("        P.utilizaFrete ,");
                sbSQL.Append("        P.exibirSite ,");
                sbSQL.Append("        TI.* ,");
                sbSQL.Append("        TE.* ,");
                sbSQL.Append("        A.* ,");
                sbSQL.Append("        CASE WHEN p.valorOferta > 0 THEN p.valorOferta");
                sbSQL.Append("             ELSE p.valorUnitario");
                sbSQL.Append("        END valor ,");
                sbSQL.Append("        NULL AS compraConjuntaId ");
                sbSQL.Append("FROM    dbo.Produto P");
                sbSQL.Append("        INNER JOIN dbo.ProdutoTipo PT ON pt.produtoTipoId = p.produtoTipoId");
                sbSQL.Append("        LEFT JOIN dbo.TituloImpresso TI ON TI.tituloImpressoId = P.produtoId");
                sbSQL.Append("        LEFT JOIN dbo.TituloEletronico TE ON TE.tituloEletronicoId = P.produtoId");
                sbSQL.Append("        LEFT JOIN dbo.CapituloImpresso CIT ON CIT.capituloImpressoId = P.produtoId");
                sbSQL.Append("        LEFT JOIN dbo.CapituloEletronico CET ON CET.capituloEletronicoId = P.produtoId");
                sbSQL.Append("        LEFT JOIN dbo.TituloEletronicoAluguel TEA ON TEA.tituloEletronicoAluguelId = P.produtoId ");
                sbSQL.Append("        LEFT JOIN dbo.ProdutoCategoria PC_TI ON PC_TI.produtoId = TI.tituloImpressoId");
                sbSQL.Append("        LEFT JOIN dbo.ProdutoCategoria PC_TE ON PC_TE.produtoId = TE.tituloEletronicoId");
                sbSQL.Append("        LEFT JOIN dbo.ProdutoCategoria PC_CIT ON PC_CIT.produtoId = CIT.capituloImpressoId");
                sbSQL.Append("        LEFT JOIN dbo.ProdutoCategoria PC_CET ON PC_CET.produtoId = CET.capituloEletronicoId");
                sbSQL.Append("        LEFT JOIN dbo.ProdutoCategoria PC_TEA ON PC_TEA.produtoId = TEA.tituloEletronicoAluguelId ");
                sbSQL.Append("        LEFT JOIN dbo.TituloImpresso TI_CIT ON TI_CIT.tituloImpressoId = CIT.tituloImpressoId");
                sbSQL.Append("        LEFT JOIN dbo.TituloEletronico TE_CET ON TE_CET.tituloEletronicoId = CET.tituloEletronicoId");
                sbSQL.Append("        LEFT JOIN dbo.TituloEletronico TE_TEA ON TE_TEA.tituloEletronicoId = TEA.tituloEletronicoId ");
                sbSQL.Append("        LEFT JOIN dbo.ProdutoImagem PIMG ON PIMG.produtoId = P.produtoId");
                sbSQL.Append("                                            AND PIMG.produtoImagemTipoId = 1");
                sbSQL.Append("        LEFT JOIN dbo.Arquivo A ON A.arquivoId = PIMG.arquivoId ");
                sbSQL.Append("WHERE   P.produtoId IN ( " + sbProduto.ToString() + " )");
            }

            if (sbProdutoCompraConjunta.Length > 0)
            {
                if (sbProduto.Length > 0)
                {
                    sbSQL.Append(" UNION ALL ");
                }

                sbSQL.Append("SELECT  CASE WHEN TI.tituloImpressoId IS NOT NULL THEN TI.tituloImpressoId");
                sbSQL.Append("             WHEN TE.tituloEletronicoId IS NOT NULL THEN TE.tituloEletronicoId");
                sbSQL.Append("             WHEN CIT.capituloImpressoId IS NOT NULL");
                sbSQL.Append("             THEN TI_CIT.tituloImpressoId");
                sbSQL.Append("             WHEN CET.capituloEletronicoId IS NOT NULL");
                sbSQL.Append("             THEN TE_CET.tituloEletronicoId");
                sbSQL.Append("             WHEN TEA.tituloEletronicoAluguelId IS NOT NULL ");
                sbSQL.Append("             THEN TE_TEA.tituloEletronicoId ");
                sbSQL.Append("        END produtoIdPai ,");
                sbSQL.Append("        CASE WHEN TI.tituloImpressoId IS NOT NULL THEN PC_TI.categoriaId");
                sbSQL.Append("             WHEN TE.tituloEletronicoId IS NOT NULL THEN PC_TE.categoriaId");
                sbSQL.Append("             WHEN CIT.capituloImpressoId IS NOT NULL THEN PC_CIT.categoriaId");
                sbSQL.Append("             WHEN CET.capituloEletronicoId IS NOT NULL THEN PC_CET.categoriaId");
                sbSQL.Append("             WHEN TEA.tituloEletronicoAluguelId IS NOT NULL THEN PC_TEA.categoriaId ");
                sbSQL.Append("        END categoriaIdPai ,");
                sbSQL.Append("        dbo.AreaDeConhecimentoDaCategoria(CASE WHEN TI.tituloImpressoId IS NOT NULL");
                sbSQL.Append("                                               THEN PC_TI.categoriaId");
                sbSQL.Append("                                               WHEN TE.tituloEletronicoId IS NOT NULL");
                sbSQL.Append("                                               THEN PC_TE.categoriaId");
                sbSQL.Append("                                               WHEN CIT.capituloImpressoId IS NOT NULL");
                sbSQL.Append("                                               THEN PC_CIT.categoriaId");
                sbSQL.Append("                                               WHEN CET.capituloEletronicoId IS NOT NULL");
                sbSQL.Append("                                               THEN PC_CET.categoriaId");
                sbSQL.Append("                                               WHEN TEA.tituloEletronicoAluguelId IS NOT NULL ");
                sbSQL.Append("                                               THEN PC_TEA.categoriaId");
                sbSQL.Append("                                          END) AreaIdPai ,");
                sbSQL.Append("        PT.produtoTipoId ,");
                sbSQL.Append("        PT.tipo ,");
                sbSQL.Append("        P.produtoId ,");
                sbSQL.Append("        P.nomeProduto ,");
                sbSQL.Append("        P.ValorUnitario ,");
                sbSQL.Append("        P.ValorOferta ,");
                sbSQL.Append("        P.ProdutoTipoId ,");
                sbSQL.Append("        P.disponivel ,");
                sbSQL.Append("        P.peso ,");
                sbSQL.Append("        P.utilizaFrete ,");
                sbSQL.Append("        P.exibirSite ,");
                sbSQL.Append("        TI.* ,");
                sbSQL.Append("        TE.* ,");
                sbSQL.Append("        A.* ,");
                sbSQL.Append("        CASE WHEN p.valorOferta > 0 THEN p.valorOferta");
                sbSQL.Append("             ELSE p.valorUnitario");
                sbSQL.Append("        END valor ,");
                sbSQL.Append("        CC.compraConjuntaId ");
                sbSQL.Append("FROM    dbo.Produto P");
                sbSQL.Append("        INNER JOIN dbo.ProdutoTipo PT ON pt.produtoTipoId = p.produtoTipoId");
                sbSQL.Append("        INNER JOIN dbo.CompraConjunta CC ON CC.produtoId = P.produtoId");
                sbSQL.Append("                                            AND CC.ativa = 1");
                sbSQL.Append("                                            AND CC.compraConjuntaStatusId = 1");
                sbSQL.Append("                                            AND GETDATE() BETWEEN dataInicialCompra");
                sbSQL.Append("                                                          AND dataFinalCompra");
                sbSQL.Append("        LEFT JOIN dbo.TituloImpresso TI ON TI.tituloImpressoId = P.produtoId");
                sbSQL.Append("        LEFT JOIN dbo.TituloEletronico TE ON TE.tituloEletronicoId = P.produtoId");
                sbSQL.Append("        LEFT JOIN dbo.CapituloImpresso CIT ON CIT.capituloImpressoId = P.produtoId");
                sbSQL.Append("        LEFT JOIN dbo.CapituloEletronico CET ON CET.capituloEletronicoId = P.produtoId");
                sbSQL.Append("        LEFT JOIN dbo.TituloEletronicoAluguel TEA ON TEA.tituloEletronicoAluguelId = P.produtoId ");
                sbSQL.Append("        LEFT JOIN dbo.ProdutoCategoria PC_TI ON PC_TI.produtoId = TI.tituloImpressoId");
                sbSQL.Append("        LEFT JOIN dbo.ProdutoCategoria PC_TE ON PC_TE.produtoId = TE.tituloEletronicoId");
                sbSQL.Append("        LEFT JOIN dbo.ProdutoCategoria PC_CIT ON PC_CIT.produtoId = CIT.capituloImpressoId");
                sbSQL.Append("        LEFT JOIN dbo.ProdutoCategoria PC_CET ON PC_CET.produtoId = CET.capituloEletronicoId");
                sbSQL.Append("        LEFT JOIN dbo.ProdutoCategoria PC_TEA ON PC_TEA.produtoId = TEA.tituloEletronicoAluguelId ");
                sbSQL.Append("        LEFT JOIN dbo.TituloImpresso TI_CIT ON TI_CIT.tituloImpressoId = TI.tituloImpressoId");
                sbSQL.Append("        LEFT JOIN dbo.TituloEletronico TE_CET ON TE_CET.tituloEletronicoId = TE.tituloEletronicoId");
                sbSQL.Append("        LEFT JOIN dbo.TituloEletronico TE_TEA ON TE_TEA.tituloEletronicoId = TEA.tituloEletronicoId ");
                sbSQL.Append("        LEFT JOIN dbo.ProdutoImagem PIMG ON PIMG.produtoId = P.produtoId");
                sbSQL.Append("                                            AND PIMG.produtoImagemTipoId = 1");
                sbSQL.Append("        LEFT JOIN dbo.Arquivo A ON A.arquivoId = PIMG.arquivoId ");
                sbSQL.Append("WHERE   P.produtoId IN (" + sbProdutoCompraConjunta.ToString() + " ) ");
            }

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@carrinhoid", DbType.Int32, carrinho.CarrinhoId);

            IDataReader reader = _db.ExecuteReader(command);
            while (reader.Read())
            {
                CarrinhoItemVH carrinhoItem = new CarrinhoItemVH();
                this.PopulaItemCarrinhoPorProduto(reader, carrinhoItem);
                if (carrinho.CarrinhoItens != null)
                {
                    bool eCompraConjunta = (carrinhoItem.CompraConjuntaId > 0 ? true : false);
                    foreach (CarrinhoItem _carrinhoItem in carrinho.CarrinhoItens)
                    {
                        // Do item do foreach 
                        // (sendo populado)
                        if ((_carrinhoItem.Produto.ProdutoId == carrinhoItem.ProdutoId) &&
                            ((!eCompraConjunta && _carrinhoItem.CarrinhoItemCompraConjunta == null) ||
                                (eCompraConjunta && _carrinhoItem.CarrinhoItemCompraConjunta != null)))
                        {
                            carrinhoItem.Quantidade = (decimal)_carrinhoItem.Quantidade;
                            break;
                        }
                    }

                    if (eCompraConjunta)
                    {
                        CompraConjuntaDesconto compraConjuntaDesconto = new CompraConjuntaADO().CarregarCompraConjuntaDesconto(carrinhoItem.CompraConjuntaId);

                        if (compraConjuntaDesconto != null)
                        {
                            carrinhoItem.Valor = carrinhoItem.Valor * (1 - (compraConjuntaDesconto.PercentualDesconto / 100));
                            carrinhoItem.ValorOferta = carrinhoItem.Valor;
                        }
                    }

                }
                entidadeRetorno.Add(carrinhoItem);
            }
            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entidade"></param>
        private void PopulaItemCarrinho(IDataReader reader, CarrinhoItemVH entidade)
        {
            if (reader["produtoId"] != DBNull.Value)
                entidade.ProdutoId = Convert.ToInt32(reader["produtoId"].ToString());

            if (reader["carrinhoItemId"] != DBNull.Value)
                entidade.CarrinhoItemId = Convert.ToInt32(reader["carrinhoItemId"].ToString());

            if (reader["CarrinhoItemCompraConjuntaId"] != DBNull.Value)
                entidade.CarrinhoItemCompraConjuntaId = Convert.ToInt32(reader["CarrinhoItemCompraConjuntaId"].ToString());

            if (reader["CompraConjuntaId"] != DBNull.Value)
                entidade.CompraConjuntaId = Convert.ToInt32(reader["CompraConjuntaId"].ToString());

            if (reader["AreaIdPai"] != DBNull.Value)
                entidade.AreaId = Convert.ToInt32(reader["AreaIdPai"].ToString());

            if (reader["CategoriaIdPai"] != DBNull.Value)
                entidade.CategoriaId = Convert.ToInt32(reader["CategoriaIdPai"].ToString());

            if (reader["NomeProduto"] != DBNull.Value)
                entidade.NomeProduto = reader["NomeProduto"].ToString();

            if (reader["Tipo"] != DBNull.Value)
                entidade.Tipo = reader["Tipo"].ToString();

            if (reader["ValorUnitario"] != DBNull.Value)
                entidade.ValorUnitario = Convert.ToDecimal(reader["ValorUnitario"].ToString());

            if (reader["ValorOferta"] != DBNull.Value)
                entidade.ValorOferta = Convert.ToDecimal(reader["ValorOferta"].ToString());

            if (reader["Valor"] != DBNull.Value)
                entidade.Valor = Convert.ToDecimal(reader["Valor"].ToString());

            if (reader["NomeArquivo"] != DBNull.Value)
                entidade.NomeArquivo = reader["NomeArquivo"].ToString();

            if (reader["exibirSite"] != DBNull.Value)
                entidade.ExibirSite = Boolean.Parse(reader["exibirSite"].ToString());

            if (reader["disponivel"] != DBNull.Value)
                entidade.Disponivel = Boolean.Parse(reader["disponivel"].ToString());

            if (reader["quantidade"] != DBNull.Value)
                entidade.Quantidade = Convert.ToDecimal(reader["quantidade"].ToString());

            if (reader["Peso"] != DBNull.Value)
                entidade.Peso = Convert.ToDecimal(reader["Peso"].ToString());

            if (reader["utilizaFrete"] != DBNull.Value)
                entidade.UtilizaFrete = Boolean.Parse(reader["utilizaFrete"].ToString());

            if (reader["produtoIdPai"] != DBNull.Value)
                entidade.ProdutoIdPai = Convert.ToInt32(reader["produtoIdPai"].ToString());

            if (reader["produtoTipoId"] != DBNull.Value)
                entidade.ProdutoTipoId = Convert.ToInt32(reader["produtoTipoId"].ToString());

            switch (entidade.ProdutoTipoId)
            {
                case 1:
                case 3:
                    // Título Impresso ou Capítulo Impresso
                    TituloImpresso tituloImpresso = new TituloImpressoADO().CarregarPorProduto(entidade.ProdutoIdPai);
                    entidade.TituloId = tituloImpresso.Titulo.TituloId;
                    break;
                case 2:
                case 4:
                case 6:
                    // Título Eletrônico ou Capítulo Eletrônico ou Título Eletrônico Aluguel
                    TituloEletronico tituloEletronico = new TituloEletronicoADO().CarregarPorProduto(entidade.ProdutoIdPai);
                    entidade.TituloId = tituloEletronico.Titulo.TituloId;
                    break;
                case 5:
                    // Revista
                    break;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entidade"></param>
        private void PopulaItemCarrinhoPorProduto(IDataReader reader, CarrinhoItemVH entidade)
        {
            if (reader["produtoId"] != DBNull.Value)
                entidade.ProdutoId = Convert.ToInt32(reader["produtoId"].ToString());

            if (reader["CompraConjuntaId"] != DBNull.Value)
                entidade.CompraConjuntaId = Convert.ToInt32(reader["CompraConjuntaId"].ToString());

            if (reader["AreaIdPai"] != DBNull.Value)
                entidade.AreaId = Convert.ToInt32(reader["AreaIdPai"].ToString());

            if (reader["CategoriaIdPai"] != DBNull.Value)
                entidade.CategoriaId = Convert.ToInt32(reader["CategoriaIdPai"].ToString());

            if (reader["NomeProduto"] != DBNull.Value)
                entidade.NomeProduto = reader["NomeProduto"].ToString();

            if (reader["Tipo"] != DBNull.Value)
                entidade.Tipo = reader["Tipo"].ToString();

            if (reader["ValorUnitario"] != DBNull.Value)
                entidade.ValorUnitario = Convert.ToDecimal(reader["ValorUnitario"].ToString());

            if (reader["ValorOferta"] != DBNull.Value)
                entidade.ValorOferta = Convert.ToDecimal(reader["ValorOferta"].ToString());

            if (reader["Valor"] != DBNull.Value)
                entidade.Valor = Convert.ToDecimal(reader["Valor"].ToString());

            if (reader["NomeArquivo"] != DBNull.Value)
                entidade.NomeArquivo = reader["NomeArquivo"].ToString();

            if (reader["exibirSite"] != DBNull.Value)
                entidade.ExibirSite = Boolean.Parse(reader["exibirSite"].ToString());

            if (reader["disponivel"] != DBNull.Value)
                entidade.Disponivel = Boolean.Parse(reader["disponivel"].ToString());

            if (reader["Peso"] != DBNull.Value)
                entidade.Peso = Convert.ToDecimal(reader["Peso"].ToString()); ;

            if (reader["utilizaFrete"] != DBNull.Value)
                entidade.UtilizaFrete = Boolean.Parse(reader["utilizaFrete"].ToString());

            if (reader["produtoIdPai"] != DBNull.Value)
                entidade.ProdutoIdPai = Convert.ToInt32(reader["produtoIdPai"].ToString());

            if (reader["produtoTipoId"] != DBNull.Value)
                entidade.ProdutoTipoId = Convert.ToInt32(reader["produtoTipoId"].ToString());

            switch (entidade.ProdutoTipoId)
            {
                case 1:
                case 3:
                    // Título Impresso ou Capítulo Impresso
                    TituloImpresso tituloImpresso = new TituloImpressoADO().CarregarPorProduto(entidade.ProdutoIdPai);
                    entidade.TituloId = tituloImpresso.Titulo.TituloId;
                    break;
                case 2:
                case 4:
                case 6:
                    // Título Eletrônico ou Capítulo Eletrônico ou Título Eletrônico Aluguel
                    TituloEletronico tituloEletronico = new TituloEletronicoADO().CarregarPorProduto(entidade.ProdutoIdPai);
                    entidade.TituloId = tituloEletronico.Titulo.TituloId;
                    break;
                case 5:
                    // Revista
                    break;
                default:
                    break;
            }
        }
    }
}