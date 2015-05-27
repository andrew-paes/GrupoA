
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
    public partial class RevistaPacoteADO : ADOSuper, IRevistaPacoteDAL
    {

        /// <summary>
        /// Método que persiste um RevistaPacote.
        /// </summary>
        /// <param name="entidade">RevistaPacote contendo os dados a serem persistidos.</param>	
        public void Inserir(RevistaPacote entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO RevistaPacote ");
            sbSQL.Append(" (revistaPacoteId, nome) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@revistaPacoteId, @nome) ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaPacoteId", DbType.Int32, entidade.RevistaPacoteId);

            _db.AddInParameter(command, "@nome", DbType.String, entidade.Nome);


            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que atualiza os dados de um RevistaPacote.
        /// </summary>
        /// <param name="entidade">RevistaPacote contendo os dados a serem atualizados.</param>
        public void Atualizar(RevistaPacote entidade)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE RevistaPacote SET ");
            sbSQL.Append(" nome=@nome ");
            sbSQL.Append(" WHERE revistaPacoteId=@revistaPacoteId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@revistaPacoteId", DbType.Int32, entidade.RevistaPacoteId);
            _db.AddInParameter(command, "@nome", DbType.String, entidade.Nome);

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que remove um RevistaPacote da base de dados.
        /// </summary>
        /// <param name="entidade">RevistaPacote a ser excluído (somente o identificador é necessário).</param>		
        public void Excluir(RevistaPacote entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM RevistaPacote ");
            sbSQL.Append("WHERE revistaPacoteId=@revistaPacoteId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaPacoteId", DbType.Int32, entidade.RevistaPacoteId);


            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que carrega um RevistaPacote.
        /// </summary>
        /// <param name="entidade">RevistaPacote a ser carregado (somente o identificador é necessário).</param>
        /// <returns>RevistaPacote</returns>
        public RevistaPacote Carregar(int revistaPacoteId)
        {
            RevistaPacote entidade = new RevistaPacote();
            entidade.RevistaPacoteId = revistaPacoteId;
            return Carregar(entidade);

        }


        /// <summary>
        /// Método que carrega um RevistaPacote.
        /// </summary>
        /// <param name="entidade">RevistaPacote a ser carregado (somente o identificador é necessário).</param>
        /// <returns>RevistaPacote</returns>
        public RevistaPacote Carregar(RevistaPacote entidade)
        {

            RevistaPacote entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM RevistaPacote WHERE revistaPacoteId=@revistaPacoteId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaPacoteId", DbType.Int32, entidade.RevistaPacoteId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new RevistaPacote();
                PopulaRevistaPacote(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }



        /// <summary>
        /// Método que retorna uma coleção de RevistaPacote.
        /// </summary>
        /// <param name="entidade">Produto relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de RevistaPacote.</returns>
        public IEnumerable<RevistaPacote> Carregar(Produto entidade)
        {
            List<RevistaPacote> entidadesRetorno = new List<RevistaPacote>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT RevistaPacote.* FROM RevistaPacote INNER JOIN RevistaPacoteBrinde ON RevistaPacote.revistaPacoteId=RevistaPacoteBrinde.revistaPacoteId WHERE RevistaPacoteBrinde.produtoId=@produtoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.ProdutoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                RevistaPacote entidadeRetorno = new RevistaPacote();
                PopulaRevistaPacote(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de RevistaPacote.
        /// </summary>
        /// <param name="entidade">RevistaPacoteBrindeRegra relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de RevistaPacote.</returns>
        public IEnumerable<RevistaPacote> Carregar(RevistaPacoteBrindeRegra entidade)
        {
            List<RevistaPacote> entidadesRetorno = new List<RevistaPacote>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT RevistaPacote.* FROM RevistaPacote INNER JOIN RevistaPacoteBrindeRegra ON RevistaPacote.revistaPacoteId=RevistaPacoteBrindeRegra.revistaPacoteId WHERE RevistaPacoteBrindeRegra.revistaPacoteBrindeRegraId=@revistaPacoteBrindeRegraId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@revistaPacoteBrindeRegraId", DbType.Int32, entidade.RevistaPacoteBrindeRegraId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                RevistaPacote entidadeRetorno = new RevistaPacote();
                PopulaRevistaPacote(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }


        /// <summary>
        /// Método que retorna uma coleção de RevistaPacote.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos RevistaPacote.</returns>
        public IEnumerable<RevistaPacote> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {

            List<RevistaPacote> entidadesRetorno = new List<RevistaPacote>();

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
                sbOrder.Append(" ORDER BY revistaPacoteId");
            }


            if (registrosPagina > 0)
            {

                //sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM RevistaPacote");
                //if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM RevistaPacote WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
                //} else {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM RevistaPacote ORDER BY " + orderBy + ")");				
                //}	
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT RevistaPacote.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM RevistaPacote ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSQL.Append("SELECT RevistaPacote.* FROM RevistaPacote ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                RevistaPacote entidadeRetorno = new RevistaPacote();
                PopulaRevistaPacote(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna todas os RevistaPacote existentes na base de dados.
        /// </summary>
        public IEnumerable<RevistaPacote> CarregarTodos()
        {
            return CarregarTodos(0, 0, null, null, null);
        }

        /// <summary>
        /// Método que retorna o total de RevistaPacote na base de dados.
        /// </summary>
        /// <returns></returns>
        public int TotalRegistros()
        {
            return TotalRegistros(null);
        }

        /// <summary>
        /// Método que retorna o total de RevistaPacote na base de dados, aceita filtro.
        /// </summary>
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
        /// <returns></returns>
        public int TotalRegistros(IFilterHelper filtro)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) AS Total FROM RevistaPacote");

            if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Executa a query.

            int resultado = (int)_db.ExecuteScalar(command);


            return resultado;
        }

        /// <summary>
        /// Método que retorna popula um RevistaPacote baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">RevistaPacote a ser populado(.</param>
        public static void PopulaRevistaPacote(IDataReader reader, RevistaPacote entidade)
        {
            if (reader["revistaPacoteId"] != DBNull.Value)
                entidade.RevistaPacoteId = Convert.ToInt32(reader["revistaPacoteId"].ToString());

            if (reader["nome"] != DBNull.Value)
                entidade.Nome = reader["nome"].ToString();


        }

    }
}
