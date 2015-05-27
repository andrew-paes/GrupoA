using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using GrupoA.BusinessObject;
using GrupoA.BusinessObject.ViewHelper;
using GrupoA.FilterHelper;

namespace GrupoA.DataAccess.ADO
{
    public partial class TituloSolicitacaoADO : ADOSuper, ITituloSolicitacaoDAL
    {
        #region Métodos

        /// <summary>
        /// Método que atualiza o status de um TituloSolicitacao.
        /// </summary>
        /// <param name="entidade">TituloSolicitacao contendo o status a serem atualizados.</param>
        public void AtualizarStatus(TituloSolicitacao entidade)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE TituloSolicitacao SET ");
            sbSQL.Append(" tituloSolicitacaoStatusId=@tituloSolicitacaoStatusId ");
            sbSQL.Append(" WHERE tituloSolicitacaoId=@tituloSolicitacaoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@tituloSolicitacaoId", DbType.Int32, entidade.TituloSolicitacaoId);
            _db.AddInParameter(command, "@tituloSolicitacaoStatusId", DbType.Int32, entidade.TituloSolicitacaoStatus.TituloSolicitacaoStatusId);

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que carrega um TituloSolicitacao.
        /// </summary>
        /// <param name="entidade">TituloSolicitacao a ser carregado.</param>
        /// <returns>TituloSolicitacao</returns>
        public TituloSolicitacao CarregarTituloSolicitacaoPorProfessorTitulo(int professorId, int tituloId)
        {
            TituloSolicitacao entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM TituloSolicitacao WHERE professorId = @professorId AND tituloId = @tituloId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@professorId", DbType.Int32, professorId);
            _db.AddInParameter(command, "@tituloId", DbType.Int32, tituloId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new TituloSolicitacao();
                PopulaTituloSolicitacao(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// Método que retorna uma coleção de TituloSolicitacao completa.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos TituloSolicitacao completa.</returns>
        public IEnumerable<TituloSolicitacaoVH> CarregarTodosCompleto(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {
            List<TituloSolicitacaoVH> entidadesRetorno = new List<TituloSolicitacaoVH>();

            StringBuilder sbSQL = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();
            StringBuilder sbOrder = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            // Monta o "OrderBy"
            sbOrder.Append(" ORDER BY dataSolicitacao DESC");


            if (registrosPagina > 0)
            {
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT TituloSolicitacao.tituloSolicitacaoId, TituloSolicitacao.professorId, TituloSolicitacao.tituloId, TituloSolicitacao.tituloSolicitacaoStatusId, ");
                sbSQL.Append("CONVERT(DATETIME, TituloSolicitacao.dataSolicitacao, 103) AS dataSolicitacao, ");
                sbSQL.Append("TituloSolicitacao.justificativaProfessor, TituloSolicitacao.exportada, ");
                sbSQL.Append("T.nomeTitulo, T.subtituloLivro, T.edicao, ");
                sbSQL.Append("TituloSolicitacaoStatus.statusSolicitacao, ");
                sbSQL.Append("A.nomeArquivo, ");
                sbSQL.Append("TA.tituloAvaliacaoId, ");
                sbSQL.Append("P.produtoId, ");
                sbSQL.Append("PC.categoriaId, ");
                sbSQL.Append("dbo.AreaDeConhecimentoDaCategoria(PC.categoriaId) AS areaId, ");
                sbSQL.Append("ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM TituloSolicitacao ");
                sbSQL.Append("INNER JOIN TituloSolicitacaoStatus ");
                sbSQL.Append("ON TituloSolicitacao.tituloSolicitacaoStatusId = TituloSolicitacaoStatus.tituloSolicitacaoStatusId ");
                sbSQL.Append("INNER JOIN Titulo T ");
                sbSQL.Append("ON TituloSolicitacao.tituloId = T.tituloId ");
                sbSQL.Append("INNER JOIN TituloImpresso TI  ");
                sbSQL.Append("ON T.tituloId = TI.tituloId  ");
                sbSQL.Append("INNER JOIN Produto P  ");
                sbSQL.Append("ON TI.tituloImpressoId = P.produtoId  ");
                sbSQL.Append("INNER JOIN ProdutoCategoria PC ");
                sbSQL.Append("ON P.produtoId = PC.produtoId ");
                sbSQL.Append("LEFT JOIN ProdutoImagem PIm  ");
                sbSQL.Append("ON P.produtoId = PIm.produtoId AND Pim.produtoImagemTipoId = 2 ");
                sbSQL.Append("LEFT JOIN Arquivo A  ");
                sbSQL.Append("ON PIm.arquivoId = A.arquivoId ");
                sbSQL.Append("LEFT JOIN TituloAvaliacao TA ");
                sbSQL.Append("ON  TituloSolicitacao.tituloSolicitacaoId = TA.tituloSolicitacaoId ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSQL.Append("SELECT TituloSolicitacao.tituloSolicitacaoId, TituloSolicitacao.professorId, TituloSolicitacao.tituloId, TituloSolicitacao.tituloSolicitacaoStatusId, ");
                sbSQL.Append("CONVERT(DATETIME, TituloSolicitacao.dataSolicitacao, 103) AS dataSolicitacao, ");
                sbSQL.Append("TituloSolicitacao.justificativaProfessor, TituloSolicitacao.exportada, ");
                sbSQL.Append("T.nomeTitulo, T.subtituloLivro, T.edicao, ");
                sbSQL.Append("TituloSolicitacaoStatus.statusSolicitacao, ");
                sbSQL.Append("A.nomeArquivo ");
                sbSQL.Append("TA.avaliacao, ");
                sbSQL.Append("P.produtoId, ");
                sbSQL.Append("PC.categoriaId, ");
                sbSQL.Append("dbo.AreaDeConhecimentoDaCategoria(PC.categoriaId) AS areaId ");
                sbSQL.Append("FROM TituloSolicitacao ");
                sbSQL.Append("INNER JOIN TituloSolicitacaoStatus ");
                sbSQL.Append("ON TituloSolicitacao.tituloSolicitacaoStatusId = TituloSolicitacaoStatus.tituloSolicitacaoStatusId ");
                sbSQL.Append("INNER JOIN Titulo T ");
                sbSQL.Append("ON TituloSolicitacao.tituloId = T.tituloId ");
                sbSQL.Append("INNER JOIN TituloImpresso TI  ");
                sbSQL.Append("ON T.tituloId = TI.tituloId  ");
                sbSQL.Append("INNER JOIN Produto P  ");
                sbSQL.Append("ON TI.tituloImpressoId = P.produtoId  ");
                sbSQL.Append("INNER JOIN ProdutoCategoria PC ");
                sbSQL.Append("ON P.produtoId = PC.produtoId ");
                sbSQL.Append("LEFT JOIN ProdutoImagem PIm  ");
                sbSQL.Append("ON P.produtoId = PIm.produtoId AND Pim.produtoImagemTipoId = 2 ");
                sbSQL.Append("LEFT JOIN Arquivo A  ");
                sbSQL.Append("ON PIm.arquivoId = A.arquivoId ");
                sbSQL.Append("LEFT JOIN TituloAvaliacao TA ");
                sbSQL.Append("ON  TituloSolicitacao.tituloSolicitacaoId = TA.tituloSolicitacaoId ");

                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                TituloSolicitacaoVH entidadeRetorno = new TituloSolicitacaoVH();
                PopulaTituloSolicitacaoCompleta(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna o total de registros
        /// </summary>
        /// <param name="filtro">Filtro que vai ser aplicado na consulta</param>
        /// <returns>Total de registros</returns>
        public int ContarTodosCompleto(IFilterHelper filtro)
        {
            int retorno = 0;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(tituloSolicitacaoId) Total ");
            sbSQL.Append("FROM TituloSolicitacao ");
            sbSQL.Append("INNER JOIN TituloSolicitacaoStatus ");
            sbSQL.Append("ON TituloSolicitacao.tituloSolicitacaoStatusId = TituloSolicitacaoStatus.tituloSolicitacaoStatusId ");
            sbSQL.Append("INNER JOIN Titulo T ");
            sbSQL.Append("ON TituloSolicitacao.tituloId = T.tituloId ");
            sbSQL.Append("INNER JOIN TituloImpresso TI  ");
            sbSQL.Append("ON T.tituloId = TI.tituloId  ");
            sbSQL.Append("INNER JOIN Produto P  ");
            sbSQL.Append("ON TI.tituloImpressoId = P.produtoId  ");
            sbSQL.Append("LEFT JOIN ProdutoImagem PIm  ");
            sbSQL.Append("ON P.produtoId = PIm.produtoId AND Pim.produtoImagemTipoId = 2 ");
            sbSQL.Append("LEFT JOIN Arquivo A  ");
            sbSQL.Append("ON PIm.arquivoId = A.arquivoId ");
            if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            IDataReader reader = _db.ExecuteReader(command);

            if ((reader.Read()) && ((reader["Total"] != DBNull.Value)))
            {
                retorno = (int)reader["Total"];
            }
            reader.Close();

            return retorno;
        }

        /// <summary>
        /// Método que retorna popula um TituloSolicitacao baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">TituloSolicitacao a ser populado(.</param>
        public static void PopulaTituloSolicitacaoCompleta(IDataReader reader, TituloSolicitacaoVH entidade)
        {
            if (reader["tituloSolicitacaoId"] != DBNull.Value)
                entidade.TituloSolicitacaoId = Convert.ToInt32(reader["tituloSolicitacaoId"].ToString());

            if (reader["dataSolicitacao"] != DBNull.Value)
                entidade.DataSolicitacao = Convert.ToDateTime(reader["dataSolicitacao"].ToString());

            if (reader["justificativaProfessor"] != DBNull.Value)
                entidade.JustificativaProfessor = reader["justificativaProfessor"].ToString();

            if (reader["exportada"] != DBNull.Value)
                entidade.Exportada = Convert.ToBoolean(reader["exportada"].ToString());

            if (reader["professorId"] != DBNull.Value)
            {
                if (entidade.Professor == null) entidade.Professor = new Professor();
                entidade.Professor.ProfessorId = Convert.ToInt32(reader["professorId"].ToString());
            }

            if (reader["tituloId"] != DBNull.Value)
            {
                if (entidade.Titulo == null) entidade.Titulo = new Titulo();
                entidade.Titulo.TituloId = Convert.ToInt32(reader["tituloId"].ToString());

                entidade.Titulo.TituloAutores = new List<TituloAutor>();
                entidade.Titulo.TituloAutores = new TituloAutorADO().CarregarComDependencias(entidade.Titulo);
            }

            if (reader["nomeTitulo"] != DBNull.Value)
            {
                if (entidade.Titulo == null) entidade.Titulo = new Titulo();
                entidade.Titulo.NomeTitulo = reader["nomeTitulo"].ToString();
            }

            if (reader["subtituloLivro"] != DBNull.Value)
            {
                if (entidade.Titulo == null) entidade.Titulo = new Titulo();
                entidade.Titulo.SubtituloLivro = reader["subtituloLivro"].ToString();
            }

            if (reader["edicao"] != DBNull.Value)
            {
                if (entidade.Titulo == null) entidade.Titulo = new Titulo();
                entidade.Titulo.Edicao = Convert.ToInt32(reader["edicao"].ToString());
            }

            if (reader["tituloSolicitacaoStatusId"] != DBNull.Value)
            {
                if (entidade.TituloSolicitacaoStatus == null) entidade.TituloSolicitacaoStatus = new TituloSolicitacaoStatus();
                entidade.TituloSolicitacaoStatus.TituloSolicitacaoStatusId = Convert.ToInt32(reader["tituloSolicitacaoStatusId"].ToString());
            }

            if (reader["statusSolicitacao"] != DBNull.Value)
            {
                if (entidade.TituloSolicitacaoStatus == null) entidade.TituloSolicitacaoStatus = new TituloSolicitacaoStatus();
                entidade.TituloSolicitacaoStatus.StatusSolicitacao = reader["statusSolicitacao"].ToString();
            }

            if (reader["nomeArquivo"] != DBNull.Value)
            {
                if (entidade.Titulo == null) entidade.Titulo = new Titulo();
                if (entidade.Titulo.TituloImpresso == null) entidade.Titulo.TituloImpresso = new TituloImpresso();
                if (entidade.Titulo.TituloImpresso.Produto == null) entidade.Titulo.TituloImpresso.Produto = new Produto();
                if (entidade.Titulo.TituloImpresso.Produto.ProdutoImagens == null) entidade.Titulo.TituloImpresso.Produto.ProdutoImagens = new List<ProdutoImagem>();

                ProdutoImagem produtoImagem = new ProdutoImagem();
                if (produtoImagem.Arquivo == null) produtoImagem.Arquivo = new Arquivo();
                produtoImagem.Arquivo.NomeArquivo = reader["nomeArquivo"].ToString();
                entidade.Titulo.TituloImpresso.Produto.ProdutoImagens.Add(produtoImagem);
            }

            if (reader["tituloAvaliacaoId"] != DBNull.Value)
            {
                if (entidade.TituloAvaliacoes == null) entidade.TituloAvaliacoes = new List<TituloAvaliacao>();
                TituloAvaliacao tituloAvaliacao = new TituloAvaliacao();
                tituloAvaliacao.TituloAvaliacaoId = Convert.ToInt32(reader["tituloAvaliacaoId"].ToString());
                entidade.TituloAvaliacoes.Add(tituloAvaliacao);
            }

            if (reader["produtoId"] != DBNull.Value)
                entidade.ProdutoId = Convert.ToInt32(reader["produtoId"].ToString());

            if (reader["categoriaId"] != DBNull.Value)
                entidade.CategoriaId = Convert.ToInt32(reader["categoriaId"].ToString());

            if (reader["areaId"] != DBNull.Value)
                entidade.AreaId = Convert.ToInt32(reader["areaId"].ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="professorId"></param>
        /// <returns></returns>
        public Int32 TotalRegistrosPendentesPorProfessor(Int32 professorId)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(@"SELECT COUNT(*) AS Total
                            FROM TituloSolicitacao
                            WHERE professorId = @professorId
	                            AND (tituloSolicitacaoStatusId = 1 
		                            OR (tituloSolicitacaoStatusId = 2
			                            AND NOT EXISTS (SELECT * FROM TituloAvaliacao
							                            WHERE TituloAvaliacao.tituloSolicitacaoId = TituloSolicitacao.tituloSolicitacaoId)
			                            )
		                            )");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@professorId", DbType.Int32, professorId);

            Int32 resultado = (Int32)_db.ExecuteScalar(command);


            return resultado;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="professorId"></param>
        /// <returns></returns>
        public Int32 ContarAvaliacoesPendencias(Int32 professorId)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(@"SELECT COUNT(TituloSolicitacao.tituloSolicitacaoId) AS Total
                            FROM TituloSolicitacao
                            WHERE not exists (SELECT TituloAvaliacao.tituloSolicitacaoId FROM TituloAvaliacao
				                              WHERE TituloSolicitacao.tituloSolicitacaoId = TituloAvaliacao.tituloSolicitacaoId)
	                            AND TituloSolicitacao.tituloSolicitacaoStatusId = 2
	                            AND TituloSolicitacao.professorId = @professorId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@professorId", DbType.Int32, professorId);

            Int32 resultado = (Int32)_db.ExecuteScalar(command);


            return resultado;
        }

        #endregion
    }
}
