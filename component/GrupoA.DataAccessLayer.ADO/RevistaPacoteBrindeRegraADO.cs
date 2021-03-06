
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
    public partial class RevistaPacoteBrindeRegraADO : ADOSuper, IRevistaPacoteBrindeRegraDAL
    {

        /// <summary>
        /// Método que persiste um RevistaPacoteBrindeRegra.
        /// </summary>
        /// <param name="entidade">RevistaPacoteBrindeRegra contendo os dados a serem persistidos.</param>	
        public void Inserir(RevistaPacoteBrindeRegra entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO RevistaPacoteBrindeRegra ");
            sbSQL.Append(" (revistaPacoteBrindeRegraId, revistaPacoteId, codigosProdutos, quantidade) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@revistaPacoteBrindeRegraId, @revistaPacoteId, @codigosProdutos, @quantidade) ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaPacoteBrindeRegraId", DbType.Int32, entidade.RevistaPacoteBrindeRegraId);

            _db.AddInParameter(command, "@revistaPacoteId", DbType.Int32, entidade.RevistaPacote.RevistaPacoteId);

            _db.AddInParameter(command, "@codigosProdutos", DbType.String, entidade.CodigosProdutos);

            _db.AddInParameter(command, "@quantidade", DbType.Int32, entidade.Quantidade);


            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que atualiza os dados de um RevistaPacoteBrindeRegra.
        /// </summary>
        /// <param name="entidade">RevistaPacoteBrindeRegra contendo os dados a serem atualizados.</param>
        public void Atualizar(RevistaPacoteBrindeRegra entidade)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE RevistaPacoteBrindeRegra SET ");
            sbSQL.Append(" revistaPacoteId=@revistaPacoteId, codigosProdutos=@codigosProdutos, quantidade=@quantidade ");
            sbSQL.Append(" WHERE revistaPacoteBrindeRegraId=@revistaPacoteBrindeRegraId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@revistaPacoteBrindeRegraId", DbType.Int32, entidade.RevistaPacoteBrindeRegraId);
            _db.AddInParameter(command, "@revistaPacoteId", DbType.Int32, entidade.RevistaPacote.RevistaPacoteId);
            _db.AddInParameter(command, "@codigosProdutos", DbType.String, entidade.CodigosProdutos);
            _db.AddInParameter(command, "@quantidade", DbType.Int32, entidade.Quantidade);

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que remove um RevistaPacoteBrindeRegra da base de dados.
        /// </summary>
        /// <param name="entidade">RevistaPacoteBrindeRegra a ser excluído (somente o identificador é necessário).</param>		
        public void Excluir(RevistaPacoteBrindeRegra entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM RevistaPacoteBrindeRegra ");
            sbSQL.Append("WHERE revistaPacoteBrindeRegraId=@revistaPacoteBrindeRegraId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaPacoteBrindeRegraId", DbType.Int32, entidade.RevistaPacoteBrindeRegraId);


            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que carrega um RevistaPacoteBrindeRegra.
        /// </summary>
        /// <param name="entidade">RevistaPacoteBrindeRegra a ser carregado (somente o identificador é necessário).</param>
        /// <returns>RevistaPacoteBrindeRegra</returns>
        public RevistaPacoteBrindeRegra Carregar(int revistaPacoteBrindeRegraId)
        {
            RevistaPacoteBrindeRegra entidade = new RevistaPacoteBrindeRegra();
            entidade.RevistaPacoteBrindeRegraId = revistaPacoteBrindeRegraId;
            return Carregar(entidade);
        }


        /// <summary>
        /// Método que carrega um RevistaPacoteBrindeRegra.
        /// </summary>
        /// <param name="entidade">RevistaPacoteBrindeRegra a ser carregado (somente o identificador é necessário).</param>
        /// <returns>RevistaPacoteBrindeRegra</returns>
        public RevistaPacoteBrindeRegra Carregar(RevistaPacoteBrindeRegra entidade)
        {
            RevistaPacoteBrindeRegra entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM RevistaPacoteBrindeRegra WHERE revistaPacoteBrindeRegraId=@revistaPacoteBrindeRegraId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaPacoteBrindeRegraId", DbType.Int32, entidade.RevistaPacoteBrindeRegraId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new RevistaPacoteBrindeRegra();
                PopulaRevistaPacoteBrindeRegra(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// Método que retorna uma coleção de RevistaPacoteBrindeRegra.
        /// </summary>
        /// <param name="entidade">RevistaPacote relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de RevistaPacoteBrindeRegra.</returns>
        public IEnumerable<RevistaPacoteBrindeRegra> Carregar(RevistaPacote entidade)
        {
            List<RevistaPacoteBrindeRegra> entidadesRetorno = new List<RevistaPacoteBrindeRegra>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT RevistaPacoteBrindeRegra.* FROM RevistaPacoteBrindeRegra WHERE RevistaPacoteBrindeRegra.revistaPacoteId=@revistaPacoteId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@revistaPacoteId", DbType.Int32, entidade.RevistaPacoteId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                RevistaPacoteBrindeRegra entidadeRetorno = new RevistaPacoteBrindeRegra();
                PopulaRevistaPacoteBrindeRegra(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }


        /// <summary>
        /// Método que retorna uma coleção de RevistaPacoteBrindeRegra.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos RevistaPacoteBrindeRegra.</returns>
        public IEnumerable<RevistaPacoteBrindeRegra> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {

            List<RevistaPacoteBrindeRegra> entidadesRetorno = new List<RevistaPacoteBrindeRegra>();

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
                sbOrder.Append(" ORDER BY revistaPacoteBrindeRegraId");
            }


            if (registrosPagina > 0)
            {

                //sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM RevistaPacoteBrindeRegra");
                //if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM RevistaPacoteBrindeRegra WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
                //} else {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM RevistaPacoteBrindeRegra ORDER BY " + orderBy + ")");				
                //}	
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT RevistaPacoteBrindeRegra.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM RevistaPacoteBrindeRegra ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSQL.Append("SELECT RevistaPacoteBrindeRegra.* FROM RevistaPacoteBrindeRegra ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                RevistaPacoteBrindeRegra entidadeRetorno = new RevistaPacoteBrindeRegra();
                PopulaRevistaPacoteBrindeRegra(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna todas os RevistaPacoteBrindeRegra existentes na base de dados.
        /// </summary>
        public IEnumerable<RevistaPacoteBrindeRegra> CarregarTodos()
        {
            return CarregarTodos(0, 0, null, null, null);
        }

        /// <summary>
        /// Método que retorna o total de RevistaPacoteBrindeRegra na base de dados.
        /// </summary>
        /// <returns></returns>
        public int TotalRegistros()
        {
            return TotalRegistros(null);
        }

        /// <summary>
        /// Método que retorna o total de RevistaPacoteBrindeRegra na base de dados, aceita filtro.
        /// </summary>
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
        /// <returns></returns>
        public int TotalRegistros(IFilterHelper filtro)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) AS Total FROM RevistaPacoteBrindeRegra");

            if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Executa a query.

            int resultado = (int)_db.ExecuteScalar(command);


            return resultado;
        }

        /// <summary>
        /// Método que retorna popula um RevistaPacoteBrindeRegra baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">RevistaPacoteBrindeRegra a ser populado(.</param>
        public static void PopulaRevistaPacoteBrindeRegra(IDataReader reader, RevistaPacoteBrindeRegra entidade)
        {
            if (reader["revistaPacoteBrindeRegraId"] != DBNull.Value)
                entidade.RevistaPacoteBrindeRegraId = Convert.ToInt32(reader["revistaPacoteBrindeRegraId"].ToString());

            if (reader["codigosProdutos"] != DBNull.Value)
                entidade.CodigosProdutos = reader["codigosProdutos"].ToString();

            if (reader["quantidade"] != DBNull.Value)
                entidade.Quantidade = Convert.ToInt32(reader["quantidade"].ToString());

            if (reader["revistaPacoteId"] != DBNull.Value)
            {
                entidade.RevistaPacote = new RevistaPacote();
                entidade.RevistaPacote.RevistaPacoteId = Convert.ToInt32(reader["revistaPacoteId"].ToString());
            }


        }

    }
}
