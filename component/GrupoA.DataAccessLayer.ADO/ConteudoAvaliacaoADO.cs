
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
	public partial class ConteudoAvaliacaoADO : ADOSuper, IConteudoAvaliacaoDAL {
	
	    /// <summary>
        /// Método que persiste um ConteudoAvaliacao.
        /// </summary>
        /// <param name="entidade">ConteudoAvaliacao contendo os dados a serem persistidos.</param>	
		public void Inserir(ConteudoAvaliacao entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO ConteudoAvaliacao ");
			sbSQL.Append(" (conteudoId, avaliacoes) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@conteudoId, @avaliacoes) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@conteudoId", DbType.Int32, entidade.ConteudoId);

			if (entidade.Avaliacoes != null ) 
				_db.AddInParameter(command, "@avaliacoes", DbType.Int32, entidade.Avaliacoes);
			else
				_db.AddInParameter(command, "@avaliacoes", DbType.Int32, null);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um ConteudoAvaliacao.
        /// </summary>
        /// <param name="entidade">ConteudoAvaliacao contendo os dados a serem atualizados.</param>
		public void Atualizar(ConteudoAvaliacao entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE ConteudoAvaliacao SET ");
			sbSQL.Append(" avaliacoes=@avaliacoes ");
			sbSQL.Append(" WHERE conteudoId=@conteudoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@conteudoId", DbType.Int32, entidade.ConteudoId);
			if (entidade.Avaliacoes != null ) 
				_db.AddInParameter(command, "@avaliacoes", DbType.Int32, entidade.Avaliacoes);
			else
				_db.AddInParameter(command, "@avaliacoes", DbType.Int32, null);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um ConteudoAvaliacao da base de dados.
        /// </summary>
        /// <param name="entidade">ConteudoAvaliacao a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(ConteudoAvaliacao entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM ConteudoAvaliacao ");
			sbSQL.Append("WHERE conteudoId=@conteudoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@conteudoId", DbType.Int32, entidade.ConteudoId);

								
			_db.ExecuteNonQuery(command);
		}
		

		/// <summary>
		/// Método que carrega um ConteudoAvaliacao.
		/// </summary>
        /// <param name="entidade">ConteudoAvaliacao a ser carregado (somente o identificador é necessário).</param>
		/// <returns>ConteudoAvaliacao</returns>
		public ConteudoAvaliacao Carregar(ConteudoAvaliacao entidade) {		
		
			ConteudoAvaliacao entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM ConteudoAvaliacao WHERE conteudoId=@conteudoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@conteudoId", DbType.Int32, entidade.ConteudoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new ConteudoAvaliacao();
				PopulaConteudoAvaliacao(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		
		
		/// <summary>
		/// Método que carrega um ConteudoAvaliacao com suas dependências.
		/// </summary>
        /// <param name="entidade">ConteudoAvaliacao a ser carregado (somente o identificador é necessário).</param>
		/// <returns>ConteudoAvaliacao</returns>
		public ConteudoAvaliacao CarregarComDependencias(ConteudoAvaliacao entidade) {		
		
			ConteudoAvaliacao entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append("SELECT ConteudoAvaliacao.conteudoId, ConteudoAvaliacao.avaliacoes");
			sbSQL.Append(", conteudoTipoId, dataHoraCadastro");
			sbSQL.Append(" FROM ConteudoAvaliacao");
			sbSQL.Append(" INNER JOIN Conteudo ON ConteudoAvaliacao.conteudoId=Conteudo.conteudoId");
			sbSQL.Append(" WHERE ConteudoAvaliacao.conteudoId=@conteudoId");
						
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@conteudoId", DbType.Int32, entidade.ConteudoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new ConteudoAvaliacao();
				PopulaConteudoAvaliacao(reader, entidadeRetorno);
				entidadeRetorno.Conteudo = new Conteudo();
				ConteudoADO.PopulaConteudo(reader, entidadeRetorno.Conteudo);
			}
			reader.Close();
			
			return entidadeRetorno;
		}		

		
		
		/// <summary>
        /// Método que retorna uma coleção de ConteudoAvaliacao.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos ConteudoAvaliacao.</returns>
		public IEnumerable<ConteudoAvaliacao> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<ConteudoAvaliacao> entidadesRetorno = new List<ConteudoAvaliacao>();
			
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
				sbOrder.Append( " ORDER BY conteudoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM ConteudoAvaliacao");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ConteudoAvaliacao WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ConteudoAvaliacao ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT ConteudoAvaliacao.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM ConteudoAvaliacao ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT ConteudoAvaliacao.* FROM ConteudoAvaliacao ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                ConteudoAvaliacao entidadeRetorno = new ConteudoAvaliacao();
                PopulaConteudoAvaliacao(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os ConteudoAvaliacao existentes na base de dados.
        /// </summary>
		public IEnumerable<ConteudoAvaliacao> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de ConteudoAvaliacao na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de ConteudoAvaliacao na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM ConteudoAvaliacao");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um ConteudoAvaliacao baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">ConteudoAvaliacao a ser populado(.</param>
		public static void PopulaConteudoAvaliacao(IDataReader reader, ConteudoAvaliacao entidade) 
		{						
			if (reader["avaliacoes"] != DBNull.Value)
				entidade.Avaliacoes = Convert.ToInt32(reader["avaliacoes"].ToString());
			
			if (reader["conteudoId"] != DBNull.Value) {
				entidade.ConteudoId = Convert.ToInt32(reader["conteudoId"].ToString());
			}


		}		
		
	}
}
		