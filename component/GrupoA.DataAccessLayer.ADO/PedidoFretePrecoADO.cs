
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
	public partial class PedidoFretePrecoADO : ADOSuper, IPedidoFretePrecoDAL {
	
	    /// <summary>
        /// Método que persiste um PedidoFretePreco.
        /// </summary>
        /// <param name="entidade">PedidoFretePreco contendo os dados a serem persistidos.</param>	
		public void Inserir(PedidoFretePreco entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO PedidoFretePreco ");
			sbSQL.Append(" (pedidoFretePrecoId, pedidoFreteGrupoId, peso, preco) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@pedidoFretePrecoId, @pedidoFreteGrupoId, @peso, @preco) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@pedidoFretePrecoId", DbType.Int32, entidade.PedidoFretePrecoId);

			_db.AddInParameter(command, "@pedidoFreteGrupoId", DbType.Int32, entidade.PedidoFreteGrupo.PedidoFreteGrupoId);

			_db.AddInParameter(command, "@peso", DbType.Decimal, entidade.Peso);

			_db.AddInParameter(command, "@preco", DbType.Decimal, entidade.Preco);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um PedidoFretePreco.
        /// </summary>
        /// <param name="entidade">PedidoFretePreco contendo os dados a serem atualizados.</param>
		public void Atualizar(PedidoFretePreco entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE PedidoFretePreco SET ");
			sbSQL.Append(" pedidoFreteGrupoId=@pedidoFreteGrupoId, peso=@peso, preco=@preco ");
			sbSQL.Append(" WHERE pedidoFretePrecoId=@pedidoFretePrecoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@pedidoFretePrecoId", DbType.Int32, entidade.PedidoFretePrecoId);
			_db.AddInParameter(command, "@pedidoFreteGrupoId", DbType.Int32, entidade.PedidoFreteGrupo.PedidoFreteGrupoId);
			_db.AddInParameter(command, "@peso", DbType.Decimal, entidade.Peso);
			_db.AddInParameter(command, "@preco", DbType.Decimal, entidade.Preco);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um PedidoFretePreco da base de dados.
        /// </summary>
        /// <param name="entidade">PedidoFretePreco a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(PedidoFretePreco entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM PedidoFretePreco ");
			sbSQL.Append("WHERE pedidoFretePrecoId=@pedidoFretePrecoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@pedidoFretePrecoId", DbType.Int32, entidade.PedidoFretePrecoId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um PedidoFretePreco.
		/// </summary>
        /// <param name="entidade">PedidoFretePreco a ser carregado (somente o identificador é necessário).</param>
		/// <returns>PedidoFretePreco</returns>
		public PedidoFretePreco Carregar(int pedidoFretePrecoId) {		
			PedidoFretePreco entidade = new PedidoFretePreco();
			entidade.PedidoFretePrecoId = pedidoFretePrecoId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um PedidoFretePreco.
		/// </summary>
        /// <param name="entidade">PedidoFretePreco a ser carregado (somente o identificador é necessário).</param>
		/// <returns>PedidoFretePreco</returns>
		public PedidoFretePreco Carregar(PedidoFretePreco entidade) {		
		
			PedidoFretePreco entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM PedidoFretePreco WHERE pedidoFretePrecoId=@pedidoFretePrecoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@pedidoFretePrecoId", DbType.Int32, entidade.PedidoFretePrecoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new PedidoFretePreco();
				PopulaPedidoFretePreco(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de PedidoFretePreco.
        /// </summary>
        /// <param name="entidade">PedidoFreteGrupo relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de PedidoFretePreco.</returns>
		public IEnumerable<PedidoFretePreco> Carregar(PedidoFreteGrupo entidade)
		{		
			List<PedidoFretePreco> entidadesRetorno = new List<PedidoFretePreco>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT PedidoFretePreco.* FROM PedidoFretePreco WHERE PedidoFretePreco.pedidoFreteGrupoId=@pedidoFreteGrupoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@pedidoFreteGrupoId", DbType.Int32, entidade.PedidoFreteGrupoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                PedidoFretePreco entidadeRetorno = new PedidoFretePreco();
                PopulaPedidoFretePreco(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de PedidoFretePreco.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos PedidoFretePreco.</returns>
		public IEnumerable<PedidoFretePreco> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<PedidoFretePreco> entidadesRetorno = new List<PedidoFretePreco>();
			
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
				sbOrder.Append( " ORDER BY pedidoFretePrecoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM PedidoFretePreco");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM PedidoFretePreco WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM PedidoFretePreco ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT PedidoFretePreco.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM PedidoFretePreco ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT PedidoFretePreco.* FROM PedidoFretePreco ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                PedidoFretePreco entidadeRetorno = new PedidoFretePreco();
                PopulaPedidoFretePreco(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os PedidoFretePreco existentes na base de dados.
        /// </summary>
		public IEnumerable<PedidoFretePreco> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de PedidoFretePreco na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de PedidoFretePreco na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM PedidoFretePreco");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um PedidoFretePreco baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">PedidoFretePreco a ser populado(.</param>
		public static void PopulaPedidoFretePreco(IDataReader reader, PedidoFretePreco entidade) 
		{						
			if (reader["pedidoFretePrecoId"] != DBNull.Value)
				entidade.PedidoFretePrecoId = Convert.ToInt32(reader["pedidoFretePrecoId"].ToString());
			
			if (reader["peso"] != DBNull.Value)
				entidade.Peso = Convert.ToDecimal(reader["peso"].ToString());
			
			if (reader["preco"] != DBNull.Value)
				entidade.Preco = float.Parse(reader["preco"].ToString());
			
			if (reader["pedidoFreteGrupoId"] != DBNull.Value) {
				entidade.PedidoFreteGrupo = new PedidoFreteGrupo();
				entidade.PedidoFreteGrupo.PedidoFreteGrupoId = Convert.ToInt32(reader["pedidoFreteGrupoId"].ToString());
			}


		}		
		
	}
}
		