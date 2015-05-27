
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
	public partial class FaqCategoriaADO : ADOSuper, IFaqCategoriaDAL {
	
	    /// <summary>
        /// Método que persiste um FaqCategoria.
        /// </summary>
        /// <param name="entidade">FaqCategoria contendo os dados a serem persistidos.</param>	
		public void Inserir(FaqCategoria entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO FaqCategoria ");
			sbSQL.Append(" (nomeCategoria, ordemCategoria) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@nomeCategoria, @ordemCategoria) ");											

			sbSQL.Append(" ; SET @faqCategoriaId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@faqCategoriaId", DbType.Int32, 8);

			_db.AddInParameter(command, "@nomeCategoria", DbType.String, entidade.NomeCategoria);

			_db.AddInParameter(command, "@ordemCategoria", DbType.Int32, entidade.OrdemCategoria);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.FaqCategoriaId = Convert.ToInt32(_db.GetParameterValue(command, "@faqCategoriaId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um FaqCategoria.
        /// </summary>
        /// <param name="entidade">FaqCategoria contendo os dados a serem atualizados.</param>
		public void Atualizar(FaqCategoria entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE FaqCategoria SET ");
			sbSQL.Append(" nomeCategoria=@nomeCategoria, ordemCategoria=@ordemCategoria ");
			sbSQL.Append(" WHERE faqCategoriaId=@faqCategoriaId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@faqCategoriaId", DbType.Int32, entidade.FaqCategoriaId);
			_db.AddInParameter(command, "@nomeCategoria", DbType.String, entidade.NomeCategoria);
			_db.AddInParameter(command, "@ordemCategoria", DbType.Int32, entidade.OrdemCategoria);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um FaqCategoria da base de dados.
        /// </summary>
        /// <param name="entidade">FaqCategoria a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(FaqCategoria entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM FaqCategoria ");
			sbSQL.Append("WHERE faqCategoriaId=@faqCategoriaId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@faqCategoriaId", DbType.Int32, entidade.FaqCategoriaId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um FaqCategoria.
		/// </summary>
        /// <param name="entidade">FaqCategoria a ser carregado (somente o identificador é necessário).</param>
		/// <returns>FaqCategoria</returns>
		public FaqCategoria Carregar(int faqCategoriaId) {		
			FaqCategoria entidade = new FaqCategoria();
			entidade.FaqCategoriaId = faqCategoriaId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um FaqCategoria.
		/// </summary>
        /// <param name="entidade">FaqCategoria a ser carregado (somente o identificador é necessário).</param>
		/// <returns>FaqCategoria</returns>
		public FaqCategoria Carregar(FaqCategoria entidade) {		
		
			FaqCategoria entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM FaqCategoria WHERE faqCategoriaId=@faqCategoriaId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@faqCategoriaId", DbType.Int32, entidade.FaqCategoriaId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new FaqCategoria();
				PopulaFaqCategoria(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de FaqCategoria.
        /// </summary>
        /// <param name="entidade">FaqItem relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de FaqCategoria.</returns>
		public IEnumerable<FaqCategoria> Carregar(FaqItem entidade)
		{		
			List<FaqCategoria> entidadesRetorno = new List<FaqCategoria>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT FaqCategoria.* FROM FaqCategoria INNER JOIN FaqItem ON FaqCategoria.faqCategoriaId=FaqItem.faqCategoriaId WHERE FaqItem.faqItemId=@faqItemId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@faqItemId", DbType.Int32, entidade.FaqItemId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                FaqCategoria entidadeRetorno = new FaqCategoria();
                PopulaFaqCategoria(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de FaqCategoria.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos FaqCategoria.</returns>
		public IEnumerable<FaqCategoria> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<FaqCategoria> entidadesRetorno = new List<FaqCategoria>();
			
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
				sbOrder.Append( " ORDER BY faqCategoriaId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM FaqCategoria");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM FaqCategoria WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM FaqCategoria ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT FaqCategoria.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM FaqCategoria ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT FaqCategoria.* FROM FaqCategoria ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                FaqCategoria entidadeRetorno = new FaqCategoria();
                PopulaFaqCategoria(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os FaqCategoria existentes na base de dados.
        /// </summary>
		public IEnumerable<FaqCategoria> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de FaqCategoria na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de FaqCategoria na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM FaqCategoria");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um FaqCategoria baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">FaqCategoria a ser populado(.</param>
		public static void PopulaFaqCategoria(IDataReader reader, FaqCategoria entidade) 
		{						
			if (reader["faqCategoriaId"] != DBNull.Value)
				entidade.FaqCategoriaId = Convert.ToInt32(reader["faqCategoriaId"].ToString());
			
			if (reader["nomeCategoria"] != DBNull.Value)
				entidade.NomeCategoria = reader["nomeCategoria"].ToString();
			
			if (reader["ordemCategoria"] != DBNull.Value)
				entidade.OrdemCategoria = Convert.ToInt32(reader["ordemCategoria"].ToString());
			

		}		
		
	}
}
		