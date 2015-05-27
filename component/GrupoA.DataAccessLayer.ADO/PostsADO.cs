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
	public partial class PostsADO : ADOSuper, IPostsDAL
	{
		/// <summary>
		/// Método que carrega um Posts.
		/// </summary>
		/// <param name="entidade">Posts a ser carregado (somente o identificador é necessário).</param>
		/// <returns>Posts</returns>
		public Posts Carregar(Posts entidade)
		{
			Posts entidadeRetorno = null;

			StringBuilder sbSQL = new StringBuilder();

			sbSQL.Append("SELECT * FROM be_Posts WHERE postId=@postId");

			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

			_db.AddInParameter(command, "@postId", DbType.Guid, entidade.PostId);

			IDataReader reader = _db.ExecuteReader(command);

			if (reader.Read())
			{
				entidadeRetorno = new Posts();
				PopulaPosts(reader, entidadeRetorno);
			}

			reader.Close();

			return entidadeRetorno;
		}

		/// <summary>
		/// Método que retorna uma coleção de Posts.
		/// </summary>
		/// <param name="registrosPagina">Número máximo de registros na página.</param>
		/// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
		/// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
		/// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos Posts.</returns>
		public List<Posts> CarregarTodos(int quantidadeRegistros)
		{
			List<Posts> entidadesRetorno = new List<Posts>();

			StringBuilder sbSQL = new StringBuilder();
			DbCommand command;
			IDataReader reader;

			sbSQL.Append("SELECT ");

			if (quantidadeRegistros != null && quantidadeRegistros > 0)
				sbSQL.Append(" TOP " + quantidadeRegistros);

			sbSQL.Append(" be_Posts.* FROM be_Posts WHERE IsPublished = 1 ORDER BY DateCreated DESC, DateModified DESC ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);

			while (reader.Read())
			{
				Posts entidadeRetorno = new Posts();
				PopulaPosts(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
			}

			reader.Close();

			return entidadesRetorno;
		}

		/// <summary>
		/// Método que retorna popula um Posts baseado nos dados de um DataReader.
		/// </summary>
		/// <param name="reader">IDataReader contendo os dados da consulta.</param>
		/// <param name="entidade">Posts a ser populado(.</param>
		public static void PopulaPosts(IDataReader reader, Posts entidade)
		{
			if (reader["postId"] != DBNull.Value)
				entidade.PostId = new Guid(reader["postId"].ToString());

			if (reader["title"] != DBNull.Value)
				entidade.Title = reader["title"].ToString();

			if (reader["description"] != DBNull.Value)
				entidade.Description = reader["description"].ToString();

			if (reader["dateCreated"] != DBNull.Value)
				entidade.DateCreated = Convert.ToDateTime(reader["dateCreated"].ToString());

			if (reader["dateModified"] != DBNull.Value)
				entidade.DateModified = Convert.ToDateTime(reader["dateModified"].ToString());

			if (reader["author"] != DBNull.Value)
				entidade.Author = reader["author"].ToString();

			if (reader["isPublished"] != DBNull.Value)
				entidade.IsPublished = Convert.ToBoolean(reader["isPublished"].ToString());

			if (reader["isCommentEnabled"] != DBNull.Value)
				entidade.IsCommentEnabled = Convert.ToBoolean(reader["isCommentEnabled"].ToString());

			if (reader["raters"] != DBNull.Value)
				entidade.Raters = Convert.ToInt32(reader["raters"].ToString());

			if (reader["rating"] != DBNull.Value)
				entidade.Rating = Convert.ToDecimal(reader["rating"].ToString());

			if (reader["slug"] != DBNull.Value)
				entidade.Slug = reader["slug"].ToString();
		}
	}
}