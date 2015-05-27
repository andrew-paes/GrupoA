
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
	public partial class CarrinhoStatusADO : ADOSuper, ICarrinhoStatusDAL {
	
	    /// <summary>
        /// Método que persiste um CarrinhoStatus.
        /// </summary>
        /// <param name="entidade">CarrinhoStatus contendo os dados a serem persistidos.</param>	
		public void Inserir(CarrinhoStatus entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO CarrinhoStatus ");
			sbSQL.Append(" (carrinhoStatusId, statusCarrinho) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@carrinhoStatusId, @statusCarrinho) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@carrinhoStatusId", DbType.Int32, entidade.CarrinhoStatusId);

			_db.AddInParameter(command, "@statusCarrinho", DbType.String, entidade.StatusCarrinho);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um CarrinhoStatus.
        /// </summary>
        /// <param name="entidade">CarrinhoStatus contendo os dados a serem atualizados.</param>
		public void Atualizar(CarrinhoStatus entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE CarrinhoStatus SET ");
			sbSQL.Append(" statusCarrinho=@statusCarrinho ");
			sbSQL.Append(" WHERE carrinhoStatusId=@carrinhoStatusId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@carrinhoStatusId", DbType.Int32, entidade.CarrinhoStatusId);
			_db.AddInParameter(command, "@statusCarrinho", DbType.String, entidade.StatusCarrinho);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um CarrinhoStatus da base de dados.
        /// </summary>
        /// <param name="entidade">CarrinhoStatus a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(CarrinhoStatus entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM CarrinhoStatus ");
			sbSQL.Append("WHERE carrinhoStatusId=@carrinhoStatusId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@carrinhoStatusId", DbType.Int32, entidade.CarrinhoStatusId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um CarrinhoStatus.
		/// </summary>
        /// <param name="entidade">CarrinhoStatus a ser carregado (somente o identificador é necessário).</param>
		/// <returns>CarrinhoStatus</returns>
		public CarrinhoStatus Carregar(int carrinhoStatusId) {		
			CarrinhoStatus entidade = new CarrinhoStatus();
			entidade.CarrinhoStatusId = carrinhoStatusId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um CarrinhoStatus.
		/// </summary>
        /// <param name="entidade">CarrinhoStatus a ser carregado (somente o identificador é necessário).</param>
		/// <returns>CarrinhoStatus</returns>
		public CarrinhoStatus Carregar(CarrinhoStatus entidade) {		
		
			CarrinhoStatus entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM CarrinhoStatus WHERE carrinhoStatusId=@carrinhoStatusId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@carrinhoStatusId", DbType.Int32, entidade.CarrinhoStatusId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new CarrinhoStatus();
				PopulaCarrinhoStatus(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de CarrinhoStatus.
        /// </summary>
        /// <param name="entidade">Carrinho relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de CarrinhoStatus.</returns>
		public IEnumerable<CarrinhoStatus> Carregar(Carrinho entidade)
		{		
			List<CarrinhoStatus> entidadesRetorno = new List<CarrinhoStatus>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT CarrinhoStatus.* FROM CarrinhoStatus INNER JOIN Carrinho ON CarrinhoStatus.carrinhoStatusId=Carrinho.carrinhoStatusId WHERE Carrinho.carrinhoId=@carrinhoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@carrinhoId", DbType.Int32, entidade.CarrinhoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                CarrinhoStatus entidadeRetorno = new CarrinhoStatus();
                PopulaCarrinhoStatus(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de CarrinhoStatus.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos CarrinhoStatus.</returns>
		public IEnumerable<CarrinhoStatus> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<CarrinhoStatus> entidadesRetorno = new List<CarrinhoStatus>();
			
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
				sbOrder.Append( " ORDER BY carrinhoStatusId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM CarrinhoStatus");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM CarrinhoStatus WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM CarrinhoStatus ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT CarrinhoStatus.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM CarrinhoStatus ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT CarrinhoStatus.* FROM CarrinhoStatus ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                CarrinhoStatus entidadeRetorno = new CarrinhoStatus();
                PopulaCarrinhoStatus(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os CarrinhoStatus existentes na base de dados.
        /// </summary>
		public IEnumerable<CarrinhoStatus> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de CarrinhoStatus na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de CarrinhoStatus na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM CarrinhoStatus");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um CarrinhoStatus baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">CarrinhoStatus a ser populado(.</param>
		public static void PopulaCarrinhoStatus(IDataReader reader, CarrinhoStatus entidade) 
		{						
			if (reader["carrinhoStatusId"] != DBNull.Value)
				entidade.CarrinhoStatusId = Convert.ToInt32(reader["carrinhoStatusId"].ToString());
			
			if (reader["statusCarrinho"] != DBNull.Value)
				entidade.StatusCarrinho = reader["statusCarrinho"].ToString();
			

		}		
		
	}
}
		