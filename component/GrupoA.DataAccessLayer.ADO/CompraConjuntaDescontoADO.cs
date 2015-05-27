
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
    public partial class CompraConjuntaDescontoADO : ADOSuper, ICompraConjuntaDescontoDAL
    {

        /// <summary>
        /// Método que persiste um CompraConjuntaDesconto.
        /// </summary>
        /// <param name="entidade">CompraConjuntaDesconto contendo os dados a serem persistidos.</param>	
        public void Inserir(CompraConjuntaDesconto entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO CompraConjuntaDesconto ");
            sbSQL.Append(" (quantidadeMinima, percentualDesconto, compraConjuntaId) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@quantidadeMinima, @percentualDesconto, @compraConjuntaId) ");

            sbSQL.Append(" ; SET @compraConjuntaDescontoId = SCOPE_IDENTITY(); ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddOutParameter(command, "@compraConjuntaDescontoId", DbType.Int32, 8);

            _db.AddInParameter(command, "@quantidadeMinima", DbType.Int32, entidade.QuantidadeMinima);

            _db.AddInParameter(command, "@percentualDesconto", DbType.Decimal, entidade.PercentualDesconto);

            _db.AddInParameter(command, "@compraConjuntaId", DbType.Int32, entidade.CompraConjunta.CompraConjuntaId);


            // Executa a query.
            _db.ExecuteNonQuery(command);

            entidade.CompraConjuntaDescontoId = Convert.ToInt32(_db.GetParameterValue(command, "@compraConjuntaDescontoId"));

        }

        /// <summary>
        /// Método que atualiza os dados de um CompraConjuntaDesconto.
        /// </summary>
        /// <param name="entidade">CompraConjuntaDesconto contendo os dados a serem atualizados.</param>
        public void Atualizar(CompraConjuntaDesconto entidade)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE CompraConjuntaDesconto SET ");
            sbSQL.Append(" quantidadeMinima=@quantidadeMinima, percentualDesconto=@percentualDesconto, compraConjuntaId=@compraConjuntaId ");
            sbSQL.Append(" WHERE compraConjuntaDescontoId=@compraConjuntaDescontoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@compraConjuntaDescontoId", DbType.Int32, entidade.CompraConjuntaDescontoId);
            _db.AddInParameter(command, "@quantidadeMinima", DbType.Int32, entidade.QuantidadeMinima);
            _db.AddInParameter(command, "@percentualDesconto", DbType.Decimal, entidade.PercentualDesconto);
            _db.AddInParameter(command, "@compraConjuntaId", DbType.Int32, entidade.CompraConjunta.CompraConjuntaId);

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que remove um CompraConjuntaDesconto da base de dados.
        /// </summary>
        /// <param name="entidade">CompraConjuntaDesconto a ser excluído (somente o identificador é necessário).</param>		
        public void Excluir(CompraConjuntaDesconto entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM CompraConjuntaDesconto ");
            sbSQL.Append("WHERE compraConjuntaDescontoId=@compraConjuntaDescontoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@compraConjuntaDescontoId", DbType.Int32, entidade.CompraConjuntaDescontoId);

            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que carrega um CompraConjuntaDesconto.
        /// </summary>
        /// <param name="entidade">CompraConjuntaDesconto a ser carregado (somente o identificador é necessário).</param>
        /// <returns>CompraConjuntaDesconto</returns>
        public CompraConjuntaDesconto Carregar(int compraConjuntaDescontoId)
        {
            CompraConjuntaDesconto entidade = new CompraConjuntaDesconto();
            entidade.CompraConjuntaDescontoId = compraConjuntaDescontoId;
            return Carregar(entidade);

        }


        /// <summary>
        /// Método que carrega um CompraConjuntaDesconto.
        /// </summary>
        /// <param name="entidade">CompraConjuntaDesconto a ser carregado (somente o identificador é necessário).</param>
        /// <returns>CompraConjuntaDesconto</returns>
        public CompraConjuntaDesconto Carregar(CompraConjuntaDesconto entidade)
        {

            CompraConjuntaDesconto entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM CompraConjuntaDesconto WHERE compraConjuntaDescontoId=@compraConjuntaDescontoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@compraConjuntaDescontoId", DbType.Int32, entidade.CompraConjuntaDescontoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new CompraConjuntaDesconto();
                PopulaCompraConjuntaDesconto(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }



        /// <summary>
        /// Método que retorna uma coleção de CompraConjuntaDesconto.
        /// </summary>
        /// <param name="entidade">PedidoCompraConjunta relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de CompraConjuntaDesconto.</returns>
        public IEnumerable<CompraConjuntaDesconto> Carregar(PedidoCompraConjunta entidade)
        {
            List<CompraConjuntaDesconto> entidadesRetorno = new List<CompraConjuntaDesconto>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT CompraConjuntaDesconto.* FROM CompraConjuntaDesconto INNER JOIN PedidoCompraConjunta ON CompraConjuntaDesconto.compraConjuntaDescontoId=PedidoCompraConjunta.compraConjuntaDescontoId WHERE PedidoCompraConjunta.pedidoCompraConjuntaId=@pedidoCompraConjuntaId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@pedidoCompraConjuntaId", DbType.Int32, entidade.PedidoCompraConjuntaId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                CompraConjuntaDesconto entidadeRetorno = new CompraConjuntaDesconto();
                PopulaCompraConjuntaDesconto(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de CompraConjuntaDesconto.
        /// </summary>
        /// <param name="entidade">CompraConjunta relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de CompraConjuntaDesconto.</returns>
        public IEnumerable<CompraConjuntaDesconto> Carregar(CompraConjunta entidade)
        {
            List<CompraConjuntaDesconto> entidadesRetorno = new List<CompraConjuntaDesconto>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT CompraConjuntaDesconto.* FROM CompraConjuntaDesconto WHERE CompraConjuntaDesconto.compraConjuntaId=@compraConjuntaId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@compraConjuntaId", DbType.Int32, entidade.CompraConjuntaId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                CompraConjuntaDesconto entidadeRetorno = new CompraConjuntaDesconto();
                PopulaCompraConjuntaDesconto(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }


        /// <summary>
        /// Método que retorna uma coleção de CompraConjuntaDesconto.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos CompraConjuntaDesconto.</returns>
        public IEnumerable<CompraConjuntaDesconto> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {

            List<CompraConjuntaDesconto> entidadesRetorno = new List<CompraConjuntaDesconto>();

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
                sbOrder.Append(" ORDER BY compraConjuntaDescontoId");
            }


            if (registrosPagina > 0)
            {

                //sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM CompraConjuntaDesconto");
                //if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM CompraConjuntaDesconto WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
                //} else {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM CompraConjuntaDesconto ORDER BY " + orderBy + ")");				
                //}	
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT CompraConjuntaDesconto.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM CompraConjuntaDesconto ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSQL.Append("SELECT CompraConjuntaDesconto.* FROM CompraConjuntaDesconto ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                CompraConjuntaDesconto entidadeRetorno = new CompraConjuntaDesconto();
                PopulaCompraConjuntaDesconto(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna todas os CompraConjuntaDesconto existentes na base de dados.
        /// </summary>
        public IEnumerable<CompraConjuntaDesconto> CarregarTodos()
        {
            return CarregarTodos(0, 0, null, null, null);
        }

        /// <summary>
        /// Método que retorna o total de CompraConjuntaDesconto na base de dados.
        /// </summary>
        /// <returns></returns>
        public int TotalRegistros()
        {
            return TotalRegistros(null);
        }

        /// <summary>
        /// Método que retorna o total de CompraConjuntaDesconto na base de dados, aceita filtro.
        /// </summary>
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
        /// <returns></returns>
        public int TotalRegistros(IFilterHelper filtro)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) AS Total FROM CompraConjuntaDesconto");

            if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Executa a query.

            int resultado = (int)_db.ExecuteScalar(command);


            return resultado;
        }

        /// <summary>
        /// Método que retorna popula um CompraConjuntaDesconto baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">CompraConjuntaDesconto a ser populado(.</param>
        public static void PopulaCompraConjuntaDesconto(IDataReader reader, CompraConjuntaDesconto entidade)
        {
            if (reader["compraConjuntaDescontoId"] != DBNull.Value)
                entidade.CompraConjuntaDescontoId = Convert.ToInt32(reader["compraConjuntaDescontoId"].ToString());

            if (reader["quantidadeMinima"] != DBNull.Value)
                entidade.QuantidadeMinima = Convert.ToInt32(reader["quantidadeMinima"].ToString());

            if (reader["percentualDesconto"] != DBNull.Value)
                entidade.PercentualDesconto = Convert.ToDecimal(reader["percentualDesconto"].ToString());

            if (reader["compraConjuntaId"] != DBNull.Value)
            {
                entidade.CompraConjunta = new CompraConjunta();
                entidade.CompraConjunta.CompraConjuntaId = Convert.ToInt32(reader["compraConjuntaId"].ToString());
            }


        }

    }
}
