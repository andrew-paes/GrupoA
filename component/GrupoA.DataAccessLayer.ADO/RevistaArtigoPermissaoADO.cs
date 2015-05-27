
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
	public partial class RevistaArtigoPermissaoADO : ADOSuper, IRevistaArtigoPermissaoDAL {
	
	    /// <summary>
        /// Método que persiste um RevistaArtigoPermissao.
        /// </summary>
        /// <param name="entidade">RevistaArtigoPermissao contendo os dados a serem persistidos.</param>	
		public void Inserir(RevistaArtigoPermissao entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO RevistaArtigoPermissao ");
			sbSQL.Append(" (revistaArtigoPermissaoId, permissao) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@revistaArtigoPermissaoId, @permissao) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@revistaArtigoPermissaoId", DbType.Int32, entidade.RevistaArtigoPermissaoId);

			_db.AddInParameter(command, "@permissao", DbType.String, entidade.Permissao);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um RevistaArtigoPermissao.
        /// </summary>
        /// <param name="entidade">RevistaArtigoPermissao contendo os dados a serem atualizados.</param>
		public void Atualizar(RevistaArtigoPermissao entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE RevistaArtigoPermissao SET ");
			sbSQL.Append(" permissao=@permissao ");
			sbSQL.Append(" WHERE revistaArtigoPermissaoId=@revistaArtigoPermissaoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@revistaArtigoPermissaoId", DbType.Int32, entidade.RevistaArtigoPermissaoId);
			_db.AddInParameter(command, "@permissao", DbType.String, entidade.Permissao);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um RevistaArtigoPermissao da base de dados.
        /// </summary>
        /// <param name="entidade">RevistaArtigoPermissao a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(RevistaArtigoPermissao entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM RevistaArtigoPermissao ");
			sbSQL.Append("WHERE revistaArtigoPermissaoId=@revistaArtigoPermissaoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@revistaArtigoPermissaoId", DbType.Int32, entidade.RevistaArtigoPermissaoId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um RevistaArtigoPermissao.
		/// </summary>
        /// <param name="entidade">RevistaArtigoPermissao a ser carregado (somente o identificador é necessário).</param>
		/// <returns>RevistaArtigoPermissao</returns>
		public RevistaArtigoPermissao Carregar(int revistaArtigoPermissaoId) {		
			RevistaArtigoPermissao entidade = new RevistaArtigoPermissao();
			entidade.RevistaArtigoPermissaoId = revistaArtigoPermissaoId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um RevistaArtigoPermissao.
		/// </summary>
        /// <param name="entidade">RevistaArtigoPermissao a ser carregado (somente o identificador é necessário).</param>
		/// <returns>RevistaArtigoPermissao</returns>
		public RevistaArtigoPermissao Carregar(RevistaArtigoPermissao entidade) {		
		
			RevistaArtigoPermissao entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM RevistaArtigoPermissao WHERE revistaArtigoPermissaoId=@revistaArtigoPermissaoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@revistaArtigoPermissaoId", DbType.Int32, entidade.RevistaArtigoPermissaoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new RevistaArtigoPermissao();
				PopulaRevistaArtigoPermissao(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de RevistaArtigoPermissao.
        /// </summary>
        /// <param name="entidade">RevistaArtigo relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de RevistaArtigoPermissao.</returns>
		public IEnumerable<RevistaArtigoPermissao> Carregar(RevistaArtigo entidade)
		{		
			List<RevistaArtigoPermissao> entidadesRetorno = new List<RevistaArtigoPermissao>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT RevistaArtigoPermissao.* FROM RevistaArtigoPermissao INNER JOIN RevistaArtigo ON RevistaArtigoPermissao.revistaArtigoPermissaoId=RevistaArtigo.revistaArtigoPermissaoId WHERE RevistaArtigo.revistaArtigoId=@revistaArtigoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@revistaArtigoId", DbType.Int32, entidade.RevistaArtigoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                RevistaArtigoPermissao entidadeRetorno = new RevistaArtigoPermissao();
                PopulaRevistaArtigoPermissao(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de RevistaArtigoPermissao.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos RevistaArtigoPermissao.</returns>
		public IEnumerable<RevistaArtigoPermissao> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<RevistaArtigoPermissao> entidadesRetorno = new List<RevistaArtigoPermissao>();
			
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
				sbOrder.Append( " ORDER BY revistaArtigoPermissaoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM RevistaArtigoPermissao");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM RevistaArtigoPermissao WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM RevistaArtigoPermissao ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT RevistaArtigoPermissao.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM RevistaArtigoPermissao ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT RevistaArtigoPermissao.* FROM RevistaArtigoPermissao ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                RevistaArtigoPermissao entidadeRetorno = new RevistaArtigoPermissao();
                PopulaRevistaArtigoPermissao(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os RevistaArtigoPermissao existentes na base de dados.
        /// </summary>
		public IEnumerable<RevistaArtigoPermissao> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de RevistaArtigoPermissao na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de RevistaArtigoPermissao na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM RevistaArtigoPermissao");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um RevistaArtigoPermissao baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">RevistaArtigoPermissao a ser populado(.</param>
		public static void PopulaRevistaArtigoPermissao(IDataReader reader, RevistaArtigoPermissao entidade) 
		{						
			if (reader["revistaArtigoPermissaoId"] != DBNull.Value)
				entidade.RevistaArtigoPermissaoId = Convert.ToInt32(reader["revistaArtigoPermissaoId"].ToString());
			
			if (reader["permissao"] != DBNull.Value)
				entidade.Permissao = reader["permissao"].ToString();
			

		}		
		
	}
}
		