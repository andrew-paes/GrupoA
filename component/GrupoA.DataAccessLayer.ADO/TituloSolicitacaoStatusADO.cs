
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
	public partial class TituloSolicitacaoStatusADO : ADOSuper, ITituloSolicitacaoStatusDAL {
	
	    /// <summary>
        /// Método que persiste um TituloSolicitacaoStatus.
        /// </summary>
        /// <param name="entidade">TituloSolicitacaoStatus contendo os dados a serem persistidos.</param>	
		public void Inserir(TituloSolicitacaoStatus entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO TituloSolicitacaoStatus ");
			sbSQL.Append(" (tituloSolicitacaoStatusId, statusSolicitacao) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@tituloSolicitacaoStatusId, @statusSolicitacao) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@tituloSolicitacaoStatusId", DbType.Int32, entidade.TituloSolicitacaoStatusId);

			_db.AddInParameter(command, "@statusSolicitacao", DbType.String, entidade.StatusSolicitacao);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um TituloSolicitacaoStatus.
        /// </summary>
        /// <param name="entidade">TituloSolicitacaoStatus contendo os dados a serem atualizados.</param>
		public void Atualizar(TituloSolicitacaoStatus entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE TituloSolicitacaoStatus SET ");
			sbSQL.Append(" statusSolicitacao=@statusSolicitacao ");
			sbSQL.Append(" WHERE tituloSolicitacaoStatusId=@tituloSolicitacaoStatusId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@tituloSolicitacaoStatusId", DbType.Int32, entidade.TituloSolicitacaoStatusId);
			_db.AddInParameter(command, "@statusSolicitacao", DbType.String, entidade.StatusSolicitacao);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um TituloSolicitacaoStatus da base de dados.
        /// </summary>
        /// <param name="entidade">TituloSolicitacaoStatus a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(TituloSolicitacaoStatus entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM TituloSolicitacaoStatus ");
			sbSQL.Append("WHERE tituloSolicitacaoStatusId=@tituloSolicitacaoStatusId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@tituloSolicitacaoStatusId", DbType.Int32, entidade.TituloSolicitacaoStatusId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um TituloSolicitacaoStatus.
		/// </summary>
        /// <param name="entidade">TituloSolicitacaoStatus a ser carregado (somente o identificador é necessário).</param>
		/// <returns>TituloSolicitacaoStatus</returns>
		public TituloSolicitacaoStatus Carregar(int tituloSolicitacaoStatusId) {		
			TituloSolicitacaoStatus entidade = new TituloSolicitacaoStatus();
			entidade.TituloSolicitacaoStatusId = tituloSolicitacaoStatusId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um TituloSolicitacaoStatus.
		/// </summary>
        /// <param name="entidade">TituloSolicitacaoStatus a ser carregado (somente o identificador é necessário).</param>
		/// <returns>TituloSolicitacaoStatus</returns>
		public TituloSolicitacaoStatus Carregar(TituloSolicitacaoStatus entidade) {		
		
			TituloSolicitacaoStatus entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM TituloSolicitacaoStatus WHERE tituloSolicitacaoStatusId=@tituloSolicitacaoStatusId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@tituloSolicitacaoStatusId", DbType.Int32, entidade.TituloSolicitacaoStatusId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new TituloSolicitacaoStatus();
				PopulaTituloSolicitacaoStatus(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de TituloSolicitacaoStatus.
        /// </summary>
        /// <param name="entidade">TituloSolicitacao relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de TituloSolicitacaoStatus.</returns>
		public IEnumerable<TituloSolicitacaoStatus> Carregar(TituloSolicitacao entidade)
		{		
			List<TituloSolicitacaoStatus> entidadesRetorno = new List<TituloSolicitacaoStatus>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT TituloSolicitacaoStatus.* FROM TituloSolicitacaoStatus INNER JOIN TituloSolicitacao ON TituloSolicitacaoStatus.tituloSolicitacaoStatusId=TituloSolicitacao.tituloSolicitacaoStatusId WHERE TituloSolicitacao.tituloSolicitacaoId=@tituloSolicitacaoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@tituloSolicitacaoId", DbType.Int32, entidade.TituloSolicitacaoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                TituloSolicitacaoStatus entidadeRetorno = new TituloSolicitacaoStatus();
                PopulaTituloSolicitacaoStatus(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de TituloSolicitacaoStatus.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos TituloSolicitacaoStatus.</returns>
		public IEnumerable<TituloSolicitacaoStatus> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<TituloSolicitacaoStatus> entidadesRetorno = new List<TituloSolicitacaoStatus>();
			
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
				sbOrder.Append( " ORDER BY tituloSolicitacaoStatusId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM TituloSolicitacaoStatus");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM TituloSolicitacaoStatus WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM TituloSolicitacaoStatus ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT TituloSolicitacaoStatus.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM TituloSolicitacaoStatus ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT TituloSolicitacaoStatus.* FROM TituloSolicitacaoStatus ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                TituloSolicitacaoStatus entidadeRetorno = new TituloSolicitacaoStatus();
                PopulaTituloSolicitacaoStatus(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os TituloSolicitacaoStatus existentes na base de dados.
        /// </summary>
		public IEnumerable<TituloSolicitacaoStatus> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de TituloSolicitacaoStatus na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de TituloSolicitacaoStatus na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM TituloSolicitacaoStatus");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um TituloSolicitacaoStatus baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">TituloSolicitacaoStatus a ser populado(.</param>
		public static void PopulaTituloSolicitacaoStatus(IDataReader reader, TituloSolicitacaoStatus entidade) 
		{						
			if (reader["tituloSolicitacaoStatusId"] != DBNull.Value)
				entidade.TituloSolicitacaoStatusId = Convert.ToInt32(reader["tituloSolicitacaoStatusId"].ToString());
			
			if (reader["statusSolicitacao"] != DBNull.Value)
				entidade.StatusSolicitacao = reader["statusSolicitacao"].ToString();
			

		}		
		
	}
}
		