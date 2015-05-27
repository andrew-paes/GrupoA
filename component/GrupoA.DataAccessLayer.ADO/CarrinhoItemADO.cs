
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
	public partial class CarrinhoItemADO : ADOSuper, ICarrinhoItemDAL {
	
	    /// <summary>
        /// Método que persiste um CarrinhoItem.
        /// </summary>
        /// <param name="entidade">CarrinhoItem contendo os dados a serem persistidos.</param>	
		public void Inserir(CarrinhoItem entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO CarrinhoItem ");
			sbSQL.Append(" (carrinhoId, produtoId, quantidade) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@carrinhoId, @produtoId, @quantidade) ");											

			sbSQL.Append(" ; SET @carrinhoItemId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@carrinhoItemId", DbType.Int32, 8);

			_db.AddInParameter(command, "@carrinhoId", DbType.Int32, entidade.Carrinho.CarrinhoId);

			_db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.Produto.ProdutoId);

			_db.AddInParameter(command, "@quantidade", DbType.Decimal, entidade.Quantidade);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.CarrinhoItemId = Convert.ToInt32(_db.GetParameterValue(command, "@carrinhoItemId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um CarrinhoItem.
        /// </summary>
        /// <param name="entidade">CarrinhoItem contendo os dados a serem atualizados.</param>
		public void Atualizar(CarrinhoItem entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE CarrinhoItem SET ");
			sbSQL.Append(" carrinhoId=@carrinhoId, produtoId=@produtoId, quantidade=@quantidade ");
			sbSQL.Append(" WHERE carrinhoItemId=@carrinhoItemId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@carrinhoItemId", DbType.Int32, entidade.CarrinhoItemId);
			_db.AddInParameter(command, "@carrinhoId", DbType.Int32, entidade.Carrinho.CarrinhoId);
			_db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.Produto.ProdutoId);
			_db.AddInParameter(command, "@quantidade", DbType.Decimal, entidade.Quantidade);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um CarrinhoItem da base de dados.
        /// </summary>
        /// <param name="entidade">CarrinhoItem a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(CarrinhoItem entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM CarrinhoItem ");
			sbSQL.Append("WHERE carrinhoItemId=@carrinhoItemId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@carrinhoItemId", DbType.Int32, entidade.CarrinhoItemId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um CarrinhoItem.
		/// </summary>
        /// <param name="entidade">CarrinhoItem a ser carregado (somente o identificador é necessário).</param>
		/// <returns>CarrinhoItem</returns>
		public CarrinhoItem Carregar(int carrinhoItemId) {		
			CarrinhoItem entidade = new CarrinhoItem();
			entidade.CarrinhoItemId = carrinhoItemId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um CarrinhoItem.
		/// </summary>
        /// <param name="entidade">CarrinhoItem a ser carregado (somente o identificador é necessário).</param>
		/// <returns>CarrinhoItem</returns>
		public CarrinhoItem Carregar(CarrinhoItem entidade) {		
		
			CarrinhoItem entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM CarrinhoItem WHERE carrinhoItemId=@carrinhoItemId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@carrinhoItemId", DbType.Int32, entidade.CarrinhoItemId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new CarrinhoItem();
				PopulaCarrinhoItem(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de CarrinhoItem.
        /// </summary>
        /// <param name="entidade">Carrinho relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de CarrinhoItem.</returns>
		public IEnumerable<CarrinhoItem> Carregar(Carrinho entidade)
		{		
			List<CarrinhoItem> entidadesRetorno = new List<CarrinhoItem>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT CarrinhoItem.* FROM CarrinhoItem WHERE CarrinhoItem.carrinhoId=@carrinhoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@carrinhoId", DbType.Int32, entidade.CarrinhoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                CarrinhoItem entidadeRetorno = new CarrinhoItem();
                PopulaCarrinhoItem(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de CarrinhoItem.
        /// </summary>
        /// <param name="entidade">Produto relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de CarrinhoItem.</returns>
		public IEnumerable<CarrinhoItem> Carregar(Produto entidade)
		{		
			List<CarrinhoItem> entidadesRetorno = new List<CarrinhoItem>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT CarrinhoItem.* FROM CarrinhoItem WHERE CarrinhoItem.produtoId=@produtoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.ProdutoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                CarrinhoItem entidadeRetorno = new CarrinhoItem();
                PopulaCarrinhoItem(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de CarrinhoItem.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos CarrinhoItem.</returns>
		public IEnumerable<CarrinhoItem> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<CarrinhoItem> entidadesRetorno = new List<CarrinhoItem>();
			
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
				sbOrder.Append( " ORDER BY carrinhoItemId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM CarrinhoItem");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM CarrinhoItem WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM CarrinhoItem ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT CarrinhoItem.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM CarrinhoItem ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT CarrinhoItem.* FROM CarrinhoItem ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                CarrinhoItem entidadeRetorno = new CarrinhoItem();
                PopulaCarrinhoItem(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os CarrinhoItem existentes na base de dados.
        /// </summary>
		public IEnumerable<CarrinhoItem> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de CarrinhoItem na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de CarrinhoItem na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM CarrinhoItem");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um CarrinhoItem baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">CarrinhoItem a ser populado(.</param>
		public static void PopulaCarrinhoItem(IDataReader reader, CarrinhoItem entidade) 
		{						
			if (reader["carrinhoItemId"] != DBNull.Value)
				entidade.CarrinhoItemId = Convert.ToInt32(reader["carrinhoItemId"].ToString());
			
			if (reader["quantidade"] != DBNull.Value)
				entidade.Quantidade = Convert.ToDecimal(reader["quantidade"].ToString());
			
			if (reader["carrinhoId"] != DBNull.Value) {
				entidade.Carrinho = new Carrinho();
				entidade.Carrinho.CarrinhoId = Convert.ToInt32(reader["carrinhoId"].ToString());
			}

			if (reader["produtoId"] != DBNull.Value) {
				entidade.Produto = new Produto();
				entidade.Produto.ProdutoId = Convert.ToInt32(reader["produtoId"].ToString());
			}


		}		
		
	}
}
		