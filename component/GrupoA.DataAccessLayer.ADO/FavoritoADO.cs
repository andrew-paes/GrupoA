
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
	public partial class FavoritoADO : ADOSuper, IFavoritoDAL {
	
	    /// <summary>
        /// Método que persiste um Favorito.
        /// </summary>
        /// <param name="entidade">Favorito contendo os dados a serem persistidos.</param>	
		public void Inserir(Favorito entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO Favorito ");
			sbSQL.Append(" (conteudoId, usuarioId) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@conteudoId, @usuarioId) ");											

			sbSQL.Append(" ; SET @favoritoId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@favoritoId", DbType.Int32, 8);

			_db.AddInParameter(command, "@conteudoId", DbType.Int32, entidade.Conteudo.ConteudoId);

			_db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.UsuarioId);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.FavoritoId = Convert.ToInt32(_db.GetParameterValue(command, "@favoritoId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um Favorito.
        /// </summary>
        /// <param name="entidade">Favorito contendo os dados a serem atualizados.</param>
		public void Atualizar(Favorito entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE Favorito SET ");
			sbSQL.Append(" conteudoId=@conteudoId, usuarioId=@usuarioId ");
			sbSQL.Append(" WHERE favoritoId=@favoritoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@favoritoId", DbType.Int32, entidade.FavoritoId);
			_db.AddInParameter(command, "@conteudoId", DbType.Int32, entidade.Conteudo.ConteudoId);
			_db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.UsuarioId);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um Favorito da base de dados.
        /// </summary>
        /// <param name="entidade">Favorito a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(Favorito entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM Favorito ");
			sbSQL.Append("WHERE favoritoId=@favoritoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@favoritoId", DbType.Int32, entidade.FavoritoId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um Favorito.
		/// </summary>
        /// <param name="entidade">Favorito a ser carregado (somente o identificador é necessário).</param>
		/// <returns>Favorito</returns>
		public Favorito Carregar(int favoritoId) {		
			Favorito entidade = new Favorito();
			entidade.FavoritoId = favoritoId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um Favorito.
		/// </summary>
        /// <param name="entidade">Favorito a ser carregado (somente o identificador é necessário).</param>
		/// <returns>Favorito</returns>
		public Favorito Carregar(Favorito entidade) {		
		
			Favorito entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM Favorito WHERE favoritoId=@favoritoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@favoritoId", DbType.Int32, entidade.FavoritoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new Favorito();
				PopulaFavorito(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de Favorito.
        /// </summary>
        /// <param name="entidade">Conteudo relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Favorito.</returns>
		public IEnumerable<Favorito> Carregar(Conteudo entidade)
		{		
			List<Favorito> entidadesRetorno = new List<Favorito>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Favorito.* FROM Favorito WHERE Favorito.conteudoId=@conteudoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@conteudoId", DbType.Int32, entidade.ConteudoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Favorito entidadeRetorno = new Favorito();
                PopulaFavorito(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de Favorito.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos Favorito.</returns>
		public IEnumerable<Favorito> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<Favorito> entidadesRetorno = new List<Favorito>();
			
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
				sbOrder.Append( " ORDER BY favoritoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM Favorito");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Favorito WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Favorito ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT Favorito.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM Favorito ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT Favorito.* FROM Favorito ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Favorito entidadeRetorno = new Favorito();
                PopulaFavorito(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os Favorito existentes na base de dados.
        /// </summary>
		public IEnumerable<Favorito> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de Favorito na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de Favorito na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM Favorito");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um Favorito baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Favorito a ser populado(.</param>
		public static void PopulaFavorito(IDataReader reader, Favorito entidade) 
		{						
			if (reader["favoritoId"] != DBNull.Value)
				entidade.FavoritoId = Convert.ToInt32(reader["favoritoId"].ToString());
			
			if (reader["usuarioId"] != DBNull.Value)
				entidade.UsuarioId = Convert.ToInt32(reader["usuarioId"].ToString());
			
			if (reader["conteudoId"] != DBNull.Value) {
				entidade.Conteudo = new Conteudo();
				entidade.Conteudo.ConteudoId = Convert.ToInt32(reader["conteudoId"].ToString());
			}


		}		
		
	}
}
		