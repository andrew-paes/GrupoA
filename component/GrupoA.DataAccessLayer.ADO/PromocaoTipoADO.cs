
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
	public partial class PromocaoTipoADO : ADOSuper, IPromocaoTipoDAL {
	
	    /// <summary>
        /// Método que persiste um PromocaoTipo.
        /// </summary>
        /// <param name="entidade">PromocaoTipo contendo os dados a serem persistidos.</param>	
		public void Inserir(PromocaoTipo entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO PromocaoTipo ");
			sbSQL.Append(" (promocaoTipoId, tipoPromocao) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@promocaoTipoId, @tipoPromocao) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@promocaoTipoId", DbType.Int32, entidade.PromocaoTipoId);

			_db.AddInParameter(command, "@tipoPromocao", DbType.String, entidade.TipoPromocao);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um PromocaoTipo.
        /// </summary>
        /// <param name="entidade">PromocaoTipo contendo os dados a serem atualizados.</param>
		public void Atualizar(PromocaoTipo entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE PromocaoTipo SET ");
			sbSQL.Append(" tipoPromocao=@tipoPromocao ");
			sbSQL.Append(" WHERE promocaoTipoId=@promocaoTipoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@promocaoTipoId", DbType.Int32, entidade.PromocaoTipoId);
			_db.AddInParameter(command, "@tipoPromocao", DbType.String, entidade.TipoPromocao);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um PromocaoTipo da base de dados.
        /// </summary>
        /// <param name="entidade">PromocaoTipo a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(PromocaoTipo entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM PromocaoTipo ");
			sbSQL.Append("WHERE promocaoTipoId=@promocaoTipoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@promocaoTipoId", DbType.Int32, entidade.PromocaoTipoId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um PromocaoTipo.
		/// </summary>
        /// <param name="entidade">PromocaoTipo a ser carregado (somente o identificador é necessário).</param>
		/// <returns>PromocaoTipo</returns>
		public PromocaoTipo Carregar(int promocaoTipoId) {		
			PromocaoTipo entidade = new PromocaoTipo();
			entidade.PromocaoTipoId = promocaoTipoId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um PromocaoTipo.
		/// </summary>
        /// <param name="entidade">PromocaoTipo a ser carregado (somente o identificador é necessário).</param>
		/// <returns>PromocaoTipo</returns>
		public PromocaoTipo Carregar(PromocaoTipo entidade) {		
		
			PromocaoTipo entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM PromocaoTipo WHERE promocaoTipoId=@promocaoTipoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@promocaoTipoId", DbType.Int32, entidade.PromocaoTipoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new PromocaoTipo();
				PopulaPromocaoTipo(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de PromocaoTipo.
        /// </summary>
        /// <param name="entidade">Promocao relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de PromocaoTipo.</returns>
		public IEnumerable<PromocaoTipo> Carregar(Promocao entidade)
		{		
			List<PromocaoTipo> entidadesRetorno = new List<PromocaoTipo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT PromocaoTipo.* FROM PromocaoTipo INNER JOIN Promocao ON PromocaoTipo.promocaoTipoId=Promocao.promocaoTipoId WHERE Promocao.promocaoId=@promocaoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@promocaoId", DbType.Int32, entidade.PromocaoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                PromocaoTipo entidadeRetorno = new PromocaoTipo();
                PopulaPromocaoTipo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de PromocaoTipo.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos PromocaoTipo.</returns>
		public IEnumerable<PromocaoTipo> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<PromocaoTipo> entidadesRetorno = new List<PromocaoTipo>();
			
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
				sbOrder.Append( " ORDER BY promocaoTipoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM PromocaoTipo");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM PromocaoTipo WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM PromocaoTipo ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT PromocaoTipo.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM PromocaoTipo ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT PromocaoTipo.* FROM PromocaoTipo ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                PromocaoTipo entidadeRetorno = new PromocaoTipo();
                PopulaPromocaoTipo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os PromocaoTipo existentes na base de dados.
        /// </summary>
		public IEnumerable<PromocaoTipo> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de PromocaoTipo na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de PromocaoTipo na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM PromocaoTipo");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um PromocaoTipo baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">PromocaoTipo a ser populado(.</param>
		public static void PopulaPromocaoTipo(IDataReader reader, PromocaoTipo entidade) 
		{						
			if (reader["promocaoTipoId"] != DBNull.Value)
				entidade.PromocaoTipoId = Convert.ToInt32(reader["promocaoTipoId"].ToString());
			
			if (reader["tipoPromocao"] != DBNull.Value)
				entidade.TipoPromocao = reader["tipoPromocao"].ToString();
			

		}		
		
	}
}
		