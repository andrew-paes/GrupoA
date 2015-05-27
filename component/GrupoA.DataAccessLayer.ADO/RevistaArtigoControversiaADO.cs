
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
	public partial class RevistaArtigoControversiaADO : ADOSuper, IRevistaArtigoControversiaDAL {
	
	    /// <summary>
        /// Método que persiste um RevistaArtigoControversia.
        /// </summary>
        /// <param name="entidade">RevistaArtigoControversia contendo os dados a serem persistidos.</param>	
		public void Inserir(RevistaArtigoControversia entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO RevistaArtigoControversia ");
			sbSQL.Append(" (revistaArtigoId, posicionamento, tituloControversia, textoControversia, autores) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@revistaArtigoId, @posicionamento, @tituloControversia, @textoControversia, @autores) ");											

			sbSQL.Append(" ; SET @revistaArtigoControversiaId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@revistaArtigoControversiaId", DbType.Int32, 8);

			_db.AddInParameter(command, "@revistaArtigoId", DbType.Int32, entidade.RevistaArtigoId);

			_db.AddInParameter(command, "@posicionamento", DbType.Int32, entidade.Posicionamento);

			if (entidade.TituloControversia != null ) 
				_db.AddInParameter(command, "@tituloControversia", DbType.String, entidade.TituloControversia);
			else
				_db.AddInParameter(command, "@tituloControversia", DbType.String, null);

			_db.AddInParameter(command, "@textoControversia", DbType.String, entidade.TextoControversia);

			if (entidade.Autores != null ) 
				_db.AddInParameter(command, "@autores", DbType.String, entidade.Autores);
			else
				_db.AddInParameter(command, "@autores", DbType.String, null);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.RevistaArtigoControversiaId = Convert.ToInt32(_db.GetParameterValue(command, "@revistaArtigoControversiaId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um RevistaArtigoControversia.
        /// </summary>
        /// <param name="entidade">RevistaArtigoControversia contendo os dados a serem atualizados.</param>
		public void Atualizar(RevistaArtigoControversia entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE RevistaArtigoControversia SET ");
			sbSQL.Append(" revistaArtigoId=@revistaArtigoId, posicionamento=@posicionamento, tituloControversia=@tituloControversia, textoControversia=@textoControversia, autores=@autores ");
			sbSQL.Append(" WHERE revistaArtigoControversiaId=@revistaArtigoControversiaId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@revistaArtigoControversiaId", DbType.Int32, entidade.RevistaArtigoControversiaId);
			_db.AddInParameter(command, "@revistaArtigoId", DbType.Int32, entidade.RevistaArtigoId);
			_db.AddInParameter(command, "@posicionamento", DbType.Int32, entidade.Posicionamento);
			if (entidade.TituloControversia != null ) 
				_db.AddInParameter(command, "@tituloControversia", DbType.String, entidade.TituloControversia);
			else
				_db.AddInParameter(command, "@tituloControversia", DbType.String, null);
			_db.AddInParameter(command, "@textoControversia", DbType.String, entidade.TextoControversia);
			if (entidade.Autores != null ) 
				_db.AddInParameter(command, "@autores", DbType.String, entidade.Autores);
			else
				_db.AddInParameter(command, "@autores", DbType.String, null);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um RevistaArtigoControversia da base de dados.
        /// </summary>
        /// <param name="entidade">RevistaArtigoControversia a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(RevistaArtigoControversia entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM RevistaArtigoControversia ");
			sbSQL.Append("WHERE revistaArtigoControversiaId=@revistaArtigoControversiaId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@revistaArtigoControversiaId", DbType.Int32, entidade.RevistaArtigoControversiaId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um RevistaArtigoControversia.
		/// </summary>
        /// <param name="entidade">RevistaArtigoControversia a ser carregado (somente o identificador é necessário).</param>
		/// <returns>RevistaArtigoControversia</returns>
		public RevistaArtigoControversia Carregar(int revistaArtigoControversiaId) {		
			RevistaArtigoControversia entidade = new RevistaArtigoControversia();
			entidade.RevistaArtigoControversiaId = revistaArtigoControversiaId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um RevistaArtigoControversia.
		/// </summary>
        /// <param name="entidade">RevistaArtigoControversia a ser carregado (somente o identificador é necessário).</param>
		/// <returns>RevistaArtigoControversia</returns>
		public RevistaArtigoControversia Carregar(RevistaArtigoControversia entidade) {		
		
			RevistaArtigoControversia entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM RevistaArtigoControversia WHERE revistaArtigoControversiaId=@revistaArtigoControversiaId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@revistaArtigoControversiaId", DbType.Int32, entidade.RevistaArtigoControversiaId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new RevistaArtigoControversia();
				PopulaRevistaArtigoControversia(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		

		
		
		/// <summary>
        /// Método que retorna uma coleção de RevistaArtigoControversia.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos RevistaArtigoControversia.</returns>
		public IEnumerable<RevistaArtigoControversia> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<RevistaArtigoControversia> entidadesRetorno = new List<RevistaArtigoControversia>();
			
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
				sbOrder.Append( " ORDER BY revistaArtigoControversiaId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM RevistaArtigoControversia");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM RevistaArtigoControversia WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM RevistaArtigoControversia ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT RevistaArtigoControversia.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM RevistaArtigoControversia ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT RevistaArtigoControversia.* FROM RevistaArtigoControversia ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                RevistaArtigoControversia entidadeRetorno = new RevistaArtigoControversia();
                PopulaRevistaArtigoControversia(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os RevistaArtigoControversia existentes na base de dados.
        /// </summary>
		public IEnumerable<RevistaArtigoControversia> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de RevistaArtigoControversia na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de RevistaArtigoControversia na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM RevistaArtigoControversia");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um RevistaArtigoControversia baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">RevistaArtigoControversia a ser populado(.</param>
		public static void PopulaRevistaArtigoControversia(IDataReader reader, RevistaArtigoControversia entidade) 
		{						
			if (reader["revistaArtigoControversiaId"] != DBNull.Value)
				entidade.RevistaArtigoControversiaId = Convert.ToInt32(reader["revistaArtigoControversiaId"].ToString());
			
			if (reader["revistaArtigoId"] != DBNull.Value)
				entidade.RevistaArtigoId = Convert.ToInt32(reader["revistaArtigoId"].ToString());
			
			if (reader["posicionamento"] != DBNull.Value)
				entidade.Posicionamento = Convert.ToInt32(reader["posicionamento"].ToString());
			
			if (reader["tituloControversia"] != DBNull.Value)
				entidade.TituloControversia = reader["tituloControversia"].ToString();
			
			if (reader["textoControversia"] != DBNull.Value)
				entidade.TextoControversia = reader["textoControversia"].ToString();
			
			if (reader["autores"] != DBNull.Value)
				entidade.Autores = reader["autores"].ToString();
			

		}		
		
	}
}
		