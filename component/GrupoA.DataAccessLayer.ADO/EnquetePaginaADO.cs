
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
	public partial class EnquetePaginaADO : ADOSuper, IEnquetePaginaDAL {
	
	    /// <summary>
        /// Método que persiste um EnquetePagina.
        /// </summary>
        /// <param name="entidade">EnquetePagina contendo os dados a serem persistidos.</param>	
		public void Inserir(EnquetePagina entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO EnquetePagina ");
			sbSQL.Append(" (enquetePaginaId, nomePagina) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@enquetePaginaId, @nomePagina) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@enquetePaginaId", DbType.Int32, entidade.EnquetePaginaId);

			_db.AddInParameter(command, "@nomePagina", DbType.String, entidade.NomePagina);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um EnquetePagina.
        /// </summary>
        /// <param name="entidade">EnquetePagina contendo os dados a serem atualizados.</param>
		public void Atualizar(EnquetePagina entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE EnquetePagina SET ");
			sbSQL.Append(" nomePagina=@nomePagina ");
			sbSQL.Append(" WHERE enquetePaginaId=@enquetePaginaId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@enquetePaginaId", DbType.Int32, entidade.EnquetePaginaId);
			_db.AddInParameter(command, "@nomePagina", DbType.String, entidade.NomePagina);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um EnquetePagina da base de dados.
        /// </summary>
        /// <param name="entidade">EnquetePagina a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(EnquetePagina entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM EnquetePagina ");
			sbSQL.Append("WHERE enquetePaginaId=@enquetePaginaId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@enquetePaginaId", DbType.Int32, entidade.EnquetePaginaId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um EnquetePagina.
		/// </summary>
        /// <param name="entidade">EnquetePagina a ser carregado (somente o identificador é necessário).</param>
		/// <returns>EnquetePagina</returns>
		public EnquetePagina Carregar(int enquetePaginaId) {		
			EnquetePagina entidade = new EnquetePagina();
			entidade.EnquetePaginaId = enquetePaginaId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um EnquetePagina.
		/// </summary>
        /// <param name="entidade">EnquetePagina a ser carregado (somente o identificador é necessário).</param>
		/// <returns>EnquetePagina</returns>
		public EnquetePagina Carregar(EnquetePagina entidade) {		
		
			EnquetePagina entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM EnquetePagina WHERE enquetePaginaId=@enquetePaginaId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@enquetePaginaId", DbType.Int32, entidade.EnquetePaginaId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new EnquetePagina();
				PopulaEnquetePagina(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de EnquetePagina.
        /// </summary>
        /// <param name="entidade">Enquete relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de EnquetePagina.</returns>
		public IEnumerable<EnquetePagina> Carregar(Enquete entidade)
		{		
			List<EnquetePagina> entidadesRetorno = new List<EnquetePagina>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT EnquetePagina.* FROM EnquetePagina INNER JOIN EnqueteLocalizacao ON EnquetePagina.enquetePaginaId=EnqueteLocalizacao.enquetePaginaId WHERE EnqueteLocalizacao.enqueteId=@enqueteId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@enqueteId", DbType.Int32, entidade.EnqueteId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                EnquetePagina entidadeRetorno = new EnquetePagina();
                PopulaEnquetePagina(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de EnquetePagina.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos EnquetePagina.</returns>
		public IEnumerable<EnquetePagina> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<EnquetePagina> entidadesRetorno = new List<EnquetePagina>();
			
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
				sbOrder.Append( " ORDER BY enquetePaginaId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM EnquetePagina");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM EnquetePagina WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM EnquetePagina ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT EnquetePagina.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM EnquetePagina ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT EnquetePagina.* FROM EnquetePagina ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                EnquetePagina entidadeRetorno = new EnquetePagina();
                PopulaEnquetePagina(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os EnquetePagina existentes na base de dados.
        /// </summary>
		public IEnumerable<EnquetePagina> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de EnquetePagina na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de EnquetePagina na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM EnquetePagina");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um EnquetePagina baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">EnquetePagina a ser populado(.</param>
		public static void PopulaEnquetePagina(IDataReader reader, EnquetePagina entidade) 
		{						
			if (reader["enquetePaginaId"] != DBNull.Value)
				entidade.EnquetePaginaId = Convert.ToInt32(reader["enquetePaginaId"].ToString());
			
			if (reader["nomePagina"] != DBNull.Value)
				entidade.NomePagina = reader["nomePagina"].ToString();
			

		}		
		
	}
}
		