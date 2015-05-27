
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
	public partial class LinkADO : ADOSuper, ILinkDAL {
	
	    /// <summary>
        /// Método que persiste um Link.
        /// </summary>
        /// <param name="entidade">Link contendo os dados a serem persistidos.</param>	
		public void Inserir(Link entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO Link ");
			sbSQL.Append(" (nomeLink, urlLink, ativo, targetBlank) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@nomeLink, @urlLink, @ativo, @targetBlank) ");											

			sbSQL.Append(" ; SET @linkId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@linkId", DbType.Int32, 8);

			_db.AddInParameter(command, "@nomeLink", DbType.String, entidade.NomeLink);

			_db.AddInParameter(command, "@urlLink", DbType.String, entidade.UrlLink);

			_db.AddInParameter(command, "@ativo", DbType.Int32, entidade.Ativo);

			_db.AddInParameter(command, "@targetBlank", DbType.Int32, entidade.TargetBlank);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.LinkId = Convert.ToInt32(_db.GetParameterValue(command, "@linkId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um Link.
        /// </summary>
        /// <param name="entidade">Link contendo os dados a serem atualizados.</param>
		public void Atualizar(Link entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE Link SET ");
			sbSQL.Append(" nomeLink=@nomeLink, urlLink=@urlLink, ativo=@ativo, targetBlank=@targetBlank ");
			sbSQL.Append(" WHERE linkId=@linkId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@linkId", DbType.Int32, entidade.LinkId);
			_db.AddInParameter(command, "@nomeLink", DbType.String, entidade.NomeLink);
			_db.AddInParameter(command, "@urlLink", DbType.String, entidade.UrlLink);
			_db.AddInParameter(command, "@ativo", DbType.Int32, entidade.Ativo);
			_db.AddInParameter(command, "@targetBlank", DbType.Int32, entidade.TargetBlank);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um Link da base de dados.
        /// </summary>
        /// <param name="entidade">Link a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(Link entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM Link ");
			sbSQL.Append("WHERE linkId=@linkId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@linkId", DbType.Int32, entidade.LinkId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um Link.
		/// </summary>
        /// <param name="entidade">Link a ser carregado (somente o identificador é necessário).</param>
		/// <returns>Link</returns>
		public Link Carregar(int linkId) {		
			Link entidade = new Link();
			entidade.LinkId = linkId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um Link.
		/// </summary>
        /// <param name="entidade">Link a ser carregado (somente o identificador é necessário).</param>
		/// <returns>Link</returns>
		public Link Carregar(Link entidade) {		
		
			Link entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM Link WHERE linkId=@linkId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@linkId", DbType.Int32, entidade.LinkId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new Link();
				PopulaLink(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		

		
		
		/// <summary>
        /// Método que retorna uma coleção de Link.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos Link.</returns>
		public IEnumerable<Link> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<Link> entidadesRetorno = new List<Link>();
			
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
				sbOrder.Append( " ORDER BY linkId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM Link");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Link WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Link ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT Link.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM Link ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT Link.* FROM Link ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Link entidadeRetorno = new Link();
                PopulaLink(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os Link existentes na base de dados.
        /// </summary>
		public IEnumerable<Link> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de Link na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de Link na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM Link");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um Link baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Link a ser populado(.</param>
		public static void PopulaLink(IDataReader reader, Link entidade) 
		{						
			if (reader["linkId"] != DBNull.Value)
				entidade.LinkId = Convert.ToInt32(reader["linkId"].ToString());
			
			if (reader["nomeLink"] != DBNull.Value)
				entidade.NomeLink = reader["nomeLink"].ToString();
			
			if (reader["urlLink"] != DBNull.Value)
				entidade.UrlLink = reader["urlLink"].ToString();
			
			if (reader["ativo"] != DBNull.Value)
				entidade.Ativo = Convert.ToBoolean(reader["ativo"].ToString());
			
			if (reader["targetBlank"] != DBNull.Value)
				entidade.TargetBlank = Convert.ToBoolean(reader["targetBlank"].ToString());
			

		}		
		
	}
}
		