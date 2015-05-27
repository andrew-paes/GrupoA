
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
    public partial class SeloADO : ADOSuper, ISeloDAL
    {

        /// <summary>
        /// Método que persiste um Selo.
        /// </summary>
        /// <param name="entidade">Selo contendo os dados a serem persistidos.</param>	
        public void Inserir(Selo entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO Selo ");
            sbSQL.Append(" (seloId, nomeSelo) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@seloId, @nomeSelo) ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@seloId", DbType.Int32, entidade.SeloId);

            _db.AddInParameter(command, "@nomeSelo", DbType.String, entidade.NomeSelo);


            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que atualiza os dados de um Selo.
        /// </summary>
        /// <param name="entidade">Selo contendo os dados a serem atualizados.</param>
        public void Atualizar(Selo entidade)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE Selo SET ");
            sbSQL.Append(" nomeSelo=@nomeSelo ");
            sbSQL.Append(" WHERE seloId=@seloId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@seloId", DbType.Int32, entidade.SeloId);
            _db.AddInParameter(command, "@nomeSelo", DbType.String, entidade.NomeSelo);

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que remove um Selo da base de dados.
        /// </summary>
        /// <param name="entidade">Selo a ser excluído (somente o identificador é necessário).</param>		
        public void Excluir(Selo entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM Selo ");
            sbSQL.Append("WHERE seloId=@seloId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@seloId", DbType.Int32, entidade.SeloId);


            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que carrega um Selo.
        /// </summary>
        /// <param name="entidade">Selo a ser carregado (somente o identificador é necessário).</param>
        /// <returns>Selo</returns>
        public Selo Carregar(int seloId)
        {
            Selo entidade = new Selo();
            entidade.SeloId = seloId;
            return Carregar(entidade);

        }


        /// <summary>
        /// Método que carrega um Selo.
        /// </summary>
        /// <param name="entidade">Selo a ser carregado (somente o identificador é necessário).</param>
        /// <returns>Selo</returns>
        public Selo Carregar(Selo entidade)
        {

            Selo entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM Selo WHERE seloId=@seloId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@seloId", DbType.Int32, entidade.SeloId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Selo();
                PopulaSelo(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }



        /// <summary>
        /// Método que retorna uma coleção de Selo.
        /// </summary>
        /// <param name="entidade">Produto relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Selo.</returns>
        public IEnumerable<Selo> Carregar(Produto entidade)
        {
            List<Selo> entidadesRetorno = new List<Selo>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Selo.* FROM Selo INNER JOIN ProdutoSelo ON Selo.seloId=ProdutoSelo.seloId WHERE ProdutoSelo.produtoId=@produtoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.ProdutoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Selo entidadeRetorno = new Selo();
                PopulaSelo(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }


        /// <summary>
        /// Método que retorna uma coleção de Selo.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos Selo.</returns>
        public IEnumerable<Selo> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {

            List<Selo> entidadesRetorno = new List<Selo>();

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
                sbOrder.Append(" ORDER BY seloId");
            }


            if (registrosPagina > 0)
            {

                //sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM Selo");
                //if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Selo WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
                //} else {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Selo ORDER BY " + orderBy + ")");				
                //}	
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT Selo.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM Selo ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSQL.Append("SELECT Selo.* FROM Selo ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Selo entidadeRetorno = new Selo();
                PopulaSelo(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna todas os Selo existentes na base de dados.
        /// </summary>
        public IEnumerable<Selo> CarregarTodos()
        {
            return CarregarTodos(0, 0, null, null, null);
        }

        /// <summary>
        /// Método que retorna o total de Selo na base de dados.
        /// </summary>
        /// <returns></returns>
        public int TotalRegistros()
        {
            return TotalRegistros(null);
        }

        /// <summary>
        /// Método que retorna o total de Selo na base de dados, aceita filtro.
        /// </summary>
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
        /// <returns></returns>
        public int TotalRegistros(IFilterHelper filtro)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) AS Total FROM Selo");

            if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Executa a query.

            int resultado = (int)_db.ExecuteScalar(command);


            return resultado;
        }

        /// <summary>
        /// Método que retorna popula um Selo baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Selo a ser populado(.</param>
        public static void PopulaSelo(IDataReader reader, Selo entidade)
        {
            if (reader["seloId"] != DBNull.Value)
                entidade.SeloId = Convert.ToInt32(reader["seloId"].ToString());

            if (reader["nomeSelo"] != DBNull.Value)
                entidade.NomeSelo = reader["nomeSelo"].ToString();


        }
    }
}