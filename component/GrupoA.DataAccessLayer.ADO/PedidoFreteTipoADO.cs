
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
	public partial class PedidoFreteTipoADO : ADOSuper, IPedidoFreteTipoDAL {
	
	    /// <summary>
        /// Método que persiste um PedidoFreteTipo.
        /// </summary>
        /// <param name="entidade">PedidoFreteTipo contendo os dados a serem persistidos.</param>	
		public void Inserir(PedidoFreteTipo entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO PedidoFreteTipo ");
			sbSQL.Append(" (PedidoFreteTipoId, nomeTipo) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@PedidoFreteTipoId, @nomeTipo) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@PedidoFreteTipoId", DbType.String, entidade.PedidoFreteTipoId);

			_db.AddInParameter(command, "@nomeTipo", DbType.String, entidade.NomeTipo);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um PedidoFreteTipo.
        /// </summary>
        /// <param name="entidade">PedidoFreteTipo contendo os dados a serem atualizados.</param>
		public void Atualizar(PedidoFreteTipo entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE PedidoFreteTipo SET ");
			sbSQL.Append(" nomeTipo=@nomeTipo ");
			sbSQL.Append(" WHERE PedidoFreteTipoId=@PedidoFreteTipoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@PedidoFreteTipoId", DbType.String, entidade.PedidoFreteTipoId);
			_db.AddInParameter(command, "@nomeTipo", DbType.String, entidade.NomeTipo);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um PedidoFreteTipo da base de dados.
        /// </summary>
        /// <param name="entidade">PedidoFreteTipo a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(PedidoFreteTipo entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM PedidoFreteTipo ");
			sbSQL.Append("WHERE PedidoFreteTipoId=@PedidoFreteTipoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@PedidoFreteTipoId", DbType.String, entidade.PedidoFreteTipoId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um PedidoFreteTipo.
		/// </summary>
        /// <param name="entidade">PedidoFreteTipo a ser carregado (somente o identificador é necessário).</param>
		/// <returns>PedidoFreteTipo</returns>
		public PedidoFreteTipo Carregar(string pedidoFreteTipoId) {		
			PedidoFreteTipo entidade = new PedidoFreteTipo();
			entidade.PedidoFreteTipoId = pedidoFreteTipoId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um PedidoFreteTipo.
		/// </summary>
        /// <param name="entidade">PedidoFreteTipo a ser carregado (somente o identificador é necessário).</param>
		/// <returns>PedidoFreteTipo</returns>
		public PedidoFreteTipo Carregar(PedidoFreteTipo entidade) {		
		
			PedidoFreteTipo entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM PedidoFreteTipo WHERE PedidoFreteTipoId=@PedidoFreteTipoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@PedidoFreteTipoId", DbType.String, entidade.PedidoFreteTipoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new PedidoFreteTipo();
				PopulaPedidoFreteTipo(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de PedidoFreteTipo.
        /// </summary>
        /// <param name="entidade">PedidoFreteGrupo relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de PedidoFreteTipo.</returns>
		public IEnumerable<PedidoFreteTipo> Carregar(PedidoFreteGrupo entidade)
		{		
			List<PedidoFreteTipo> entidadesRetorno = new List<PedidoFreteTipo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT PedidoFreteTipo.* FROM PedidoFreteTipo INNER JOIN PedidoFreteGrupo ON PedidoFreteTipo.PedidoFreteTipoId=PedidoFreteGrupo.PedidoFreteTipoId WHERE PedidoFreteGrupo.pedidoFreteGrupoId=@pedidoFreteGrupoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@pedidoFreteGrupoId", DbType.Int32, entidade.PedidoFreteGrupoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                PedidoFreteTipo entidadeRetorno = new PedidoFreteTipo();
                PopulaPedidoFreteTipo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de PedidoFreteTipo.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos PedidoFreteTipo.</returns>
		public IEnumerable<PedidoFreteTipo> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<PedidoFreteTipo> entidadesRetorno = new List<PedidoFreteTipo>();
			
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
				sbOrder.Append( " ORDER BY PedidoFreteTipoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM PedidoFreteTipo");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM PedidoFreteTipo WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM PedidoFreteTipo ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT PedidoFreteTipo.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM PedidoFreteTipo ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT PedidoFreteTipo.* FROM PedidoFreteTipo ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                PedidoFreteTipo entidadeRetorno = new PedidoFreteTipo();
                PopulaPedidoFreteTipo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os PedidoFreteTipo existentes na base de dados.
        /// </summary>
		public IEnumerable<PedidoFreteTipo> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de PedidoFreteTipo na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de PedidoFreteTipo na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM PedidoFreteTipo");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um PedidoFreteTipo baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">PedidoFreteTipo a ser populado(.</param>
		public static void PopulaPedidoFreteTipo(IDataReader reader, PedidoFreteTipo entidade) 
		{						
			if (reader["PedidoFreteTipoId"] != DBNull.Value)
				entidade.PedidoFreteTipoId = reader["PedidoFreteTipoId"].ToString();
			
			if (reader["nomeTipo"] != DBNull.Value)
				entidade.NomeTipo = reader["nomeTipo"].ToString();
			

		}		
		
	}
}
		