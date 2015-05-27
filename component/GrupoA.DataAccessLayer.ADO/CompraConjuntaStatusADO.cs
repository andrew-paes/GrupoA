
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
	public partial class CompraConjuntaStatusADO : ADOSuper, ICompraConjuntaStatusDAL {
	
	    /// <summary>
        /// Método que persiste um CompraConjuntaStatus.
        /// </summary>
        /// <param name="entidade">CompraConjuntaStatus contendo os dados a serem persistidos.</param>	
		public void Inserir(CompraConjuntaStatus entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO CompraConjuntaStatus ");
			sbSQL.Append(" (compraConjuntaStatusId, statusCompra) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@compraConjuntaStatusId, @statusCompra) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@compraConjuntaStatusId", DbType.Int32, entidade.CompraConjuntaStatusId);

			_db.AddInParameter(command, "@statusCompra", DbType.String, entidade.StatusCompra);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um CompraConjuntaStatus.
        /// </summary>
        /// <param name="entidade">CompraConjuntaStatus contendo os dados a serem atualizados.</param>
		public void Atualizar(CompraConjuntaStatus entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE CompraConjuntaStatus SET ");
			sbSQL.Append(" statusCompra=@statusCompra ");
			sbSQL.Append(" WHERE compraConjuntaStatusId=@compraConjuntaStatusId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@compraConjuntaStatusId", DbType.Int32, entidade.CompraConjuntaStatusId);
			_db.AddInParameter(command, "@statusCompra", DbType.String, entidade.StatusCompra);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um CompraConjuntaStatus da base de dados.
        /// </summary>
        /// <param name="entidade">CompraConjuntaStatus a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(CompraConjuntaStatus entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM CompraConjuntaStatus ");
			sbSQL.Append("WHERE compraConjuntaStatusId=@compraConjuntaStatusId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@compraConjuntaStatusId", DbType.Int32, entidade.CompraConjuntaStatusId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um CompraConjuntaStatus.
		/// </summary>
        /// <param name="entidade">CompraConjuntaStatus a ser carregado (somente o identificador é necessário).</param>
		/// <returns>CompraConjuntaStatus</returns>
		public CompraConjuntaStatus Carregar(int compraConjuntaStatusId) {		
			CompraConjuntaStatus entidade = new CompraConjuntaStatus();
			entidade.CompraConjuntaStatusId = compraConjuntaStatusId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um CompraConjuntaStatus.
		/// </summary>
        /// <param name="entidade">CompraConjuntaStatus a ser carregado (somente o identificador é necessário).</param>
		/// <returns>CompraConjuntaStatus</returns>
		public CompraConjuntaStatus Carregar(CompraConjuntaStatus entidade) {		
		
			CompraConjuntaStatus entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM CompraConjuntaStatus WHERE compraConjuntaStatusId=@compraConjuntaStatusId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@compraConjuntaStatusId", DbType.Int32, entidade.CompraConjuntaStatusId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new CompraConjuntaStatus();
				PopulaCompraConjuntaStatus(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de CompraConjuntaStatus.
        /// </summary>
        /// <param name="entidade">CompraConjunta relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de CompraConjuntaStatus.</returns>
		public IEnumerable<CompraConjuntaStatus> Carregar(CompraConjunta entidade)
		{		
			List<CompraConjuntaStatus> entidadesRetorno = new List<CompraConjuntaStatus>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT CompraConjuntaStatus.* FROM CompraConjuntaStatus INNER JOIN CompraConjunta ON CompraConjuntaStatus.compraConjuntaStatusId=CompraConjunta.compraConjuntaStatusId WHERE CompraConjunta.compraConjuntaId=@compraConjuntaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@compraConjuntaId", DbType.Int32, entidade.CompraConjuntaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                CompraConjuntaStatus entidadeRetorno = new CompraConjuntaStatus();
                PopulaCompraConjuntaStatus(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de CompraConjuntaStatus.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos CompraConjuntaStatus.</returns>
		public IEnumerable<CompraConjuntaStatus> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<CompraConjuntaStatus> entidadesRetorno = new List<CompraConjuntaStatus>();
			
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
				sbOrder.Append( " ORDER BY compraConjuntaStatusId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM CompraConjuntaStatus");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM CompraConjuntaStatus WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM CompraConjuntaStatus ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT CompraConjuntaStatus.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM CompraConjuntaStatus ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT CompraConjuntaStatus.* FROM CompraConjuntaStatus ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                CompraConjuntaStatus entidadeRetorno = new CompraConjuntaStatus();
                PopulaCompraConjuntaStatus(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os CompraConjuntaStatus existentes na base de dados.
        /// </summary>
		public IEnumerable<CompraConjuntaStatus> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de CompraConjuntaStatus na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de CompraConjuntaStatus na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM CompraConjuntaStatus");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um CompraConjuntaStatus baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">CompraConjuntaStatus a ser populado(.</param>
		public static void PopulaCompraConjuntaStatus(IDataReader reader, CompraConjuntaStatus entidade) 
		{						
			if (reader["compraConjuntaStatusId"] != DBNull.Value)
				entidade.CompraConjuntaStatusId = Convert.ToInt32(reader["compraConjuntaStatusId"].ToString());
			
			if (reader["statusCompra"] != DBNull.Value)
				entidade.StatusCompra = reader["statusCompra"].ToString();
			

		}		
		
	}
}
		