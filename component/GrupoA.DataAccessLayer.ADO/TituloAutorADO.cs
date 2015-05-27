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
    public partial class TituloAutorADO : ADOSuper, ITituloAutorDAL
    {
        /// <summary>
        /// Método que persiste um TituloAutor.
        /// </summary>
        /// <param name="entidade">TituloAutor contendo os dados a serem persistidos.</param>	
        public void Inserir(TituloAutor entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO TituloAutor ");
            sbSQL.Append(" (tituloId, autorId, principal, ordem) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@tituloId, @autorId, @principal, @ordem) ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@tituloId", DbType.Int32, entidade.Titulo.TituloId);

            _db.AddInParameter(command, "@autorId", DbType.Int32, entidade.Autor.AutorId);

            _db.AddInParameter(command, "@principal", DbType.Int32, entidade.Principal);

            _db.AddInParameter(command, "@ordem", DbType.Int32, entidade.Ordem);

            // Executa a query.
            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que atualiza os dados de um TituloAutor.
        /// </summary>
        /// <param name="entidade">TituloAutor contendo os dados a serem atualizados.</param>
        public void Atualizar(TituloAutor entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE TituloAutor SET ");
            sbSQL.Append(" principal=@principal ");
            sbSQL.Append(" WHERE tituloId=@tituloId AND autorId=@autorId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@tituloId", DbType.Int32, entidade.Titulo.TituloId);
            _db.AddInParameter(command, "@autorId", DbType.Int32, entidade.Autor.AutorId);
            _db.AddInParameter(command, "@principal", DbType.Int32, entidade.Principal);

            // Executa a query.
            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que remove um TituloAutor da base de dados.
        /// </summary>
        /// <param name="entidade">TituloAutor a ser excluído (somente o identificador é necessário).</param>		
        public void Excluir(TituloAutor entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM TituloAutor ");
            sbSQL.Append("WHERE tituloId=@tituloId AND autorId=@autorId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@tituloId", DbType.Int32, entidade.Titulo.TituloId);
            _db.AddInParameter(command, "@autorId", DbType.Int32, entidade.Autor.AutorId);

            _db.ExecuteNonQuery(command);
        }


        /// <summary>
        /// Método que carrega um TituloAutor.
        /// </summary>
        /// <param name="entidade">TituloAutor a ser carregado (somente o identificador é necessário).</param>
        /// <returns>TituloAutor</returns>
        public TituloAutor Carregar(TituloAutor entidade)
        {
            TituloAutor entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM TituloAutor WHERE tituloId=@tituloId AND autorId=@autorId");
            sbSQL.Append(" ORDER BY ordem");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@tituloId", DbType.Int32, entidade.Titulo.TituloId);
            _db.AddInParameter(command, "@autorId", DbType.Int32, entidade.Autor.AutorId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new TituloAutor();
                PopulaTituloAutor(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// Método que retorna uma coleção de TituloAutor.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos TituloAutor.</returns>
        public IEnumerable<TituloAutor> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {
            List<TituloAutor> entidadesRetorno = new List<TituloAutor>();

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
                sbOrder.Append(" ORDER BY ordem");
            }

            if (registrosPagina > 0)
            {

                //sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM TituloAutor");
                //if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM TituloAutor WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
                //} else {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM TituloAutor ORDER BY " + orderBy + ")");				
                //}	
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT TituloAutor.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM TituloAutor ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());
            }
            else
            {
                sbSQL.Append("SELECT TituloAutor.* FROM TituloAutor ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                TituloAutor entidadeRetorno = new TituloAutor();
                PopulaTituloAutor(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }

            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// Método que retorna todas os TituloAutor existentes na base de dados.
        /// </summary>
        public IEnumerable<TituloAutor> CarregarTodos()
        {
            return CarregarTodos(0, 0, null, null, null);
        }

        /// <summary>
        /// Método que retorna o total de TituloAutor na base de dados.
        /// </summary>
        /// <returns></returns>
        public int TotalRegistros()
        {
            return TotalRegistros(null);
        }

        /// <summary>
        /// Método que retorna o total de TituloAutor na base de dados, aceita filtro.
        /// </summary>
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
        /// <returns></returns>
        public int TotalRegistros(IFilterHelper filtro)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) AS Total FROM TituloAutor");

            if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Executa a query.

            int resultado = (int)_db.ExecuteScalar(command);


            return resultado;
        }

        /// <summary>
        /// Método que retorna popula um TituloAutor baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">TituloAutor a ser populado(.</param>
        public static void PopulaTituloAutor(IDataReader reader, TituloAutor entidade)
        {
            if (reader["principal"] != DBNull.Value)
                entidade.Principal = Convert.ToBoolean(reader["principal"].ToString());

            if (reader["tituloId"] != DBNull.Value)
            {
                entidade.Titulo = new Titulo();
                entidade.Titulo.TituloId = Convert.ToInt32(reader["tituloId"].ToString());
            }

            if (reader["autorId"] != DBNull.Value)
            {
                entidade.Autor = new Autor();
                entidade.Autor.AutorId = Convert.ToInt32(reader["autorId"].ToString());
            }
        }
    }
}