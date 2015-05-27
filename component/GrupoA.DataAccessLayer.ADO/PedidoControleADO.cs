
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
	public partial class PedidoControleADO : ADOSuper, IPedidoControleDAL {
	
	    /// <summary>
        /// Método que persiste um PedidoControle.
        /// </summary>
        /// <param name="entidade">PedidoControle contendo os dados a serem persistidos.</param>	
		public void Inserir(PedidoControle entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO PedidoControle ");
			sbSQL.Append(" (pedidoId, dataHoraExportacao) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@pedidoId, @dataHoraExportacao) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@pedidoId", DbType.Int32, entidade.PedidoId);

			if (entidade.DataHoraExportacao != null && entidade.DataHoraExportacao != DateTime.MinValue ) 
				_db.AddInParameter(command, "@dataHoraExportacao", DbType.DateTime, entidade.DataHoraExportacao);
			else
				_db.AddInParameter(command, "@dataHoraExportacao", DbType.DateTime, null);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um PedidoControle.
        /// </summary>
        /// <param name="entidade">PedidoControle contendo os dados a serem atualizados.</param>
		public void Atualizar(PedidoControle entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE PedidoControle SET ");
			sbSQL.Append(" dataHoraExportacao=@dataHoraExportacao ");
			sbSQL.Append(" WHERE pedidoId=@pedidoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@pedidoId", DbType.Int32, entidade.PedidoId);
			if (entidade.DataHoraExportacao != null && entidade.DataHoraExportacao != DateTime.MinValue ) 
				_db.AddInParameter(command, "@dataHoraExportacao", DbType.DateTime, entidade.DataHoraExportacao);
			else
				_db.AddInParameter(command, "@dataHoraExportacao", DbType.DateTime, null);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um PedidoControle da base de dados.
        /// </summary>
        /// <param name="entidade">PedidoControle a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(PedidoControle entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM PedidoControle ");
			sbSQL.Append("WHERE pedidoId=@pedidoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@pedidoId", DbType.Int32, entidade.PedidoId);

								
			_db.ExecuteNonQuery(command);
		}
		

		/// <summary>
		/// Método que carrega um PedidoControle.
		/// </summary>
        /// <param name="entidade">PedidoControle a ser carregado (somente o identificador é necessário).</param>
		/// <returns>PedidoControle</returns>
		public PedidoControle Carregar(PedidoControle entidade) {		
		
			PedidoControle entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM PedidoControle WHERE pedidoId=@pedidoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@pedidoId", DbType.Int32, entidade.PedidoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new PedidoControle();
				PopulaPedidoControle(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		
		
		/// <summary>
		/// Método que carrega um PedidoControle com suas dependências.
		/// </summary>
        /// <param name="entidade">PedidoControle a ser carregado (somente o identificador é necessário).</param>
		/// <returns>PedidoControle</returns>
		public PedidoControle CarregarComDependencias(PedidoControle entidade) {		
		
			PedidoControle entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append("SELECT PedidoControle.pedidoId, PedidoControle.dataHoraExportacao");
			sbSQL.Append(", usuarioId, dataHoraPedido, carrinhoId, pedidoStatusId, freteValor, valorPedido, pagamentoId, transportadoraServicoId, pedidoCodigo");
			sbSQL.Append(" FROM PedidoControle");
			sbSQL.Append(" INNER JOIN Pedido ON PedidoControle.pedidoId=Pedido.pedidoId");
			sbSQL.Append(" WHERE PedidoControle.pedidoId=@pedidoId");
						
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@pedidoId", DbType.Int32, entidade.PedidoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new PedidoControle();
				PopulaPedidoControle(reader, entidadeRetorno);
				entidadeRetorno.Pedido = new Pedido();
				PedidoADO.PopulaPedido(reader, entidadeRetorno.Pedido);
			}
			reader.Close();
			
			return entidadeRetorno;
		}		

		
		
		/// <summary>
        /// Método que retorna uma coleção de PedidoControle.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos PedidoControle.</returns>
		public IEnumerable<PedidoControle> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<PedidoControle> entidadesRetorno = new List<PedidoControle>();
			
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
				sbOrder.Append( " ORDER BY pedidoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM PedidoControle");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM PedidoControle WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM PedidoControle ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT PedidoControle.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM PedidoControle ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT PedidoControle.* FROM PedidoControle ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                PedidoControle entidadeRetorno = new PedidoControle();
                PopulaPedidoControle(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os PedidoControle existentes na base de dados.
        /// </summary>
		public IEnumerable<PedidoControle> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de PedidoControle na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de PedidoControle na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM PedidoControle");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um PedidoControle baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">PedidoControle a ser populado(.</param>
		public static void PopulaPedidoControle(IDataReader reader, PedidoControle entidade) 
		{						
			if (reader["dataHoraExportacao"] != DBNull.Value)
				entidade.DataHoraExportacao = Convert.ToDateTime(reader["dataHoraExportacao"].ToString());
			
			if (reader["pedidoId"] != DBNull.Value) {
				entidade.PedidoId = Convert.ToInt32(reader["pedidoId"].ToString());
			}


		}		
		
	}
}
		