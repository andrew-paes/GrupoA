
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
	public partial class PedidoItemCompraConjuntaADO : ADOSuper, IPedidoItemCompraConjuntaDAL {
	
	    /// <summary>
        /// Método que persiste um PedidoItemCompraConjunta.
        /// </summary>
        /// <param name="entidade">PedidoItemCompraConjunta contendo os dados a serem persistidos.</param>	
		public void Inserir(PedidoItemCompraConjunta entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO PedidoItemCompraConjunta ");
			sbSQL.Append(" (pedidoItemCompraConjuntaId, compraConjuntaId) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@pedidoItemCompraConjuntaId, @compraConjuntaId) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@pedidoItemCompraConjuntaId", DbType.Int32, entidade.PedidoItemCompraConjuntaId);

			_db.AddInParameter(command, "@compraConjuntaId", DbType.Int32, entidade.CompraConjunta.CompraConjuntaId);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um PedidoItemCompraConjunta.
        /// </summary>
        /// <param name="entidade">PedidoItemCompraConjunta contendo os dados a serem atualizados.</param>
		public void Atualizar(PedidoItemCompraConjunta entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE PedidoItemCompraConjunta SET ");
			sbSQL.Append(" compraConjuntaId=@compraConjuntaId ");
			sbSQL.Append(" WHERE pedidoItemCompraConjuntaId=@pedidoItemCompraConjuntaId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@pedidoItemCompraConjuntaId", DbType.Int32, entidade.PedidoItemCompraConjuntaId);
			_db.AddInParameter(command, "@compraConjuntaId", DbType.Int32, entidade.CompraConjunta.CompraConjuntaId);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um PedidoItemCompraConjunta da base de dados.
        /// </summary>
        /// <param name="entidade">PedidoItemCompraConjunta a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(PedidoItemCompraConjunta entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM PedidoItemCompraConjunta ");
			sbSQL.Append("WHERE pedidoItemCompraConjuntaId=@pedidoItemCompraConjuntaId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@pedidoItemCompraConjuntaId", DbType.Int32, entidade.PedidoItemCompraConjuntaId);

								
			_db.ExecuteNonQuery(command);
		}
		

		/// <summary>
		/// Método que carrega um PedidoItemCompraConjunta.
		/// </summary>
        /// <param name="entidade">PedidoItemCompraConjunta a ser carregado (somente o identificador é necessário).</param>
		/// <returns>PedidoItemCompraConjunta</returns>
		public PedidoItemCompraConjunta Carregar(PedidoItemCompraConjunta entidade) {		
		
			PedidoItemCompraConjunta entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM PedidoItemCompraConjunta WHERE pedidoItemCompraConjuntaId=@pedidoItemCompraConjuntaId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@pedidoItemCompraConjuntaId", DbType.Int32, entidade.PedidoItemCompraConjuntaId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new PedidoItemCompraConjunta();
				PopulaPedidoItemCompraConjunta(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		
		
		/// <summary>
		/// Método que carrega um PedidoItemCompraConjunta com suas dependências.
		/// </summary>
        /// <param name="entidade">PedidoItemCompraConjunta a ser carregado (somente o identificador é necessário).</param>
		/// <returns>PedidoItemCompraConjunta</returns>
		public PedidoItemCompraConjunta CarregarComDependencias(PedidoItemCompraConjunta entidade) {		
		
			PedidoItemCompraConjunta entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append("SELECT PedidoItemCompraConjunta.pedidoItemCompraConjuntaId, PedidoItemCompraConjunta.compraConjuntaId");
			sbSQL.Append(", pedidoItemId, produtoId, pedidoId, quantidade, valorUnitarioBase, valorUnitarioFinal");
			sbSQL.Append(" FROM PedidoItemCompraConjunta");
			sbSQL.Append(" INNER JOIN PedidoItem ON PedidoItemCompraConjunta.pedidoItemCompraConjuntaId=PedidoItem.pedidoItemId");
			sbSQL.Append(" WHERE PedidoItemCompraConjunta.pedidoItemCompraConjuntaId=@pedidoItemCompraConjuntaId");
						
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@pedidoItemCompraConjuntaId", DbType.Int32, entidade.PedidoItemCompraConjuntaId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new PedidoItemCompraConjunta();
				PopulaPedidoItemCompraConjunta(reader, entidadeRetorno);
				entidadeRetorno.PedidoItem = new PedidoItem();
				PedidoItemADO.PopulaPedidoItem(reader, entidadeRetorno.PedidoItem);
			}
			reader.Close();
			
			return entidadeRetorno;
		}		


		/// <summary>
        /// Método que retorna uma coleção de PedidoItemCompraConjunta.
        /// </summary>
        /// <param name="entidade">CompraConjunta relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de PedidoItemCompraConjunta.</returns>
		public IEnumerable<PedidoItemCompraConjunta> Carregar(CompraConjunta entidade)
		{		
			List<PedidoItemCompraConjunta> entidadesRetorno = new List<PedidoItemCompraConjunta>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT PedidoItemCompraConjunta.* FROM PedidoItemCompraConjunta WHERE PedidoItemCompraConjunta.compraConjuntaId=@compraConjuntaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@compraConjuntaId", DbType.Int32, entidade.CompraConjuntaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                PedidoItemCompraConjunta entidadeRetorno = new PedidoItemCompraConjunta();
                PopulaPedidoItemCompraConjunta(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de PedidoItemCompraConjunta.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos PedidoItemCompraConjunta.</returns>
		public IEnumerable<PedidoItemCompraConjunta> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<PedidoItemCompraConjunta> entidadesRetorno = new List<PedidoItemCompraConjunta>();
			
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
				sbOrder.Append( " ORDER BY pedidoItemCompraConjuntaId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM PedidoItemCompraConjunta");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM PedidoItemCompraConjunta WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM PedidoItemCompraConjunta ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT PedidoItemCompraConjunta.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM PedidoItemCompraConjunta ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT PedidoItemCompraConjunta.* FROM PedidoItemCompraConjunta ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                PedidoItemCompraConjunta entidadeRetorno = new PedidoItemCompraConjunta();
                PopulaPedidoItemCompraConjunta(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os PedidoItemCompraConjunta existentes na base de dados.
        /// </summary>
		public IEnumerable<PedidoItemCompraConjunta> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de PedidoItemCompraConjunta na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de PedidoItemCompraConjunta na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM PedidoItemCompraConjunta");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um PedidoItemCompraConjunta baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">PedidoItemCompraConjunta a ser populado(.</param>
		public static void PopulaPedidoItemCompraConjunta(IDataReader reader, PedidoItemCompraConjunta entidade) 
		{						
			if (reader["pedidoItemCompraConjuntaId"] != DBNull.Value) {
				entidade.PedidoItemCompraConjuntaId = Convert.ToInt32(reader["pedidoItemCompraConjuntaId"].ToString());
			}

			if (reader["compraConjuntaId"] != DBNull.Value) {
				entidade.CompraConjunta = new CompraConjunta();
				entidade.CompraConjunta.CompraConjuntaId = Convert.ToInt32(reader["compraConjuntaId"].ToString());
			}


		}		
		
	}
}
		