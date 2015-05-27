
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
    public partial class AutorADO : ADOSuper, IAutorDAL
    {

        /// <summary>
        /// Método que persiste um Autor.
        /// </summary>
        /// <param name="entidade">Autor contendo os dados a serem persistidos.</param>	
        public void Inserir(Autor entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO Autor ");
            sbSQL.Append(" (url, email, blog, nomeAutor, codigoLegado, biografia, arquivoIdImagem) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@url, @email, @blog, @nomeAutor, @codigoLegado, @biografia, @arquivoIdImagem) ");

            sbSQL.Append(" ; SET @autorId = SCOPE_IDENTITY(); ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddOutParameter(command, "@autorId", DbType.Int32, 8);

            if (entidade.Url != null)
                _db.AddInParameter(command, "@url", DbType.String, entidade.Url);
            else
                _db.AddInParameter(command, "@url", DbType.String, null);

            _db.AddInParameter(command, "@email", DbType.String, entidade.Email);

            if (entidade.Blog != null)
                _db.AddInParameter(command, "@blog", DbType.String, entidade.Blog);
            else
                _db.AddInParameter(command, "@blog", DbType.String, null);

            _db.AddInParameter(command, "@nomeAutor", DbType.String, entidade.NomeAutor);

            if (entidade.CodigoLegado != null)
                _db.AddInParameter(command, "@codigoLegado", DbType.String, entidade.CodigoLegado);
            else
                _db.AddInParameter(command, "@codigoLegado", DbType.String, null);

            if (entidade.Biografia != null)
                _db.AddInParameter(command, "@biografia", DbType.String, entidade.Biografia);
            else
                _db.AddInParameter(command, "@biografia", DbType.String, null);

            if (entidade.ArquivoImagem != null)
                _db.AddInParameter(command, "@arquivoIdImagem", DbType.Int32, entidade.ArquivoImagem.ArquivoId);
            else
                _db.AddInParameter(command, "@arquivoIdImagem", DbType.Int32, null);


            // Executa a query.
            _db.ExecuteNonQuery(command);

            entidade.AutorId = Convert.ToInt32(_db.GetParameterValue(command, "@autorId"));

        }

        /// <summary>
        /// Método que atualiza os dados de um Autor.
        /// </summary>
        /// <param name="entidade">Autor contendo os dados a serem atualizados.</param>
        public void Atualizar(Autor entidade)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE Autor SET ");
            sbSQL.Append(" url=@url, email=@email, blog=@blog, nomeAutor=@nomeAutor, codigoLegado=@codigoLegado, biografia=@biografia, arquivoIdImagem=@arquivoIdImagem ");
            sbSQL.Append(" WHERE autorId=@autorId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@autorId", DbType.Int32, entidade.AutorId);
            if (entidade.Url != null)
                _db.AddInParameter(command, "@url", DbType.String, entidade.Url);
            else
                _db.AddInParameter(command, "@url", DbType.String, null);
            _db.AddInParameter(command, "@email", DbType.String, entidade.Email);
            if (entidade.Blog != null)
                _db.AddInParameter(command, "@blog", DbType.String, entidade.Blog);
            else
                _db.AddInParameter(command, "@blog", DbType.String, null);
            _db.AddInParameter(command, "@nomeAutor", DbType.String, entidade.NomeAutor);
            if (entidade.CodigoLegado != null)
                _db.AddInParameter(command, "@codigoLegado", DbType.String, entidade.CodigoLegado);
            else
                _db.AddInParameter(command, "@codigoLegado", DbType.String, null);
            if (entidade.Biografia != null)
                _db.AddInParameter(command, "@biografia", DbType.String, entidade.Biografia);
            else
                _db.AddInParameter(command, "@biografia", DbType.String, null);
            if (entidade.ArquivoImagem != null)
                _db.AddInParameter(command, "@arquivoIdImagem", DbType.Int32, entidade.ArquivoImagem.ArquivoId);
            else
                _db.AddInParameter(command, "@arquivoIdImagem", DbType.Int32, null);

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que remove um Autor da base de dados.
        /// </summary>
        /// <param name="entidade">Autor a ser excluído (somente o identificador é necessário).</param>		
        public void Excluir(Autor entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM Autor ");
            sbSQL.Append("WHERE autorId=@autorId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@autorId", DbType.Int32, entidade.AutorId);


            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que carrega um Autor.
        /// </summary>
        /// <param name="entidade">Autor a ser carregado (somente o identificador é necessário).</param>
        /// <returns>Autor</returns>
        public Autor Carregar(int autorId)
        {
            Autor entidade = new Autor();
            entidade.AutorId = autorId;
            return Carregar(entidade);

        }


        /// <summary>
        /// Método que carrega um Autor.
        /// </summary>
        /// <param name="entidade">Autor a ser carregado (somente o identificador é necessário).</param>
        /// <returns>Autor</returns>
        public Autor Carregar(Autor entidade)
        {

            Autor entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM Autor WHERE autorId=@autorId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@autorId", DbType.Int32, entidade.AutorId);

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
        /// Método que retorna uma coleção de Autor.
        /// </summary>
        /// <param name="entidade">Capitulo relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Autor.</returns>
        public IEnumerable<Autor> Carregar(Capitulo entidade)
        {
            List<Autor> entidadesRetorno = new List<Autor>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Autor.* FROM Autor INNER JOIN CapituloAutor ON Autor.autorId=CapituloAutor.autorId WHERE CapituloAutor.capituloId=@capituloId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@capituloId", DbType.Int32, entidade.CapituloId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Autor entidadeRetorno = new Autor();
                PopulaAutor(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de Autor.
        /// </summary>
        /// <param name="entidade">Arquivo relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Autor.</returns>
        public IEnumerable<Autor> Carregar(Arquivo entidade)
        {
            List<Autor> entidadesRetorno = new List<Autor>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Autor.* FROM Autor WHERE Autor.arquivoId=@arquivoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.ArquivoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Autor entidadeRetorno = new Autor();
                PopulaAutor(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }


        /// <summary>
        /// Método que retorna uma coleção de Autor.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos Autor.</returns>
        public IEnumerable<Autor> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {

            List<Autor> entidadesRetorno = new List<Autor>();

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
                sbOrder.Append(" ORDER BY autorId");
            }


            if (registrosPagina > 0)
            {

                //sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM Autor");
                //if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Autor WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
                //} else {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Autor ORDER BY " + orderBy + ")");				
                //}	
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT Autor.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM Autor ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSQL.Append("SELECT Autor.* FROM Autor ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Autor entidadeRetorno = new Autor();
                PopulaAutor(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna todas os Autor existentes na base de dados.
        /// </summary>
        public IEnumerable<Autor> CarregarTodos()
        {
            return CarregarTodos(0, 0, null, null, null);
        }

        /// <summary>
        /// Método que retorna o total de Autor na base de dados.
        /// </summary>
        /// <returns></returns>
        public int TotalRegistros()
        {
            return TotalRegistros(null);
        }

        /// <summary>
        /// Método que retorna o total de Autor na base de dados, aceita filtro.
        /// </summary>
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
        /// <returns></returns>
        public int TotalRegistros(IFilterHelper filtro)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) AS Total FROM Autor");

            if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Executa a query.

            int resultado = (int)_db.ExecuteScalar(command);


            return resultado;
        }

        /// <summary>
        /// Método que retorna popula um Autor baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Autor a ser populado(.</param>
        public static void PopulaAutor(IDataReader reader, Autor entidade)
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
                entidade.ArquivoImagem = new Arquivo();
                entidade.ArquivoImagem.ArquivoId = Convert.ToInt32(reader["arquivoIdImagem"].ToString());
            }


        }

    }
}
