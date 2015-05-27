
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
	public partial class UsuarioControleADO : ADOSuper, IUsuarioControleDAL {
	
	    /// <summary>
        /// Método que persiste um UsuarioControle.
        /// </summary>
        /// <param name="entidade">UsuarioControle contendo os dados a serem persistidos.</param>	
		public void Inserir(UsuarioControle entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO UsuarioControle ");
			sbSQL.Append(" (usuarioId, dataHoraUltimaSincronia, realizarSincronizacao, customerId, prospectId) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@usuarioId, @dataHoraUltimaSincronia, @realizarSincronizacao, @customerId, @prospectId) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.UsuarioId);

			if (entidade.DataHoraUltimaSincronia != null && entidade.DataHoraUltimaSincronia != DateTime.MinValue ) 
				_db.AddInParameter(command, "@dataHoraUltimaSincronia", DbType.DateTime, entidade.DataHoraUltimaSincronia);
			else
				_db.AddInParameter(command, "@dataHoraUltimaSincronia", DbType.DateTime, null);

			_db.AddInParameter(command, "@realizarSincronizacao", DbType.Int32, entidade.RealizarSincronizacao);

			if (entidade.CustomerId != null ) 
				_db.AddInParameter(command, "@customerId", DbType.String, entidade.CustomerId);
			else
				_db.AddInParameter(command, "@customerId", DbType.String, null);

			if (entidade.ProspectId != null ) 
				_db.AddInParameter(command, "@prospectId", DbType.String, entidade.ProspectId);
			else
				_db.AddInParameter(command, "@prospectId", DbType.String, null);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}

        public void InserirOuAtualizar(UsuarioControle entidade)
        {
            var usuarioControle = this.Carregar(entidade);

            if (usuarioControle != null && usuarioControle.UsuarioId > 0)
            {
                usuarioControle.RealizarSincronizacao = true;
                this.Atualizar(usuarioControle);
            }
            else
            {
                this.Inserir(entidade);
            }

        }
		
        /// <summary>
        /// Método que atualiza os dados de um UsuarioControle.
        /// </summary>
        /// <param name="entidade">UsuarioControle contendo os dados a serem atualizados.</param>
		public void Atualizar(UsuarioControle entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE UsuarioControle SET ");
			sbSQL.Append(" dataHoraUltimaSincronia=@dataHoraUltimaSincronia, realizarSincronizacao=@realizarSincronizacao, customerId=@customerId, prospectId=@prospectId ");
			sbSQL.Append(" WHERE usuarioId=@usuarioId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.UsuarioId);
			if (entidade.DataHoraUltimaSincronia != null && entidade.DataHoraUltimaSincronia != DateTime.MinValue ) 
				_db.AddInParameter(command, "@dataHoraUltimaSincronia", DbType.DateTime, entidade.DataHoraUltimaSincronia);
			else
				_db.AddInParameter(command, "@dataHoraUltimaSincronia", DbType.DateTime, null);
			_db.AddInParameter(command, "@realizarSincronizacao", DbType.Int32, entidade.RealizarSincronizacao);
			if (entidade.CustomerId != null ) 
				_db.AddInParameter(command, "@customerId", DbType.String, entidade.CustomerId);
			else
				_db.AddInParameter(command, "@customerId", DbType.String, null);
			if (entidade.ProspectId != null ) 
				_db.AddInParameter(command, "@prospectId", DbType.String, entidade.ProspectId);
			else
				_db.AddInParameter(command, "@prospectId", DbType.String, null);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um UsuarioControle da base de dados.
        /// </summary>
        /// <param name="entidade">UsuarioControle a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(UsuarioControle entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM UsuarioControle ");
			sbSQL.Append("WHERE usuarioId=@usuarioId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.UsuarioId);

								
			_db.ExecuteNonQuery(command);
		}
		

		/// <summary>
		/// Método que carrega um UsuarioControle.
		/// </summary>
        /// <param name="entidade">UsuarioControle a ser carregado (somente o identificador é necessário).</param>
		/// <returns>UsuarioControle</returns>
		public UsuarioControle Carregar(UsuarioControle entidade) {		
		
			UsuarioControle entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM UsuarioControle WHERE usuarioId=@usuarioId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.UsuarioId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new UsuarioControle();
				PopulaUsuarioControle(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		
		
		/// <summary>
		/// Método que carrega um UsuarioControle com suas dependências.
		/// </summary>
        /// <param name="entidade">UsuarioControle a ser carregado (somente o identificador é necessário).</param>
		/// <returns>UsuarioControle</returns>
		public UsuarioControle CarregarComDependencias(UsuarioControle entidade) {		
		
			UsuarioControle entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append("SELECT UsuarioControle.usuarioId, UsuarioControle.dataHoraUltimaSincronia, UsuarioControle.realizarSincronizacao, UsuarioControle.customerId, UsuarioControle.prospectId");
			sbSQL.Append(", tipoPessoa, sexo, ativo, nomeUsuario, cadastroPessoa, emailUsuario, login, dataNascimento, dataHoraCadastro, optinSMS, optinNewsletter, codigoUsuario, profissionalOcupacaoId, senha");
			sbSQL.Append(" FROM UsuarioControle");
			sbSQL.Append(" INNER JOIN Usuario ON UsuarioControle.usuarioId=Usuario.usuarioId");
			sbSQL.Append(" WHERE UsuarioControle.usuarioId=@usuarioId");
						
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.UsuarioId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new UsuarioControle();
				PopulaUsuarioControle(reader, entidadeRetorno);
				entidadeRetorno.Usuario = new Usuario();
				UsuarioADO.PopulaUsuario(reader, entidadeRetorno.Usuario);
			}
			reader.Close();
			
			return entidadeRetorno;
		}		

		
		
		/// <summary>
        /// Método que retorna uma coleção de UsuarioControle.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos UsuarioControle.</returns>
		public IEnumerable<UsuarioControle> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<UsuarioControle> entidadesRetorno = new List<UsuarioControle>();
			
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
				sbOrder.Append( " ORDER BY usuarioId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM UsuarioControle");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM UsuarioControle WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM UsuarioControle ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT UsuarioControle.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM UsuarioControle ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT UsuarioControle.* FROM UsuarioControle ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                UsuarioControle entidadeRetorno = new UsuarioControle();
                PopulaUsuarioControle(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os UsuarioControle existentes na base de dados.
        /// </summary>
		public IEnumerable<UsuarioControle> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de UsuarioControle na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de UsuarioControle na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM UsuarioControle");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um UsuarioControle baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">UsuarioControle a ser populado(.</param>
		public static void PopulaUsuarioControle(IDataReader reader, UsuarioControle entidade) 
		{						
			if (reader["dataHoraUltimaSincronia"] != DBNull.Value)
				entidade.DataHoraUltimaSincronia = Convert.ToDateTime(reader["dataHoraUltimaSincronia"].ToString());
			
			if (reader["realizarSincronizacao"] != DBNull.Value)
				entidade.RealizarSincronizacao = Convert.ToBoolean(reader["realizarSincronizacao"].ToString());
			
			if (reader["customerId"] != DBNull.Value)
				entidade.CustomerId = reader["customerId"].ToString();
			
			if (reader["prospectId"] != DBNull.Value)
				entidade.ProspectId = reader["prospectId"].ToString();
			
			if (reader["usuarioId"] != DBNull.Value) {
				entidade.UsuarioId = Convert.ToInt32(reader["usuarioId"].ToString());
			}


		}		
		
	}
}
		