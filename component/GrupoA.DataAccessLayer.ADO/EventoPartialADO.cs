using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using GrupoA.BusinessObject;
using GrupoA.FilterHelper;

namespace GrupoA.DataAccess.ADO
{
    public partial class EventoADO : ADOSuper, IEventoDAL
    {
        /// <summary>
        /// Retorna uma coleção de eventos de acordo com as áreas de interesse do usuário
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public IEnumerable<Evento> CarregarPorAreaInteresse(Usuario entidade, Int32 quantidadeRegistros)
        {
            List<Evento> entidadesRetorno = new List<Evento>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(" WITH Categorias (categoriaId, nomeCategoria, categoriaIdPai, Nivel) ");
            sbSQL.Append(" AS ");
            sbSQL.Append(" ( ");
            sbSQL.Append(" SELECT C.categoriaId, C.nomeCategoria, C.categoriaIdPai, 0 AS Nivel ");
            sbSQL.Append(" FROM Categoria AS C ");
            sbSQL.Append(" WHERE C.categoriaId IN (SELECT UsuarioInteresse.categoriaId FROM UsuarioInteresse WHERE UsuarioInteresse.usuarioId = @usuarioId) ");
            sbSQL.Append(" UNION ALL ");
            sbSQL.Append(" SELECT C.categoriaId, C.nomeCategoria, C.categoriaIdPai, Nivel-1 ");
            sbSQL.Append(" FROM Categoria AS C ");
            sbSQL.Append(" INNER JOIN Categorias AS CS ");
            sbSQL.Append(" ON c.categoriaId = CS.categoriaIdPai ");
            sbSQL.Append(" ) ");
            sbSQL.AppendFormat(" SELECT TOP {0} E.*, A.*, A.nomeArquivo as arquivoIdThumb FROM dbo.Evento E ", quantidadeRegistros);
            sbSQL.Append(" LEFT JOIN dbo.Arquivo A ON A.arquivoId = E.arquivoIdThumb ");
            sbSQL.Append(" WHERE E.eventoId IN ");
            sbSQL.Append(" ( ");
            sbSQL.Append(" SELECT C.ConteudoId FROM dbo.Conteudo C ");
            sbSQL.Append(" INNER JOIN dbo.ConteudoImprensa CI ON CI.conteudoImprensaId = C.conteudoId ");
            sbSQL.Append(" INNER JOIN dbo.ConteudoAreaConhecimento CAC ON CAC.conteudoId = C.conteudoId ");
            sbSQL.Append(" WHERE CAC.categoriaId IN (SELECT CAT.categoriaId FROM Categorias CAT WHERE CAT.categoriaIdPai IS null) ");
            sbSQL.Append(" AND CI.destaque = 1 ");
            sbSQL.Append(" ) ");
            sbSQL.Append(" AND dataEventoInicio >= @dataEventoInicio ");
            sbSQL.Append(" ORDER BY dataEventoInicio ASC ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.UsuarioId);
            _db.AddInParameter(command, "@dataEventoInicio", DbType.DateTime, DateTime.Today);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Evento entidadeRetorno = new Evento();
                PopulaEvento(reader, entidadeRetorno);

                // Popula as imagens
                entidadeRetorno.ArquivoThumb = new Arquivo();
                ArquivoADO.PopulaArquivo(reader, entidadeRetorno.ArquivoThumb);


                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// Método que atualiza o status de um Evento.
        /// </summary>
        /// <param name="entidade">Evento contendo os dados a serem atualizados.</param>
        public void AtualizarStatus(Evento entidade)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE Evento SET ");
            sbSQL.Append(" ativo=@ativo ");
            sbSQL.Append(" WHERE eventoId=@eventoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@eventoId", DbType.Int32, entidade.EventoId);
            _db.AddInParameter(command, "@ativo", DbType.Int32, entidade.ConteudoImprensa.Ativo);

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que retorna uma coleção de Evento com conteudo Imprensa.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos Evento.</returns>
        public IEnumerable<Evento> CarregarTodosValidosComDependencias(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {
            List<Evento> entidadesRetorno = new List<Evento>();

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
            else
            {
                sbOrder.Append(" ORDER BY eventoId");
            }


            if (registrosPagina > 0)
            {

                sbSql.Append("SELECT * FROM ( ");

                sbSql.Append("SELECT Evento.* ");
                sbSql.Append(", ConteudoImprensa.* ");
                sbSql.Append(", Conteudo.* ");
                sbSql.Append(", Arquivo.* ");
                sbSql.Append(", ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R ");
                sbSql.Append(" FROM Evento");
                sbSql.Append(" INNER JOIN ConteudoImprensa ON Evento.eventoId=ConteudoImprensa.conteudoImprensaId");
                sbSql.Append(" INNER JOIN Conteudo ON ConteudoImprensa.conteudoImprensaId=Conteudo.conteudoId");
                sbSql.Append(" LEFT JOIN ConteudoAreaConhecimento ON ConteudoAreaConhecimento.conteudoId = Conteudo.conteudoId");
                sbSql.Append(" LEFT JOIN Arquivo ON Arquivo.arquivoId=Evento.arquivoIdThumb");

                sbSql.Append(" WHERE ((Evento.dataEventoInicio <= GETDATE() AND Evento.dataEventoFim >= GETDATE())  ");
                sbSql.Append("    OR (Evento.dataEventoInicio >= GETDATE())) ");
                sbSql.Append("AND ConteudoImprensa.ativo = 1 ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                {
                    sbSql.Append(string.Concat(" AND ", filtro.GetWhereString()));
                }
                sbSql.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSql.Append("SELECT Evento.* ");
                sbSql.Append(", ConteudoImprensa.* ");
                sbSql.Append(", Conteudo.* ");
                sbSql.Append(", Arquivo.* ");
                sbSql.Append(" FROM Evento");
                sbSql.Append(" INNER JOIN ConteudoImprensa ON Evento.eventoId=ConteudoImprensa.conteudoImprensaId");
                sbSql.Append(" INNER JOIN Conteudo ON ConteudoImprensa.conteudoImprensaId=Conteudo.conteudoId");
                sbSql.Append(" LEFT JOIN ConteudoAreaConhecimento ON ConteudoAreaConhecimento.conteudoId = Conteudo.conteudoId");
                sbSql.Append(" LEFT JOIN Arquivo ON Arquivo.arquivoId=Evento.arquivoIdThumb");
                sbSql.Append(" WHERE ((Evento.dataEventoInicio <= GETDATE() AND Evento.dataEventoFim >= GETDATE())  ");
                sbSql.Append("    OR (Evento.dataEventoInicio >= GETDATE())) ");
                sbSql.Append("AND ConteudoImprensa.ativo = 1 ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                {
                    sbSql.Append(string.Concat(" AND ", filtro.GetWhereString()));
                }

                if (sbOrder.Length > 0) { sbSql.Append(sbOrder.ToString()); }
            }

            DbCommand command = _db.GetSqlStringCommand(sbSql.ToString());
            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Evento entidadeRetorno = new Evento();
                PopulaEvento(reader, entidadeRetorno);
                entidadeRetorno.ConteudoImprensa = new ConteudoImprensa();
                ConteudoImprensaADO.PopulaConteudoImprensa(reader, entidadeRetorno.ConteudoImprensa);
                entidadeRetorno.ConteudoImprensa.Conteudo = new Conteudo();
                ConteudoADO.PopulaConteudo(reader, entidadeRetorno.ConteudoImprensa.Conteudo);
                if (reader["arquivoId"] != DBNull.Value)
                {
                    entidadeRetorno.ArquivoThumb = new Arquivo();
                    ArquivoADO.PopulaArquivo(reader, entidadeRetorno.ArquivoThumb);
                }

                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int ContarTodosValidosComDependencias()
        {

            int retorno = 0;

            StringBuilder sbSql = new StringBuilder();
            //StringBuilder sbWhere = new StriKCngBuilder();

            sbSql.Append("SELECT COUNT(EventoId) Total ");
            sbSql.Append(" FROM Evento");
            sbSql.Append(" INNER JOIN ConteudoImprensa ON Evento.eventoId=ConteudoImprensa.conteudoImprensaId");
            sbSql.Append(" INNER JOIN Conteudo ON ConteudoImprensa.conteudoImprensaId=Conteudo.conteudoId");
            sbSql.Append(" LEFT JOIN Arquivo ON Arquivo.arquivoId=Evento.arquivoIdThumb");
            sbSql.Append(" WHERE (GETDATE() >= ISNULL(dbo.ConteudoImprensa.dataExibicaoInicio, GETDATE()) ");
            sbSql.Append("AND GETDATE() <= ISNULL(dbo.ConteudoImprensa.dataExibicaoFim, GETDATE())) ");
            sbSql.Append("AND ConteudoImprensa.ativo = 1 ");


            DbCommand command = _db.GetSqlStringCommand(sbSql.ToString());
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
        /// <param name="palavra"></param>
        /// <returns></returns>
        public int ContarEventoBusca(string palavra)
        {
            int total = 0;

            StringBuilder sbSql = new StringBuilder();

            sbSql.Append(@"SELECT
								COUNT(F.eventoId) AS Total
							FROM
								(
								SELECT 
									R.*
									, ROW_NUMBER() OVER ( ORDER BY RANKT DESC ) AS RowNumber
								FROM
									(
									SELECT 
										T.*
										, CI.*
										, ISNULL(R1.RANK, 0) + ISNULL(R2.RANK, 0) AS RANKT
									FROM
										Evento T
										INNER JOIN ConteudoImprensa CI ON CI.conteudoImprensaId = T.eventoId
										LEFT JOIN CONTAINSTABLE(ConteudoImprensa, *, @palavra) AS R1 ON R1.[KEY] = CI.conteudoImprensaId
										LEFT JOIN CONTAINSTABLE(Evento, *, @palavra) AS R2 ON R2.[KEY] = T.eventoId
									WHERE CI.Ativo = 1
										AND GETDATE() BETWEEN ISNULL(dataExibicaoInicio,GETDATE()) 
										AND ISNULL(dataExibicaoFim,GETDATE())
										AND ( R1.[RANK] IS NOT NULL OR R2.[RANK] IS NOT NULL)
									) AS R
								) AS F");

            DbCommand command = _db.GetSqlStringCommand(sbSql.ToString());

            if (!String.IsNullOrEmpty(palavra))
            {
                _db.AddInParameter(command, "@palavra", DbType.String, palavra);
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
        /// <param name="registrosPagina"></param>
        /// <param name="numeroPagina"></param>
        /// <param name="palavra"></param>
        /// <returns></returns>
        public List<Evento> CarregarEventoBusca(int registrosPagina, int numeroPagina, String[] ordenacao, String[] ordenacaoSentido, String palavra)
        {
            List<Evento> entidadesRetorno = new List<Evento>();

            string iniRegister = (((numeroPagina - 1) * registrosPagina) + 1).ToString();
            string endRegister = ((numeroPagina) * registrosPagina).ToString();

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            sbSQL.AppendFormat(@"SELECT 
									F.*
								FROM
									(
									SELECT 
										R.*  ");

            if (ordenacao != null && !String.IsNullOrEmpty(ordenacao[0]))
                sbSQL.AppendFormat(@", ROW_NUMBER() OVER ( ORDER BY {0} ) AS RowNumber ", ordenacao[0].ToString());
            else
                sbSQL.Append(@", ROW_NUMBER() OVER ( ORDER BY RANKT DESC ) AS RowNumber  ");

            sbSQL.AppendFormat(@"FROM
										(
										SELECT 
											T.*
											, CI.*
											, ISNULL(R1.RANK, 0) + ISNULL(R2.RANK, 0) AS RANKT
										FROM
											Evento T
											INNER JOIN ConteudoImprensa CI ON CI.conteudoImprensaId = T.eventoId
											LEFT JOIN CONTAINSTABLE(ConteudoImprensa, *, @palavra) AS R1 ON R1.[KEY] = CI.conteudoImprensaId
											LEFT JOIN CONTAINSTABLE(Evento, *, @palavra) AS R2 ON R2.[KEY] = T.eventoId
										WHERE CI.Ativo = 1
											AND GETDATE() BETWEEN ISNULL(dataExibicaoInicio,GETDATE()) 
											AND ISNULL(dataExibicaoFim,GETDATE())
											AND ( R1.[RANK] IS NOT NULL OR R2.[RANK] IS NOT NULL)
										) AS R
									) AS F
								WHERE 
									F.RowNumber BETWEEN {0} AND {1}"
                                    , iniRegister
                                    , endRegister);

            if (ordenacao != null && !String.IsNullOrEmpty(ordenacao[0]))
                sbSQL.AppendFormat(@"ORDER BY {0}", ordenacao[0].ToString());
            else
                sbSQL.Append(@"ORDER BY RANKT DESC");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@palavra", DbType.String, palavra);

            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Evento entidadeRetorno = new Evento();
                PopulaEvento(reader, entidadeRetorno);

                entidadeRetorno.ConteudoImprensa = new ConteudoImprensa();
                ConteudoImprensaADO.PopulaConteudoImprensa(reader, entidadeRetorno.ConteudoImprensa);

                entidadesRetorno.Add(entidadeRetorno);
            }

            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoriaBO"></param>
        /// <returns></returns>
        public List<Evento> CarregarEventosPorCategoria(Categoria categoriaBO)
        {
            List<Evento> entidadesRetorno = new List<Evento>();

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            sbSQL.AppendFormat(@"SELECT
	                                TOP 2
	                                Evento.eventoId
	                                , Evento.local
	                                , Evento.dataEventoInicio
	                                , Evento.dataEventoFim
	                                , ConteudoImprensa.titulo
	                                , CONVERT(VARCHAR(20), Conteudo.dataHoraCadastro, 103) + ' ' + Convert(varchar(20), Conteudo.dataHoraCadastro, 108) AS dataHoraCadastro
	                                , Arquivo.nomeArquivoOriginal
                                FROM
	                                Evento
	                                INNER JOIN ConteudoImprensa ON Evento.eventoId = ConteudoImprensa.conteudoImprensaId
	                                INNER JOIN Conteudo ON ConteudoImprensa.conteudoImprensaId = Conteudo.conteudoId
	                                LEFT JOIN Arquivo ON Evento.arquivoIdThumb = Arquivo.arquivoId ");

            if (categoriaBO != null && categoriaBO.CategoriaId > 0)
            {
                sbSQL.AppendFormat("INNER JOIN ConteudoAreaConhecimento ON Conteudo.conteudoId = ConteudoAreaConhecimento.conteudoId ");
            }

            sbSQL.AppendFormat("WHERE ((Evento.dataEventoInicio <= GETDATE() AND Evento.dataEventoFim >= GETDATE())  ");
            sbSQL.AppendFormat("    OR (Evento.dataEventoInicio >= GETDATE())) ");

            if (categoriaBO != null && categoriaBO.CategoriaId > 0)
            {
                sbSQL.AppendFormat(" AND ConteudoAreaConhecimento.categoriaId = @categoriaId ");
            }

            sbSQL.AppendFormat("ORDER BY Evento.dataEventoInicio ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            if (categoriaBO != null && categoriaBO.CategoriaId > 0)
            {
                _db.AddInParameter(command, "@categoriaId", DbType.String, categoriaBO.CategoriaId);
            }

            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Evento entidadeRetorno = new Evento();
                PopulaUltimosEventos(reader, entidadeRetorno);

                entidadesRetorno.Add(entidadeRetorno);
            }

            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoriaBOList"></param>
        /// <returns></returns>
        public List<Evento> CarregarEventosPorCategoria(List<Categoria> categoriaBOList)
        {
            List<Evento> entidadesRetorno = new List<Evento>();

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            sbSQL.AppendFormat(@"SELECT
	                                TOP 2
	                                Evento.eventoId
	                                , Evento.local
	                                , Evento.dataEventoInicio
	                                , Evento.dataEventoFim
	                                , ConteudoImprensa.titulo
	                                , CONVERT(VARCHAR(20), Conteudo.dataHoraCadastro, 103) + ' ' + Convert(varchar(20), Conteudo.dataHoraCadastro, 108) AS dataHoraCadastro
	                                , Arquivo.nomeArquivoOriginal
                                FROM
	                                Evento
	                                INNER JOIN ConteudoImprensa ON Evento.eventoId = ConteudoImprensa.conteudoImprensaId
	                                INNER JOIN Conteudo ON ConteudoImprensa.conteudoImprensaId = Conteudo.conteudoId
	                                LEFT JOIN Arquivo ON Evento.arquivoIdThumb = Arquivo.arquivoId ");

            if (categoriaBOList != null && categoriaBOList.Count > 0)
            {
                sbSQL.AppendFormat("INNER JOIN ConteudoAreaConhecimento ON Conteudo.conteudoId = ConteudoAreaConhecimento.conteudoId ");
            }

            sbSQL.AppendFormat("WHERE ((Evento.dataEventoInicio <= GETDATE() AND Evento.dataEventoFim >= GETDATE())  ");
            sbSQL.AppendFormat("    OR (Evento.dataEventoInicio >= GETDATE())) ");

            if (categoriaBOList != null && categoriaBOList.Count > 0)
            {
                sbSQL.AppendFormat(" AND ConteudoAreaConhecimento.categoriaId IN ( 0 ");

                foreach (Categoria categoriaBOTemp in categoriaBOList)
                {
                    sbSQL.AppendFormat(String.Format(" , {0}", categoriaBOTemp.CategoriaId.ToString()));
                }

                sbSQL.AppendFormat(" )");
            }

            sbSQL.AppendFormat("ORDER BY Evento.dataEventoInicio ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Evento entidadeRetorno = new Evento();
                PopulaUltimosEventos(reader, entidadeRetorno);

                entidadesRetorno.Add(entidadeRetorno);
            }

            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entidade"></param>
        public static void PopulaUltimosEventos(IDataReader reader, Evento entidade)
        {
            if (reader["eventoId"] != DBNull.Value)
            {
                entidade.EventoId = Convert.ToInt32(reader["eventoId"].ToString());
            }

            if (reader["local"] != DBNull.Value)
            {
                entidade.Local = reader["local"].ToString();
            }

            if (reader["dataEventoInicio"] != DBNull.Value)
            {
                entidade.DataEventoInicio = Convert.ToDateTime(reader["dataEventoInicio"].ToString());
            }

            if (reader["dataEventoFim"] != DBNull.Value)
            {
                entidade.DataEventoFim = Convert.ToDateTime(reader["dataEventoFim"].ToString());
            }

            if (reader["nomeArquivoOriginal"] != DBNull.Value)
            {
                if (entidade.ArquivoThumb == null) entidade.ArquivoThumb = new Arquivo();
                entidade.ArquivoThumb.NomeArquivoOriginal = reader["nomeArquivoOriginal"].ToString();
            }

            if (reader["titulo"] != DBNull.Value)
            {
                if (entidade.ConteudoImprensa == null) entidade.ConteudoImprensa = new ConteudoImprensa();
                entidade.ConteudoImprensa.Titulo = reader["titulo"].ToString();
            }

            if (reader["dataHoraCadastro"] != DBNull.Value)
            {
                if (entidade.ConteudoImprensa == null) entidade.ConteudoImprensa = new ConteudoImprensa();
                if (entidade.ConteudoImprensa.Conteudo == null) entidade.ConteudoImprensa.Conteudo = new Conteudo();
                entidade.ConteudoImprensa.Conteudo.DataHoraCadastro = Convert.ToDateTime(reader["dataHoraCadastro"].ToString());
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="areaId"></param>
        /// <returns></returns>
        public int ContarTodosEventosValidosComDependencias(int areaId)
        {
            int retorno = 0;

            StringBuilder sbSql = new StringBuilder();
            //StringBuilder sbWhere = new StriKCngBuilder();

            sbSql.Append(@"SELECT 
	                        COUNT(EventoId) Total
                        FROM
	                        Evento
	                        INNER JOIN ConteudoImprensa ON Evento.eventoId  =ConteudoImprensa.conteudoImprensaId
	                        INNER JOIN Conteudo ON ConteudoImprensa.conteudoImprensaId = Conteudo.conteudoId
	                        LEFT JOIN ConteudoAreaConhecimento ON ConteudoAreaConhecimento.conteudoId = Conteudo.conteudoId
	                        LEFT JOIN Arquivo ON Arquivo.arquivoId = Evento.arquivoIdThumb
                        WHERE
	                        (GETDATE() >= ISNULL(dbo.ConteudoImprensa.dataExibicaoInicio, GETDATE()) AND GETDATE() <= ISNULL(dbo.ConteudoImprensa.dataExibicaoFim, GETDATE()))
	                        AND ConteudoImprensa.ativo = 1
	                        AND ConteudoAreaConhecimento.categoriaId = @categoriaId");

            DbCommand command = _db.GetSqlStringCommand(sbSql.ToString());
            _db.AddInParameter(command, "@categoriaId", DbType.String, areaId);
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
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        public Int32 ContarTodosEventosPorInteresseUsuarioId(Int32 usuarioId)
        {
            Int32 retorno = 0;

            StringBuilder sbSql = new StringBuilder();

            sbSql.Append(@"WITH Categorias (categoriaId, nomeCategoria, categoriaIdPai, nivel ) AS (
	                            SELECT C.categoriaId, C.nomeCategoria, C.categoriaIdPai, 0 AS Nivel
	                            FROM Categoria AS C
	                            WHERE C.categoriaId IN ('1')
	                            UNION ALL
	                            SELECT C.categoriaId, C.nomeCategoria, C.categoriaIdPai, Nivel+1
	                            FROM Categoria AS C
	                            INNER JOIN Categorias AS CS
	                            ON c.CategoriaIdPai = CS.categoriaId )
                            SELECT COUNT(*) AS Total FROM (
	                            SELECT distinct Evento.*
	                            , ConteudoImprensa.*
	                            , Conteudo.*
	                            FROM Evento
	                            INNER JOIN ConteudoImprensa ON Evento.eventoId=ConteudoImprensa.conteudoImprensaId
	                            INNER JOIN Conteudo ON ConteudoImprensa.conteudoImprensaId=Conteudo.conteudoId
	                            INNER JOIN ConteudoAreaConhecimento ON ConteudoAreaConhecimento.conteudoId = Conteudo.conteudoId
	                            INNER JOIN Categorias ON Categorias.categoriaIdPai = ConteudoAreaConhecimento.categoriaId
	                            INNER JOIN UsuarioInteresse ON UsuarioInteresse.categoriaId = Categorias.categoriaId AND UsuarioInteresse.usuarioId = @usuarioId
	                            WHERE ((Evento.dataEventoInicio <= GETDATE() AND Evento.dataEventoFim >= GETDATE()) 
		                            OR (Evento.dataEventoInicio >= GETDATE()))
		                            AND ConteudoImprensa.ativo = 1
                            ) AS Q");

            DbCommand command = _db.GetSqlStringCommand(sbSql.ToString());
            _db.AddInParameter(command, "@usuarioId", DbType.Int32, usuarioId);
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
        /// <param name="registrosPagina"></param>
        /// <param name="numeroPagina"></param>
        /// <param name="ordemColunas"></param>
        /// <param name="ordemSentidos"></param>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        public IEnumerable<Evento> CarregarTodosEventosPorInteresseUsuarioId(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, Int32 usuarioId)
        {
            List<Evento> entidadesRetorno = new List<Evento>();

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
            else
            {
                sbOrder.Append(" ORDER BY eventoId");
            }


            if (registrosPagina > 0)
            {



                sbSql.Append("WITH Categorias (categoriaId, nomeCategoria, categoriaIdPai, nivel ) AS ( ");
                sbSql.Append("    SELECT C.categoriaId, C.nomeCategoria, C.categoriaIdPai, 0 AS Nivel ");
                sbSql.Append("    FROM Categoria AS C ");
                sbSql.Append("    UNION ALL ");
                sbSql.Append("    SELECT C.categoriaId, C.nomeCategoria, C.categoriaIdPai, Nivel+1 ");
                sbSql.Append("    FROM Categoria AS C ");
                sbSql.Append("    INNER JOIN Categorias AS CS ");
                sbSql.Append("    ON c.CategoriaIdPai = CS.categoriaId ) ");

                sbSql.Append("SELECT * FROM ( ");
                sbSql.Append("    SELECT P.*  ");
                sbSql.Append("        , ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R ");
                sbSql.Append("    FROM ( ");
                sbSql.Append("        SELECT distinct Evento.* ");
                sbSql.Append("        , ConteudoImprensa.* ");
                sbSql.Append("        , Conteudo.* ");
                sbSql.Append("        , Arquivo.*  ");
                sbSql.Append("        FROM Evento ");
                sbSql.Append("        INNER JOIN ConteudoImprensa ON Evento.eventoId=ConteudoImprensa.conteudoImprensaId ");
                sbSql.Append("        INNER JOIN Conteudo ON ConteudoImprensa.conteudoImprensaId=Conteudo.conteudoId ");
                sbSql.Append("        INNER JOIN ConteudoAreaConhecimento ON ConteudoAreaConhecimento.conteudoId = Conteudo.conteudoId ");
                sbSql.Append("        INNER JOIN Categorias ON Categorias.categoriaIdPai = ConteudoAreaConhecimento.categoriaId ");
                sbSql.Append("        INNER JOIN UsuarioInteresse ON UsuarioInteresse.categoriaId = Categorias.categoriaId AND UsuarioInteresse.usuarioId = @usuarioId ");
                sbSql.Append("        LEFT JOIN Arquivo ON Arquivo.arquivoId = Evento.arquivoIdThumb ");
                sbSql.Append("        WHERE ((Evento.dataEventoInicio <= GETDATE() AND Evento.dataEventoFim >= GETDATE()) ");
                sbSql.Append("            OR (Evento.dataEventoInicio >= GETDATE())) ");
                sbSql.Append("            AND ConteudoImprensa.ativo = 1 ");
                sbSql.Append("    ) as P ");

                sbSql.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSql.Append("WITH Categorias (categoriaId, nomeCategoria, categoriaIdPai, nivel ) AS ( ");
                sbSql.Append("    SELECT C.categoriaId, C.nomeCategoria, C.categoriaIdPai, 0 AS Nivel ");
                sbSql.Append("    FROM Categoria AS C ");
                sbSql.Append("    WHERE C.categoriaId IN ('1') ");
                sbSql.Append("    UNION ALL ");
                sbSql.Append("    SELECT C.categoriaId, C.nomeCategoria, C.categoriaIdPai, Nivel+1 ");
                sbSql.Append("    FROM Categoria AS C ");
                sbSql.Append("    INNER JOIN Categorias AS CS ");
                sbSql.Append("    ON c.CategoriaIdPai = CS.categoriaId ) ");
                sbSql.Append("SELECT DISTINCT Evento.* ");
                sbSql.Append(", ConteudoImprensa.* ");
                sbSql.Append(", Conteudo.* ");
                sbSql.Append(", Arquivo.* ");
                sbSql.Append("FROM Evento ");
                sbSql.Append("INNER JOIN ConteudoImprensa ON Evento.eventoId=ConteudoImprensa.conteudoImprensaId ");
                sbSql.Append("INNER JOIN Conteudo ON ConteudoImprensa.conteudoImprensaId=Conteudo.conteudoId ");
                sbSql.Append("INNER JOIN ConteudoAreaConhecimento ON ConteudoAreaConhecimento.conteudoId = Conteudo.conteudoId ");
                sbSql.Append("INNER JOIN Categorias ON Categorias.categoriaIdPai = ConteudoAreaConhecimento.categoriaId ");
                sbSql.Append("INNER JOIN UsuarioInteresse ON UsuarioInteresse.categoriaId = Categorias.categoriaId AND UsuarioInteresse.usuarioId = @usuarioId ");
                sbSql.Append("LEFT JOIN Arquivo ON Arquivo.arquivoId = Evento.arquivoIdThumb ");
                sbSql.Append("WHERE ((Evento.dataEventoInicio <= GETDATE() AND Evento.dataEventoFim >= GETDATE())  ");
                sbSql.Append("    OR (Evento.dataEventoInicio >= GETDATE())) ");
                sbSql.Append("    AND ConteudoImprensa.ativo = 1 ");

                if (sbOrder.Length > 0) { sbSql.Append(sbOrder.ToString()); }
            }

            DbCommand command = _db.GetSqlStringCommand(sbSql.ToString());
            _db.AddInParameter(command, "@usuarioId", DbType.Int32, usuarioId);
            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Evento entidadeRetorno = new Evento();
                PopulaEvento(reader, entidadeRetorno);
                entidadeRetorno.ConteudoImprensa = new ConteudoImprensa();
                ConteudoImprensaADO.PopulaConteudoImprensa(reader, entidadeRetorno.ConteudoImprensa);
                entidadeRetorno.ConteudoImprensa.Conteudo = new Conteudo();
                ConteudoADO.PopulaConteudo(reader, entidadeRetorno.ConteudoImprensa.Conteudo);
                if (reader["arquivoId"] != DBNull.Value)
                {
                    entidadeRetorno.ArquivoThumb = new Arquivo();
                    ArquivoADO.PopulaArquivo(reader, entidadeRetorno.ArquivoThumb);
                }

                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Evento> CarregarEventosParaEnviarAlerta()
        {
            List<Evento> entidadesRetorno = new List<Evento>();

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            sbSQL.AppendFormat(@"SELECT Evento.*,
	                                    EventoAlerta.eventoAlertaId,
	                                    EventoAlerta.usuarioId,
	                                    EventoAlerta.dias,
	                                    EventoAlerta.ativo,
	                                    EventoAlerta.dataHoraEncaminhamento,
	                                    EventoAlerta.dataHoraCancelamento,
	                                    Usuario.nomeUsuario,
	                                    Usuario.emailUsuario,
	                                    ConteudoImprensa.fonte, 
	                                    ConteudoImprensa.titulo
                                    FROM Evento
                                    INNER JOIN EventoAlerta ON EventoAlerta.eventoId = Evento.eventoId
                                    INNER JOIN Usuario ON Usuario.usuarioId = EventoAlerta.usuarioId
                                    INNER JOIN ConteudoImprensa ON Evento.eventoId=ConteudoImprensa.conteudoImprensaId
                                    WHERE CONVERT(VARCHAR, Evento.dataEventoInicio, 103) = CONVERT(VARCHAR, DATEADD(DAY, EventoAlerta.dias, GETDATE()), 103)
	                                    AND EventoAlerta.dataHoraEncaminhamento IS NULL
                                        AND ConteudoImprensa.ativo = 1
	                                    AND EventoAlerta.ativo = 1
	                                    AND Usuario.ativo = 1 ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Evento entidadeRetorno = new Evento();
                PopulaEventosAlertasCompletos(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }

            reader.Close();

            return entidadesRetorno;
        }

        public static void PopulaEventosAlertasCompletos(IDataReader reader, Evento entidade)
        {
            if (reader["eventoId"] != DBNull.Value)
            {
                entidade.EventoId = Convert.ToInt32(reader["eventoId"].ToString());
            }

            if (reader["local"] != DBNull.Value)
            {
                entidade.Local = reader["local"].ToString();
            }

            if (reader["dataEventoInicio"] != DBNull.Value)
            {
                entidade.DataEventoInicio = Convert.ToDateTime(reader["dataEventoInicio"].ToString());
            }

            if (reader["dataEventoFim"] != DBNull.Value)
            {
                entidade.DataEventoFim = Convert.ToDateTime(reader["dataEventoFim"].ToString());
            }

            ConteudoImprensa conteudoImprensa = new ConteudoImprensa();

            if (reader["fonte"] != DBNull.Value)
            {
                conteudoImprensa.Fonte = reader["fonte"].ToString();
            }

            if (reader["titulo"] != DBNull.Value)
            {
                conteudoImprensa.Titulo = reader["titulo"].ToString();
            }

            entidade.ConteudoImprensa = conteudoImprensa;

            EventoAlerta eventoAlerta = new EventoAlerta();

            if (reader["eventoAlertaId"] != DBNull.Value)
            {
                eventoAlerta.EventoAlertaId = Convert.ToInt32(reader["eventoAlertaId"].ToString());
            }

            if (reader["dias"] != DBNull.Value)
            {
                eventoAlerta.Dias = Convert.ToInt32(reader["dias"].ToString());
            }

            if (reader["ativo"] != DBNull.Value)
            {
                eventoAlerta.Ativo = Convert.ToBoolean(reader["ativo"].ToString());
            }

            if (reader["dataHoraEncaminhamento"] != DBNull.Value)
            {
                eventoAlerta.DataHoraEncaminhamento = Convert.ToDateTime(reader["dataHoraEncaminhamento"].ToString());
            }

            if (reader["dataHoraCancelamento"] != DBNull.Value)
            {
                eventoAlerta.DataHoraCancelamento = Convert.ToDateTime(reader["dataHoraCancelamento"].ToString());
            }

            if (reader["usuarioId"] != DBNull.Value)
            {
                if (eventoAlerta.Usuario == null) eventoAlerta.Usuario = new Usuario();
                eventoAlerta.Usuario.UsuarioId = Convert.ToInt32(reader["usuarioId"].ToString());
            }

            if (reader["nomeUsuario"] != DBNull.Value)
            {
                if (eventoAlerta.Usuario == null) eventoAlerta.Usuario = new Usuario();
                eventoAlerta.Usuario.NomeUsuario = reader["nomeUsuario"].ToString();
            }

            if (reader["emailUsuario"] != DBNull.Value)
            {
                if (eventoAlerta.Usuario == null) eventoAlerta.Usuario = new Usuario();
                eventoAlerta.Usuario.EmailUsuario = reader["emailUsuario"].ToString();
            }

            entidade.EventoAlertas = new List<EventoAlerta>();
            entidade.EventoAlertas.Add(eventoAlerta);
        }
    }
}