using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using GrupoA.BusinessObject;
using GrupoA.BusinessObject.Enumerator;

namespace GrupoA.DataAccess.ADO
{
    public partial class PromocaoADO : ADOSuper, IPromocaoDAL
    {
        /// <summary>
        /// Insere um novo registro ligando uma Promoção a um Usuário
        /// </summary>
        /// <param name="promocao">Promoção que deverá ser ligada ao Usuário (somente o identificador é utilizado usuarioId)</param>
        /// <param name="usuario">Usuário que deverá ser ligado a Promoção (somente o identificador é utilizado promocaoId)</param>
        public void InserirPromocaoUsuario(Promocao promocao, Usuario usuario)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO PromocaoUsuario ");
            sbSQL.Append(" (usuarioId, promocaoId) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@usuarioId, @promocaoId) ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@usuarioId", DbType.String, usuario.UsuarioId);
            _db.AddInParameter(command, "@promocaoId", DbType.String, promocao.PromocaoId);

            // Executa a query.
            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Exclui o relacionamento entre Promoção e Usuário
        /// </summary>
        /// <param name="promocao">Promoção que deverá ser ligada ao Usuário (somente o identificador é utilizado usuarioId)</param>
        /// <param name="usuario">Usuário que deverá ser ligado a Promoção (somente o identificador é utilizado promocaoId)</param>
        public void ExcluirPromocaoUsuario(Promocao promocao, Usuario usuario)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM PromocaoUsuario ");
            sbSQL.Append("WHERE usuarioId=@usuarioId ");
            sbSQL.Append("AND promocaoId=@promocaoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@usuarioId", DbType.String, usuario.UsuarioId);
            _db.AddInParameter(command, "@promocaoId", DbType.String, promocao.PromocaoId);

            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Exclui todos os relacionamentos entre Usuários e Promoção através do código identificador de uma Promoção
        /// </summary>
        /// <param name="promocao">Promoção que deverá ser ligada ao Usuário (somente o identificador é utilizado usuarioId)</param>
        public void ExcluirUsuariosPorPromocao(Promocao promocao)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM PromocaoUsuario ");
            sbSQL.Append("WHERE promocaoId=@promocaoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@promocaoId", DbType.String, promocao.PromocaoId);

            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Insere um novo relacionamento entre uma Promoção e um Perfil
        /// </summary>
        /// <param name="promocao">Promoção a ser relacionada com Peril (somente o identificador é utilizado promocaoId)</param>
        /// <param name="perfil">Perfil a ser relacionado com a Promoção (somente o identificador é utilizado perfilId)</param>
        public void InserirPromocaoPerfil(Promocao promocao, Perfil perfil)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO PromocaoPerfil ");
            sbSQL.Append(" (perfilId, promocaoId) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@perfilId, @promocaoId) ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@perfilId", DbType.String, perfil.PerfilId);
            _db.AddInParameter(command, "@promocaoId", DbType.String, promocao.PromocaoId);

            // Executa a query.
            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Exclui um relacionamento entre uma Promoção e um Perfil.
        /// </summary>
        /// <param name="promocao">Promoção a ser excluído o relacionamento com Peril (somente o identificador é utilizado promocaoId)</param>
        /// <param name="perfil">Perfil a ser excluído o relacionamento com a Promoção (somente o identificador é utilizado perfilId)</param>
        public void ExcluirPromocaoPerfil(Promocao promocao, Perfil perfil)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM PromocaoPerfil ");
            sbSQL.Append("WHERE perfilId=@perfilId ");
            sbSQL.Append("AND promocaoId=@promocaoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@perfilId", DbType.String, perfil.PerfilId);
            _db.AddInParameter(command, "@promocaoId", DbType.String, promocao.PromocaoId);

            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Exclui todos os relacionamentos entre Perfis e uma Promoção
        /// </summary>
        /// <param name="promocao">Promoção a ser excluído o relacionamento com Peril (somente o identificador é utilizado promocaoId)</param>
        public void ExcluirPerfisPorPromocao(Promocao promocao)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM PromocaoPerfil ");
            sbSQL.Append("WHERE promocaoId=@promocaoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@promocaoId", DbType.String, promocao.PromocaoId);

            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="promocao"></param>
        /// <param name="revista"></param>
        public void InserirPromocaoRevista(Promocao promocao, Revista revista)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO PromocaoRevista ");
            sbSQL.Append(" (revistaId, promocaoId) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@revistaId, @promocaoId) ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaId", DbType.Int32, revista.RevistaId);
            _db.AddInParameter(command, "@promocaoId", DbType.Int32, promocao.PromocaoId);

            // Executa a query.
            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="promocao"></param>
        /// <param name="revista"></param>
        public void ExcluirPromocaoRevista(Promocao promocao, Revista revista)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM PromocaoRevista ");
            sbSQL.Append("WHERE revistaId=@revistaId ");
            sbSQL.Append("AND promocaoId=@promocaoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaId", DbType.Int32, revista.RevistaId);
            _db.AddInParameter(command, "@promocaoId", DbType.Int32, promocao.PromocaoId);

            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="promocao"></param>
        public void ExcluirRevistasPorPromocao(Promocao promocao)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM PromocaoRevista ");
            sbSQL.Append("WHERE promocaoId=@promocaoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@promocaoId", DbType.Int32, promocao.PromocaoId);

            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="promocao"></param>
        public void ExcluirCuponsPorPromocao(Promocao promocao)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM PromocaoCupom ");
            sbSQL.Append("WHERE promocaoId=@promocaoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@promocaoId", DbType.String, promocao.PromocaoId);

            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Insere um novo relacionamento entre uma Promoção e um Produto
        /// </summary>
        /// <param name="promocao">Promoção a ser relacionada com Produto (somente o identificador é utilizado promocaoId)</param>
        /// <param name="produto">Produto a ser relacionado com a Promoção (somente o identificador é utilizado produtoId)</param>
        public void InserirPromocaoProduto(Promocao promocao, Produto produto)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO PromocaoProduto ");
            sbSQL.Append(" (produtoId, promocaoId) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@produtoId, @promocaoId) ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@produtoId", DbType.String, produto.ProdutoId);
            _db.AddInParameter(command, "@promocaoId", DbType.String, promocao.PromocaoId);

            // Executa a query.
            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Exclui o relacionamento entre uma promoção e um Produto
        /// </summary>
        /// <param name="promocao">Promoção a ser excluído o relacionamento com Produto (somente o identificador é utilizado promocaoId)</param>
        /// <param name="produto">Produto a ser excluído o relacionamento com Promoção (somente o identificador é utilizado produtoId)</param>
        public void ExcluirPromocaoProduto(Promocao promocao, Produto produto)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM PromocaoProduto ");
            sbSQL.Append("WHERE produtoId=@produtoId ");
            sbSQL.Append("AND promocaoId=@promocaoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@produtoId", DbType.String, produto.ProdutoId);
            _db.AddInParameter(command, "@promocaoId", DbType.String, promocao.PromocaoId);

            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Exclui todos os relacionamentos de uma Promoção
        /// </summary>
        /// <param name="promocao">Promoção a ser excluído o relacionamento com Produto (somente o identificador é utilizado promocaoId)</param>
        public void ExcluirProdutosPorPromocao(Promocao promocao)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM PromocaoProduto ");
            sbSQL.Append("WHERE promocaoId=@promocaoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@promocaoId", DbType.String, promocao.PromocaoId);

            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Insere um relacionamento entre uma Promoção e um Tipo de Produto
        /// </summary>
        /// <param name="promocao">Promoção a ser relacionada com Tipo de Produto (somente o identificador é utilizado promocaoId)</param>
        /// <param name="produtoTipo">Tipo de Produto a ser relacionado com Promoção (somente o identificador é utilizado produtoTipoId)</param>
        public void InserirPromocaoProdutoTipo(Promocao promocao, ProdutoTipo produtoTipo)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO PromocaoProdutoTipo ");
            sbSQL.Append(" (produtoTipoId, promocaoId) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@produtoTipoId, @promocaoId) ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@produtoTipoId", DbType.String, produtoTipo.ProdutoTipoId);
            _db.AddInParameter(command, "@promocaoId", DbType.String, promocao.PromocaoId);

            // Executa a query.
            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Exclui um relacionamento entre uma Promoção e um Tipo de Produto
        /// </summary>
        /// <param name="promocao">Promoção a ser excluído o relacionamento com Tipo de Produto (somente o identificador é utilizado promocaoId)</param>
        /// <param name="produtoTipo">Tipo de Produto a ser excluído o relacionamento com Promoção (somente o identificador é utilizado produtoTipoId)</param>
        public void ExcluirPromocaoProdutoTipo(Promocao promocao, ProdutoTipo produtoTipo)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM PromocaoProdutoTipo ");
            sbSQL.Append("WHERE produtoTipoId=@produtoTipoId ");
            sbSQL.Append("AND promocaoId=@promocaoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@produtoTipoId", DbType.String, produtoTipo.ProdutoTipoId);
            _db.AddInParameter(command, "@promocaoId", DbType.String, promocao.PromocaoId);

            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Exclui todos os relacionamentos de uma Promoção com os Tipos de Produto
        /// </summary>
        /// <param name="promocao">Promoção a ser excluído o relacionamento com Tipo de Produto (somente o identificador é utilizado promocaoId)</param>
        public void ExcluirProdutoTiposPorPromocao(Promocao promocao)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM PromocaoProdutoTipo ");
            sbSQL.Append("WHERE promocaoId=@promocaoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@promocaoId", DbType.String, promocao.PromocaoId);

            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Insere um novo relacionamento entre uma Promoção e uma Categoria
        /// </summary>
        /// <param name="promocao">Promoção a ser relacionada com Categoria (somente o identificador é utilizado promocaoId)</param>
        /// <param name="categoria">Categoria a ser relacionada com Promoção (somente o identificador é utilizado categoriaId)</param>
        public void InserirPromocaoCategoria(Promocao promocao, Categoria categoria)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO PromocaoCategoria ");
            sbSQL.Append(" (categoriaId, promocaoId) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@categoriaId, @promocaoId) ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@categoriaId", DbType.String, categoria.CategoriaId);
            _db.AddInParameter(command, "@promocaoId", DbType.String, promocao.PromocaoId);

            // Executa a query.
            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Exclui todos os relacionamentos entre Categorias e Promoção conforme o código identificador da Promoção
        /// </summary>
        /// <param name="promocao">Promoção a ser relacionada com Categoria (somente o identificador é utilizado promocaoId)</param>
        public void ExcluirCategoriasPorPromocao(Promocao promocao)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM PromocaoCategoria ");
            sbSQL.Append("WHERE promocaoId=@promocaoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@promocaoId", DbType.String, promocao.PromocaoId);

            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Carrega as promoções automáticas válidas (ativas e não expiradas) com suas respectivas restrições.
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="perfil"></param>
        /// <param name="categorias"></param>
        /// <param name="produtos"></param>
        /// <param name="tiposDeProdutos"></param>
        /// <returns></returns>
        public IEnumerable<Promocao> CarregarPromocoesAutomaticasValidas(Usuario usuario, Perfil perfil, List<Categoria> categorias, List<Produto> produtos, List<ProdutoTipo> tiposDeProdutos, String codigoPromocao, Boolean valida)
        {
            StringBuilder sbCatoriasIds = new StringBuilder();
            StringBuilder sbProdutosIds = new StringBuilder();
            StringBuilder sbProdutoTiposIds = new StringBuilder();
            String revistaIds = null;

            if (categorias != null)
            {
                sbCatoriasIds.Append(categorias.Select(s => s.CategoriaId.ToString()).Aggregate((atual, proximo) => atual + ", " + proximo));
            }

            if (produtos != null)
            {
                sbProdutosIds.Append(produtos.Select(s => s.ProdutoId.ToString()).Aggregate((atual, proximo) => atual + ", " + proximo));
            }

            if (tiposDeProdutos != null)
            {
                sbProdutoTiposIds.Append(tiposDeProdutos.Select(s => s.ProdutoTipoId.ToString()).Aggregate((atual, proximo) => atual + ", " + proximo));
            }

            if (usuario != null)
            {
                if (usuario.AssinanteRevistaBmj)
                {
                    revistaIds = String.Concat(AreaDeRevista.Bmj.GetHashCode(), ",");
                }

                if (usuario.AssinanteRevistaPatioEnsFundamental)
                {
                    revistaIds = String.Concat(AreaDeRevista.PatioFundamental.GetHashCode(), ",");
                }

                if (usuario.AssinanteRevistaPatioEnsMedio)
                {
                    revistaIds = String.Concat(AreaDeRevista.PatioEnsinoMedio.GetHashCode(), ",");
                }

                if (usuario.AssinanteRevistaPatioPedagogica)
                {
                    revistaIds = String.Concat(AreaDeRevista.PatioPedagogica.GetHashCode(), ",");
                }

                if (!String.IsNullOrEmpty(revistaIds))
                {
                    revistaIds = revistaIds.Substring(0, revistaIds.Length - 1);
                }
            }

            List<Promocao> entidadesDeRetorno = new List<Promocao>();

            StringBuilder sbSql = new StringBuilder();

            sbSql.Append("WITH Categorias(CategoriaId, Nivel) AS ");
            sbSql.Append("(");
            sbSql.Append("	SELECT CAST(Valor AS int) AS CategoriaId, 0 AS Nivel  FROM fnSplit(@categoriaIds, ',')");
            sbSql.Append("	UNION ALL");
            sbSql.Append("    SELECT ");
            sbSql.Append("		C.categoriaId,");
            sbSql.Append("		Nivel+1 ");
            sbSql.Append("    FROM Categoria AS C");
            sbSql.Append("    INNER JOIN Categorias AS CS");
            sbSql.Append("        ON C.CategoriaIdPai = CS.categoriaId");
            sbSql.Append("), Produtos(ProdutoId) AS ");
            sbSql.Append("(");
            sbSql.Append("	SELECT CAST(Valor AS int) AS ProdutoId  FROM fnSplit(@produtoIds, ',')");
            sbSql.Append("), ProdutoTipos(ProdutoTipoId) AS ");
            sbSql.Append("(");
            sbSql.Append("	SELECT CAST(Valor AS int) AS ProdutoTipoId  FROM fnSplit(@produtoTipoIds, ',')");
            sbSql.Append(")");
            sbSql.Append(" SELECT  *");
            sbSql.Append(" FROM    ( SELECT    P.* ,");
            sbSql.Append("        PT.tipoPromocao ,");
            sbSql.Append("        PP.perfilId ,");
            sbSql.Append("        PC.categoriaId ,");
            sbSql.Append("        PPR.produtoId ,");
            sbSql.Append("        PPT.produtoTipoId ,");
            sbSql.Append("        PU.usuarioId ,");
            sbSql.Append("        PF.valorMinimo ,");
            sbSql.Append("        PF.percentualDesconto ,");
            sbSql.Append("        PF.valorDesconto ,");
            sbSql.Append("        PF.freteGratis,");
            sbSql.Append("        PF.promocaoFaixaId");
            sbSql.Append(" FROM    dbo.Promocao P");
            sbSql.Append("        INNER JOIN dbo.PromocaoTipo PT ON P.promocaoTipoId = PT.promocaoTipoId");
            sbSql.Append("        INNER JOIN dbo.PromocaoFaixa PF ON P.promocaoId = PF.promocaoId");

            if (!String.IsNullOrEmpty(codigoPromocao))
            {
                sbSql.Append("        INNER JOIN dbo.PromocaoCupom ON P.promocaoId = PromocaoCupom.promocaoId");
            }

            sbSql.Append("        LEFT JOIN dbo.PromocaoPerfil PP ON P.promocaoId = pp.promocaoId");
            sbSql.Append("        LEFT JOIN dbo.PromocaoCategoria PC ON p.promocaoId = pc.promocaoId");
            sbSql.Append("        LEFT JOIN dbo.PromocaoProduto PPR ON p.promocaoId = ppr.promocaoId");
            sbSql.Append("        LEFT JOIN dbo.PromocaoProdutoTipo PPT ON P.promocaoId = ppt.promocaoId");
            sbSql.Append("        LEFT JOIN dbo.PromocaoUsuario PU ON P.promocaoId = PU.promocaoId");
            sbSql.Append("        LEFT JOIN dbo.PromocaoRevista PR ON P.promocaoId = PR.promocaoId");
            sbSql.Append(" WHERE   ");

            if (valida)
            {
                sbSql.Append("        P.ativa = 1");
                sbSql.Append("        AND P.aplicaAutomaticamente = 1");
                sbSql.Append("        AND ( P.dataHoraInicio <= GETDATE()");
                sbSql.Append("              AND P.dataHoraFim >= GETDATE()");
                sbSql.Append("            ) AND ");
            }

            if (!String.IsNullOrEmpty(codigoPromocao))
            {
                sbSql.Append("        PromocaoCupom.codigoCupom = @codigoCupom AND");
            }

            if (usuario != null || perfil != null)
            {
                sbSql.Append("        ( ( PU.usuarioId = @usuarioId");
                sbSql.Append("                AND PP.perfilId IS NULL");
                sbSql.Append("              )");
                sbSql.Append("              OR ( PP.perfilId = @perfilId");
                sbSql.Append("                   AND PU.usuarioId IS NULL");
                sbSql.Append("                 )");
                sbSql.Append("              OR ( PP.perfilId IS NULL");
                sbSql.Append("                   AND PU.usuarioId IS NULL");
                sbSql.Append("                 )");
                sbSql.Append("        ) AND ");
            }
            else
            {
                sbSql.Append("        PU.usuarioId IS NULL AND PP.perfilId IS NULL AND");
            }

            sbSql.Append("        ( ( PPT.produtoTipoId IS NULL");
            sbSql.Append("                AND ( ( PC.categoriaId IN ( SELECT CategoriaId FROM Categorias )");
            sbSql.Append("                        AND PPR.produtoId IS NULL");
            sbSql.Append("                      )");
            sbSql.Append("                      OR ( PC.categoriaId IS NULL");
            sbSql.Append("                           AND PPR.produtoId IN ( SELECT ProdutoId FROM Produtos )");
            sbSql.Append("                         )");
            sbSql.Append("                      OR ( PC.categoriaId IS NULL");
            sbSql.Append("                           AND PPR.produtoId IS NULL");
            sbSql.Append("                         )");
            sbSql.Append("                    )");
            sbSql.Append("              )");
            sbSql.Append("              OR ( PPT.produtoTipoId IN ( SELECT ProdutoTipoId FROM ProdutoTipos ) ");
            sbSql.Append("                   AND PPR.produtoId IS NULL");
            sbSql.Append("                   AND PC.categoriaId IS NULL");
            sbSql.Append("                 )");
            sbSql.Append("            )");

            if (!String.IsNullOrEmpty(revistaIds))
            {
                sbSql.Append("            AND ( ");
                sbSql.Append("                    PR.promocaoId IS NULL ");
                sbSql.Append("                    OR ");
                if (revistaIds.Contains(','))
                {
                    sbSql.Append("                    PR.revistaId IN (fnSplit(@revistasIds, ',')) ");
                }
                else
                {
                    sbSql.Append("                    PR.revistaId IN (@revistasIds) ");
                }
                sbSql.Append("            ) ");
            }
            else
            {
                sbSql.Append("        AND PR.revistaID IS NULL");
            }

            sbSql.Append(" ) AS R");
            sbSql.Append(" OUTER APPLY DBO.fnCategoriasFilhas(R.categoriaId, 1)");
            sbSql.Append(" ORDER BY r.promocaoId");


            DbCommand command = _db.GetSqlStringCommand(sbSql.ToString());

            Guid cupomPromocao = new Guid();

            Guid.TryParse(codigoPromocao, out cupomPromocao);

            if (!String.IsNullOrEmpty(codigoPromocao))
            {
                _db.AddInParameter(command, "@codigoCupom", DbType.Guid, cupomPromocao);
            }

            if (usuario != null || perfil != null)
            {
                _db.AddInParameter(command, "@usuarioId", DbType.Int32, usuario == null ? -1 : usuario.UsuarioId);
                _db.AddInParameter(command, "@perfilId", DbType.Int32, perfil == null ? -1 : perfil.PerfilId);
            }
            _db.AddInParameter(command, "@categoriaIds", DbType.String, sbCatoriasIds.ToString());
            _db.AddInParameter(command, "@produtoIds", DbType.String, sbProdutosIds.ToString());
            _db.AddInParameter(command, "@produtoTipoIds", DbType.String, sbProdutoTiposIds.ToString());
            if (!String.IsNullOrEmpty(revistaIds))
            {
                _db.AddInParameter(command, "@revistasIds", DbType.String, revistaIds);
            }

            IDataReader reader = _db.ExecuteReader(command);

            Promocao promocao = new Promocao();
            while (reader.Read())
            {
                if (promocao.PromocaoId != Convert.ToInt32(reader["promocaoId"].ToString()))
                {
                    promocao = new Promocao();
                    promocao.Usuarios = new List<Usuario>();
                    promocao.Perfis = new List<Perfil>();
                    promocao.Categorias = new List<Categoria>();
                    promocao.Produtos = new List<Produto>();
                    promocao.Perfis = new List<Perfil>();
                    promocao.PromocaoFaixas = new List<PromocaoFaixa>();
                    promocao.ProdutoTipos = new List<ProdutoTipo>();
                    PopulaPromocao(reader, promocao);
                    entidadesDeRetorno.Add(promocao);
                }

                // Restrições por usuário e perfis.
                if (reader["usuarioId"] != DBNull.Value && !promocao.Usuarios.Exists(s => s.UsuarioId == Convert.ToInt32(reader["usuarioId"].ToString())))
                {
                    promocao.Usuarios.Add(new Usuario(Convert.ToInt32(reader["usuarioId"].ToString())));
                }
                else if (reader["perfilId"] != DBNull.Value && !promocao.Perfis.Exists(s => s.PerfilId == Convert.ToInt32(reader["perfilId"].ToString())))
                {
                    promocao.Perfis.Add(new Perfil(Convert.ToInt32(reader["perfilId"].ToString())));
                }

                // Restrições por produtos, categorias e tipos de produtos.
                if (reader["produtoId"] != DBNull.Value && !promocao.Produtos.Exists(s => s.ProdutoId == Convert.ToInt32(reader["produtoId"].ToString())))
                {
                    promocao.Produtos.Add(new Produto() { ProdutoId = Convert.ToInt32(reader["produtoId"].ToString()) });
                }

                else if (reader["identificadorCategoria"] != DBNull.Value && !promocao.Categorias.Exists(s => s.CategoriaId == Convert.ToInt32(reader["identificadorCategoria"].ToString())))
                {
                    promocao.Categorias.Add(new Categoria() { CategoriaId = Convert.ToInt32(reader["identificadorCategoria"].ToString()) });
                }
                else if (reader["produtoTipoId"] != DBNull.Value && !promocao.ProdutoTipos.Exists(s => s.ProdutoTipoId == Convert.ToInt32(reader["produtoTipoId"].ToString())))
                {
                    promocao.ProdutoTipos.Add(new ProdutoTipo() { ProdutoTipoId = Convert.ToInt32(reader["produtoTipoId"].ToString()) });
                }

                // Faixa de promoção.
                if (!promocao.PromocaoFaixas.Exists(s => s.PromocaoFaixaId == Convert.ToInt32(reader["promocaoFaixaId"].ToString())))
                {
                    PromocaoFaixa promocaoFaixa = new PromocaoFaixa();
                    PromocaoFaixaADO.PopulaPromocaoFaixa(reader, promocaoFaixa);
                    promocao.PromocaoFaixas.Add(promocaoFaixa);
                }
            }
            reader.Close();

            return entidadesDeRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="codigoDaPromocao"></param>
        /// <returns></returns>
        public Promocao Carregar(string codigoDaPromocao)
        {

            Promocao entidadeRetorno = null;

            StringBuilder sbSql = new StringBuilder();


            sbSql.Append("SELECT P.promocaoId, ");
            sbSql.Append("       P.nomePromocao, ");
            sbSql.Append("       P.codigoPromocao, ");
            sbSql.Append("       P.dataHoraInicio, ");
            sbSql.Append("       P.dataHoraFim, ");
            sbSql.Append("       P.aplicaAutomaticamente, ");
            sbSql.Append("       P.promocaoTipoId, ");
            sbSql.Append("       P.ativa, ");
            sbSql.Append("       P.descricaoPromocao, ");
            sbSql.Append("       PF.promocaoFaixaId, ");
            sbSql.Append("       PF.valorMinimo, ");
            sbSql.Append("       PF.percentualDesconto, ");
            sbSql.Append("       PF.valorDesconto, ");
            sbSql.Append("       PF.freteGratis, ");
            sbSql.Append("       PC.categoriaId, ");
            sbSql.Append("       PP.perfilId, ");
            sbSql.Append("       PPro.produtoId, ");
            sbSql.Append("       PPT.produtoTipoId, ");
            sbSql.Append("       PU.usuarioId ");
            sbSql.Append("FROM Promocao P ");
            sbSql.Append("INNER JOIN PromocaoFaixa PF ");
            sbSql.Append("    ON P.promocaoId = PF.promocaoId ");
            sbSql.Append("INNER JOIN PromocaoCupom ");
            sbSql.Append("    ON P.promocaoId = PromocaoCupom.promocaoId ");
            sbSql.Append("LEFT JOIN PromocaoCategoria PC ");
            sbSql.Append("    ON P.promocaoId = PC.promocaoId ");
            sbSql.Append("LEFT JOIN PromocaoPerfil PP ");
            sbSql.Append("    ON P.promocaoId = PP.promocaoId ");
            sbSql.Append("LEFT JOIN PromocaoProduto PPro ");
            sbSql.Append("    ON P.promocaoId = PPro.promocaoId ");
            sbSql.Append("LEFT JOIN PromocaoProdutoTipo PPT ");
            sbSql.Append("    ON P.promocaoId = PPT.promocaoId ");
            sbSql.Append("LEFT JOIN PromocaoUsuario PU ");
            sbSql.Append("    ON P.promocaoId = PU.promocaoId ");
            sbSql.Append("WHERE PromocaoCupom.codigoCupom = @codigoCupom ");

            DbCommand command = _db.GetSqlStringCommand(sbSql.ToString());

            _db.AddInParameter(command, "@codigoCupom", DbType.Guid, new Guid(codigoDaPromocao));

            IDataReader reader = _db.ExecuteReader(command);

            Int32 promocaoId = 0;
            Int32 promocaoFaixaId = 0;
            Int32 categoriaId = 0;
            Int32 perfilId = 0;
            Int32 produtoId = 0;
            Int32 produtoTipoId = 0;
            Int32 usuarioId = 0;

            if (reader.Read())
            {
                entidadeRetorno = new Promocao();

                PopulaPromocao(reader, entidadeRetorno);
                entidadeRetorno.PromocaoFaixas = new List<PromocaoFaixa>();

                PromocaoFaixa promocaoFaixa = new PromocaoFaixa();
                PopulaPromocaoFaixa(reader, promocaoFaixa);
                entidadeRetorno.PromocaoFaixas.Add(promocaoFaixa);

                while (reader.Read())
                {
                    promocaoFaixa = new PromocaoFaixa();
                    PopulaPromocaoFaixa(reader, promocaoFaixa);
                    entidadeRetorno.PromocaoFaixas.Add(promocaoFaixa);
                }
            }
            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// Método que retorna popula um Promocao baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Promocao a ser populado(.</param>
        public static void PopulaPromocaoFaixa(IDataReader reader, PromocaoFaixa entidade)
        {
            if (reader["promocaoFaixaId"] != DBNull.Value)
            {
                entidade.PromocaoFaixaId = Convert.ToInt32(reader["promocaoFaixaId"].ToString());
            }

            if (reader["promocaoId"] != DBNull.Value)
            {
                entidade.Promocao = new Promocao();
                entidade.Promocao.PromocaoId = Convert.ToInt32(reader["promocaoId"].ToString());
            }

            if (reader["valorMinimo"] != DBNull.Value)
            {
                entidade.ValorMinimo = Convert.ToDecimal(reader["valorMinimo"].ToString());
            }

            if (reader["percentualDesconto"] != DBNull.Value)
            {
                entidade.PercentualDesconto = Convert.ToDecimal(reader["percentualDesconto"].ToString());
            }

            if (reader["valorDesconto"] != DBNull.Value)
            {
                entidade.ValorDesconto = Convert.ToDecimal(reader["valorDesconto"].ToString());
            }

            if (reader["freteGratis"] != DBNull.Value)
            {
                entidade.FreteGratis = Convert.ToBoolean(reader["freteGratis"].ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="codigoCupom"></param>
        /// <returns></returns>
        public Promocao CarregarPromocaoPorCupom(String codigoCupom)
        {
            Promocao entidadeRetorno = null;

            StringBuilder sbSql = new StringBuilder();

            sbSql.Append("SELECT Promocao.promocaoId, ");
            sbSql.Append("    Promocao.nomePromocao, ");
            sbSql.Append("    Promocao.codigoPromocao, ");
            sbSql.Append("    Promocao.dataHoraInicio, ");
            sbSql.Append("    Promocao.dataHoraFim, ");
            sbSql.Append("    Promocao.aplicaAutomaticamente, ");
            sbSql.Append("    Promocao.promocaoTipoId, ");
            sbSql.Append("    Promocao.ativa, ");
            sbSql.Append("    Promocao.descricaoPromocao, ");
            sbSql.Append("    Promocao.numeroMaximoCupom, ");
            sbSql.Append("    PromocaoCupom.codigoCupom, ");
            sbSql.Append("    PromocaoCupom.promocaoCupomId, ");
            sbSql.Append("    PromocaoCupom.reutilizavel, ");
            sbSql.Append("    PromocaoTipo.tipoPromocao ");
            sbSql.Append("FROM Promocao ");
            sbSql.Append("INNER JOIN PromocaoCupom ON Promocao.promocaoId = PromocaoCupom.promocaoId ");
            sbSql.Append("INNER JOIN PromocaoTipo ON Promocao.promocaoTipoId = PromocaoTipo.promocaoTipoId ");
            sbSql.Append("WHERE PromocaoCupom.codigoCupom = @codigoCupom ");

            DbCommand command = _db.GetSqlStringCommand(sbSql.ToString());

            Guid cupomPromocao = new Guid();
            Guid.TryParse(codigoCupom, out cupomPromocao);

            _db.AddInParameter(command, "@codigoCupom", DbType.Guid, cupomPromocao);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Promocao();

                PopulaPromocaoPorCupom(reader, entidadeRetorno);
                entidadeRetorno.PromocaoCupons = new List<PromocaoCupom>();

                PromocaoCupom promocaoCupom = new PromocaoCupom();
                PopulaPromocaoCupom(reader, promocaoCupom);

                entidadeRetorno.PromocaoCupons.Add(promocaoCupom);
            }
            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entidade"></param>
        public static void PopulaPromocaoPorCupom(IDataReader reader, Promocao entidade)
        {
            if (reader["promocaoId"] != DBNull.Value)
                entidade.PromocaoId = Convert.ToInt32(reader["promocaoId"].ToString());

            if (reader["nomePromocao"] != DBNull.Value)
                entidade.NomePromocao = reader["nomePromocao"].ToString();

            if (reader["codigoPromocao"] != DBNull.Value)
                entidade.CodigoPromocao = reader["codigoPromocao"].ToString();

            if (reader["dataHoraInicio"] != DBNull.Value)
                entidade.DataHoraInicio = Convert.ToDateTime(reader["dataHoraInicio"].ToString());

            if (reader["dataHoraFim"] != DBNull.Value)
                entidade.DataHoraFim = Convert.ToDateTime(reader["dataHoraFim"].ToString());

            if (reader["aplicaAutomaticamente"] != DBNull.Value)
                entidade.AplicaAutomaticamente = Convert.ToBoolean(reader["aplicaAutomaticamente"].ToString());

            if (reader["ativa"] != DBNull.Value)
                entidade.Ativa = Convert.ToBoolean(reader["ativa"].ToString());

            if (reader["descricaoPromocao"] != DBNull.Value)
                entidade.DescricaoPromocao = reader["descricaoPromocao"].ToString();

            if (reader["numeroMaximoCupom"] != DBNull.Value)
                entidade.NumeroMaximoCupom = Convert.ToInt32(reader["numeroMaximoCupom"].ToString());

            if (reader["promocaoTipoId"] != DBNull.Value)
            {
                if (entidade.PromocaoTipo == null) entidade.PromocaoTipo = new PromocaoTipo();
                entidade.PromocaoTipo.PromocaoTipoId = Convert.ToInt32(reader["promocaoTipoId"].ToString());
            }

            if (reader["tipoPromocao"] != DBNull.Value)
            {
                if (entidade.PromocaoTipo == null) entidade.PromocaoTipo = new PromocaoTipo();
                entidade.PromocaoTipo.TipoPromocao = reader["tipoPromocao"].ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entidade"></param>
        public static void PopulaPromocaoCupom(IDataReader reader, PromocaoCupom entidade)
        {
            if (reader["promocaoCupomId"] != DBNull.Value)
                entidade.PromocaoCupomId = Convert.ToInt32(reader["promocaoCupomId"].ToString());

            if (reader["codigoCupom"] != DBNull.Value)
                entidade.CodigoCupom = new Guid(reader["codigoCupom"].ToString());

            if (reader["reutilizavel"] != DBNull.Value)
                entidade.Reutilizavel = Convert.ToBoolean(reader["reutilizavel"].ToString());

            if (reader["promocaoId"] != DBNull.Value)
            {
                entidade.Promocao = new Promocao();
                entidade.Promocao.PromocaoId = Convert.ToInt32(reader["promocaoId"].ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="promocaoId"></param>
        /// <returns></returns>
        public List<Usuario> CarregarPromocaoUsuarioPorPromocao(Int32 promocaoId)
        {
            List<Usuario> entidadesRetorno = new List<Usuario>();

            StringBuilder sbSql = new StringBuilder();

            sbSql.Append("SELECT PromocaoUsuario.usuarioId ");
            sbSql.Append("FROM PromocaoUsuario ");
            sbSql.Append("WHERE promocaoId = @promocaoId ");

            DbCommand command = _db.GetSqlStringCommand(sbSql.ToString());

            _db.AddInParameter(command, "@promocaoId", DbType.Int32, promocaoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Usuario entidadeRetorno = new Usuario();
                entidadeRetorno.UsuarioId = Convert.ToInt32(reader["usuarioId"].ToString());

                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="promocaoId"></param>
        /// <returns></returns>
        public List<Categoria> CarregarPromocaoCategoriaPorPromocao(Int32 promocaoId)
        {
            List<Categoria> entidadesRetorno = new List<Categoria>();

            StringBuilder sbSql = new StringBuilder();

            sbSql.Append("SELECT categoriaId ");
            sbSql.Append("FROM PromocaoCategoria ");
            sbSql.Append("WHERE promocaoId = @promocaoId ");

            DbCommand command = _db.GetSqlStringCommand(sbSql.ToString());

            _db.AddInParameter(command, "@promocaoId", DbType.Int32, promocaoId);

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="promocaoId"></param>
        /// <returns></returns>
        public List<Perfil> CarregarPromocaoPerfilPorPromocao(Int32 promocaoId)
        {
            List<Perfil> entidadesRetorno = new List<Perfil>();

            StringBuilder sbSql = new StringBuilder();

            sbSql.Append("SELECT perfilId ");
            sbSql.Append("FROM PromocaoPerfil ");
            sbSql.Append("WHERE promocaoId = @promocaoId ");

            DbCommand command = _db.GetSqlStringCommand(sbSql.ToString());

            _db.AddInParameter(command, "@promocaoId", DbType.Int32, promocaoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Perfil entidadeRetorno = new Perfil();
                entidadeRetorno.PerfilId = Convert.ToInt32(reader["perfilId"].ToString());

                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="promocaoId"></param>
        /// <returns></returns>
        public List<Revista> CarregarPromocaoRevistaPorPromocao(Int32 promocaoId)
        {
            List<Revista> entidadesRetorno = new List<Revista>();

            StringBuilder sbSql = new StringBuilder();

            sbSql.Append("SELECT revistaId ");
            sbSql.Append("FROM PromocaoRevista ");
            sbSql.Append("WHERE promocaoId = @promocaoId ");

            DbCommand command = _db.GetSqlStringCommand(sbSql.ToString());

            _db.AddInParameter(command, "@promocaoId", DbType.Int32, promocaoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Revista entidadeRetorno = new Revista();
                entidadeRetorno.RevistaId = Convert.ToInt32(reader["revistaId"].ToString());

                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="promocaoId"></param>
        /// <returns></returns>
        public List<Produto> CarregarPromocaoProdutoPorPromocao(Int32 promocaoId)
        {
            List<Produto> entidadesRetorno = new List<Produto>();

            StringBuilder sbSql = new StringBuilder();

            sbSql.Append("SELECT produtoId ");
            sbSql.Append("FROM PromocaoProduto ");
            sbSql.Append("WHERE promocaoId = @promocaoId ");

            DbCommand command = _db.GetSqlStringCommand(sbSql.ToString());

            _db.AddInParameter(command, "@promocaoId", DbType.Int32, promocaoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Produto entidadeRetorno = new Produto();
                entidadeRetorno.ProdutoId = Convert.ToInt32(reader["produtoId"].ToString());

                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="promocaoId"></param>
        /// <returns></returns>
        public List<ProdutoTipo> CarregarPromocaoProdutoTipoPorPromocao(Int32 promocaoId)
        {
            List<ProdutoTipo> entidadesRetorno = new List<ProdutoTipo>();

            StringBuilder sbSql = new StringBuilder();

            sbSql.Append("SELECT produtoTipoId ");
            sbSql.Append("FROM PromocaoProdutoTipo ");
            sbSql.Append("WHERE promocaoId = @promocaoId ");

            DbCommand command = _db.GetSqlStringCommand(sbSql.ToString());

            _db.AddInParameter(command, "@promocaoId", DbType.Int32, promocaoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                ProdutoTipo entidadeRetorno = new ProdutoTipo();
                entidadeRetorno.ProdutoTipoId = Convert.ToInt32(reader["produtoTipoId"].ToString());

                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="promocaoId"></param>
        /// <returns></returns>
        public StringBuilder CarregarCategoriasSelecionadasPorPromocao(Int32 promocaoId)
        {
            StringBuilder entidadesRetorno = new StringBuilder();

            StringBuilder sbSQL = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            sbSQL.Append("SELECT PromocaoCategoria.categoriaId ");
            sbSQL.Append("FROM PromocaoCategoria ");
            sbSQL.Append("WHERE PromocaoCategoria.promocaoId = @promocaoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@promocaoId", DbType.Int32, promocaoId);

            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                entidadesRetorno.Append(reader["categoriaId"].ToString());
                entidadesRetorno.Append(", ");
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revista"></param>
        /// <returns></returns>
        public Int32 ContarPromocaoRevistaSemProdutoPorRevista(Revista revista)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(@"SELECT COUNT(Promocao.promocaoId) AS Total
                            FROM PromocaoRevista
                            INNER JOIN Promocao ON Promocao.promocaoId = PromocaoRevista.promocaoId
                            LEFT JOIN PromocaoFaixa ON PromocaoFaixa.promocaoId = Promocao.promocaoId
                            LEFT JOIN PromocaoCategoria ON PromocaoCategoria.promocaoId = Promocao.promocaoId
                            LEFT JOIN PromocaoProduto ON PromocaoProduto.promocaoId = Promocao.promocaoId
                            LEFT JOIN PromocaoProdutoTipo ON PromocaoProdutoTipo.promocaoId = Promocao.promocaoId
                            WHERE PromocaoRevista.revistaId = @revistaId
	                            AND Promocao.ativa = 1
	                            AND Promocao.aplicaAutomaticamente = 1
	                            AND Promocao.dataHoraInicio <= GETDATE()
	                            AND Promocao.dataHoraFim >= GETDATE()
	                            AND PromocaoFaixa.promocaoFaixaId IS NOT NULL
	                            AND PromocaoCategoria.categoriaId IS NULL
	                            AND PromocaoProduto.produtoId IS NULL
	                            AND PromocaoProdutoTipo.produtoTipoId IS NULL");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@revistaId", DbType.Int32, revista.RevistaId);

            Int32 resultado = (Int32)_db.ExecuteScalar(command);


            return resultado;
        }
    }
}