
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
	public partial class ConfiguracaoADO : ADOSuper, IConfiguracaoDAL {
	
	    /// <summary>
        /// Método que persiste um Configuracao.
        /// </summary>
        /// <param name="entidade">Configuracao contendo os dados a serem persistidos.</param>	
		public void Inserir(Configuracao entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO Configuracao ");
			sbSQL.Append(" (configuracaoId, chave, descricaoConfiguracao) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@configuracaoId, @chave, @descricaoConfiguracao) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@configuracaoId", DbType.Int32, entidade.ConfiguracaoId);

			_db.AddInParameter(command, "@chave", DbType.String, entidade.Chave);

			_db.AddInParameter(command, "@descricaoConfiguracao", DbType.String, entidade.DescricaoConfiguracao);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um Configuracao.
        /// </summary>
        /// <param name="entidade">Configuracao contendo os dados a serem atualizados.</param>
		public void Atualizar(Configuracao entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE Configuracao SET ");
			sbSQL.Append(" chave=@chave, descricaoConfiguracao=@descricaoConfiguracao ");
			sbSQL.Append(" WHERE configuracaoId=@configuracaoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@configuracaoId", DbType.Int32, entidade.ConfiguracaoId);
			_db.AddInParameter(command, "@chave", DbType.String, entidade.Chave);
			_db.AddInParameter(command, "@descricaoConfiguracao", DbType.String, entidade.DescricaoConfiguracao);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um Configuracao da base de dados.
        /// </summary>
        /// <param name="entidade">Configuracao a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(Configuracao entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM Configuracao ");
			sbSQL.Append("WHERE configuracaoId=@configuracaoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@configuracaoId", DbType.Int32, entidade.ConfiguracaoId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um Configuracao.
		/// </summary>
        /// <param name="entidade">Configuracao a ser carregado (somente o identificador é necessário).</param>
		/// <returns>Configuracao</returns>
		public Configuracao Carregar(int configuracaoId) {		
			Configuracao entidade = new Configuracao();
			entidade.ConfiguracaoId = configuracaoId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um Configuracao.
		/// </summary>
        /// <param name="entidade">Configuracao a ser carregado (somente o identificador é necessário).</param>
		/// <returns>Configuracao</returns>
		public Configuracao Carregar(Configuracao entidade) {		
		
			Configuracao entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM Configuracao WHERE configuracaoId=@configuracaoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@configuracaoId", DbType.Int32, entidade.ConfiguracaoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new Configuracao();
				PopulaConfiguracao(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		

		
		
		/// <summary>
        /// Método que retorna uma coleção de Configuracao.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos Configuracao.</returns>
		public IEnumerable<Configuracao> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<Configuracao> entidadesRetorno = new List<Configuracao>();
			
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
				sbOrder.Append( " ORDER BY configuracaoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM Configuracao");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Configuracao WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Configuracao ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT Configuracao.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM Configuracao ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT Configuracao.* FROM Configuracao ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Configuracao entidadeRetorno = new Configuracao();
                PopulaConfiguracao(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os Configuracao existentes na base de dados.
        /// </summary>
		public IEnumerable<Configuracao> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de Configuracao na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de Configuracao na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM Configuracao");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um Configuracao baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Configuracao a ser populado(.</param>
		public static void PopulaConfiguracao(IDataReader reader, Configuracao entidade) 
		{						
			if (reader["configuracaoId"] != DBNull.Value)
				entidade.ConfiguracaoId = Convert.ToInt32(reader["configuracaoId"].ToString());
			
			if (reader["chave"] != DBNull.Value)
				entidade.Chave = reader["chave"].ToString();
			
			if (reader["descricaoConfiguracao"] != DBNull.Value)
				entidade.DescricaoConfiguracao = reader["descricaoConfiguracao"].ToString();
			

		}		
		
	}
}
		