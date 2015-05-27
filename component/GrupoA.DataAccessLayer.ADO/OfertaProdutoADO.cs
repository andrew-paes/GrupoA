
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
	public partial class OfertaProdutoADO : ADOSuper, IOfertaProdutoDAL {
	
	    /// <summary>
        /// Método que persiste um OfertaProduto.
        /// </summary>
        /// <param name="entidade">OfertaProduto contendo os dados a serem persistidos.</param>	
		public void Inserir(OfertaProduto entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO OfertaProduto ");
			sbSQL.Append(" (ofertaId, produtoId) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@ofertaId, @produtoId) ");											

			sbSQL.Append(" ; SET @ofertaProdutoId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@ofertaProdutoId", DbType.Int32, 8);

			_db.AddInParameter(command, "@ofertaId", DbType.Int32, entidade.Oferta.OfertaId);

			_db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.Produto.ProdutoId);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.OfertaProdutoId = Convert.ToInt32(_db.GetParameterValue(command, "@ofertaProdutoId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um OfertaProduto.
        /// </summary>
        /// <param name="entidade">OfertaProduto contendo os dados a serem atualizados.</param>
		public void Atualizar(OfertaProduto entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE OfertaProduto SET ");
			sbSQL.Append(" ofertaId=@ofertaId, produtoId=@produtoId ");
			sbSQL.Append(" WHERE ofertaProdutoId=@ofertaProdutoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@ofertaProdutoId", DbType.Int32, entidade.OfertaProdutoId);
			_db.AddInParameter(command, "@ofertaId", DbType.Int32, entidade.Oferta.OfertaId);
			_db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.Produto.ProdutoId);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um OfertaProduto da base de dados.
        /// </summary>
        /// <param name="entidade">OfertaProduto a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(OfertaProduto entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM OfertaProduto ");
			sbSQL.Append("WHERE ofertaProdutoId=@ofertaProdutoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@ofertaProdutoId", DbType.Int32, entidade.OfertaProdutoId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um OfertaProduto.
		/// </summary>
        /// <param name="entidade">OfertaProduto a ser carregado (somente o identificador é necessário).</param>
		/// <returns>OfertaProduto</returns>
		public OfertaProduto Carregar(int ofertaProdutoId) {		
			OfertaProduto entidade = new OfertaProduto();
			entidade.OfertaProdutoId = ofertaProdutoId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um OfertaProduto.
		/// </summary>
        /// <param name="entidade">OfertaProduto a ser carregado (somente o identificador é necessário).</param>
		/// <returns>OfertaProduto</returns>
		public OfertaProduto Carregar(OfertaProduto entidade) {		
		
			OfertaProduto entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM OfertaProduto WHERE ofertaProdutoId=@ofertaProdutoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@ofertaProdutoId", DbType.Int32, entidade.OfertaProdutoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new OfertaProduto();
				PopulaOfertaProduto(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de OfertaProduto.
        /// </summary>
        /// <param name="entidade">Oferta relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de OfertaProduto.</returns>
		public IEnumerable<OfertaProduto> Carregar(Oferta entidade)
		{		
			List<OfertaProduto> entidadesRetorno = new List<OfertaProduto>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT OfertaProduto.* FROM OfertaProduto WHERE OfertaProduto.ofertaId=@ofertaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@ofertaId", DbType.Int32, entidade.OfertaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                OfertaProduto entidadeRetorno = new OfertaProduto();
                PopulaOfertaProduto(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de OfertaProduto.
        /// </summary>
        /// <param name="entidade">Produto relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de OfertaProduto.</returns>
		public IEnumerable<OfertaProduto> Carregar(Produto entidade)
		{		
			List<OfertaProduto> entidadesRetorno = new List<OfertaProduto>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT OfertaProduto.* FROM OfertaProduto WHERE OfertaProduto.produtoId=@produtoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.ProdutoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                OfertaProduto entidadeRetorno = new OfertaProduto();
                PopulaOfertaProduto(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de OfertaProduto.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos OfertaProduto.</returns>
		public IEnumerable<OfertaProduto> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<OfertaProduto> entidadesRetorno = new List<OfertaProduto>();
			
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
				sbOrder.Append( " ORDER BY ofertaProdutoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM OfertaProduto");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM OfertaProduto WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM OfertaProduto ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT OfertaProduto.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM OfertaProduto ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT OfertaProduto.* FROM OfertaProduto ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                OfertaProduto entidadeRetorno = new OfertaProduto();
                PopulaOfertaProduto(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os OfertaProduto existentes na base de dados.
        /// </summary>
		public IEnumerable<OfertaProduto> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de OfertaProduto na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de OfertaProduto na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM OfertaProduto");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um OfertaProduto baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">OfertaProduto a ser populado(.</param>
		public static void PopulaOfertaProduto(IDataReader reader, OfertaProduto entidade) 
		{						
			if (reader["ofertaProdutoId"] != DBNull.Value)
				entidade.OfertaProdutoId = Convert.ToInt32(reader["ofertaProdutoId"].ToString());
			
			if (reader["ofertaId"] != DBNull.Value) {
				entidade.Oferta = new Oferta();
				entidade.Oferta.OfertaId = Convert.ToInt32(reader["ofertaId"].ToString());
			}

			if (reader["produtoId"] != DBNull.Value) {
				entidade.Produto = new Produto();
				entidade.Produto.ProdutoId = Convert.ToInt32(reader["produtoId"].ToString());
			}


		}		
		
	}
}
		