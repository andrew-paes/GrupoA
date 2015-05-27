
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
	public partial class PedidoCompraConjuntaADO : ADOSuper, IPedidoCompraConjuntaDAL {
	
	    /// <summary>
        /// Método que persiste um PedidoCompraConjunta.
        /// </summary>
        /// <param name="entidade">PedidoCompraConjunta contendo os dados a serem persistidos.</param>	
		public void Inserir(PedidoCompraConjunta entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO PedidoCompraConjunta ");
			sbSQL.Append(" (pedidoCompraConjuntaId, compraConjuntaDescontoId, dataHoraNotificacaoFinalizacao, compraConjuntaId, fechamentoSincronizado, tokenCofre, numeroTentativa) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@pedidoCompraConjuntaId, @compraConjuntaDescontoId, @dataHoraNotificacaoFinalizacao, @compraConjuntaId, @fechamentoSincronizado, @tokenCofre, @numeroTentativa) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@pedidoCompraConjuntaId", DbType.Int32, entidade.PedidoCompraConjuntaId);

			if (entidade.CompraConjuntaDesconto != null ) 
				_db.AddInParameter(command, "@compraConjuntaDescontoId", DbType.Int32, entidade.CompraConjuntaDesconto.CompraConjuntaDescontoId);
			else
				_db.AddInParameter(command, "@compraConjuntaDescontoId", DbType.Int32, null);

			if (entidade.DataHoraNotificacaoFinalizacao != null && entidade.DataHoraNotificacaoFinalizacao != DateTime.MinValue ) 
				_db.AddInParameter(command, "@dataHoraNotificacaoFinalizacao", DbType.DateTime, entidade.DataHoraNotificacaoFinalizacao);
			else
				_db.AddInParameter(command, "@dataHoraNotificacaoFinalizacao", DbType.DateTime, null);

			_db.AddInParameter(command, "@compraConjuntaId", DbType.Int32, entidade.CompraConjunta.CompraConjuntaId);

			_db.AddInParameter(command, "@fechamentoSincronizado", DbType.Int32, entidade.FechamentoSincronizado);

			_db.AddInParameter(command, "@tokenCofre", DbType.String, entidade.TokenCofre);

			_db.AddInParameter(command, "@numeroTentativa", DbType.Int32, entidade.NumeroTentativa);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um PedidoCompraConjunta.
        /// </summary>
        /// <param name="entidade">PedidoCompraConjunta contendo os dados a serem atualizados.</param>
		public void Atualizar(PedidoCompraConjunta entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE PedidoCompraConjunta SET ");
			sbSQL.Append(" compraConjuntaDescontoId=@compraConjuntaDescontoId, dataHoraNotificacaoFinalizacao=@dataHoraNotificacaoFinalizacao, compraConjuntaId=@compraConjuntaId, fechamentoSincronizado=@fechamentoSincronizado, tokenCofre=@tokenCofre, numeroTentativa=@numeroTentativa ");
			sbSQL.Append(" WHERE pedidoCompraConjuntaId=@pedidoCompraConjuntaId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@pedidoCompraConjuntaId", DbType.Int32, entidade.PedidoCompraConjuntaId);
			if (entidade.CompraConjuntaDesconto != null ) 
				_db.AddInParameter(command, "@compraConjuntaDescontoId", DbType.Int32, entidade.CompraConjuntaDesconto.CompraConjuntaDescontoId);
			else
				_db.AddInParameter(command, "@compraConjuntaDescontoId", DbType.Int32, null);
			if (entidade.DataHoraNotificacaoFinalizacao != null && entidade.DataHoraNotificacaoFinalizacao != DateTime.MinValue ) 
				_db.AddInParameter(command, "@dataHoraNotificacaoFinalizacao", DbType.DateTime, entidade.DataHoraNotificacaoFinalizacao);
			else
				_db.AddInParameter(command, "@dataHoraNotificacaoFinalizacao", DbType.DateTime, null);
			_db.AddInParameter(command, "@compraConjuntaId", DbType.Int32, entidade.CompraConjunta.CompraConjuntaId);
			_db.AddInParameter(command, "@fechamentoSincronizado", DbType.Int32, entidade.FechamentoSincronizado);
			_db.AddInParameter(command, "@tokenCofre", DbType.String, entidade.TokenCofre);
			_db.AddInParameter(command, "@numeroTentativa", DbType.Int32, entidade.NumeroTentativa);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um PedidoCompraConjunta da base de dados.
        /// </summary>
        /// <param name="entidade">PedidoCompraConjunta a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(PedidoCompraConjunta entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM PedidoCompraConjunta ");
			sbSQL.Append("WHERE pedidoCompraConjuntaId=@pedidoCompraConjuntaId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@pedidoCompraConjuntaId", DbType.Int32, entidade.PedidoCompraConjuntaId);

								
			_db.ExecuteNonQuery(command);
		}
		

		/// <summary>
		/// Método que carrega um PedidoCompraConjunta.
		/// </summary>
        /// <param name="entidade">PedidoCompraConjunta a ser carregado (somente o identificador é necessário).</param>
		/// <returns>PedidoCompraConjunta</returns>
		public PedidoCompraConjunta Carregar(PedidoCompraConjunta entidade) {		
		
			PedidoCompraConjunta entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM PedidoCompraConjunta WHERE pedidoCompraConjuntaId=@pedidoCompraConjuntaId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@pedidoCompraConjuntaId", DbType.Int32, entidade.PedidoCompraConjuntaId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new PedidoCompraConjunta();
				PopulaPedidoCompraConjunta(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		
		
		/// <summary>
		/// Método que carrega um PedidoCompraConjunta com suas dependências.
		/// </summary>
        /// <param name="entidade">PedidoCompraConjunta a ser carregado (somente o identificador é necessário).</param>
		/// <returns>PedidoCompraConjunta</returns>
		public PedidoCompraConjunta CarregarComDependencias(PedidoCompraConjunta entidade) {		
		
			PedidoCompraConjunta entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append("SELECT PedidoCompraConjunta.pedidoCompraConjuntaId, PedidoCompraConjunta.compraConjuntaDescontoId, PedidoCompraConjunta.dataHoraNotificacaoFinalizacao, PedidoCompraConjunta.compraConjuntaId, PedidoCompraConjunta.fechamentoSincronizado, PedidoCompraConjunta.tokenCofre, PedidoCompraConjunta.numeroTentativa");
			sbSQL.Append(", pedidoId, usuarioId, dataHoraPedido, carrinhoId, pedidoStatusId, freteValor, valorPedido, pagamentoId, transportadoraServicoId, pedidoCodigo");
			sbSQL.Append(" FROM PedidoCompraConjunta");
			sbSQL.Append(" INNER JOIN Pedido ON PedidoCompraConjunta.pedidoCompraConjuntaId=Pedido.pedidoId");
			sbSQL.Append(" WHERE PedidoCompraConjunta.pedidoCompraConjuntaId=@pedidoCompraConjuntaId");
						
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@pedidoCompraConjuntaId", DbType.Int32, entidade.PedidoCompraConjuntaId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new PedidoCompraConjunta();
				PopulaPedidoCompraConjunta(reader, entidadeRetorno);
				entidadeRetorno.Pedido = new Pedido();
				PedidoADO.PopulaPedido(reader, entidadeRetorno.Pedido);
			}
			reader.Close();
			
			return entidadeRetorno;
		}		


		/// <summary>
        /// Método que retorna uma coleção de PedidoCompraConjunta.
        /// </summary>
        /// <param name="entidade">CompraConjunta relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de PedidoCompraConjunta.</returns>
		public IEnumerable<PedidoCompraConjunta> Carregar(CompraConjunta entidade)
		{		
			List<PedidoCompraConjunta> entidadesRetorno = new List<PedidoCompraConjunta>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT PedidoCompraConjunta.* FROM PedidoCompraConjunta WHERE PedidoCompraConjunta.compraConjuntaId=@compraConjuntaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@compraConjuntaId", DbType.Int32, entidade.CompraConjuntaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                PedidoCompraConjunta entidadeRetorno = new PedidoCompraConjunta();
                PopulaPedidoCompraConjunta(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de PedidoCompraConjunta.
        /// </summary>
        /// <param name="entidade">CompraConjuntaDesconto relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de PedidoCompraConjunta.</returns>
		public IEnumerable<PedidoCompraConjunta> Carregar(CompraConjuntaDesconto entidade)
		{		
			List<PedidoCompraConjunta> entidadesRetorno = new List<PedidoCompraConjunta>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT PedidoCompraConjunta.* FROM PedidoCompraConjunta WHERE PedidoCompraConjunta.compraConjuntaDescontoId=@compraConjuntaDescontoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@compraConjuntaDescontoId", DbType.Int32, entidade.CompraConjuntaDescontoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                PedidoCompraConjunta entidadeRetorno = new PedidoCompraConjunta();
                PopulaPedidoCompraConjunta(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de PedidoCompraConjunta.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos PedidoCompraConjunta.</returns>
		public IEnumerable<PedidoCompraConjunta> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<PedidoCompraConjunta> entidadesRetorno = new List<PedidoCompraConjunta>();
			
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
				sbOrder.Append( " ORDER BY pedidoCompraConjuntaId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM PedidoCompraConjunta");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM PedidoCompraConjunta WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM PedidoCompraConjunta ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT PedidoCompraConjunta.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM PedidoCompraConjunta ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT PedidoCompraConjunta.* FROM PedidoCompraConjunta ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                PedidoCompraConjunta entidadeRetorno = new PedidoCompraConjunta();
                PopulaPedidoCompraConjunta(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os PedidoCompraConjunta existentes na base de dados.
        /// </summary>
		public IEnumerable<PedidoCompraConjunta> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de PedidoCompraConjunta na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de PedidoCompraConjunta na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM PedidoCompraConjunta");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um PedidoCompraConjunta baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">PedidoCompraConjunta a ser populado(.</param>
		public static void PopulaPedidoCompraConjunta(IDataReader reader, PedidoCompraConjunta entidade) 
		{						
			if (reader["dataHoraNotificacaoFinalizacao"] != DBNull.Value)
				entidade.DataHoraNotificacaoFinalizacao = Convert.ToDateTime(reader["dataHoraNotificacaoFinalizacao"].ToString());
			
			if (reader["fechamentoSincronizado"] != DBNull.Value)
				entidade.FechamentoSincronizado = Convert.ToBoolean(reader["fechamentoSincronizado"].ToString());
			
			if (reader["tokenCofre"] != DBNull.Value)
				entidade.TokenCofre = reader["tokenCofre"].ToString();
			
			if (reader["numeroTentativa"] != DBNull.Value)
				entidade.NumeroTentativa = Convert.ToInt32(reader["numeroTentativa"].ToString());
			
			if (reader["pedidoCompraConjuntaId"] != DBNull.Value) {
				entidade.PedidoCompraConjuntaId = Convert.ToInt32(reader["pedidoCompraConjuntaId"].ToString());
			}

			if (reader["compraConjuntaDescontoId"] != DBNull.Value) {
				entidade.CompraConjuntaDesconto = new CompraConjuntaDesconto();
				entidade.CompraConjuntaDesconto.CompraConjuntaDescontoId = Convert.ToInt32(reader["compraConjuntaDescontoId"].ToString());
			}

			if (reader["compraConjuntaId"] != DBNull.Value) {
				entidade.CompraConjunta = new CompraConjunta();
				entidade.CompraConjunta.CompraConjuntaId = Convert.ToInt32(reader["compraConjuntaId"].ToString());
			}


		}		
		
	}
}
		