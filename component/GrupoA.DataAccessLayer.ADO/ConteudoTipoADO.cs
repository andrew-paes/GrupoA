
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
	public partial class ConteudoTipoADO : ADOSuper, IConteudoTipoDAL {
	
	    /// <summary>
        /// Método que persiste um ConteudoTipo.
        /// </summary>
        /// <param name="entidade">ConteudoTipo contendo os dados a serem persistidos.</param>	
		public void Inserir(ConteudoTipo entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO ConteudoTipo ");
			sbSQL.Append(" (conteudoTipoId, tipo) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@conteudoTipoId, @tipo) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@conteudoTipoId", DbType.Int32, entidade.ConteudoTipoId);

			_db.AddInParameter(command, "@tipo", DbType.String, entidade.Tipo);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um ConteudoTipo.
        /// </summary>
        /// <param name="entidade">ConteudoTipo contendo os dados a serem atualizados.</param>
		public void Atualizar(ConteudoTipo entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE ConteudoTipo SET ");
			sbSQL.Append(" tipo=@tipo ");
			sbSQL.Append(" WHERE conteudoTipoId=@conteudoTipoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@conteudoTipoId", DbType.Int32, entidade.ConteudoTipoId);
			_db.AddInParameter(command, "@tipo", DbType.String, entidade.Tipo);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um ConteudoTipo da base de dados.
        /// </summary>
        /// <param name="entidade">ConteudoTipo a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(ConteudoTipo entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM ConteudoTipo ");
			sbSQL.Append("WHERE conteudoTipoId=@conteudoTipoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@conteudoTipoId", DbType.Int32, entidade.ConteudoTipoId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um ConteudoTipo.
		/// </summary>
        /// <param name="entidade">ConteudoTipo a ser carregado (somente o identificador é necessário).</param>
		/// <returns>ConteudoTipo</returns>
		public ConteudoTipo Carregar(int conteudoTipoId) {		
			ConteudoTipo entidade = new ConteudoTipo();
			entidade.ConteudoTipoId = conteudoTipoId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um ConteudoTipo.
		/// </summary>
        /// <param name="entidade">ConteudoTipo a ser carregado (somente o identificador é necessário).</param>
		/// <returns>ConteudoTipo</returns>
		public ConteudoTipo Carregar(ConteudoTipo entidade) {		
		
			ConteudoTipo entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM ConteudoTipo WHERE conteudoTipoId=@conteudoTipoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@conteudoTipoId", DbType.Int32, entidade.ConteudoTipoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new ConteudoTipo();
				PopulaConteudoTipo(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de ConteudoTipo.
        /// </summary>
        /// <param name="entidade">Conteudo relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de ConteudoTipo.</returns>
		public IEnumerable<ConteudoTipo> Carregar(Conteudo entidade)
		{		
			List<ConteudoTipo> entidadesRetorno = new List<ConteudoTipo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT ConteudoTipo.* FROM ConteudoTipo INNER JOIN Conteudo ON ConteudoTipo.conteudoTipoId=Conteudo.conteudoTipoId WHERE Conteudo.conteudoId=@conteudoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@conteudoId", DbType.Int32, entidade.ConteudoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                ConteudoTipo entidadeRetorno = new ConteudoTipo();
                PopulaConteudoTipo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de ConteudoTipo.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos ConteudoTipo.</returns>
		public IEnumerable<ConteudoTipo> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<ConteudoTipo> entidadesRetorno = new List<ConteudoTipo>();
			
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
				sbOrder.Append( " ORDER BY conteudoTipoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM ConteudoTipo");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ConteudoTipo WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ConteudoTipo ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT ConteudoTipo.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM ConteudoTipo ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT ConteudoTipo.* FROM ConteudoTipo ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                ConteudoTipo entidadeRetorno = new ConteudoTipo();
                PopulaConteudoTipo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os ConteudoTipo existentes na base de dados.
        /// </summary>
		public IEnumerable<ConteudoTipo> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de ConteudoTipo na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de ConteudoTipo na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM ConteudoTipo");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um ConteudoTipo baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">ConteudoTipo a ser populado(.</param>
		public static void PopulaConteudoTipo(IDataReader reader, ConteudoTipo entidade) 
		{						
			if (reader["conteudoTipoId"] != DBNull.Value)
				entidade.ConteudoTipoId = Convert.ToInt32(reader["conteudoTipoId"].ToString());
			
			if (reader["tipo"] != DBNull.Value)
				entidade.Tipo = reader["tipo"].ToString();
			

		}		
		
	}
}
		