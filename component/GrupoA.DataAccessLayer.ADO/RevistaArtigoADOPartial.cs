using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using GrupoA.BusinessObject;

namespace GrupoA.DataAccess.ADO
{
    public partial class RevistaArtigoADO : ADOSuper, IRevistaArtigoDAL
    {
        #region Métodos

        #region [ Exlcuir ]

        /// <summary>
        /// Método que Exclui registros da tabela RevistaArtigoGaleriaImagem conforme revistaArtigoId e arquivoId passados
        /// </summary>
        public void ExcluirRevistaArtigoImagem(int revistaArtigoId, int arquivoId, bool todosArquivos)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM RevistaArtigoGaleriaImagem ");
            if (todosArquivos)
            {
                sbSQL.Append("WHERE revistaArtigoId=@revistaArtigoId");
            }
            else
            {
                sbSQL.Append("WHERE revistaArtigoId=@revistaArtigoId and arquivoId=@arquivoId");
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaArtigoId", DbType.Int32, revistaArtigoId);
            _db.AddInParameter(command, "@arquivoId", DbType.Int32, arquivoId);

            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que Exclui Auto-Relacionamentos  na tabela RevistaArtigo
        /// </summary>
        public void ExcluirRelacionamentoRevistaArtigoIdAssociado(int revistaArtigoId)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("UPDATE RevistaArtigo ");
            sbSQL.Append("SET  RevistaArtigoIdAssociado = NULL ");
            sbSQL.Append("WHERE revistaArtigoIdAssociado=@revistaArtigoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaArtigoId", DbType.Int32, revistaArtigoId);

            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que Exclui registros da tabela RevistaArtigoGaleriaImagem conforme arquivoId passado
        /// </summary>
        public void ExcluirRevistaArtigoArquivo(int arquivoId)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;
            sbSQL.Append("DELETE FROM RevistaArtigoLocalizacaoImagem ");
            sbSQL.Append("WHERE arquivoId=@arquivoId ");
            command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@arquivoId", DbType.Int32, arquivoId);
            _db.ExecuteNonQuery(command);

            //new ArquivoADO().Excluir(entidade.Arquivo);

        }

        #endregion

        /// <summary>
        /// Método que persiste um InserirRevistaArtigoGaleriaImagem.
        /// </summary>
        /// <param name="entidade">InserirRevistaArtigoGaleriaImagem contendo os dados a serem persistidos.</param>	
        public void InserirRevistaArtigoGaleriaImagem(RevistaGaleriaArtigoImagem entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO RevistaArtigoGaleriaImagem ");
            sbSQL.Append(" (arquivoId, revistaArtigoId) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@arquivoId, @revistaArtigoId) ");

            //sbSQL.Append(" ; SET @clippingImagemId = SCOPE_IDENTITY(); ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.Arquivo.ArquivoId);

            _db.AddInParameter(command, "@revistaArtigoId", DbType.Int32, entidade.RevistaArtigoId);

            // Executa a query.
            _db.ExecuteNonQuery(command);

            //entidade.RevistaArtigoId.ClippingImagemId = Convert.ToInt32(_db.GetParameterValue(command, "@clippingImagemId"));

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revistaEdicaoId"></param>
        /// <param name="revistaArtigoId"></param>
        /// <returns></returns>
        public Int32 ValidarArtigoPrincipalCapa(Int32 revistaEdicaoId, Int32 revistaArtigoId)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(RevistaArtigo.revistaArtigoId) as Total ");
            sbSQL.Append("FROM RevistaArtigo ");
            sbSQL.Append("WHERE RevistaArtigo.revistaEdicaoId = @revistaEdicaoId ");
            sbSQL.Append("    AND RevistaArtigo.revistaArtigoId <> @revistaArtigoId ");
            sbSQL.Append("    AND RevistaArtigo.destaquePrincipal = 1 ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaEdicaoId", DbType.Int32, revistaEdicaoId);
            _db.AddInParameter(command, "@revistaArtigoId", DbType.Int32, revistaArtigoId);

            Int32 resultado = (Int32)_db.ExecuteScalar(command);

            return resultado;
        }

        #region [ Carregar ]

        /// <summary>
        /// Método que Carrega Todos Artigos Por RevistaEdicaoId
        /// </summary>
        public IEnumerable<RevistaArtigo> CarregarTodosArtigosPorRevistaEdicaoId(Int32 revistaEdicaoId)
        {
            List<RevistaArtigo> entidadeRetorno = new List<RevistaArtigo>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(" SELECT * FROM RevistaArtigo ");
            sbSQL.Append(" WHERE RevistaArtigo.revistaEdicaoId = @revistaEdicaoId AND ativo = 1");
            sbSQL.Append(" ORDER BY RevistaArtigo.tituloArtigo ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaEdicaoId", DbType.Int32, revistaEdicaoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                RevistaArtigo revistaArtigo = new RevistaArtigo();
                PopulaRevistaArtigo(reader, revistaArtigo);
                entidadeRetorno.Add(revistaArtigo);
            }
            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revistaEdicaoId"></param>
        /// <param name="qtdRegistros"></param>
        /// <param name="destaque"></param>
        /// <param name="revistaSecaoId"></param>
        /// <returns></returns>
        public List<RevistaArtigo> CarregarArtigosPorRevistaEdicaoId(Int32 registrosPagina, Int32 numeroPagina, Int32 revistaEdicaoId, Int32 qtdRegistros, Boolean destaque, Int32? revistaSecaoId)
        {
            List<RevistaArtigo> entidadeRetorno = new List<RevistaArtigo>();

            StringBuilder sbSQL = new StringBuilder();

            if (registrosPagina > 0)
            {
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT RevistaArtigo.*, ");
                sbSQL.Append("    RevistaSecao.nomeSecao, ");
                sbSQL.Append("    RevistaSecao.revistaId, ");
                sbSQL.Append("    RevistaArtigoPermissao.permissao, ");
                sbSQL.Append("    Arquivo.nomeArquivo, ");
                sbSQL.Append("    ROW_NUMBER() OVER ( ORDER BY RevistaSecao.nomeSecao, RevistaArtigo.tituloArtigo ) R ");
                sbSQL.Append("FROM RevistaArtigo ");
                sbSQL.Append("INNER JOIN RevistaSecao ON RevistaArtigo.revistaSecaoId = RevistaSecao.revistaSecaoId ");
                sbSQL.Append("INNER JOIN RevistaArtigoPermissao ON RevistaArtigo.revistaArtigoPermissaoId = RevistaArtigoPermissao.revistaArtigoPermissaoId ");
                sbSQL.Append("LEFT JOIN Arquivo ON RevistaArtigo.arquivoIdThumbP = Arquivo.arquivoId ");
                sbSQL.Append("WHERE RevistaArtigo.revistaEdicaoId = @revistaEdicaoId ");
                sbSQL.Append("    AND ativo = 1 ");
                if (destaque)
                {
                    sbSQL.Append("  AND RevistaArtigo.destaqueHome = 1 ");
                }
                if (revistaSecaoId != null)
                {
                    sbSQL.Append("  AND RevistaArtigo.revistaSecaoId = @revistaSecaoId ");
                }
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());
            }
            else
            {
                sbSQL.Append("SELECT ");
                if (qtdRegistros > 0)
                {
                    sbSQL.Append(String.Format("    TOP {0} ", qtdRegistros));
                }
                sbSQL.Append("    RevistaArtigo.*, ");
                sbSQL.Append("    RevistaSecao.nomeSecao, ");
                sbSQL.Append("    RevistaSecao.revistaId, ");
                sbSQL.Append("    RevistaArtigoPermissao.permissao, ");
                sbSQL.Append("    Arquivo.nomeArquivo ");
                sbSQL.Append("FROM RevistaArtigo ");
                sbSQL.Append("INNER JOIN RevistaSecao ON RevistaArtigo.revistaSecaoId = RevistaSecao.revistaSecaoId ");
                sbSQL.Append("INNER JOIN RevistaArtigoPermissao ON RevistaArtigo.revistaArtigoPermissaoId = RevistaArtigoPermissao.revistaArtigoPermissaoId ");
                sbSQL.Append("LEFT JOIN Arquivo ON RevistaArtigo.arquivoIdThumbP = Arquivo.arquivoId ");
                sbSQL.Append("WHERE RevistaArtigo.revistaEdicaoId = @revistaEdicaoId ");
                sbSQL.Append("    AND RevistaArtigo.destaquePrincipal = 0 AND ativo = 1 ");
                if (destaque)
                {
                    sbSQL.Append("  AND RevistaArtigo.destaqueHome = 1 ");
                }
                if (revistaSecaoId != null)
                {
                    sbSQL.Append("  AND RevistaArtigo.revistaSecaoId = @revistaSecaoId ");
                }
                if (qtdRegistros > 0)
                {
                    sbSQL.Append("ORDER BY NEWID() ");
                }
                else
                {
                    sbSQL.Append("ORDER BY RevistaSecao.nomeSecao, RevistaArtigo.tituloArtigo ");
                }
            }

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaEdicaoId", DbType.Int32, revistaEdicaoId);
            if (revistaSecaoId != null)
            {
                _db.AddInParameter(command, "@revistaSecaoId", DbType.Int32, revistaSecaoId.Value);
            }

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                RevistaArtigo revistaArtigo = new RevistaArtigo();
                PopulaRevistaArtigoComDependencia(reader, revistaArtigo);
                PopulaRevistaArtigoArquivoThumbP(reader, revistaArtigo);
                entidadeRetorno.Add(revistaArtigo);
            }
            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// Método que carrega todas imagens de RevistaArtigoGaleriaImagem conforme revistaArtigoId passado
        /// </summary>
        public IEnumerable<RevistaGaleriaArtigoImagem> CarregarTodasRevistaGaleriaArtigoImagens(int revistaArtigoId)
        {
            return CarregarTodasImagens(revistaArtigoId);
        }

        /// <summary>
        /// Método que carrega todas imagens de RevistaArtigoGaleriaImagem conforme revistaArtigoId passado
        /// </summary>
        public IEnumerable<RevistaGaleriaArtigoImagem> CarregarTodasImagens(int revistaArtigoId)
        {

            List<RevistaGaleriaArtigoImagem> entidadesRetorno = new List<RevistaGaleriaArtigoImagem>();

            StringBuilder sbSQL = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();
            StringBuilder sbOrder = new StringBuilder();

            sbSQL.Append("SELECT * FROM RevistaArtigoGaleriaImagem ");
            sbSQL.Append("WHERE RevistaArtigoGaleriaImagem.revistaArtigoId = @revistaArtigoId ");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaArtigoId", DbType.Int32, revistaArtigoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                RevistaGaleriaArtigoImagem entidadeRetorno = new RevistaGaleriaArtigoImagem();
                PopulaRevistaArtigoGaleriaImagem(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public RevistaArtigo CarregarCompleto(Int32 revistaArtigoId)
        {
            RevistaArtigo entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("SELECT RevistaArtigo.*,  ");
            sbSQL.Append("    RevistaSecao.nomeSecao, ");
            sbSQL.Append("    RevistaSecao.revistaId, ");
            sbSQL.Append("    RevistaArtigoPermissao.permissao, ");
            sbSQL.Append("    Arquivo.nomeArquivo, ");
            sbSQL.Append("    ArquivoLateral.nomeArquivo AS nomeArquivoLateral  ");
            sbSQL.Append("FROM RevistaArtigo ");
            sbSQL.Append("INNER JOIN RevistaSecao ON RevistaArtigo.revistaSecaoId = RevistaSecao.revistaSecaoId ");
            sbSQL.Append("INNER JOIN RevistaArtigoPermissao ON RevistaArtigo.revistaArtigoPermissaoId = RevistaArtigoPermissao.revistaArtigoPermissaoId ");
            sbSQL.Append("LEFT JOIN Arquivo ON RevistaArtigo.arquivoIdThumbM = Arquivo.arquivoId ");
            sbSQL.Append("LEFT JOIN Arquivo AS ArquivoLateral ON RevistaArtigo.arquivoIdLateral = ArquivoLateral.arquivoId ");
            sbSQL.Append("WHERE RevistaArtigo.revistaArtigoId = @revistaArtigoId ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaArtigoId", DbType.Int32, revistaArtigoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new RevistaArtigo();
                PopulaRevistaArtigoComDependencia(reader, entidadeRetorno);
                PopulaRevistaArtigoArquivoThumbM(reader, entidadeRetorno);
                PopulaRevistaArtigoArquivoLateral(reader, entidadeRetorno);
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
        public List<RevistaArtigo> CarregarTodosArtigosSelecionados(Int32 registrosPagina, Int32 numeroPagina, String[] ordemColunas, String[] ordemSentidos, Int32 revistaId)
        {
            List<RevistaArtigo> entidadesRetorno = new List<RevistaArtigo>();

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
                sbOrder.Append(" ORDER BY RevistaArtigo.tituloArtigo");
            }

            if (registrosPagina > 0)
            {
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT RevistaArtigo.*, ");
                sbSQL.Append("       Arquivo.nomeArquivo, ");
                sbSQL.Append("       RevistaSecao.nomeSecao, ");
                sbSQL.Append("       RevistaSecao.revistaId, ");
                sbSQL.Append("       RevistaArtigoPermissao.permissao, ");
                sbSQL.Append("       ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R ");
                sbSQL.Append("FROM RevistaArtigo ");
                sbSQL.Append("INNER JOIN RevistaSecao ON RevistaArtigo.revistaSecaoId = RevistaSecao.revistaSecaoId ");
                sbSQL.Append("INNER JOIN RevistaArtigoPermissao ON RevistaArtigo.revistaArtigoPermissaoId = RevistaArtigoPermissao.revistaArtigoPermissaoId ");
                sbSQL.Append("LEFT JOIN Arquivo ON RevistaArtigo.arquivoIdThumbP = Arquivo.arquivoId ");
                sbSQL.Append("WHERE RevistaSecao.revistaId = @revistaId ");
                sbSQL.Append("    AND RevistaArtigo.revistaEdicaoId IS NULL AND ativo = 1 ");
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());
            }
            else
            {
                sbSQL.Append("SELECT RevistaArtigo.*, ");
                sbSQL.Append("       Arquivo.nomeArquivoOriginal, ");
                sbSQL.Append("       RevistaSecao.nomeSecao, ");
                sbSQL.Append("       RevistaSecao.revistaId, ");
                sbSQL.Append("       RevistaArtigoPermissao.permissao ");
                sbSQL.Append("FROM RevistaArtigo ");
                sbSQL.Append("INNER JOIN RevistaSecao ON RevistaArtigo.revistaSecaoId = RevistaSecao.revistaSecaoId ");
                sbSQL.Append("INNER JOIN RevistaArtigoPermissao ON RevistaArtigo.revistaArtigoPermissaoId = RevistaArtigoPermissao.revistaArtigoPermissaoId ");
                sbSQL.Append("LEFT JOIN Arquivo ON RevistaArtigo.arquivoIdThumbP = Arquivo.arquivoId ");
                sbSQL.Append("WHERE RevistaSecao.revistaId = @revistaId ");
                sbSQL.Append("    AND RevistaArtigo.revistaEdicaoId IS NULL AND ativo = 1 ");
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaId", DbType.Int32, revistaId);

            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                RevistaArtigo entidadeRetorno = new RevistaArtigo();
                PopulaRevistaArtigoComDependencia(reader, entidadeRetorno);
                PopulaRevistaArtigoArquivoThumbP(reader, entidadeRetorno);
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
        public List<RevistaArtigo> CarregarArtigosSemDestaquePorRevistaEdicaoId(Int32 revistaEdicaoId)
        {
            List<RevistaArtigo> entidadeRetorno = new List<RevistaArtigo>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT TOP 9 ");
            sbSQL.Append("    RevistaArtigo.*, ");
            sbSQL.Append("    RevistaSecao.nomeSecao, ");
            sbSQL.Append("    RevistaSecao.revistaId, ");
            sbSQL.Append("    RevistaArtigoPermissao.permissao, ");
            sbSQL.Append("    Arquivo.nomeArquivo ");
            sbSQL.Append("FROM RevistaArtigo ");
            sbSQL.Append("INNER JOIN Conteudo ON RevistaArtigo.revistaArtigoId = Conteudo.conteudoId ");
            sbSQL.Append("INNER JOIN RevistaSecao ON RevistaArtigo.revistaSecaoId = RevistaSecao.revistaSecaoId ");
            sbSQL.Append("INNER JOIN RevistaArtigoPermissao ON RevistaArtigo.revistaArtigoPermissaoId = RevistaArtigoPermissao.revistaArtigoPermissaoId ");
            sbSQL.Append("LEFT JOIN Arquivo ON RevistaArtigo.arquivoIdThumbP = Arquivo.arquivoId ");
            sbSQL.Append("WHERE RevistaArtigo.revistaEdicaoId = @revistaEdicaoId ");
            sbSQL.Append("    AND RevistaArtigo.destaquePrincipal = 0 ");
            sbSQL.Append("    AND RevistaArtigo.destaqueHome = 0 AND ativo = 1 ");
            sbSQL.Append("ORDER BY RevistaArtigo.dataPublicacao DESC");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaEdicaoId", DbType.Int32, revistaEdicaoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                RevistaArtigo revistaArtigo = new RevistaArtigo();
                PopulaRevistaArtigoComDependencia(reader, revistaArtigo);
                PopulaRevistaArtigoArquivoThumbP(reader, revistaArtigo);
                entidadeRetorno.Add(revistaArtigo);
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
        /// <param name="revistaEdicaoId"></param>
        /// <param name="revistaId"></param>
        /// <returns></returns>
        public List<RevistaArtigo> CarregarArtigosConteudoOnlinePorRevistaEdicaoId(Int32 registrosPagina, Int32 numeroPagina, String[] ordemColunas, String[] ordemSentidos, Int32 revistaEdicaoId, string revistaId)
        {
            List<RevistaArtigo> entidadesRetorno = new List<RevistaArtigo>();

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
                sbOrder.Append(" ORDER BY RevistaArtigo.tituloArtigo");
            }

            if (registrosPagina > 0)
            {
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT RevistaArtigo.*, ");
                sbSQL.Append("    RevistaSecao.nomeSecao, ");
                sbSQL.Append("    RevistaSecao.revistaId, ");
                sbSQL.Append("    RevistaArtigoPermissao.permissao, ");
                sbSQL.Append("    Arquivo.nomeArquivo, ");
                sbSQL.Append("    ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R ");
                sbSQL.Append("FROM RevistaArtigo ");
                sbSQL.Append("INNER JOIN RevistaSecao ON RevistaArtigo.revistaSecaoId = RevistaSecao.revistaSecaoId ");
                sbSQL.Append("INNER JOIN RevistaArtigoPermissao ON RevistaArtigo.revistaArtigoPermissaoId = RevistaArtigoPermissao.revistaArtigoPermissaoId ");
                sbSQL.Append("LEFT JOIN Arquivo ON RevistaArtigo.arquivoIdThumbP = Arquivo.arquivoId ");
                sbSQL.Append("WHERE ativo = 1 ");
                //sbSQL.Append("    AND RevistaArtigo.conteudoOnline = 1  ");
                //sbSQL.Append("    AND RevistaArtigo.revistaEdicaoId = @revistaEdicaoId ");
                sbSQL.Append("    AND RevistaArtigo.revistaEdicaoId IS NULL ");
                sbSQL.Append(String.Format("    AND RevistaSecao.revistaId IN ({0}) ", revistaId));
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());
            }
            else
            {
                sbSQL.Append("SELECT RevistaArtigo.*, ");
                sbSQL.Append("    RevistaSecao.nomeSecao, ");
                sbSQL.Append("    RevistaSecao.revistaId, ");
                sbSQL.Append("    RevistaArtigoPermissao.permissao, ");
                sbSQL.Append("    Arquivo.nomeArquivo ");
                sbSQL.Append("FROM RevistaArtigo ");
                sbSQL.Append("INNER JOIN RevistaSecao ON RevistaArtigo.revistaSecaoId = RevistaSecao.revistaSecaoId ");
                sbSQL.Append("INNER JOIN RevistaArtigoPermissao ON RevistaArtigo.revistaArtigoPermissaoId = RevistaArtigoPermissao.revistaArtigoPermissaoId ");
                sbSQL.Append("LEFT JOIN Arquivo ON RevistaArtigo.arquivoIdThumbP = Arquivo.arquivoId ");
                sbSQL.Append("WHERE ativo = 1 ");
                //sbSQL.Append("    AND RevistaArtigo.conteudoOnline = 1  ");
                //sbSQL.Append("    AND RevistaArtigo.revistaEdicaoId = @revistaEdicaoId ");
                sbSQL.Append(String.Format("    AND RevistaSecao.revistaId IN ({0}) ", revistaId));
                sbSQL.Append("    AND RevistaArtigo.revistaEdicaoId IS NULL ");
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            //_db.AddInParameter(command, "@revistaEdicaoId", DbType.Int32, revistaEdicaoId);

            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                RevistaArtigo entidadeRetorno = new RevistaArtigo();
                PopulaRevistaArtigoComDependencia(reader, entidadeRetorno);
                PopulaRevistaArtigoArquivoThumbP(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revistaEdicaoId"></param>
        /// <param name="qtdRegistros"></param>
        /// <returns></returns>
        public List<RevistaArtigo> CarregarArtigosPorRevistaEdicaoIdParaSite(Int32 revistaEdicaoId, Int32 qtdRegistros)
        {
            List<RevistaArtigo> entidadeRetorno = new List<RevistaArtigo>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(String.Format("SELECT TOP {0} RevistaArtigo.revistaArtigoId ", qtdRegistros));
            sbSQL.Append("    , RevistaArtigo.tituloArtigo ");
            sbSQL.Append("    , RevistaArtigo.autores ");
            sbSQL.Append("    , RevistaArtigo.revistaEdicaoId ");
            sbSQL.Append("    , RevistaEdicao.revistaId ");
            sbSQL.Append("    , Revista.nomeRevista ");
            sbSQL.Append("FROM RevistaArtigo ");
            sbSQL.Append("INNER JOIN Conteudo ON Conteudo.conteudoId = RevistaArtigo.revistaArtigoId ");
            sbSQL.Append("INNER JOIN RevistaEdicao ON RevistaEdicao.revistaEdicaoId = RevistaArtigo.revistaEdicaoId ");
            sbSQL.Append("INNER JOIN Revista ON Revista.revistaId = RevistaEdicao.revistaId ");
            sbSQL.Append("WHERE RevistaEdicao.revistaEdicaoId = @revistaEdicaoId ");
            sbSQL.Append("	AND RevistaArtigo.destaqueHome = 1 AND RevistaEdicao.ativo = 1 AND RevistaArtigo.ativo = 1 ");
            sbSQL.Append("ORDER BY Conteudo.dataHoraCadastro DESC ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaEdicaoId", DbType.Int32, revistaEdicaoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                RevistaArtigo revistaArtigo = new RevistaArtigo();
                PopulaRevistaArtigoParaSite(reader, revistaArtigo);
                entidadeRetorno.Add(revistaArtigo);
            }
            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revistaEdicaoId"></param>
        /// <returns></returns>
        public RevistaArtigo CarregarArtigoCapaPorRevistaEdicaoId(Int32 revistaEdicaoId)
        {
            RevistaArtigo entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("SELECT TOP 1 RevistaArtigo.*, ");
            sbSQL.Append("    RevistaSecao.nomeSecao, ");
            sbSQL.Append("    RevistaSecao.revistaId, ");
            sbSQL.Append("    RevistaArtigoPermissao.permissao, ");
            sbSQL.Append("    Arquivo.nomeArquivo ");
            sbSQL.Append("FROM RevistaArtigo ");
            sbSQL.Append("INNER JOIN RevistaSecao ON RevistaArtigo.revistaSecaoId = RevistaSecao.revistaSecaoId ");
            sbSQL.Append("INNER JOIN RevistaArtigoPermissao ON RevistaArtigo.revistaArtigoPermissaoId = RevistaArtigoPermissao.revistaArtigoPermissaoId ");
            sbSQL.Append("INNER JOIN Arquivo ON RevistaArtigo.arquivoIdCapa = Arquivo.arquivoId ");
            sbSQL.Append("WHERE RevistaArtigo.revistaEdicaoId = @revistaEdicaoId ");
            sbSQL.Append("    AND RevistaArtigo.destaquePrincipal = 1 AND ativo = 1 ");
            sbSQL.Append("ORDER BY NEWID() ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaEdicaoId", DbType.Int32, revistaEdicaoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new RevistaArtigo();
                PopulaRevistaArtigoComDependencia(reader, entidadeRetorno);
                PopulaRevistaArtigoArquivoCapa(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }

        public List<RevistaArtigo> CarregarRevistaArtigoBusca(int registrosPagina, int numeroPagina, String[] ordenacao, String[] ordenacaoSentido, String palavra, Revista entidade)
        {
            List<RevistaArtigo> entidadesRetorno = new List<RevistaArtigo>();

            string iniRegister = (((numeroPagina - 1) * registrosPagina) + 1).ToString();
            string endRegister = ((numeroPagina) * registrosPagina).ToString();

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            sbSQL.Append(@"SELECT
	                                *
                                FROM
	                                (
	                                SELECT ");

            if (ordenacao != null && !String.IsNullOrEmpty(ordenacao[0]))
                sbSQL.AppendFormat(@"       ROW_NUMBER() OVER ( ORDER BY {0} ) AS RowNumber ", ordenacao[0].ToString());
            else
                sbSQL.Append(@"       ROW_NUMBER() OVER ( ORDER BY RANKT DESC ) AS RowNumber  ");

            sbSQL.Append(@"             , *
                                    FROM
		                                (
		                                SELECT 
			                                RevistaArtigo.*
			                                , ISNULL(R1.RANK, 0) AS RANKT
		                                FROM
			                                RevistaArtigo
			                                INNER JOIN RevistaEdicao ON RevistaEdicao.revistaEdicaoId = RevistaArtigo.revistaEdicaoId 
                                            INNER JOIN CONTAINSTABLE(RevistaArtigo, *, @palavra) AS R1 ON R1.[KEY] = RevistaArtigo.revistaArtigoId 
                                        WHERE
                                            RevistaArtigo.Ativo = 1
			                                AND R1.[RANK] IS NOT NULL ");

            if (entidade != null && entidade.RevistaId > 0)
            {
                sbSQL.Append(@"             AND RevistaEdicao.revistaId = @revistaId ");
            }

            sbSQL.Append(@"             ) AS tabelaTemp1
	                                ) AS tabelaTemp2 ");

            sbSQL.AppendFormat(@"WHERE 
	                                RowNumber BETWEEN {0} AND {1}"
                                    , iniRegister
                                    , endRegister);

            if (ordenacao != null && !String.IsNullOrEmpty(ordenacao[0]))
                sbSQL.AppendFormat(@"ORDER BY {0}", ordenacao[0].ToString());
            else
                sbSQL.Append(@"ORDER BY RANKT DESC");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            if (!String.IsNullOrEmpty(palavra))
            {
                _db.AddInParameter(command, "@palavra", DbType.String, palavra);
            }

            if (entidade != null && entidade.RevistaId > 0)
            {
                _db.AddInParameter(command, "@revistaId", DbType.Int32, entidade.RevistaId);
            }

            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                RevistaArtigo entidadeRetorno = new RevistaArtigo();
                PopulaRevistaArtigo(reader, entidadeRetorno);

                if (entidadeRetorno.RevistaSecao != null && entidadeRetorno.RevistaSecao.RevistaSecaoId > 0)
                {
                    entidadeRetorno.RevistaSecao = new RevistaSecaoADO().Carregar(entidadeRetorno.RevistaSecao);
                }

                entidadesRetorno.Add(entidadeRetorno);
            }

            reader.Close();

            return entidadesRetorno;
        }

        public List<RevistaArtigo> CarregarArtigosAssociados(Int32 revistaArtigoId)
        {
            List<RevistaArtigo> entidadeRetorno = new List<RevistaArtigo>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT TOP 2 ");
            sbSQL.Append("    RevistaArtigo.*, ");
            sbSQL.Append("    RevistaSecao.nomeSecao, ");
            sbSQL.Append("    RevistaSecao.revistaId, ");
            sbSQL.Append("    RevistaArtigoPermissao.permissao, ");
            sbSQL.Append("    Arquivo.nomeArquivo ");
            sbSQL.Append("FROM RevistaArtigo ");
            sbSQL.Append("INNER JOIN RevistaSecao ON RevistaArtigo.revistaSecaoId = RevistaSecao.revistaSecaoId ");
            sbSQL.Append("INNER JOIN RevistaArtigoPermissao ON RevistaArtigo.revistaArtigoPermissaoId = RevistaArtigoPermissao.revistaArtigoPermissaoId ");
            sbSQL.Append("LEFT JOIN Arquivo ON RevistaArtigo.arquivoIdThumbP = Arquivo.arquivoId ");
            sbSQL.Append("WHERE revistaArtigoIdAssociado = @revistaArtigoId ");
            sbSQL.Append("    AND ativo = 1 ");
            sbSQL.Append("ORDER BY NEWID()");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaArtigoId", DbType.Int32, revistaArtigoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                RevistaArtigo revistaArtigo = new RevistaArtigo();
                PopulaRevistaArtigoComDependencia(reader, revistaArtigo);
                PopulaRevistaArtigoArquivoThumbP(reader, revistaArtigo);
                entidadeRetorno.Add(revistaArtigo);
            }
            reader.Close();

            return entidadeRetorno;
        }

        #endregion

        #region [ Contar ]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revistaEdicaoId"></param>
        /// <returns></returns>
        public Int32 ContarArtigosConteudoOnlinePorRevistaEdicaoId(Int32 revistaEdicaoId, string revistaId)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(RevistaArtigo.revistaArtigoId) as Total ");
            sbSQL.Append("FROM RevistaArtigo ");
            sbSQL.Append("INNER JOIN RevistaSecao ON RevistaArtigo.revistaSecaoId = RevistaSecao.revistaSecaoId ");
            sbSQL.Append("INNER JOIN RevistaArtigoPermissao ON RevistaArtigo.revistaArtigoPermissaoId = RevistaArtigoPermissao.revistaArtigoPermissaoId ");
            sbSQL.Append("WHERE ativo = 1 ");
            //sbSQL.Append("    AND RevistaArtigo.conteudoOnline = 1  ");
            //sbSQL.Append("    AND RevistaArtigo.revistaEdicaoId = @revistaEdicaoId ");
            sbSQL.Append(String.Format("    AND RevistaSecao.revistaId IN ({0}) ", revistaId));
            sbSQL.Append("    AND RevistaArtigo.revistaEdicaoId IS NULL ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            //_db.AddInParameter(command, "@revistaEdicaoId", DbType.Int32, revistaEdicaoId);

            Int32 resultado = (Int32)_db.ExecuteScalar(command);

            return resultado;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revistaId"></param>
        /// <returns></returns>
        public Int32 ContarArtigosSelecionados(Int32 revistaId)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT( RevistaArtigo.revistaArtigoId) as Total ");
            sbSQL.Append("FROM RevistaArtigo ");
            sbSQL.Append("INNER JOIN RevistaSecao ON RevistaArtigo.revistaSecaoId = RevistaSecao.revistaSecaoId ");
            sbSQL.Append("WHERE RevistaSecao.revistaId = @revistaId ");
            sbSQL.Append("    AND RevistaArtigo.revistaEdicaoId IS NULL AND ativo = 1 ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaId", DbType.Int32, revistaId);

            Int32 resultado = (Int32)_db.ExecuteScalar(command);

            return resultado;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="palavra"></param>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public int ContarArtigoBusca(String palavra, Revista entidade)
        {
            int total = 0;

            StringBuilder sbSql = new StringBuilder();

            sbSql.Append(@"SELECT 
	                            COUNT(RevistaArtigo.revistaArtigoId) AS Total
                            FROM
	                            RevistaArtigo
	                            INNER JOIN RevistaEdicao ON RevistaEdicao.revistaEdicaoId = RevistaArtigo.revistaEdicaoId 
							");

            if (!String.IsNullOrEmpty(palavra))
            {
                sbSql.Append(@"INNER JOIN CONTAINSTABLE(RevistaArtigo, *, @palavra) AS R1 ON R1.[KEY] = RevistaArtigo.revistaArtigoId ");
            }

            sbSql.Append(@"WHERE
                            RevistaArtigo.Ativo = 1 ");

            if (entidade != null && entidade.RevistaId > 0)
            {
                sbSql.Append(@"AND RevistaEdicao.revistaId = @revistaId	");
            }

            DbCommand command = _db.GetSqlStringCommand(sbSql.ToString());

            if (!String.IsNullOrEmpty(palavra))
            {
                _db.AddInParameter(command, "@palavra", DbType.String, palavra);
            }

            if (entidade != null && entidade.RevistaId > 0)
            {
                _db.AddInParameter(command, "@revistaId", DbType.Int32, entidade.RevistaId);
            }

            IDataReader reader = _db.ExecuteReader(command);

            if ((reader.Read()) && (reader["Total"] != null))
            {
                total = Int32.Parse(reader["Total"].ToString());
            }

            reader.Close();

            return total;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revistaEdicaoId"></param>
        /// <param name="qtdRegistros"></param>
        /// <param name="destaque"></param>
        /// <param name="revistaSecaoId"></param>
        /// <returns></returns>
        public Int32 ContarArtigosPorRevistaEdicaoId(Int32 revistaEdicaoId, Boolean destaque, Int32? revistaSecaoId)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT( RevistaArtigo.revistaArtigoId) as Total ");
            sbSQL.Append("FROM RevistaArtigo ");
            sbSQL.Append("INNER JOIN RevistaSecao ON RevistaArtigo.revistaSecaoId = RevistaSecao.revistaSecaoId ");
            sbSQL.Append("INNER JOIN RevistaArtigoPermissao ON RevistaArtigo.revistaArtigoPermissaoId = RevistaArtigoPermissao.revistaArtigoPermissaoId ");
            sbSQL.Append("WHERE RevistaArtigo.revistaEdicaoId = @revistaEdicaoId ");
            sbSQL.Append("    AND ativo = 1 ");
            if (destaque)
            {
                sbSQL.Append("  AND RevistaArtigo.destaqueHome = 1 ");
            }
            if (revistaSecaoId != null)
            {
                sbSQL.Append("  AND RevistaArtigo.revistaSecaoId = @revistaSecaoId ");
            }

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaEdicaoId", DbType.Int32, revistaEdicaoId);
            if (revistaSecaoId != null)
            {
                _db.AddInParameter(command, "@revistaSecaoId", DbType.Int32, revistaSecaoId.Value);
            }

            Int32 resultado = (Int32)_db.ExecuteScalar(command);

            return resultado;
        }

        #endregion

        #region [ Popula ]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entidade"></param>
        public static void PopulaRevistaArtigoParaSite(IDataReader reader, RevistaArtigo entidade)
        {
            if (reader["tituloArtigo"] != DBNull.Value)
            {
                entidade.TituloArtigo = reader["tituloArtigo"].ToString();
            }

            if (reader["autores"] != DBNull.Value)
            {
                entidade.Autores = reader["autores"].ToString();
            }

            if (reader["revistaArtigoId"] != DBNull.Value)
            {
                entidade.RevistaArtigoId = Convert.ToInt32(reader["revistaArtigoId"].ToString());
            }

            if (reader["revistaEdicaoId"] != DBNull.Value)
            {
                if (entidade.RevistaEdicao == null) entidade.RevistaEdicao = new RevistaEdicao();
                entidade.RevistaEdicao.RevistaEdicaoId = Convert.ToInt32(reader["revistaEdicaoId"].ToString());
            }

            if (reader["revistaId"] != DBNull.Value)
            {
                if (entidade.RevistaEdicao == null) entidade.RevistaEdicao = new RevistaEdicao();
                if (entidade.RevistaEdicao.Revista == null) entidade.RevistaEdicao.Revista = new Revista();
                entidade.RevistaEdicao.Revista.RevistaId = Convert.ToInt32(reader["revistaId"].ToString());
            }

            if (reader["nomeRevista"] != DBNull.Value)
            {
                if (entidade.RevistaEdicao == null) entidade.RevistaEdicao = new RevistaEdicao();
                if (entidade.RevistaEdicao.Revista == null) entidade.RevistaEdicao.Revista = new Revista();
                entidade.RevistaEdicao.Revista.NomeRevista = reader["nomeRevista"].ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entidade"></param>
        public static void PopulaRevistaArtigoArquivoCapa(IDataReader reader, RevistaArtigo entidade)
        {
            if (reader["arquivoIdCapa"] != DBNull.Value)
            {
                if (entidade.ArquivoCapa == null) entidade.ArquivoCapa = new Arquivo();
                entidade.ArquivoCapa.ArquivoId = Convert.ToInt32(reader["arquivoIdCapa"].ToString());
            }

            if (reader["nomeArquivo"] != DBNull.Value)
            {
                if (entidade.ArquivoCapa == null) entidade.ArquivoCapa = new Arquivo();
                entidade.ArquivoCapa.NomeArquivo = reader["nomeArquivo"].ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entidade"></param>
        public static void PopulaRevistaArtigoArquivoLateral(IDataReader reader, RevistaArtigo entidade)
        {
            if (reader["arquivoIdLateral"] != DBNull.Value)
            {
                if (entidade.ArquivoLateral == null) entidade.ArquivoLateral = new Arquivo();
                entidade.ArquivoLateral.ArquivoId = Convert.ToInt32(reader["arquivoIdLateral"].ToString());
            }

            if (reader["nomeArquivoLateral"] != DBNull.Value)
            {
                if (entidade.ArquivoLateral == null) entidade.ArquivoLateral = new Arquivo();
                entidade.ArquivoLateral.NomeArquivo = reader["nomeArquivoLateral"].ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entidade"></param>
        public static void PopulaRevistaArtigoArquivoThumbP(IDataReader reader, RevistaArtigo entidade)
        {
            if (reader["arquivoIdThumbP"] != DBNull.Value)
            {
                if (entidade.ArquivoThumbP == null) entidade.ArquivoThumbP = new Arquivo();
                entidade.ArquivoThumbP.ArquivoId = Convert.ToInt32(reader["arquivoIdThumbP"].ToString());
            }

            if (reader["nomeArquivo"] != DBNull.Value)
            {
                if (entidade.ArquivoThumbP == null) entidade.ArquivoThumbP = new Arquivo();
                entidade.ArquivoThumbP.NomeArquivo = reader["nomeArquivo"].ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entidade"></param>
        public static void PopulaRevistaArtigoArquivoThumbM(IDataReader reader, RevistaArtigo entidade)
        {
            if (reader["arquivoIdThumbM"] != DBNull.Value)
            {
                if (entidade.ArquivoThumbM == null) entidade.ArquivoThumbM = new Arquivo();
                entidade.ArquivoThumbM.ArquivoId = Convert.ToInt32(reader["arquivoIdThumbM"].ToString());
            }

            if (reader["nomeArquivo"] != DBNull.Value)
            {
                if (entidade.ArquivoThumbM == null) entidade.ArquivoThumbM = new Arquivo();
                entidade.ArquivoThumbM.NomeArquivo = reader["nomeArquivo"].ToString();
            }
        }

        /// <summary>
        /// Método que popula RevistaGaleriaArtigoImagem
        /// </summary>
        public static void PopulaRevistaArtigoGaleriaImagem(IDataReader reader, RevistaGaleriaArtigoImagem entidade)
        {
            if (reader["revistaArtigoId"] != DBNull.Value)
                entidade.RevistaArtigoId = Convert.ToInt32(reader["revistaArtigoId"].ToString());

            if (reader["arquivoId"] != DBNull.Value)
            {
                entidade.Arquivo = new Arquivo();
                entidade.Arquivo.ArquivoId = Convert.ToInt32(reader["arquivoId"].ToString());
                entidade.Arquivo = new ArquivoADO().Carregar(entidade.Arquivo);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entidade"></param>
        public static void PopulaRevistaArtigoComDependencia(IDataReader reader, RevistaArtigo entidade)
        {
            if (reader["tituloArtigo"] != DBNull.Value)
            {
                entidade.TituloArtigo = reader["tituloArtigo"].ToString();
            }

            if (reader["subTituloArtigo"] != DBNull.Value)
            {
                entidade.SubTituloArtigo = reader["subTituloArtigo"].ToString();
            }

            if (reader["resumo"] != DBNull.Value)
            {
                entidade.Resumo = reader["resumo"].ToString();
            }

            if (reader["textoArtigo"] != DBNull.Value)
            {
                entidade.TextoArtigo = reader["textoArtigo"].ToString();
            }

            if (reader["autores"] != DBNull.Value)
            {
                entidade.Autores = reader["autores"].ToString();
            }

            if (reader["bibliografia"] != DBNull.Value)
            {
                entidade.Bibliografia = reader["bibliografia"].ToString();
            }

            if (reader["destaqueHome"] != DBNull.Value)
            {
                entidade.DestaqueHome = Convert.ToBoolean(reader["destaqueHome"].ToString());
            }

            if (reader["revistaArtigoId"] != DBNull.Value)
            {
                entidade.RevistaArtigoId = Convert.ToInt32(reader["revistaArtigoId"].ToString());
            }

            if (reader["conteudoOnline"] != DBNull.Value)
            {
                entidade.ConteudoOnline = Convert.ToBoolean(reader["conteudoOnline"].ToString());
            }

            if (reader["ativo"] != DBNull.Value)
            {
                entidade.Ativo = Convert.ToBoolean(reader["ativo"].ToString());
            }

            if (reader["dataPublicacao"] != DBNull.Value)
            {
                entidade.DataPublicacao = Convert.ToDateTime(reader["dataPublicacao"].ToString());
            }

            if (reader["revistaEdicaoId"] != DBNull.Value)
            {
                entidade.RevistaEdicao = new RevistaEdicao();
                entidade.RevistaEdicao.RevistaEdicaoId = Convert.ToInt32(reader["revistaEdicaoId"].ToString());
            }

            if (reader["revistaSecaoId"] != DBNull.Value)
            {
                if (entidade.RevistaSecao == null) entidade.RevistaSecao = new RevistaSecao();
                entidade.RevistaSecao.RevistaSecaoId = Convert.ToInt32(reader["revistaSecaoId"].ToString());
            }

            if (reader["nomeSecao"] != DBNull.Value)
            {
                if (entidade.RevistaSecao == null) entidade.RevistaSecao = new RevistaSecao();
                entidade.RevistaSecao.NomeSecao = reader["nomeSecao"].ToString();
            }

            if (reader["revistaId"] != DBNull.Value)
            {
                if (entidade.RevistaSecao == null) entidade.RevistaSecao = new RevistaSecao();
                if (entidade.RevistaSecao.Revista == null) entidade.RevistaSecao.Revista = new Revista();
                entidade.RevistaSecao.Revista.RevistaId = Convert.ToInt32(reader["revistaId"].ToString());
            }

            if (reader["revistaArtigoPermissaoId"] != DBNull.Value)
            {
                if (entidade.RevistaArtigoPermissao == null) entidade.RevistaArtigoPermissao = new RevistaArtigoPermissao();
                entidade.RevistaArtigoPermissao.RevistaArtigoPermissaoId = Convert.ToInt32(reader["revistaArtigoPermissaoId"].ToString());
            }

            if (reader["permissao"] != DBNull.Value)
            {
                if (entidade.RevistaArtigoPermissao == null) entidade.RevistaArtigoPermissao = new RevistaArtigoPermissao();
                entidade.RevistaArtigoPermissao.Permissao = reader["permissao"].ToString();
            }

            if (reader["revistaArtigoIdAssociado"] != DBNull.Value)
            {
                entidade.RevistaArtigoAssociado = new RevistaArtigo();
                entidade.RevistaArtigoAssociado.RevistaArtigoId = Convert.ToInt32(reader["revistaArtigoIdAssociado"].ToString());
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revistaArtigo"></param>
        /// <param name="produto"></param>
        public void ExcluirRevistaArtigoProduto(RevistaArtigo revistaArtigo, Produto produto)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM RevistaArtigoProduto ");
            sbSQL.Append("WHERE revistaArtigoId = @revistaArtigoId AND produtoId = @produtoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaArtigoId", DbType.Int32, revistaArtigo.RevistaArtigoId);
            _db.AddInParameter(command, "@produtoId", DbType.Int32, produto.ProdutoId);
            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revistaArtigo"></param>
        /// <param name="produto"></param>
        public void IncluirRevistaArtigoProduto(RevistaArtigo revistaArtigo, Produto produto)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO RevistaArtigoProduto ");
            sbSQL.Append(" (revistaArtigoId, produtoId) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@revistaArtigoId, @produtoId) ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaArtigoId", DbType.Int32, revistaArtigo.RevistaArtigoId);
            _db.AddInParameter(command, "@produtoId", DbType.Int32, produto.ProdutoId);

            // Executa a query.
            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="palavra"></param>
        /// <returns></returns>
        public StringBuilder CarregarAutoCompleteBusca(String palavra)
        {
            StringBuilder retorno = new StringBuilder();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(@"SELECT 
	                            TOP 20 * 
                            FROM
	                            (
                                SELECT
		                            tituloArtigo AS texto
		                            , '1' as ordem 
	                            FROM
		                            RevistaArtigo
                                    INNER JOIN RevistaEdicao ON RevistaEdicao.revistaEdicaoId = RevistaArtigo.revistaEdicaoId AND RevistaEdicao.ativo = 1
		                            INNER JOIN Produto ON Produto.produtoId = RevistaEdicao.revistaEdicaoId
                                    --AND Produto.exibirSite = 1 AND Produto.homologado=1
	                            WHERE
		                            RevistaArtigo.tituloArtigo LIKE @palavra
                                    AND RevistaArtigo.Ativo = 1
	                            ) AS R
                            ORDER BY
	                            R.ordem
	                            , R.texto");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@palavra", DbType.String, "%" + palavra + "%");

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                retorno.Append("{\"");
                retorno.Append(reader["texto"].ToString());
                retorno.Append("\":\"");
                retorno.Append(reader["texto"].ToString());
                retorno.Append("\"},");
            }

            reader.Close();

            return retorno;
        }

        #endregion
    }
}