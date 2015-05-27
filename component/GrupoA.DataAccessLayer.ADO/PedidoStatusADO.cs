
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
	public partial class PedidoStatusADO : ADOSuper, IPedidoStatusDAL {
	
	    /// <summary>
        /// Método que persiste um PedidoStatus.
        /// </summary>
        /// <param name="entidade">PedidoStatus contendo os dados a serem persistidos.</param>	
		public void Inserir(PedidoStatus entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO PedidoStatus ");
			sbSQL.Append(" (pedidoStatusId, statusPedido) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@pedidoStatusId, @statusPedido) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@pedidoStatusId", DbType.Int32, entidade.PedidoStatusId);

			_db.AddInParameter(command, "@statusPedido", DbType.String, entidade.StatusPedido);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um PedidoStatus.
        /// </summary>
        /// <param name="entidade">PedidoStatus contendo os dados a serem atualizados.</param>
		public void Atualizar(PedidoStatus entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE PedidoStatus SET ");
			sbSQL.Append(" statusPedido=@statusPedido ");
			sbSQL.Append(" WHERE pedidoStatusId=@pedidoStatusId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@pedidoStatusId", DbType.Int32, entidade.PedidoStatusId);
			_db.AddInParameter(command, "@statusPedido", DbType.String, entidade.StatusPedido);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um PedidoStatus da base de dados.
        /// </summary>
        /// <param name="entidade">PedidoStatus a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(PedidoStatus entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM PedidoStatus ");
			sbSQL.Append("WHERE pedidoStatusId=@pedidoStatusId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@pedidoStatusId", DbType.Int32, entidade.PedidoStatusId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um PedidoStatus.
		/// </summary>
        /// <param name="entidade">PedidoStatus a ser carregado (somente o identificador é necessário).</param>
		/// <returns>PedidoStatus</returns>
		public PedidoStatus Carregar(int pedidoStatusId) {		
			PedidoStatus entidade = new PedidoStatus();
			entidade.PedidoStatusId = pedidoStatusId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um PedidoStatus.
		/// </summary>
        /// <param name="entidade">PedidoStatus a ser carregado (somente o identificador é necessário).</param>
		/// <returns>PedidoStatus</returns>
		public PedidoStatus Carregar(PedidoStatus entidade) {		
		
			PedidoStatus entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM PedidoStatus WHERE pedidoStatusId=@pedidoStatusId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@pedidoStatusId", DbType.Int32, entidade.PedidoStatusId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new PedidoStatus();
				PopulaPedidoStatus(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de PedidoStatus.
        /// </summary>
        /// <param name="entidade">Pedido relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de PedidoStatus.</returns>
		public IEnumerable<PedidoStatus> Carregar(Pedido entidade)
		{		
			List<PedidoStatus> entidadesRetorno = new List<PedidoStatus>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT PedidoStatus.* FROM PedidoStatus INNER JOIN Pedido ON PedidoStatus.pedidoStatusId=Pedido.pedidoStatusId WHERE Pedido.pedidoId=@pedidoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@pedidoId", DbType.Int32, entidade.PedidoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                PedidoStatus entidadeRetorno = new PedidoStatus();
                PopulaPedidoStatus(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de PedidoStatus.
        /// </summary>
        /// <param name="entidade">PedidoSituacao relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de PedidoStatus.</returns>
		public IEnumerable<PedidoStatus> Carregar(PedidoSituacao entidade)
		{		
			List<PedidoStatus> entidadesRetorno = new List<PedidoStatus>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT PedidoStatus.* FROM PedidoStatus INNER JOIN PedidoSituacao ON PedidoStatus.pedidoStatusId=PedidoSituacao.pedidoStatusId WHERE PedidoSituacao.pedidoSituacaoId=@pedidoSituacaoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@pedidoSituacaoId", DbType.Int32, entidade.PedidoSituacaoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                PedidoStatus entidadeRetorno = new PedidoStatus();
                PopulaPedidoStatus(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de PedidoStatus.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos PedidoStatus.</returns>
		public IEnumerable<PedidoStatus> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<PedidoStatus> entidadesRetorno = new List<PedidoStatus>();
			
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
				sbOrder.Append( " ORDER BY pedidoStatusId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM PedidoStatus");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM PedidoStatus WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM PedidoStatus ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT PedidoStatus.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM PedidoStatus ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT PedidoStatus.* FROM PedidoStatus ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                PedidoStatus entidadeRetorno = new PedidoStatus();
                PopulaPedidoStatus(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os PedidoStatus existentes na base de dados.
        /// </summary>
		public IEnumerable<PedidoStatus> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de PedidoStatus na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de PedidoStatus na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM PedidoStatus");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um PedidoStatus baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">PedidoStatus a ser populado(.</param>
		public static void PopulaPedidoStatus(IDataReader reader, PedidoStatus entidade) 
		{						
			if (reader["pedidoStatusId"] != DBNull.Value)
				entidade.PedidoStatusId = Convert.ToInt32(reader["pedidoStatusId"].ToString());
			
			if (reader["statusPedido"] != DBNull.Value)
				entidade.StatusPedido = reader["statusPedido"].ToString();
			

		}		
		
	}
}
		