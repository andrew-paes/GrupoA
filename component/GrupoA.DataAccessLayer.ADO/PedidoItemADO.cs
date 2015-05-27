
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
    public partial class PedidoItemADO : ADOSuper, IPedidoItemDAL
    {

        /// <summary>
        /// Método que persiste um PedidoItem.
        /// </summary>
        /// <param name="entidade">PedidoItem contendo os dados a serem persistidos.</param>	
        public void Inserir(PedidoItem entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO PedidoItem ");
            sbSQL.Append(" (produtoId, pedidoId, quantidade, valorUnitarioBase, valorUnitarioFinal) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@produtoId, @pedidoId, @quantidade, @valorUnitarioBase, @valorUnitarioFinal) ");

            sbSQL.Append(" ; SET @pedidoItemId = SCOPE_IDENTITY(); ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddOutParameter(command, "@pedidoItemId", DbType.Int32, 8);

            _db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.Produto.ProdutoId);

            _db.AddInParameter(command, "@pedidoId", DbType.Int32, entidade.Pedido.PedidoId);

            _db.AddInParameter(command, "@quantidade", DbType.Decimal, entidade.Quantidade);

            _db.AddInParameter(command, "@valorUnitarioBase", DbType.Decimal, entidade.ValorUnitarioBase);

            _db.AddInParameter(command, "@valorUnitarioFinal", DbType.Decimal, entidade.ValorUnitarioFinal);


            // Executa a query.
            _db.ExecuteNonQuery(command);

            entidade.PedidoItemId = Convert.ToInt32(_db.GetParameterValue(command, "@pedidoItemId"));

        }

        /// <summary>
        /// Método que atualiza os dados de um PedidoItem.
        /// </summary>
        /// <param name="entidade">PedidoItem contendo os dados a serem atualizados.</param>
        public void Atualizar(PedidoItem entidade)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE PedidoItem SET ");
            sbSQL.Append(" produtoId=@produtoId, pedidoId=@pedidoId, quantidade=@quantidade, valorUnitarioBase=@valorUnitarioBase, valorUnitarioFinal=@valorUnitarioFinal ");
            sbSQL.Append(" WHERE pedidoItemId=@pedidoItemId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@pedidoItemId", DbType.Int32, entidade.PedidoItemId);
            _db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.Produto.ProdutoId);
            _db.AddInParameter(command, "@pedidoId", DbType.Int32, entidade.Pedido.PedidoId);
            _db.AddInParameter(command, "@quantidade", DbType.Decimal, entidade.Quantidade);
            _db.AddInParameter(command, "@valorUnitarioBase", DbType.Decimal, entidade.ValorUnitarioBase);
            _db.AddInParameter(command, "@valorUnitarioFinal", DbType.Decimal, entidade.ValorUnitarioFinal);

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que remove um PedidoItem da base de dados.
        /// </summary>
        /// <param name="entidade">PedidoItem a ser excluído (somente o identificador é necessário).</param>		
        public void Excluir(PedidoItem entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM PedidoItem ");
            sbSQL.Append("WHERE pedidoItemId=@pedidoItemId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@pedidoItemId", DbType.Int32, entidade.PedidoItemId);


            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que carrega um PedidoItem.
        /// </summary>
        /// <param name="entidade">PedidoItem a ser carregado (somente o identificador é necessário).</param>
        /// <returns>PedidoItem</returns>
        public PedidoItem Carregar(int pedidoItemId)
        {
            PedidoItem entidade = new PedidoItem();
            entidade.PedidoItemId = pedidoItemId;
            return Carregar(entidade);

        }


        /// <summary>
        /// Método que carrega um PedidoItem.
        /// </summary>
        /// <param name="entidade">PedidoItem a ser carregado (somente o identificador é necessário).</param>
        /// <returns>PedidoItem</returns>
        public PedidoItem Carregar(PedidoItem entidade)
        {

            PedidoItem entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM PedidoItem WHERE pedidoItemId=@pedidoItemId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@pedidoItemId", DbType.Int32, entidade.PedidoItemId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new PedidoItem();
                PopulaPedidoItem(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }



        /// <summary>
        /// Método que retorna uma coleção de PedidoItem.
        /// </summary>
        /// <param name="entidade">Pedido relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de PedidoItem.</returns>
        public IEnumerable<PedidoItem> Carregar(Pedido entidade)
        {
            List<PedidoItem> entidadesRetorno = new List<PedidoItem>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT PedidoItem.* FROM PedidoItem WHERE PedidoItem.pedidoId=@pedidoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@pedidoId", DbType.Int32, entidade.PedidoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                PedidoItem entidadeRetorno = new PedidoItem();
                PopulaPedidoItem(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de PedidoItem.
        /// </summary>
        /// <param name="entidade">Produto relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de PedidoItem.</returns>
        public IEnumerable<PedidoItem> Carregar(Produto entidade)
        {
            List<PedidoItem> entidadesRetorno = new List<PedidoItem>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT PedidoItem.* FROM PedidoItem WHERE PedidoItem.produtoId=@produtoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.ProdutoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                PedidoItem entidadeRetorno = new PedidoItem();
                PopulaPedidoItem(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }



        /// <summary>
        /// Método que retorna uma coleção de PedidoItem.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos PedidoItem.</returns>
        public IEnumerable<PedidoItem> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {

            List<PedidoItem> entidadesRetorno = new List<PedidoItem>();

            StringBuilder sbSQL = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();
            StringBuilder sbOrder = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            // Monta o "OrderBy"
            if (ordemColunas != null)
            {
                for (int i = 0; i < ordemColunas.Length; i++)
                {
                    if (sbOrder.Length > 0) { sbOrder.Append(", "); }
                    sbOrder.Append(ordemColunas[i] + " " + ordemSentidos[i]);
                }
                if (sbOrder.Length > 0) { sbOrder.Insert(0, " ORDER BY "); }
            }
            else
            {
                sbOrder.Append(" ORDER BY pedidoItemId");
            }


            if (registrosPagina > 0)
            {

                //sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM PedidoItem");
                //if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM PedidoItem WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
                //} else {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM PedidoItem ORDER BY " + orderBy + ")");				
                //}	
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT PedidoItem.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM PedidoItem ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSQL.Append("SELECT PedidoItem.* FROM PedidoItem ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                PedidoItem entidadeRetorno = new PedidoItem();
                PopulaPedidoItem(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna todas os PedidoItem existentes na base de dados.
        /// </summary>
        public IEnumerable<PedidoItem> CarregarTodos()
        {
            return CarregarTodos(0, 0, null, null, null);
        }

        /// <summary>
        /// Método que retorna o total de PedidoItem na base de dados.
        /// </summary>
        /// <returns></returns>
        public int TotalRegistros()
        {
            return TotalRegistros(null);
        }

        /// <summary>
        /// Método que retorna o total de PedidoItem na base de dados, aceita filtro.
        /// </summary>
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
        /// <returns></returns>
        public int TotalRegistros(IFilterHelper filtro)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) AS Total FROM PedidoItem");

            if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Executa a query.

            int resultado = (int)_db.ExecuteScalar(command);


            return resultado;
        }

        /// <summary>
        /// Método que retorna popula um PedidoItem baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">PedidoItem a ser populado(.</param>
        public static void PopulaPedidoItem(IDataReader reader, PedidoItem entidade)
        {
            if (reader["pedidoItemId"] != DBNull.Value)
                entidade.PedidoItemId = Convert.ToInt32(reader["pedidoItemId"].ToString());

            if (reader["quantidade"] != DBNull.Value)
                entidade.Quantidade = Convert.ToDecimal(reader["quantidade"].ToString());

            if (reader["valorUnitarioBase"] != DBNull.Value)
                entidade.ValorUnitarioBase = Convert.ToDecimal(reader["valorUnitarioBase"].ToString());

            if (reader["valorUnitarioFinal"] != DBNull.Value)
                entidade.ValorUnitarioFinal = Convert.ToDecimal(reader["valorUnitarioFinal"].ToString());

            if (reader["produtoId"] != DBNull.Value)
            {
                entidade.Produto = new Produto();
                entidade.Produto.ProdutoId = Convert.ToInt32(reader["produtoId"].ToString());
            }

            if (reader["pedidoId"] != DBNull.Value)
            {
                entidade.Pedido = new Pedido();
                entidade.Pedido.PedidoId = Convert.ToInt32(reader["pedidoId"].ToString());
            }


        }

    }
}
