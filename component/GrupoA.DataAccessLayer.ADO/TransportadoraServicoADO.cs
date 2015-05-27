
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
	public partial class TransportadoraServicoADO : ADOSuper, ITransportadoraServicoDAL {
	
	    /// <summary>
        /// Método que persiste um TransportadoraServico.
        /// </summary>
        /// <param name="entidade">TransportadoraServico contendo os dados a serem persistidos.</param>	
		public void Inserir(TransportadoraServico entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO TransportadoraServico ");
			sbSQL.Append(" (transportadoraId, nomeServicoe, ativo) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@transportadoraId, @nomeServicoe, @ativo) ");											

			sbSQL.Append(" ; SET @transportadoraServicoId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@transportadoraServicoId", DbType.Int32, 8);

			_db.AddInParameter(command, "@transportadoraId", DbType.Int32, entidade.Transportadora.TransportadoraId);

			_db.AddInParameter(command, "@nomeServicoe", DbType.String, entidade.NomeServicoe);

			_db.AddInParameter(command, "@ativo", DbType.Int32, entidade.Ativo);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.TransportadoraServicoId = Convert.ToInt32(_db.GetParameterValue(command, "@transportadoraServicoId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um TransportadoraServico.
        /// </summary>
        /// <param name="entidade">TransportadoraServico contendo os dados a serem atualizados.</param>
		public void Atualizar(TransportadoraServico entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE TransportadoraServico SET ");
			sbSQL.Append(" transportadoraId=@transportadoraId, nomeServicoe=@nomeServicoe, ativo=@ativo ");
			sbSQL.Append(" WHERE transportadoraServicoId=@transportadoraServicoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@transportadoraServicoId", DbType.Int32, entidade.TransportadoraServicoId);
			_db.AddInParameter(command, "@transportadoraId", DbType.Int32, entidade.Transportadora.TransportadoraId);
			_db.AddInParameter(command, "@nomeServicoe", DbType.String, entidade.NomeServicoe);
			_db.AddInParameter(command, "@ativo", DbType.Int32, entidade.Ativo);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um TransportadoraServico da base de dados.
        /// </summary>
        /// <param name="entidade">TransportadoraServico a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(TransportadoraServico entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM TransportadoraServico ");
			sbSQL.Append("WHERE transportadoraServicoId=@transportadoraServicoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@transportadoraServicoId", DbType.Int32, entidade.TransportadoraServicoId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um TransportadoraServico.
		/// </summary>
        /// <param name="entidade">TransportadoraServico a ser carregado (somente o identificador é necessário).</param>
		/// <returns>TransportadoraServico</returns>
		public TransportadoraServico Carregar(int transportadoraServicoId) {		
			TransportadoraServico entidade = new TransportadoraServico();
			entidade.TransportadoraServicoId = transportadoraServicoId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um TransportadoraServico.
		/// </summary>
        /// <param name="entidade">TransportadoraServico a ser carregado (somente o identificador é necessário).</param>
		/// <returns>TransportadoraServico</returns>
		public TransportadoraServico Carregar(TransportadoraServico entidade) {		
		
			TransportadoraServico entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM TransportadoraServico WHERE transportadoraServicoId=@transportadoraServicoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@transportadoraServicoId", DbType.Int32, entidade.TransportadoraServicoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new TransportadoraServico();
				PopulaTransportadoraServico(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de TransportadoraServico.
        /// </summary>
        /// <param name="entidade">Pedido relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de TransportadoraServico.</returns>
		public IEnumerable<TransportadoraServico> Carregar(Pedido entidade)
		{		
			List<TransportadoraServico> entidadesRetorno = new List<TransportadoraServico>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT TransportadoraServico.* FROM TransportadoraServico INNER JOIN Pedido ON TransportadoraServico.transportadoraServicoId=Pedido.transportadoraServicoId WHERE Pedido.pedidoId=@pedidoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@pedidoId", DbType.Int32, entidade.PedidoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                TransportadoraServico entidadeRetorno = new TransportadoraServico();
                PopulaTransportadoraServico(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de TransportadoraServico.
        /// </summary>
        /// <param name="entidade">Transportadora relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de TransportadoraServico.</returns>
		public IEnumerable<TransportadoraServico> Carregar(Transportadora entidade)
		{		
			List<TransportadoraServico> entidadesRetorno = new List<TransportadoraServico>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT TransportadoraServico.* FROM TransportadoraServico WHERE TransportadoraServico.transportadoraId=@transportadoraId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@transportadoraId", DbType.Int32, entidade.TransportadoraId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                TransportadoraServico entidadeRetorno = new TransportadoraServico();
                PopulaTransportadoraServico(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de TransportadoraServico.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos TransportadoraServico.</returns>
		public IEnumerable<TransportadoraServico> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<TransportadoraServico> entidadesRetorno = new List<TransportadoraServico>();
			
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
				sbOrder.Append( " ORDER BY transportadoraServicoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM TransportadoraServico");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM TransportadoraServico WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM TransportadoraServico ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT TransportadoraServico.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM TransportadoraServico ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT TransportadoraServico.* FROM TransportadoraServico ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                TransportadoraServico entidadeRetorno = new TransportadoraServico();
                PopulaTransportadoraServico(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os TransportadoraServico existentes na base de dados.
        /// </summary>
		public IEnumerable<TransportadoraServico> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de TransportadoraServico na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de TransportadoraServico na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM TransportadoraServico");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um TransportadoraServico baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">TransportadoraServico a ser populado(.</param>
		public static void PopulaTransportadoraServico(IDataReader reader, TransportadoraServico entidade) 
		{						
			if (reader["transportadoraServicoId"] != DBNull.Value)
				entidade.TransportadoraServicoId = Convert.ToInt32(reader["transportadoraServicoId"].ToString());
			
			if (reader["nomeServicoe"] != DBNull.Value)
				entidade.NomeServicoe = reader["nomeServicoe"].ToString();
			
			if (reader["ativo"] != DBNull.Value)
				entidade.Ativo = Convert.ToBoolean(reader["ativo"].ToString());
			
			if (reader["transportadoraId"] != DBNull.Value) {
				entidade.Transportadora = new Transportadora();
				entidade.Transportadora.TransportadoraId = Convert.ToInt32(reader["transportadoraId"].ToString());
			}


		}		
		
	}
}
		