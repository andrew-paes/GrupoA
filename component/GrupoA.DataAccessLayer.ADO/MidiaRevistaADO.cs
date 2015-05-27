
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
	public partial class MidiaRevistaADO : ADOSuper, IMidiaRevistaDAL {
	
	    /// <summary>
        /// Método que persiste um MidiaRevista.
        /// </summary>
        /// <param name="entidade">MidiaRevista contendo os dados a serem persistidos.</param>	
		public void Inserir(MidiaRevista entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO MidiaRevista ");
			sbSQL.Append(" (midiaId, revistaId) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@midiaId, @revistaId) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			//_db.AddInParameter(command, "@midiaRevistaId", DbType.Int32, entidade.MidiaRevistaId);

			_db.AddInParameter(command, "@midiaId", DbType.Int32, entidade.Midia.MidiaId);

			_db.AddInParameter(command, "@revistaId", DbType.Int32, entidade.Revista.RevistaId);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um MidiaRevista.
        /// </summary>
        /// <param name="entidade">MidiaRevista contendo os dados a serem atualizados.</param>
		public void Atualizar(MidiaRevista entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE MidiaRevista SET ");
			sbSQL.Append(" midiaId=@midiaId, revistaId=@revistaId ");
			sbSQL.Append(" WHERE midiaRevistaId=@midiaRevistaId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@midiaRevistaId", DbType.Int32, entidade.MidiaRevistaId);
			_db.AddInParameter(command, "@midiaId", DbType.Int32, entidade.Midia.MidiaId);
			_db.AddInParameter(command, "@revistaId", DbType.Int32, entidade.Revista.RevistaId);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um MidiaRevista da base de dados.
        /// </summary>
        /// <param name="entidade">MidiaRevista a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(MidiaRevista entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM MidiaRevista ");
			sbSQL.Append("WHERE midiaRevistaId=@midiaRevistaId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@midiaRevistaId", DbType.Int32, entidade.MidiaRevistaId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um MidiaRevista.
		/// </summary>
        /// <param name="entidade">MidiaRevista a ser carregado (somente o identificador é necessário).</param>
		/// <returns>MidiaRevista</returns>
		public MidiaRevista Carregar(int midiaRevistaId) {		
			MidiaRevista entidade = new MidiaRevista();
			entidade.MidiaRevistaId = midiaRevistaId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um MidiaRevista.
		/// </summary>
        /// <param name="entidade">MidiaRevista a ser carregado (somente o identificador é necessário).</param>
		/// <returns>MidiaRevista</returns>
		public MidiaRevista Carregar(MidiaRevista entidade) {		
		
			MidiaRevista entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM MidiaRevista WHERE midiaRevistaId=@midiaRevistaId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@midiaRevistaId", DbType.Int32, entidade.MidiaRevistaId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new MidiaRevista();
				PopulaMidiaRevista(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de MidiaRevista.
        /// </summary>
        /// <param name="entidade">Midia relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de MidiaRevista.</returns>
		public IEnumerable<MidiaRevista> Carregar(Midia entidade)
		{		
			List<MidiaRevista> entidadesRetorno = new List<MidiaRevista>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT MidiaRevista.* FROM MidiaRevista WHERE MidiaRevista.midiaId=@midiaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@midiaId", DbType.Int32, entidade.MidiaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                MidiaRevista entidadeRetorno = new MidiaRevista();
                PopulaMidiaRevista(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de MidiaRevista.
        /// </summary>
        /// <param name="entidade">Revista relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de MidiaRevista.</returns>
		public IEnumerable<MidiaRevista> Carregar(Revista entidade)
		{		
			List<MidiaRevista> entidadesRetorno = new List<MidiaRevista>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT MidiaRevista.* FROM MidiaRevista WHERE MidiaRevista.revistaId=@revistaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@revistaId", DbType.Int32, entidade.RevistaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                MidiaRevista entidadeRetorno = new MidiaRevista();
                PopulaMidiaRevista(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de MidiaRevista.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos MidiaRevista.</returns>
		public IEnumerable<MidiaRevista> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<MidiaRevista> entidadesRetorno = new List<MidiaRevista>();
			
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
				sbOrder.Append( " ORDER BY midiaRevistaId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM MidiaRevista");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM MidiaRevista WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM MidiaRevista ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT MidiaRevista.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM MidiaRevista ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT MidiaRevista.* FROM MidiaRevista ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                MidiaRevista entidadeRetorno = new MidiaRevista();
                PopulaMidiaRevista(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os MidiaRevista existentes na base de dados.
        /// </summary>
		public IEnumerable<MidiaRevista> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de MidiaRevista na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de MidiaRevista na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM MidiaRevista");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um MidiaRevista baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">MidiaRevista a ser populado(.</param>
		public static void PopulaMidiaRevista(IDataReader reader, MidiaRevista entidade) 
		{						
			if (reader["midiaRevistaId"] != DBNull.Value)
				entidade.MidiaRevistaId = Convert.ToInt32(reader["midiaRevistaId"].ToString());
			
			if (reader["midiaId"] != DBNull.Value) {
				entidade.Midia = new Midia();
				entidade.Midia.MidiaId = Convert.ToInt32(reader["midiaId"].ToString());
			}

			if (reader["revistaId"] != DBNull.Value) {
				entidade.Revista = new Revista();
				entidade.Revista.RevistaId = Convert.ToInt32(reader["revistaId"].ToString());
			}


		}		
		
	}
}
		