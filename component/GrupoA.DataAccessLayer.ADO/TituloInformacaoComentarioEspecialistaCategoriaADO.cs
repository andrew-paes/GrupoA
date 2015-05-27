
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
	public partial class TituloInformacaoComentarioEspecialistaCategoriaADO : ADOSuper, ITituloInformacaoComentarioEspecialistaCategoriaDAL {
	
	    /// <summary>
        /// Método que persiste um TituloInformacaoComentarioEspecialistaCategoria.
        /// </summary>
        /// <param name="entidade">TituloInformacaoComentarioEspecialistaCategoria contendo os dados a serem persistidos.</param>	
		public void Inserir(TituloInformacaoComentarioEspecialistaCategoria entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO TituloInformacaoComentarioEspecialistaCategoria ");
			sbSQL.Append(" (tituloInformacaoComentarioEspecialistaId, categoriaId) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@tituloInformacaoComentarioEspecialistaId, @categoriaId) ");											

			sbSQL.Append(" ; SET @tituloInformacaoComentarioEspecialistaCategoriaId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddOutParameter(command, "@tituloInformacaoComentarioEspecialistaCategoriaId", DbType.Int32, 8);

			_db.AddInParameter(command, "@tituloInformacaoComentarioEspecialistaId", DbType.Int32, entidade.TituloInformacaoComentarioEspecialista.TituloInformacaoComentarioEspecialistaId);

			_db.AddInParameter(command, "@categoriaId", DbType.Int32, entidade.Categoria.CategoriaId);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);

            entidade.TituloInformacaoComentarioEspecialistaCategoriaId = Convert.ToInt32(_db.GetParameterValue(command, "@tituloInformacaoComentarioEspecialistaCategoriaId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um TituloInformacaoComentarioEspecialistaCategoria.
        /// </summary>
        /// <param name="entidade">TituloInformacaoComentarioEspecialistaCategoria contendo os dados a serem atualizados.</param>
		public void Atualizar(TituloInformacaoComentarioEspecialistaCategoria entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE TituloInformacaoComentarioEspecialistaCategoria SET ");
			sbSQL.Append(" tituloInformacaoComentarioEspecialistaId=@tituloInformacaoComentarioEspecialistaId, categoriaId=@categoriaId ");
			sbSQL.Append(" WHERE tituloInformacaoComentarioEspecialistaComentarioId=@tituloInformacaoComentarioEspecialistaCategoriaId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@tituloInformacaoComentarioEspecialistaCategoriaId", DbType.Int32, entidade.TituloInformacaoComentarioEspecialistaCategoriaId);
			_db.AddInParameter(command, "@tituloInformacaoComentarioEspecialistaId", DbType.Int32, entidade.TituloInformacaoComentarioEspecialista.TituloInformacaoComentarioEspecialistaId);
			_db.AddInParameter(command, "@categoriaId", DbType.Int32, entidade.Categoria.CategoriaId);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um TituloInformacaoComentarioEspecialistaCategoria da base de dados.
        /// </summary>
        /// <param name="entidade">TituloInformacaoComentarioEspecialistaCategoria a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(TituloInformacaoComentarioEspecialistaCategoria entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM TituloInformacaoComentarioEspecialistaCategoria ");
			sbSQL.Append("WHERE tituloInformacaoComentarioEspecialistaComentarioId=@tituloInformacaoComentarioEspecialistaCategoriaId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@tituloInformacaoComentarioEspecialistaCategoriaId", DbType.Int32, entidade.TituloInformacaoComentarioEspecialistaCategoriaId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um TituloInformacaoComentarioEspecialistaCategoria.
		/// </summary>
        /// <param name="entidade">TituloInformacaoComentarioEspecialistaCategoria a ser carregado (somente o identificador é necessário).</param>
		/// <returns>TituloInformacaoComentarioEspecialistaCategoria</returns>
		public TituloInformacaoComentarioEspecialistaCategoria Carregar(int tituloInformacaoComentarioEspecialistaCategoriaId) {		
			TituloInformacaoComentarioEspecialistaCategoria entidade = new TituloInformacaoComentarioEspecialistaCategoria();
            entidade.TituloInformacaoComentarioEspecialistaCategoriaId = tituloInformacaoComentarioEspecialistaCategoriaId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um TituloInformacaoComentarioEspecialistaCategoria.
		/// </summary>
        /// <param name="entidade">TituloInformacaoComentarioEspecialistaCategoria a ser carregado (somente o identificador é necessário).</param>
		/// <returns>TituloInformacaoComentarioEspecialistaCategoria</returns>
		public TituloInformacaoComentarioEspecialistaCategoria Carregar(TituloInformacaoComentarioEspecialistaCategoria entidade) {		
		
			TituloInformacaoComentarioEspecialistaCategoria entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM TituloInformacaoComentarioEspecialistaCategoria WHERE tituloInformacaoComentarioEspecialistaComentarioId=@tituloInformacaoComentarioEspecialistaCategoriaId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@tituloInformacaoComentarioEspecialistaCategoriaId", DbType.Int32, entidade.TituloInformacaoComentarioEspecialistaCategoriaId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new TituloInformacaoComentarioEspecialistaCategoria();
				PopulaTituloInformacaoComentarioEspecialistaCategoria(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de TituloInformacaoComentarioEspecialistaCategoria.
        /// </summary>
        /// <param name="entidade">Categoria relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de TituloInformacaoComentarioEspecialistaCategoria.</returns>
		public IEnumerable<TituloInformacaoComentarioEspecialistaCategoria> Carregar(Categoria entidade)
		{		
			List<TituloInformacaoComentarioEspecialistaCategoria> entidadesRetorno = new List<TituloInformacaoComentarioEspecialistaCategoria>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT TituloInformacaoComentarioEspecialistaCategoria.* FROM TituloInformacaoComentarioEspecialistaCategoria WHERE TituloInformacaoComentarioEspecialistaCategoria.categoriaId=@categoriaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@categoriaId", DbType.Int32, entidade.CategoriaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                TituloInformacaoComentarioEspecialistaCategoria entidadeRetorno = new TituloInformacaoComentarioEspecialistaCategoria();
                PopulaTituloInformacaoComentarioEspecialistaCategoria(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de TituloInformacaoComentarioEspecialistaCategoria.
        /// </summary>
        /// <param name="entidade">TituloInformacaoComentarioEspecialista relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de TituloInformacaoComentarioEspecialistaCategoria.</returns>
		public IEnumerable<TituloInformacaoComentarioEspecialistaCategoria> Carregar(TituloInformacaoComentarioEspecialista entidade)
		{		
			List<TituloInformacaoComentarioEspecialistaCategoria> entidadesRetorno = new List<TituloInformacaoComentarioEspecialistaCategoria>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT TituloInformacaoComentarioEspecialistaCategoria.* FROM TituloInformacaoComentarioEspecialistaCategoria WHERE TituloInformacaoComentarioEspecialistaCategoria.tituloInformacaoComentarioEspecialistaId=@tituloInformacaoComentarioEspecialistaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@tituloInformacaoComentarioEspecialistaId", DbType.Int32, entidade.TituloInformacaoComentarioEspecialistaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                TituloInformacaoComentarioEspecialistaCategoria entidadeRetorno = new TituloInformacaoComentarioEspecialistaCategoria();
                PopulaTituloInformacaoComentarioEspecialistaCategoria(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de TituloInformacaoComentarioEspecialistaCategoria.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos TituloInformacaoComentarioEspecialistaCategoria.</returns>
		public IEnumerable<TituloInformacaoComentarioEspecialistaCategoria> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<TituloInformacaoComentarioEspecialistaCategoria> entidadesRetorno = new List<TituloInformacaoComentarioEspecialistaCategoria>();
			
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
				sbOrder.Append( " ORDER BY tituloInformacaoComentarioEspecialistaComentarioId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM TituloInformacaoComentarioEspecialistaCategoria");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM TituloInformacaoComentarioEspecialistaCategoria WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM TituloInformacaoComentarioEspecialistaCategoria ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT TituloInformacaoComentarioEspecialistaCategoria.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM TituloInformacaoComentarioEspecialistaCategoria ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT TituloInformacaoComentarioEspecialistaCategoria.* FROM TituloInformacaoComentarioEspecialistaCategoria ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                TituloInformacaoComentarioEspecialistaCategoria entidadeRetorno = new TituloInformacaoComentarioEspecialistaCategoria();
                PopulaTituloInformacaoComentarioEspecialistaCategoria(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os TituloInformacaoComentarioEspecialistaCategoria existentes na base de dados.
        /// </summary>
		public IEnumerable<TituloInformacaoComentarioEspecialistaCategoria> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de TituloInformacaoComentarioEspecialistaCategoria na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de TituloInformacaoComentarioEspecialistaCategoria na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM TituloInformacaoComentarioEspecialistaCategoria");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um TituloInformacaoComentarioEspecialistaCategoria baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">TituloInformacaoComentarioEspecialistaCategoria a ser populado(.</param>
		public static void PopulaTituloInformacaoComentarioEspecialistaCategoria(IDataReader reader, TituloInformacaoComentarioEspecialistaCategoria entidade) 
		{						
			if (reader["tituloInformacaoComentarioEspecialistaCategoriaId"] != DBNull.Value)
                entidade.TituloInformacaoComentarioEspecialistaCategoriaId = Convert.ToInt32(reader["tituloInformacaoComentarioEspecialistaCategoriaId"].ToString());
			
			if (reader["tituloInformacaoComentarioEspecialistaId"] != DBNull.Value) {
				entidade.TituloInformacaoComentarioEspecialista = new TituloInformacaoComentarioEspecialista();
				entidade.TituloInformacaoComentarioEspecialista.TituloInformacaoComentarioEspecialistaId = Convert.ToInt32(reader["tituloInformacaoComentarioEspecialistaId"].ToString());
			}

			if (reader["categoriaId"] != DBNull.Value) {
				entidade.Categoria = new Categoria();
				entidade.Categoria.CategoriaId = Convert.ToInt32(reader["categoriaId"].ToString());
			}


		}		
		
	}
}
		