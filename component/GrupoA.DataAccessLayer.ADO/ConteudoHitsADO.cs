
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
	public partial class ConteudoHitsADO : ADOSuper, IConteudoHitsDAL {
	
	    /// <summary>
        /// Método que persiste um ConteudoHits.
        /// </summary>
        /// <param name="entidade">ConteudoHits contendo os dados a serem persistidos.</param>	
		public void Inserir(ConteudoHits entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO ConteudoHits ");
			sbSQL.Append(" (conteudoId, hits) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@conteudoId, @hits) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@conteudoId", DbType.Int32, entidade.ConteudoId);

			_db.AddInParameter(command, "@hits", DbType.Int32, entidade.Hits);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um ConteudoHits.
        /// </summary>
        /// <param name="entidade">ConteudoHits contendo os dados a serem atualizados.</param>
		public void Atualizar(ConteudoHits entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE ConteudoHits SET ");
			sbSQL.Append(" hits=@hits ");
			sbSQL.Append(" WHERE conteudoId=@conteudoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@conteudoId", DbType.Int32, entidade.ConteudoId);
			_db.AddInParameter(command, "@hits", DbType.Int32, entidade.Hits);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um ConteudoHits da base de dados.
        /// </summary>
        /// <param name="entidade">ConteudoHits a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(ConteudoHits entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM ConteudoHits ");
			sbSQL.Append("WHERE conteudoId=@conteudoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@conteudoId", DbType.Int32, entidade.ConteudoId);

								
			_db.ExecuteNonQuery(command);
		}
		

		/// <summary>
		/// Método que carrega um ConteudoHits.
		/// </summary>
        /// <param name="entidade">ConteudoHits a ser carregado (somente o identificador é necessário).</param>
		/// <returns>ConteudoHits</returns>
		public ConteudoHits Carregar(ConteudoHits entidade) {		
		
			ConteudoHits entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM ConteudoHits WHERE conteudoId=@conteudoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@conteudoId", DbType.Int32, entidade.ConteudoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new ConteudoHits();
				PopulaConteudoHits(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		
		
		/// <summary>
		/// Método que carrega um ConteudoHits com suas dependências.
		/// </summary>
        /// <param name="entidade">ConteudoHits a ser carregado (somente o identificador é necessário).</param>
		/// <returns>ConteudoHits</returns>
		public ConteudoHits CarregarComDependencias(ConteudoHits entidade) {		
		
			ConteudoHits entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append("SELECT ConteudoHits.conteudoId, ConteudoHits.hits");
			sbSQL.Append(", conteudoTipoId, dataHoraCadastro");
			sbSQL.Append(" FROM ConteudoHits");
			sbSQL.Append(" INNER JOIN Conteudo ON ConteudoHits.conteudoId=Conteudo.conteudoId");
			sbSQL.Append(" WHERE ConteudoHits.conteudoId=@conteudoId");
						
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@conteudoId", DbType.Int32, entidade.ConteudoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new ConteudoHits();
				PopulaConteudoHits(reader, entidadeRetorno);
				entidadeRetorno.Conteudo = new Conteudo();
				ConteudoADO.PopulaConteudo(reader, entidadeRetorno.Conteudo);
			}
			reader.Close();
			
			return entidadeRetorno;
		}		

		
		
		/// <summary>
        /// Método que retorna uma coleção de ConteudoHits.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos ConteudoHits.</returns>
		public IEnumerable<ConteudoHits> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<ConteudoHits> entidadesRetorno = new List<ConteudoHits>();
			
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
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM ConteudoHits");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ConteudoHits WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ConteudoHits ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT ConteudoHits.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM ConteudoHits ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT ConteudoHits.* FROM ConteudoHits ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                ConteudoHits entidadeRetorno = new ConteudoHits();
                PopulaConteudoHits(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os ConteudoHits existentes na base de dados.
        /// </summary>
		public IEnumerable<ConteudoHits> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de ConteudoHits na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de ConteudoHits na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM ConteudoHits");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um ConteudoHits baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">ConteudoHits a ser populado(.</param>
		public static void PopulaConteudoHits(IDataReader reader, ConteudoHits entidade) 
		{						
			if (reader["hits"] != DBNull.Value)
				entidade.Hits = Convert.ToInt32(reader["hits"].ToString());
			
			if (reader["conteudoId"] != DBNull.Value) {
				entidade.ConteudoId = Convert.ToInt32(reader["conteudoId"].ToString());
			}


		}		
		
	}
}
		