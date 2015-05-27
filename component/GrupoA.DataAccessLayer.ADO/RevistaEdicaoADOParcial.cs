using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using GrupoA.BusinessObject;

namespace GrupoA.DataAccess.ADO
{
    public partial class RevistaEdicaoADO : ADOSuper, IRevistaEdicaoDAL
    {
        #region Métodos

        /// <summary>
        /// Método que Carrega as Ultimas Edicoes Por Area de Interesse
        /// </summary>
        public IEnumerable<RevistaEdicao> CarregarUltimasEdicoesPorAreaInteresse(List<Categoria> categoriasDasAreasDeInteresse, Int32 quantidadeRegistros)
        {
            List<RevistaEdicao> entidadesRetorno = new List<RevistaEdicao>();

            StringBuilder sbSQL = new StringBuilder();

            string idsDasCategorias =
                    categoriasDasAreasDeInteresse.Select(categoria => categoria.CategoriaId.ToString()).Aggregate(
                        (anterior, proximo) => anterior + ", " + proximo);

            sbSQL.AppendFormat(" SELECT TOP {0} ", quantidadeRegistros);
            sbSQL.Append(" RE.revistaEdicaoId,  ");
            sbSQL.Append(" RE.revistaId,  ");
            sbSQL.Append(" RE.periodoPublicacao,  ");
            sbSQL.Append(" RE.numeroEdicao,  ");
            sbSQL.Append(" RE.anoPublicacao,  ");
            sbSQL.Append(" RE.anoEdicao,  ");
            sbSQL.Append(" RE.descricaoEdicao, ");
            sbSQL.Append(" P.*, ");
            sbSQL.Append(" PIMG.produtoImagemId , ");
            sbSQL.Append(" PIMG.arquivoId , ");
            sbSQL.Append(" PIMG.produtoImagemTipoId , ");
            sbSQL.Append(" A.tamanhoArquivo , ");
            sbSQL.Append(" A.dataHoraUpload , ");
            sbSQL.Append(" A.nomeArquivo , ");
            sbSQL.Append(" A.nomeArquivoOriginal , ");
            sbSQL.Append(" R.nomeRevista , ");
            sbSQL.Append(" R.periodicidade , ");
            sbSQL.Append(" R.descricaoRevista , ");
            sbSQL.Append(" R.publicoAlvo , ");
            sbSQL.Append(" R.ISSN   ");
            sbSQL.Append(" FROM dbo.RevistaEdicao RE ");
            sbSQL.Append(" INNER JOIN dbo.Produto P ON P.produtoId = RE.revistaEdicaoId ");
            sbSQL.Append(" LEFT JOIN ProdutoImagem PIMG ON PIMG.produtoImagemTipoId = 1 AND P.produtoId = PIMG.produtoId ");
            sbSQL.Append(" LEFT JOIN dbo.Arquivo A ON A.arquivoId = PIMG.arquivoId ");
            sbSQL.Append(" INNER JOIN dbo.Revista R ON RE.revistaId = R.revistaId ");
            sbSQL.Append(" INNER JOIN dbo.RevistaAreaConhecimento RAC ON RAC.revistaId = R.revistaId ");
            sbSQL.AppendFormat(" WHERE RAC.categoriaId IN ({0}) ", idsDasCategorias);
            sbSQL.Append(" ORDER BY RE.numeroEdicao DESC ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                RevistaEdicao entidadeRetorno = new RevistaEdicao();
                PopulaRevistaEdicao(reader, entidadeRetorno);
                RevistaADO.PopulaRevista(reader, entidadeRetorno.Revista);
                entidadeRetorno.Produto = new Produto();
                ProdutoADO.PopulaProduto(reader, entidadeRetorno.Produto);

                if (reader["arquivoId"] != DBNull.Value)
                {
                    entidadeRetorno.Produto.ProdutoImagens = new List<ProdutoImagem>();
                    entidadeRetorno.Produto.ProdutoImagens.Add(new ProdutoImagem());
                    ProdutoImagemADO.PopulaProdutoImagem(reader, entidadeRetorno.Produto.ProdutoImagens[0]);
                    ArquivoADO.PopulaArquivo(reader, entidadeRetorno.Produto.ProdutoImagens[0].Arquivo);
                }

                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// Método que Carrega Todas as Edicoes Por RevistaId
        /// </summary>
        public IEnumerable<RevistaEdicao> CarregarTodasEdicoesPorRevistaId(int revistaId)
        {
            List<RevistaEdicao> entidadeRetorno = new List<RevistaEdicao>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(" SELECT * FROM RevistaEdicao ");
            sbSQL.Append(" WHERE RevistaEdicao.revistaId = @revistaId ");
            sbSQL.Append(" ORDER BY RevistaEdicao.numeroEdicao ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaId", DbType.Int32, revistaId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                RevistaEdicao revistaEdicao = new RevistaEdicao();
                PopulaRevistaEdicao(reader, revistaEdicao);
                entidadeRetorno.Add(revistaEdicao);
            }
            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// Método que carrega um RevistaEdicao.
        /// </summary>
        /// <param name="entidade">RevistaEdicao a ser carregado (somente o identificador é necessário).</param>
        /// <returns>RevistaEdicao</returns>
        public RevistaEdicao CarregarComDependencia(RevistaEdicao entidade)
        {

            RevistaEdicao entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT RevistaEdicao.*, ");
            sbSQL.Append("       Revista.nomeRevista, ");
            sbSQL.Append("       Arquivo.arquivoId, ");
            sbSQL.Append("       Arquivo.nomeArquivoOriginal, ");
            sbSQL.Append("       Produto.disponivel, ");
            sbSQL.Append("       Produto.homologado ");
            sbSQL.Append("FROM RevistaEdicao ");
            sbSQL.Append("INNER JOIN Revista ON RevistaEdicao.revistaId = Revista.revistaId ");
            sbSQL.Append("INNER JOIN Produto ON Produto.produtoId = RevistaEdicao.revistaEdicaoId ");
            sbSQL.Append("LEFT JOIN ProdutoImagem ON ProdutoImagem.produtoId = Produto.produtoId AND ProdutoImagem.produtoImagemTipoId = 2 ");
            sbSQL.Append("LEFT JOIN Arquivo ON Arquivo.arquivoId = ProdutoImagem.arquivoId ");
            sbSQL.Append("WHERE RevistaEdicao.revistaEdicaoId = @revistaEdicaoId ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaEdicaoId", DbType.Int32, entidade.RevistaEdicaoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new RevistaEdicao();
                PopulaRevistaEdicaoComDependencia(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// Método que retorna popula um RevistaEdicao baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">RevistaEdicao a ser populado(.</param>
        public static void PopulaRevistaEdicaoComDependencia(IDataReader reader, RevistaEdicao entidade)
        {
            if (reader["periodoPublicacao"] != DBNull.Value)
                entidade.PeriodoPublicacao = reader["periodoPublicacao"].ToString();

            if (reader["anoPublicacao"] != DBNull.Value)
                entidade.AnoPublicacao = Convert.ToInt32(reader["anoPublicacao"].ToString());

            if (reader["mesPublicacao"] != DBNull.Value)
                entidade.MesPublicacao = Convert.ToInt32(reader["mesPublicacao"].ToString());

            if (reader["numeroEdicao"] != DBNull.Value)
                entidade.NumeroEdicao = Convert.ToInt32(reader["numeroEdicao"].ToString());

            if (reader["anoEdicao"] != DBNull.Value)
                entidade.AnoEdicao = reader["anoEdicao"].ToString();

            if (reader["tituloEdicao"] != DBNull.Value)
                entidade.TituloEdicao = reader["tituloEdicao"].ToString();

            if (reader["descricaoEdicao"] != DBNull.Value)
                entidade.DescricaoEdicao = reader["descricaoEdicao"].ToString();

            if (reader["ativo"] != DBNull.Value)
                entidade.Ativo = Convert.ToBoolean(reader["ativo"].ToString());

            if (reader["revistaEdicaoId"] != DBNull.Value)
            {
                entidade.RevistaEdicaoId = Convert.ToInt32(reader["revistaEdicaoId"].ToString());
            }

            if (reader["revistaId"] != DBNull.Value)
            {
                if (entidade.Revista == null) entidade.Revista = new Revista();
                entidade.Revista.RevistaId = Convert.ToInt32(reader["revistaId"].ToString());
            }

            if (reader["nomeRevista"] != DBNull.Value)
            {
                if (entidade.Revista == null) entidade.Revista = new Revista();
                entidade.Revista.NomeRevista = reader["nomeRevista"].ToString();
            }

            if (reader["disponivel"] != DBNull.Value)
            {
                if (entidade.Produto == null) entidade.Produto = new Produto();
                entidade.Produto.Disponivel = Convert.ToBoolean(reader["disponivel"].ToString());
            }

            if (reader["homologado"] != DBNull.Value)
            {
                if (entidade.Produto == null) entidade.Produto = new Produto();
                entidade.Produto.Homologado = Convert.ToBoolean(reader["homologado"].ToString());
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

            if (produtoImagens != null && produtoImagens.Count() > 0)
            {
                if (entidade.Produto == null) entidade.Produto = new Produto();
                entidade.Produto.ProdutoImagens = produtoImagens;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public RevistaEdicao CarregarPorRevistaNumEdicao(RevistaEdicao entidade)
        {
            RevistaEdicao entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM RevistaEdicao WHERE revistaId=@revistaId AND numeroEdicao=@numeroEdicao");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaId", DbType.Int32, entidade.Revista.RevistaId);
            _db.AddInParameter(command, "@numeroEdicao", DbType.Int32, entidade.NumeroEdicao);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new RevistaEdicao();
                PopulaRevistaEdicao(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revistaId"></param>
        /// <returns></returns>
        public RevistaEdicao CarregarMaiorEdicaoPorRevistaId(Int32 revistaId)
        {
            RevistaEdicao entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT RevistaEdicao.*, ");
            sbSQL.Append("    Produto.disponivel, ");
            sbSQL.Append("    Produto.homologado, ");
            sbSQL.Append("    Revista.nomeRevista, ");
            sbSQL.Append("    Arquivo.arquivoId, ");
            sbSQL.Append("    Arquivo.nomeArquivoOriginal ");
            sbSQL.Append("FROM RevistaEdicao ");
            sbSQL.Append("INNER JOIN Revista ON Revista.revistaId = RevistaEdicao.revistaId ");
            sbSQL.Append("INNER JOIN Produto ON Produto.produtoId = RevistaEdicao.revistaEdicaoId ");
            sbSQL.Append("LEFT JOIN ProdutoImagem ON ProdutoImagem.produtoId = Produto.produtoId AND ProdutoImagem.produtoImagemTipoId = 1 ");
            sbSQL.Append("LEFT JOIN Arquivo ON Arquivo.arquivoId = ProdutoImagem.arquivoId ");
            sbSQL.Append("WHERE RevistaEdicao.numeroEdicao = (SELECT MAX(RevistaEdicao.numeroEdicao) ");
            sbSQL.Append("                      FROM RevistaEdicao ");
            sbSQL.Append("                    INNER JOIN Produto ON Produto.produtoId = RevistaEdicao.revistaEdicaoId ");
            sbSQL.Append("                      WHERE RevistaEdicao.revistaId = @revistaId ");
            sbSQL.Append("                    AND RevistaEdicao.ativo = 1) ");
            sbSQL.Append("    AND RevistaEdicao.revistaId = @revistaId ");
            sbSQL.Append("    AND RevistaEdicao.ativo = 1 ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaId", DbType.Int32, revistaId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new RevistaEdicao();
                PopulaRevistaEdicaoComDependencia(reader, entidadeRetorno);
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
        /// <param name="revistaEdicaoId"></param>
        /// <returns></returns>
        public List<RevistaEdicao> CarregarTodasEdicoes(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, Int32 revistaId)
        {

            List<RevistaEdicao> entidadesRetorno = new List<RevistaEdicao>();

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
                sbOrder.Append(" ORDER BY revistaEdicaoId");
            }


            if (registrosPagina > 0)
            {
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT RevistaEdicao.*, ");
                sbSQL.Append("       Revista.nomeRevista, ");
                sbSQL.Append("       Arquivo.arquivoId, ");
                sbSQL.Append("       Arquivo.nomeArquivoOriginal, ");
                sbSQL.Append("       Produto.disponivel, ");
                sbSQL.Append("       Produto.homologado, ");
                sbSQL.Append("       ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R ");
                sbSQL.Append("FROM RevistaEdicao ");
                sbSQL.Append("INNER JOIN Revista ON RevistaEdicao.revistaId = Revista.revistaId ");
                sbSQL.Append("INNER JOIN Produto ON Produto.produtoId = RevistaEdicao.revistaEdicaoId ");
                sbSQL.Append("LEFT JOIN ProdutoImagem ON ProdutoImagem.produtoId = Produto.produtoId AND ProdutoImagem.produtoImagemTipoId = 1 ");
                sbSQL.Append("LEFT JOIN Arquivo ON Arquivo.arquivoId = ProdutoImagem.arquivoId ");
                sbSQL.Append(" WHERE RevistaEdicao.revistaId = @revistaId ");
                //sbSQL.Append("    AND Produto.homologado = 1 ");
                //sbSQL.Append("    AND Produto.exibirSite = 1 ");
                sbSQL.Append("    AND RevistaEdicao.ativo = 1 ");
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSQL.Append("SELECT RevistaEdicao.*, ");
                sbSQL.Append("       Revista.nomeRevista, ");
                sbSQL.Append("       Arquivo.arquivoId, ");
                sbSQL.Append("       Arquivo.nomeArquivoOriginal, ");
                sbSQL.Append("       Produto.disponivel, ");
                sbSQL.Append("       Produto.homologado ");
                sbSQL.Append("FROM RevistaEdicao ");
                sbSQL.Append("INNER JOIN Revista ON RevistaEdicao.revistaId = Revista.revistaId ");
                sbSQL.Append("INNER JOIN Produto ON Produto.produtoId = RevistaEdicao.revistaEdicaoId ");
                sbSQL.Append("LEFT JOIN ProdutoImagem ON ProdutoImagem.produtoId = Produto.produtoId AND ProdutoImagem.produtoImagemTipoId = 1 ");
                sbSQL.Append("LEFT JOIN Arquivo ON Arquivo.arquivoId = ProdutoImagem.arquivoId ");
                sbSQL.Append(" WHERE RevistaEdicao.revistaId = @revistaId ");
                //sbSQL.Append("    AND Produto.homologado = 1 ");
                //sbSQL.Append("    AND Produto.exibirSite = 1 ");
                sbSQL.Append("    AND RevistaEdicao.ativo = 1 ");
                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaId", DbType.Int32, revistaId);

            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                RevistaEdicao entidadeRetorno = new RevistaEdicao();
                PopulaRevistaEdicaoComDependencia(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revistaEdicaoId"></param>
        /// <returns></returns>
        public Int32 ContarEdicoesDiferentesDaEdicaoEntrada(Int32 revistaId)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(" SELECT Count(RevistaEdicao.revistaEdicaoId) FROM RevistaEdicao ");
            sbSQL.Append("INNER JOIN Produto ON Produto.produtoId = RevistaEdicao.revistaEdicaoId ");
            sbSQL.Append(" WHERE RevistaEdicao.revistaId = @revistaId ");
            sbSQL.Append("    AND Produto.homologado = 1 ");
            sbSQL.Append("    AND Produto.exibirSite = 1 ");
            sbSQL.Append("    AND RevistaEdicao.ativo = 1 ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaId", DbType.Int32, revistaId);

            // Executa a query.
            int resultado = (int)_db.ExecuteScalar(command);

            return resultado;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revistaEdicaoId"></param>
        /// <returns></returns>
        public RevistaEdicao CarregarEdicaoComProduto(Int32 revistaEdicaoId)
        {
            RevistaEdicao entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Revista.nomeRevista, ");
            sbSQL.Append("    Revista.ISSN, ");
            sbSQL.Append("    RevistaEdicao.*, ");
            sbSQL.Append("    Produto.codigoProduto, ");
            sbSQL.Append("    Produto.nomeProduto, ");
            sbSQL.Append("    Produto.valorUnitario, ");
            sbSQL.Append("    Produto.valorOferta, ");
            sbSQL.Append("    Produto.exibirSite, ");
            sbSQL.Append("    Produto.disponivel, ");
            sbSQL.Append("    Produto.utilizaFrete, ");
            sbSQL.Append("    Produto.peso, ");
            sbSQL.Append("    Produto.homologado ");
            sbSQL.Append("FROM RevistaEdicao ");
            sbSQL.Append("INNER JOIN Revista ON RevistaEdicao.revistaId = Revista.revistaId ");
            sbSQL.Append("INNER JOIN Produto ON RevistaEdicao.revistaEdicaoId = Produto.produtoId ");
            sbSQL.Append("WHERE RevistaEdicao.revistaEdicaoId = @revistaEdicaoId ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaEdicaoId", DbType.Int32, revistaEdicaoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new RevistaEdicao();
                PopulaRevistaEdicaoComProduto(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entidade"></param>
        public static void PopulaRevistaEdicaoComProduto(IDataReader reader, RevistaEdicao entidade)
        {
            if (reader["periodoPublicacao"] != DBNull.Value)
                entidade.PeriodoPublicacao = reader["periodoPublicacao"].ToString();

            if (reader["anoPublicacao"] != DBNull.Value)
                entidade.AnoPublicacao = Convert.ToInt32(reader["anoPublicacao"].ToString());

            if (reader["mesPublicacao"] != DBNull.Value)
                entidade.MesPublicacao = Convert.ToInt32(reader["mesPublicacao"].ToString());

            if (reader["numeroEdicao"] != DBNull.Value)
                entidade.NumeroEdicao = Convert.ToInt32(reader["numeroEdicao"].ToString());

            if (reader["anoEdicao"] != DBNull.Value)
                entidade.AnoEdicao = reader["anoEdicao"].ToString();

            if (reader["numeroPaginas"] != DBNull.Value)
                entidade.NumeroPaginas = Convert.ToInt32(reader["numeroPaginas"].ToString());

            if (reader["tituloEdicao"] != DBNull.Value)
                entidade.TituloEdicao = reader["tituloEdicao"].ToString();

            if (reader["descricaoEdicao"] != DBNull.Value)
                entidade.DescricaoEdicao = reader["descricaoEdicao"].ToString();

            if (reader["ativo"] != DBNull.Value)
                entidade.Ativo = Convert.ToBoolean(reader["ativo"].ToString());

            if (reader["revistaEdicaoId"] != DBNull.Value)
            {
                entidade.RevistaEdicaoId = Convert.ToInt32(reader["revistaEdicaoId"].ToString());
            }

            if (reader["revistaId"] != DBNull.Value)
            {
                if (entidade.Revista == null) entidade.Revista = new Revista();
                entidade.Revista.RevistaId = Convert.ToInt32(reader["revistaId"].ToString());
            }

            if (reader["nomeRevista"] != DBNull.Value)
            {
                if (entidade.Revista == null) entidade.Revista = new Revista();
                entidade.Revista.NomeRevista = reader["nomeRevista"].ToString();
            }

            if (reader["ISSN"] != DBNull.Value)
            {
                if (entidade.Revista == null) entidade.Revista = new Revista();
                entidade.Revista.ISSN = reader["ISSN"].ToString();
            }

            if (reader["nomeProduto"] != DBNull.Value)
            {
                if (entidade.Produto == null) entidade.Produto = new Produto();
                entidade.Produto.CodigoProduto = reader["codigoProduto"].ToString();
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

            if (reader["exibirSite"] != DBNull.Value)
            {
                if (entidade.Produto == null) entidade.Produto = new Produto();
                entidade.Produto.ExibirSite = Convert.ToBoolean(reader["exibirSite"].ToString());
            }

            if (reader["disponivel"] != DBNull.Value)
            {
                if (entidade.Produto == null) entidade.Produto = new Produto();
                entidade.Produto.Disponivel = Convert.ToBoolean(reader["disponivel"].ToString());
            }

            if (reader["utilizaFrete"] != DBNull.Value)
            {
                if (entidade.Produto == null) entidade.Produto = new Produto();
                entidade.Produto.UtilizaFrete = Convert.ToBoolean(reader["utilizaFrete"].ToString());
            }

            if (reader["peso"] != DBNull.Value)
            {
                if (entidade.Produto == null) entidade.Produto = new Produto();
                entidade.Produto.Peso = Convert.ToDecimal(reader["peso"].ToString());
            }

            if (reader["homologado"] != DBNull.Value)
            {
                if (entidade.Produto == null) entidade.Produto = new Produto();
                entidade.Produto.Homologado = Convert.ToBoolean(reader["homologado"].ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Int32 CarregarRevistaIdUltimaEdicaoCadastrada()
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(@"SELECT TOP 1 ISNULL(RevistaEdicao.revistaId,0)
                            FROM RevistaEdicao
                            INNER JOIN Conteudo ON Conteudo.conteudoId = RevistaEdicao.revistaEdicaoId
                            INNER JOIN Produto ON Produto.produtoId = Conteudo.conteudoId
                            WHERE RevistaEdicao.revistaId in (2, 3, 4)
	                            AND RevistaEdicao.ativo = 1
                            ORDER BY Conteudo.dataHoraCadastro DESC ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Executa a query.
            Int32 resultado = Convert.ToInt32(_db.ExecuteScalar(command));

            return resultado;
        }

        #endregion
    }
}