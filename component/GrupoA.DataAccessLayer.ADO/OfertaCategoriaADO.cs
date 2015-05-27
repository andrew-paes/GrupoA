
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
	public partial class OfertaCategoriaADO : ADOSuper, IOfertaCategoriaDAL {
	
	    /// <summary>
        /// Método que persiste um OfertaCategoria.
        /// </summary>
        /// <param name="entidade">OfertaCategoria contendo os dados a serem persistidos.</param>	
		public void Inserir(OfertaCategoria entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO OfertaCategoria ");
			sbSQL.Append(" (ofertaId, categoriaId) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@ofertaId, @categoriaId) ");											

			sbSQL.Append(" ; SET @ofertaCategoriaId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@ofertaCategoriaId", DbType.Int32, 8);

			_db.AddInParameter(command, "@ofertaId", DbType.Int32, entidade.Oferta.OfertaId);

			_db.AddInParameter(command, "@categoriaId", DbType.Int32, entidade.Categoria.CategoriaId);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.OfertaCategoriaId = Convert.ToInt32(_db.GetParameterValue(command, "@ofertaCategoriaId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um OfertaCategoria.
        /// </summary>
        /// <param name="entidade">OfertaCategoria contendo os dados a serem atualizados.</param>
		public void Atualizar(OfertaCategoria entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE OfertaCategoria SET ");
			sbSQL.Append(" ofertaId=@ofertaId, categoriaId=@categoriaId ");
			sbSQL.Append(" WHERE ofertaCategoriaId=@ofertaCategoriaId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@ofertaCategoriaId", DbType.Int32, entidade.OfertaCategoriaId);
			_db.AddInParameter(command, "@ofertaId", DbType.Int32, entidade.Oferta.OfertaId);
			_db.AddInParameter(command, "@categoriaId", DbType.Int32, entidade.Categoria.CategoriaId);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um OfertaCategoria da base de dados.
        /// </summary>
        /// <param name="entidade">OfertaCategoria a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(OfertaCategoria entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM OfertaCategoria ");
			sbSQL.Append("WHERE ofertaCategoriaId=@ofertaCategoriaId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@ofertaCategoriaId", DbType.Int32, entidade.OfertaCategoriaId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um OfertaCategoria.
		/// </summary>
        /// <param name="entidade">OfertaCategoria a ser carregado (somente o identificador é necessário).</param>
		/// <returns>OfertaCategoria</returns>
		public OfertaCategoria Carregar(int ofertaCategoriaId) {		
			OfertaCategoria entidade = new OfertaCategoria();
			entidade.OfertaCategoriaId = ofertaCategoriaId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um OfertaCategoria.
		/// </summary>
        /// <param name="entidade">OfertaCategoria a ser carregado (somente o identificador é necessário).</param>
		/// <returns>OfertaCategoria</returns>
		public OfertaCategoria Carregar(OfertaCategoria entidade) {		
		
			OfertaCategoria entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM OfertaCategoria WHERE ofertaCategoriaId=@ofertaCategoriaId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@ofertaCategoriaId", DbType.Int32, entidade.OfertaCategoriaId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new OfertaCategoria();
				PopulaOfertaCategoria(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de OfertaCategoria.
        /// </summary>
        /// <param name="entidade">Categoria relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de OfertaCategoria.</returns>
		public IEnumerable<OfertaCategoria> Carregar(Categoria entidade)
		{		
			List<OfertaCategoria> entidadesRetorno = new List<OfertaCategoria>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT OfertaCategoria.* FROM OfertaCategoria WHERE OfertaCategoria.categoriaId=@categoriaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@categoriaId", DbType.Int32, entidade.CategoriaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                OfertaCategoria entidadeRetorno = new OfertaCategoria();
                PopulaOfertaCategoria(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de OfertaCategoria.
        /// </summary>
        /// <param name="entidade">Oferta relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de OfertaCategoria.</returns>
		public IEnumerable<OfertaCategoria> Carregar(Oferta entidade)
		{		
			List<OfertaCategoria> entidadesRetorno = new List<OfertaCategoria>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT OfertaCategoria.* FROM OfertaCategoria WHERE OfertaCategoria.ofertaId=@ofertaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@ofertaId", DbType.Int32, entidade.OfertaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                OfertaCategoria entidadeRetorno = new OfertaCategoria();
                PopulaOfertaCategoria(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de OfertaCategoria.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos OfertaCategoria.</returns>
		public IEnumerable<OfertaCategoria> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<OfertaCategoria> entidadesRetorno = new List<OfertaCategoria>();
			
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
				sbOrder.Append( " ORDER BY ofertaCategoriaId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM OfertaCategoria");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM OfertaCategoria WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM OfertaCategoria ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT OfertaCategoria.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM OfertaCategoria ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT OfertaCategoria.* FROM OfertaCategoria ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                OfertaCategoria entidadeRetorno = new OfertaCategoria();
                PopulaOfertaCategoria(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os OfertaCategoria existentes na base de dados.
        /// </summary>
		public IEnumerable<OfertaCategoria> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de OfertaCategoria na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de OfertaCategoria na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM OfertaCategoria");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um OfertaCategoria baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">OfertaCategoria a ser populado(.</param>
		public static void PopulaOfertaCategoria(IDataReader reader, OfertaCategoria entidade) 
		{						
			if (reader["ofertaCategoriaId"] != DBNull.Value)
				entidade.OfertaCategoriaId = Convert.ToInt32(reader["ofertaCategoriaId"].ToString());
			
			if (reader["ofertaId"] != DBNull.Value) {
				entidade.Oferta = new Oferta();
				entidade.Oferta.OfertaId = Convert.ToInt32(reader["ofertaId"].ToString());
			}

			if (reader["categoriaId"] != DBNull.Value) {
				entidade.Categoria = new Categoria();
				entidade.Categoria.CategoriaId = Convert.ToInt32(reader["categoriaId"].ToString());
			}


		}		
		
	}
}
		