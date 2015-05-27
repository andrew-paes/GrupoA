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
    public partial class TituloImpressoADO : ADOSuper, ITituloImpressoDAL
    {
        /// <summary>
        /// Método que carrega um Titulo por ISBN13.
        /// </summary>
        /// <param name="ISBN13">ISBN13 do titulo.</param>
        /// <returns>Titulo</returns>
        public TituloImpresso CarregarPorIsbn13(String Isbn13)
        {
            TituloImpresso entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT TituloImpresso.*");
            sbSQL.Append(", Produto.*");
            sbSQL.Append(", conteudoId, conteudoTipoId, dataHoraCadastro");
            sbSQL.Append(" FROM TituloImpresso");
            sbSQL.Append(" INNER JOIN Produto ON TituloImpresso.TituloImpressoId=Produto.produtoId");
            sbSQL.Append(" INNER JOIN Conteudo ON Produto.produtoId=Conteudo.conteudoId");
            sbSQL.Append(" WHERE isbn13=@isbn13");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@isbn13", DbType.String, Isbn13);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new TituloImpresso();
                PopulaTituloImpresso(reader, entidadeRetorno);
                entidadeRetorno.Produto = new Produto();
                ProdutoADO.PopulaProduto(reader, entidadeRetorno.Produto);
                entidadeRetorno.Produto.Conteudo = new Conteudo();
                ConteudoADO.PopulaConteudo(reader, entidadeRetorno.Produto.Conteudo);
            }

            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// Método que carrega um título impresso por produto.
        /// </summary>
        /// <param name="produto">Produto do título impresso.</param>
        /// <returns>TituloImpresso</returns>
        public TituloImpresso CarregarPorProduto(Produto produto)
        {
            TituloImpresso entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT TI.*");
            sbSQL.Append(" , P.*");
            sbSQL.Append(" FROM TituloImpresso TI");
            sbSQL.Append(" JOIN Produto P");
            sbSQL.Append(" ON TI.tituloImpressoId = P.produtoId");
            sbSQL.Append(" WHERE p.produtoId = @produtoId");
            //sbSQL.Append(" WHERE p.nomeProduto = @nomeProduto");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@produtoId", DbType.Int32, produto.ProdutoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new TituloImpresso();
                PopulaTituloImpresso(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="produtoId"></param>
        /// <returns></returns>
        public TituloImpresso CarregarPorProduto(Int32 produtoId)
        {
            TituloImpresso entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT *");
            sbSQL.Append(" FROM TituloImpresso");
            sbSQL.Append(" WHERE tituloImpressoId = @produtoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@produtoId", DbType.Int32, produtoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new TituloImpresso();
                PopulaTituloImpresso(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tituloId"></param>
        /// <returns></returns>
        public TituloImpresso CarregarPorTitulo(Int32 tituloId)
        {
            TituloImpresso entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT *");
            sbSQL.Append(" FROM TituloImpresso");
            sbSQL.Append(" WHERE tituloId = @tituloId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@tituloId", DbType.Int32, tituloId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new TituloImpresso();
                PopulaTituloImpresso(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void AtualizarISBN(TituloImpresso entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE TituloImpresso SET ");
            sbSQL.Append(" isbn10=@isbn10, isbn13=@isbn13 ");
            sbSQL.Append(" WHERE tituloImpressoId=@tituloImpressoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@tituloImpressoId", DbType.Int32, entidade.TituloImpressoId);

            if (entidade.Isbn10 != null)
                _db.AddInParameter(command, "@isbn10", DbType.String, entidade.Isbn10);
            else
                _db.AddInParameter(command, "@isbn10", DbType.String, null);

            _db.AddInParameter(command, "@isbn13", DbType.String, entidade.Isbn13);

            // Executa a query.
            _db.ExecuteNonQuery(command);
        }
    }
}