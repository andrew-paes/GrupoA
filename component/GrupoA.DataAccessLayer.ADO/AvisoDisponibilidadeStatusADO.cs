
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
	public partial class AvisoDisponibilidadeStatusADO : ADOSuper, IAvisoDisponibilidadeStatusDAL {
	
	    /// <summary>
        /// Método que persiste um AvisoDisponibilidadeStatus.
        /// </summary>
        /// <param name="entidade">AvisoDisponibilidadeStatus contendo os dados a serem persistidos.</param>	
		public void Inserir(AvisoDisponibilidadeStatus entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO AvisoDisponibilidadeStatus ");
			sbSQL.Append(" (avisoDisponibilidadeStatusId, statusAviso) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@avisoDisponibilidadeStatusId, @statusAviso) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@avisoDisponibilidadeStatusId", DbType.Int32, entidade.AvisoDisponibilidadeStatusId);

			_db.AddInParameter(command, "@statusAviso", DbType.String, entidade.StatusAviso);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um AvisoDisponibilidadeStatus.
        /// </summary>
        /// <param name="entidade">AvisoDisponibilidadeStatus contendo os dados a serem atualizados.</param>
		public void Atualizar(AvisoDisponibilidadeStatus entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE AvisoDisponibilidadeStatus SET ");
			sbSQL.Append(" statusAviso=@statusAviso ");
			sbSQL.Append(" WHERE avisoDisponibilidadeStatusId=@avisoDisponibilidadeStatusId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@avisoDisponibilidadeStatusId", DbType.Int32, entidade.AvisoDisponibilidadeStatusId);
			_db.AddInParameter(command, "@statusAviso", DbType.String, entidade.StatusAviso);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um AvisoDisponibilidadeStatus da base de dados.
        /// </summary>
        /// <param name="entidade">AvisoDisponibilidadeStatus a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(AvisoDisponibilidadeStatus entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM AvisoDisponibilidadeStatus ");
			sbSQL.Append("WHERE avisoDisponibilidadeStatusId=@avisoDisponibilidadeStatusId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@avisoDisponibilidadeStatusId", DbType.Int32, entidade.AvisoDisponibilidadeStatusId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um AvisoDisponibilidadeStatus.
		/// </summary>
        /// <param name="entidade">AvisoDisponibilidadeStatus a ser carregado (somente o identificador é necessário).</param>
		/// <returns>AvisoDisponibilidadeStatus</returns>
		public AvisoDisponibilidadeStatus Carregar(int avisoDisponibilidadeStatusId) {		
			AvisoDisponibilidadeStatus entidade = new AvisoDisponibilidadeStatus();
			entidade.AvisoDisponibilidadeStatusId = avisoDisponibilidadeStatusId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um AvisoDisponibilidadeStatus.
		/// </summary>
        /// <param name="entidade">AvisoDisponibilidadeStatus a ser carregado (somente o identificador é necessário).</param>
		/// <returns>AvisoDisponibilidadeStatus</returns>
		public AvisoDisponibilidadeStatus Carregar(AvisoDisponibilidadeStatus entidade) {		
		
			AvisoDisponibilidadeStatus entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM AvisoDisponibilidadeStatus WHERE avisoDisponibilidadeStatusId=@avisoDisponibilidadeStatusId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@avisoDisponibilidadeStatusId", DbType.Int32, entidade.AvisoDisponibilidadeStatusId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new AvisoDisponibilidadeStatus();
				PopulaAvisoDisponibilidadeStatus(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de AvisoDisponibilidadeStatus.
        /// </summary>
        /// <param name="entidade">AvisoDisponibilidade relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de AvisoDisponibilidadeStatus.</returns>
		public IEnumerable<AvisoDisponibilidadeStatus> Carregar(AvisoDisponibilidade entidade)
		{		
			List<AvisoDisponibilidadeStatus> entidadesRetorno = new List<AvisoDisponibilidadeStatus>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT AvisoDisponibilidadeStatus.* FROM AvisoDisponibilidadeStatus INNER JOIN AvisoDisponibilidade ON AvisoDisponibilidadeStatus.avisoDisponibilidadeStatusId=AvisoDisponibilidade.avisoDisponibilidadeStatusId WHERE AvisoDisponibilidade.avisoDisponibilidadeId=@avisoDisponibilidadeId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@avisoDisponibilidadeId", DbType.Int32, entidade.AvisoDisponibilidadeId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                AvisoDisponibilidadeStatus entidadeRetorno = new AvisoDisponibilidadeStatus();
                PopulaAvisoDisponibilidadeStatus(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de AvisoDisponibilidadeStatus.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos AvisoDisponibilidadeStatus.</returns>
		public IEnumerable<AvisoDisponibilidadeStatus> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<AvisoDisponibilidadeStatus> entidadesRetorno = new List<AvisoDisponibilidadeStatus>();
			
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
				sbOrder.Append( " ORDER BY avisoDisponibilidadeStatusId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM AvisoDisponibilidadeStatus");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM AvisoDisponibilidadeStatus WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM AvisoDisponibilidadeStatus ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT AvisoDisponibilidadeStatus.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM AvisoDisponibilidadeStatus ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT AvisoDisponibilidadeStatus.* FROM AvisoDisponibilidadeStatus ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                AvisoDisponibilidadeStatus entidadeRetorno = new AvisoDisponibilidadeStatus();
                PopulaAvisoDisponibilidadeStatus(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os AvisoDisponibilidadeStatus existentes na base de dados.
        /// </summary>
		public IEnumerable<AvisoDisponibilidadeStatus> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de AvisoDisponibilidadeStatus na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de AvisoDisponibilidadeStatus na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM AvisoDisponibilidadeStatus");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um AvisoDisponibilidadeStatus baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">AvisoDisponibilidadeStatus a ser populado(.</param>
		public static void PopulaAvisoDisponibilidadeStatus(IDataReader reader, AvisoDisponibilidadeStatus entidade) 
		{						
			if (reader["avisoDisponibilidadeStatusId"] != DBNull.Value)
				entidade.AvisoDisponibilidadeStatusId = Convert.ToInt32(reader["avisoDisponibilidadeStatusId"].ToString());
			
			if (reader["statusAviso"] != DBNull.Value)
				entidade.StatusAviso = reader["statusAviso"].ToString();
			

		}		
		
	}
}
		