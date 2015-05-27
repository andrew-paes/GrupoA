
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
	public partial class PedidoItemPromocaoADO : ADOSuper, IPedidoItemPromocaoDAL {
	
	    /// <summary>
        /// Método que persiste um PedidoItemPromocao.
        /// </summary>
        /// <param name="entidade">PedidoItemPromocao contendo os dados a serem persistidos.</param>	
		public void Inserir(PedidoItemPromocao entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO PedidoItemPromocao ");
			sbSQL.Append(" (pedidoItemPromocaoId, freteGratis, descontoPercentual, descontoValor, promocaoId) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@pedidoItemPromocaoId, @freteGratis, @descontoPercentual, @descontoValor, @promocaoId) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@pedidoItemPromocaoId", DbType.Int32, entidade.PedidoItemPromocaoId);

			_db.AddInParameter(command, "@freteGratis", DbType.Int32, entidade.FreteGratis);

			if (entidade.DescontoPercentual != null ) 
				_db.AddInParameter(command, "@descontoPercentual", DbType.Decimal, entidade.DescontoPercentual);
			else
				_db.AddInParameter(command, "@descontoPercentual", DbType.Decimal, null);

			if (entidade.DescontoValor != null ) 
				_db.AddInParameter(command, "@descontoValor", DbType.Decimal, entidade.DescontoValor);
			else
				_db.AddInParameter(command, "@descontoValor", DbType.Decimal, null);

			_db.AddInParameter(command, "@promocaoId", DbType.Int32, entidade.Promocao.PromocaoId);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um PedidoItemPromocao.
        /// </summary>
        /// <param name="entidade">PedidoItemPromocao contendo os dados a serem atualizados.</param>
		public void Atualizar(PedidoItemPromocao entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE PedidoItemPromocao SET ");
			sbSQL.Append(" freteGratis=@freteGratis, descontoPercentual=@descontoPercentual, descontoValor=@descontoValor, promocaoId=@promocaoId ");
			sbSQL.Append(" WHERE pedidoItemPromocaoId=@pedidoItemPromocaoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@pedidoItemPromocaoId", DbType.Int32, entidade.PedidoItemPromocaoId);
			_db.AddInParameter(command, "@freteGratis", DbType.Int32, entidade.FreteGratis);
			if (entidade.DescontoPercentual != null ) 
				_db.AddInParameter(command, "@descontoPercentual", DbType.Decimal, entidade.DescontoPercentual);
			else
				_db.AddInParameter(command, "@descontoPercentual", DbType.Decimal, null);
			if (entidade.DescontoValor != null ) 
				_db.AddInParameter(command, "@descontoValor", DbType.Decimal, entidade.DescontoValor);
			else
				_db.AddInParameter(command, "@descontoValor", DbType.Decimal, null);
			_db.AddInParameter(command, "@promocaoId", DbType.Int32, entidade.Promocao.PromocaoId);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um PedidoItemPromocao da base de dados.
        /// </summary>
        /// <param name="entidade">PedidoItemPromocao a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(PedidoItemPromocao entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM PedidoItemPromocao ");
			sbSQL.Append("WHERE pedidoItemPromocaoId=@pedidoItemPromocaoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@pedidoItemPromocaoId", DbType.Int32, entidade.PedidoItemPromocaoId);

								
			_db.ExecuteNonQuery(command);
		}
		

		/// <summary>
		/// Método que carrega um PedidoItemPromocao.
		/// </summary>
        /// <param name="entidade">PedidoItemPromocao a ser carregado (somente o identificador é necessário).</param>
		/// <returns>PedidoItemPromocao</returns>
		public PedidoItemPromocao Carregar(PedidoItemPromocao entidade) {		
		
			PedidoItemPromocao entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM PedidoItemPromocao WHERE pedidoItemPromocaoId=@pedidoItemPromocaoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@pedidoItemPromocaoId", DbType.Int32, entidade.PedidoItemPromocaoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new PedidoItemPromocao();
				PopulaPedidoItemPromocao(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		
		
		/// <summary>
		/// Método que carrega um PedidoItemPromocao com suas dependências.
		/// </summary>
        /// <param name="entidade">PedidoItemPromocao a ser carregado (somente o identificador é necessário).</param>
		/// <returns>PedidoItemPromocao</returns>
		public PedidoItemPromocao CarregarComDependencias(PedidoItemPromocao entidade) {		
		
			PedidoItemPromocao entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append("SELECT PedidoItemPromocao.pedidoItemPromocaoId, PedidoItemPromocao.freteGratis, PedidoItemPromocao.descontoPercentual, PedidoItemPromocao.descontoValor, PedidoItemPromocao.promocaoId");
			sbSQL.Append(", pedidoItemId, produtoId, pedidoId, quantidade, valorUnitarioBase, valorUnitarioFinal");
			sbSQL.Append(" FROM PedidoItemPromocao");
			sbSQL.Append(" INNER JOIN PedidoItem ON PedidoItemPromocao.pedidoItemPromocaoId=PedidoItem.pedidoItemId");
			sbSQL.Append(" WHERE PedidoItemPromocao.pedidoItemPromocaoId=@pedidoItemPromocaoId");
						
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@pedidoItemPromocaoId", DbType.Int32, entidade.PedidoItemPromocaoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new PedidoItemPromocao();
				PopulaPedidoItemPromocao(reader, entidadeRetorno);
				entidadeRetorno.PedidoItem = new PedidoItem();
				PedidoItemADO.PopulaPedidoItem(reader, entidadeRetorno.PedidoItem);
			}
			reader.Close();
			
			return entidadeRetorno;
		}		


		/// <summary>
        /// Método que retorna uma coleção de PedidoItemPromocao.
        /// </summary>
        /// <param name="entidade">Promocao relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de PedidoItemPromocao.</returns>
		public IEnumerable<PedidoItemPromocao> Carregar(Promocao entidade)
		{		
			List<PedidoItemPromocao> entidadesRetorno = new List<PedidoItemPromocao>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT PedidoItemPromocao.* FROM PedidoItemPromocao WHERE PedidoItemPromocao.promocaoId=@promocaoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@promocaoId", DbType.Int32, entidade.PromocaoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                PedidoItemPromocao entidadeRetorno = new PedidoItemPromocao();
                PopulaPedidoItemPromocao(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de PedidoItemPromocao.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos PedidoItemPromocao.</returns>
		public IEnumerable<PedidoItemPromocao> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<PedidoItemPromocao> entidadesRetorno = new List<PedidoItemPromocao>();
			
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
				sbOrder.Append( " ORDER BY pedidoItemPromocaoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM PedidoItemPromocao");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM PedidoItemPromocao WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM PedidoItemPromocao ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT PedidoItemPromocao.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM PedidoItemPromocao ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT PedidoItemPromocao.* FROM PedidoItemPromocao ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                PedidoItemPromocao entidadeRetorno = new PedidoItemPromocao();
                PopulaPedidoItemPromocao(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os PedidoItemPromocao existentes na base de dados.
        /// </summary>
		public IEnumerable<PedidoItemPromocao> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de PedidoItemPromocao na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de PedidoItemPromocao na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM PedidoItemPromocao");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um PedidoItemPromocao baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">PedidoItemPromocao a ser populado(.</param>
		public static void PopulaPedidoItemPromocao(IDataReader reader, PedidoItemPromocao entidade) 
		{						
			if (reader["freteGratis"] != DBNull.Value)
				entidade.FreteGratis = Convert.ToBoolean(reader["freteGratis"].ToString());
			
			if (reader["descontoPercentual"] != DBNull.Value)
				entidade.DescontoPercentual = Convert.ToDecimal(reader["descontoPercentual"].ToString());
			
			if (reader["descontoValor"] != DBNull.Value)
				entidade.DescontoValor = Convert.ToDecimal(reader["descontoValor"].ToString());
			
			if (reader["pedidoItemPromocaoId"] != DBNull.Value) {
				entidade.PedidoItemPromocaoId = Convert.ToInt32(reader["pedidoItemPromocaoId"].ToString());
			}

			if (reader["promocaoId"] != DBNull.Value) {
				entidade.Promocao = new Promocao();
				entidade.Promocao.PromocaoId = Convert.ToInt32(reader["promocaoId"].ToString());
			}


		}		
		
	}
}
		