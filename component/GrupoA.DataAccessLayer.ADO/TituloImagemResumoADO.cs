
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
	public partial class TituloImagemResumoADO : ADOSuper, ITituloImagemResumoDAL {
	
	    /// <summary>
        /// Método que persiste um TituloImagemResumo.
        /// </summary>
        /// <param name="entidade">TituloImagemResumo contendo os dados a serem persistidos.</param>	
		public void Inserir(TituloImagemResumo entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO TituloImagemResumo ");
			sbSQL.Append(" (arquivoId, tituloId) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@arquivoId, @tituloId) ");											

			sbSQL.Append(" ; SET @tituloImagemResumoId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@tituloImagemResumoId", DbType.Int32, 8);

			_db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.Arquivo.ArquivoId);

			_db.AddInParameter(command, "@tituloId", DbType.Int32, entidade.Titulo.TituloId);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.TituloImagemResumoId = Convert.ToInt32(_db.GetParameterValue(command, "@tituloImagemResumoId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um TituloImagemResumo.
        /// </summary>
        /// <param name="entidade">TituloImagemResumo contendo os dados a serem atualizados.</param>
		public void Atualizar(TituloImagemResumo entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE TituloImagemResumo SET ");
			sbSQL.Append(" arquivoId=@arquivoId, tituloId=@tituloId ");
			sbSQL.Append(" WHERE tituloImagemResumoId=@tituloImagemResumoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@tituloImagemResumoId", DbType.Int32, entidade.TituloImagemResumoId);
			_db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.Arquivo.ArquivoId);
			_db.AddInParameter(command, "@tituloId", DbType.Int32, entidade.Titulo.TituloId);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um TituloImagemResumo da base de dados.
        /// </summary>
        /// <param name="entidade">TituloImagemResumo a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(TituloImagemResumo entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM TituloImagemResumo ");
			sbSQL.Append("WHERE tituloImagemResumoId=@tituloImagemResumoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@tituloImagemResumoId", DbType.Int32, entidade.TituloImagemResumoId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um TituloImagemResumo.
		/// </summary>
        /// <param name="entidade">TituloImagemResumo a ser carregado (somente o identificador é necessário).</param>
		/// <returns>TituloImagemResumo</returns>
		public TituloImagemResumo Carregar(int tituloImagemResumoId) {		
			TituloImagemResumo entidade = new TituloImagemResumo();
			entidade.TituloImagemResumoId = tituloImagemResumoId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um TituloImagemResumo.
		/// </summary>
        /// <param name="entidade">TituloImagemResumo a ser carregado (somente o identificador é necessário).</param>
		/// <returns>TituloImagemResumo</returns>
		public TituloImagemResumo Carregar(TituloImagemResumo entidade) {		
		
			TituloImagemResumo entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM TituloImagemResumo WHERE tituloImagemResumoId=@tituloImagemResumoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@tituloImagemResumoId", DbType.Int32, entidade.TituloImagemResumoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new TituloImagemResumo();
				PopulaTituloImagemResumo(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de TituloImagemResumo.
        /// </summary>
        /// <param name="entidade">Arquivo relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de TituloImagemResumo.</returns>
		public IEnumerable<TituloImagemResumo> Carregar(Arquivo entidade)
		{		
			List<TituloImagemResumo> entidadesRetorno = new List<TituloImagemResumo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT TituloImagemResumo.* FROM TituloImagemResumo WHERE TituloImagemResumo.arquivoId=@arquivoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.ArquivoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                TituloImagemResumo entidadeRetorno = new TituloImagemResumo();
                PopulaTituloImagemResumo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de TituloImagemResumo.
        /// </summary>
        /// <param name="entidade">Titulo relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de TituloImagemResumo.</returns>
		public IEnumerable<TituloImagemResumo> Carregar(Titulo entidade)
		{		
			List<TituloImagemResumo> entidadesRetorno = new List<TituloImagemResumo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT TituloImagemResumo.* FROM TituloImagemResumo WHERE TituloImagemResumo.tituloId=@tituloId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@tituloId", DbType.Int32, entidade.TituloId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                TituloImagemResumo entidadeRetorno = new TituloImagemResumo();
                PopulaTituloImagemResumo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de TituloImagemResumo.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos TituloImagemResumo.</returns>
		public IEnumerable<TituloImagemResumo> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<TituloImagemResumo> entidadesRetorno = new List<TituloImagemResumo>();
			
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
				sbOrder.Append( " ORDER BY tituloImagemResumoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM TituloImagemResumo");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM TituloImagemResumo WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM TituloImagemResumo ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT TituloImagemResumo.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM TituloImagemResumo ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT TituloImagemResumo.* FROM TituloImagemResumo ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                TituloImagemResumo entidadeRetorno = new TituloImagemResumo();
                PopulaTituloImagemResumo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os TituloImagemResumo existentes na base de dados.
        /// </summary>
		public IEnumerable<TituloImagemResumo> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de TituloImagemResumo na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de TituloImagemResumo na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM TituloImagemResumo");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um TituloImagemResumo baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">TituloImagemResumo a ser populado(.</param>
		public static void PopulaTituloImagemResumo(IDataReader reader, TituloImagemResumo entidade) 
		{						
			if (reader["tituloImagemResumoId"] != DBNull.Value)
				entidade.TituloImagemResumoId = Convert.ToInt32(reader["tituloImagemResumoId"].ToString());
			
			if (reader["arquivoId"] != DBNull.Value) {
				entidade.Arquivo = new Arquivo();
				entidade.Arquivo.ArquivoId = Convert.ToInt32(reader["arquivoId"].ToString());
			}

			if (reader["tituloId"] != DBNull.Value) {
				entidade.Titulo = new Titulo();
				entidade.Titulo.TituloId = Convert.ToInt32(reader["tituloId"].ToString());
			}


		}		
		
	}
}
		