using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using GrupoA.BusinessObject;
using GrupoA.BusinessObject.ViewHelper;

namespace GrupoA.DataAccess.ADO
{
    public partial class TituloInformacaoComentarioEspecialistaADO : ADOSuper, ITituloInformacaoComentarioEspecialistaDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="quantidadeRegistros"></param>
        /// <returns></returns>
        public IEnumerable<TituloInformacaoComentarioEspecialista> CarregarAvaliacoesEspecialistas(Usuario usuario, Int32 quantidadeRegistros)
        {
            List<TituloInformacaoComentarioEspecialista> entidadesRetorno = new List<TituloInformacaoComentarioEspecialista>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(" WITH Categorias (categoriaId, nomeCategoria, categoriaIdPai, Nivel) ");
            sbSQL.Append(" AS ");
            sbSQL.Append(" ( ");
            sbSQL.Append(" SELECT C.categoriaId, C.nomeCategoria, C.categoriaIdPai, 0 AS Nivel ");
            sbSQL.Append(" FROM Categoria AS C ");
            sbSQL.Append(" WHERE C.categoriaId IN (SELECT categoriaId FROM dbo.UsuarioInteresse WHERE usuarioId = @usuarioId) ");
            sbSQL.Append(" UNION ALL ");
            sbSQL.Append(" SELECT C.categoriaId, C.nomeCategoria, C.categoriaIdPai, Nivel+1 ");
            sbSQL.Append(" FROM Categoria AS C ");
            sbSQL.Append(" INNER JOIN Categorias AS CS ");
            sbSQL.Append(" ON c.CategoriaIdPai = CS.categoriaId ");
            sbSQL.Append(" ) ");
            sbSQL.AppendFormat(" SELECT TOP {0} TIC.*, A.*, T.* FROM dbo.TituloInformacaoComentarioEspecialista TIC ", quantidadeRegistros);
            sbSQL.Append(" INNER JOIN dbo.Titulo T ON T.tituloId = TIC.tituloInformacaoComentarioEspecialistaId ");
            sbSQL.Append(" INNER JOIN dbo.TituloImpresso TI ON TI.tituloId = T.tituloId ");
            sbSQL.Append(" INNER JOIN dbo.Produto P ON P.produtoId = TI.tituloImpressoId ");
            sbSQL.Append(" INNER JOIN dbo.ProdutoCategoria PC ON P.produtoId = PC.produtoId ");
            sbSQL.Append(" INNER JOIN dbo.Arquivo A ON TIC.arquivoIdImagem = A.arquivoId ");
            sbSQL.Append(" WHERE P.exibirSite = 1 AND P.homologado=1 AND PC.categoriaId IN (SELECT categoriaId FROM Categorias) ");
            sbSQL.Append(" ORDER BY NEWID() ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@usuarioId", DbType.Int32, usuario.UsuarioId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                TituloInformacaoComentarioEspecialista entidadeRetorno = new TituloInformacaoComentarioEspecialista();
                PopulaTituloInformacaoComentarioEspecialista(reader, entidadeRetorno);

                // Popula as imagens
                entidadeRetorno.ArquivoImagem = new Arquivo();
                ArquivoADO.PopulaArquivo(reader, entidadeRetorno.ArquivoImagem);

                // Popula titulos
                entidadeRetorno.Titulo = new Titulo();
                TituloADO.PopulaTitulo(reader, entidadeRetorno.Titulo);

                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="quantidadeRegistros"></param>
        /// <returns></returns>
        public IEnumerable<ComentarioEspecialistaDestaqueVH> CarregarAvaliacoesEspecialistasDestaque(Usuario usuario, Int32 quantidadeRegistros)
        {
            List<ComentarioEspecialistaDestaqueVH> entidadesRetorno = new List<ComentarioEspecialistaDestaqueVH>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(@" WITH Categorias (categoriaId, nomeCategoria, categoriaIdPai, Nivel)
                            AS ( 
	                            SELECT C.categoriaId ,
		                            C.nomeCategoria ,
                                    C.categoriaIdPai ,
		                            0 AS Nivel
	                            FROM Categoria AS C
	                            WHERE C.categoriaId IN ( SELECT   categoriaId
							                             FROM dbo.UsuarioInteresse
							                             WHERE usuarioId = @usuarioId)
	                            UNION ALL
	                            SELECT C.categoriaId ,
		                            C.nomeCategoria ,
		                            C.categoriaIdPai ,
		                            Nivel + 1
	                            FROM Categoria AS C
	                            INNER JOIN Categorias AS CS ON c.CategoriaIdPai = CS.categoriaId
                            )
                            SELECT TOP 3
	                            TituloInformacaoComentarioEspecialista.textoComentario ,
	                            TituloInformacaoComentarioEspecialista.nomeEspecialista ,
	                            Titulo.nomeTitulo,
	                            API.arquivoId AS arquivoIdCapa,
	                            API.nomeArquivo AS nomeArquivoCapa
                            FROM dbo.TituloInformacaoComentarioEspecialista
                            INNER JOIN dbo.Titulo ON Titulo.tituloId = TituloInformacaoComentarioEspecialista.tituloInformacaoComentarioEspecialistaId
                            INNER JOIN dbo.TituloImpresso ON TituloImpresso.tituloId = Titulo.tituloId
                            INNER JOIN dbo.Produto ON Produto.produtoId = TituloImpresso.tituloImpressoId
                            INNER JOIN dbo.ProdutoCategoria ON Produto.produtoId = ProdutoCategoria.produtoId
                            /*INNER JOIN dbo.Arquivo ON TituloInformacaoComentarioEspecialista.arquivoIdImagem = Arquivo.arquivoId*/
                            LEFT JOIN dbo.ProdutoImagem ON ProdutoImagem.produtoId = Produto.produtoId AND ProdutoImagem.produtoImagemTipoId=1
                            LEFT JOIN dbo.Arquivo API ON API.arquivoId = ProdutoImagem.arquivoId
                            WHERE Produto.exibirSite = 1 AND Produto.homologado=1
	                            AND ProdutoCategoria.categoriaId IN (SELECT categoriaId
						                               FROM Categorias)
                            ORDER BY NEWID()");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@usuarioId", DbType.Int32, usuario.UsuarioId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                ComentarioEspecialistaDestaqueVH entidade = new ComentarioEspecialistaDestaqueVH();

                if (reader["textoComentario"] != DBNull.Value)
                {
                    entidade.ComentarioTitulo = reader["textoComentario"].ToString();
                }

                if (reader["nomeEspecialista"] != DBNull.Value)
                {
                    entidade.NomeEspecialista = reader["nomeEspecialista"].ToString();
                }

                if (reader["nomeTitulo"] != DBNull.Value)
                {
                    entidade.NomeTitulo = reader["nomeTitulo"].ToString();
                }

                if (reader["nomeArquivoCapa"] != DBNull.Value)
                {
                    entidade.NomeArquivo = reader["nomeArquivoCapa"].ToString();
                }

                entidadesRetorno.Add(entidade);
            }

            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns></returns>
        public TituloInformacaoComentarioEspecialista CarregarComentarioEspecialistaPorCategoria(Categoria categoria, Usuario usuario)
        {
            TituloInformacaoComentarioEspecialista entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT TOP 1 ");
            sbSQL.Append("    TituloInformacaoComentarioEspecialista.tituloInformacaoComentarioEspecialistaId, ");
            sbSQL.Append("    TituloInformacaoComentarioEspecialista.nomeEspecialista, ");
            sbSQL.Append("    TituloInformacaoComentarioEspecialista.especialidade, ");
            sbSQL.Append("    TituloInformacaoComentarioEspecialista.resumoComentario, ");
            sbSQL.Append("    TituloInformacaoComentarioEspecialista.comentarioFormatoId, ");
            sbSQL.Append("    TituloInformacaoComentarioEspecialista.urlMidia, ");
            sbSQL.Append("    TituloInformacaoComentarioEspecialista.textoComentario, ");
            sbSQL.Append("    AA.nomeArquivo AS arquivoAudio, ");
            sbSQL.Append("    Ai.nomeArquivo AS arquivoImagem ");
            sbSQL.Append("FROM TituloInformacaoComentarioEspecialista ");
            if (categoria != null)
            {
                sbSQL.Append("INNER JOIN TituloInformacaoComentarioEspecialistaCategoria ");
                sbSQL.Append("    ON TituloInformacaoComentarioEspecialista.tituloInformacaoComentarioEspecialistaId = TituloInformacaoComentarioEspecialistaCategoria.tituloInformacaoComentarioEspecialistaId ");
            }
            else if (usuario != null)
            {
                sbSQL.Append("INNER JOIN TituloInformacaoComentarioEspecialistaCategoria ON TituloInformacaoComentarioEspecialistaCategoria.tituloInformacaoComentarioEspecialistaId = TituloInformacaoComentarioEspecialista.tituloInformacaoComentarioEspecialistaId ");
            }
            sbSQL.Append("LEFT JOIN Arquivo AA ");
            sbSQL.Append("    ON TituloInformacaoComentarioEspecialista.arquivoIdAudio = AA.arquivoId ");
            sbSQL.Append("LEFT JOIN Arquivo AI ");
            sbSQL.Append("    ON TituloInformacaoComentarioEspecialista.arquivoIdImagem = AI.arquivoId ");
            sbSQL.Append("WHERE TituloInformacaoComentarioEspecialista.destaqueAreaConhecimento = 1 ");
            if (categoria != null)
            {
                sbSQL.Append("    AND TituloInformacaoComentarioEspecialistaCategoria.categoriaId IN ");
                sbSQL.Append("    (SELECT categoriaId FROM Categoria WHERE LEFT(codigoCategoria, 1) = CAST(@categoriaId AS VARCHAR))");
            }
            else if (usuario != null)
            {
                sbSQL.Append(@"AND EXISTS (SELECT * FROM UsuarioInteresse
				                WHERE usuarioId = @usuarioId
					                AND UsuarioInteresse.categoriaId = TituloInformacaoComentarioEspecialistaCategoria.categoriaId)");
            }
            sbSQL.Append("ORDER BY NEWID() ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            if (categoria != null)
            {
                _db.AddInParameter(command, "@categoriaId", DbType.Int32, categoria.CategoriaId);
            }
            else if (usuario != null)
            {
                _db.AddInParameter(command, "@usuarioId", DbType.Int32, usuario.UsuarioId);
            }

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new TituloInformacaoComentarioEspecialista();
                PopulaUltimoComentarioEspecialista(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }

        public TituloInformacaoComentarioEspecialista CarregarComentarioEspecialistaPorCategoriaParaRevista(Categoria categoria)
        {
            TituloInformacaoComentarioEspecialista entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT TOP 1 ");
            sbSQL.Append("    TituloInformacaoComentarioEspecialista.tituloInformacaoComentarioEspecialistaId, ");
            sbSQL.Append("    TituloInformacaoComentarioEspecialista.nomeEspecialista, ");
            sbSQL.Append("    TituloInformacaoComentarioEspecialista.especialidade, ");
            sbSQL.Append("    TituloInformacaoComentarioEspecialista.resumoComentario, ");
            sbSQL.Append("    TituloInformacaoComentarioEspecialista.comentarioFormatoId, ");
            sbSQL.Append("    TituloInformacaoComentarioEspecialista.urlMidia, ");
            sbSQL.Append("    TituloInformacaoComentarioEspecialista.textoComentario, ");
            sbSQL.Append("    AA.nomeArquivo AS arquivoAudio, ");
            sbSQL.Append("    Ai.nomeArquivo AS arquivoImagem, ");
            sbSQL.Append("    Titulo.nomeTitulo, ");
            sbSQL.Append("    Titulo.edicao ");
            sbSQL.Append("FROM TituloInformacaoComentarioEspecialista ");
            if (categoria != null)
            {
                sbSQL.Append("INNER JOIN TituloInformacaoComentarioEspecialistaCategoria ");
                sbSQL.Append("    ON TituloInformacaoComentarioEspecialista.tituloInformacaoComentarioEspecialistaId = TituloInformacaoComentarioEspecialistaCategoria.tituloInformacaoComentarioEspecialistaId ");
            }
            sbSQL.Append("INNER JOIN Titulo ");
            sbSQL.Append("    ON TituloInformacaoComentarioEspecialista.tituloInformacaoComentarioEspecialistaId = Titulo.tituloId ");
            sbSQL.Append("LEFT JOIN Arquivo AA ");
            sbSQL.Append("    ON TituloInformacaoComentarioEspecialista.arquivoIdAudio = AA.arquivoId ");
            sbSQL.Append("LEFT JOIN Arquivo AI ");
            sbSQL.Append("    ON TituloInformacaoComentarioEspecialista.arquivoIdImagem = AI.arquivoId ");
            sbSQL.Append("WHERE TituloInformacaoComentarioEspecialista.destaqueAreaConhecimento = 1 ");
            if (categoria != null)
            {
                sbSQL.Append("    AND TituloInformacaoComentarioEspecialistaCategoria.categoriaId IN ");
                sbSQL.Append("    (SELECT categoriaId FROM Categoria WHERE LEFT(codigoCategoria, 1) = CAST(@categoriaId AS VARCHAR))");
            }
            sbSQL.Append("ORDER BY NEWID() ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            if (categoria != null)
            {
                _db.AddInParameter(command, "@categoriaId", DbType.Int32, categoria.CategoriaId);
            }

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new TituloInformacaoComentarioEspecialista();
                PopulaUltimoComentarioEspecialista(reader, entidadeRetorno);
                PopulaUltimoComentarioEspecialistaComTitulo(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entidade"></param>
        public static void PopulaUltimoComentarioEspecialista(IDataReader reader, TituloInformacaoComentarioEspecialista entidade)
        {
            if (reader["tituloInformacaoComentarioEspecialistaId"] != DBNull.Value)
            {
                entidade.TituloInformacaoComentarioEspecialistaId = Convert.ToInt32(reader["tituloInformacaoComentarioEspecialistaId"].ToString());
            }

            if (reader["nomeEspecialista"] != DBNull.Value)
            {
                entidade.NomeEspecialista = reader["nomeEspecialista"].ToString();
            }

            if (reader["especialidade"] != DBNull.Value)
            {
                entidade.Especialidade = reader["especialidade"].ToString();
            }

            if (reader["resumoComentario"] != DBNull.Value)
            {
                entidade.ResumoComentario = reader["resumoComentario"].ToString();
            }

            if (reader["comentarioFormatoId"] != DBNull.Value)
            {
                if (entidade.ComentarioFormato == null) entidade.ComentarioFormato = new ComentarioFormato();
                entidade.ComentarioFormato.ComentarioFormatoId = Convert.ToInt32(reader["comentarioFormatoId"].ToString());
            }

            if (reader["arquivoAudio"] != DBNull.Value)
            {
                if (entidade.ArquivoAudio == null) entidade.ArquivoAudio = new Arquivo();
                entidade.ArquivoAudio.NomeArquivo = reader["arquivoAudio"].ToString();
            }

            if (reader["arquivoImagem"] != DBNull.Value)
            {
                if (entidade.ArquivoImagem == null) entidade.ArquivoImagem = new Arquivo();
                entidade.ArquivoImagem.NomeArquivo = reader["arquivoImagem"].ToString();
            }

            if (reader["urlMidia"] != DBNull.Value)
            {
                entidade.UrlMidia = reader["urlMidia"].ToString();
            }

            if (reader["textoComentario"] != DBNull.Value)
            {
                entidade.TextoComentario = reader["textoComentario"].ToString();
            }
        }

        public static void PopulaUltimoComentarioEspecialistaComTitulo(IDataReader reader, TituloInformacaoComentarioEspecialista entidade)
        {
            if (reader["nomeTitulo"] != DBNull.Value)
            {
                if (entidade.Titulo == null) entidade.Titulo = new Titulo();
                entidade.Titulo.NomeTitulo = reader["nomeTitulo"].ToString();
            }

            if (reader["edicao"] != DBNull.Value)
            {
                if (entidade.Titulo == null) entidade.Titulo = new Titulo();
                entidade.Titulo.Edicao = Convert.ToInt32(reader["edicao"].ToString());
            }
        }

        /// <summary>
        /// Método que carrega um TituloInformacaoComentarioEspecialista.
        /// </summary>
        /// <param name="entidade">TituloInformacaoComentarioEspecialista a ser carregado (somente o identificador é necessário).</param>
        /// <returns>TituloInformacaoComentarioEspecialista</returns>
        public TituloInformacaoComentarioEspecialista Carregar(Titulo entidade)
        {
            TituloInformacaoComentarioEspecialista entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM TituloInformacaoComentarioEspecialista WHERE tituloInformacaoComentarioEspecialistaId=@tituloInformacaoComentarioEspecialistaId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@tituloInformacaoComentarioEspecialistaId", DbType.Int32, entidade.TituloId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new TituloInformacaoComentarioEspecialista();
                PopulaTituloInformacaoComentarioEspecialista(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }
    }
}
