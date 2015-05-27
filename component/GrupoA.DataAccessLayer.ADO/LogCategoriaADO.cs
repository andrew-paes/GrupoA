
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
	public partial class LogCategoriaADO : ADOSuper, ILogCategoriaDAL {
	
	    /// <summary>
        /// Método que persiste um LogCategoria.
        /// </summary>
        /// <param name="entidade">LogCategoria contendo os dados a serem persistidos.</param>	
		public void Inserir(LogCategoria entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO LogCategoria ");
			sbSQL.Append(" (logCategoriaId, categoria) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@logCategoriaId, @categoria) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@logCategoriaId", DbType.Int32, entidade.LogCategoriaId);

			_db.AddInParameter(command, "@categoria", DbType.String, entidade.Categoria);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um LogCategoria.
        /// </summary>
        /// <param name="entidade">LogCategoria contendo os dados a serem atualizados.</param>
		public void Atualizar(LogCategoria entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE LogCategoria SET ");
			sbSQL.Append(" categoria=@categoria ");
			sbSQL.Append(" WHERE logCategoriaId=@logCategoriaId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@logCategoriaId", DbType.Int32, entidade.LogCategoriaId);
			_db.AddInParameter(command, "@categoria", DbType.String, entidade.Categoria);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um LogCategoria da base de dados.
        /// </summary>
        /// <param name="entidade">LogCategoria a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(LogCategoria entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM LogCategoria ");
			sbSQL.Append("WHERE logCategoriaId=@logCategoriaId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@logCategoriaId", DbType.Int32, entidade.LogCategoriaId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um LogCategoria.
		/// </summary>
        /// <param name="entidade">LogCategoria a ser carregado (somente o identificador é necessário).</param>
		/// <returns>LogCategoria</returns>
		public LogCategoria Carregar(int logCategoriaId) {		
			LogCategoria entidade = new LogCategoria();
			entidade.LogCategoriaId = logCategoriaId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um LogCategoria.
		/// </summary>
        /// <param name="entidade">LogCategoria a ser carregado (somente o identificador é necessário).</param>
		/// <returns>LogCategoria</returns>
		public LogCategoria Carregar(LogCategoria entidade) {		
		
			LogCategoria entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM LogCategoria WHERE logCategoriaId=@logCategoriaId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@logCategoriaId", DbType.Int32, entidade.LogCategoriaId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new LogCategoria();
				PopulaLogCategoria(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de LogCategoria.
        /// </summary>
        /// <param name="entidade">LogEvento relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de LogCategoria.</returns>
		public IEnumerable<LogCategoria> Carregar(LogEvento entidade)
		{		
			List<LogCategoria> entidadesRetorno = new List<LogCategoria>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT LogCategoria.* FROM LogCategoria INNER JOIN LogEvento ON LogCategoria.logCategoriaId=LogEvento.logCategoriaId WHERE LogEvento.logEventoId=@logEventoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@logEventoId", DbType.Int32, entidade.LogEventoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                LogCategoria entidadeRetorno = new LogCategoria();
                PopulaLogCategoria(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de LogCategoria.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos LogCategoria.</returns>
		public IEnumerable<LogCategoria> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<LogCategoria> entidadesRetorno = new List<LogCategoria>();
			
			StringBuilder sbSQL = new StringBuilder();
			StringBuilder sbWhere = new StringBuilder();
			StringBuilder sbOrder = new StringBuilder();
			DbCommand command;
			IDataReader reader;
			
			// Monta o "OrderBy"
			if (ordemColunas!=null) {
				for(int i=0; i<ordemColunas.Length; i++) {
					if (sbOrder.Length>0) { sbOrder.Append( ", " ); }
					sbOrder.Append(ordemColunas[i] + " " + ordemSentidos[i]);
				} 
				if (sbOrder.Length > 0) { sbOrder.Insert(0, " ORDER BY "); }				
			} else {
				sbOrder.Append( " ORDER BY logCategoriaId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM LogCategoria");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM LogCategoria WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM LogCategoria ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT LogCategoria.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM LogCategoria ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT LogCategoria.* FROM LogCategoria ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                LogCategoria entidadeRetorno = new LogCategoria();
                PopulaLogCategoria(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os LogCategoria existentes na base de dados.
        /// </summary>
		public IEnumerable<LogCategoria> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de LogCategoria na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de LogCategoria na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM LogCategoria");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um LogCategoria baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">LogCategoria a ser populado(.</param>
		public static void PopulaLogCategoria(IDataReader reader, LogCategoria entidade) 
		{						
			if (reader["logCategoriaId"] != DBNull.Value)
				entidade.LogCategoriaId = Convert.ToInt32(reader["logCategoriaId"].ToString());
			
			if (reader["categoria"] != DBNull.Value)
				entidade.Categoria = reader["categoria"].ToString();
			

		}		
		
	}
}
		