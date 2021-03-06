
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
    public partial class TituloEletronicoADO : ADOSuper, ITituloEletronicoDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="produtoId"></param>
        /// <returns></returns>
        public TituloEletronico CarregarPorProduto(Int32 produtoId)
        {
            TituloEletronico entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT *");
            sbSQL.Append(" FROM TituloEletronico");
            sbSQL.Append(" WHERE tituloEletronicoId = @produtoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@produtoId", DbType.Int32, produtoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new TituloEletronico();
                PopulaTituloEletronico(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// Método que carrega um Titulo por ISBN13.
        /// </summary>
        /// <param name="ISBN13">ISBN13 do titulo.</param>
        /// <returns>Titulo</returns>
        public TituloEletronico CarregarPorIsbn13(String Isbn13)
        {
            TituloEletronico entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT TituloEletronico.*");
            sbSQL.Append(", Produto.*");
            sbSQL.Append(", conteudoId, conteudoTipoId, dataHoraCadastro");
            sbSQL.Append(" FROM TituloEletronico");
            sbSQL.Append(" INNER JOIN Produto ON TituloEletronico.tituloEletronicoId = Produto.produtoId");
            sbSQL.Append(" INNER JOIN Conteudo ON Produto.produtoId = Conteudo.conteudoId");
            sbSQL.Append(" WHERE isbn13=@isbn13");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@isbn13", DbType.String, Isbn13);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new TituloEletronico();
                PopulaTituloEletronico(reader, entidadeRetorno);
                entidadeRetorno.Produto = new Produto();
                ProdutoADO.PopulaProduto(reader, entidadeRetorno.Produto);
                entidadeRetorno.Produto.Conteudo = new Conteudo();
                ConteudoADO.PopulaConteudo(reader, entidadeRetorno.Produto.Conteudo);
            }

            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void AtualizarISBN(TituloEletronico entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE TituloEletronico SET ");
            sbSQL.Append(" isbn13=@isbn13 ");
            sbSQL.Append(" WHERE tituloEletronicoId=@tituloEletronicoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@tituloEletronicoId", DbType.Int32, entidade.TituloEletronicoId);
            _db.AddInParameter(command, "@isbn13", DbType.String, entidade.Isbn13);

            // Executa a query.
            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tituloBO"></param>
        /// <returns></returns>
        public TituloEletronico CarregarPorTitulo(Titulo tituloBO)
        {
            TituloEletronico entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT *");
            sbSQL.Append(" FROM TituloEletronico");
            sbSQL.Append(" WHERE TituloEletronico.tituloId = @tituloId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@tituloId", DbType.Int32, tituloBO.TituloId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new TituloEletronico();
                PopulaTituloEletronico(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }
    }
}