using System.Data;
using System.Data.Common;
using System.Text;
using GrupoA.BusinessObject;
using System;
using System.Collections.Generic;

namespace GrupoA.DataAccess.ADO
{
    public partial class RevistaAssinaturaADO : ADOSuper, IRevistaAssinaturaDAL
    {
        /// <summary>
        /// Método que carrega um RevistaAssinatura.
        /// </summary>
        /// <param name="entidade">RevistaAssinatura a ser carregado (somente o identificador é necessário).</param>
        /// <returns>RevistaAssinatura</returns>
        public RevistaAssinatura CarregarPorRevistaNumExemplares(RevistaAssinatura entidade)
        {
            RevistaAssinatura entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM RevistaAssinatura WHERE revistaId = @revistaId AND numeroExemplares = @numeroExemplares");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaId", DbType.Int32, entidade.Revista.RevistaId);
            _db.AddInParameter(command, "@numeroExemplares", DbType.Int32, entidade.NumeroExemplares);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new RevistaAssinatura();
                PopulaRevistaAssinatura(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registrosPagina"></param>
        /// <param name="numeroPagina"></param>
        /// <param name="ordemColunas"></param>
        /// <param name="ordemSentidos"></param>
        /// <param name="revistaId"></param>
        /// <returns></returns>
        public List<RevistaAssinatura> CarregarTodosAssinaturasPorRevistaId(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, Int32 revistaId)
        {
            List<RevistaAssinatura> entidadesRetorno = new List<RevistaAssinatura>();

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
                sbOrder.Append(" ORDER BY revistaAssinaturaId");
            }

            if (registrosPagina > 0)
            {
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT RevistaAssinatura.numeroExemplares, ");
	            sbSQL.Append("       Produto.nomeProduto, ");
	            sbSQL.Append("       Produto.valorUnitario, ");
	            sbSQL.Append("       Produto.valorOferta, ");
	            sbSQL.Append("       Produto.produtoId, ");
                sbSQL.Append("       Produto.disponivel, ");
	            sbSQL.Append("       Revista.descricaoRevista, ");
	            sbSQL.Append("       Arquivo.arquivoId, ");
	            sbSQL.Append("       Arquivo.nomeArquivoOriginal, ");
	            sbSQL.Append("       CASE WHEN Produto.valorOferta IS NOT NULL THEN ( SELECT ISNULL(MAX(MPF.numeroParcelas),0) FROM dbo.MeioPagamentoFaixa MPF INNER JOIN MeioPagamento MP ON MP.meioPagamentoId = MPF.meioPagamentoId WHERE MP.ativo = 1 AND valorMinimo <= Produto.valorOferta) ");
	            sbSQL.Append("       ELSE ( SELECT ISNULL(MAX(MPF.numeroParcelas),0) FROM dbo.MeioPagamentoFaixa MPF INNER JOIN MeioPagamento MP ON MP.meioPagamentoId = MPF.meioPagamentoId WHERE MP.ativo = 1 AND valorMinimo <= Produto.valorUnitario) ");
	            sbSQL.Append("       END Parcelas, ");
	            sbSQL.Append("       (SELECT ISNULL(MAX(taxaJuros),0) FROM dbo.MeioPagamentoFaixa) taxaJuros, ");
                sbSQL.Append("       ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R ");
                sbSQL.Append("FROM RevistaAssinatura ");
                sbSQL.Append("INNER JOIN Produto ON RevistaAssinatura.revistaAssinaturaId = Produto.produtoId ");
                sbSQL.Append("INNER JOIN Revista ON RevistaAssinatura.revistaId = Revista.revistaId ");
                sbSQL.Append("LEFT JOIN ProdutoImagem ON Produto.produtoId = ProdutoImagem.produtoId AND ProdutoImagem.produtoImagemTipoId = 1 ");
                sbSQL.Append("LEFT JOIN Arquivo ON ProdutoImagem.arquivoId = Arquivo.arquivoId ");
                sbSQL.Append("WHERE Revista.revistaId = @revistaId ");
	            sbSQL.Append("      AND Produto.exibirSite = 1 ");
	            sbSQL.Append("      AND Produto.disponivel = 1 ");
                sbSQL.Append("      AND Produto.homologado = 1 ");
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSQL.Append("SELECT RevistaAssinatura.numeroExemplares, ");
                sbSQL.Append("       Produto.nomeProduto, ");
                sbSQL.Append("       Produto.valorUnitario, ");
                sbSQL.Append("       Produto.valorOferta, ");
                sbSQL.Append("       Produto.produtoId, ");
                sbSQL.Append("       Produto.disponivel, ");
                sbSQL.Append("       Revista.descricaoRevista, ");
                sbSQL.Append("       Arquivo.arquivoId, ");
                sbSQL.Append("       Arquivo.nomeArquivoOriginal, ");
                sbSQL.Append("       CASE WHEN Produto.valorOferta IS NOT NULL THEN ( SELECT ISNULL(MAX(MPF.numeroParcelas),0) FROM dbo.MeioPagamentoFaixa MPF INNER JOIN MeioPagamento MP ON MP.meioPagamentoId = MPF.meioPagamentoId WHERE MP.ativo = 1 AND valorMinimo <= Produto.valorOferta) ");
                sbSQL.Append("       ELSE ( SELECT ISNULL(MAX(MPF.numeroParcelas),0) FROM dbo.MeioPagamentoFaixa MPF INNER JOIN MeioPagamento MP ON MP.meioPagamentoId = MPF.meioPagamentoId WHERE MP.ativo = 1 AND valorMinimo <= Produto.valorUnitario) ");
                sbSQL.Append("       END Parcelas, ");
                sbSQL.Append("       (SELECT ISNULL(MAX(taxaJuros),0) FROM dbo.MeioPagamentoFaixa) taxaJuros ");
                sbSQL.Append("FROM RevistaAssinatura ");
                sbSQL.Append("INNER JOIN Produto ON RevistaAssinatura.revistaAssinaturaId = Produto.produtoId ");
                sbSQL.Append("INNER JOIN Revista ON RevistaAssinatura.revistaId = Revista.revistaId ");
                sbSQL.Append("LEFT JOIN ProdutoImagem ON Produto.produtoId = ProdutoImagem.produtoId AND ProdutoImagem.produtoImagemTipoId = 1 ");
                sbSQL.Append("LEFT JOIN Arquivo ON ProdutoImagem.arquivoId = Arquivo.arquivoId ");
                sbSQL.Append("WHERE Revista.revistaId = @revistaId ");
                sbSQL.Append("      AND Produto.exibirSite = 1 ");
                sbSQL.Append("      AND Produto.disponivel = 1 ");
                sbSQL.Append("      AND Produto.homologado = 1 ");
                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaId", DbType.Int32, revistaId);

            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                RevistaAssinatura entidadeRetorno = new RevistaAssinatura();
                PopulaRevistaAssinaturaComDependencia(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
        }

        public static void PopulaRevistaAssinaturaComDependencia(IDataReader reader, RevistaAssinatura entidade)
        {
            if (reader["numeroExemplares"] != DBNull.Value)
            {
                entidade.NumeroExemplares = Convert.ToInt32(reader["numeroExemplares"].ToString());
            }

            if (reader["descricaoRevista"] != DBNull.Value)
            {
                entidade.Revista = new Revista();
                entidade.Revista.DescricaoRevista = reader["descricaoRevista"].ToString();
            }

            if (reader["nomeProduto"] != DBNull.Value)
            {
                if (entidade.Produto == null) entidade.Produto = new Produto();
                entidade.Produto.NomeProduto = reader["nomeProduto"].ToString();
            }

            if (reader["valorUnitario"] != DBNull.Value)
            {
                if (entidade.Produto == null) entidade.Produto = new Produto();
                entidade.Produto.ValorUnitario = Convert.ToDecimal(reader["valorUnitario"].ToString());
            }

            if (reader["valorOferta"] != DBNull.Value)
            {
                if (entidade.Produto == null) entidade.Produto = new Produto();
                entidade.Produto.ValorOferta = Convert.ToDecimal(reader["valorOferta"].ToString());
            }

            if (reader["produtoId"] != DBNull.Value)
            {
                if (entidade.Produto == null) entidade.Produto = new Produto();
                entidade.Produto.ProdutoId = Convert.ToInt32(reader["produtoId"].ToString());
            }

            if (reader["disponivel"] != DBNull.Value)
            {
                if (entidade.Produto == null) entidade.Produto = new Produto();
                entidade.Produto.Disponivel = Convert.ToBoolean(reader["disponivel"].ToString());
            }

            if (reader["Parcelas"] != DBNull.Value)
            {
                if (entidade.Produto == null) entidade.Produto = new Produto();
                entidade.Produto.Parcelas = Convert.ToInt32(reader["Parcelas"].ToString());
            }

            if (reader["taxaJuros"] != DBNull.Value)
            {
                if (entidade.Produto == null) entidade.Produto = new Produto();
                entidade.Produto.TaxaJuros = Convert.ToDecimal(reader["taxaJuros"].ToString());
            }

            List<ProdutoImagem> produtoImagens = null;

            if (reader["arquivoId"] != DBNull.Value)
            {
                produtoImagens = new List<ProdutoImagem>();
                ProdutoImagem produtoImagem = new ProdutoImagem();
                produtoImagem.Arquivo = new Arquivo();
                produtoImagem.Arquivo.ArquivoId = Convert.ToInt32(reader["arquivoId"].ToString());

                if (reader["nomeArquivoOriginal"] != DBNull.Value)
                {
                    produtoImagem.Arquivo.NomeArquivoOriginal = reader["nomeArquivoOriginal"].ToString();
                }

                produtoImagens.Add(produtoImagem);
            }

            if (produtoImagens != null && produtoImagens.Count > 0)
            {
                entidade.Produto.ProdutoImagens = produtoImagens;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revistaId"></param>
        /// <returns></returns>
        public Int32 ContarTodasAssinaturasPorRevistaId(Int32 revistaId)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(Produto.produtoId) ");
            sbSQL.Append("FROM RevistaAssinatura ");
            sbSQL.Append("INNER JOIN Produto ON RevistaAssinatura.revistaAssinaturaId = Produto.produtoId ");
            sbSQL.Append("INNER JOIN Revista ON RevistaAssinatura.revistaId = Revista.revistaId ");
            sbSQL.Append("WHERE Revista.revistaId = @revistaId ");
	        sbSQL.Append("      AND Produto.exibirSite = 1 ");
	        sbSQL.Append("      AND Produto.disponivel = 1 ");
            sbSQL.Append("      AND Produto.homologado = 1 ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaId", DbType.Int32, revistaId);

            int resultado = (int)_db.ExecuteScalar(command);


            return resultado;
        }
    }
}