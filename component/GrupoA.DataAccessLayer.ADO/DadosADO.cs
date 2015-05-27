
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
	public partial class DadosADO : ADOSuper, IDadosDAL {
	
	    /// <summary>
        /// Método que persiste um Dados.
        /// </summary>
        /// <param name="entidade">Dados contendo os dados a serem persistidos.</param>	
		public void Inserir(Dados entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO Dados ");
			sbSQL.Append(" (ProdutoId, CategoriaId, SeloId, Lancamento) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@ProdutoId, @CategoriaId, @SeloId, @Lancamento) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			if (entidade.ProdutoId != null ) 
				_db.AddInParameter(command, "@ProdutoId", DbType.Int32, entidade.ProdutoId);
			else
				_db.AddInParameter(command, "@ProdutoId", DbType.Int32, null);

			if (entidade.CategoriaId != null ) 
				_db.AddInParameter(command, "@CategoriaId", DbType.Int32, entidade.CategoriaId);
			else
				_db.AddInParameter(command, "@CategoriaId", DbType.Int32, null);

			if (entidade.SeloId != null ) 
				_db.AddInParameter(command, "@SeloId", DbType.Int32, entidade.SeloId);
			else
				_db.AddInParameter(command, "@SeloId", DbType.Int32, null);

			if (entidade.Lancamento != null ) 
				_db.AddInParameter(command, "@Lancamento", DbType.Int32, entidade.Lancamento);
			else
				_db.AddInParameter(command, "@Lancamento", DbType.Int32, null);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um Dados.
        /// </summary>
        /// <param name="entidade">Dados contendo os dados a serem atualizados.</param>
		public void Atualizar(Dados entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE Dados SET ");
			sbSQL.Append(" ProdutoId=@ProdutoId, CategoriaId=@CategoriaId, SeloId=@SeloId, Lancamento=@Lancamento ");
			sbSQL.Append(" WHERE  ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			if (entidade.ProdutoId != null ) 
				_db.AddInParameter(command, "@ProdutoId", DbType.Int32, entidade.ProdutoId);
			else
				_db.AddInParameter(command, "@ProdutoId", DbType.Int32, null);
			if (entidade.CategoriaId != null ) 
				_db.AddInParameter(command, "@CategoriaId", DbType.Int32, entidade.CategoriaId);
			else
				_db.AddInParameter(command, "@CategoriaId", DbType.Int32, null);
			if (entidade.SeloId != null ) 
				_db.AddInParameter(command, "@SeloId", DbType.Int32, entidade.SeloId);
			else
				_db.AddInParameter(command, "@SeloId", DbType.Int32, null);
			if (entidade.Lancamento != null ) 
				_db.AddInParameter(command, "@Lancamento", DbType.Int32, entidade.Lancamento);
			else
				_db.AddInParameter(command, "@Lancamento", DbType.Int32, null);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um Dados da base de dados.
        /// </summary>
        /// <param name="entidade">Dados a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(Dados entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM Dados ");
			sbSQL.Append("WHERE  ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			

								
			_db.ExecuteNonQuery(command);
		}
		

		/// <summary>
		/// Método que carrega um Dados.
		/// </summary>
        /// <param name="entidade">Dados a ser carregado (somente o identificador é necessário).</param>
		/// <returns>Dados</returns>
		public Dados Carregar(Dados entidade) {		
		
			Dados entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM Dados WHERE ");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new Dados();
				PopulaDados(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		

		
		
		/// <summary>
        /// Método que retorna uma coleção de Dados.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos Dados.</returns>
		public IEnumerable<Dados> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<Dados> entidadesRetorno = new List<Dados>();
			
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
				sbOrder.Append( " ORDER BY " );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM Dados");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Dados WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Dados ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT Dados.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM Dados ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT Dados.* FROM Dados ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Dados entidadeRetorno = new Dados();
                PopulaDados(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os Dados existentes na base de dados.
        /// </summary>
		public IEnumerable<Dados> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de Dados na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de Dados na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM Dados");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um Dados baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Dados a ser populado(.</param>
		public static void PopulaDados(IDataReader reader, Dados entidade) 
		{						
			if (reader["ProdutoId"] != DBNull.Value)
				entidade.ProdutoId = Convert.ToInt32(reader["ProdutoId"].ToString());
			
			if (reader["CategoriaId"] != DBNull.Value)
				entidade.CategoriaId = Convert.ToInt32(reader["CategoriaId"].ToString());
			
			if (reader["SeloId"] != DBNull.Value)
				entidade.SeloId = Convert.ToInt32(reader["SeloId"].ToString());
			
			if (reader["Lancamento"] != DBNull.Value)
				entidade.Lancamento = Convert.ToInt32(reader["Lancamento"].ToString());
			

		}		
		
	}
}
		