
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
    public partial class OfertaProdutoADO : ADOSuper, IOfertaProdutoDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oferta"></param>
        /// <returns></returns>
        public List<OfertaProduto> CarregarProdutosPorOferta(Oferta oferta)
        {

            List<OfertaProduto> entidadesRetorno = new List<OfertaProduto>();

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            sbSQL.Append("SELECT OfertaProduto.*, ");
	        sbSQL.Append("    Produto.nomeProduto ");
            sbSQL.Append("FROM OfertaProduto ");
            sbSQL.Append("INNER JOIN Produto ");
	        sbSQL.Append("    ON OfertaProduto.produtoId = Produto.produtoId ");
            sbSQL.Append("WHERE OfertaProduto.ofertaId = @ofertaId ");
            sbSQL.Append("ORDER BY Produto.nomeProduto ");
            
            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@ofertaId", DbType.Int32, oferta.OfertaId);

            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                OfertaProduto entidadeRetorno = new OfertaProduto();
                PopulaOfertaProdutoDetalhe(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna popula um OfertaProduto baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">OfertaProduto a ser populado(.</param>
        public static void PopulaOfertaProdutoDetalhe(IDataReader reader, OfertaProduto entidade)
        {
            if (reader["ofertaProdutoId"] != DBNull.Value)
                entidade.OfertaProdutoId = Convert.ToInt32(reader["ofertaProdutoId"].ToString());

            if (reader["ofertaId"] != DBNull.Value)
            {
                entidade.Oferta = new Oferta();
                entidade.Oferta.OfertaId = Convert.ToInt32(reader["ofertaId"].ToString());
            }

            if (reader["produtoId"] != DBNull.Value)
            {
                if (entidade.Produto == null) entidade.Produto = new Produto();
                entidade.Produto.ProdutoId = Convert.ToInt32(reader["produtoId"].ToString());
            }

            if (reader["nomeProduto"] != DBNull.Value)
            {
                if (entidade.Produto == null) entidade.Produto = new Produto();
                entidade.Produto.NomeProduto = reader["nomeProduto"].ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void ExcluirTodosPorOferta(Oferta entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM OfertaProduto ");
            sbSQL.Append("WHERE ofertaId = @ofertaId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@ofertaId", DbType.Int32, entidade.OfertaId);

            _db.ExecuteNonQuery(command);
        }
    }
}
