
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
	public partial class ConfiguracaoValorADO : ADOSuper, IConfiguracaoValorDAL {
	
	    /// <summary>
        /// Método que persiste um ConfiguracaoValor.
        /// </summary>
        /// <param name="entidade">ConfiguracaoValor contendo os dados a serem persistidos.</param>	
		public void Inserir(ConfiguracaoValor entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO ConfiguracaoValor ");
			sbSQL.Append(" (configuracaoId, valor) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@configuracaoId, @valor) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@configuracaoId", DbType.Int32, entidade.ConfiguracaoId);

			_db.AddInParameter(command, "@valor", DbType.String, entidade.Valor);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um ConfiguracaoValor.
        /// </summary>
        /// <param name="entidade">ConfiguracaoValor contendo os dados a serem atualizados.</param>
		public void Atualizar(ConfiguracaoValor entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE ConfiguracaoValor SET ");
			sbSQL.Append(" valor=@valor ");
			sbSQL.Append(" WHERE configuracaoId=@configuracaoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@configuracaoId", DbType.Int32, entidade.ConfiguracaoId);
			_db.AddInParameter(command, "@valor", DbType.String, entidade.Valor);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um ConfiguracaoValor da base de dados.
        /// </summary>
        /// <param name="entidade">ConfiguracaoValor a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(ConfiguracaoValor entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM ConfiguracaoValor ");
			sbSQL.Append("WHERE configuracaoId=@configuracaoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@configuracaoId", DbType.Int32, entidade.ConfiguracaoId);

								
			_db.ExecuteNonQuery(command);
		}
		

		/// <summary>
		/// Método que carrega um ConfiguracaoValor.
		/// </summary>
        /// <param name="entidade">ConfiguracaoValor a ser carregado (somente o identificador é necessário).</param>
		/// <returns>ConfiguracaoValor</returns>
		public ConfiguracaoValor Carregar(ConfiguracaoValor entidade) {		
		
			ConfiguracaoValor entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM ConfiguracaoValor WHERE configuracaoId=@configuracaoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@configuracaoId", DbType.Int32, entidade.ConfiguracaoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new ConfiguracaoValor();
				PopulaConfiguracaoValor(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		
		
		/// <summary>
		/// Método que carrega um ConfiguracaoValor com suas dependências.
		/// </summary>
        /// <param name="entidade">ConfiguracaoValor a ser carregado (somente o identificador é necessário).</param>
		/// <returns>ConfiguracaoValor</returns>
		public ConfiguracaoValor CarregarComDependencias(ConfiguracaoValor entidade) {		
		
			ConfiguracaoValor entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append("SELECT ConfiguracaoValor.configuracaoId, ConfiguracaoValor.valor");
			sbSQL.Append(", chave, descricaoConfiguracao");
			sbSQL.Append(" FROM ConfiguracaoValor");
			sbSQL.Append(" INNER JOIN Configuracao ON ConfiguracaoValor.configuracaoId=Configuracao.configuracaoId");
			sbSQL.Append(" WHERE ConfiguracaoValor.configuracaoId=@configuracaoId");
						
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@configuracaoId", DbType.Int32, entidade.ConfiguracaoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new ConfiguracaoValor();
				PopulaConfiguracaoValor(reader, entidadeRetorno);
				entidadeRetorno.Configuracao = new Configuracao();
				ConfiguracaoADO.PopulaConfiguracao(reader, entidadeRetorno.Configuracao);
			}
			reader.Close();
			
			return entidadeRetorno;
		}		

		
		
		/// <summary>
        /// Método que retorna uma coleção de ConfiguracaoValor.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos ConfiguracaoValor.</returns>
		public IEnumerable<ConfiguracaoValor> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<ConfiguracaoValor> entidadesRetorno = new List<ConfiguracaoValor>();
			
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
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM ConfiguracaoValor");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ConfiguracaoValor WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ConfiguracaoValor ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT ConfiguracaoValor.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM ConfiguracaoValor ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT ConfiguracaoValor.* FROM ConfiguracaoValor ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                ConfiguracaoValor entidadeRetorno = new ConfiguracaoValor();
                PopulaConfiguracaoValor(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os ConfiguracaoValor existentes na base de dados.
        /// </summary>
		public IEnumerable<ConfiguracaoValor> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de ConfiguracaoValor na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de ConfiguracaoValor na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM ConfiguracaoValor");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um ConfiguracaoValor baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">ConfiguracaoValor a ser populado(.</param>
		public static void PopulaConfiguracaoValor(IDataReader reader, ConfiguracaoValor entidade) 
		{						
			if (reader["valor"] != DBNull.Value)
				entidade.Valor = reader["valor"].ToString();
			
			if (reader["configuracaoId"] != DBNull.Value) {
				entidade.ConfiguracaoId = Convert.ToInt32(reader["configuracaoId"].ToString());
			}


		}		
		
	}
}
		