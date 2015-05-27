
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
	public partial class LogEventoADO : ADOSuper, ILogEventoDAL {
	
	    /// <summary>
        /// Método que persiste um LogEvento.
        /// </summary>
        /// <param name="entidade">LogEvento contendo os dados a serem persistidos.</param>	
		public void Inserir(LogEvento entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO LogEvento ");
			sbSQL.Append(" (logEventoId, logCategoriaId, evento, descricaoEvento) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@logEventoId, @logCategoriaId, @evento, @descricaoEvento) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@logEventoId", DbType.Int32, entidade.LogEventoId);

			_db.AddInParameter(command, "@logCategoriaId", DbType.Int32, entidade.LogCategoria.LogCategoriaId);

			_db.AddInParameter(command, "@evento", DbType.String, entidade.Evento);

			if (entidade.DescricaoEvento != null ) 
				_db.AddInParameter(command, "@descricaoEvento", DbType.String, entidade.DescricaoEvento);
			else
				_db.AddInParameter(command, "@descricaoEvento", DbType.String, null);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um LogEvento.
        /// </summary>
        /// <param name="entidade">LogEvento contendo os dados a serem atualizados.</param>
		public void Atualizar(LogEvento entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE LogEvento SET ");
			sbSQL.Append(" logCategoriaId=@logCategoriaId, evento=@evento, descricaoEvento=@descricaoEvento ");
			sbSQL.Append(" WHERE logEventoId=@logEventoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@logEventoId", DbType.Int32, entidade.LogEventoId);
			_db.AddInParameter(command, "@logCategoriaId", DbType.Int32, entidade.LogCategoria.LogCategoriaId);
			_db.AddInParameter(command, "@evento", DbType.String, entidade.Evento);
			if (entidade.DescricaoEvento != null ) 
				_db.AddInParameter(command, "@descricaoEvento", DbType.String, entidade.DescricaoEvento);
			else
				_db.AddInParameter(command, "@descricaoEvento", DbType.String, null);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um LogEvento da base de dados.
        /// </summary>
        /// <param name="entidade">LogEvento a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(LogEvento entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM LogEvento ");
			sbSQL.Append("WHERE logEventoId=@logEventoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@logEventoId", DbType.Int32, entidade.LogEventoId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um LogEvento.
		/// </summary>
        /// <param name="entidade">LogEvento a ser carregado (somente o identificador é necessário).</param>
		/// <returns>LogEvento</returns>
		public LogEvento Carregar(int logEventoId) {		
			LogEvento entidade = new LogEvento();
			entidade.LogEventoId = logEventoId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um LogEvento.
		/// </summary>
        /// <param name="entidade">LogEvento a ser carregado (somente o identificador é necessário).</param>
		/// <returns>LogEvento</returns>
		public LogEvento Carregar(LogEvento entidade) {		
		
			LogEvento entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM LogEvento WHERE logEventoId=@logEventoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@logEventoId", DbType.Int32, entidade.LogEventoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new LogEvento();
				PopulaLogEvento(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de LogEvento.
        /// </summary>
        /// <param name="entidade">LogOcorrencia relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de LogEvento.</returns>
		public IEnumerable<LogEvento> Carregar(LogOcorrencia entidade)
		{		
			List<LogEvento> entidadesRetorno = new List<LogEvento>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT LogEvento.* FROM LogEvento INNER JOIN LogOcorrencia ON LogEvento.logEventoId=LogOcorrencia.logEventoId WHERE LogOcorrencia.logOcorrenciaId=@logOcorrenciaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@logOcorrenciaId", DbType.Int32, entidade.LogOcorrenciaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                LogEvento entidadeRetorno = new LogEvento();
                PopulaLogEvento(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de LogEvento.
        /// </summary>
        /// <param name="entidade">LogCategoria relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de LogEvento.</returns>
		public IEnumerable<LogEvento> Carregar(LogCategoria entidade)
		{		
			List<LogEvento> entidadesRetorno = new List<LogEvento>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT LogEvento.* FROM LogEvento WHERE LogEvento.logCategoriaId=@logCategoriaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@logCategoriaId", DbType.Int32, entidade.LogCategoriaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                LogEvento entidadeRetorno = new LogEvento();
                PopulaLogEvento(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de LogEvento.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos LogEvento.</returns>
		public IEnumerable<LogEvento> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<LogEvento> entidadesRetorno = new List<LogEvento>();
			
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
				sbOrder.Append( " ORDER BY logEventoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM LogEvento");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM LogEvento WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM LogEvento ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT LogEvento.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM LogEvento ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT LogEvento.* FROM LogEvento ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                LogEvento entidadeRetorno = new LogEvento();
                PopulaLogEvento(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os LogEvento existentes na base de dados.
        /// </summary>
		public IEnumerable<LogEvento> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de LogEvento na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de LogEvento na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM LogEvento");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um LogEvento baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">LogEvento a ser populado(.</param>
		public static void PopulaLogEvento(IDataReader reader, LogEvento entidade) 
		{						
			if (reader["logEventoId"] != DBNull.Value)
				entidade.LogEventoId = Convert.ToInt32(reader["logEventoId"].ToString());
			
			if (reader["evento"] != DBNull.Value)
				entidade.Evento = reader["evento"].ToString();
			
			if (reader["descricaoEvento"] != DBNull.Value)
				entidade.DescricaoEvento = reader["descricaoEvento"].ToString();
			
			if (reader["logCategoriaId"] != DBNull.Value) {
				entidade.LogCategoria = new LogCategoria();
				entidade.LogCategoria.LogCategoriaId = Convert.ToInt32(reader["logCategoriaId"].ToString());
			}


		}		
		
	}
}
		