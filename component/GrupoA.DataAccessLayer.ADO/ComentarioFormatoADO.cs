
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
	public partial class ComentarioFormatoADO : ADOSuper, IComentarioFormatoDAL {
	
	    /// <summary>
        /// Método que persiste um ComentarioFormato.
        /// </summary>
        /// <param name="entidade">ComentarioFormato contendo os dados a serem persistidos.</param>	
		public void Inserir(ComentarioFormato entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO ComentarioFormato ");
			sbSQL.Append(" (comentarioFormatoId, formato) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@comentarioFormatoId, @formato) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@comentarioFormatoId", DbType.Int32, entidade.ComentarioFormatoId);

			_db.AddInParameter(command, "@formato", DbType.String, entidade.Formato);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um ComentarioFormato.
        /// </summary>
        /// <param name="entidade">ComentarioFormato contendo os dados a serem atualizados.</param>
		public void Atualizar(ComentarioFormato entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE ComentarioFormato SET ");
			sbSQL.Append(" formato=@formato ");
			sbSQL.Append(" WHERE comentarioFormatoId=@comentarioFormatoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@comentarioFormatoId", DbType.Int32, entidade.ComentarioFormatoId);
			_db.AddInParameter(command, "@formato", DbType.String, entidade.Formato);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um ComentarioFormato da base de dados.
        /// </summary>
        /// <param name="entidade">ComentarioFormato a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(ComentarioFormato entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM ComentarioFormato ");
			sbSQL.Append("WHERE comentarioFormatoId=@comentarioFormatoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@comentarioFormatoId", DbType.Int32, entidade.ComentarioFormatoId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um ComentarioFormato.
		/// </summary>
        /// <param name="entidade">ComentarioFormato a ser carregado (somente o identificador é necessário).</param>
		/// <returns>ComentarioFormato</returns>
		public ComentarioFormato Carregar(int comentarioFormatoId) {		
			ComentarioFormato entidade = new ComentarioFormato();
			entidade.ComentarioFormatoId = comentarioFormatoId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um ComentarioFormato.
		/// </summary>
        /// <param name="entidade">ComentarioFormato a ser carregado (somente o identificador é necessário).</param>
		/// <returns>ComentarioFormato</returns>
		public ComentarioFormato Carregar(ComentarioFormato entidade) {		
		
			ComentarioFormato entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM ComentarioFormato WHERE comentarioFormatoId=@comentarioFormatoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@comentarioFormatoId", DbType.Int32, entidade.ComentarioFormatoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new ComentarioFormato();
				PopulaComentarioFormato(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de ComentarioFormato.
        /// </summary>
        /// <param name="entidade">TituloInformacaoComentarioEspecialista relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de ComentarioFormato.</returns>
		public IEnumerable<ComentarioFormato> Carregar(TituloInformacaoComentarioEspecialista entidade)
		{		
			List<ComentarioFormato> entidadesRetorno = new List<ComentarioFormato>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT ComentarioFormato.* FROM ComentarioFormato INNER JOIN TituloInformacaoComentarioEspecialista ON ComentarioFormato.comentarioFormatoId=TituloInformacaoComentarioEspecialista.comentarioFormatoId WHERE TituloInformacaoComentarioEspecialista.tituloInformacaoComentarioEspecialistaId=@tituloInformacaoComentarioEspecialistaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@tituloInformacaoComentarioEspecialistaId", DbType.Int32, entidade.TituloInformacaoComentarioEspecialistaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                ComentarioFormato entidadeRetorno = new ComentarioFormato();
                PopulaComentarioFormato(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de ComentarioFormato.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos ComentarioFormato.</returns>
		public IEnumerable<ComentarioFormato> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<ComentarioFormato> entidadesRetorno = new List<ComentarioFormato>();
			
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
				sbOrder.Append( " ORDER BY comentarioFormatoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM ComentarioFormato");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ComentarioFormato WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ComentarioFormato ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT ComentarioFormato.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM ComentarioFormato ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT ComentarioFormato.* FROM ComentarioFormato ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                ComentarioFormato entidadeRetorno = new ComentarioFormato();
                PopulaComentarioFormato(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os ComentarioFormato existentes na base de dados.
        /// </summary>
		public IEnumerable<ComentarioFormato> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de ComentarioFormato na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de ComentarioFormato na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM ComentarioFormato");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um ComentarioFormato baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">ComentarioFormato a ser populado(.</param>
		public static void PopulaComentarioFormato(IDataReader reader, ComentarioFormato entidade) 
		{						
			if (reader["comentarioFormatoId"] != DBNull.Value)
				entidade.ComentarioFormatoId = Convert.ToInt32(reader["comentarioFormatoId"].ToString());
			
			if (reader["formato"] != DBNull.Value)
				entidade.Formato = reader["formato"].ToString();
			

		}		
		
	}
}
		