
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
	public partial class MidiaCategoriaADO : ADOSuper, IMidiaCategoriaDAL {
	
	    /// <summary>
        /// Método que persiste um MidiaCategoria.
        /// </summary>
        /// <param name="entidade">MidiaCategoria contendo os dados a serem persistidos.</param>	
		public void Inserir(MidiaCategoria entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO MidiaCategoria ");
			sbSQL.Append(" (midiaId, categoriaId) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@midiaId, @categoriaId) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			//_db.AddInParameter(command, "@midiaCategoriaId", DbType.Int32, entidade.MidiaCategoriaId);

			_db.AddInParameter(command, "@midiaId", DbType.Int32, entidade.Midia.MidiaId);

			_db.AddInParameter(command, "@categoriaId", DbType.Int32, entidade.Categoria.CategoriaId);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um MidiaCategoria.
        /// </summary>
        /// <param name="entidade">MidiaCategoria contendo os dados a serem atualizados.</param>
		public void Atualizar(MidiaCategoria entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE MidiaCategoria SET ");
			sbSQL.Append(" midiaId=@midiaId, categoriaId=@categoriaId ");
			sbSQL.Append(" WHERE midiaCategoriaId=@midiaCategoriaId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@midiaCategoriaId", DbType.Int32, entidade.MidiaCategoriaId);
			_db.AddInParameter(command, "@midiaId", DbType.Int32, entidade.Midia.MidiaId);
			_db.AddInParameter(command, "@categoriaId", DbType.Int32, entidade.Categoria.CategoriaId);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um MidiaCategoria da base de dados.
        /// </summary>
        /// <param name="entidade">MidiaCategoria a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(MidiaCategoria entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM MidiaCategoria ");
			sbSQL.Append("WHERE midiaCategoriaId=@midiaCategoriaId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@midiaCategoriaId", DbType.Int32, entidade.MidiaCategoriaId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um MidiaCategoria.
		/// </summary>
        /// <param name="entidade">MidiaCategoria a ser carregado (somente o identificador é necessário).</param>
		/// <returns>MidiaCategoria</returns>
		public MidiaCategoria Carregar(int midiaCategoriaId) {		
			MidiaCategoria entidade = new MidiaCategoria();
			entidade.MidiaCategoriaId = midiaCategoriaId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um MidiaCategoria.
		/// </summary>
        /// <param name="entidade">MidiaCategoria a ser carregado (somente o identificador é necessário).</param>
		/// <returns>MidiaCategoria</returns>
		public MidiaCategoria Carregar(MidiaCategoria entidade) {		
		
			MidiaCategoria entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM MidiaCategoria WHERE midiaCategoriaId=@midiaCategoriaId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@midiaCategoriaId", DbType.Int32, entidade.MidiaCategoriaId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new MidiaCategoria();
				PopulaMidiaCategoria(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de MidiaCategoria.
        /// </summary>
        /// <param name="entidade">Categoria relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de MidiaCategoria.</returns>
		public IEnumerable<MidiaCategoria> Carregar(Categoria entidade)
		{		
			List<MidiaCategoria> entidadesRetorno = new List<MidiaCategoria>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT MidiaCategoria.* FROM MidiaCategoria WHERE MidiaCategoria.categoriaId=@categoriaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@categoriaId", DbType.Int32, entidade.CategoriaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                MidiaCategoria entidadeRetorno = new MidiaCategoria();
                PopulaMidiaCategoria(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de MidiaCategoria.
        /// </summary>
        /// <param name="entidade">Midia relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de MidiaCategoria.</returns>
		public IEnumerable<MidiaCategoria> Carregar(Midia entidade)
		{		
			List<MidiaCategoria> entidadesRetorno = new List<MidiaCategoria>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT MidiaCategoria.* FROM MidiaCategoria WHERE MidiaCategoria.midiaId=@midiaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@midiaId", DbType.Int32, entidade.MidiaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                MidiaCategoria entidadeRetorno = new MidiaCategoria();
                PopulaMidiaCategoria(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de MidiaCategoria.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos MidiaCategoria.</returns>
		public IEnumerable<MidiaCategoria> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<MidiaCategoria> entidadesRetorno = new List<MidiaCategoria>();
			
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
				sbOrder.Append( " ORDER BY midiaCategoriaId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM MidiaCategoria");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM MidiaCategoria WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM MidiaCategoria ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT MidiaCategoria.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM MidiaCategoria ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT MidiaCategoria.* FROM MidiaCategoria ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                MidiaCategoria entidadeRetorno = new MidiaCategoria();
                PopulaMidiaCategoria(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os MidiaCategoria existentes na base de dados.
        /// </summary>
		public IEnumerable<MidiaCategoria> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de MidiaCategoria na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de MidiaCategoria na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM MidiaCategoria");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um MidiaCategoria baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">MidiaCategoria a ser populado(.</param>
		public static void PopulaMidiaCategoria(IDataReader reader, MidiaCategoria entidade) 
		{						
			if (reader["midiaCategoriaId"] != DBNull.Value)
				entidade.MidiaCategoriaId = Convert.ToInt32(reader["midiaCategoriaId"].ToString());
			
			if (reader["midiaId"] != DBNull.Value) {
				entidade.Midia = new Midia();
				entidade.Midia.MidiaId = Convert.ToInt32(reader["midiaId"].ToString());
			}

			if (reader["categoriaId"] != DBNull.Value) {
				entidade.Categoria = new Categoria();
				entidade.Categoria.CategoriaId = Convert.ToInt32(reader["categoriaId"].ToString());
			}


		}		
		
	}
}
		