
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
	public partial class RevistaSecaoADO : ADOSuper, IRevistaSecaoDAL {
	
	    /// <summary>
        /// Método que persiste um RevistaSecao.
        /// </summary>
        /// <param name="entidade">RevistaSecao contendo os dados a serem persistidos.</param>	
		public void Inserir(RevistaSecao entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO RevistaSecao ");
			sbSQL.Append(" (nomeSecao, revistaId) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@nomeSecao, @revistaId) ");											

			sbSQL.Append(" ; SET @revistaSecaoId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@revistaSecaoId", DbType.Int32, 8);

			_db.AddInParameter(command, "@nomeSecao", DbType.String, entidade.NomeSecao);

			_db.AddInParameter(command, "@revistaId", DbType.Int32, entidade.Revista.RevistaId);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.RevistaSecaoId = Convert.ToInt32(_db.GetParameterValue(command, "@revistaSecaoId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um RevistaSecao.
        /// </summary>
        /// <param name="entidade">RevistaSecao contendo os dados a serem atualizados.</param>
		public void Atualizar(RevistaSecao entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE RevistaSecao SET ");
			sbSQL.Append(" nomeSecao=@nomeSecao, revistaId=@revistaId ");
			sbSQL.Append(" WHERE revistaSecaoId=@revistaSecaoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@revistaSecaoId", DbType.Int32, entidade.RevistaSecaoId);
			_db.AddInParameter(command, "@nomeSecao", DbType.String, entidade.NomeSecao);
			_db.AddInParameter(command, "@revistaId", DbType.Int32, entidade.Revista.RevistaId);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um RevistaSecao da base de dados.
        /// </summary>
        /// <param name="entidade">RevistaSecao a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(RevistaSecao entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM RevistaSecao ");
			sbSQL.Append("WHERE revistaSecaoId=@revistaSecaoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@revistaSecaoId", DbType.Int32, entidade.RevistaSecaoId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um RevistaSecao.
		/// </summary>
        /// <param name="entidade">RevistaSecao a ser carregado (somente o identificador é necessário).</param>
		/// <returns>RevistaSecao</returns>
		public RevistaSecao Carregar(int revistaSecaoId) {		
			RevistaSecao entidade = new RevistaSecao();
			entidade.RevistaSecaoId = revistaSecaoId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um RevistaSecao.
		/// </summary>
        /// <param name="entidade">RevistaSecao a ser carregado (somente o identificador é necessário).</param>
		/// <returns>RevistaSecao</returns>
		public RevistaSecao Carregar(RevistaSecao entidade) {		
		
			RevistaSecao entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM RevistaSecao WHERE revistaSecaoId=@revistaSecaoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@revistaSecaoId", DbType.Int32, entidade.RevistaSecaoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new RevistaSecao();
				PopulaRevistaSecao(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de RevistaSecao.
        /// </summary>
        /// <param name="entidade">RevistaArtigo relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de RevistaSecao.</returns>
		public IEnumerable<RevistaSecao> Carregar(RevistaArtigo entidade)
		{		
			List<RevistaSecao> entidadesRetorno = new List<RevistaSecao>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT RevistaSecao.* FROM RevistaSecao INNER JOIN RevistaArtigo ON RevistaSecao.revistaSecaoId=RevistaArtigo.revistaSecaoId WHERE RevistaArtigo.revistaArtigoId=@revistaArtigoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@revistaArtigoId", DbType.Int32, entidade.RevistaArtigoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                RevistaSecao entidadeRetorno = new RevistaSecao();
                PopulaRevistaSecao(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de RevistaSecao.
        /// </summary>
        /// <param name="entidade">Revista relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de RevistaSecao.</returns>
		public IEnumerable<RevistaSecao> Carregar(Revista entidade)
		{		
			List<RevistaSecao> entidadesRetorno = new List<RevistaSecao>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT RevistaSecao.* FROM RevistaSecao WHERE RevistaSecao.revistaId=@revistaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@revistaId", DbType.Int32, entidade.RevistaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                RevistaSecao entidadeRetorno = new RevistaSecao();
                PopulaRevistaSecao(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de RevistaSecao.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos RevistaSecao.</returns>
		public IEnumerable<RevistaSecao> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<RevistaSecao> entidadesRetorno = new List<RevistaSecao>();
			
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
				sbOrder.Append( " ORDER BY revistaSecaoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM RevistaSecao");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM RevistaSecao WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM RevistaSecao ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT RevistaSecao.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM RevistaSecao ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT RevistaSecao.* FROM RevistaSecao ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                RevistaSecao entidadeRetorno = new RevistaSecao();
                PopulaRevistaSecao(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os RevistaSecao existentes na base de dados.
        /// </summary>
		public IEnumerable<RevistaSecao> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de RevistaSecao na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de RevistaSecao na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM RevistaSecao");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um RevistaSecao baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">RevistaSecao a ser populado(.</param>
		public static void PopulaRevistaSecao(IDataReader reader, RevistaSecao entidade) 
		{						
			if (reader["revistaSecaoId"] != DBNull.Value)
				entidade.RevistaSecaoId = Convert.ToInt32(reader["revistaSecaoId"].ToString());
			
			if (reader["nomeSecao"] != DBNull.Value)
				entidade.NomeSecao = reader["nomeSecao"].ToString();
			
			if (reader["revistaId"] != DBNull.Value) {
				entidade.Revista = new Revista();
				entidade.Revista.RevistaId = Convert.ToInt32(reader["revistaId"].ToString());
			}


		}		
		
	}
}
		