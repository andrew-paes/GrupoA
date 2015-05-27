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
    public partial class BlogADO : ADOSuper, IBlogDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns></returns>
        public List<Blog> CarregarBlogsPorCategoria(Categoria categoria, Int32 qtdRegistros)
        {
            List<Blog> entidadesRetorno = new List<Blog>();
            StringBuilder sbSQL = new StringBuilder();
            String categoriaId = null;

            if (categoria != null)
            {
                if (categoria.CategoriaId == 5)
                {
                    categoriaId = "47bdb280-4926-43a6-a7ab-ae6e9b6c447c";
                }
                else if (categoria.CategoriaId == 6)
                {
                    categoriaId = "63f806e8-4a37-4873-9cea-7429feb7667a";
                }
                else if (categoria.CategoriaId == 7)
                {
                    categoriaId = "9b0e12c4-e03b-4442-9aa6-d8634449edc2";
                }
            }

            sbSQL.Append(String.Concat("SELECT TOP ", qtdRegistros));
            sbSQL.Append("    be_Posts.PostID, ");
            sbSQL.Append("    be_Posts.DateCreated, ");
            sbSQL.Append("    be_Posts.Title, ");
            sbSQL.Append("    (select top 1 be_PostCategory.CategoryId from be_PostCategory inner join be_Categories ON be_Categories.categoryId = be_PostCategory.categoryId AND parentId IS NULL  where be_Posts.PostId = be_PostCategory.PostId ) as CategoryId ");
            sbSQL.Append("FROM be_Posts ");
            sbSQL.Append("WHERE be_Posts.IsPublished = 1");

            if (categoriaId != null)
            {
                sbSQL.Append(" AND be_Posts.PostID in ( ");
                sbSQL.Append("    select distinct be_Posts.PostID ");
                sbSQL.Append("    from be_Posts ");
                sbSQL.Append("    inner JOIN be_PostCategory ");
                sbSQL.Append("        ON be_Posts.PostId = be_PostCategory.PostId ");
                sbSQL.Append("    inner join be_Categories ");
                sbSQL.Append("        on be_PostCategory.CategoryID = be_Categories.CategoryID ");
                sbSQL.Append("    where be_Categories.CategoryId = @categoryId ");
                sbSQL.Append("        OR be_Categories.ParentID = @categoryId ");
                sbSQL.Append(") ");
            }

            sbSQL.Append("ORDER BY be_Posts.DateCreated DESC ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            if (categoriaId != null)
            {
                _db.AddInParameter(command, "@categoryId", DbType.String, categoriaId);
            }

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Blog entidadeRetorno = new Blog();
                PopulaBanner(reader, entidadeRetorno);
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
        public static void PopulaBanner(IDataReader reader, Blog entidade)
        {
            if (reader["PostID"] != DBNull.Value)
            {
                entidade.PostId = reader["PostID"].ToString();
            }

            if (reader["DateCreated"] != DBNull.Value)
            {
                entidade.DateCreated = Convert.ToDateTime(reader["DateCreated"].ToString());
            }

            if (reader["Title"] != DBNull.Value)
            {
                entidade.Title = reader["Title"].ToString();
            }

            if (reader["CategoryId"] != DBNull.Value)
            {
                if (reader["CategoryId"].ToString() == "47bdb280-4926-43a6-a7ab-ae6e9b6c447c")
                {
                    entidade.CategoryId = "Biociências";
                }
                else if (reader["CategoryId"].ToString() == "63f806e8-4a37-4873-9cea-7429feb7667a")
                {
                    entidade.CategoryId = "Humanas";
                }
                else if (reader["CategoryId"].ToString() == "9b0e12c4-e03b-4442-9aa6-d8634449edc2")
                {
                    entidade.CategoryId = "Exatas, sociais e aplicadas";
                }
            }
        }
    }
}
