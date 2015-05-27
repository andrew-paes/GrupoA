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
using GrupoA.BusinessObject.Enumerator;

namespace GrupoA.DataAccess.ADO
{
    public partial class ProdutoADO : ADOSuper, IProdutoDAL
    {
        /// <summary>
        /// Método que carrega uma coleção de produtos conforme o código EAN13 recebido excluindo a coleção de produtos recebida como produto.
        /// </summary>
        /// <param name="produto">Produto que contém o código EAN13 desejado.</param>
        /// <param name="produtos">Coleção de produtos que não deve ser retornada.</param>
        /// <returns>Coleção de produtos conforme o código EAN13 recebido.</returns>
        public List<Produto> CarregarTodosPorIsbn13ExcetoProdutos(string isbn13, List<Produto> produtos)
        {
            List<Produto> entidadeRetorno = new List<Produto>();
            String strIds = "0";

            foreach (Produto _produto in produtos)
            {
                strIds += string.Concat(",", _produto.ProdutoId);
            }

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(@"SELECT
	                            *
                            FROM
	                            (
	                            SELECT
		                            Produto.*
		                            , TabelaTitulo.isbn13 AS ISBN13
	                            FROM
		                            (
		                            SELECT
			                            TituloImpresso.isbn13 AS isbn13
			                            , TituloImpresso.tituloImpressoId AS produtoId
		                            FROM
			                            TituloImpresso
		                            UNION
		                            SELECT
			                            TituloEletronico.isbn13 AS isbn13
			                            , TituloEletronico.tituloEletronicoId AS produtoId
		                            FROM
			                            TituloEletronico
		                            ) AS TabelaTitulo
		                            INNER JOIN Produto ON Produto.produtoId = TabelaTitulo.produtoId
		                        WHERE
			                            Produto.produtoId != 0 ");
            sbSQL.Append(string.Format("AND Produto.produtoId NOT IN ({0}) ", strIds));
			sbSQL.Append(string.Format("AND TabelaTitulo.isbn13 LIKE '%{0}%' ", isbn13));
            sbSQL.Append(@"  UNION
	                            SELECT
		                            Produto.*
		                            , Produto.codigoProduto AS ISBN13
	                            FROM
		                            RevistaAssinatura
		                            INNER JOIN Produto ON Produto.produtoId = RevistaAssinatura.revistaAssinaturaId
		                        WHERE
			                            Produto.produtoId != 0 ");
            sbSQL.Append(string.Format("AND Produto.produtoId NOT IN ({0}) ", strIds));
            sbSQL.Append(string.Format("AND Produto.codigoProduto LIKE '%{0}%' ", isbn13));
            sbSQL.Append(@") AS TabelaDerivada
                             ORDER BY
	                            TabelaDerivada.nomeProduto ASC");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            //_db.AddInParameter(command, "@Ids", DbType.Int64, ids);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Produto _produto = new Produto();
                PopulaProduto(reader, _produto);
                entidadeRetorno.Add(_produto);
            }

            reader.Close();

            return entidadeRetorno;
        }

        #region public List<Produto> CarregarTodosPorEANExcetoProdutos(Produto produto, List<Produto> produtos)
        /// <summary>
        /// Método que carrega uma coleção de produtos conforme o código EAN13 recebido excluindo a coleção de produtos recebida como produto.
        /// </summary>
        /// <param name="produto">Produto que contém o código EAN13 desejado.</param>
        /// <param name="produtos">Coleção de produtos que não deve ser retornada.</param>
        /// <returns>Coleção de produtos conforme o código EAN13 recebido.</returns>
        public List<Produto> CarregarTodosPorEANExcetoProdutos(Produto produto, List<Produto> produtos)
        {

            List<Produto> entidadeRetorno = new List<Produto>();
            String ids = "";
            foreach (Produto _produto in produtos)
            {
                ids += string.Concat(",", _produto.ProdutoId);
            }

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(string.Concat("SELECT * FROM Produto WHERE produtoId NOT IN (0", ids, ") AND CodigoEAN13 like '%", produto.CodigoEAN13, "%'"));

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            //_db.AddInParameter(command, "@Ids", DbType.Int64, ids);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Produto _produto = new Produto();
                PopulaProduto(reader, _produto);
                entidadeRetorno.Add(_produto);
            }
            reader.Close();

            return entidadeRetorno;
        }
        #endregion

        #region public List<Produto> CarregarPorPromocao(Promocao promocao)
        /// <summary>
        /// Carrega todos os Produtos de uma Promoção conforme o código identificador da Promoção recebido.
        /// </summary>
        /// <param name="promocao">Objeto Promocao que contém o código identificador da Promoção (promocaoId)</param>
        /// <returns>Coleção de Produtos da Promoção</returns>
        public List<Produto> CarregarPorPromocao(Promocao promocao)
        {
            List<Produto> entidadeRetorno = new List<Produto>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(" SELECT p.* FROM PromocaoProduto pp ");
            sbSQL.Append(" INNER JOIN Produto p ON p.produtoId = pp.produtoId ");
            sbSQL.Append(" WHERE pp.promocaoId = @promocaoId ");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@promocaoId", DbType.Int32, promocao.PromocaoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Produto _produto = new Produto();
                PopulaProduto(reader, _produto);
                entidadeRetorno.Add(_produto);
            }
            reader.Close();

            return entidadeRetorno;
        }
        #endregion

        public List<Produto> CarregarPorRevistaArtigo(RevistaArtigo revistaArtigo)
        {
            List<Produto> entidadeRetorno = new List<Produto>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(" SELECT Produto.* FROM RevistaArtigoProduto");
            sbSQL.Append(" INNER JOIN Produto ON Produto.produtoId = RevistaArtigoProduto.produtoId ");
            sbSQL.Append(" WHERE RevistaArtigoProduto.revistaArtigoId = @revistaArtigoId ");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaArtigoId", DbType.Int32, revistaArtigo.RevistaArtigoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Produto _produto = new Produto();
                PopulaProduto(reader, _produto);
                entidadeRetorno.Add(_produto);
            }
            reader.Close();

            return entidadeRetorno;
        }

        #region Listagem de Produtos
        /// <summary>
        /// Carrega todos os Produtos de uma Categoria
        /// </summary>
        /// <param name="promocao">Objeto Promocao que contém o código identificador da Promoção (promocaoId)</param>
        /// <returns>Coleção de Produtos da Promoção</returns>
        public List<ProdutoListaVH> CarregarPorCategoria(Categoria entidade)
        {
            List<ProdutoListaVH> entidadeRetorno = new List<ProdutoListaVH>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(@"WITH Categorias (categoriaId, nomeCategoria, categoriaIdPai, Nivel)
                            AS
                            ( 
                                SELECT C.categoriaId, C.nomeCategoria, C.categoriaIdPai, 0 AS Nivel
                                FROM Categoria AS C
                                WHERE C.categoriaId = @CategoriaId
                                UNION ALL
                                SELECT C.categoriaId, C.nomeCategoria, C.categoriaIdPai, Nivel+1
                                FROM Categoria AS C
                                INNER JOIN Categorias AS CS
                                    ON c.CategoriaIdPai = CS.categoriaId
                            )
                            SELECT 
	                            P.ProdutoId,
	                            T.TituloId,
	                            P.NomeProduto,
	                            P.homologado,
	                            'Livro Impresso' Tipo,
	                            P.ValorUnitario,
	                            P.ValorOferta,
	                            T.DataLancamento
	                            ,A.NomeArquivo
	                            ,CASE WHEN p.valorOferta>0 THEN p.valorOferta ELSE p.valorUnitario END valor
	                            ,CASE WHEN p.valorOferta IS NOT NULL THEN ( SELECT ISNULL(MAX(MPF.numeroParcelas),0) FROM dbo.MeioPagamentoFaixa MPF INNER JOIN MeioPagamento MP ON MP.meioPagamentoId = MPF.meioPagamentoId WHERE MP.ativo = 1 AND valorMinimo <= P.valorOferta)
		                            ELSE ( SELECT ISNULL(MAX(MPF.numeroParcelas),0) FROM dbo.MeioPagamentoFaixa MPF INNER JOIN MeioPagamento MP ON MP.meioPagamentoId = MPF.meioPagamentoId WHERE MP.ativo = 1 AND valorMinimo <= P.valorUnitario)
		                            END Parcelas
	                            , (SELECT ISNULL(MAX(taxaJuros),0) FROM dbo.MeioPagamentoFaixa) taxaJuros
	                            ,P.disponivel
                            FROM Produto P 
                            INNER JOIN ProdutoCategoria PC ON PC.produtoId = P.produtoId
                            INNER JOIN Categorias C ON c.categoriaId = pc.categoriaId
                            INNER JOIN TituloImpresso TI ON p.produtoId = ti.tituloImpressoId
                            INNER JOIN Titulo T ON t.tituloId = ti.tituloId
                            LEFT JOIN dbo.ProdutoImagem TIMG ON TIMG.produtoId = P.produtoId AND TIMG.produtoImagemTipoId = 1
                            LEFT JOIN Arquivo A ON A.ArquivoId = TIMG.ArquivoId
                            WHERE P.exibirSite = 1 AND P.homologado = 1 AND P.produtoTipoId=1
                            UNION ALL
                            SELECT 
	                            P.ProdutoId,
	                            TI.TituloId,
	                            P.NomeProduto,
	                            P.homologado,
	                            'eBook' Tipo,
	                            P.ValorUnitario,
	                            P.ValorOferta,
	                            T.DataLancamento
	                            ,A.NomeArquivo 
	                            ,CASE WHEN p.valorOferta>0 THEN p.valorOferta ELSE p.valorUnitario END valor
	                            ,CASE WHEN p.valorOferta IS NOT NULL THEN ( SELECT ISNULL(MAX(MPF.numeroParcelas),0) FROM dbo.MeioPagamentoFaixa MPF INNER JOIN MeioPagamento MP ON MP.meioPagamentoId = MPF.meioPagamentoId WHERE MP.ativo = 1 AND valorMinimo <= P.valorOferta)
		                            ELSE ( SELECT ISNULL(MAX(MPF.numeroParcelas),0) FROM dbo.MeioPagamentoFaixa MPF INNER JOIN MeioPagamento MP ON MP.meioPagamentoId = MPF.meioPagamentoId WHERE MP.ativo = 1 AND valorMinimo <= P.valorUnitario)
		                            END Parcelas
	                            , (SELECT ISNULL(MAX(taxaJuros),0) FROM dbo.MeioPagamentoFaixa) taxaJuros
	                            ,P.disponivel
                            FROM Produto P 
                            INNER JOIN ProdutoCategoria PC ON PC.produtoId = P.produtoId
                            INNER JOIN Categorias C ON c.categoriaId = pc.categoriaId
                            INNER JOIN dbo.TituloEletronico TI ON p.produtoId = ti.tituloEletronicoId
                            INNER JOIN Titulo T ON t.tituloId = ti.tituloId
                            LEFT JOIN dbo.ProdutoImagem TIMG ON TIMG.produtoId = P.produtoId AND TIMG.produtoImagemTipoId = 1
                            LEFT JOIN Arquivo A ON A.ArquivoId = TIMG.ArquivoId
                            WHERE P.exibirSite = 1 AND P.homologado=1 AND P.produtoTipoId=2
                            UNION ALL
                            SELECT 
	                            Produto.ProdutoId,
	                            0 AS TituloId,
	                            Produto.NomeProduto,
	                            Produto.homologado,
	                            'Revista' AS Tipo,
	                            Produto.ValorUnitario,
	                            Produto.ValorOferta,
	                            '' AS DataLancamento
	                            ,A.NomeArquivo 
	                            ,CASE WHEN Produto.valorOferta>0 THEN Produto.valorOferta ELSE Produto.valorUnitario END valor
	                            ,CASE WHEN Produto.valorOferta IS NOT NULL THEN ( SELECT ISNULL(MAX(MPF.numeroParcelas),0) FROM dbo.MeioPagamentoFaixa MPF INNER JOIN MeioPagamento MP ON MP.meioPagamentoId = MPF.meioPagamentoId WHERE MP.ativo = 1 AND valorMinimo <= Produto.valorOferta)
		                            ELSE ( SELECT ISNULL(MAX(MPF.numeroParcelas),0) FROM dbo.MeioPagamentoFaixa MPF INNER JOIN MeioPagamento MP ON MP.meioPagamentoId = MPF.meioPagamentoId WHERE MP.ativo = 1 AND valorMinimo <= Produto.valorUnitario)
		                            END Parcelas
	                            , (SELECT ISNULL(MAX(taxaJuros),0) FROM dbo.MeioPagamentoFaixa) taxaJuros
	                            ,Produto.disponivel
                            FROM Produto 
                            INNER JOIN ProdutoCategoria ON ProdutoCategoria.produtoId = Produto.produtoId
                            INNER JOIN Categorias ON Categorias.categoriaId = ProdutoCategoria.categoriaId
                            INNER JOIN RevistaEdicao ON Produto.produtoId = RevistaEdicao.revistaEdicaoId
                            INNER JOIN Revista ON Revista.revistaId = RevistaEdicao.revistaId
                            LEFT JOIN dbo.ProdutoImagem TIMG ON TIMG.produtoId = Produto.produtoId AND TIMG.produtoImagemTipoId = 1
                            LEFT JOIN Arquivo A ON A.ArquivoId = TIMG.ArquivoId
                            WHERE Produto.exibirSite = 1 AND Produto.homologado=1 AND Produto.produtoTipoId=5
                            UNION ALL
                            SELECT 
	                            Produto.ProdutoId,
	                            0 AS TituloId,
	                            Produto.NomeProduto,
	                            Produto.homologado,
	                            'Assinatura' AS Tipo,
	                            Produto.ValorUnitario,
	                            Produto.ValorOferta,
	                            '' AS DataLancamento
	                            ,A.NomeArquivo 
	                            ,CASE WHEN Produto.valorOferta>0 THEN Produto.valorOferta ELSE Produto.valorUnitario END valor
	                            ,CASE WHEN Produto.valorOferta IS NOT NULL THEN ( SELECT ISNULL(MAX(MPF.numeroParcelas),0) FROM dbo.MeioPagamentoFaixa MPF INNER JOIN MeioPagamento MP ON MP.meioPagamentoId = MPF.meioPagamentoId WHERE MP.ativo = 1 AND valorMinimo <= Produto.valorOferta)
		                            ELSE ( SELECT ISNULL(MAX(MPF.numeroParcelas),0) FROM dbo.MeioPagamentoFaixa MPF INNER JOIN MeioPagamento MP ON MP.meioPagamentoId = MPF.meioPagamentoId WHERE MP.ativo = 1 AND valorMinimo <= Produto.valorUnitario)
		                            END Parcelas
	                            , (SELECT ISNULL(MAX(taxaJuros),0) FROM dbo.MeioPagamentoFaixa) taxaJuros
	                            ,Produto.disponivel
                            FROM Produto 
                            INNER JOIN ProdutoCategoria ON ProdutoCategoria.produtoId = Produto.produtoId
                            INNER JOIN Categorias ON Categorias.categoriaId = ProdutoCategoria.categoriaId
                            INNER JOIN RevistaAssinatura ON Produto.produtoId = RevistaAssinatura.revistaAssinaturaId
                            INNER JOIN Revista ON Revista.revistaId = RevistaAssinatura.revistaId
                            LEFT JOIN dbo.ProdutoImagem TIMG ON TIMG.produtoId = Produto.produtoId AND TIMG.produtoImagemTipoId = 1
                            LEFT JOIN Arquivo A ON A.ArquivoId = TIMG.ArquivoId
                            WHERE Produto.exibirSite = 1 AND Produto.homologado=1 AND Produto.produtoTipoId=7
                            ORDER BY nomeProduto");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@CategoriaId", DbType.Int32, entidade.CategoriaId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                ProdutoListaVH _produto = new ProdutoListaVH();
                PopulaProdutoLista(reader, _produto);
                entidadeRetorno.Add(_produto);
            }
            reader.Close();

            return entidadeRetorno;
        }

        public ProdutoListaVH CarregarProdutoDetalhe(ProdutoListaVH entidade)
        {
            ProdutoListaVH entidadeRetorno = new ProdutoListaVH();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(" ");
            sbSQL.Append(" SELECT ");
            sbSQL.Append(" P.ProdutoId, ");
            sbSQL.Append(" TI.TituloId, ");
            sbSQL.Append(" P.NomeProduto, ");
            sbSQL.Append(" 'Livro Impresso' Tipo,");
            sbSQL.Append(" P.ValorUnitario, ");
            sbSQL.Append(" P.Homologado, ");
            sbSQL.Append("P.ValorOferta, ");
            sbSQL.Append(" T.DataLancamento ");
            sbSQL.Append(",A.NomeArquivo ");
            sbSQL.Append(",CASE WHEN p.valorOferta>0 THEN p.valorOferta ELSE p.valorUnitario END valor ");
            sbSQL.Append(",CASE WHEN p.valorOferta IS NOT NULL THEN ( SELECT ISNULL(MAX(MPF.numeroParcelas),0) FROM dbo.MeioPagamentoFaixa MPF INNER JOIN MeioPagamento MP ON MP.meioPagamentoId = MPF.meioPagamentoId WHERE MP.ativo = 1 AND valorMinimo <= P.valorOferta) ");
            sbSQL.Append("	ELSE ( SELECT ISNULL(MAX(MPF.numeroParcelas),0) FROM dbo.MeioPagamentoFaixa MPF INNER JOIN MeioPagamento MP ON MP.meioPagamentoId = MPF.meioPagamentoId WHERE MP.ativo = 1 AND valorMinimo <= P.valorUnitario) ");
            sbSQL.Append("	END Parcelas ");
            sbSQL.Append(", (SELECT ISNULL(MAX(taxaJuros),0) FROM dbo.MeioPagamentoFaixa) taxaJuros ");
            sbSQL.Append(",P.disponivel  ");
            sbSQL.Append(" FROM Produto P  ");
            sbSQL.Append(" INNER JOIN ProdutoCategoria PC ON PC.produtoId = P.produtoId ");
            sbSQL.Append(" INNER JOIN Categoria C ON c.categoriaId = pc.categoriaId ");
            sbSQL.Append(" INNER JOIN TituloImpresso TI ON p.produtoId = ti.tituloImpressoId ");
            sbSQL.Append(" INNER JOIN Titulo T ON t.tituloId = ti.tituloId ");
            sbSQL.Append(" LEFT JOIN dbo.ProdutoImagem TIMG ON TIMG.produtoId = P.produtoId AND TIMG.produtoImagemTipoId = @tituloImagemTipoId ");
            sbSQL.Append("	   LEFT JOIN Arquivo A ON A.ArquivoId = TIMG.ArquivoId ");
            sbSQL.Append(" WHERE P.ProdutoId = @ProdutoId ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@ProdutoId", DbType.Int32, entidade.ProdutoId);
            _db.AddInParameter(command, "@tituloImagemTipoId", DbType.Int32, entidade.TituloImagemTipoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {

                PopulaProdutoLista(reader, entidadeRetorno);

            }
            reader.Close();

            return entidadeRetorno;
        }

        private void PopulaProdutoLista(IDataReader reader, ProdutoListaVH entidade)
        {
            if (reader["produtoId"] != DBNull.Value)
                entidade.ProdutoId = Convert.ToInt32(reader["produtoId"].ToString());
            if (reader["TituloId"] != DBNull.Value)
                entidade.TituloId = Convert.ToInt32(reader["TituloId"].ToString());
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
            if (reader["DataLancamento"] != DBNull.Value)
                entidade.DataLancamento = Convert.ToDateTime(reader["DataLancamento"].ToString());
            if (reader["NomeArquivo"] != DBNull.Value)
                entidade.NomeArquivo = reader["NomeArquivo"].ToString();
            if (reader["Parcelas"] != DBNull.Value)
                entidade.Parcelas = Convert.ToInt32(reader["Parcelas"].ToString());
            if (reader["disponivel"] != DBNull.Value)
                entidade.Disponivel = Convert.ToBoolean(reader["disponivel"].ToString());
            if (reader["taxaJuros"] != DBNull.Value)
                entidade.TaxaJuros = Convert.ToDecimal(reader["taxaJuros"].ToString());
            entidade.Autores = new AutorADO().CarregarAutores(new Titulo() { TituloId = entidade.TituloId });

        }

        /// <summary>
        /// Carrega todos os Produtos filtrando por nome
        /// </summary>
        /// <param name="Categoria">Produto que contém o nome desejado</param>
        /// <param name="qtdLinhas">Quantidade de linhas desejadas para retorno</param>
        /// <returns>Coleção de Produtos da Promoção</returns>
        public StringBuilder CarregarPorNome(Produto entidade, int qtdLinhas)
        {
            StringBuilder retorno = new StringBuilder();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT TOP ");
            sbSQL.Append(qtdLinhas);
            sbSQL.Append("	produtoId, ");
            sbSQL.Append("    nomeProduto, ");
            sbSQL.Append("    produtoTipoId ");
            sbSQL.Append("FROM Produto P  ");
            sbSQL.Append("WHERE (produtoTipoId = 1) ");
            sbSQL.Append("    AND nomeProduto like @nomeProduto ");
            sbSQL.Append("    AND homologado = 1 ");
            sbSQL.Append("    AND disponivel = 1 ");
            sbSQL.Append("    AND exibirSite = 1 ");
            sbSQL.Append("ORDER BY nomeProduto ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@nomeProduto", DbType.String, "%" + entidade.NomeProduto + "%");

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                retorno.Append("{\"");
                retorno.Append(reader["produtoId"].ToString());
                retorno.Append("\":\"");
                if (Convert.ToInt32(reader["produtoTipoId"].ToString()) == 1)
                {
                    retorno.Append(reader["NomeProduto"].ToString());
                }
                else
                {
                    retorno.Append(String.Concat(reader["NomeProduto"].ToString(), " - Ebook"));
                }
                retorno.Append("\"},");
            }
            reader.Close();

            return retorno;
        }
        #endregion

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

            sbSQL.Append(" SELECT  ");
            sbSQL.Append(" '#TIPO#' Tipo ");
            sbSQL.Append(" ,ProdutoCategoria.CategoriaId ");
            sbSQL.Append(" ,dbo.AreaDeConhecimentoDaCategoria(ProdutoCategoria.CategoriaId) AreaId");
            sbSQL.Append(" ,P.ProdutoId ");
            sbSQL.Append(" ,P.NomeProduto ");
            sbSQL.Append(" ,P.ValorUnitario ");
            sbSQL.Append(" ,P.ValorOferta ");
            sbSQL.Append(" ,P.ProdutoTipoId ");
            sbSQL.Append(" ,P.disponivel ");
            sbSQL.Append(" ,P.homologado ");
            sbSQL.Append(" ,P.peso ");
            sbSQL.Append(" ,P.utilizaFrete ");
            sbSQL.Append(" ,P.exibirSite ");
            sbSQL.Append(" ,P.homologado ");
            sbSQL.Append(" ,T.DataLancamento ");
            sbSQL.Append(" ,A.NomeArquivo ");
            sbSQL.Append(" ,relacionamentoTitulo.tituloId ");

            //sbSQL.Append(" ,#CAMPO_PESO# ");
            sbSQL.Append(" ,'#E_COMPRA_CONJUNTA#' eCompraConjunta");
            sbSQL.Append(" ,CI.quantidade ");
            sbSQL.Append(" ,CI.carrinhoItemId ");
            sbSQL.Append(" ,cicc.* ");
            sbSQL.Append(" ,CASE WHEN p.valorOferta>0 THEN p.valorOferta ELSE p.valorUnitario END valor  ");
            sbSQL.Append(" ,CASE WHEN p.valorOferta IS NOT NULL THEN ( SELECT ISNULL(MAX(MPF.numeroParcelas),0) FROM dbo.MeioPagamentoFaixa MPF INNER JOIN MeioPagamento MP ON MP.meioPagamentoId = MPF.meioPagamentoId WHERE MP.ativo = 1 AND valorMinimo <= P.valorOferta)  ");
            sbSQL.Append(" ELSE ( SELECT ISNULL(MAX(MPF.numeroParcelas),0) FROM dbo.MeioPagamentoFaixa MPF INNER JOIN MeioPagamento MP ON MP.meioPagamentoId = MPF.meioPagamentoId WHERE MP.ativo = 1 AND valorMinimo <= P.valorUnitario)  ");
            sbSQL.Append(" END Parcelas  ");
            sbSQL.Append(" FROM Produto P   ");
            sbSQL.Append(" #RELACIONAMENTO# ");
            sbSQL.Append(" INNER JOIN Titulo T ON t.tituloId = relacionamentoTitulo.tituloId  ");
            sbSQL.Append(" LEFT JOIN CarrinhoItem CI ON CI.produtoId = p.produtoId AND CI.carrinhoId = @carrinhoId ");
            sbSQL.Append(" LEFT JOIN dbo.ProdutoImagem TIMG ON TIMG.produtoId = P.produtoId AND TIMG.produtoImagemTipoId = 1  ");
            sbSQL.Append(" LEFT JOIN Arquivo A ON A.ArquivoId = TIMG.ArquivoId  ");
            sbSQL.Append(" LEFT JOIN CarrinhoItemCompraConjunta cicc ON cicc.CarrinhoItemCompraConjuntaId = ci.carrinhoItemId  ");
            sbSQL.Append(" JOIN ProdutoCategoria ON ProdutoCategoria.ProdutoId = P.ProdutoId ");
            sbSQL.Append("  WHERE  ");

            StringBuilder sbProdutos = new StringBuilder();
            StringBuilder sbProdutosCompraConjunta = new StringBuilder();
            if (carrinho.CarrinhoItens == null || carrinho.CarrinhoItens.Count == 0)
            {
                sbSQL.Append(" 	P.ProdutoId IN (SELECT produtoId FROM CarrinhoItem WHERE carrinhoId = @carrinhoid ) ");
                sbSQL.Append(" AND P.ProdutoTipoId = #TIPO_PRODUTO# ");
            }
            else
            {
                //string produtos = "";
                foreach (CarrinhoItem carrinhoItem in carrinho.CarrinhoItens)
                {
                    if (carrinhoItem.CarrinhoItemCompraConjunta == null)
                    {
                        // Selecao de produtos
                        sbProdutos.Append(string.Concat((sbProdutos.ToString().Length > 0 ? "," : string.Empty), carrinhoItem.Produto.ProdutoId.ToString()));
                    }
                    else
                    {
                        sbProdutosCompraConjunta.Append(string.Concat((sbProdutosCompraConjunta.ToString().Length > 0 ? "," : string.Empty), carrinhoItem.Produto.ProdutoId.ToString()));
                    }
                }
                //sbSQL.AppendFormat(" 	P.ProdutoId IN ({0}) ", sbProdutos.ToString());
                sbSQL.Append(" 	P.ProdutoId IN (#PRODUTOS#) ");
                sbSQL.Append(" AND P.ProdutoTipoId = #TIPO_PRODUTO# ");
            }
            //Define ordem
            //sbSQL.Append(" ORDER BY P.NomeProduto ");

            // Faz a replicação e a substituição conforme o relacionamento
            // TítuloImpresso
            string relacionamentoTituloImpresso = "INNER JOIN TituloImpresso relacionamentoTitulo ON p.produtoId = relacionamentoTitulo.tituloImpressoId";
            string sqlTituloImpresso = sbSQL.ToString().Replace("#RELACIONAMENTO#", relacionamentoTituloImpresso)
                                                       .Replace("#TIPO#", "Livro Impresso")
                //.Replace("#CAMPO_PESO#", " CASE P.utilizaFrete WHEN 1 THEN relacionamentoTitulo.Peso ELSE '0' END Peso")
                                                       .Replace("#TIPO_PRODUTO#", Convert.ToInt32(TipoDeProduto.TituloImpresso).ToString());
            // Título Eletrônico
            string relacionamentoTituloEletronico = "INNER JOIN TituloEletronico relacionamentoTitulo ON p.produtoId = relacionamentoTitulo.tituloEletronicoId";
            string sqlTituloEletronico = sbSQL.ToString().Replace("#RELACIONAMENTO#", relacionamentoTituloEletronico)
                                                       .Replace("#TIPO#", "eBook")
                //.Replace("#CAMPO_PESO#", "'0' Peso")
                                                       .Replace("#TIPO_PRODUTO#", Convert.ToInt32(TipoDeProduto.TituloEletronico).ToString());
            // Capítulo Impresso
            //string relacionamentoCapituloImpresso = string.Concat(relacionamentoTituloImpresso, " JOIN capituloImpresso capi ON relacionamentoTitulo.tituloImpressoId = capi.tituloImpressoId ");
            StringBuilder relacionamentoCapituloImpresso = new StringBuilder();
            relacionamentoCapituloImpresso.Append(" JOIN capituloImpresso ON capituloImpresso.capituloImpressoId = P.ProdutoId ");
            relacionamentoCapituloImpresso.Append(" JOIN TituloImpresso relacionamentoTitulo ON relacionamentoTitulo.tituloImpressoId = capituloImpresso.tituloImpressoId ");
            string sqlCapituloImpresso = sbSQL.ToString().Replace("#RELACIONAMENTO#", relacionamentoCapituloImpresso.ToString())
                                                       .Replace("#TIPO#", "Capítulo Impresso")
                //.Replace("#CAMPO_PESO#", "'0' Peso")
                                                       .Replace("#TIPO_PRODUTO#", Convert.ToInt32(TipoDeProduto.CapituloImpresso).ToString());
            // Capítulo Eletrônico
            //string relacionamentoCapituloEletronico = string.Concat(relacionamentoTituloEletronico, " JOIN capituloEletronico cape ON relacionamentoTitulo.tituloEletronicoId = cape.tituloEletronicoId ");
            StringBuilder relacionamentoCapituloEletronico = new StringBuilder();
            relacionamentoCapituloEletronico.Append(" JOIN capituloEletronico ON capituloEletronico.capituloEletronicoId = P.ProdutoId ");
            relacionamentoCapituloEletronico.Append(" JOIN TituloEletronico relacionamentoTitulo ON relacionamentoTitulo.tituloEletronicoId = capituloEletronico.tituloEletronicoId ");
            string sqlCapituloEletronico = sbSQL.ToString().Replace("#RELACIONAMENTO#", relacionamentoCapituloEletronico.ToString())
                                                       .Replace("#TIPO#", "Capítulo eBook")
                //.Replace("#CAMPO_PESO#", "'0' Peso")
                                                       .Replace("#TIPO_PRODUTO#", Convert.ToInt32(TipoDeProduto.CapituloEletronico).ToString());

            sbSQL = new StringBuilder();
            sbSQL.Append(string.Concat("(", sqlTituloImpresso));
            sbSQL.Append(") UNION ALL (");
            sbSQL.Append(sqlTituloEletronico);
            sbSQL.Append(") UNION ALL (");
            sbSQL.Append(sqlCapituloImpresso);
            sbSQL.Append(") UNION ALL (");
            sbSQL.Append(string.Concat(sqlCapituloEletronico, ")"));

            // Faz a replicação e a substituição conforme o relacionamento
            // TítuloImpresso
            //string relacionamentoTituloImpresso = "INNER JOIN TituloImpresso relacionamentoTitulo ON p.produtoId = relacionamentoTitulo.tituloImpressoId";
            //string sqlTituloImpresso = sbSQL.ToString().Replace("#RELACIONAMENTO#", relacionamentoTituloImpresso)
            //                                           .Replace("#TIPO#", "Título Impresso")
            //                                           .Replace("#CAMPO_PESO#", " CASE P.utilizaFrete WHEN 1 THEN relacionamentoTitulo.Peso ELSE '0' END Peso");
            //// Título Eletrônico
            //string relacionamentoTituloEletronico = "INNER JOIN TituloEletronico relacionamentoTitulo ON p.produtoId = relacionamentoTitulo.tituloEletronicoId";
            //string sqlTituloEletronico = sbSQL.ToString().Replace("#RELACIONAMENTO#", relacionamentoTituloEletronico)
            //                                           .Replace("#TIPO#", "Título Eletrônico")
            //                                             .Replace("#CAMPO_PESO#", "'0' Peso");
            //// Capítulo Impresso
            //string relacionamentoCapituloImpresso = string.Concat(relacionamentoTituloImpresso, " JOIN capituloImpresso capi ON relacionamentoTitulo.tituloImpressoId = capi.tituloImpressoId ");
            //string sqlCapituloImpresso = sbSQL.ToString().Replace("#RELACIONAMENTO#", relacionamentoCapituloImpresso)
            //                                           .Replace("#TIPO#", "Capítulo Impresso")
            //                                             .Replace("#CAMPO_PESO#", "'0' Peso");
            //// Capítulo Eletrônico
            //string relacionamentoCapituloEletronico = string.Concat(relacionamentoTituloEletronico, " JOIN capituloEletronico cape ON relacionamentoTitulo.tituloEletronicoId = cape.tituloEletronicoId ");
            //string sqlCapituloEletronico = sbSQL.ToString().Replace("#RELACIONAMENTO#", relacionamentoCapituloEletronico)
            //                                           .Replace("#TIPO#", "Capítulo Eletrônico")
            //                                               .Replace("#CAMPO_PESO#", "'0' Peso");

            //sbSQL = new StringBuilder();
            //sbSQL.Append(string.Concat("(", sqlTituloImpresso));
            //sbSQL.Append(") UNION ALL (");
            //sbSQL.Append(sqlTituloEletronico);
            //sbSQL.Append(") UNION ALL (");
            //sbSQL.Append(sqlCapituloImpresso);
            //sbSQL.Append(") UNION ALL (");
            //sbSQL.Append(string.Concat(sqlCapituloEletronico, ")"));

            StringBuilder sbSQLFinal = new StringBuilder();
            if ((sbProdutosCompraConjunta.ToString().Length > 0) && (sbProdutos.ToString().Length > 0))
            {
                StringBuilder sbSQLCompraConjunta = new StringBuilder();
                sbSQLCompraConjunta.AppendFormat("({0}) UNION ALL ({1})", sqlTituloImpresso, sqlTituloEletronico);
                // Duplica o SQL
                sbSQLFinal = new StringBuilder();
                string sqlSemCompraConjunta = sbSQL.ToString().Replace("#E_COMPRA_CONJUNTA#", "0").Replace("#PRODUTOS#", sbProdutos.ToString());
                string sqlCompraConjunta = sbSQLCompraConjunta.ToString().Replace("#E_COMPRA_CONJUNTA#", "1").Replace("#PRODUTOS#", sbProdutosCompraConjunta.ToString());
                // Une os SQLs de produtos sem e com compra conjunta
                sbSQLFinal.AppendFormat("({0}) UNION ALL ({1})", sqlSemCompraConjunta, sqlCompraConjunta);
            }
            else if (sbProdutosCompraConjunta.ToString().Length > 0)
            {
                sbSQLFinal.Append(sbSQL.ToString().Replace("#E_COMPRA_CONJUNTA#", "1").Replace("#PRODUTOS#", sbProdutosCompraConjunta.ToString()));
            }
            else
            {
                sbSQLFinal.Append(sbSQL.ToString().Replace("#E_COMPRA_CONJUNTA#", "0").Replace("#PRODUTOS#", sbProdutos.ToString()));
            }

            DbCommand command = _db.GetSqlStringCommand(sbSQLFinal.ToString());

            _db.AddInParameter(command, "@carrinhoid", DbType.Int32, carrinho.CarrinhoId);

            IDataReader reader = _db.ExecuteReader(command);
            while (reader.Read())
            {
                CarrinhoItemVH carrinhoItem = new CarrinhoItemVH();
                PopulaItemCarrinho(reader, carrinhoItem);
                if (carrinho.CarrinhoItens != null && carrinho.CarrinhoItens.Count > 0)
                {
                    bool eCompraConjunta = (reader["eCompraConjunta"].ToString().Equals("1") ? true : false);
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
        /// <param name="totalItensCarrinho"></param>
        /// <returns></returns>
        public List<CarrinhoItemVH> CarregarSimplificadoPorCarrinho(Carrinho carrinho, int totalItensCarrinho)
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

            // Título Impresso
            sbSQL.AppendFormat(" SELECT {0} ", (totalItensCarrinho > 0 ? string.Format("TOP({0})", totalItensCarrinho.ToString()) : string.Empty));
            sbSQL.Append(" P.ProdutoId ");
            sbSQL.Append(" ,P.NomeProduto ");
            sbSQL.Append(" ,P.Homologado ");
            sbSQL.Append(" ,P.ValorUnitario ");
            sbSQL.Append(" ,P.ValorOferta ");
            sbSQL.Append(" ,'#E_COMPRA_CONJUNTA#' eCompraConjunta");
            sbSQL.Append(" ,CI.quantidade ");
            sbSQL.Append(" ,CI.carrinhoItemId ");
            sbSQL.Append(" ,CI.carrinhoId ");
            sbSQL.Append(" ,cicc.* ");
            sbSQL.Append(" ,CASE WHEN p.valorOferta>0 THEN p.valorOferta ELSE p.valorUnitario END valor  ");
            sbSQL.Append(" FROM Produto P   ");
            sbSQL.Append(" LEFT JOIN CarrinhoItem CI ON CI.produtoId = p.produtoId AND CI.carrinhoId = @carrinhoId ");
            sbSQL.Append(" LEFT JOIN CarrinhoItemCompraConjunta cicc ON cicc.CarrinhoItemCompraConjuntaId = ci.carrinhoItemId  ");
            sbSQL.Append("  WHERE  ");

            StringBuilder sbProdutos = new StringBuilder();
            StringBuilder sbProdutosCompraConjunta = new StringBuilder();
            if (carrinho.CarrinhoItens == null || carrinho.CarrinhoItens.Count == 0)
            {
                sbSQL.Append(" 	P.ProdutoId IN (SELECT produtoId FROM CarrinhoItem WHERE carrinhoId = @carrinhoid ) ");
            }
            else
            {
                //string produtos = "";
                foreach (CarrinhoItem carrinhoItem in carrinho.CarrinhoItens)
                {
                    if (carrinhoItem.CarrinhoItemCompraConjunta == null)
                    {
                        // Selecao de produtos
                        sbProdutos.Append(string.Concat((sbProdutos.ToString().Length > 0 ? "," : string.Empty), carrinhoItem.Produto.ProdutoId.ToString()));
                    }
                    else
                    {
                        sbProdutosCompraConjunta.Append(string.Concat((sbProdutosCompraConjunta.ToString().Length > 0 ? "," : string.Empty), carrinhoItem.Produto.ProdutoId.ToString()));
                    }
                }
                sbSQL.Append(" 	P.ProdutoId IN (#PRODUTOS#) ");
            }

            StringBuilder sbSQLFinal = new StringBuilder();
            if ((sbProdutosCompraConjunta.ToString().Length > 0) && (sbProdutos.ToString().Length > 0))
            {
                // Duplica o SQL
                sbSQLFinal = new StringBuilder();
                string sqlSemCompraConjunta = sbSQL.ToString().Replace("#E_COMPRA_CONJUNTA#", "0").Replace("#PRODUTOS#", sbProdutos.ToString());
                string sqlCompraConjunta = sbSQL.ToString().Replace("#E_COMPRA_CONJUNTA#", "1").Replace("#PRODUTOS#", sbProdutosCompraConjunta.ToString());
                // Une os SQLs de produtos sem e com compra conjunta
                sbSQLFinal.AppendFormat("({0}) UNION ALL ({1})", sqlSemCompraConjunta, sqlCompraConjunta);
            }
            else if (sbProdutosCompraConjunta.ToString().Length > 0)
            {
                sbSQLFinal.Append(sbSQL.ToString().Replace("#E_COMPRA_CONJUNTA#", "1").Replace("#PRODUTOS#", sbProdutosCompraConjunta.ToString()));
            }
            else
            {
                sbSQLFinal.Append(sbSQL.ToString().Replace("#E_COMPRA_CONJUNTA#", "0").Replace("#PRODUTOS#", sbProdutos.ToString()));
            }

            DbCommand command = _db.GetSqlStringCommand(sbSQLFinal.ToString());

            _db.AddInParameter(command, "@carrinhoid", DbType.Int32, carrinho.CarrinhoId);

            IDataReader reader = _db.ExecuteReader(command);
            while (reader.Read())
            {
                CarrinhoItemVH carrinhoItem = new CarrinhoItemVH();
                PopulaItemCarrinhoSimplificado(reader, carrinhoItem);
                if (carrinho.CarrinhoItens != null && carrinho.CarrinhoItens.Count > 0)
                {
                    bool eCompraConjunta = (reader["eCompraConjunta"].ToString().Equals("1") ? true : false);
                    foreach (CarrinhoItem _carrinhoItem in carrinho.CarrinhoItens)
                    {
                        if ((_carrinhoItem.Produto.ProdutoId == carrinhoItem.ProdutoId) &&
                            ((!eCompraConjunta && _carrinhoItem.CarrinhoItemCompraConjunta == null) ||
                                (eCompraConjunta && _carrinhoItem.CarrinhoItemCompraConjunta != null)))
                        {
                            carrinhoItem.Quantidade = (decimal)_carrinhoItem.Quantidade;
                            break;
                        }
                    }

                }
                entidadeRetorno.Add(carrinhoItem);
            }
            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// Método que carrega uma coleção de produtos relacionados ao titulo.
        /// </summary>
        /// <param name="titulo">Titulo que se deseja listar Produtos relacionados</param>        
        /// <returns>Coleção de produtos relacionado ao título recebido.</returns>
        public List<Produto> CarregaFormatosDisponiveis(Titulo titulo)
        {
            List<Produto> entidadeRetorno = new List<Produto>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(@"SELECT 
	                            * 
                            FROM 
	                            (
	                            SELECT  
		                            P.*
		                            , posicao = ROW_NUMBER() OVER ( 
									                            PARTITION BY 
										                            produtoTipoId 
									                            ORDER BY 
										                            CASE
										                            WHEN ISNULL(valorOferta,0) = 0
										                            THEN valorUnitario
										                            ELSE valorOferta
										                            END
									                            )
	                            FROM
		                            Produto P
                                    INNER JOIN ProdutoCategoria ON P.produtoId = ProdutoCategoria.produtoId
	                            WHERE
                                    P.produtoId != @produtoId
                                    AND P.exibirSite = 1
                                    AND P.homologado = 1
		                            AND (
		                                EXISTS 
				                                (
				                                SELECT 
					                                *
				                                FROM
					                                dbo.TituloEletronico TA
				                                WHERE
					                                TA.tituloEletronicoId = P.produtoId
					                                AND TA.tituloId = @tituloId
				                                )
		                                OR EXISTS 
				                                (
				                                SELECT
					                                *
				                                FROM
					                                dbo.TituloImpresso TA
				                                WHERE
					                                TA.tituloImpressoId = P.produtoId
					                                AND TA.tituloId = @tituloId
				                                )
		                                OR EXISTS
				                                (
				                                SELECT
					                                *
				                                FROM
					                                dbo.TituloEletronico TA
					                                INNER JOIN dbo.CapituloEletronico CE ON CE.tituloEletronicoId = TA.tituloEletronicoId
				                                WHERE
					                                CE.capituloEletronicoId = P.produtoId
					                                AND TA.tituloId = @tituloId
				                                )
		                                OR EXISTS
				                                (
				                                SELECT
					                                *
				                                FROM
					                                dbo.TituloImpresso TA
					                                INNER JOIN dbo.CapituloImpresso CE ON CE.tituloImpressoId = TA.tituloImpressoId
				                                WHERE
					                                CE.capituloImpressoId = P.produtoId
					                                AND TA.tituloId = @tituloId
				                                )
                                        OR EXISTS
                                                (
                                                SELECT
                                                    *
                                                FROM
                                                    dbo.TituloEletronico TA
                                                    INNER JOIN dbo.TituloEletronicoAluguel TEA ON TEA.tituloEletronicoId = TA.tituloEletronicoId
                                                WHERE
                                                    TEA.tituloEletronicoAluguelId = P.produtoId
                                                    AND TA.tituloId = @tituloId
                                                )
                                        )
	                            ) Derivada 
                            /*WHERE 
	                            Derivada.posicao = 1 */
                            ORDER BY 
	                            Derivada.nomeProduto");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@tituloId", DbType.Int32, titulo.TituloId);

            if (titulo.Conteudo != null && titulo.Conteudo.Produto != null && titulo.Conteudo.Produto.ProdutoId != 0)
                _db.AddInParameter(command, "@produtoId", DbType.Int32, titulo.Conteudo.Produto.ProdutoId);
            else
                _db.AddInParameter(command, "@produtoId", DbType.Int32, 0);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Produto _produto = new Produto();
                PopulaProduto(reader, _produto);
                entidadeRetorno.Add(_produto);
            }
            reader.Close();

            return entidadeRetorno;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entidade"></param>
        private void PopulaItemCarrinhoSimplificado(IDataReader reader, CarrinhoItemVH entidade)
        {
            if (reader["carrinhoId"] != DBNull.Value)
                entidade.CarrinhoId = Convert.ToInt32(reader["carrinhoId"].ToString());
            if (reader["produtoId"] != DBNull.Value)
                entidade.ProdutoId = Convert.ToInt32(reader["produtoId"].ToString());
            if (reader["carrinhoItemId"] != DBNull.Value)
                entidade.CarrinhoItemId = Convert.ToInt32(reader["carrinhoItemId"].ToString());
            if (reader["CarrinhoItemCompraConjuntaId"] != DBNull.Value)
                entidade.CarrinhoItemCompraConjuntaId = Convert.ToInt32(reader["CarrinhoItemCompraConjuntaId"].ToString());
            if (reader["NomeProduto"] != DBNull.Value)
                entidade.NomeProduto = reader["NomeProduto"].ToString();
            if (reader["ValorUnitario"] != DBNull.Value)
                entidade.ValorUnitario = Convert.ToDecimal(reader["ValorUnitario"].ToString());
            if (reader["ValorOferta"] != DBNull.Value)
                entidade.ValorOferta = Convert.ToDecimal(reader["ValorOferta"].ToString());
            if (reader["Valor"] != DBNull.Value)
                entidade.Valor = Convert.ToDecimal(reader["Valor"].ToString());
            if (reader["quantidade"] != DBNull.Value)
                entidade.Quantidade = Convert.ToDecimal(reader["quantidade"].ToString()); ;
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
            if (reader["AreaId"] != DBNull.Value)
                entidade.AreaId = Convert.ToInt32(reader["AreaId"].ToString());
            if (reader["CategoriaId"] != DBNull.Value)
                entidade.CategoriaId = Convert.ToInt32(reader["CategoriaId"].ToString());
            //if (reader["TituloId"] != DBNull.Value)
            //    entidade.TituloId = Convert.ToInt32(reader["TituloId"].ToString());
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
            if (reader["DataLancamento"] != DBNull.Value)
                entidade.DataLancamento = Convert.ToDateTime(reader["DataLancamento"].ToString());
            if (reader["NomeArquivo"] != DBNull.Value)
                entidade.NomeArquivo = reader["NomeArquivo"].ToString();
            if (reader["Parcelas"] != DBNull.Value)
                entidade.Parcelas = Convert.ToInt32(reader["Parcelas"].ToString());
            if (reader["exibirSite"] != DBNull.Value)
                entidade.ExibirSite = Boolean.Parse(reader["exibirSite"].ToString());
            if (reader["disponivel"] != DBNull.Value)
                entidade.Disponivel = Boolean.Parse(reader["disponivel"].ToString());
            if (reader["quantidade"] != DBNull.Value)
                entidade.Quantidade = Convert.ToDecimal(reader["quantidade"].ToString()); ;
            if (reader["Peso"] != DBNull.Value)
                entidade.Peso = Convert.ToDecimal(reader["Peso"].ToString()); ;
            if (reader["utilizaFrete"] != DBNull.Value)
                entidade.UtilizaFrete = Boolean.Parse(reader["utilizaFrete"].ToString());
            if (reader["tituloId"] != DBNull.Value)
                entidade.TituloId = Convert.ToInt32(reader["tituloId"].ToString());
        }

        /// <summary>
        /// Método que retorna uma coleção de Todas Publicacoes do Usuario.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos Produtos.</returns>
        public List<TituloVH> ConsultaTodasPublicacoesUsuario(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro, int usuarioId)
        {

            List<TituloVH> entidadesRetorno = new List<TituloVH>();

            StringBuilder sbSql = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();
            StringBuilder sbOrder = new StringBuilder();

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

            //sbSql.Append("WITH    Categorias ( categoriaId, nomeCategoria, categoriaIdPai, Nivel ) ");
            //sbSql.Append("          AS ( SELECT   C.categoriaId , ");
            //sbSql.Append("                        C.nomeCategoria , ");
            //sbSql.Append("                        C.categoriaIdPai , ");
            //sbSql.Append("                        0 AS Nivel ");
            //sbSql.Append("               FROM     Categoria AS C ");
            //sbSql.Append("               WHERE    EXISTS ( SELECT UI.categoriaId ");
            //sbSql.Append("                                 FROM   dbo.UsuarioInteresse UI ");
            //sbSql.Append("                                 WHERE  C.categoriaId = UI.categoriaId ");
            //sbSql.Append("                                        AND usuarioId = @usuarioIdLogado ) ");
            //sbSql.Append("               UNION ALL ");
            //sbSql.Append("               SELECT   C.categoriaId , ");
            //sbSql.Append("                        C.nomeCategoria , ");
            //sbSql.Append("                        C.categoriaIdPai , ");
            //sbSql.Append("                        Nivel + 1 ");
            //sbSql.Append("               FROM     Categoria AS C ");
            //sbSql.Append("                        INNER JOIN Categorias AS CS ON c.CategoriaIdPai = CS.categoriaId ");
            //sbSql.Append("             ), ");
            sbSql.Append("WITH        Produtos ( produtoId, tituloId, tipo, disponivel ) ");
            sbSql.Append("          AS ( SELECT   P.tituloImpressoId , ");
            sbSql.Append("                        p.tituloId, ");
            sbSql.Append("                        'Livro Impresso', ");
            sbSql.Append("                        PO.disponivel ");
            sbSql.Append("               FROM     dbo.TituloImpresso P ");
            sbSql.Append("                        INNER JOIN dbo.Produto PO ON P.tituloImpressoId = PO.produtoId ");
            sbSql.Append("                                           AND PO.homologado = 1 AND PO.exibirSite = 1 ");
            sbSql.Append("               UNION ALL ");
            sbSql.Append("               SELECT   P.tituloEletronicoId , ");
            sbSql.Append("                        p.tituloId, ");
            sbSql.Append("                        'eBook', ");
            sbSql.Append("                        PO.disponivel ");
            sbSql.Append("               FROM     dbo.TituloEletronico P ");
            sbSql.Append("                        INNER JOIN dbo.Produto PO ON P.tituloEletronicoId = PO.produtoId ");
            sbSql.Append("                                           AND PO.homologado = 1 AND PO.exibirSite = 1 ");
            sbSql.Append("             ) ");
            sbSql.Append("SELECT P.*, PIA.arquivoId AS arquivoIdCapa , ");
            sbSql.Append("            PIA.nomeArquivo AS nomeArquivoCapa , ");
            sbSql.Append("            PIA.nomeArquivoOriginal AS nomeArquivoOriginalCapa , ");
            sbSql.Append("            PIA.dataHoraUpload AS dataHoraUploadCapa , ");
            sbSql.Append("            PIA.tamanhoArquivo AS tamanhoArquivoCapa  ");
            sbSql.Append("FROM (  ");
            sbSql.Append("SELECT * FROM (             ");
            sbSql.Append("    SELECT  P.*,  ");
            sbSql.Append("            PS.tituloId, ");
            sbSql.Append("            PS.tipo, ");
            sbSql.Append("            T.dataLancamento, ");
            sbSql.Append("            T.nomeTitulo, ");
            sbSql.Append("            (SELECT    A.nomeAutor + '; ' AS [text()]");
            sbSql.Append("                  FROM      Autor A");
            sbSql.Append("                  INNER JOIN dbo.TituloAutor TA ON TA.autorId = A.autorId");
            sbSql.Append("                                              AND TA.tituloId = PS.tituloId");
            sbSql.Append("                  ORDER BY  A.nomeAutor");
            sbSql.Append("                  FOR");
            sbSql.Append("                  XML PATH('')");
            sbSql.Append("             ) AS Autores,");
            sbSql.Append("            PC.categoriaId,");
            sbSql.Append("            CASE WHEN P.valorOferta IS NOT NULL THEN ( SELECT ISNULL(MAX(MPF.numeroParcelas),0) FROM dbo.MeioPagamentoFaixa MPF INNER JOIN MeioPagamento MP ON MP.meioPagamentoId = MPF.meioPagamentoId WHERE MP.ativo = 1 AND valorMinimo <= P.valorOferta) ");
            sbSql.Append("	            ELSE ( SELECT ISNULL(MAX(MPF.numeroParcelas),0) FROM dbo.MeioPagamentoFaixa MPF INNER JOIN MeioPagamento MP ON MP.meioPagamentoId = MPF.meioPagamentoId WHERE MP.ativo = 1 AND valorMinimo <= P.valorUnitario) ");
            sbSql.Append("	            END Parcelas, ");
            sbSql.Append("            (SELECT ISNULL(MAX(taxaJuros),0) FROM dbo.MeioPagamentoFaixa) taxaJuros, ");
            sbSql.Append("            dbo.AreaDeConhecimentoDaCategoria(pc.categoriaId) AS identificadorArea,");
            sbSql.Append("            ROW_NUMBER() OVER (ORDER BY P.nomeProduto) AS R ");
            sbSql.Append("    FROM    Produtos PS ");
            sbSql.Append("            INNER JOIN dbo.ProdutoCategoria PC ON pc.produtoId = PS.produtoId ");
            sbSql.Append("            INNER JOIN dbo.UsuarioInteresse UI ON PC.categoriaId = UI.categoriaId AND usuarioId = @usuarioIdLogado ");
            //sbSql.Append("            INNER JOIN Categorias CS ON CS.categoriaId = PC.categoriaId ");
            sbSql.Append("            INNER JOIN dbo.Produto P ON P.produtoId = PS.produtoId ");
            sbSql.Append("            INNER JOIN dbo.Titulo T ON PS.tituloId = T.tituloId ");
            sbSql.Append(" ) AS Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            sbSql.Append(") AS P  ");
            sbSql.Append("LEFT  JOIN dbo.ProdutoImagem PI ON PI.produtoId = P.produtoId AND pi.produtoImagemTipoId = 1  ");
            sbSql.Append("LEFT  JOIN dbo.Arquivo PIA ON PI.arquivoId = PIA.arquivoId ");
            if (sbOrder.Length > 0) { sbSql.Append(sbOrder.ToString()); }

            if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
            {
                sbSql.Append(string.Concat(" AND ", filtro.GetWhereString()));
            }

            DbCommand command = _db.GetSqlStringCommand(sbSql.ToString());
            _db.AddInParameter(command, "@usuarioIdLogado", DbType.Int32, usuarioId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                TituloVH entidadeRetornoAnterior = new TituloVH();
                PopulaTituloVHCompleto(reader, entidadeRetornoAnterior);
                entidadesRetorno.Add(entidadeRetornoAnterior);

            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que popula a PublicacaoUsuarioVH.
        /// </summary>
        public static void PopulaTituloVH(IDataReader reader, TituloVH entidade)
        {
            if (reader["tituloId"] != DBNull.Value)
            {
                entidade.TituloId = Convert.ToInt32(reader["tituloId"].ToString());
            }

            if (reader["produtoId"] != DBNull.Value)
            {
                entidade.ProdutoId = Convert.ToInt32(reader["produtoId"].ToString());
            }

            if (reader["categoriaId"] != DBNull.Value)
            {
                entidade.CategoriaId = Convert.ToInt32(reader["categoriaId"].ToString());
            }

            if (reader["identificadorArea"] != DBNull.Value)
            {
                entidade.AreaId = Convert.ToInt32(reader["identificadorArea"].ToString());
            }

            if (reader["nomeTitulo"] != DBNull.Value)
                entidade.NomeTitulo = reader["nomeTitulo"].ToString();

            if (reader["tipo"] != DBNull.Value)
                entidade.Tipo = reader["tipo"].ToString();

            if (reader["autores"] != DBNull.Value)
                entidade.Autores = reader["autores"].ToString();

            if (reader["arquivoIdCapa"] != DBNull.Value)
            {
                if (entidade.Arquivo == null) entidade.Arquivo = new Arquivo();
                entidade.Arquivo.ArquivoId = Convert.ToInt32(reader["arquivoIdCapa"].ToString());
            }

            if (reader["nomeArquivoCapa"] != DBNull.Value)
            {
                if (entidade.Arquivo == null) entidade.Arquivo = new Arquivo();
                entidade.Arquivo.NomeArquivo = reader["nomeArquivoCapa"].ToString();
            }

            if (reader["nomeArquivoOriginalCapa"] != DBNull.Value)
            {
                if (entidade.Arquivo == null) entidade.Arquivo = new Arquivo();
                entidade.Arquivo.NomeArquivoOriginal = reader["nomeArquivoOriginalCapa"].ToString();
            }

            if (reader["disponivel"] != DBNull.Value)
                entidade.Disponivel = Convert.ToBoolean(reader["disponivel"].ToString());
        }

        public static void PopulaTituloVHPerfil(IDataReader reader, TituloVH entidade)
        {
            if (reader["tituloId"] != DBNull.Value)
            {
                entidade.TituloId = Convert.ToInt32(reader["tituloId"].ToString());
            }

            if (reader["produtoId"] != DBNull.Value)
            {
                entidade.ProdutoId = Convert.ToInt32(reader["produtoId"].ToString());
            }

            if (reader["categoriaId"] != DBNull.Value)
            {
                entidade.CategoriaId = Convert.ToInt32(reader["categoriaId"].ToString());
            }

            if (reader["identificadorArea"] != DBNull.Value)
            {
                entidade.AreaId = Convert.ToInt32(reader["identificadorArea"].ToString());
            }

            if (reader["nomeTitulo"] != DBNull.Value)
            {
                entidade.NomeTitulo = reader["nomeTitulo"].ToString();
            }

            if (reader["tipo"] != DBNull.Value)
            {
                entidade.Tipo = reader["tipo"].ToString();
            }

            if (reader["arquivoIdCapa"] != DBNull.Value)
            {
                if (entidade.Arquivo == null) entidade.Arquivo = new Arquivo();
                entidade.Arquivo.ArquivoId = Convert.ToInt32(reader["arquivoIdCapa"].ToString());
            }

            if (reader["nomeArquivoCapa"] != DBNull.Value)
            {
                if (entidade.Arquivo == null) entidade.Arquivo = new Arquivo();
                entidade.Arquivo.NomeArquivo = reader["nomeArquivoCapa"].ToString();
            }

            if (reader["nomeArquivoOriginalCapa"] != DBNull.Value)
            {
                if (entidade.Arquivo == null) entidade.Arquivo = new Arquivo();
                entidade.Arquivo.NomeArquivoOriginal = reader["nomeArquivoOriginalCapa"].ToString();
            }

            if (reader["disponivel"] != DBNull.Value)
                entidade.Disponivel = Convert.ToBoolean(reader["disponivel"].ToString());
        }

        /// <summary>
        /// Método que conta os registros de Publicações do usuário.
        /// </summary>
        public int ContaPublicacaoUsuario(Int32 usuarioId)
        {
            int retorno = 0;

            StringBuilder sbSql = new StringBuilder();
            //StringBuilder sbWhere = new StriKCngBuilder();

            //sbSql.Append("WITH    Categorias ( categoriaId, nomeCategoria, categoriaIdPai, Nivel ) ");
            //sbSql.Append("          AS ( SELECT   C.categoriaId , ");
            //sbSql.Append("                        C.nomeCategoria , ");
            //sbSql.Append("                        C.categoriaIdPai , ");
            //sbSql.Append("                        0 AS Nivel ");
            //sbSql.Append("               FROM     Categoria AS C ");
            //sbSql.Append("               WHERE    EXISTS ( SELECT UI.categoriaId ");
            //sbSql.Append("                                 FROM   dbo.UsuarioInteresse UI ");
            //sbSql.Append("                                 WHERE  C.categoriaId = UI.categoriaId ");
            //sbSql.Append("                                        AND usuarioId = @usuarioIdLogado ) ");
            //sbSql.Append("               UNION ALL ");
            //sbSql.Append("               SELECT   C.categoriaId , ");
            //sbSql.Append("                        C.nomeCategoria , ");
            //sbSql.Append("                        C.categoriaIdPai , ");
            //sbSql.Append("                        Nivel + 1 ");
            //sbSql.Append("               FROM     Categoria AS C ");
            //sbSql.Append("                        INNER JOIN Categorias AS CS ON c.CategoriaIdPai = CS.categoriaId ");
            //sbSql.Append("             ), ");
            sbSql.Append("WITH        Produtos ( produtoId, tituloId ) ");
            sbSql.Append("          AS ( SELECT   P.tituloImpressoId , ");
            sbSql.Append("                        p.tituloId ");
            sbSql.Append("               FROM     dbo.TituloImpresso P ");
            sbSql.Append("                        INNER JOIN dbo.Produto PO ON P.tituloImpressoId = PO.produtoId ");
            sbSql.Append("                                           AND PO.homologado = 1 AND PO.exibirSite = 1 ");
            sbSql.Append("               UNION ALL ");
            sbSql.Append("               SELECT   P.tituloEletronicoId , ");
            sbSql.Append("                        p.tituloId ");
            sbSql.Append("               FROM     dbo.TituloEletronico P ");
            sbSql.Append("                        INNER JOIN dbo.Produto PO ON P.tituloEletronicoId = PO.produtoId ");
            sbSql.Append("                                           AND PO.homologado = 1 AND PO.exibirSite = 1 ");
            sbSql.Append("             ) ");
            sbSql.Append("    SELECT  COUNT(*) as Total ");
            sbSql.Append("    FROM    Produtos PS ");
            sbSql.Append("            INNER JOIN dbo.ProdutoCategoria PC ON pc.produtoId = PS.produtoId ");
            sbSql.Append("            INNER JOIN dbo.UsuarioInteresse UI ON PC.categoriaId = UI.categoriaId AND usuarioId = @usuarioIdLogado ");
            //sbSql.Append("            INNER JOIN Categorias CS ON CS.categoriaId = PC.categoriaId ");
            sbSql.Append("            INNER JOIN dbo.Produto P ON P.produtoId = PS.produtoId          ");

            DbCommand command = _db.GetSqlStringCommand(sbSql.ToString());
            _db.AddInParameter(command, "@usuarioIdLogado", DbType.Int32, usuarioId);
            IDataReader reader = _db.ExecuteReader(command);

            if ((reader.Read()) && ((reader["Total"] != DBNull.Value)))
            {
                retorno = (int)reader["Total"];
            }
            reader.Close();

            return retorno;
        }

        /// <summary>
        /// Método que retorna uma coleção de Todas Biliotecas do Usuario.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos Produtos.</returns>
        public List<TituloVH> ConsultaTodasBibliotecasUsuario(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro, int usuarioId)
        {
            List<TituloVH> entidadesRetorno = new List<TituloVH>();

            StringBuilder sbSql = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();
            StringBuilder sbOrder = new StringBuilder();

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

            sbSql.Append(@"WITH Produtos(produtoId, tituloId, tipo, disponivel)
                                        AS (SELECT TituloImpresso.tituloImpressoId,
                                                    TituloImpresso.tituloId AS ProdutoId,
                                                    'Livro Impresso',
                                                    Produto.disponivel
                                            FROM TituloImpresso
                                            INNER JOIN Produto ON TituloImpresso.tituloImpressoId = Produto.produtoId
                                                                AND Produto.homologado = 1 AND Produto.exibirSite = 1
                                            WHERE EXISTS (SELECT produtoId
							                                FROM PedidoItem
							                                INNER JOIN Pedido on Pedido.pedidoId = PedidoItem.pedidoId
							                                WHERE Pedido.usuarioId = @usuarioIdLogado
			  					                            AND Pedido.pedidoStatusId = 1
			  					                            AND Produto.produtoId = PedidoItem.produtoId)
                                            UNION ALL
                                            SELECT TituloEletronico.tituloEletronicoId,
                                                    TituloEletronico.tituloId AS ProdutoId,
                                                    'eBook',
                                                    Produto.disponivel
                                            FROM TituloEletronico
                                            INNER JOIN Produto ON TituloEletronico.tituloEletronicoId = Produto.produtoId
                                                                AND Produto.homologado = 1 AND Produto.exibirSite = 1
                                            WHERE EXISTS (SELECT produtoId
							                                FROM PedidoItem
							                                INNER JOIN Pedido on Pedido.pedidoId = PedidoItem.pedidoId
							                                WHERE Pedido.usuarioId = @usuarioIdLogado
			  					                            AND Pedido.pedidoStatusId = 1
			  					                            AND Produto.produtoId = PedidoItem.produtoId)
                                            )
                            SELECT P.*, 
	                            Arquivo.arquivoId AS arquivoIdCapa,
                                Arquivo.nomeArquivo AS nomeArquivoCapa,
                                Arquivo.nomeArquivoOriginal AS nomeArquivoOriginalCapa,
                                Arquivo.dataHoraUpload AS dataHoraUploadCapa,
                                Arquivo.tamanhoArquivo AS tamanhoArquivoCapa
                            FROM (
                                SELECT Produtos.produtoId, 
                                    Produtos.tipo, 
                                    Titulo.*,
                                    ProdutoCategoria.categoriaId,
                                    dbo.AreaDeConhecimentoDaCategoria(ProdutoCategoria.categoriaId) AS identificadorArea,
                                    Produtos.disponivel,
	                                ROW_NUMBER() OVER (ORDER BY Titulo.nomeTitulo) AS R
                                FROM Titulo
                                INNER JOIN Produtos ON Titulo.tituloId = Produtos.tituloId
                                INNER JOIN ProdutoCategoria ON ProdutoCategoria.produtoId = Produtos.produtoId
                            ) AS P 
                            LEFT JOIN ProdutoImagem ON ProdutoImagem.produtoId = P.produtoId AND ProdutoImagem.produtoImagemTipoId = 1 
                            LEFT JOIN Arquivo ON ProdutoImagem.arquivoId = Arquivo.arquivoId
                            WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            if (sbOrder.Length > 0) { sbSql.Append(sbOrder.ToString()); }

            DbCommand command = _db.GetSqlStringCommand(sbSql.ToString());
            _db.AddInParameter(command, "@usuarioIdLogado", DbType.Int32, usuarioId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                TituloVH entidadeRetornoAnterior = new TituloVH();
                PopulaTituloVHPerfil(reader, entidadeRetornoAnterior);
                entidadesRetorno.Add(entidadeRetornoAnterior);

            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que conta os registros de Bibliotecas do usuário.
        /// </summary>
        public int ContaBibliotecaUsuario(Int32 usuarioId)
        {
            int retorno = 0;

            StringBuilder sbSql = new StringBuilder();

            sbSql.Append(@"WITH Produtos(produtoId, tituloId, tipo, disponivel)
                                      AS (SELECT TituloImpresso.tituloImpressoId,
                                                 TituloImpresso.tituloId AS ProdutoId,
                                                 'Livro Impresso',
                                                 Produto.disponivel
                                           FROM TituloImpresso
                                           INNER JOIN Produto ON TituloImpresso.tituloImpressoId = Produto.produtoId
                                                              AND Produto.homologado = 1 AND Produto.exibirSite = 1
                                           WHERE EXISTS (SELECT produtoId
							                              FROM PedidoItem
							                              INNER JOIN Pedido on Pedido.pedidoId = PedidoItem.pedidoId
							                              WHERE Pedido.usuarioId = @usuarioIdLogado
			  					                            AND Pedido.pedidoStatusId = 1
			  					                            AND Produto.produtoId = PedidoItem.produtoId)
                                           UNION ALL
                                           SELECT TituloEletronico.tituloEletronicoId,
                                                  TituloEletronico.tituloId AS ProdutoId,
                                                  'eBook',
                                                  Produto.disponivel
                                           FROM TituloEletronico
                                           INNER JOIN Produto ON TituloEletronico.tituloEletronicoId = Produto.produtoId
                                                              AND Produto.homologado = 1 AND Produto.exibirSite = 1
                                           WHERE EXISTS (SELECT produtoId
							                              FROM PedidoItem
							                              INNER JOIN Pedido on Pedido.pedidoId = PedidoItem.pedidoId
							                              WHERE Pedido.usuarioId = @usuarioIdLogado
			  					                            AND Pedido.pedidoStatusId = 1
			  					                            AND Produto.produtoId = PedidoItem.produtoId)
                                         )
                            SELECT COUNT(Produtos.produtoId) AS Total
                            FROM Titulo
                            INNER JOIN Produtos ON Titulo.tituloId = Produtos.tituloId
                            INNER JOIN ProdutoCategoria ON ProdutoCategoria.produtoId = Produtos.produtoId");

            DbCommand command = _db.GetSqlStringCommand(sbSql.ToString());
            _db.AddInParameter(command, "@usuarioIdLogado", DbType.Int32, usuarioId);
            IDataReader reader = _db.ExecuteReader(command);

            if ((reader.Read()) && ((reader["Total"] != DBNull.Value)))
            {
                retorno = (int)reader["Total"];
            }
            reader.Close();

            return retorno;
        }

        /// <summary>
        /// Método que retorna uma coleção de Todas Publicacoes do Usuario.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos Produtos.</returns>
        public List<TituloVH> ConsultaTodasFavoritosUsuario(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro, int usuarioId)
        {
            List<TituloVH> entidadesRetorno = new List<TituloVH>();

            StringBuilder sbSql = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();
            StringBuilder sbOrder = new StringBuilder();

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

            sbSql.Append(@"WITH Produtos(produtoId, tituloId, tipo, disponivel)
                                        AS (SELECT TituloImpresso.tituloImpressoId,
                                                    TituloImpresso.tituloId AS ProdutoId,
                                                    'Livro Impresso',
                                                    Produto.disponivel
                                            FROM TituloImpresso
                                            INNER JOIN Produto ON TituloImpresso.tituloImpressoId = Produto.produtoId
                                                                AND Produto.homologado = 1 AND Produto.exibirSite = 1
                                            WHERE EXISTS (SELECT conteudoId
                                                            FROM Favorito
                                                            WHERE usuarioId = @usuarioIdLogado
	                                                        AND Produto.produtoId = conteudoId)
                                            UNION ALL
                                            SELECT TituloEletronico.tituloEletronicoId,
                                                    TituloEletronico.tituloId AS ProdutoId,
                                                    'eBook',
                                                    Produto.disponivel
                                            FROM TituloEletronico
                                            INNER JOIN Produto ON TituloEletronico.tituloEletronicoId = Produto.produtoId
                                                                AND Produto.homologado = 1 AND Produto.exibirSite = 1
                                            WHERE EXISTS (SELECT conteudoId
                                                            FROM Favorito
                                                            WHERE usuarioId = @usuarioIdLogado
	                                                        AND Produto.produtoId = conteudoId)
                                            )
                            SELECT P.*, 
                                Arquivo.arquivoId AS arquivoIdCapa,
                                Arquivo.nomeArquivo AS nomeArquivoCapa,
                                Arquivo.nomeArquivoOriginal AS nomeArquivoOriginalCapa,
                                Arquivo.dataHoraUpload AS dataHoraUploadCapa,
                                Arquivo.tamanhoArquivo AS tamanhoArquivoCapa
                            FROM (
                                SELECT Produtos.produtoId, 
                                    Produtos.tipo, 
                                    Titulo.*,
                                    ProdutoCategoria.categoriaId,
                                    dbo.AreaDeConhecimentoDaCategoria(ProdutoCategoria.categoriaId) AS identificadorArea,
                                    Produtos.disponivel,
                                    ROW_NUMBER() OVER (ORDER BY Titulo.nomeTitulo) AS R
                                FROM Titulo
                                INNER JOIN Produtos ON Titulo.tituloId = Produtos.tituloId
                                INNER JOIN ProdutoCategoria ON ProdutoCategoria.produtoId = Produtos.produtoId
                            ) AS P 
                            LEFT JOIN ProdutoImagem ON ProdutoImagem.produtoId = P.produtoId AND ProdutoImagem.produtoImagemTipoId = 1 
                            LEFT JOIN Arquivo ON ProdutoImagem.arquivoId = Arquivo.arquivoId
                            WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            if (sbOrder.Length > 0) { sbSql.Append(sbOrder.ToString()); }

            DbCommand command = _db.GetSqlStringCommand(sbSql.ToString());
            _db.AddInParameter(command, "@usuarioIdLogado", DbType.Int32, usuarioId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                TituloVH entidadeRetornoAnterior = new TituloVH();
                PopulaTituloVHPerfil(reader, entidadeRetornoAnterior);
                entidadesRetorno.Add(entidadeRetornoAnterior);

            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que conta os registros de Publicações do usuário.
        /// </summary>
        public int ContaFavoritosUsuario(Int32 usuarioId)
        {
            int retorno = 0;

            StringBuilder sbSql = new StringBuilder();

            sbSql.Append(@"WITH Produtos(produtoId, tituloId, tipo, disponivel)
                                    AS (SELECT TituloImpresso.tituloImpressoId,
                                                TituloImpresso.tituloId AS ProdutoId,
                                                'Livro Impresso',
                                                Produto.disponivel
                                        FROM TituloImpresso
                                        INNER JOIN Produto ON TituloImpresso.tituloImpressoId = Produto.produtoId
                                                            AND Produto.homologado = 1 AND Produto.exibirSite = 1
                                        WHERE EXISTS (SELECT conteudoId
                                                        FROM Favorito
                                                        WHERE usuarioId = @usuarioIdLogado
	                                                    AND Produto.produtoId = conteudoId)
                                        UNION ALL
                                        SELECT TituloEletronico.tituloEletronicoId,
                                                TituloEletronico.tituloId AS ProdutoId,
                                                'eBook',
                                                Produto.disponivel
                                        FROM TituloEletronico
                                        INNER JOIN Produto ON TituloEletronico.tituloEletronicoId = Produto.produtoId
                                                            AND Produto.homologado = 1 AND Produto.exibirSite = 1
                                        WHERE EXISTS (SELECT conteudoId
                                                        FROM Favorito
                                                        WHERE usuarioId = @usuarioIdLogado
	                                                    AND Produto.produtoId = conteudoId)
                                        )
                        SELECT COUNT(Produtos.produtoId) AS Total
                        FROM Titulo
                        INNER JOIN Produtos ON Titulo.tituloId = Produtos.tituloId
                        INNER JOIN ProdutoCategoria ON ProdutoCategoria.produtoId = Produtos.produtoId");

            DbCommand command = _db.GetSqlStringCommand(sbSql.ToString());
            _db.AddInParameter(command, "@usuarioIdLogado", DbType.Int32, usuarioId);
            IDataReader reader = _db.ExecuteReader(command);

            if ((reader.Read()) && ((reader["Total"] != DBNull.Value)))
            {
                retorno = (int)reader["Total"];
            }
            reader.Close();

            return retorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="produtoBO"></param>
        /// <param name="seloBO"></param>
        public void RelacionarProdutoSelo(Produto produtoBO, Selo seloBO)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO ProdutoSelo ");
            sbSQL.Append(" (produtoId, seloId) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@produtoId, @seloId) ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@produtoId", DbType.Int32, produtoBO.ProdutoId);
            _db.AddInParameter(command, "@seloId", DbType.Int32, seloBO.SeloId);

            // Executa a query.
            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="produtoBO"></param>
        /// <param name="categoriaBO"></param>
        /// <returns></returns>
        public bool ProdutoCategoriaRelacionado(Produto produtoBO, Categoria categoriaBO)
        {
            bool entidadeRetorno = false;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(@"SELECT
								COUNT(*) AS Total
							FROM 
								ProdutoCategoria
							WHERE
                                ProdutoCategoria.produtoId = @produtoId
								AND ProdutoCategoria.categoriaId = @categoriaId
                            ");
            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@produtoId", DbType.Int32, produtoBO.ProdutoId);
            _db.AddInParameter(command, "@categoriaId", DbType.Int32, categoriaBO.CategoriaId);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="produtoBO"></param>
        /// <param name="categoriaBO"></param>
        public void RelacionarProdutoCategoria(Produto produtoBO, Categoria categoriaBO)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO ProdutoCategoria ");
            sbSQL.Append(" (produtoId, categoriaId) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@produtoId, @categoriaId) ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@produtoId", DbType.Int32, produtoBO.ProdutoId);
            _db.AddInParameter(command, "@categoriaId", DbType.Int32, categoriaBO.CategoriaId);

            // Executa a query.
            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="produto"></param>
        public void AtualizarNome(Produto produto)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append("UPDATE Produto ");
            sbSQL.Append("SET nomeProduto = @nomeProduto ");
            sbSQL.Append("WHERE produtoId = @produtoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@nomeProduto", DbType.String, produto.NomeProduto);
            _db.AddInParameter(command, "@produtoId", DbType.Int32, produto.ProdutoId);

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void AtualizarMenosNome(Produto entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE Produto SET ");
            sbSQL.Append(" produtoTipoId=@produtoTipoId, disponivel=@disponivel, fabricanteId=@fabricanteId, valorUnitario=@valorUnitario, valorOferta=@valorOferta, codigoEAN13=@codigoEAN13, codigoProduto=@codigoProduto, exibirSite=@exibirSite, utilizaFrete=@utilizaFrete, peso=@peso ");
            sbSQL.Append(" WHERE produtoId=@produtoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.ProdutoId);
            _db.AddInParameter(command, "@produtoTipoId", DbType.Int32, entidade.ProdutoTipo.ProdutoTipoId);
            _db.AddInParameter(command, "@disponivel", DbType.Boolean, entidade.Disponivel);
            _db.AddInParameter(command, "@fabricanteId", DbType.Int32, entidade.Fabricante.FabricanteId);
            _db.AddInParameter(command, "@valorUnitario", DbType.Decimal, entidade.ValorUnitario);
            if (entidade.ValorOferta != null)
                _db.AddInParameter(command, "@valorOferta", DbType.Decimal, entidade.ValorOferta);
            else
                _db.AddInParameter(command, "@valorOferta", DbType.Decimal, null);
            if (entidade.CodigoEAN13 != null)
                _db.AddInParameter(command, "@codigoEAN13", DbType.String, entidade.CodigoEAN13);
            else
                _db.AddInParameter(command, "@codigoEAN13", DbType.String, null);
            if (entidade.CodigoProduto != null)
                _db.AddInParameter(command, "@codigoProduto", DbType.String, entidade.CodigoProduto);
            else
                _db.AddInParameter(command, "@codigoProduto", DbType.String, null);
            _db.AddInParameter(command, "@exibirSite", DbType.Boolean, entidade.ExibirSite);
            _db.AddInParameter(command, "@utilizaFrete", DbType.Boolean, entidade.UtilizaFrete);
            _db.AddInParameter(command, "@peso", DbType.Decimal, entidade.Peso);

            // Executa a query.
            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="produtos"></param>
        public void AtualizarValorOfertaParaNull(List<Produto> produtos)
        {
            StringBuilder sbSQL = new StringBuilder();
            StringBuilder sbSQLWhere = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE Produto SET ");
            sbSQL.Append(" valorOferta = @valorOferta ");

            if (produtos != null && produtos.Count > 0)
            {
                sbSQL.Append(" WHERE produtoId NOT IN ( ");

                for (int i = 0; i < produtos.Count; i++)
                {
                    sbSQLWhere.Append(String.Concat("@produtoId", i, ","));
                }

                sbSQL.Append(sbSQLWhere.ToString(0, sbSQLWhere.Length - 1));
                sbSQL.Append(")");

                command = _db.GetSqlStringCommand(sbSQL.ToString());

                for (int i = 0; i < produtos.Count; i++)
                {
                    _db.AddInParameter(command, "@produtoId" + i, DbType.Int32, produtos[i].ProdutoId);
                }
            }
            else
            {
                sbSQL.Append(" WHERE valorOferta IS NOT NULL ");
                command = _db.GetSqlStringCommand(sbSQL.ToString());
            }
            // Parâmetros
            _db.AddInParameter(command, "@valorOferta", DbType.Decimal, null);

            // Executa a query.
            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="produto"></param>
        public void AtualizarValorOferta(Produto produto)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE Produto SET ");
            sbSQL.Append(" valorOferta = @valorOferta ");
            sbSQL.Append(" WHERE produtoId = @produtoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@valorOferta", DbType.Decimal, produto.ValorOferta);
            _db.AddInParameter(command, "@produtoId", DbType.Int32, produto.ProdutoId);

            // Executa a query.
            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oferta"></param>
        /// <returns></returns>
        public List<Produto> CarregarProdutosPorOferta(Oferta oferta)
        {
            List<Produto> entidadesRetorno = new List<Produto>();

            StringBuilder sbSQL = new StringBuilder();

            if (oferta.OfertaTipo.OfertaTipoId == 1)
            {
                sbSQL.Append("SELECT Produto.* ");
                sbSQL.Append("FROM Produto ");
                sbSQL.Append("INNER JOIN OfertaProduto ");
                sbSQL.Append("    ON Produto.produtoId = OfertaProduto.produtoId ");
                sbSQL.Append("WHERE OfertaProduto.ofertaId = @ofertaId ");
                sbSQL.Append("    AND Produto.exibirSite = 1 ");
                sbSQL.Append("    AND Produto.homologado = 1 ");
            }
            else if (oferta.OfertaTipo.OfertaTipoId == 2)
            {
                sbSQL.Append("SELECT Produto.* ");
                sbSQL.Append("FROM Produto ");
                sbSQL.Append("WHERE Produto.exibirSite = 1 ");
                sbSQL.Append("    AND Produto.homologado = 1 ");
            }
            else if (oferta.OfertaTipo.OfertaTipoId == 3)
            {
                sbSQL.Append("SELECT Produto.* ");
                sbSQL.Append("FROM Produto ");
                sbSQL.Append("INNER JOIN ProdutoCategoria ");
                sbSQL.Append("    ON Produto.produtoId = ProdutoCategoria.produtoId ");
                sbSQL.Append("INNER JOIN OfertaCategoria ");
                sbSQL.Append("    ON ProdutoCategoria.categoriaId = OfertaCategoria.categoriaId ");
                sbSQL.Append("WHERE OfertaCategoria.ofertaId = @ofertaId ");
                sbSQL.Append("    AND Produto.exibirSite = 1 ");
                sbSQL.Append("    AND Produto.homologado = 1 ");
            }

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            if (oferta.OfertaTipo.OfertaTipoId != 2)
            {
                _db.AddInParameter(command, "@ofertaId", DbType.Int32, oferta.OfertaId);
            }

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Produto _produto = new Produto();
                PopulaProduto(reader, _produto);
                entidadesRetorno.Add(_produto);
            }
            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <param name="oferta"></param>
        /// <returns></returns>
        public List<Produto> CarregarProdutosPorNome(Produto entidade, Oferta oferta)
        {
            List<Produto> entidadesRetorno = new List<Produto>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT produtoId, ");
            sbSQL.Append("    nomeProduto ");
            sbSQL.Append("FROM Produto P ");
            sbSQL.Append("WHERE homologado = 1 AND exibirSite = 1 AND nomeProduto like @nomeProduto ");
            sbSQL.Append("    AND NOT produtoId IN ( ");
            sbSQL.Append("        SELECT produtoId ");
            sbSQL.Append("        FROM OfertaProduto ");
            sbSQL.Append("        WHERE ofertaId = @ofertaId ");
            sbSQL.Append("    ) ");
            sbSQL.Append("ORDER BY nomeProduto ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@nomeProduto", DbType.String, "%" + entidade.NomeProduto + "%");
            _db.AddInParameter(command, "@ofertaId", DbType.Int32, oferta.OfertaId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Produto produto = new Produto();
                produto.ProdutoId = Convert.ToInt32(reader["produtoId"].ToString());
                produto.NomeProduto = reader["NomeProduto"].ToString();

                entidadesRetorno.Add(produto);
            }
            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="autorId"></param>
        /// <returns></returns>
        public List<TituloVH> CarregarUltimosProdutosPublicadosPorAutor(int autorId)
        {
            List<TituloVH> entidadesRetorno = new List<TituloVH>();

            StringBuilder sbSql = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();
            StringBuilder sbOrder = new StringBuilder();

            sbSql.Append("WITH    Categorias ( categoriaId, nomeCategoria, categoriaIdPai, Nivel )  ");
            sbSql.Append("          AS ( SELECT   C.categoriaId ,  ");
            sbSql.Append("                        C.nomeCategoria ,  ");
            sbSql.Append("                        C.categoriaIdPai ,  ");
            sbSql.Append("                        0 AS Nivel  ");
            sbSql.Append("               FROM     Categoria AS C  ");
            sbSql.Append("               WHERE    EXISTS ( SELECT UI.categoriaId  ");
            sbSql.Append("                                 FROM   dbo.UsuarioInteresse UI  ");
            sbSql.Append("                                 WHERE  C.categoriaId = UI.categoriaId )  ");
            //sbSql.Append("               UNION ALL  ");
            //sbSql.Append("               SELECT   C.categoriaId ,  ");
            //sbSql.Append("                        C.nomeCategoria ,  ");
            //sbSql.Append("                        C.categoriaIdPai ,  ");
            //sbSql.Append("                        Nivel + 1  ");
            //sbSql.Append("               FROM     Categoria AS C  ");
            //sbSql.Append("                        INNER JOIN Categorias AS CS ON c.CategoriaIdPai = CS.categoriaId  ");
            sbSql.Append("             ),  ");
            sbSql.Append("        Produtos ( produtoId, tituloId, tipo )  ");
            sbSql.Append("          AS ( SELECT   P.tituloImpressoId ,  ");
            sbSql.Append("                        p.tituloId,  ");
            sbSql.Append("                        'Livro Impresso'  ");
            sbSql.Append("               FROM     dbo.TituloImpresso P  ");
            sbSql.Append("                        INNER JOIN dbo.Produto PO ON P.tituloImpressoId = PO.produtoId  ");
            sbSql.Append("                                           AND PO.homologado = 1 AND PO.exibirSite = 1 ");
            sbSql.Append("               UNION ALL  ");
            sbSql.Append("               SELECT   P.tituloEletronicoId ,  ");
            sbSql.Append("                        p.tituloId,  ");
            sbSql.Append("                        'eBook'  ");
            sbSql.Append("               FROM     dbo.TituloEletronico P  ");
            sbSql.Append("                        INNER JOIN dbo.Produto PO ON P.tituloEletronicoId = PO.produtoId  ");
            sbSql.Append("                                           AND PO.homologado = 1 AND PO.exibirSite = 1 ");
            sbSql.Append("             )  ");
            sbSql.Append("SELECT P.*, PIA.arquivoId AS arquivoIdCapa ,  ");
            sbSql.Append("            PIA.nomeArquivo AS nomeArquivoCapa ,  ");
            sbSql.Append("            PIA.nomeArquivoOriginal AS nomeArquivoOriginalCapa ,  ");
            sbSql.Append("            PIA.dataHoraUpload AS dataHoraUploadCapa ,  ");
            sbSql.Append("            PIA.tamanhoArquivo AS tamanhoArquivoCapa   ");
            sbSql.Append("FROM (   ");
            sbSql.Append("SELECT * FROM (        ");
            sbSql.Append("    SELECT  P.*,   ");
            sbSql.Append("            PS.tituloId,  ");
            sbSql.Append("            PS.tipo,  ");
            sbSql.Append("            T.dataLancamento,  ");
            sbSql.Append("            T.nomeTitulo,  ");
            sbSql.Append("            (SELECT    A.nomeAutor + '; ' AS [text()] ");
            sbSql.Append("                  FROM      Autor A ");
            sbSql.Append("                  INNER JOIN dbo.TituloAutor TA ON TA.autorId = A.autorId ");
            sbSql.Append("                                              AND TA.tituloId = PS.tituloId ");
            sbSql.Append("                  ORDER BY  A.nomeAutor ");
            sbSql.Append("                  FOR ");
            sbSql.Append("                  XML PATH('') ");
            sbSql.Append("             ) AS Autores, ");
            sbSql.Append("            PC.categoriaId, ");
            sbSql.Append("            CASE WHEN P.valorOferta IS NOT NULL THEN ( SELECT ISNULL(MAX(MPF.numeroParcelas),0) FROM dbo.MeioPagamentoFaixa MPF INNER JOIN MeioPagamento MP ON MP.meioPagamentoId = MPF.meioPagamentoId WHERE MP.ativo = 1 AND valorMinimo <= P.valorOferta) ");
            sbSql.Append("	            ELSE ( SELECT ISNULL(MAX(MPF.numeroParcelas),0) FROM dbo.MeioPagamentoFaixa MPF INNER JOIN MeioPagamento MP ON MP.meioPagamentoId = MPF.meioPagamentoId WHERE MP.ativo = 1 AND valorMinimo <= P.valorUnitario) ");
            sbSql.Append("	            END Parcelas, ");
            sbSql.Append("            (SELECT ISNULL(MAX(taxaJuros),0) FROM dbo.MeioPagamentoFaixa) taxaJuros, ");
            sbSql.Append("            dbo.AreaDeConhecimentoDaCategoria(pc.categoriaId) AS identificadorArea, ");
            sbSql.Append("            ROW_NUMBER() OVER (ORDER BY P.nomeProduto) AS R  ");
            sbSql.Append("    FROM    Produtos PS  ");
            sbSql.Append("            INNER JOIN dbo.ProdutoCategoria PC ON pc.produtoId = PS.produtoId  ");
            sbSql.Append("            INNER JOIN Categorias CS ON CS.categoriaId = PC.categoriaId ");
            sbSql.Append("            INNER JOIN dbo.Produto P ON P.produtoId = PS.produtoId  ");
            sbSql.Append("            INNER JOIN dbo.Titulo T ON PS.tituloId = T.tituloId  ");
            sbSql.Append("            INNER JOIN dbo.TituloAutor TA ON T.tituloId = TA.tituloId AND TA.autorId = @autorID ");
            sbSql.Append("    WHERE TA.autorId = @autorID AND P.homologado = 1 AND P.exibirSite = 1 ");
            sbSql.Append(" ) AS Q  ");

            sbSql.Append(") AS P   ");
            sbSql.Append("LEFT  JOIN dbo.ProdutoImagem PI ON PI.produtoId = P.produtoId AND pi.produtoImagemTipoId = 1   ");
            sbSql.Append("LEFT  JOIN dbo.Arquivo PIA ON PI.arquivoId = PIA.arquivoId ");
            sbSql.Append("ORDER BY dataLancamento DESC ");

            DbCommand command = _db.GetSqlStringCommand(sbSql.ToString());
            _db.AddInParameter(command, "@autorID", DbType.Int32, autorId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                TituloVH entidadeRetornoAnterior = new TituloVH();
                PopulaTituloVHCompleto(reader, entidadeRetornoAnterior);
                entidadesRetorno.Add(entidadeRetornoAnterior);

            }
            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entidade"></param>
        public static void PopulaTituloVHCompleto(IDataReader reader, TituloVH entidade)
        {
            if (reader["tituloId"] != DBNull.Value)
            {
                entidade.TituloId = Convert.ToInt32(reader["tituloId"].ToString());
            }

            if (reader["produtoId"] != DBNull.Value)
            {
                entidade.ProdutoId = Convert.ToInt32(reader["produtoId"].ToString());
            }

            if (reader["categoriaId"] != DBNull.Value)
            {
                entidade.CategoriaId = Convert.ToInt32(reader["categoriaId"].ToString());
            }

            if (reader["identificadorArea"] != DBNull.Value)
            {
                entidade.AreaId = Convert.ToInt32(reader["identificadorArea"].ToString());
            }

            if (reader["nomeTitulo"] != DBNull.Value)
                entidade.NomeTitulo = reader["nomeTitulo"].ToString();

            if (reader["tipo"] != DBNull.Value)
                entidade.Tipo = reader["tipo"].ToString();

            if (reader["autores"] != DBNull.Value)
                entidade.Autores = reader["autores"].ToString();

            if (reader["arquivoIdCapa"] != DBNull.Value)
            {
                if (entidade.Arquivo == null) entidade.Arquivo = new Arquivo();
                entidade.Arquivo.ArquivoId = Convert.ToInt32(reader["arquivoIdCapa"].ToString());
            }

            if (reader["nomeArquivoCapa"] != DBNull.Value)
            {
                if (entidade.Arquivo == null) entidade.Arquivo = new Arquivo();
                entidade.Arquivo.NomeArquivo = reader["nomeArquivoCapa"].ToString();
            }

            if (reader["nomeArquivoOriginalCapa"] != DBNull.Value)
            {
                if (entidade.Arquivo == null) entidade.Arquivo = new Arquivo();
                entidade.Arquivo.NomeArquivoOriginal = reader["nomeArquivoOriginalCapa"].ToString();
            }

            if (reader["valorUnitario"] != DBNull.Value)
            {
                entidade.ValorUnitario = Convert.ToDecimal(reader["valorUnitario"].ToString());
                entidade.Valor = Convert.ToDecimal(reader["valorUnitario"].ToString());
            }

            if (reader["valorOferta"] != DBNull.Value)
            {
                entidade.ValorOferta = Convert.ToDecimal(reader["valorOferta"].ToString());
                entidade.Valor = Convert.ToDecimal(reader["valorOferta"].ToString());
            }

            if (reader["parcelas"] != DBNull.Value)
                entidade.Parcelas = Convert.ToInt32(reader["parcelas"].ToString());

            if (reader["taxaJuros"] != DBNull.Value)
                entidade.TaxaJuros = Convert.ToDecimal(reader["taxaJuros"].ToString());

            if (reader["disponivel"] != DBNull.Value)
                entidade.Disponivel = Convert.ToBoolean(reader["disponivel"].ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="produtoBO"></param>
        /// <param name="arquivoBO"></param>
        /// <returns></returns>
        public bool ProdutoArquivoRelacionado(Produto produtoBO, Arquivo arquivoBO)
        {
            bool entidadeRetorno = false;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(@"SELECT
								COUNT(*) AS Total
							FROM 
								ProdutoImagem
							WHERE
                                ProdutoImagem.produtoId = @produtoId
								AND ProdutoImagem.arquivoId = @arquivoId
                            ");
            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@produtoId", DbType.Int32, produtoBO.ProdutoId);
            _db.AddInParameter(command, "@arquivoId", DbType.Int32, arquivoBO.ArquivoId);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="produtoId"></param>
        /// <returns></returns>
        public List<TituloVH> CarregarProdutosSugeridos(Int32 produtoId)
        {
            Produto produtoBO = new Produto() { ProdutoId = produtoId };

            List<Produto> produtoBOList = new List<Produto>();
            produtoBOList.Add(produtoBO);

            return this.CarregarProdutosSugeridos(produtoBOList, 3);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="produtoId"></param>
        /// <returns></returns>
        public List<TituloVH> CarregarProdutosSugeridos(List<Produto> produtoBOList, int quantidade)
        {
            List<TituloVH> entidadesRetorno = new List<TituloVH>();

            StringBuilder sbSql = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();
            StringBuilder sbOrder = new StringBuilder();

            sbSql.Append("WITH Produtos (produtoId, qtd) ");
            sbSql.Append("    AS (SELECT ");

            sbSql.Append(String.Format("    TOP {0} ", quantidade != null && quantidade > 3 ? quantidade : 3 ));

            sbSql.Append("                PedidoItem.produtoId ");
            sbSql.Append("            , count(*) AS Qtd ");
            sbSql.Append("        FROM ");
            sbSql.Append("            PedidoItem ");
            sbSql.Append("        INNER JOIN Produto ");
            sbSql.Append("            ON PedidoItem.produtoId = Produto.produtoId ");
            sbSql.Append("        WHERE ");
            sbSql.Append("            PedidoItem.pedidoId IN ( ");
            sbSql.Append("			            SELECT ");
            sbSql.Append("				            pedidoItem.pedidoId ");
            sbSql.Append("			            FROM ");
            sbSql.Append("				            PedidoItem ");
            sbSql.Append("			            INNER JOIN Pedido ON pedidoItem.pedidoId = pedido.pedidoId ");
            sbSql.Append("			            WHERE ");
            sbSql.Append("				            produtoId IN ( 0");

            foreach (Produto produtoBOTemp in produtoBOList)
            {
                sbSql.Append(String.Format("				    ,{0} ", produtoBOTemp.ProdutoId));
            }

            sbSql.Append("				                        ) ");
            sbSql.Append("			            ) ");
            sbSql.Append("            AND Produto.homologado = 1 ");
            sbSql.Append("            AND Produto.exibirSite = 1 ");
            sbSql.Append("            AND PedidoItem.produtoId NOT IN ( 0 ");

            foreach (Produto produtoBOTemp in produtoBOList)
            {
                sbSql.Append(String.Format("				    ,{0} ", produtoBOTemp.ProdutoId));
            }

            sbSql.Append("                                          ) ");

            sbSql.Append("        GROUP BY PedidoItem.produtoId ");
            sbSql.Append("        HAVING count(*) > 1 ");
            sbSql.Append("        ORDER BY Qtd DESC ");
            sbSql.Append("        ) ");
            sbSql.Append("SELECT ");
            sbSql.Append("    Produto.produtoId, ");
            sbSql.Append("    Produto.valorUnitario, ");
            sbSql.Append("    Produto.valorOferta, ");
            sbSql.Append("    Tipo = CASE WHEN TituloImpresso.tituloId IS NOT NULL THEN 'Livro Impresso' ELSE 'eBook' END, ");
            sbSql.Append("    tituloId = CASE WHEN TituloImpresso.tituloId IS NOT NULL THEN TituloImpresso.tituloId ELSE TituloEletronico.tituloId END, ");
            sbSql.Append("    ProdutoCategoria.categoriaId, ");
            sbSql.Append("    nomeTitulo = Produto.nomeProduto, ");
            sbSql.Append("    Arquivo.nomeArquivo ");
            sbSql.Append("FROM Produtos ");
            sbSql.Append("INNER JOIN Produto ");
            sbSql.Append("    ON Produtos.produtoId = Produto.produtoId ");
            sbSql.Append("INNER JOIN ProdutoCategoria ");
            sbSql.Append("    ON Produto.produtoId = ProdutoCategoria.produtoId ");
            sbSql.Append("LEFT JOIN TituloImpresso ");
            sbSql.Append("    ON Produto.produtoId = TituloImpresso.tituloImpressoId ");
            sbSql.Append("LEFT JOIN TituloEletronico ");
            sbSql.Append("    ON Produto.produtoId = TituloEletronico.tituloEletronicoId ");
            sbSql.Append("LEFT  JOIN dbo.ProdutoImagem ");
            sbSql.Append("    ON Produto.produtoId = ProdutoImagem.produtoId AND ProdutoImagem.produtoImagemTipoId = 1 ");
            sbSql.Append("LEFT JOIN Arquivo ");
            sbSql.Append("    ON ProdutoImagem.arquivoId = Arquivo.arquivoId ");

            DbCommand command = _db.GetSqlStringCommand(sbSql.ToString());
            //_db.AddInParameter(command, "@produtoId", DbType.Int32, produtoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                TituloVH entidadeRetornoAnterior = new TituloVH();
                PopulaProdutosSugeridos(reader, entidadeRetornoAnterior);
                entidadesRetorno.Add(entidadeRetornoAnterior);

            }
            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entidade"></param>
        public static void PopulaProdutosSugeridos(IDataReader reader, TituloVH entidade)
        {
            if (reader["tituloId"] != DBNull.Value)
            {
                entidade.TituloId = Convert.ToInt32(reader["tituloId"].ToString());
            }

            if (reader["produtoId"] != DBNull.Value)
            {
                entidade.ProdutoId = Convert.ToInt32(reader["produtoId"].ToString());
            }

            if (reader["categoriaId"] != DBNull.Value)
            {
                entidade.CategoriaId = Convert.ToInt32(reader["categoriaId"].ToString());
            }

            if (reader["nomeTitulo"] != DBNull.Value)
            {
                entidade.NomeTitulo = reader["nomeTitulo"].ToString();
            }

            if (reader["tipo"] != DBNull.Value)
            {
                entidade.Tipo = reader["tipo"].ToString();
            }

            if (reader["nomeArquivo"] != DBNull.Value)
            {
                if (entidade.Arquivo == null) entidade.Arquivo = new Arquivo();
                entidade.Arquivo.NomeArquivo = reader["nomeArquivo"].ToString();
            }

            if (reader["valorUnitario"] != DBNull.Value)
            {
                entidade.ValorUnitario = Convert.ToDecimal(reader["valorUnitario"].ToString());
            }

            if (reader["valorOferta"] != DBNull.Value)
            {
                entidade.ValorOferta = Convert.ToDecimal(reader["valorOferta"].ToString());
            }
        }

        /// <summary>
        /// Método que carrega um Produto.
        /// </summary>
        /// <param name="entidade">Produto a ser carregado (somente o identificador é necessário).</param>
        /// <returns>Produto</returns>
        public Produto CarregarDetalheLivro(Produto entidade)
        {

            Produto entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT P.*, ");
            sbSQL.Append("CASE WHEN p.valorOferta IS NOT NULL THEN ( SELECT ISNULL(MAX(MPF.numeroParcelas),0) FROM dbo.MeioPagamentoFaixa MPF INNER JOIN MeioPagamento MP ON MP.meioPagamentoId = MPF.meioPagamentoId WHERE MP.ativo = 1 AND valorMinimo <= P.valorOferta)  ");
            sbSQL.Append("ELSE ( SELECT ISNULL(MAX(MPF.numeroParcelas),0) FROM dbo.MeioPagamentoFaixa MPF INNER JOIN MeioPagamento MP ON MP.meioPagamentoId = MPF.meioPagamentoId WHERE MP.ativo = 1 AND valorMinimo <= P.valorUnitario)  ");
            sbSQL.Append("END Parcelas ");
            sbSQL.Append(", (SELECT ISNULL(MAX(taxaJuros),0) FROM dbo.MeioPagamentoFaixa) taxaJuros ");
            sbSQL.Append("FROM Produto P WHERE P.produtoId=@produtoId ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.ProdutoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Produto();
                PopulaProdutoDetalheLivro(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }

        public void AtualizaHomologado(Produto entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE Produto SET ");
            sbSQL.Append(" homologado=@homologado ");
            sbSQL.Append(" WHERE produtoId=@produtoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.ProdutoId);
            _db.AddInParameter(command, "@homologado", DbType.Boolean, entidade.Homologado);

            _db.ExecuteNonQuery(command);
        }


        public List<Selo> CarregarSelos(Produto entidade)
        {
            var entidadeRetorno = new List<Selo>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM ProdutoSelo WHERE produtoId=@produtoId ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.ProdutoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                var selo = new Selo(Convert.ToInt32(reader["seloId"]));
                entidadeRetorno.Add(selo);
            }
            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// Método que retorna popula um Produto baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Produto a ser populado(.</param>
        public static void PopulaProdutoDetalheLivro(IDataReader reader, Produto entidade)
        {
            if (reader["disponivel"] != DBNull.Value)
                entidade.Disponivel = Convert.ToBoolean(reader["disponivel"].ToString());

            if (reader["valorUnitario"] != DBNull.Value)
                entidade.ValorUnitario = Convert.ToDecimal(reader["valorUnitario"].ToString());

            if (reader["valorOferta"] != DBNull.Value)
                entidade.ValorOferta = Convert.ToDecimal(reader["valorOferta"].ToString());

            if (reader["codigoEAN13"] != DBNull.Value)
                entidade.CodigoEAN13 = reader["codigoEAN13"].ToString();

            if (reader["codigoProduto"] != DBNull.Value)
                entidade.CodigoProduto = reader["codigoProduto"].ToString();

            if (reader["exibirSite"] != DBNull.Value)
                entidade.ExibirSite = Convert.ToBoolean(reader["exibirSite"].ToString());

            if (reader["homologado"] != DBNull.Value)
                entidade.Homologado = Convert.ToBoolean(reader["homologado"].ToString());

            if (reader["nomeProduto"] != DBNull.Value)
                entidade.NomeProduto = reader["nomeProduto"].ToString();

            if (reader["utilizaFrete"] != DBNull.Value)
                entidade.UtilizaFrete = Convert.ToBoolean(reader["utilizaFrete"].ToString());

            if (reader["peso"] != DBNull.Value)
                entidade.Peso = Convert.ToDecimal(reader["peso"].ToString());

            if (reader["produtoId"] != DBNull.Value)
            {
                entidade.ProdutoId = Convert.ToInt32(reader["produtoId"].ToString());
            }

            if (reader["produtoTipoId"] != DBNull.Value)
            {
                entidade.ProdutoTipo = new ProdutoTipo();
                entidade.ProdutoTipo.ProdutoTipoId = Convert.ToInt32(reader["produtoTipoId"].ToString());
            }

            if (reader["fabricanteId"] != DBNull.Value)
            {
                entidade.Fabricante = new Fabricante();
                entidade.Fabricante.FabricanteId = Convert.ToInt32(reader["fabricanteId"].ToString());
            }

            if (reader["Parcelas"] != DBNull.Value)
            {
                entidade.Parcelas = Convert.ToInt32(reader["Parcelas"].ToString());
            }

            if (reader["taxaJuros"] != DBNull.Value)
            {
                entidade.TaxaJuros = Convert.ToDecimal(reader["taxaJuros"].ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void ExcluirProdutoCategoria(Produto entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM ProdutoCategoria ");
            sbSQL.Append("WHERE produtoId=@produtoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.ProdutoId);

            _db.ExecuteNonQuery(command);
        }


        /// <summary>
        /// Exclui os relacionamentos de produto com o selo
        /// </summary>
        /// <param name="entidade"></param>
        public void ExcluirProdutoSelo(Produto entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM ProdutoSelo ");
            sbSQL.Append("WHERE produtoId=@produtoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.ProdutoId);

            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public List<Produto> CarregarProdutosPorNome(Produto entidade)
        {
            List<Produto> entidadesRetorno = new List<Produto>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(@"SELECT Produto.produtoId,
                                Produto.nomeProduto,
                                TituloImpresso.isbn13,
                                Titulo.edicao,
                                Titulo.tituloId
                            FROM Produto
                            INNER JOIN TituloImpresso ON TituloImpresso.tituloImpressoId = Produto.produtoId
                            INNER JOIN Titulo ON Titulo.tituloId = TituloImpresso.tituloId
                            WHERE Produto.homologado = 1
                                AND Produto.exibirSite = 1
                                AND Produto.disponivel = 1
                                AND Produto.nomeProduto like @nomeProduto
                            ORDER BY Produto.nomeProduto ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@nomeProduto", DbType.String, "%" + entidade.NomeProduto + "%");

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Produto produto = new Produto();
                PopulaProdutoMinhasSolicitacoes(reader, produto);
                entidadesRetorno.Add(produto);
            }
            reader.Close();

            return entidadesRetorno;
        }

        public static void PopulaProdutoMinhasSolicitacoes(IDataReader reader, Produto entidade)
        {
            if (reader["nomeProduto"] != DBNull.Value)
            {
                entidade.NomeProduto = reader["nomeProduto"].ToString();
            }

            if (reader["produtoId"] != DBNull.Value)
            {
                entidade.ProdutoId = Convert.ToInt32(reader["produtoId"].ToString());
            }

            entidade.TituloImpresso = new TituloImpresso();
            entidade.TituloImpresso.Titulo = new Titulo();

            if (reader["isbn13"] != DBNull.Value)
            {
                entidade.TituloImpresso.Isbn13 = reader["isbn13"].ToString();
            }

            if (reader["edicao"] != DBNull.Value)
            {
                entidade.TituloImpresso.Titulo.Edicao = Convert.ToInt32(reader["edicao"].ToString());
            }

            if (reader["tituloId"] != DBNull.Value)
            {
                entidade.TituloImpresso.Titulo.TituloId = Convert.ToInt32(reader["tituloId"].ToString());
            }
        }

        public List<Produto> Carregar(RevistaPacote entidade)
        {
            List<Produto> entidadeRetorno = new List<Produto>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(" SELECT Produto.* FROM RevistaPacoteBrinde ");
            sbSQL.Append(" INNER JOIN Produto ON Produto.produtoId = RevistaPacoteBrinde.produtoId ");
            sbSQL.Append(" WHERE RevistaPacoteBrinde.revistaPacoteId = @revistaPacoteId ");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaPacoteId", DbType.Int32, entidade.RevistaPacoteId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Produto _produto = new Produto();
                PopulaProduto(reader, _produto);
                entidadeRetorno.Add(_produto);
            }

            reader.Close();

            return entidadeRetorno;
        }
    }
}