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
    public partial class CursoPanamericanoADO : ADOSuper, ICursoPanamericanoDAL
    {
        #region Métodos

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cursoPanamericanoId"></param>
        /// <returns></returns>
        public List<Categoria> CarregarCategoriasDoCursoPanamericano(int cursoPanamericanoId)
        {
            List<Categoria> entidadesRetorno = new List<Categoria>();

            StringBuilder sbSQL = new StringBuilder();

            //sbSQL.Append("SELECT CursoPanamericano.* ");
            //sbSQL.Append("FROM CursoPanamericano ");
            //sbSQL.Append("INNER JOIN CursoPanamericanoCategoria ON CursoPanamericano.cursoPanamericanoId = CursoPanamericanoCategoria.cursoPanamericanoId ");
            //sbSQL.Append("WHERE CursoPanamericanoCategoria.cursoPanamericanoId = @cursoPanamericanoId ");

            sbSQL.Append("SELECT CursoPanamericanoCategoria.*, Categoria.* ");
            sbSQL.Append("FROM CursoPanamericanoCategoria ");
            sbSQL.Append("INNER JOIN Categoria ON CursoPanamericanoCategoria.categoriaId = Categoria.categoriaId ");
            sbSQL.Append("WHERE CursoPanamericanoCategoria.cursoPanamericanoId = @cursoPanamericanoId ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@cursoPanamericanoId", DbType.Int32, @cursoPanamericanoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Categoria entidadeRetorno = new Categoria();
                PopulaSomenteCategoriasFilhas(reader, entidadeRetorno);
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
        public static void PopulaSomenteCategoriasFilhas(IDataReader reader, Categoria entidade)
        {
            if (reader["categoriaId"] != DBNull.Value)
                entidade.CategoriaId = Convert.ToInt32(reader["categoriaId"].ToString());

            if (reader["nomeCategoria"] != DBNull.Value)
                entidade.NomeCategoria = reader["nomeCategoria"].ToString();

            if (reader["codigoCategoria"] != DBNull.Value)
                entidade.CodigoCategoria = reader["codigoCategoria"].ToString();

            if (reader["categoriaIdPai"] != DBNull.Value)
            {
                entidade.CategoriaPai = new Categoria();
                entidade.CategoriaPai.CategoriaId = Convert.ToInt32(reader["categoriaIdPai"].ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cursoPanamericanoId"></param>
        public void ExcluiCursoPanamericanoCategoria(int cursoPanamericanoId)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM CursoPanamericanoCategoria ");
            sbSQL.Append("WHERE cursoPanamericanoId=@cursoPanamericanoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@cursoPanamericanoId", DbType.Int32, cursoPanamericanoId);

            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cursoPanamericanoId"></param>
        /// <param name="categoriaId"></param>
        public void InserirLocalizacaoCursoPanamericano(int cursoPanamericanoId, int categoriaId)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO CursoPanamericanoCategoria ");
            sbSQL.Append(" (cursoPanamericanoId, categoriaId) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@cursoPanamericanoId, @categoriaId) ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@cursoPanamericanoId", DbType.Int32, cursoPanamericanoId);
            _db.AddInParameter(command, "@categoriaId", DbType.Int32, categoriaId);

            // Executa a query.
            _db.ExecuteNonQuery(command);


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public IEnumerable<CursoPanamericano> CarregarCursosPanamericano(Usuario entidade)
        {
            List<CursoPanamericano> entidadesRetorno = new List<CursoPanamericano>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(" WITH Categorias (categoriaId, nomeCategoria, categoriaIdPai, Nivel) AS (");
            sbSQL.Append(" SELECT C.categoriaId, C.nomeCategoria, C.categoriaIdPai, 0 AS Nivel");
            sbSQL.Append(" FROM Categoria AS C WHERE C.categoriaId IN (SELECT categoriaId FROM dbo.UsuarioInteresse WHERE usuarioId = @usuarioId)");
            sbSQL.Append("  UNION ALL    SELECT C.categoriaId, C.nomeCategoria, C.categoriaIdPai, Nivel+1");
            sbSQL.Append("  FROM Categoria AS C  INNER JOIN Categorias AS CS   ON c.CategoriaIdPai = CS.categoriaId) ");
            sbSQL.Append("  SELECT TOP 3 * FROM (SELECT DISTINCT CP.*, a.* FROM CursoPanamericano CP ");
            sbSQL.Append(" INNER JOIN CursoPanamericanoCategoria CPC ON CP.cursoPanamericanoId = CPC.cursoPanamericanoId");
            sbSQL.Append(" INNER JOIN Categorias C ON C.categoriaId = CPC.categoriaId");
            sbSQL.Append(" LEFT JOIN Arquivo A ON A.arquivoId = CP.arquivoIdImagem) AS RESULTADO ORDER BY NEWID()");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.UsuarioId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                CursoPanamericano entidadeRetorno = new CursoPanamericano();
                PopulaCursoPanamericano(reader, entidadeRetorno);

                // Carrega Imagens
                entidadeRetorno.ArquivoImagem = new Arquivo();
                ArquivoADO.PopulaArquivo(reader, entidadeRetorno.ArquivoImagem);

                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        public CursoPanamericano CarregarCursoPanamericanoPorInteresseUsuario(Int32? usuarioId)
        {
            CursoPanamericano entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(" SELECT TOP 1 CursoPanamericano.*,");
            sbSQL.Append("	Arquivo.*");
            sbSQL.Append(" FROM CursoPanamericano");
            if (usuarioId != null)
            {
                sbSQL.Append(" INNER JOIN CursoPanamericanoCategoria ON CursoPanamericano.cursoPanamericanoId = CursoPanamericanoCategoria.cursoPanamericanoId");
                sbSQL.Append(" INNER JOIN UsuarioInteresse ON CursoPanamericanoCategoria.categoriaId = UsuarioInteresse.categoriaId AND UsuarioInteresse.usuarioId = @usuarioId");
            }
            sbSQL.Append(" LEFT JOIN Arquivo ON CursoPanamericano.arquivoIdImagem = Arquivo.arquivoId");
            sbSQL.Append(" ORDER BY NEWID()");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            if (usuarioId != null)
            {
                _db.AddInParameter(command, "@usuarioId", DbType.Int32, usuarioId);
            }

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new CursoPanamericano();

                PopulaCursoPanamericano(reader, entidadeRetorno);

                // Carrega Imagens
                entidadeRetorno.ArquivoImagem = new Arquivo();
                ArquivoADO.PopulaArquivo(reader, entidadeRetorno.ArquivoImagem);
            }
            reader.Close();

            return entidadeRetorno;
        }

        public List<CursoPanamericano> CarregarCursosPanamericanoParaRevistas(Int32? usuarioId)
        {
            List<CursoPanamericano> entidadesRetorno = new List<CursoPanamericano>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(" SELECT TOP 2 CursoPanamericano.*,");
            sbSQL.Append("	Arquivo.*");
            sbSQL.Append(" FROM CursoPanamericano");
            if (usuarioId != null)
            {
                sbSQL.Append(" INNER JOIN CursoPanamericanoCategoria ON CursoPanamericano.cursoPanamericanoId = CursoPanamericanoCategoria.cursoPanamericanoId");
                sbSQL.Append(" INNER JOIN UsuarioInteresse ON CursoPanamericanoCategoria.categoriaId = UsuarioInteresse.categoriaId AND UsuarioInteresse.usuarioId = @usuarioId");
            }
            sbSQL.Append(" LEFT JOIN Arquivo ON CursoPanamericano.arquivoIdImagem = Arquivo.arquivoId");
            sbSQL.Append(" ORDER BY NEWID()");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            if (usuarioId != null)
            {
                _db.AddInParameter(command, "@usuarioId", DbType.Int32, usuarioId);
            }

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                CursoPanamericano entidadeRetorno = new CursoPanamericano();

                PopulaCursoPanamericano(reader, entidadeRetorno);

                // Carrega Imagens
                entidadeRetorno.ArquivoImagem = new Arquivo();
                ArquivoADO.PopulaArquivo(reader, entidadeRetorno.ArquivoImagem);

                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
        }

        #endregion
    }
}
