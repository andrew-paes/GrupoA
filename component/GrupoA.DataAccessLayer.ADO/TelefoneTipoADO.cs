
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
	public partial class TelefoneTipoADO : ADOSuper, ITelefoneTipoDAL {
	
	    /// <summary>
        /// Método que persiste um TelefoneTipo.
        /// </summary>
        /// <param name="entidade">TelefoneTipo contendo os dados a serem persistidos.</param>	
		public void Inserir(TelefoneTipo entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO TelefoneTipo ");
			sbSQL.Append(" (telefoneTipoId, tipoTelefone) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@telefoneTipoId, @tipoTelefone) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@telefoneTipoId", DbType.Int32, entidade.TelefoneTipoId);

			_db.AddInParameter(command, "@tipoTelefone", DbType.String, entidade.TipoTelefone);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um TelefoneTipo.
        /// </summary>
        /// <param name="entidade">TelefoneTipo contendo os dados a serem atualizados.</param>
		public void Atualizar(TelefoneTipo entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE TelefoneTipo SET ");
			sbSQL.Append(" tipoTelefone=@tipoTelefone ");
			sbSQL.Append(" WHERE telefoneTipoId=@telefoneTipoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@telefoneTipoId", DbType.Int32, entidade.TelefoneTipoId);
			_db.AddInParameter(command, "@tipoTelefone", DbType.String, entidade.TipoTelefone);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um TelefoneTipo da base de dados.
        /// </summary>
        /// <param name="entidade">TelefoneTipo a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(TelefoneTipo entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM TelefoneTipo ");
			sbSQL.Append("WHERE telefoneTipoId=@telefoneTipoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@telefoneTipoId", DbType.Int32, entidade.TelefoneTipoId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um TelefoneTipo.
		/// </summary>
        /// <param name="entidade">TelefoneTipo a ser carregado (somente o identificador é necessário).</param>
		/// <returns>TelefoneTipo</returns>
		public TelefoneTipo Carregar(int telefoneTipoId) {		
			TelefoneTipo entidade = new TelefoneTipo();
			entidade.TelefoneTipoId = telefoneTipoId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um TelefoneTipo.
		/// </summary>
        /// <param name="entidade">TelefoneTipo a ser carregado (somente o identificador é necessário).</param>
		/// <returns>TelefoneTipo</returns>
		public TelefoneTipo Carregar(TelefoneTipo entidade) {		
		
			TelefoneTipo entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM TelefoneTipo WHERE telefoneTipoId=@telefoneTipoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@telefoneTipoId", DbType.Int32, entidade.TelefoneTipoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new TelefoneTipo();
				PopulaTelefoneTipo(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de TelefoneTipo.
        /// </summary>
        /// <param name="entidade">Telefone relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de TelefoneTipo.</returns>
		public IEnumerable<TelefoneTipo> Carregar(Telefone entidade)
		{		
			List<TelefoneTipo> entidadesRetorno = new List<TelefoneTipo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT TelefoneTipo.* FROM TelefoneTipo INNER JOIN Telefone ON TelefoneTipo.telefoneTipoId=Telefone.telefoneTipoId WHERE Telefone.telefoneId=@telefoneId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@telefoneId", DbType.Int32, entidade.TelefoneId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                TelefoneTipo entidadeRetorno = new TelefoneTipo();
                PopulaTelefoneTipo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de TelefoneTipo.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos TelefoneTipo.</returns>
		public IEnumerable<TelefoneTipo> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<TelefoneTipo> entidadesRetorno = new List<TelefoneTipo>();
			
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
				sbOrder.Append( " ORDER BY telefoneTipoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM TelefoneTipo");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM TelefoneTipo WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM TelefoneTipo ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT TelefoneTipo.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM TelefoneTipo ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT TelefoneTipo.* FROM TelefoneTipo ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                TelefoneTipo entidadeRetorno = new TelefoneTipo();
                PopulaTelefoneTipo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os TelefoneTipo existentes na base de dados.
        /// </summary>
		public IEnumerable<TelefoneTipo> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de TelefoneTipo na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de TelefoneTipo na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM TelefoneTipo");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um TelefoneTipo baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">TelefoneTipo a ser populado(.</param>
		public static void PopulaTelefoneTipo(IDataReader reader, TelefoneTipo entidade) 
		{						
			if (reader["telefoneTipoId"] != DBNull.Value)
				entidade.TelefoneTipoId = Convert.ToInt32(reader["telefoneTipoId"].ToString());
			
			if (reader["tipoTelefone"] != DBNull.Value)
				entidade.TipoTelefone = reader["tipoTelefone"].ToString();
			

		}		
		
	}
}
		