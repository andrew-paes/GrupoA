
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
	public partial class TituloConteudoExtraUrlADO : ADOSuper, ITituloConteudoExtraUrlDAL {
	
	    /// <summary>
        /// Método que persiste um TituloConteudoExtraUrl.
        /// </summary>
        /// <param name="entidade">TituloConteudoExtraUrl contendo os dados a serem persistidos.</param>	
		public void Inserir(TituloConteudoExtraUrl entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO TituloConteudoExtraUrl ");
			sbSQL.Append(" (tituloConteudoExtraUrlId, url, targetBlank) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@tituloConteudoExtraUrlId, @url, @targetBlank) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@tituloConteudoExtraUrlId", DbType.Int32, entidade.TituloConteudoExtraUrlId);

			_db.AddInParameter(command, "@url", DbType.String, entidade.Url);

			_db.AddInParameter(command, "@targetBlank", DbType.Int32, entidade.TargetBlank);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um TituloConteudoExtraUrl.
        /// </summary>
        /// <param name="entidade">TituloConteudoExtraUrl contendo os dados a serem atualizados.</param>
		public void Atualizar(TituloConteudoExtraUrl entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE TituloConteudoExtraUrl SET ");
			sbSQL.Append(" url=@url, targetBlank=@targetBlank ");
			sbSQL.Append(" WHERE tituloConteudoExtraUrlId=@tituloConteudoExtraUrlId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@tituloConteudoExtraUrlId", DbType.Int32, entidade.TituloConteudoExtraUrlId);
			_db.AddInParameter(command, "@url", DbType.String, entidade.Url);
			_db.AddInParameter(command, "@targetBlank", DbType.Int32, entidade.TargetBlank);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um TituloConteudoExtraUrl da base de dados.
        /// </summary>
        /// <param name="entidade">TituloConteudoExtraUrl a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(TituloConteudoExtraUrl entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM TituloConteudoExtraUrl ");
			sbSQL.Append("WHERE tituloConteudoExtraUrlId=@tituloConteudoExtraUrlId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@tituloConteudoExtraUrlId", DbType.Int32, entidade.TituloConteudoExtraUrlId);

								
			_db.ExecuteNonQuery(command);
		}
		

		/// <summary>
		/// Método que carrega um TituloConteudoExtraUrl.
		/// </summary>
        /// <param name="entidade">TituloConteudoExtraUrl a ser carregado (somente o identificador é necessário).</param>
		/// <returns>TituloConteudoExtraUrl</returns>
		public TituloConteudoExtraUrl Carregar(TituloConteudoExtraUrl entidade) {		
		
			TituloConteudoExtraUrl entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM TituloConteudoExtraUrl WHERE tituloConteudoExtraUrlId=@tituloConteudoExtraUrlId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@tituloConteudoExtraUrlId", DbType.Int32, entidade.TituloConteudoExtraUrlId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new TituloConteudoExtraUrl();
				PopulaTituloConteudoExtraUrl(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		
		
		/// <summary>
		/// Método que carrega um TituloConteudoExtraUrl com suas dependências.
		/// </summary>
        /// <param name="entidade">TituloConteudoExtraUrl a ser carregado (somente o identificador é necessário).</param>
		/// <returns>TituloConteudoExtraUrl</returns>
		public TituloConteudoExtraUrl CarregarComDependencias(TituloConteudoExtraUrl entidade) {		
		
			TituloConteudoExtraUrl entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append("SELECT TituloConteudoExtraUrl.tituloConteudoExtraUrlId, TituloConteudoExtraUrl.url, TituloConteudoExtraUrl.targetBlank");
			sbSQL.Append(", tituloId, subtituloLivro, numeroPaginas, edicao, dataLancamento, dataPublicacao, maisVendido, nomeTitulo, formato");
			sbSQL.Append(", conteudoId, conteudoTipoId, dataHoraCadastro");
			sbSQL.Append(" FROM TituloConteudoExtraUrl");
			sbSQL.Append(" INNER JOIN Titulo ON TituloConteudoExtraUrl.tituloConteudoExtraUrlId=Titulo.tituloId");
			sbSQL.Append(" INNER JOIN Conteudo ON Titulo.tituloId=Conteudo.conteudoId");
			sbSQL.Append(" WHERE TituloConteudoExtraUrl.tituloConteudoExtraUrlId=@tituloConteudoExtraUrlId");
						
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@tituloConteudoExtraUrlId", DbType.Int32, entidade.TituloConteudoExtraUrlId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new TituloConteudoExtraUrl();
				PopulaTituloConteudoExtraUrl(reader, entidadeRetorno);
				entidadeRetorno.Titulo = new Titulo();
				TituloADO.PopulaTitulo(reader, entidadeRetorno.Titulo);
				entidadeRetorno.Titulo.Conteudo = new Conteudo();
				ConteudoADO.PopulaConteudo(reader, entidadeRetorno.Titulo.Conteudo);
			}
			reader.Close();
			
			return entidadeRetorno;
		}		

		
		
		/// <summary>
        /// Método que retorna uma coleção de TituloConteudoExtraUrl.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos TituloConteudoExtraUrl.</returns>
		public IEnumerable<TituloConteudoExtraUrl> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<TituloConteudoExtraUrl> entidadesRetorno = new List<TituloConteudoExtraUrl>();
			
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
				sbOrder.Append( " ORDER BY tituloConteudoExtraUrlId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM TituloConteudoExtraUrl");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM TituloConteudoExtraUrl WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM TituloConteudoExtraUrl ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT TituloConteudoExtraUrl.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM TituloConteudoExtraUrl ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT TituloConteudoExtraUrl.* FROM TituloConteudoExtraUrl ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                TituloConteudoExtraUrl entidadeRetorno = new TituloConteudoExtraUrl();
                PopulaTituloConteudoExtraUrl(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os TituloConteudoExtraUrl existentes na base de dados.
        /// </summary>
		public IEnumerable<TituloConteudoExtraUrl> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de TituloConteudoExtraUrl na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de TituloConteudoExtraUrl na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM TituloConteudoExtraUrl");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um TituloConteudoExtraUrl baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">TituloConteudoExtraUrl a ser populado(.</param>
		public static void PopulaTituloConteudoExtraUrl(IDataReader reader, TituloConteudoExtraUrl entidade) 
		{						
			if (reader["url"] != DBNull.Value)
				entidade.Url = reader["url"].ToString();
			
			if (reader["targetBlank"] != DBNull.Value)
				entidade.TargetBlank = Convert.ToBoolean(reader["targetBlank"].ToString());
			
			if (reader["tituloConteudoExtraUrlId"] != DBNull.Value) {
				entidade.TituloConteudoExtraUrlId = Convert.ToInt32(reader["tituloConteudoExtraUrlId"].ToString());
			}


		}		
		
	}
}
		