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
    public partial class TituloConteudoExtraArquivoADO : ADOSuper, ITituloConteudoExtraArquivoDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns></returns>
        public List<Titulo> CarregarSalaAulaPorCategoria(Usuario usuario)
        {
            List<Titulo> entidadesRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(@"WITH RESULT ( produtoId, tituloId ) AS
                            (
                                SELECT TOP 3 produtoId, tituloId FROM (
                                    SELECT  
                                          P.tituloImpressoId AS ProdutoId 
                                        , p.tituloId 
                                    FROM     
                                        dbo.TituloImpresso P 
                                        INNER JOIN dbo.Produto PO ON P.tituloImpressoId = PO.produtoId  AND PO.exibirSite = 1 AND PO.homologado=1 ");
            if (usuario != null)
            {
                sbSQL.Append("            INNER JOIN ProdutoCategoria ON ProdutoCategoria.produtoId = PO.produtoId ");
                sbSQL.Append("            INNER JOIN UsuarioInteresse ON ProdutoCategoria.categoriaId = UsuarioInteresse.categoriaId AND UsuarioInteresse.usuarioId = @usuarioId ");
            }
            sbSQL.Append(@"         UNION  
                                    SELECT   
                                          P.tituloEletronicoId AS ProdutoId
                                        , p.tituloId 
                                    FROM     
                                        dbo.TituloEletronico P 
                                        INNER JOIN dbo.Produto PO ON P.tituloEletronicoId = PO.produtoId AND PO.exibirSite = 1 AND PO.homologado=1 ");
            if (usuario != null)
            {
                sbSQL.Append("            INNER JOIN ProdutoCategoria ON ProdutoCategoria.produtoId = PO.produtoId ");
                sbSQL.Append("            INNER JOIN UsuarioInteresse ON ProdutoCategoria.categoriaId = UsuarioInteresse.categoriaId AND UsuarioInteresse.usuarioId = @usuarioId ");
            }
            sbSQL.Append(@"     ) AS PROD 
                                WHERE tituloId in (SELECT tituloId FROM TituloConteudoExtraArquivo WHERE TituloConteudoExtraArquivo.restritoProfessor = 1)
                                ORDER BY NEWID()
                            ) 
                            SELECT
                                  TituloConteudoExtraArquivo.TituloConteudoExtraArquivoId
                                , TituloConteudoExtraArquivo.tituloId
                                , Titulo.nomeTitulo
                                , TituloConteudoExtraArquivo.nomeConteudo
                                , Arquivo.arquivoId
                                , Arquivo.nomeArquivo
                                , Arquivo.tamanhoArquivo
                                , Produto.produtoId
                                , ProdutoCategoria.categoriaId
                                , ArquivoImagem.arquivoId arquivoIdImagem
                                , ArquivoImagem.nomeArquivo nomeArquivoImagem
                            FROM TituloConteudoExtraArquivo
                            INNER JOIN RESULT ON RESULT.tituloId = TituloConteudoExtraArquivo.tituloid
                            INNER JOIN Titulo ON Titulo.tituloId = TituloConteudoExtraArquivo.tituloid
                            INNER JOIN Arquivo ON Arquivo.arquivoId = TituloConteudoExtraArquivo.arquivoId
                            INNER JOIN Produto ON produto.produtoId = RESULT.produtoId 
                            INNER JOIN ProdutoCategoria ON ProdutoCategoria.produtoId = Produto.produtoId
                            LEFT JOIN ProdutoImagem ON ProdutoImagem.produtoId = Produto.produtoId AND ProdutoImagem.produtoImagemTipoId = 1
                            LEFT JOIN Arquivo ArquivoImagem ON ProdutoImagem.arquivoId = ArquivoImagem.arquivoId
                            WHERE TituloConteudoExtraArquivo.restritoProfessor = 1");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            if (usuario != null)
            {
                _db.AddInParameter(command, "@usuarioId", DbType.Int32, usuario.UsuarioId);
            }

            IDataReader reader = _db.ExecuteReader(command);
            String tituloId = "";
            Titulo titulo = new Titulo();
            Int32 qtdArquivos = 0;

            while (reader.Read())
            {
                if (reader["tituloId"].ToString() != tituloId)
                {
                    tituloId = reader["tituloId"].ToString();
                    titulo = new Titulo();
                    PopulaSalaAulaTitulo(reader, titulo);
                    titulo.TituloConteudoExtraArquivos = new List<TituloConteudoExtraArquivo>();

                    if (entidadesRetorno == null) entidadesRetorno = new List<Titulo>();
                    entidadesRetorno.Add(titulo);
                    qtdArquivos = 0;
                }

                if (qtdArquivos < 3)
                {
                    TituloConteudoExtraArquivo tituloConteudoExtraArquivo = new TituloConteudoExtraArquivo();
                    PopulaSalaAulaConteudo(reader, tituloConteudoExtraArquivo);
                    titulo.TituloConteudoExtraArquivos.Add(tituloConteudoExtraArquivo);
                    qtdArquivos++;
                }
            }
            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// Método que retorna popula um TituloConteudoExtraArquivo baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">TituloConteudoExtraArquivo a ser populado(.</param>
        public static void PopulaSalaAulaConteudo(IDataReader reader, TituloConteudoExtraArquivo entidade)
        {
            if (reader["tituloConteudoExtraArquivoId"] != DBNull.Value)
            {
                entidade.TituloConteudoExtraArquivoId = Convert.ToInt32(reader["tituloConteudoExtraArquivoId"].ToString());
            }

            if (reader["nomeConteudo"] != DBNull.Value)
            {
                entidade.NomeConteudo = reader["nomeConteudo"].ToString();
            }

            if (reader["arquivoId"] != DBNull.Value)
            {
                if (entidade.Arquivo == null) entidade.Arquivo = new Arquivo();
                entidade.Arquivo.ArquivoId = Convert.ToInt32(reader["arquivoId"].ToString());
            }

            if (reader["nomeArquivo"] != DBNull.Value)
            {
                if (entidade.Arquivo == null) entidade.Arquivo = new Arquivo();
                entidade.Arquivo.NomeArquivo = reader["nomeArquivo"].ToString();
            }

            if (reader["tamanhoArquivo"] != DBNull.Value)
            {
                if (entidade.Arquivo == null) entidade.Arquivo = new Arquivo();
                entidade.Arquivo.TamanhoArquivo = Convert.ToInt32(reader["tamanhoArquivo"].ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entidade"></param>
        public static void PopulaSalaAulaTitulo(IDataReader reader, Titulo entidade)
        {
            if (reader["tituloId"] != DBNull.Value)
            {
                entidade.TituloId = Convert.ToInt32(reader["tituloId"].ToString());
            }

            if (reader["nomeTitulo"] != DBNull.Value)
            {
                entidade.NomeTitulo = reader["nomeTitulo"].ToString();
            }

            if (reader["produtoId"] != DBNull.Value)
            {
                if (entidade.TituloImpresso == null) entidade.TituloImpresso = new TituloImpresso();
                if (entidade.TituloImpresso.Produto == null) entidade.TituloImpresso.Produto = new Produto();
                entidade.TituloImpresso.Produto.ProdutoId = Convert.ToInt32(reader["produtoId"].ToString());
            }

            if (reader["categoriaId"] != DBNull.Value)
            {
                if (entidade.TituloImpresso == null) entidade.TituloImpresso = new TituloImpresso();
                if (entidade.TituloImpresso.Produto == null) entidade.TituloImpresso.Produto = new Produto();
                entidade.TituloImpresso.Produto.Categorias = new List<Categoria>();
                entidade.TituloImpresso.Produto.Categorias.Add(new Categoria() { CategoriaId = Convert.ToInt32(reader["categoriaId"].ToString()) });
            }

            if (reader["arquivoIdImagem"] != DBNull.Value && reader["nomeArquivoImagem"] != DBNull.Value)
            {
                if (entidade.TituloImpresso == null) entidade.TituloImpresso = new TituloImpresso();
                if (entidade.TituloImpresso.Produto == null) entidade.TituloImpresso.Produto = new Produto();
                entidade.TituloImpresso.Produto.ProdutoImagens = new List<ProdutoImagem>();

                ProdutoImagem produtoImagem = new ProdutoImagem();
                produtoImagem.Arquivo = new Arquivo();
                produtoImagem.Arquivo.ArquivoId = Convert.ToInt32(reader["arquivoIdImagem"].ToString());
                produtoImagem.Arquivo.NomeArquivo = reader["nomeArquivoImagem"].ToString();

                entidade.TituloImpresso.Produto.ProdutoImagens.Add(produtoImagem);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public TituloConteudoExtraArquivo CarregarComDependencia(TituloConteudoExtraArquivo entidade)
        {
            TituloConteudoExtraArquivo entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT TituloConteudoExtraArquivo.*, ");
            sbSQL.Append("    Arquivo.nomeArquivo, ");
            sbSQL.Append("    Arquivo.nomeArquivoOriginal ");
            sbSQL.Append("FROM TituloConteudoExtraArquivo ");
            sbSQL.Append("INNER JOIN Arquivo ON TituloConteudoExtraArquivo.arquivoId = Arquivo.arquivoId ");
            sbSQL.Append("WHERE TituloConteudoExtraArquivo.tituloConteudoExtraArquivoId = @tituloConteudoExtraArquivoId ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@tituloConteudoExtraArquivoId", DbType.Int32, entidade.TituloConteudoExtraArquivoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new TituloConteudoExtraArquivo();
                PopulaTituloConteudoExtraArquivoComDependencia(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entidade"></param>
        public static void PopulaTituloConteudoExtraArquivoComDependencia(IDataReader reader, TituloConteudoExtraArquivo entidade)
        {
            if (reader["tituloConteudoExtraArquivoId"] != DBNull.Value)
            {
                entidade.TituloConteudoExtraArquivoId = Convert.ToInt32(reader["tituloConteudoExtraArquivoId"].ToString());
            }

            if (reader["somenteLogado"] != DBNull.Value)
            {
                entidade.SomenteLogado = Convert.ToBoolean(reader["somenteLogado"].ToString());
            }

            if (reader["restritoProfessor"] != DBNull.Value)
            {
                entidade.RestritoProfessor = Convert.ToBoolean(reader["restritoProfessor"].ToString());
            }

            if (reader["nomeConteudo"] != DBNull.Value)
            {
                entidade.NomeConteudo = reader["nomeConteudo"].ToString();
            }

            if (reader["ativo"] != DBNull.Value)
            {
                entidade.Ativo = Convert.ToBoolean(reader["ativo"]);
            }

            if (reader["dataCadastro"] != DBNull.Value)
            {
                entidade.DataCadastro = Convert.ToDateTime(reader["dataCadastro"]);
            }

            if (reader["tituloId"] != DBNull.Value)
            {
                entidade.Titulo = new Titulo();
                entidade.Titulo.TituloId = Convert.ToInt32(reader["tituloId"].ToString());
            }

            if (reader["arquivoId"] != DBNull.Value)
            {
                if (entidade.Arquivo == null) entidade.Arquivo = new Arquivo();
                entidade.Arquivo.ArquivoId = Convert.ToInt32(reader["arquivoId"].ToString());
            }

            if (reader["nomeArquivo"] != DBNull.Value)
            {
                if (entidade.Arquivo == null) entidade.Arquivo = new Arquivo();
                entidade.Arquivo.NomeArquivo = reader["nomeArquivo"].ToString();
            }

            if (reader["nomeArquivoOriginal"] != DBNull.Value)
            {
                if (entidade.Arquivo == null) entidade.Arquivo = new Arquivo();
                entidade.Arquivo.NomeArquivoOriginal = reader["nomeArquivoOriginal"].ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tituloId"></param>
        /// <returns></returns>
        public List<TituloConteudoExtraArquivo> CarregarTodosComDependenciaPorTitulo(Int32 tituloId)
        {
            List<TituloConteudoExtraArquivo> entidadesRetorno = new List<TituloConteudoExtraArquivo>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT TituloConteudoExtraArquivo.*, ");
            sbSQL.Append("    Arquivo.nomeArquivo, ");
            sbSQL.Append("    Arquivo.nomeArquivoOriginal ");
            sbSQL.Append("FROM TituloConteudoExtraArquivo ");
            sbSQL.Append("INNER JOIN Arquivo ON TituloConteudoExtraArquivo.arquivoId = Arquivo.arquivoId ");
            sbSQL.Append("WHERE TituloConteudoExtraArquivo.tituloId = @tituloId ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@tituloId", DbType.Int32, tituloId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                TituloConteudoExtraArquivo tituloConteudoExtraArquivo = new TituloConteudoExtraArquivo();
                PopulaTituloConteudoExtraArquivoComDependencia(reader, tituloConteudoExtraArquivo);
                entidadesRetorno.Add(tituloConteudoExtraArquivo);
            }
            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void AtualizarNomeConteudo(TituloConteudoExtraArquivo entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE TituloConteudoExtraArquivo SET ");
            sbSQL.Append(" nomeConteudo=@nomeConteudo, ativo=@ativo ");
            sbSQL.Append(" WHERE arquivoId=@arquivoId");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.Arquivo.ArquivoId);
            _db.AddInParameter(command, "@nomeConteudo", DbType.String, entidade.NomeConteudo);
            _db.AddInParameter(command, "@ativo", DbType.Boolean, entidade.Ativo);

            // Executa a query.
            _db.ExecuteNonQuery(command);
        }
    }
}