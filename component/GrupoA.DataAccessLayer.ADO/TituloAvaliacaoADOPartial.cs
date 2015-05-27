using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using GrupoA.BusinessObject;

namespace GrupoA.DataAccess.ADO
{
    public partial class TituloAvaliacaoADO : ADOSuper, ITituloAvaliacaoDAL
    {
        /// <summary>
        /// Método que retorna uma coleção de TituloAvaliacao.
        /// </summary>
        /// <param name="entidade">TituloSolicitacao relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de TituloAvaliacao.</returns>
        public TituloAvaliacao CarregarPorSolicitacao(TituloSolicitacao entidade)
        {
            TituloAvaliacao entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT TituloAvaliacao.* FROM TituloAvaliacao WHERE TituloAvaliacao.tituloSolicitacaoId=@tituloSolicitacaoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@tituloSolicitacaoId", DbType.Int32, entidade.TituloSolicitacaoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new TituloAvaliacao();
                PopulaTituloAvaliacao(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        public List<TituloAvaliacao> CarregarAvaliacoesPublicacao(Int32 usuarioId)
        {
            List<TituloAvaliacao> entidadesRetorno = new List<TituloAvaliacao>();

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            sbSQL.Append(@"WITH Categorias (categoriaId, nomeCategoria, categoriaIdPai, Nivel)
                            AS ( 
                                SELECT C.categoriaId ,
                                    C.nomeCategoria ,
                                    C.categoriaIdPai ,
                                    0 AS Nivel
                                FROM Categoria AS C
                                WHERE C.categoriaId IN ( SELECT   categoriaId
                                                            FROM UsuarioInteresse
                                                            WHERE usuarioId = @usuarioId)
                                UNION ALL
                                SELECT C.categoriaId ,
                                    C.nomeCategoria ,
                                    C.categoriaIdPai ,
                                    Nivel + 1
                                FROM Categoria AS C
                                INNER JOIN Categorias AS CS ON c.CategoriaIdPai = CS.categoriaId
                            )
                            SELECT TOP 4
	                            TituloAvaliacao.tituloAvaliacaoId,
                                TituloAvaliacao.avaliacao,
                                TituloAvaliacao.nomeAvaliador,
                                Titulo.nomeTitulo,
                                Arquivo.arquivoId AS arquivoIdCapa,
                                Arquivo.nomeArquivo AS nomeArquivoCapa,
                                ProdutoCategoria.categoriaId,
                                TituloImpresso.tituloImpressoId,
                                Titulo.tituloId,
                                Titulo.edicao
                            FROM TituloAvaliacao
                            INNER JOIN TituloSolicitacao ON TituloSolicitacao.tituloSolicitacaoId = TituloAvaliacao.tituloSolicitacaoId
                            INNER JOIN Titulo ON Titulo.tituloId = TituloSolicitacao.tituloId
                            INNER JOIN TituloImpresso ON TituloImpresso.tituloId = Titulo.tituloId
                            INNER JOIN Produto ON Produto.produtoId = TituloImpresso.tituloImpressoId
                            INNER JOIN ProdutoCategoria ON Produto.produtoId = ProdutoCategoria.produtoId
                            LEFT JOIN ProdutoImagem ON ProdutoImagem.produtoId = Produto.produtoId AND ProdutoImagem.produtoImagemTipoId=1
                            LEFT JOIN Arquivo ON Arquivo.arquivoId = ProdutoImagem.arquivoId
                            WHERE Produto.exibirSite = 1 AND Produto.homologado=1
	                            AND ProdutoCategoria.categoriaId IN (SELECT categoriaId
							                            FROM Categorias)
	                            AND (TituloAvaliacao.avaliacao IS NOT NULL AND TituloAvaliacao.avaliacao <> '')
                            ORDER BY NEWID()");

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@usuarioId", DbType.Int32, usuarioId);

            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                TituloAvaliacao entidadeRetorno = new TituloAvaliacao();
                PopulaTituloAvaliacaoPublicacao(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
        }

        public static void PopulaTituloAvaliacaoPublicacao(IDataReader reader, TituloAvaliacao entidade)
        {
            if (reader["avaliacao"] != DBNull.Value)
            {
                entidade.Avaliacao = reader["avaliacao"].ToString();
            }

            if (reader["nomeAvaliador"] != DBNull.Value)
            {
                entidade.NomeAvaliador = reader["nomeAvaliador"].ToString();
            }

            entidade.TituloSolicitacao = new TituloSolicitacao();
            entidade.TituloSolicitacao.Titulo = new Titulo();

            if (reader["nomeTitulo"] != DBNull.Value)
            {
                entidade.TituloSolicitacao.Titulo.NomeTitulo = reader["nomeTitulo"].ToString();
            }

            if (reader["tituloId"] != DBNull.Value)
            {
                entidade.TituloSolicitacao.Titulo.TituloId = Convert.ToInt32(reader["tituloId"].ToString());
            }

            if (reader["edicao"] != DBNull.Value)
            {
                entidade.TituloSolicitacao.Titulo.Edicao = Convert.ToInt32(reader["edicao"].ToString());
            }

            entidade.TituloSolicitacao.Titulo.TituloImpresso = new TituloImpresso();
            entidade.TituloSolicitacao.Titulo.TituloImpresso.Produto = new Produto();

            if (reader["tituloImpressoId"] != DBNull.Value)
            {
                entidade.TituloSolicitacao.Titulo.TituloImpresso.Produto.ProdutoId = Convert.ToInt32(reader["tituloImpressoId"].ToString());
            }

            if (reader["categoriaId"] != DBNull.Value)
            {
                entidade.TituloSolicitacao.Titulo.TituloImpresso.Produto.Categorias = new List<Categoria>();
                Categoria categoria = new Categoria();

                categoria.CategoriaId = Convert.ToInt32(reader["categoriaId"].ToString());

                entidade.TituloSolicitacao.Titulo.TituloImpresso.Produto.Categorias.Add(categoria);
            }

            if (reader["arquivoIdCapa"] != DBNull.Value && reader["nomeArquivoCapa"] != DBNull.Value)
            {
                entidade.TituloSolicitacao.Titulo.TituloImpresso.Produto.ProdutoImagens = new List<ProdutoImagem>();
                
                ProdutoImagem produtoImagem = new ProdutoImagem();
                produtoImagem.Arquivo = new Arquivo();

                produtoImagem.Arquivo.ArquivoId = Convert.ToInt32(reader["arquivoIdCapa"].ToString());
                produtoImagem.Arquivo.NomeArquivo = reader["nomeArquivoCapa"].ToString();
                
                entidade.TituloSolicitacao.Titulo.TituloImpresso.Produto.ProdutoImagens.Add(produtoImagem);
            }
        }
    }
}
