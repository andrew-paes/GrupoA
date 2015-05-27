
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
	public partial class DestaqueTituloImpressoADO : ADOSuper, IDestaqueTituloImpressoDAL {
	
	    /// <summary>
        /// Método que persiste um DestaqueTituloImpresso.
        /// </summary>
        /// <param name="entidade">DestaqueTituloImpresso contendo os dados a serem persistidos.</param>	
		public void Inserir(DestaqueTituloImpresso entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO DestaqueTituloImpresso ");
			sbSQL.Append(" (destaqueTituloImpressoId, nomeArea) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@destaqueTituloImpressoId, @nomeArea) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@destaqueTituloImpressoId", DbType.Int32, entidade.DestaqueTituloImpressoId);

			_db.AddInParameter(command, "@nomeArea", DbType.String, entidade.NomeArea);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um DestaqueTituloImpresso.
        /// </summary>
        /// <param name="entidade">DestaqueTituloImpresso contendo os dados a serem atualizados.</param>
		public void Atualizar(DestaqueTituloImpresso entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE DestaqueTituloImpresso SET ");
			sbSQL.Append(" nomeArea=@nomeArea ");
			sbSQL.Append(" WHERE destaqueTituloImpressoId=@destaqueTituloImpressoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@destaqueTituloImpressoId", DbType.Int32, entidade.DestaqueTituloImpressoId);
			_db.AddInParameter(command, "@nomeArea", DbType.String, entidade.NomeArea);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um DestaqueTituloImpresso da base de dados.
        /// </summary>
        /// <param name="entidade">DestaqueTituloImpresso a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(DestaqueTituloImpresso entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM DestaqueTituloImpresso ");
			sbSQL.Append("WHERE destaqueTituloImpressoId=@destaqueTituloImpressoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@destaqueTituloImpressoId", DbType.Int32, entidade.DestaqueTituloImpressoId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um DestaqueTituloImpresso.
		/// </summary>
        /// <param name="entidade">DestaqueTituloImpresso a ser carregado (somente o identificador é necessário).</param>
		/// <returns>DestaqueTituloImpresso</returns>
		public DestaqueTituloImpresso Carregar(int destaqueTituloImpressoId) {		
			DestaqueTituloImpresso entidade = new DestaqueTituloImpresso();
			entidade.DestaqueTituloImpressoId = destaqueTituloImpressoId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um DestaqueTituloImpresso.
		/// </summary>
        /// <param name="entidade">DestaqueTituloImpresso a ser carregado (somente o identificador é necessário).</param>
		/// <returns>DestaqueTituloImpresso</returns>
		public DestaqueTituloImpresso Carregar(DestaqueTituloImpresso entidade) {		
		
			DestaqueTituloImpresso entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM DestaqueTituloImpresso WHERE destaqueTituloImpressoId=@destaqueTituloImpressoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@destaqueTituloImpressoId", DbType.Int32, entidade.DestaqueTituloImpressoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new DestaqueTituloImpresso();
				PopulaDestaqueTituloImpresso(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de DestaqueTituloImpresso.
        /// </summary>
        /// <param name="entidade">Titulo relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de DestaqueTituloImpresso.</returns>
		public IEnumerable<DestaqueTituloImpresso> Carregar(Titulo entidade)
		{		
			List<DestaqueTituloImpresso> entidadesRetorno = new List<DestaqueTituloImpresso>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT DestaqueTituloImpresso.* FROM DestaqueTituloImpresso INNER JOIN DestaqueTituloImpressoRelacionado ON DestaqueTituloImpresso.destaqueTituloImpressoId=DestaqueTituloImpressoRelacionado.destaqueTituloImpressoId WHERE DestaqueTituloImpressoRelacionado.tituloId=@tituloId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@tituloId", DbType.Int32, entidade.TituloId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                DestaqueTituloImpresso entidadeRetorno = new DestaqueTituloImpresso();
                PopulaDestaqueTituloImpresso(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de DestaqueTituloImpresso.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos DestaqueTituloImpresso.</returns>
		public IEnumerable<DestaqueTituloImpresso> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<DestaqueTituloImpresso> entidadesRetorno = new List<DestaqueTituloImpresso>();
			
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
				sbOrder.Append( " ORDER BY destaqueTituloImpressoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM DestaqueTituloImpresso");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM DestaqueTituloImpresso WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM DestaqueTituloImpresso ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT DestaqueTituloImpresso.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM DestaqueTituloImpresso ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT DestaqueTituloImpresso.* FROM DestaqueTituloImpresso ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                DestaqueTituloImpresso entidadeRetorno = new DestaqueTituloImpresso();
                PopulaDestaqueTituloImpresso(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os DestaqueTituloImpresso existentes na base de dados.
        /// </summary>
		public IEnumerable<DestaqueTituloImpresso> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de DestaqueTituloImpresso na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de DestaqueTituloImpresso na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM DestaqueTituloImpresso");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um DestaqueTituloImpresso baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">DestaqueTituloImpresso a ser populado(.</param>
		public static void PopulaDestaqueTituloImpresso(IDataReader reader, DestaqueTituloImpresso entidade) 
		{						
			if (reader["destaqueTituloImpressoId"] != DBNull.Value)
				entidade.DestaqueTituloImpressoId = Convert.ToInt32(reader["destaqueTituloImpressoId"].ToString());
			
			if (reader["nomeArea"] != DBNull.Value)
				entidade.NomeArea = reader["nomeArea"].ToString();
			

		}		
		
	}
}
		