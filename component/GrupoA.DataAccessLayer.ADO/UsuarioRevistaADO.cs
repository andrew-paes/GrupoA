
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
	public partial class UsuarioRevistaADO : ADOSuper, IUsuarioRevistaDAL {
	
	    /// <summary>
        /// Método que persiste um UsuarioRevista.
        /// </summary>
        /// <param name="entidade">UsuarioRevista contendo os dados a serem persistidos.</param>	
		public void Inserir(UsuarioRevista entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO UsuarioRevista ");
			sbSQL.Append(" (usuarioId, revistaId, dataFimAssinatura, dataInicioAssinatura) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@usuarioId, @revistaId, @dataFimAssinatura, @dataInicioAssinatura) ");											

			sbSQL.Append(" ; SET @usuarioRevistaId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@usuarioRevistaId", DbType.Int32, 8);

			_db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.Usuario.UsuarioId);

			_db.AddInParameter(command, "@revistaId", DbType.Int32, entidade.Revista.RevistaId);

			_db.AddInParameter(command, "@dataFimAssinatura", DbType.DateTime, entidade.DataFimAssinatura);

			_db.AddInParameter(command, "@dataInicioAssinatura", DbType.DateTime, entidade.DataInicioAssinatura);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.UsuarioRevistaId = Convert.ToInt32(_db.GetParameterValue(command, "@usuarioRevistaId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um UsuarioRevista.
        /// </summary>
        /// <param name="entidade">UsuarioRevista contendo os dados a serem atualizados.</param>
		public void Atualizar(UsuarioRevista entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE UsuarioRevista SET ");
			sbSQL.Append(" usuarioId=@usuarioId, revistaId=@revistaId, dataFimAssinatura=@dataFimAssinatura, dataInicioAssinatura=@dataInicioAssinatura ");
			sbSQL.Append(" WHERE usuarioRevistaId=@usuarioRevistaId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@usuarioRevistaId", DbType.Int32, entidade.UsuarioRevistaId);
			_db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.Usuario.UsuarioId);
			_db.AddInParameter(command, "@revistaId", DbType.Int32, entidade.Revista.RevistaId);
			_db.AddInParameter(command, "@dataFimAssinatura", DbType.DateTime, entidade.DataFimAssinatura);
			_db.AddInParameter(command, "@dataInicioAssinatura", DbType.DateTime, entidade.DataInicioAssinatura);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um UsuarioRevista da base de dados.
        /// </summary>
        /// <param name="entidade">UsuarioRevista a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(UsuarioRevista entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM UsuarioRevista ");
			sbSQL.Append("WHERE usuarioRevistaId=@usuarioRevistaId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@usuarioRevistaId", DbType.Int32, entidade.UsuarioRevistaId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um UsuarioRevista.
		/// </summary>
        /// <param name="entidade">UsuarioRevista a ser carregado (somente o identificador é necessário).</param>
		/// <returns>UsuarioRevista</returns>
		public UsuarioRevista Carregar(int usuarioRevistaId) {		
			UsuarioRevista entidade = new UsuarioRevista();
			entidade.UsuarioRevistaId = usuarioRevistaId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um UsuarioRevista.
		/// </summary>
        /// <param name="entidade">UsuarioRevista a ser carregado (somente o identificador é necessário).</param>
		/// <returns>UsuarioRevista</returns>
		public UsuarioRevista Carregar(UsuarioRevista entidade) {		
		
			UsuarioRevista entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM UsuarioRevista WHERE usuarioRevistaId=@usuarioRevistaId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@usuarioRevistaId", DbType.Int32, entidade.UsuarioRevistaId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new UsuarioRevista();
				PopulaUsuarioRevista(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de UsuarioRevista.
        /// </summary>
        /// <param name="entidade">Revista relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de UsuarioRevista.</returns>
		public IEnumerable<UsuarioRevista> Carregar(Revista entidade)
		{		
			List<UsuarioRevista> entidadesRetorno = new List<UsuarioRevista>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT UsuarioRevista.* FROM UsuarioRevista WHERE UsuarioRevista.revistaId=@revistaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@revistaId", DbType.Int32, entidade.RevistaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                UsuarioRevista entidadeRetorno = new UsuarioRevista();
                PopulaUsuarioRevista(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de UsuarioRevista.
        /// </summary>
        /// <param name="entidade">Usuario relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de UsuarioRevista.</returns>
		public IEnumerable<UsuarioRevista> Carregar(Usuario entidade)
		{		
			List<UsuarioRevista> entidadesRetorno = new List<UsuarioRevista>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT UsuarioRevista.* FROM UsuarioRevista WHERE UsuarioRevista.usuarioId=@usuarioId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.UsuarioId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                UsuarioRevista entidadeRetorno = new UsuarioRevista();
                PopulaUsuarioRevista(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de UsuarioRevista.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos UsuarioRevista.</returns>
		public IEnumerable<UsuarioRevista> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<UsuarioRevista> entidadesRetorno = new List<UsuarioRevista>();
			
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
				sbOrder.Append( " ORDER BY usuarioRevistaId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM UsuarioRevista");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM UsuarioRevista WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM UsuarioRevista ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT UsuarioRevista.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM UsuarioRevista ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT UsuarioRevista.* FROM UsuarioRevista ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                UsuarioRevista entidadeRetorno = new UsuarioRevista();
                PopulaUsuarioRevista(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os UsuarioRevista existentes na base de dados.
        /// </summary>
		public IEnumerable<UsuarioRevista> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de UsuarioRevista na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de UsuarioRevista na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM UsuarioRevista");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um UsuarioRevista baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">UsuarioRevista a ser populado(.</param>
		public static void PopulaUsuarioRevista(IDataReader reader, UsuarioRevista entidade) 
		{						
			if (reader["usuarioRevistaId"] != DBNull.Value)
				entidade.UsuarioRevistaId = Convert.ToInt32(reader["usuarioRevistaId"].ToString());
			
			if (reader["dataFimAssinatura"] != DBNull.Value)
				entidade.DataFimAssinatura = Convert.ToDateTime(reader["dataFimAssinatura"].ToString());
			
			if (reader["dataInicioAssinatura"] != DBNull.Value)
				entidade.DataInicioAssinatura = Convert.ToDateTime(reader["dataInicioAssinatura"].ToString());
			
			if (reader["usuarioId"] != DBNull.Value) {
				entidade.Usuario = new Usuario();
				entidade.Usuario.UsuarioId = Convert.ToInt32(reader["usuarioId"].ToString());
			}

			if (reader["revistaId"] != DBNull.Value) {
				entidade.Revista = new Revista();
				entidade.Revista.RevistaId = Convert.ToInt32(reader["revistaId"].ToString());
			}


		}		
		
	}
}
		