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
    public partial class LogPaymentGatewayADO : ADOSuper, ILogPaymentGatewayDAL
    {

        /// <summary>
        /// Método que persiste um LogPaymentGateway.
        /// </summary>
        /// <param name="entidade">LogPaymentGateway contendo os dados a serem persistidos.</param>	
        public void Inserir(LogPaymentGateway entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO LogPaymentGateway ");
            sbSQL.Append(" (codigoPedido, conteudoParametros, conteudoXML) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@codigoPedido, @conteudoParametros, @conteudoXML) ");

            sbSQL.Append(" ; SET @logPaymentGatewayId = SCOPE_IDENTITY(); ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddOutParameter(command, "@logPaymentGatewayId", DbType.Int32, 8);

            if (entidade.CodigoPedido != null)
                _db.AddInParameter(command, "@codigoPedido", DbType.Int32, entidade.CodigoPedido);
            else
                _db.AddInParameter(command, "@codigoPedido", DbType.Int32, null);

            if (entidade.ConteudoParametros != null)
                _db.AddInParameter(command, "@conteudoParametros", DbType.String, entidade.ConteudoParametros);
            else
                _db.AddInParameter(command, "@conteudoParametros", DbType.String, null);

            if (entidade.ConteudoXML != null)
                _db.AddInParameter(command, "@conteudoXML", DbType.String, entidade.ConteudoXML);
            else
                _db.AddInParameter(command, "@conteudoXML", DbType.String, null);


            // Executa a query.
            _db.ExecuteNonQuery(command);

            entidade.LogPaymentGatewayId = Convert.ToInt32(_db.GetParameterValue(command, "@logPaymentGatewayId"));

        }

        /// <summary>
        /// Método que atualiza os dados de um LogPaymentGateway.
        /// </summary>
        /// <param name="entidade">LogPaymentGateway contendo os dados a serem atualizados.</param>
        public void Atualizar(LogPaymentGateway entidade)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE LogPaymentGateway SET ");
            sbSQL.Append(" logPaymentGatewayId=@logPaymentGatewayId, codigoPedido=@codigoPedido, conteudoParametros=@conteudoParametros, conteudoXML=@conteudoXML, dataHora=@dataHora ");
            sbSQL.Append(" WHERE  ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@logPaymentGatewayId", DbType.Int32, entidade.LogPaymentGatewayId);
            if (entidade.CodigoPedido != null)
                _db.AddInParameter(command, "@codigoPedido", DbType.Int32, entidade.CodigoPedido);
            else
                _db.AddInParameter(command, "@codigoPedido", DbType.Int32, null);
            if (entidade.ConteudoParametros != null)
                _db.AddInParameter(command, "@conteudoParametros", DbType.String, entidade.ConteudoParametros);
            else
                _db.AddInParameter(command, "@conteudoParametros", DbType.String, null);
            if (entidade.ConteudoXML != null)
                _db.AddInParameter(command, "@conteudoXML", DbType.String, entidade.ConteudoXML);
            else
                _db.AddInParameter(command, "@conteudoXML", DbType.String, null);
            _db.AddInParameter(command, "@dataHora", DbType.DateTime, entidade.DataHora);

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que remove um LogPaymentGateway da base de dados.
        /// </summary>
        /// <param name="entidade">LogPaymentGateway a ser excluído (somente o identificador é necessário).</param>		
        public void Excluir(LogPaymentGateway entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM LogPaymentGateway ");
            sbSQL.Append("WHERE  ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());



            _db.ExecuteNonQuery(command);
        }


        /// <summary>
        /// Método que carrega um LogPaymentGateway.
        /// </summary>
        /// <param name="entidade">LogPaymentGateway a ser carregado (somente o identificador é necessário).</param>
        /// <returns>LogPaymentGateway</returns>
        public LogPaymentGateway Carregar(LogPaymentGateway entidade)
        {

            LogPaymentGateway entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM LogPaymentGateway WHERE logPaymentGatewayId=@logPaymentGatewayId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@logPaymentGatewayId", DbType.Int32, entidade.LogPaymentGatewayId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new LogPaymentGateway();
                PopulaLogPaymentGateway(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }




        /// <summary>
        /// Método que retorna uma coleção de LogPaymentGateway.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos LogPaymentGateway.</returns>
        public IEnumerable<LogPaymentGateway> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {

            List<LogPaymentGateway> entidadesRetorno = new List<LogPaymentGateway>();

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
                sbOrder.Append(" ORDER BY ");
            }


            if (registrosPagina > 0)
            {

                //sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM LogPaymentGateway");
                //if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM LogPaymentGateway WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
                //} else {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM LogPaymentGateway ORDER BY " + orderBy + ")");				
                //}	
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT LogPaymentGateway.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM LogPaymentGateway ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSQL.Append("SELECT LogPaymentGateway.* FROM LogPaymentGateway ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                LogPaymentGateway entidadeRetorno = new LogPaymentGateway();
                PopulaLogPaymentGateway(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna todas os LogPaymentGateway existentes na base de dados.
        /// </summary>
        public IEnumerable<LogPaymentGateway> CarregarTodos()
        {
            return CarregarTodos(0, 0, null, null, null);
        }

        /// <summary>
        /// Método que retorna o total de LogPaymentGateway na base de dados.
        /// </summary>
        /// <returns></returns>
        public int TotalRegistros()
        {
            return TotalRegistros(null);
        }

        /// <summary>
        /// Método que retorna o total de LogPaymentGateway na base de dados, aceita filtro.
        /// </summary>
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
        /// <returns></returns>
        public int TotalRegistros(IFilterHelper filtro)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) AS Total FROM LogPaymentGateway");

            if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Executa a query.

            int resultado = (int)_db.ExecuteScalar(command);


            return resultado;
        }

        /// <summary>
        /// Método que retorna popula um LogPaymentGateway baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">LogPaymentGateway a ser populado(.</param>
        public static void PopulaLogPaymentGateway(IDataReader reader, LogPaymentGateway entidade)
        {
            if (reader["logPaymentGatewayId"] != DBNull.Value)
                entidade.LogPaymentGatewayId = Convert.ToInt32(reader["logPaymentGatewayId"].ToString());

            if (reader["codigoPedido"] != DBNull.Value)
                entidade.CodigoPedido = Convert.ToInt32(reader["codigoPedido"].ToString());

            if (reader["conteudoParametros"] != DBNull.Value)
                entidade.ConteudoParametros = reader["conteudoParametros"].ToString();

            if (reader["conteudoXML"] != DBNull.Value)
                entidade.ConteudoXML = reader["conteudoXML"].ToString();

            if (reader["dataHora"] != DBNull.Value)
                entidade.DataHora = Convert.ToDateTime(reader["dataHora"].ToString());
        }
    }
}