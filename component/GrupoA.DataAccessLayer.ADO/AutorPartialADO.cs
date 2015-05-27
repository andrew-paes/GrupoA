using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Xml.Linq;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using GrupoA.BusinessObject;
using GrupoA.FilterHelper;

namespace GrupoA.DataAccess.ADO
{
    public partial class AutorADO : ADOSuper, IAutorDAL
    {
        /// <summary>
        /// Método que carrega os autores de um título.
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>

        public List<Autor> CarregarAutores(Titulo entidade)
        {
            return this.CarregarAutores(entidade, 0);
        }

        public List<Autor> CarregarAutores(Titulo entidade, Int32 principal)
        {
            List<Autor> entidadesRetorno = new List<Autor>();
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("SELECT Autor.* FROM TituloAutor ");
            sbSql.Append("JOIN Autor ON Autor.autorId = TituloAutor.autorId ");
            sbSql.Append("WHERE tituloId=@tituloId ");
            if (principal > 0)
            {
                sbSql.Append(" AND principal = @principal ");
            }
            sbSql.Append(" ORDER BY ordem ");

            DbCommand command = _db.GetSqlStringCommand(sbSql.ToString());
            _db.AddInParameter(command, "@tituloId", DbType.Int32, entidade.TituloId);
            if (principal > 0)
            {
                _db.AddInParameter(command, "@principal", DbType.Int32, principal);
            }
            IDataReader reader = _db.ExecuteReader(command);
            while (reader.Read())
            {
                Autor entidadeRetorno = new Autor();
                AutorADO.PopulaAutor(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();
            return entidadesRetorno;
        }

        public List<Autor> CarregarAutores(Titulo entidade, Autor autor)
        {
            List<Autor> entidadesRetorno = new List<Autor>();
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("SELECT Autor.* FROM TituloAutor ");
            sbSql.Append("JOIN Autor ON Autor.autorId = TituloAutor.autorId ");
            sbSql.Append("WHERE tituloId=@tituloId ");
            if (autor != null)
            {
                sbSql.Append("AND Autor.nomeAutor LIKE @nomeAutor ");
            }
            sbSql.Append(" ORDER BY ordem ");

            DbCommand command = _db.GetSqlStringCommand(sbSql.ToString());
            _db.AddInParameter(command, "@tituloId", DbType.Int32, entidade.TituloId);
            if (autor != null)
            {
                _db.AddInParameter(command, "@nomeAutor", DbType.String, "%" + autor.NomeAutor + "%");
            }
            IDataReader reader = _db.ExecuteReader(command);
            while (reader.Read())
            {
                Autor entidadeRetorno = new Autor();
                AutorADO.PopulaAutor(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();
            return entidadesRetorno;
        }

        public List<Autor> CarregarAutores(Produto produto)
        {
            List<Autor> entidadesRetorno = new List<Autor>();
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append(" SELECT a.* FROM Produto p ");
            sbSql.Append(" JOIN TituloImpresso ti ON ti.tituloImpressoId = p.produtoId ");
            sbSql.Append(" JOIN TituloAutor ta ON ta.tituloId = ti.tituloId ");
            sbSql.Append(" JOIN Autor a ON a.autorId = ta.autorId ");
            sbSql.Append(" WHERE p.produtoId = @produtoId ");
            sbSql.Append(" UNION ALL ");
            sbSql.Append(" SELECT a.* FROM Produto p ");
            sbSql.Append(" JOIN TituloEletronico te ON te.tituloEletronicoId = p.produtoId ");
            sbSql.Append(" JOIN TituloAutor ta ON ta.tituloId = te.tituloId ");
            sbSql.Append(" JOIN Autor a ON a.autorId = ta.autorId ");
            sbSql.Append(" WHERE p.produtoId = @produtoId ");
            sbSql.Append(" UNION ALL ");
            sbSql.Append(" SELECT a.* FROM Produto p ");
            sbSql.Append(" JOIN CapituloImpresso ci ON ci.capituloId = p.produtoId ");
            sbSql.Append(" JOIN TituloImpresso ti ON ti.tituloImpressoId = ci.tituloImpressoId ");
            sbSql.Append(" JOIN TituloAutor ta ON ta.tituloId = ti.tituloId ");
            sbSql.Append(" JOIN Autor a ON a.autorId = ta.autorId ");
            sbSql.Append(" WHERE p.produtoId = @produtoId ");
            sbSql.Append(" UNION ALL ");

            sbSql.Append(" SELECT a.* FROM Produto p ");
            sbSql.Append(" JOIN CapituloEletronico ce ON ce.capituloId = p.produtoId ");
            sbSql.Append(" JOIN TituloEletronico te ON te.tituloEletronicoId = ce.tituloEletronicoId ");
            sbSql.Append(" JOIN TituloAutor ta ON ta.tituloId = te.tituloId ");
            sbSql.Append(" JOIN Autor a ON a.autorId = ta.autorId ");
            sbSql.Append(" WHERE p.produtoId = @produtoId ");

            DbCommand command = _db.GetSqlStringCommand(sbSql.ToString());
            _db.AddInParameter(command, "@produtoId", DbType.Int32, produto.ProdutoId);
            IDataReader reader = _db.ExecuteReader(command);
            while (reader.Read())
            {
                Autor entidadeRetorno = new Autor();
                AutorADO.PopulaAutor(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();
            return entidadesRetorno;
        }

        /// <summary>
        /// Método que carrega um Autor.
        /// </summary>
        /// <param name="entidade">Autor a ser carregado (somente o identificador é necessário).</param>
        /// <returns>Autor</returns>
        public Autor CarregarAutorCodigoLegado(Autor entidade)
        {
            Autor entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM Autor WHERE codigoLegado=@codigoLegado");
            sbSQL.Append(" ORDER BY ordem ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@codigoLegado", DbType.String, entidade.CodigoLegado);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Autor();
                PopulaAutor(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="autor"></param>
        /// <returns></returns>
        public Autor CarregarAutorComDependencia(Autor autor)
        {
            Autor entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Autor.*, ");
            sbSQL.Append("    Arquivo.nomeArquivoOriginal, ");
            sbSQL.Append("    Arquivo.nomeArquivo ");
            sbSQL.Append("FROM Autor ");
            sbSQL.Append("LEFT JOIN Arquivo ");
            sbSQL.Append("    ON Autor.arquivoIdImagem = Arquivo.arquivoId ");
            sbSQL.Append("WHERE autorId=@autorId ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@autorId", DbType.Int32, autor.AutorId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Autor();
                PopulaAutorComDependencia(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// Método que retorna popula um Autor baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Autor a ser populado(.</param>
        public static void PopulaAutorComDependencia(IDataReader reader, Autor entidade)
        {
            if (reader["autorId"] != DBNull.Value)
                entidade.AutorId = Convert.ToInt32(reader["autorId"].ToString());

            if (reader["url"] != DBNull.Value)
                entidade.Url = reader["url"].ToString();

            if (reader["email"] != DBNull.Value)
                entidade.Email = reader["email"].ToString();

            if (reader["blog"] != DBNull.Value)
                entidade.Blog = reader["blog"].ToString();

            if (reader["nomeAutor"] != DBNull.Value)
                entidade.NomeAutor = reader["nomeAutor"].ToString();

            if (reader["codigoLegado"] != DBNull.Value)
                entidade.CodigoLegado = reader["codigoLegado"].ToString();

            if (reader["biografia"] != DBNull.Value)
                entidade.Biografia = reader["biografia"].ToString();

            if (reader["arquivoIdImagem"] != DBNull.Value)
            {
                if (entidade.ArquivoImagem == null) entidade.ArquivoImagem = new Arquivo();
                entidade.ArquivoImagem.ArquivoId = Convert.ToInt32(reader["arquivoIdImagem"].ToString());
            }

            if (reader["nomeArquivoOriginal"] != DBNull.Value)
            {
                if (entidade.ArquivoImagem == null) entidade.ArquivoImagem = new Arquivo();
                entidade.ArquivoImagem.NomeArquivoOriginal = reader["nomeArquivoOriginal"].ToString();
            }

            if (reader["nomeArquivo"] != DBNull.Value)
            {
                if (entidade.ArquivoImagem == null) entidade.ArquivoImagem = new Arquivo();
                entidade.ArquivoImagem.NomeArquivo = reader["nomeArquivo"].ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <param name="titulo"></param>
        /// <returns></returns>
        public List<Autor> CarregarAutoresPorNome(Autor entidade, Titulo titulo)
        {
            List<Autor> entidadesRetorno = new List<Autor>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT autorId, ");
            sbSQL.Append("    nomeAutor ");
            sbSQL.Append("FROM Autor A ");
            sbSQL.Append("WHERE nomeAutor like @nomeAutor ");
            sbSQL.Append("      AND NOT autorId IN ( ");
            sbSQL.Append("        SELECT autorId ");
            sbSQL.Append("        FROM TituloAutor ");
            sbSQL.Append("        WHERE tituloId = @tituloId ");
            sbSQL.Append("    ) ");
            sbSQL.Append("ORDER BY nomeAutor ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@nomeAutor", DbType.String, "%" + entidade.NomeAutor + "%");
            _db.AddInParameter(command, "@tituloId", DbType.Int32, titulo.TituloId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Autor produto = new Autor();
                produto.AutorId = Convert.ToInt32(reader["autorId"].ToString());
                produto.NomeAutor = reader["nomeAutor"].ToString();

                entidadesRetorno.Add(produto);
            }
            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public Autor CarregarAutorPorNome(Autor entidade)
        {
            Autor entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM Autor WHERE nomeAutor=@nomeAutor");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@nomeAutor", DbType.String, entidade.NomeAutor);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Autor();
                PopulaAutor(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }
    }
}