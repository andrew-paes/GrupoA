
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
	public partial class EnqueteOpcaoADO : ADOSuper, IEnqueteOpcaoDAL {
	
	    /// <summary>
        /// Método que persiste um EnqueteOpcao.
        /// </summary>
        /// <param name="entidade">EnqueteOpcao contendo os dados a serem persistidos.</param>	
		public void Inserir(EnqueteOpcao entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO EnqueteOpcao ");
			sbSQL.Append(" (enqueteId, descricao, contador) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@enqueteId, @descricao, @contador) ");											

			sbSQL.Append(" ; SET @enqueteOpcaoId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@enqueteOpcaoId", DbType.Int32, 8);

			_db.AddInParameter(command, "@enqueteId", DbType.Int32, entidade.Enquete.EnqueteId);

			_db.AddInParameter(command, "@descricao", DbType.String, entidade.Descricao);

			_db.AddInParameter(command, "@contador", DbType.Int32, entidade.Contador);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.EnqueteOpcaoId = Convert.ToInt32(_db.GetParameterValue(command, "@enqueteOpcaoId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um EnqueteOpcao.
        /// </summary>
        /// <param name="entidade">EnqueteOpcao contendo os dados a serem atualizados.</param>
		public void Atualizar(EnqueteOpcao entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE EnqueteOpcao SET ");
			sbSQL.Append(" enqueteId=@enqueteId, descricao=@descricao, contador=@contador ");
			sbSQL.Append(" WHERE enqueteOpcaoId=@enqueteOpcaoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@enqueteOpcaoId", DbType.Int32, entidade.EnqueteOpcaoId);
			_db.AddInParameter(command, "@enqueteId", DbType.Int32, entidade.Enquete.EnqueteId);
			_db.AddInParameter(command, "@descricao", DbType.String, entidade.Descricao);
			_db.AddInParameter(command, "@contador", DbType.Int32, entidade.Contador);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um EnqueteOpcao da base de dados.
        /// </summary>
        /// <param name="entidade">EnqueteOpcao a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(EnqueteOpcao entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM EnqueteOpcao ");
			sbSQL.Append("WHERE enqueteOpcaoId=@enqueteOpcaoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@enqueteOpcaoId", DbType.Int32, entidade.EnqueteOpcaoId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um EnqueteOpcao.
		/// </summary>
        /// <param name="entidade">EnqueteOpcao a ser carregado (somente o identificador é necessário).</param>
		/// <returns>EnqueteOpcao</returns>
		public EnqueteOpcao Carregar(int enqueteOpcaoId) {		
			EnqueteOpcao entidade = new EnqueteOpcao();
			entidade.EnqueteOpcaoId = enqueteOpcaoId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um EnqueteOpcao.
		/// </summary>
        /// <param name="entidade">EnqueteOpcao a ser carregado (somente o identificador é necessário).</param>
		/// <returns>EnqueteOpcao</returns>
		public EnqueteOpcao Carregar(EnqueteOpcao entidade) {		
		
			EnqueteOpcao entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM EnqueteOpcao WHERE enqueteOpcaoId=@enqueteOpcaoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@enqueteOpcaoId", DbType.Int32, entidade.EnqueteOpcaoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new EnqueteOpcao();
				PopulaEnqueteOpcao(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de EnqueteOpcao.
        /// </summary>
        /// <param name="entidade">Enquete relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de EnqueteOpcao.</returns>
		public IEnumerable<EnqueteOpcao> Carregar(Enquete entidade)
		{		
			List<EnqueteOpcao> entidadesRetorno = new List<EnqueteOpcao>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT EnqueteOpcao.* FROM EnqueteOpcao WHERE EnqueteOpcao.enqueteId=@enqueteId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@enqueteId", DbType.Int32, entidade.EnqueteId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                EnqueteOpcao entidadeRetorno = new EnqueteOpcao();
                PopulaEnqueteOpcao(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de EnqueteOpcao.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos EnqueteOpcao.</returns>
		public IEnumerable<EnqueteOpcao> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<EnqueteOpcao> entidadesRetorno = new List<EnqueteOpcao>();
			
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
				sbOrder.Append( " ORDER BY enqueteOpcaoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM EnqueteOpcao");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM EnqueteOpcao WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM EnqueteOpcao ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT EnqueteOpcao.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM EnqueteOpcao ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT EnqueteOpcao.* FROM EnqueteOpcao ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                EnqueteOpcao entidadeRetorno = new EnqueteOpcao();
                PopulaEnqueteOpcao(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os EnqueteOpcao existentes na base de dados.
        /// </summary>
		public IEnumerable<EnqueteOpcao> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de EnqueteOpcao na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de EnqueteOpcao na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM EnqueteOpcao");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um EnqueteOpcao baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">EnqueteOpcao a ser populado(.</param>
		public static void PopulaEnqueteOpcao(IDataReader reader, EnqueteOpcao entidade) 
		{						
			if (reader["enqueteOpcaoId"] != DBNull.Value)
				entidade.EnqueteOpcaoId = Convert.ToInt32(reader["enqueteOpcaoId"].ToString());
			
			if (reader["descricao"] != DBNull.Value)
				entidade.Descricao = reader["descricao"].ToString();
			
			if (reader["contador"] != DBNull.Value)
				entidade.Contador = Convert.ToInt32(reader["contador"].ToString());
			
			if (reader["enqueteId"] != DBNull.Value) {
				entidade.Enquete = new Enquete();
				entidade.Enquete.EnqueteId = Convert.ToInt32(reader["enqueteId"].ToString());
			}


		}		
		
	}
}
		