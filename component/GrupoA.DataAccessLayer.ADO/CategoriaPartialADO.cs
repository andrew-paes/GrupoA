using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using GrupoA.BusinessObject;

namespace GrupoA.DataAccess.ADO
{
    public partial class CategoriaADO : ADOSuper, ICategoriaDAL
    {
        #region Métodos

        public IEnumerable<Categoria> CarregarTodosBase()
        {
            List<Categoria> entidadesRetorno = new List<Categoria>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Categoria.* FROM Categoria WHERE categoriaIdPai is null");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            //_db.AddInParameter(command, "@promocaoId", DbType.Int32, entidade.PromocaoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Categoria entidadeRetorno = new Categoria();
                PopulaCategoria(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        public List<Categoria> CarregarTodasCategorias()
        {
            List<Categoria> entidadesRetorno = new List<Categoria>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Categoria.* FROM Categoria ");
            sbSQL.Append("ORDER BY Categoria.categoriaId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Categoria entidadeRetorno = new Categoria();
                PopulaCategoria(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        public List<Categoria> CarregarCategoriaPorProduto(int produtoId)
        {
            List<Categoria> entidadesRetorno = new List<Categoria>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Categoria.*");
            sbSQL.Append("  FROM Categoria");
            sbSQL.Append(" INNER JOIN ProdutoCategoria ON Categoria.categoriaId = ProdutoCategoria.categoriaId");
            sbSQL.Append(" INNER JOIN Produto ON ProdutoCategoria.produtoId = Produto.produtoId");
            sbSQL.Append(string.Concat(" WHERE Produto.produtoId = ", produtoId.ToString()));
            sbSQL.Append(" ORDER BY Categoria.categoriaId DESC");
            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            //_db.AddInParameter(command, "@promocaoId", DbType.Int32, entidade.PromocaoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Categoria entidadeRetorno = new Categoria();
                PopulaCategoria(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        public void ExcluirPorConteudo(Conteudo conteudo)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM conteudoAreaConhecimento ");
            sbSQL.Append("WHERE conteudoId=@conteudoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@conteudoId", DbType.Int32, conteudo.ConteudoId);


            _db.ExecuteNonQuery(command);
        }

        public List<Categoria> CarregarPorPromocao(Promocao promocao)
        {
            List<Categoria> entidadeRetorno = new List<Categoria>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(" SELECT c.* FROM PromocaoCategoria pc ");
            sbSQL.Append(" INNER JOIN Categoria c ON c.categoriaId = pc.categoriaId ");
            sbSQL.Append(" WHERE pc.promocaoId = @promocaoId ");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@promocaoId", DbType.Int32, promocao.PromocaoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Categoria _categoria = new Categoria();
                PopulaCategoria(reader, _categoria);
                entidadeRetorno.Add(_categoria);
            }
            reader.Close();

            return entidadeRetorno;
        }

        public List<CategoriaVH> CarregarSubMenuPorIdentificador(int categoriaId)
        {
            List<CategoriaVH> entidadeRetorno = new List<CategoriaVH>();
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(" WITH Categorias (categoriaId, nomeCategoria, categoriaIdPai, grupoId, nivel, totalProdutos) ");
            sbSQL.Append(" AS ");

            //-- Definição do membro ancora.
            sbSQL.Append(" ( ");
            sbSQL.Append("     SELECT C.categoriaId, C.nomeCategoria, C.categoriaIdPai, C.categoriaId AS grupoId, 1 AS nivel, COUNT(P.produtoId) AS Total ");
            sbSQL.Append("     FROM Categoria AS C ");
            sbSQL.Append("     LEFT JOIN ProdutoCategoria PC ON PC.categoriaId = C.CategoriaId ");
            sbSQL.Append("     LEFT JOIN dbo.Produto P ON p.produtoId = pc.produtoId AND p.exibirSite=1 AND p.homologado=1   ");
            sbSQL.Append("     WHERE C.categoriaIdPai = @categoriaId ");
            sbSQL.Append("     GROUP BY C.categoriaId, C.nomeCategoria, C.categoriaIdPai ");
            sbSQL.Append(" ), ");
            sbSQL.Append(" Subcategorias (categoriaId, nomeCategoria, categoriaIdPai, grupoId, nivel, totalProdutos) ");
            sbSQL.Append(" AS ");
            sbSQL.Append(" ( ");
            sbSQL.Append(" SELECT C.categoriaId, C.nomeCategoria, C.categoriaIdPai, C.categoriaIdPai AS grupoId, 2 AS nivel, COUNT(P.produtoId) AS Total ");
            sbSQL.Append("     FROM Categoria AS C ");
            sbSQL.Append("     INNER JOIN Categorias CSUP ON CSUP.categoriaId = C.categoriaIdPai ");
            sbSQL.Append("     LEFT JOIN ProdutoCategoria PC ON PC.categoriaId = C.CategoriaId ");
            sbSQL.Append("     LEFT JOIN dbo.Produto P ON p.produtoId = pc.produtoId AND p.exibirSite=1 AND p.homologado=1   ");
            sbSQL.Append("     GROUP BY C.categoriaId, C.nomeCategoria, C.categoriaIdPai ");
            sbSQL.Append("     UNION ALL ");
            sbSQL.Append("     SELECT * FROM Categorias ");
            sbSQL.Append(" ) ");

            // -- Execução do CTE para Categorias com Produto.
            sbSQL.Append(" SELECT C.*, 0 AS nivel FROM ( ");
            sbSQL.Append(" SELECT grupoId AS categoriaId ");
            sbSQL.Append("     FROM Subcategorias ");
            sbSQL.Append("     GROUP BY grupoId ");
            sbSQL.Append("     HAVING SUM(Subcategorias.totalProdutos)>0 ");
            sbSQL.Append(" ) AS CT INNER JOIN dbo.Categoria C ON C.categoriaId=CT.categoriaId ");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@categoriaId", DbType.Int32, categoriaId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                CategoriaVH _categoria = new CategoriaVH();
                PopulaCategoriaComFilha(reader, _categoria);
                entidadeRetorno.Add(_categoria);
            }
            reader.Close();

            return entidadeRetorno;
        }

        public static void PopulaCategoriaComFilha(IDataReader reader, CategoriaVH entidade)
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

            if (reader["nivel"] != DBNull.Value)
                entidade.Nivel = Convert.ToInt32(reader["nivel"].ToString());
        }

        public List<Categoria> CarregarCategoriasDoSegundoNivel()
        {
            List<Categoria> entidadesRetorno = new List<Categoria>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT  c.categoriaId, c.categoriaIdPai, c.codigoCategoria, '  ' +c.NomeCategoria + ' ( '+CM.nomeCategoria+ + ' )' AS nomeCategoria ");
            sbSQL.Append("FROM    Categoria C  ");
            sbSQL.Append("INNER JOIN dbo.Categoria CM ON C.categoriaIdPai=CM.categoriaId AND cm.categoriaIdPai IS NULL ");
            sbSQL.Append("ORDER BY C.categoriaIdPai, C.nomeCategoria ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Categoria entidadeRetorno = new Categoria();
                PopulaCategoriaComFilhaCat(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
        }

        public List<Categoria> CarregarCategoriasFilhas(Categoria categoria, bool incluirCategoriaInformada)
        {
            List<Categoria> entidadesRetorno = new List<Categoria>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(" WITH    Categorias ( CategoriaId, Nivel )");
            sbSQL.Append(" AS ( SELECT   CategoriaId ,");
            sbSQL.Append(" 0 AS Nivel");
            sbSQL.Append(" FROM     Categoria");
            sbSQL.Append(" WHERE    categoriaId = @categoriaId");
            sbSQL.Append(" UNION ALL");
            sbSQL.Append(" SELECT   C.categoriaId ,");
            sbSQL.Append(" Nivel + 1");
            sbSQL.Append(" FROM     Categoria AS C");
            sbSQL.Append(" INNER JOIN Categorias AS CS ON C.CategoriaIdPai = CS.categoriaId");
            sbSQL.Append(" )");
            sbSQL.Append(" SELECT  C.*");
            sbSQL.Append(" FROM    Categorias CS");
            sbSQL.Append(" INNER JOIN dbo.Categoria C ON C.categoriaId = CS.CategoriaId");

            if (!incluirCategoriaInformada)
            {
                sbSQL.Append(" WHERE CS.Nivel>0 ");
            }
            sbSQL.Append(" ORDER BY C.nomeCategoria");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@categoriaId", DbType.Int32, categoria.CategoriaId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Categoria entidadeRetorno = new Categoria();
                PopulaCategoria(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// Metodo que retorna as areas de conhecimento na qual o usuario tem interesse.
        /// </summary>
        public IEnumerable<Categoria> CarregarAreaDeInteresseUsuario(Usuario entidade)
        {
            List<Categoria> entidadesRetorno = new List<Categoria>();

            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("WITH Categorias (CategoriaId) AS ");
            sbSQL.Append("( ");
	        sbSQL.Append("    SELECT DISTINCT Categoria.categoriaIdPai ");
	        sbSQL.Append("    FROM dbo.Categoria ");
	        sbSQL.Append("    INNER JOIN UsuarioInteresse ON UsuarioInteresse.categoriaId = Categoria.CategoriaId ");
            sbSQL.Append("    WHERE UsuarioInteresse.usuarioId = @usuarioId ");
	        sbSQL.Append("    UNION ALL ");
	        sbSQL.Append("    SELECT Categoria.categoriaId ");
	        sbSQL.Append("    FROM Categoria ");
	        sbSQL.Append("    INNER JOIN UsuarioInteresse ON UsuarioInteresse.categoriaId = Categoria.CategoriaId ");
            sbSQL.Append("    WHERE UsuarioInteresse.usuarioId = @usuarioId ");
            sbSQL.Append(") ");
            sbSQL.Append("SELECT DISTINCT Categorias.categoriaId ");
            sbSQL.Append("FROM Categorias ");
            sbSQL.Append("ORDER BY Categorias.categoriaId ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.UsuarioId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Categoria entidadeRetorno = new Categoria();
                entidadeRetorno.CategoriaId = Convert.ToInt32(reader["categoriaId"].ToString());
                entidadesRetorno.Add(entidadeRetorno);
            }

            reader.Close();

            return entidadesRetorno;
        }

        public Categoria CarregarCategoriaMestre(Categoria entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append(" WITH Categorias (categoriaId, nomeCategoria, categoriaIdPai, Nivel) AS ");
            sbSQL.Append("(");
            sbSQL.Append("SELECT C.categoriaId, C.nomeCategoria, C.categoriaIdPai, 0 AS Nivel ");
            sbSQL.Append("FROM Categoria AS C ");
            sbSQL.Append("WHERE C.categoriaId = @categoriaId ");
            sbSQL.Append("UNION ALL ");
            sbSQL.Append("SELECT C.categoriaId, C.nomeCategoria, C.categoriaIdPai, Nivel-1 ");
            sbSQL.Append("FROM Categoria AS C ");
            sbSQL.Append("INNER JOIN Categorias AS CS ");
            sbSQL.Append(" ON c.categoriaId = CS.categoriaIdPai ");
            sbSQL.Append(") ");
            sbSQL.Append("SELECT TOP 1 C.* FROM Categorias C ORDER BY Nivel");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@categoriaId", DbType.Int32, entidade.CategoriaId);

            IDataReader reader = _db.ExecuteReader(command);

            Categoria entidadeRetorno = new Categoria();

            if (reader.Read())
            {
                entidadeRetorno.CategoriaId = Convert.ToInt32(reader["categoriaId"].ToString());
            }

            reader.Close();

            return entidadeRetorno;
        }

        public Categoria CarregarCategoriaMestre(Int64 produtoId)
        {
            if (produtoId > 0)
            {
                StringBuilder sbSQL = new StringBuilder();
                sbSQL.Append(" WITH Categorias (categoriaId, nomeCategoria, categoriaIdPai, Nivel) AS  ");
                sbSQL.Append(" ( ");
                sbSQL.Append(" SELECT C.categoriaId, C.nomeCategoria, C.categoriaIdPai, 0 AS Nivel  ");
                sbSQL.Append(" FROM Categoria AS C  ");
                sbSQL.Append(" WHERE C.categoriaId = (SELECT TOP 1 Categoria.categoriaId ");
                sbSQL.Append(" FROM Categoria ");
                sbSQL.Append(" INNER JOIN ProdutoCategoria ON Categoria.categoriaId = ProdutoCategoria.categoriaId ");
                sbSQL.Append(" INNER JOIN Produto ON ProdutoCategoria.produtoId = Produto.produtoId ");
                sbSQL.Append(" WHERE Produto.produtoId = @produtoId) ");
                sbSQL.Append(" UNION ALL  ");
                sbSQL.Append(" SELECT C.categoriaId, C.nomeCategoria, C.categoriaIdPai, Nivel-1  ");
                sbSQL.Append(" FROM Categoria AS C  ");
                sbSQL.Append(" INNER JOIN Categorias AS CS  ");
                sbSQL.Append(" ON c.categoriaId = CS.categoriaIdPai  ");
                sbSQL.Append(" )  ");
                sbSQL.Append(" SELECT TOP 1 C.* FROM Categorias C ORDER BY Nivel ");

                DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

                _db.AddInParameter(command, "@produtoId", DbType.Int64, produtoId);

                IDataReader reader = _db.ExecuteReader(command);

                Categoria entidadeRetorno = new Categoria();

                if (reader.Read())
                {
                    entidadeRetorno.CategoriaId = Convert.ToInt32(reader["categoriaId"].ToString());
                    entidadeRetorno.NomeCategoria = Convert.ToString(reader["nomeCategoria"]);
                }

                reader.Close();

                return entidadeRetorno;
            }
            else
            {
                return null;
            }
        }

        public static void PopulaCategoriaComFilhaCat(IDataReader reader, Categoria entidade)
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
        /// <param name="entidade"></param>
        /// <returns></returns>
        public Categoria CarregarPorCodigoLegado(Categoria entidade)
        {
            Categoria entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM Categoria WHERE codigoCategoria=@codigoCategoria");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@codigoCategoria", DbType.String, entidade.CodigoCategoria);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Categoria();
                PopulaCategoria(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }

        public List<CategoriaVH> CarregarTodasSubCategorias(Categoria entidade)
        {
            List<CategoriaVH> entidadeRetorno = new List<CategoriaVH>();
            StringBuilder sbSQL = new StringBuilder();

            //-- Definição do membro ancora.
            sbSQL.Append(@"WITH 
	                        Categorias (
				                        categoriaId
				                        , nomeCategoria
				                        , categoriaIdPai
				                        , grupoId
				                        , nivel
				                        , totalProdutos
				                        ) AS (
					                        SELECT 
						                        C.categoriaId
						                        , C.nomeCategoria
						                        , C.categoriaIdPai
						                        , C.categoriaId AS grupoId
						                        , 1 AS nivel
						                        , COUNT(P.produtoId) AS Total
					                        FROM
						                        Categoria AS C
						                        LEFT JOIN ProdutoCategoria PC ON PC.categoriaId = C.CategoriaId
						                        LEFT JOIN dbo.Produto P ON p.produtoId = pc.produtoId AND p.exibirSite = 1 AND p.homologado = 1
					                        WHERE
						                        C.categoriaIdPai = @categoriaId
					                        GROUP BY
						                        C.categoriaId
						                        , C.nomeCategoria
						                        , C.categoriaIdPai
					                        )
	                        , Subcategorias (
					                        categoriaId
					                        , nomeCategoria
					                        , categoriaIdPai
					                        , grupoId
					                        , nivel
					                        , totalProdutos
					                        ) AS (
							                        SELECT
								                        C.categoriaId
								                        , C.nomeCategoria
								                        , C.categoriaIdPai
								                        , C.categoriaIdPai AS grupoId
								                        , 2 AS nivel
								                        , COUNT(P.produtoId) AS Total
							                        FROM
								                        Categoria AS C
								                        INNER JOIN Categorias CSUP ON CSUP.categoriaId = C.categoriaIdPai
								                        LEFT JOIN ProdutoCategoria PC ON PC.categoriaId = C.CategoriaId
								                        LEFT JOIN dbo.Produto P ON p.produtoId = pc.produtoId AND p.exibirSite = 1 AND p.homologado = 1
							                        GROUP BY
								                        C.categoriaId
								                        , C.nomeCategoria
								                        , C.categoriaIdPai
							                        UNION ALL
								                        SELECT 
									                        * 
								                        FROM
									                        Categorias
							                        ) ");

            // -- Execução do CTE para Categorias
            sbSQL.Append(@"SELECT 
	                            Subcategorias.categoriaId
	                            , Subcategorias.nomeCategoria
	                            , Subcategorias.categoriaIdPai
	                            , Subcategorias.nivel
	                            , Categoria.codigoCategoria
                            FROM
	                            Subcategorias
	                            INNER JOIN Categoria ON Categoria.categoriaId = Subcategorias.categoriaId
                            ORDER BY
	                            Subcategorias.nivel
	                            , Subcategorias.nomeCategoria");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@categoriaId", DbType.Int32, entidade.CategoriaId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                CategoriaVH _categoria = new CategoriaVH();
                PopulaCategoriaComFilha(reader, _categoria);
                entidadeRetorno.Add(_categoria);
            }
            reader.Close();

            return entidadeRetorno;
        }

        #endregion
    }
}