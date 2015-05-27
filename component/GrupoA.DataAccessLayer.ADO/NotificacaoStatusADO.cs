
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
	public partial class NotificacaoStatusADO : ADOSuper, INotificacaoStatusDAL {
	
	    /// <summary>
        /// Método que persiste um NotificacaoStatus.
        /// </summary>
        /// <param name="entidade">NotificacaoStatus contendo os dados a serem persistidos.</param>	
		public void Inserir(NotificacaoStatus entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO NotificacaoStatus ");
			sbSQL.Append(" (notificacaoStatusId, statusNotificacao) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@notificacaoStatusId, @statusNotificacao) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@notificacaoStatusId", DbType.Int32, entidade.NotificacaoStatusId);

			_db.AddInParameter(command, "@statusNotificacao", DbType.String, entidade.StatusNotificacao);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um NotificacaoStatus.
        /// </summary>
        /// <param name="entidade">NotificacaoStatus contendo os dados a serem atualizados.</param>
		public void Atualizar(NotificacaoStatus entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE NotificacaoStatus SET ");
			sbSQL.Append(" statusNotificacao=@statusNotificacao ");
			sbSQL.Append(" WHERE notificacaoStatusId=@notificacaoStatusId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@notificacaoStatusId", DbType.Int32, entidade.NotificacaoStatusId);
			_db.AddInParameter(command, "@statusNotificacao", DbType.String, entidade.StatusNotificacao);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um NotificacaoStatus da base de dados.
        /// </summary>
        /// <param name="entidade">NotificacaoStatus a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(NotificacaoStatus entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM NotificacaoStatus ");
			sbSQL.Append("WHERE notificacaoStatusId=@notificacaoStatusId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@notificacaoStatusId", DbType.Int32, entidade.NotificacaoStatusId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um NotificacaoStatus.
		/// </summary>
        /// <param name="entidade">NotificacaoStatus a ser carregado (somente o identificador é necessário).</param>
		/// <returns>NotificacaoStatus</returns>
		public NotificacaoStatus Carregar(int notificacaoStatusId) {		
			NotificacaoStatus entidade = new NotificacaoStatus();
			entidade.NotificacaoStatusId = notificacaoStatusId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um NotificacaoStatus.
		/// </summary>
        /// <param name="entidade">NotificacaoStatus a ser carregado (somente o identificador é necessário).</param>
		/// <returns>NotificacaoStatus</returns>
		public NotificacaoStatus Carregar(NotificacaoStatus entidade) {		
		
			NotificacaoStatus entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM NotificacaoStatus WHERE notificacaoStatusId=@notificacaoStatusId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@notificacaoStatusId", DbType.Int32, entidade.NotificacaoStatusId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new NotificacaoStatus();
				PopulaNotificacaoStatus(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de NotificacaoStatus.
        /// </summary>
        /// <param name="entidade">NotificacaoDisponibilidade relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de NotificacaoStatus.</returns>
		public IEnumerable<NotificacaoStatus> Carregar(NotificacaoDisponibilidade entidade)
		{		
			List<NotificacaoStatus> entidadesRetorno = new List<NotificacaoStatus>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT NotificacaoStatus.* FROM NotificacaoStatus INNER JOIN NotificacaoDisponibilidade ON NotificacaoStatus.notificacaoStatusId=NotificacaoDisponibilidade.notificacaoStatusId WHERE NotificacaoDisponibilidade.notificacaoDisponibilidadeId=@notificacaoDisponibilidadeId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@notificacaoDisponibilidadeId", DbType.Int32, entidade.NotificacaoDisponibilidadeId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                NotificacaoStatus entidadeRetorno = new NotificacaoStatus();
                PopulaNotificacaoStatus(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de NotificacaoStatus.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos NotificacaoStatus.</returns>
		public IEnumerable<NotificacaoStatus> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<NotificacaoStatus> entidadesRetorno = new List<NotificacaoStatus>();
			
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
				sbOrder.Append( " ORDER BY notificacaoStatusId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM NotificacaoStatus");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM NotificacaoStatus WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM NotificacaoStatus ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT NotificacaoStatus.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM NotificacaoStatus ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT NotificacaoStatus.* FROM NotificacaoStatus ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                NotificacaoStatus entidadeRetorno = new NotificacaoStatus();
                PopulaNotificacaoStatus(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os NotificacaoStatus existentes na base de dados.
        /// </summary>
		public IEnumerable<NotificacaoStatus> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de NotificacaoStatus na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de NotificacaoStatus na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM NotificacaoStatus");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um NotificacaoStatus baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">NotificacaoStatus a ser populado(.</param>
		public static void PopulaNotificacaoStatus(IDataReader reader, NotificacaoStatus entidade) 
		{						
			if (reader["notificacaoStatusId"] != DBNull.Value)
				entidade.NotificacaoStatusId = Convert.ToInt32(reader["notificacaoStatusId"].ToString());
			
			if (reader["statusNotificacao"] != DBNull.Value)
				entidade.StatusNotificacao = reader["statusNotificacao"].ToString();
			

		}		
		
	}
}
		