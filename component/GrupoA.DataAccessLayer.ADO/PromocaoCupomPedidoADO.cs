
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
	public partial class PromocaoCupomPedidoADO : ADOSuper, IPromocaoCupomPedidoDAL {
	
	    /// <summary>
        /// Método que persiste um PromocaoCupomPedido.
        /// </summary>
        /// <param name="entidade">PromocaoCupomPedido contendo os dados a serem persistidos.</param>	
		public void Inserir(PromocaoCupomPedido entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO PromocaoCupomPedido ");
			sbSQL.Append(" (promocaoCupomId, pedidoId) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@promocaoCupomId, @pedidoId) ");											

			sbSQL.Append(" ; SET @promocaoCupomPedidoId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@promocaoCupomPedidoId", DbType.Int32, 8);

			_db.AddInParameter(command, "@promocaoCupomId", DbType.Int32, entidade.PromocaoCupom.PromocaoCupomId);

			if (entidade.Pedido != null ) 
				_db.AddInParameter(command, "@pedidoId", DbType.Int32, entidade.Pedido.PedidoId);
			else
				_db.AddInParameter(command, "@pedidoId", DbType.Int32, null);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.PromocaoCupomPedidoId = Convert.ToInt32(_db.GetParameterValue(command, "@promocaoCupomPedidoId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um PromocaoCupomPedido.
        /// </summary>
        /// <param name="entidade">PromocaoCupomPedido contendo os dados a serem atualizados.</param>
		public void Atualizar(PromocaoCupomPedido entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE PromocaoCupomPedido SET ");
			sbSQL.Append(" promocaoCupomId=@promocaoCupomId, pedidoId=@pedidoId ");
			sbSQL.Append(" WHERE promocaoCupomPedidoId=@promocaoCupomPedidoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@promocaoCupomPedidoId", DbType.Int32, entidade.PromocaoCupomPedidoId);
			_db.AddInParameter(command, "@promocaoCupomId", DbType.Int32, entidade.PromocaoCupom.PromocaoCupomId);
			if (entidade.Pedido != null ) 
				_db.AddInParameter(command, "@pedidoId", DbType.Int32, entidade.Pedido.PedidoId);
			else
				_db.AddInParameter(command, "@pedidoId", DbType.Int32, null);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um PromocaoCupomPedido da base de dados.
        /// </summary>
        /// <param name="entidade">PromocaoCupomPedido a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(PromocaoCupomPedido entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM PromocaoCupomPedido ");
			sbSQL.Append("WHERE promocaoCupomPedidoId=@promocaoCupomPedidoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@promocaoCupomPedidoId", DbType.Int32, entidade.PromocaoCupomPedidoId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um PromocaoCupomPedido.
		/// </summary>
        /// <param name="entidade">PromocaoCupomPedido a ser carregado (somente o identificador é necessário).</param>
		/// <returns>PromocaoCupomPedido</returns>
		public PromocaoCupomPedido Carregar(int promocaoCupomPedidoId) {		
			PromocaoCupomPedido entidade = new PromocaoCupomPedido();
			entidade.PromocaoCupomPedidoId = promocaoCupomPedidoId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um PromocaoCupomPedido.
		/// </summary>
        /// <param name="entidade">PromocaoCupomPedido a ser carregado (somente o identificador é necessário).</param>
		/// <returns>PromocaoCupomPedido</returns>
		public PromocaoCupomPedido Carregar(PromocaoCupomPedido entidade) {		
		
			PromocaoCupomPedido entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM PromocaoCupomPedido WHERE promocaoCupomPedidoId=@promocaoCupomPedidoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@promocaoCupomPedidoId", DbType.Int32, entidade.PromocaoCupomPedidoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new PromocaoCupomPedido();
				PopulaPromocaoCupomPedido(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de PromocaoCupomPedido.
        /// </summary>
        /// <param name="entidade">Pedido relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de PromocaoCupomPedido.</returns>
		public IEnumerable<PromocaoCupomPedido> Carregar(Pedido entidade)
		{		
			List<PromocaoCupomPedido> entidadesRetorno = new List<PromocaoCupomPedido>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT PromocaoCupomPedido.* FROM PromocaoCupomPedido WHERE PromocaoCupomPedido.pedidoId=@pedidoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@pedidoId", DbType.Int32, entidade.PedidoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                PromocaoCupomPedido entidadeRetorno = new PromocaoCupomPedido();
                PopulaPromocaoCupomPedido(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de PromocaoCupomPedido.
        /// </summary>
        /// <param name="entidade">PromocaoCupom relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de PromocaoCupomPedido.</returns>
		public IEnumerable<PromocaoCupomPedido> Carregar(PromocaoCupom entidade)
		{		
			List<PromocaoCupomPedido> entidadesRetorno = new List<PromocaoCupomPedido>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT PromocaoCupomPedido.* FROM PromocaoCupomPedido WHERE PromocaoCupomPedido.promocaoCupomId=@promocaoCupomId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@promocaoCupomId", DbType.Int32, entidade.PromocaoCupomId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                PromocaoCupomPedido entidadeRetorno = new PromocaoCupomPedido();
                PopulaPromocaoCupomPedido(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de PromocaoCupomPedido.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos PromocaoCupomPedido.</returns>
		public IEnumerable<PromocaoCupomPedido> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<PromocaoCupomPedido> entidadesRetorno = new List<PromocaoCupomPedido>();
			
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
				sbOrder.Append( " ORDER BY promocaoCupomPedidoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM PromocaoCupomPedido");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM PromocaoCupomPedido WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM PromocaoCupomPedido ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT PromocaoCupomPedido.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM PromocaoCupomPedido ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT PromocaoCupomPedido.* FROM PromocaoCupomPedido ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                PromocaoCupomPedido entidadeRetorno = new PromocaoCupomPedido();
                PopulaPromocaoCupomPedido(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os PromocaoCupomPedido existentes na base de dados.
        /// </summary>
		public IEnumerable<PromocaoCupomPedido> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de PromocaoCupomPedido na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de PromocaoCupomPedido na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM PromocaoCupomPedido");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um PromocaoCupomPedido baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">PromocaoCupomPedido a ser populado(.</param>
		public static void PopulaPromocaoCupomPedido(IDataReader reader, PromocaoCupomPedido entidade) 
		{						
			if (reader["promocaoCupomPedidoId"] != DBNull.Value)
				entidade.PromocaoCupomPedidoId = Convert.ToInt32(reader["promocaoCupomPedidoId"].ToString());
			
			if (reader["promocaoCupomId"] != DBNull.Value) {
				entidade.PromocaoCupom = new PromocaoCupom();
				entidade.PromocaoCupom.PromocaoCupomId = Convert.ToInt32(reader["promocaoCupomId"].ToString());
			}

			if (reader["pedidoId"] != DBNull.Value) {
				entidade.Pedido = new Pedido();
				entidade.Pedido.PedidoId = Convert.ToInt32(reader["pedidoId"].ToString());
			}


		}		
		
	}
}
		