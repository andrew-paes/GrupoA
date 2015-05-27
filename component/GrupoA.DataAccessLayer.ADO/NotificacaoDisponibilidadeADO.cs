
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
	public partial class NotificacaoDisponibilidadeADO : ADOSuper, INotificacaoDisponibilidadeDAL {
	
	    /// <summary>
        /// Método que persiste um NotificacaoDisponibilidade.
        /// </summary>
        /// <param name="entidade">NotificacaoDisponibilidade contendo os dados a serem persistidos.</param>	
		public void Inserir(NotificacaoDisponibilidade entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO NotificacaoDisponibilidade ");
			sbSQL.Append(" (produtoId, usuarioId, dataHoraSolicitacao, notificacaoStatusId) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@produtoId, @usuarioId, @dataHoraSolicitacao, @notificacaoStatusId) ");											

			sbSQL.Append(" ; SET @notificacaoDisponibilidadeId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@notificacaoDisponibilidadeId", DbType.Int32, 8);

			_db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.Produto.ProdutoId);

			_db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.Usuario.UsuarioId);

			_db.AddInParameter(command, "@dataHoraSolicitacao", DbType.DateTime, entidade.DataHoraSolicitacao);

			_db.AddInParameter(command, "@notificacaoStatusId", DbType.Int32, entidade.NotificacaoStatus.NotificacaoStatusId);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.NotificacaoDisponibilidadeId = Convert.ToInt32(_db.GetParameterValue(command, "@notificacaoDisponibilidadeId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um NotificacaoDisponibilidade.
        /// </summary>
        /// <param name="entidade">NotificacaoDisponibilidade contendo os dados a serem atualizados.</param>
		public void Atualizar(NotificacaoDisponibilidade entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE NotificacaoDisponibilidade SET ");
			sbSQL.Append(" produtoId=@produtoId, usuarioId=@usuarioId, dataHoraSolicitacao=@dataHoraSolicitacao, notificacaoStatusId=@notificacaoStatusId ");
			sbSQL.Append(" WHERE notificacaoDisponibilidadeId=@notificacaoDisponibilidadeId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@notificacaoDisponibilidadeId", DbType.Int32, entidade.NotificacaoDisponibilidadeId);
			_db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.Produto.ProdutoId);
			_db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.Usuario.UsuarioId);
			_db.AddInParameter(command, "@dataHoraSolicitacao", DbType.DateTime, entidade.DataHoraSolicitacao);
			_db.AddInParameter(command, "@notificacaoStatusId", DbType.Int32, entidade.NotificacaoStatus.NotificacaoStatusId);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um NotificacaoDisponibilidade da base de dados.
        /// </summary>
        /// <param name="entidade">NotificacaoDisponibilidade a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(NotificacaoDisponibilidade entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM NotificacaoDisponibilidade ");
			sbSQL.Append("WHERE notificacaoDisponibilidadeId=@notificacaoDisponibilidadeId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@notificacaoDisponibilidadeId", DbType.Int32, entidade.NotificacaoDisponibilidadeId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um NotificacaoDisponibilidade.
		/// </summary>
        /// <param name="entidade">NotificacaoDisponibilidade a ser carregado (somente o identificador é necessário).</param>
		/// <returns>NotificacaoDisponibilidade</returns>
		public NotificacaoDisponibilidade Carregar(int notificacaoDisponibilidadeId) {		
			NotificacaoDisponibilidade entidade = new NotificacaoDisponibilidade();
			entidade.NotificacaoDisponibilidadeId = notificacaoDisponibilidadeId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um NotificacaoDisponibilidade.
		/// </summary>
        /// <param name="entidade">NotificacaoDisponibilidade a ser carregado (somente o identificador é necessário).</param>
		/// <returns>NotificacaoDisponibilidade</returns>
		public NotificacaoDisponibilidade Carregar(NotificacaoDisponibilidade entidade) {		
		
			NotificacaoDisponibilidade entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM NotificacaoDisponibilidade WHERE notificacaoDisponibilidadeId=@notificacaoDisponibilidadeId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@notificacaoDisponibilidadeId", DbType.Int32, entidade.NotificacaoDisponibilidadeId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new NotificacaoDisponibilidade();
				PopulaNotificacaoDisponibilidade(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de NotificacaoDisponibilidade.
        /// </summary>
        /// <param name="entidade">NotificacaoStatus relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de NotificacaoDisponibilidade.</returns>
		public IEnumerable<NotificacaoDisponibilidade> Carregar(NotificacaoStatus entidade)
		{		
			List<NotificacaoDisponibilidade> entidadesRetorno = new List<NotificacaoDisponibilidade>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT NotificacaoDisponibilidade.* FROM NotificacaoDisponibilidade WHERE NotificacaoDisponibilidade.notificacaoStatusId=@notificacaoStatusId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@notificacaoStatusId", DbType.Int32, entidade.NotificacaoStatusId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                NotificacaoDisponibilidade entidadeRetorno = new NotificacaoDisponibilidade();
                PopulaNotificacaoDisponibilidade(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de NotificacaoDisponibilidade.
        /// </summary>
        /// <param name="entidade">Produto relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de NotificacaoDisponibilidade.</returns>
		public IEnumerable<NotificacaoDisponibilidade> Carregar(Produto entidade)
		{		
			List<NotificacaoDisponibilidade> entidadesRetorno = new List<NotificacaoDisponibilidade>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT NotificacaoDisponibilidade.* FROM NotificacaoDisponibilidade WHERE NotificacaoDisponibilidade.produtoId=@produtoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.ProdutoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                NotificacaoDisponibilidade entidadeRetorno = new NotificacaoDisponibilidade();
                PopulaNotificacaoDisponibilidade(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de NotificacaoDisponibilidade.
        /// </summary>
        /// <param name="entidade">Usuario relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de NotificacaoDisponibilidade.</returns>
		public IEnumerable<NotificacaoDisponibilidade> Carregar(Usuario entidade)
		{		
			List<NotificacaoDisponibilidade> entidadesRetorno = new List<NotificacaoDisponibilidade>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT NotificacaoDisponibilidade.* FROM NotificacaoDisponibilidade WHERE NotificacaoDisponibilidade.usuarioId=@usuarioId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.UsuarioId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                NotificacaoDisponibilidade entidadeRetorno = new NotificacaoDisponibilidade();
                PopulaNotificacaoDisponibilidade(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de NotificacaoDisponibilidade.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos NotificacaoDisponibilidade.</returns>
		public IEnumerable<NotificacaoDisponibilidade> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<NotificacaoDisponibilidade> entidadesRetorno = new List<NotificacaoDisponibilidade>();
			
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
				sbOrder.Append( " ORDER BY notificacaoDisponibilidadeId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM NotificacaoDisponibilidade");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM NotificacaoDisponibilidade WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM NotificacaoDisponibilidade ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT NotificacaoDisponibilidade.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM NotificacaoDisponibilidade ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT NotificacaoDisponibilidade.* FROM NotificacaoDisponibilidade ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                NotificacaoDisponibilidade entidadeRetorno = new NotificacaoDisponibilidade();
                PopulaNotificacaoDisponibilidade(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os NotificacaoDisponibilidade existentes na base de dados.
        /// </summary>
		public IEnumerable<NotificacaoDisponibilidade> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de NotificacaoDisponibilidade na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de NotificacaoDisponibilidade na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM NotificacaoDisponibilidade");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um NotificacaoDisponibilidade baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">NotificacaoDisponibilidade a ser populado(.</param>
		public static void PopulaNotificacaoDisponibilidade(IDataReader reader, NotificacaoDisponibilidade entidade) 
		{						
			if (reader["notificacaoDisponibilidadeId"] != DBNull.Value)
				entidade.NotificacaoDisponibilidadeId = Convert.ToInt32(reader["notificacaoDisponibilidadeId"].ToString());
			
			if (reader["dataHoraSolicitacao"] != DBNull.Value)
				entidade.DataHoraSolicitacao = Convert.ToDateTime(reader["dataHoraSolicitacao"].ToString());
			
			if (reader["produtoId"] != DBNull.Value) {
				entidade.Produto = new Produto();
				entidade.Produto.ProdutoId = Convert.ToInt32(reader["produtoId"].ToString());
			}

			if (reader["usuarioId"] != DBNull.Value) {
				entidade.Usuario = new Usuario();
				entidade.Usuario.UsuarioId = Convert.ToInt32(reader["usuarioId"].ToString());
			}

			if (reader["notificacaoStatusId"] != DBNull.Value) {
				entidade.NotificacaoStatus = new NotificacaoStatus();
				entidade.NotificacaoStatus.NotificacaoStatusId = Convert.ToInt32(reader["notificacaoStatusId"].ToString());
			}


		}		
		
	}
}
		