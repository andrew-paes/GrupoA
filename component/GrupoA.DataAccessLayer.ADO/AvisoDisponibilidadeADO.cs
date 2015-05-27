
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
	public partial class AvisoDisponibilidadeADO : ADOSuper, IAvisoDisponibilidadeDAL {
	
	    /// <summary>
        /// Método que persiste um AvisoDisponibilidade.
        /// </summary>
        /// <param name="entidade">AvisoDisponibilidade contendo os dados a serem persistidos.</param>	
		public void Inserir(AvisoDisponibilidade entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO AvisoDisponibilidade ");
			sbSQL.Append(" (email, dataSolicitacao, dataNotificacao, produtoId, avisoDisponibilidadeStatusId) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@email, @dataSolicitacao, @dataNotificacao, @produtoId, @avisoDisponibilidadeStatusId) ");											

			sbSQL.Append(" ; SET @avisoDisponibilidadeId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@avisoDisponibilidadeId", DbType.Int32, 8);

			_db.AddInParameter(command, "@email", DbType.String, entidade.Email);

			if (entidade.DataSolicitacao != null && entidade.DataSolicitacao != DateTime.MinValue ) 
				_db.AddInParameter(command, "@dataSolicitacao", DbType.DateTime, entidade.DataSolicitacao);
			else
				_db.AddInParameter(command, "@dataSolicitacao", DbType.DateTime, null);

			if (entidade.DataNotificacao != null && entidade.DataNotificacao != DateTime.MinValue ) 
				_db.AddInParameter(command, "@dataNotificacao", DbType.DateTime, entidade.DataNotificacao);
			else
				_db.AddInParameter(command, "@dataNotificacao", DbType.DateTime, null);

			_db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.Produto.ProdutoId);

			_db.AddInParameter(command, "@avisoDisponibilidadeStatusId", DbType.Int32, entidade.AvisoDisponibilidadeStatus.AvisoDisponibilidadeStatusId);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.AvisoDisponibilidadeId = Convert.ToInt32(_db.GetParameterValue(command, "@avisoDisponibilidadeId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um AvisoDisponibilidade.
        /// </summary>
        /// <param name="entidade">AvisoDisponibilidade contendo os dados a serem atualizados.</param>
		public void Atualizar(AvisoDisponibilidade entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE AvisoDisponibilidade SET ");
			sbSQL.Append(" email=@email, dataSolicitacao=@dataSolicitacao, dataNotificacao=@dataNotificacao, produtoId=@produtoId, avisoDisponibilidadeStatusId=@avisoDisponibilidadeStatusId ");
			sbSQL.Append(" WHERE avisoDisponibilidadeId=@avisoDisponibilidadeId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@avisoDisponibilidadeId", DbType.Int32, entidade.AvisoDisponibilidadeId);
			_db.AddInParameter(command, "@email", DbType.String, entidade.Email);
			if (entidade.DataSolicitacao != null && entidade.DataSolicitacao != DateTime.MinValue ) 
				_db.AddInParameter(command, "@dataSolicitacao", DbType.DateTime, entidade.DataSolicitacao);
			else
				_db.AddInParameter(command, "@dataSolicitacao", DbType.DateTime, null);
			if (entidade.DataNotificacao != null && entidade.DataNotificacao != DateTime.MinValue ) 
				_db.AddInParameter(command, "@dataNotificacao", DbType.DateTime, entidade.DataNotificacao);
			else
				_db.AddInParameter(command, "@dataNotificacao", DbType.DateTime, null);
			_db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.Produto.ProdutoId);
			_db.AddInParameter(command, "@avisoDisponibilidadeStatusId", DbType.Int32, entidade.AvisoDisponibilidadeStatus.AvisoDisponibilidadeStatusId);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um AvisoDisponibilidade da base de dados.
        /// </summary>
        /// <param name="entidade">AvisoDisponibilidade a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(AvisoDisponibilidade entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM AvisoDisponibilidade ");
			sbSQL.Append("WHERE avisoDisponibilidadeId=@avisoDisponibilidadeId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@avisoDisponibilidadeId", DbType.Int32, entidade.AvisoDisponibilidadeId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um AvisoDisponibilidade.
		/// </summary>
        /// <param name="entidade">AvisoDisponibilidade a ser carregado (somente o identificador é necessário).</param>
		/// <returns>AvisoDisponibilidade</returns>
		public AvisoDisponibilidade Carregar(int avisoDisponibilidadeId) {		
			AvisoDisponibilidade entidade = new AvisoDisponibilidade();
			entidade.AvisoDisponibilidadeId = avisoDisponibilidadeId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um AvisoDisponibilidade.
		/// </summary>
        /// <param name="entidade">AvisoDisponibilidade a ser carregado (somente o identificador é necessário).</param>
		/// <returns>AvisoDisponibilidade</returns>
		public AvisoDisponibilidade Carregar(AvisoDisponibilidade entidade) {		
		
			AvisoDisponibilidade entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM AvisoDisponibilidade WHERE avisoDisponibilidadeId=@avisoDisponibilidadeId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@avisoDisponibilidadeId", DbType.Int32, entidade.AvisoDisponibilidadeId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new AvisoDisponibilidade();
				PopulaAvisoDisponibilidade(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de AvisoDisponibilidade.
        /// </summary>
        /// <param name="entidade">AvisoDisponibilidadeStatus relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de AvisoDisponibilidade.</returns>
		public IEnumerable<AvisoDisponibilidade> Carregar(AvisoDisponibilidadeStatus entidade)
		{		
			List<AvisoDisponibilidade> entidadesRetorno = new List<AvisoDisponibilidade>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT AvisoDisponibilidade.* FROM AvisoDisponibilidade WHERE AvisoDisponibilidade.avisoDisponibilidadeStatusId=@avisoDisponibilidadeStatusId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@avisoDisponibilidadeStatusId", DbType.Int32, entidade.AvisoDisponibilidadeStatusId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                AvisoDisponibilidade entidadeRetorno = new AvisoDisponibilidade();
                PopulaAvisoDisponibilidade(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de AvisoDisponibilidade.
        /// </summary>
        /// <param name="entidade">Produto relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de AvisoDisponibilidade.</returns>
		public IEnumerable<AvisoDisponibilidade> Carregar(Produto entidade)
		{		
			List<AvisoDisponibilidade> entidadesRetorno = new List<AvisoDisponibilidade>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT AvisoDisponibilidade.* FROM AvisoDisponibilidade WHERE AvisoDisponibilidade.produtoId=@produtoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.ProdutoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                AvisoDisponibilidade entidadeRetorno = new AvisoDisponibilidade();
                PopulaAvisoDisponibilidade(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de AvisoDisponibilidade.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos AvisoDisponibilidade.</returns>
		public IEnumerable<AvisoDisponibilidade> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<AvisoDisponibilidade> entidadesRetorno = new List<AvisoDisponibilidade>();
			
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
				sbOrder.Append( " ORDER BY avisoDisponibilidadeId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM AvisoDisponibilidade");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM AvisoDisponibilidade WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM AvisoDisponibilidade ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT AvisoDisponibilidade.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM AvisoDisponibilidade ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT AvisoDisponibilidade.* FROM AvisoDisponibilidade ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                AvisoDisponibilidade entidadeRetorno = new AvisoDisponibilidade();
                PopulaAvisoDisponibilidade(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os AvisoDisponibilidade existentes na base de dados.
        /// </summary>
		public IEnumerable<AvisoDisponibilidade> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de AvisoDisponibilidade na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de AvisoDisponibilidade na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM AvisoDisponibilidade");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um AvisoDisponibilidade baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">AvisoDisponibilidade a ser populado(.</param>
		public static void PopulaAvisoDisponibilidade(IDataReader reader, AvisoDisponibilidade entidade) 
		{						
			if (reader["avisoDisponibilidadeId"] != DBNull.Value)
				entidade.AvisoDisponibilidadeId = Convert.ToInt32(reader["avisoDisponibilidadeId"].ToString());
			
			if (reader["email"] != DBNull.Value)
				entidade.Email = reader["email"].ToString();
			
			if (reader["dataSolicitacao"] != DBNull.Value)
				entidade.DataSolicitacao = Convert.ToDateTime(reader["dataSolicitacao"].ToString());
			
			if (reader["dataNotificacao"] != DBNull.Value)
				entidade.DataNotificacao = Convert.ToDateTime(reader["dataNotificacao"].ToString());
			
			if (reader["produtoId"] != DBNull.Value) {
				entidade.Produto = new Produto();
				entidade.Produto.ProdutoId = Convert.ToInt32(reader["produtoId"].ToString());
			}

			if (reader["avisoDisponibilidadeStatusId"] != DBNull.Value) {
				entidade.AvisoDisponibilidadeStatus = new AvisoDisponibilidadeStatus();
				entidade.AvisoDisponibilidadeStatus.AvisoDisponibilidadeStatusId = Convert.ToInt32(reader["avisoDisponibilidadeStatusId"].ToString());
			}


		}		
		
	}
}
		