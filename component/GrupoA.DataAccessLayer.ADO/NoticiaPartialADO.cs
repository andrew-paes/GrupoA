
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
    public partial class NoticiaADO : ADOSuper, INoticiaDAL
    {

        /// <summary>
        /// Método que retorna uma coleção de Noticia com conteudo Imprensa.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos Noticia.</returns>
        public IEnumerable<Noticia> CarregarTodosValidosComDependencias(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {
            List<Noticia> noticias = new List<Noticia>();

            StringBuilder sbSql = new StringBuilder();
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
                sbOrder.Append(" ORDER BY noticiaId");
            }


            if (registrosPagina > 0)
            {

                sbSql.Append("SELECT * FROM ( ");

                sbSql.Append("SELECT Noticia.* ");
                sbSql.Append(", ConteudoImprensa.* ");
                sbSql.Append(", Conteudo.* ");
                sbSql.Append(", Arquivo.* ");
                sbSql.Append(", CategoriaNoticia.nomeCategoriaNoticia ");
                sbSql.Append(", ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R ");
                sbSql.Append(" FROM Noticia");
                sbSql.Append(" INNER JOIN ConteudoImprensa ON Noticia.noticiaId=ConteudoImprensa.conteudoImprensaId");
                sbSql.Append(" INNER JOIN Conteudo ON ConteudoImprensa.conteudoImprensaId=Conteudo.conteudoId");
                sbSql.Append(" INNER JOIN CategoriaNoticia ON CategoriaNoticia.categoriaNoticiaId=Noticia.categoriaNoticiaId");
                sbSql.Append(" LEFT JOIN Arquivo ON Arquivo.arquivoId=Noticia.arquivoIdThumb");
                sbSql.Append(" WHERE (GETDATE() >= ISNULL(dbo.ConteudoImprensa.dataExibicaoInicio, GETDATE()) ");
                sbSql.Append("AND GETDATE() <= ISNULL(dbo.ConteudoImprensa.dataExibicaoFim, GETDATE())) ");
                sbSql.Append("AND ConteudoImprensa.ativo = 1 ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                {
                    sbSql.Append(string.Concat(" AND ", filtro.GetWhereString()));
                }
                sbSql.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSql.Append("SELECT Noticia.* ");
                sbSql.Append(", ConteudoImprensa.* ");
                sbSql.Append(", Conteudo.* ");
                sbSql.Append(", Arquivo.* ");
                sbSql.Append(", CategoriaNoticia.* ");
                sbSql.Append(" FROM Noticia");
                sbSql.Append(" INNER JOIN ConteudoImprensa ON Noticia.noticiaId=ConteudoImprensa.conteudoImprensaId");
                sbSql.Append(" INNER JOIN Conteudo ON ConteudoImprensa.conteudoImprensaId=Conteudo.conteudoId");
                sbSql.Append(" INNER JOIN CategoriaNoticia ON CategoriaNoticia.categoriaNoticiaId=Noticia.categoriaNoticiaId");
                sbSql.Append(" LEFT JOIN Arquivo ON Arquivo.arquivoId=Noticia.arquivoIdThumb");
                sbSql.Append(" WHERE (GETDATE() >= ISNULL(dbo.ConteudoImprensa.dataExibicaoInicio, GETDATE()) ");
                sbSql.Append("AND GETDATE() <= ISNULL(dbo.ConteudoImprensa.dataExibicaoFim, GETDATE())) ");
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
                Noticia entidadeRetorno = new Noticia();
                PopulaNoticia(reader, entidadeRetorno);
                entidadeRetorno.ConteudoImprensa = new ConteudoImprensa();
                ConteudoImprensaADO.PopulaConteudoImprensa(reader, entidadeRetorno.ConteudoImprensa);
                entidadeRetorno.ConteudoImprensa.Conteudo = new Conteudo();
                ConteudoADO.PopulaConteudo(reader, entidadeRetorno.ConteudoImprensa.Conteudo);
                entidadeRetorno.ArquivoThumb = new Arquivo();
                if (reader["arquivoId"] != DBNull.Value)
                {
                    ArquivoADO.PopulaArquivo(reader, entidadeRetorno.ArquivoThumb);
                    entidadeRetorno.CategoriaNoticia = new CategoriaNoticia();
                }
                CategoriaNoticiaADO.PopulaCategoriaNoticia(reader, entidadeRetorno.CategoriaNoticia);

                noticias.Add(entidadeRetorno);
            }
            reader.Close();

            return noticias;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int ContarTodosValidosComDependencias()
        {
            int total = 0;

            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("SELECT COUNT(NoticiaId) total ");
            sbSql.Append(" FROM Noticia");
            sbSql.Append(" INNER JOIN ConteudoImprensa ON Noticia.noticiaId=ConteudoImprensa.conteudoImprensaId");
            sbSql.Append(" INNER JOIN Conteudo ON ConteudoImprensa.conteudoImprensaId=Conteudo.conteudoId");
            sbSql.Append(" INNER JOIN CategoriaNoticia ON CategoriaNoticia.categoriaNoticiaId=Noticia.categoriaNoticiaId");
            sbSql.Append(" LEFT JOIN Arquivo ON Arquivo.arquivoId=Noticia.arquivoIdThumb");
            sbSql.Append(" WHERE (GETDATE() >= ISNULL(dbo.ConteudoImprensa.dataExibicaoInicio, GETDATE()) ");
            sbSql.Append("AND GETDATE() <= ISNULL(dbo.ConteudoImprensa.dataExibicaoFim, GETDATE())) ");
            sbSql.Append("AND ConteudoImprensa.ativo = 1 ");


            DbCommand command = _db.GetSqlStringCommand(sbSql.ToString());
            IDataReader reader = _db.ExecuteReader(command);

            if ((reader.Read()) && (reader["total"] != null))
            {
                total = Int32.Parse(reader["total"].ToString());
            }
            reader.Close();

            return total;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="palavra"></param>
        /// <returns></returns>
        public int ContarNoticiaBusca(string palavra)
        {
            int total = 0;

            StringBuilder sbSql = new StringBuilder();

            sbSql.Append(@"SELECT 
								COUNT(F.NoticiaId) Total
							FROM
								(
								SELECT
									R.*
									, ROW_NUMBER() OVER ( ORDER BY RANKT DESC ) AS RowNumber
								FROM
									(
									SELECT
										N.*
										, CI.*
										, ISNULL(R1.RANK, 0) + ISNULL(R2.RANK, 0) AS RANKT
									FROM
										Noticia N
										INNER JOIN ConteudoImprensa CI ON CI.conteudoImprensaId = N.noticiaId
										LEFT JOIN CONTAINSTABLE(ConteudoImprensa, *, @palavra) AS R1 ON R1.[KEY] = CI.conteudoImprensaId
										LEFT JOIN CONTAINSTABLE(Noticia, *, @palavra) AS R2 ON R2.[KEY] = N.noticiaId
									WHERE
										CI.Ativo = 1
										AND GETDATE() BETWEEN ISNULL(dataExibicaoInicio,GETDATE()) and ISNULL(dataExibicaoFim,GETDATE())
										AND ( R1.[RANK] IS NOT NULL OR R2.[RANK] IS NOT NULL
									)
								) AS R
							) AS F
							");

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
        public List<Noticia> CarregarNoticiaBusca(int registrosPagina, int numeroPagina, String[] ordenacao, String[] ordenacaoSentido, String palavra)
        {
            List<Noticia> entidadesRetorno = new List<Noticia>();

            string iniRegister = (((numeroPagina - 1) * registrosPagina) + 1).ToString();
            string endRegister = ((numeroPagina) * registrosPagina).ToString();

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            sbSQL.Append(@"SELECT 
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
											N.*
											, CI.*
											, ISNULL(R1.RANK, 0) + ISNULL(R2.RANK, 0) AS RANKT
										FROM
											Noticia N
											INNER JOIN ConteudoImprensa CI ON CI.conteudoImprensaId = N.noticiaId
											LEFT JOIN CONTAINSTABLE(ConteudoImprensa, *, @palavra) AS R1 ON R1.[KEY] = CI.conteudoImprensaId
											LEFT JOIN CONTAINSTABLE(Noticia, *, @palavra) AS R2 ON R2.[KEY] = N.noticiaId
										WHERE
											CI.Ativo = 1
											AND GETDATE() BETWEEN ISNULL(dataExibicaoInicio,GETDATE()) and ISNULL(dataExibicaoFim,GETDATE())
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
                Noticia entidadeRetorno = new Noticia();
                PopulaNoticia(reader, entidadeRetorno);

                entidadeRetorno.ConteudoImprensa = new ConteudoImprensa();
                ConteudoImprensaADO.PopulaConteudoImprensa(reader, entidadeRetorno.ConteudoImprensa);

                if (entidadeRetorno.CategoriaNoticia != null && entidadeRetorno.CategoriaNoticia.CategoriaNoticiaId > 0)
                {
                    entidadeRetorno.CategoriaNoticia = new CategoriaNoticiaADO().Carregar(entidadeRetorno.CategoriaNoticia);
                }

                entidadesRetorno.Add(entidadeRetorno);
            }

            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns></returns>
        public List<Noticia> CarregarNoticiasPorCategoria(Categoria categoria, Int32 qtdRegistros)
        {
            List<Noticia> entidadesRetorno = new List<Noticia>();

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            sbSQL.AppendFormat(String.Concat("SELECT TOP ", qtdRegistros));
            sbSQL.AppendFormat("    Noticia.noticiaId, ");
            sbSQL.AppendFormat("    ConteudoImprensa.titulo, ");
            sbSQL.AppendFormat("    ConteudoImprensa.fonte, ");
            sbSQL.AppendFormat("    Convert(varchar(20), Conteudo.dataHoraCadastro, 103) + ' ' + Convert(varchar(20), Conteudo.dataHoraCadastro, 108) as dataHoraCadastro ");
            sbSQL.AppendFormat("FROM Noticia ");
            sbSQL.AppendFormat("INNER JOIN ConteudoImprensa ");
            sbSQL.AppendFormat("    ON Noticia.noticiaId = ConteudoImprensa.conteudoImprensaId ");
            sbSQL.AppendFormat("INNER JOIN Conteudo ");
            sbSQL.AppendFormat("    ON Noticia.noticiaId = Conteudo.conteudoId ");
            if (categoria != null)
            {
                sbSQL.AppendFormat("INNER JOIN ConteudoAreaConhecimento ");
                sbSQL.AppendFormat("    ON Conteudo.conteudoId = ConteudoAreaConhecimento.conteudoId ");
                sbSQL.AppendFormat("WHERE ConteudoAreaConhecimento.categoriaId = @categoriaId ");
            }
            sbSQL.AppendFormat("ORDER BY Conteudo.dataHoraCadastro DESC ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            if (categoria != null)
            {
                _db.AddInParameter(command, "@categoriaId", DbType.String, categoria.CategoriaId);
            }

            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Noticia entidadeRetorno = new Noticia();
                PopulaUltimasNoticias(reader, entidadeRetorno);

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
        public static void PopulaUltimasNoticias(IDataReader reader, Noticia entidade)
        {
            if (reader["noticiaId"] != DBNull.Value)
            {
                entidade.NoticiaId = Convert.ToInt32(reader["noticiaId"].ToString());
            }

            if (reader["titulo"] != DBNull.Value)
            {
                if (entidade.ConteudoImprensa == null) entidade.ConteudoImprensa = new ConteudoImprensa();
                entidade.ConteudoImprensa.Titulo = reader["titulo"].ToString();
            }

            if (reader["fonte"] != DBNull.Value)
            {
                if (entidade.ConteudoImprensa == null) entidade.ConteudoImprensa = new ConteudoImprensa();
                entidade.ConteudoImprensa.Fonte = reader["fonte"].ToString();
            }

            if (reader["dataHoraCadastro"] != DBNull.Value)
            {
                if (entidade.ConteudoImprensa == null) entidade.ConteudoImprensa = new ConteudoImprensa();
                if (entidade.ConteudoImprensa.Conteudo == null) entidade.ConteudoImprensa.Conteudo = new Conteudo();
                entidade.ConteudoImprensa.Conteudo.DataHoraCadastro = Convert.ToDateTime(reader["dataHoraCadastro"].ToString());
            }

        }
    }
}
