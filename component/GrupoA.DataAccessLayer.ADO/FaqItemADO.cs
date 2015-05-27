
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
	public partial class FaqItemADO : ADOSuper, IFaqItemDAL {
	
	    /// <summary>
        /// Método que persiste um FaqItem.
        /// </summary>
        /// <param name="entidade">FaqItem contendo os dados a serem persistidos.</param>	
		public void Inserir(FaqItem entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO FaqItem ");
			sbSQL.Append(" (pergunta, resposta, ativo, ordemItem, faqCategoriaId) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@pergunta, @resposta, @ativo, @ordemItem, @faqCategoriaId) ");											

			sbSQL.Append(" ; SET @faqItemId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@faqItemId", DbType.Int32, 8);

			_db.AddInParameter(command, "@pergunta", DbType.String, entidade.Pergunta);

			_db.AddInParameter(command, "@resposta", DbType.String, entidade.Resposta);

			_db.AddInParameter(command, "@ativo", DbType.Int32, entidade.Ativo);

			_db.AddInParameter(command, "@ordemItem", DbType.Int32, entidade.OrdemItem);

			_db.AddInParameter(command, "@faqCategoriaId", DbType.Int32, entidade.FaqCategoria.FaqCategoriaId);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.FaqItemId = Convert.ToInt32(_db.GetParameterValue(command, "@faqItemId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um FaqItem.
        /// </summary>
        /// <param name="entidade">FaqItem contendo os dados a serem atualizados.</param>
		public void Atualizar(FaqItem entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE FaqItem SET ");
			sbSQL.Append(" pergunta=@pergunta, resposta=@resposta, ativo=@ativo, ordemItem=@ordemItem, faqCategoriaId=@faqCategoriaId ");
			sbSQL.Append(" WHERE faqItemId=@faqItemId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@faqItemId", DbType.Int32, entidade.FaqItemId);
			_db.AddInParameter(command, "@pergunta", DbType.String, entidade.Pergunta);
			_db.AddInParameter(command, "@resposta", DbType.String, entidade.Resposta);
			_db.AddInParameter(command, "@ativo", DbType.Int32, entidade.Ativo);
			_db.AddInParameter(command, "@ordemItem", DbType.Int32, entidade.OrdemItem);
			_db.AddInParameter(command, "@faqCategoriaId", DbType.Int32, entidade.FaqCategoria.FaqCategoriaId);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um FaqItem da base de dados.
        /// </summary>
        /// <param name="entidade">FaqItem a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(FaqItem entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM FaqItem ");
			sbSQL.Append("WHERE faqItemId=@faqItemId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@faqItemId", DbType.Int32, entidade.FaqItemId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um FaqItem.
		/// </summary>
        /// <param name="entidade">FaqItem a ser carregado (somente o identificador é necessário).</param>
		/// <returns>FaqItem</returns>
		public FaqItem Carregar(int faqItemId) {		
			FaqItem entidade = new FaqItem();
			entidade.FaqItemId = faqItemId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um FaqItem.
		/// </summary>
        /// <param name="entidade">FaqItem a ser carregado (somente o identificador é necessário).</param>
		/// <returns>FaqItem</returns>
		public FaqItem Carregar(FaqItem entidade) {		
		
			FaqItem entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM FaqItem WHERE faqItemId=@faqItemId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@faqItemId", DbType.Int32, entidade.FaqItemId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new FaqItem();
				PopulaFaqItem(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de FaqItem.
        /// </summary>
        /// <param name="entidade">FaqCategoria relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de FaqItem.</returns>
		public IEnumerable<FaqItem> Carregar(FaqCategoria entidade)
		{		
			List<FaqItem> entidadesRetorno = new List<FaqItem>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT FaqItem.* FROM FaqItem WHERE FaqItem.faqCategoriaId=@faqCategoriaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@faqCategoriaId", DbType.Int32, entidade.FaqCategoriaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                FaqItem entidadeRetorno = new FaqItem();
                PopulaFaqItem(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de FaqItem.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos FaqItem.</returns>
		public IEnumerable<FaqItem> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<FaqItem> entidadesRetorno = new List<FaqItem>();
			
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
				sbOrder.Append( " ORDER BY faqItemId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM FaqItem");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM FaqItem WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM FaqItem ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT FaqItem.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM FaqItem ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT FaqItem.* FROM FaqItem ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                FaqItem entidadeRetorno = new FaqItem();
                PopulaFaqItem(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os FaqItem existentes na base de dados.
        /// </summary>
		public IEnumerable<FaqItem> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de FaqItem na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de FaqItem na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM FaqItem");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um FaqItem baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">FaqItem a ser populado(.</param>
		public static void PopulaFaqItem(IDataReader reader, FaqItem entidade) 
		{						
			if (reader["faqItemId"] != DBNull.Value)
				entidade.FaqItemId = Convert.ToInt32(reader["faqItemId"].ToString());
			
			if (reader["pergunta"] != DBNull.Value)
				entidade.Pergunta = reader["pergunta"].ToString();
			
			if (reader["resposta"] != DBNull.Value)
				entidade.Resposta = reader["resposta"].ToString();
			
			if (reader["ativo"] != DBNull.Value)
				entidade.Ativo = Convert.ToBoolean(reader["ativo"].ToString());
			
			if (reader["ordemItem"] != DBNull.Value)
				entidade.OrdemItem = Convert.ToInt32(reader["ordemItem"].ToString());
			
			if (reader["faqCategoriaId"] != DBNull.Value) {
				entidade.FaqCategoria = new FaqCategoria();
				entidade.FaqCategoria.FaqCategoriaId = Convert.ToInt32(reader["faqCategoriaId"].ToString());
			}


		}		
		
	}
}
		