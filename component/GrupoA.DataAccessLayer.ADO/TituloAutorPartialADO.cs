
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
    public partial class TituloAutorADO
    {

        /// <summary>
        /// Método que remove um TituloAutor da base de dados.
        /// </summary>
        /// <param name="entidade">TituloAutor a ser excluído (somente o identificador é necessário).</param>		
        public void ExcluirTituloAutor(TituloAutor entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;
            sbSQL.Append("DELETE FROM TituloAutor ");
            sbSQL.Append("WHERE tituloId=@tituloId");
            command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@tituloId", DbType.Int32, entidade.Titulo.TituloId);
            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que carrega os autores de um título.
        /// </summary>
        /// <param name="titulo">Titulo com identificador configurado.</param>
        /// <returns></returns>
        public IEnumerable<TituloAutor> Carregar(Titulo titulo)
        {
            List<TituloAutor> entidadeRetorno = new List<TituloAutor>();

            StringBuilder sbSql = new StringBuilder();

            sbSql.Append("SELECT * FROM TituloAutor WHERE tituloId=@tituloId");
            sbSql.Append(" ORDER BY ordem ");

            DbCommand command = _db.GetSqlStringCommand(sbSql.ToString());
            _db.AddInParameter(command, "@tituloId", DbType.Int32, titulo.TituloId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                TituloAutor entidade = new TituloAutor();
                PopulaTituloAutor(reader, entidade);
                entidadeRetorno.Add(entidade);
            }
            reader.Close();


            return entidadeRetorno;
        }

        /// <summary>
        /// Método que carrega os autores de um título.
        /// </summary>
        /// <param name="titulo">Titulo com identificador configurado.</param>
        /// <returns></returns>
        public List<TituloAutor> CarregarComDependencias(Titulo titulo)
        {
            List<TituloAutor> entidadeRetorno = new List<TituloAutor>();

            StringBuilder sbSql = new StringBuilder();

            sbSql.Append("SELECT tituloid, ");
            sbSql.Append("principal, ");
            sbSql.Append("a.* ");
            sbSql.Append("FROM TituloAutor t ");
            sbSql.Append("JOIN Autor a ");
            sbSql.Append("ON t.autorId = a.autorId ");
            sbSql.Append("WHERE tituloId=@tituloId ");
            sbSql.Append(" ORDER BY t.ordem");

            DbCommand command = _db.GetSqlStringCommand(sbSql.ToString());
            _db.AddInParameter(command, "@tituloId", DbType.Int32, titulo.TituloId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                TituloAutor entidade = new TituloAutor();
                PopulaTituloAutorComDependencias(reader, entidade);
                entidadeRetorno.Add(entidade);
            }
            reader.Close();


            return entidadeRetorno;
        }

        /// <summary>
        /// Método que retorna popula um TituloAutor baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">TituloAutor a ser populado(.</param>
        public static void PopulaTituloAutorComDependencias(IDataReader reader, TituloAutor entidade)
        {
            if (reader["principal"] != DBNull.Value)
                entidade.Principal = Convert.ToBoolean(reader["principal"].ToString());

            if (reader["tituloId"] != DBNull.Value)
            {
                entidade.Titulo = new Titulo();
                entidade.Titulo.TituloId = Convert.ToInt32(reader["tituloId"].ToString());
            }

            if (reader["autorId"] != DBNull.Value)
            {
                if (entidade.Autor == null) entidade.Autor = new Autor();
                entidade.Autor.AutorId = Convert.ToInt32(reader["autorId"].ToString());
            }

            if (reader["url"] != DBNull.Value)
            {
                if (entidade.Autor == null) entidade.Autor = new Autor();
                entidade.Autor.Url = reader["url"].ToString();
            }

            if (reader["email"] != DBNull.Value)
            {
                if (entidade.Autor == null) entidade.Autor = new Autor();
                entidade.Autor.Email = reader["email"].ToString();
            }

            if (reader["blog"] != DBNull.Value)
            {
                if (entidade.Autor == null) entidade.Autor = new Autor();
                entidade.Autor.Blog = reader["blog"].ToString();
            }

            if (reader["nomeAutor"] != DBNull.Value)
            {
                if (entidade.Autor == null) entidade.Autor = new Autor();
                entidade.Autor.NomeAutor = reader["nomeAutor"].ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tituloBO"></param>
        /// <param name="autorBO"></param>
        /// <returns></returns>
        public bool TituloAutorRelacionado(Titulo tituloBO, Autor autorBO)
        {
            bool entidadeRetorno = false;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(@"SELECT
								COUNT(*) AS Total
							FROM 
								TituloAutor
							WHERE
                                TituloAutor.tituloId = @tituloId
								AND TituloAutor.autorId = @autorId
                            ");
            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@tituloId", DbType.Int32, tituloBO.TituloId);
            _db.AddInParameter(command, "@autorId", DbType.Int32, autorBO.AutorId);
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
        /// <param name="titulo"></param>
        public void ExcluirTodosPorTitulo(Titulo titulo)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM TituloAutor ");
            sbSQL.Append("WHERE tituloId = @tituloId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@tituloId", DbType.Int32, titulo.TituloId);

            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tituloBO"></param>
        /// <param name="arquivoBO"></param>
        /// <returns></returns>
        public bool TituloArquivoRelacionado(Titulo tituloBO, Arquivo arquivoBO)
        {
            bool entidadeRetorno = false;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(@"SELECT
								COUNT(*) AS Total
							FROM 
								TituloConteudoExtraArquivo
							WHERE
                                TituloConteudoExtraArquivo.tituloId = @tituloId
								AND TituloConteudoExtraArquivo.arquivoId = @arquivoId
                            ");
            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@tituloId", DbType.Int32, tituloBO.TituloId);
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
    }
}
