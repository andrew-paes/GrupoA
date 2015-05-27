
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
	public partial class PedidoSituacaoADO : ADOSuper, IPedidoSituacaoDAL {
	
	    /// <summary>
        /// Método que persiste um PedidoSituacao.
        /// </summary>
        /// <param name="entidade">PedidoSituacao contendo os dados a serem persistidos.</param>	
		public void Inserir(PedidoSituacao entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO PedidoSituacao ");
			sbSQL.Append(" (pedidoId, pedidoStatusId, dataHoraAlteracao) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@pedidoId, @pedidoStatusId, @dataHoraAlteracao) ");											

			sbSQL.Append(" ; SET @pedidoSituacaoId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@pedidoSituacaoId", DbType.Int32, 8);

			_db.AddInParameter(command, "@pedidoId", DbType.Int32, entidade.Pedido.PedidoId);

			_db.AddInParameter(command, "@pedidoStatusId", DbType.Int32, entidade.PedidoStatus.PedidoStatusId);

			_db.AddInParameter(command, "@dataHoraAlteracao", DbType.DateTime, entidade.DataHoraAlteracao);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.PedidoSituacaoId = Convert.ToInt32(_db.GetParameterValue(command, "@pedidoSituacaoId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um PedidoSituacao.
        /// </summary>
        /// <param name="entidade">PedidoSituacao contendo os dados a serem atualizados.</param>
		public void Atualizar(PedidoSituacao entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE PedidoSituacao SET ");
			sbSQL.Append(" pedidoId=@pedidoId, pedidoStatusId=@pedidoStatusId, dataHoraAlteracao=@dataHoraAlteracao ");
			sbSQL.Append(" WHERE pedidoSituacaoId=@pedidoSituacaoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@pedidoSituacaoId", DbType.Int32, entidade.PedidoSituacaoId);
			_db.AddInParameter(command, "@pedidoId", DbType.Int32, entidade.Pedido.PedidoId);
			_db.AddInParameter(command, "@pedidoStatusId", DbType.Int32, entidade.PedidoStatus.PedidoStatusId);
			_db.AddInParameter(command, "@dataHoraAlteracao", DbType.DateTime, entidade.DataHoraAlteracao);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um PedidoSituacao da base de dados.
        /// </summary>
        /// <param name="entidade">PedidoSituacao a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(PedidoSituacao entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM PedidoSituacao ");
			sbSQL.Append("WHERE pedidoSituacaoId=@pedidoSituacaoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@pedidoSituacaoId", DbType.Int32, entidade.PedidoSituacaoId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um PedidoSituacao.
		/// </summary>
        /// <param name="entidade">PedidoSituacao a ser carregado (somente o identificador é necessário).</param>
		/// <returns>PedidoSituacao</returns>
		public PedidoSituacao Carregar(int pedidoSituacaoId) {		
			PedidoSituacao entidade = new PedidoSituacao();
			entidade.PedidoSituacaoId = pedidoSituacaoId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um PedidoSituacao.
		/// </summary>
        /// <param name="entidade">PedidoSituacao a ser carregado (somente o identificador é necessário).</param>
		/// <returns>PedidoSituacao</returns>
		public PedidoSituacao Carregar(PedidoSituacao entidade) {		
		
			PedidoSituacao entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM PedidoSituacao WHERE pedidoSituacaoId=@pedidoSituacaoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@pedidoSituacaoId", DbType.Int32, entidade.PedidoSituacaoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new PedidoSituacao();
				PopulaPedidoSituacao(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de PedidoSituacao.
        /// </summary>
        /// <param name="entidade">Pedido relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de PedidoSituacao.</returns>
		public IEnumerable<PedidoSituacao> Carregar(Pedido entidade)
		{		
			List<PedidoSituacao> entidadesRetorno = new List<PedidoSituacao>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT PedidoSituacao.* FROM PedidoSituacao WHERE PedidoSituacao.pedidoId=@pedidoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@pedidoId", DbType.Int32, entidade.PedidoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                PedidoSituacao entidadeRetorno = new PedidoSituacao();
                PopulaPedidoSituacao(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de PedidoSituacao.
        /// </summary>
        /// <param name="entidade">PedidoStatus relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de PedidoSituacao.</returns>
		public IEnumerable<PedidoSituacao> Carregar(PedidoStatus entidade)
		{		
			List<PedidoSituacao> entidadesRetorno = new List<PedidoSituacao>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT PedidoSituacao.* FROM PedidoSituacao WHERE PedidoSituacao.pedidoStatusId=@pedidoStatusId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@pedidoStatusId", DbType.Int32, entidade.PedidoStatusId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                PedidoSituacao entidadeRetorno = new PedidoSituacao();
                PopulaPedidoSituacao(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de PedidoSituacao.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos PedidoSituacao.</returns>
		public IEnumerable<PedidoSituacao> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<PedidoSituacao> entidadesRetorno = new List<PedidoSituacao>();
			
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
				sbOrder.Append( " ORDER BY pedidoSituacaoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM PedidoSituacao");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM PedidoSituacao WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM PedidoSituacao ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT PedidoSituacao.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM PedidoSituacao ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT PedidoSituacao.* FROM PedidoSituacao ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                PedidoSituacao entidadeRetorno = new PedidoSituacao();
                PopulaPedidoSituacao(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os PedidoSituacao existentes na base de dados.
        /// </summary>
		public IEnumerable<PedidoSituacao> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de PedidoSituacao na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de PedidoSituacao na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM PedidoSituacao");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um PedidoSituacao baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">PedidoSituacao a ser populado(.</param>
		public static void PopulaPedidoSituacao(IDataReader reader, PedidoSituacao entidade) 
		{						
			if (reader["pedidoSituacaoId"] != DBNull.Value)
				entidade.PedidoSituacaoId = Convert.ToInt32(reader["pedidoSituacaoId"].ToString());
			
			if (reader["dataHoraAlteracao"] != DBNull.Value)
				entidade.DataHoraAlteracao = Convert.ToDateTime(reader["dataHoraAlteracao"].ToString());
			
			if (reader["pedidoId"] != DBNull.Value) {
				entidade.Pedido = new Pedido();
				entidade.Pedido.PedidoId = Convert.ToInt32(reader["pedidoId"].ToString());
			}

			if (reader["pedidoStatusId"] != DBNull.Value) {
				entidade.PedidoStatus = new PedidoStatus();
				entidade.PedidoStatus.PedidoStatusId = Convert.ToInt32(reader["pedidoStatusId"].ToString());
			}


		}		
		
	}
}
		