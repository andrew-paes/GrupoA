
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
	public partial class TituloSolicitacaoADO : ADOSuper, ITituloSolicitacaoDAL {
	
	    /// <summary>
        /// Método que persiste um TituloSolicitacao.
        /// </summary>
        /// <param name="entidade">TituloSolicitacao contendo os dados a serem persistidos.</param>	
		public void Inserir(TituloSolicitacao entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO TituloSolicitacao ");
			sbSQL.Append(" (professorId, tituloId, tituloSolicitacaoStatusId, dataSolicitacao, justificativaProfessor, exportada) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@professorId, @tituloId, @tituloSolicitacaoStatusId, @dataSolicitacao, @justificativaProfessor, @exportada) ");											

			sbSQL.Append(" ; SET @tituloSolicitacaoId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@tituloSolicitacaoId", DbType.Int32, 8);

			_db.AddInParameter(command, "@professorId", DbType.Int32, entidade.Professor.ProfessorId);

			_db.AddInParameter(command, "@tituloId", DbType.Int32, entidade.Titulo.TituloId);

			_db.AddInParameter(command, "@tituloSolicitacaoStatusId", DbType.Int32, entidade.TituloSolicitacaoStatus.TituloSolicitacaoStatusId);

			_db.AddInParameter(command, "@dataSolicitacao", DbType.DateTime, entidade.DataSolicitacao);

			_db.AddInParameter(command, "@justificativaProfessor", DbType.String, entidade.JustificativaProfessor);

			_db.AddInParameter(command, "@exportada", DbType.Int32, entidade.Exportada);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.TituloSolicitacaoId = Convert.ToInt32(_db.GetParameterValue(command, "@tituloSolicitacaoId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um TituloSolicitacao.
        /// </summary>
        /// <param name="entidade">TituloSolicitacao contendo os dados a serem atualizados.</param>
		public void Atualizar(TituloSolicitacao entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE TituloSolicitacao SET ");
			sbSQL.Append(" professorId=@professorId, tituloId=@tituloId, tituloSolicitacaoStatusId=@tituloSolicitacaoStatusId, dataSolicitacao=@dataSolicitacao, justificativaProfessor=@justificativaProfessor, exportada=@exportada ");
			sbSQL.Append(" WHERE tituloSolicitacaoId=@tituloSolicitacaoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@tituloSolicitacaoId", DbType.Int32, entidade.TituloSolicitacaoId);
			_db.AddInParameter(command, "@professorId", DbType.Int32, entidade.Professor.ProfessorId);
			_db.AddInParameter(command, "@tituloId", DbType.Int32, entidade.Titulo.TituloId);
			_db.AddInParameter(command, "@tituloSolicitacaoStatusId", DbType.Int32, entidade.TituloSolicitacaoStatus.TituloSolicitacaoStatusId);
			_db.AddInParameter(command, "@dataSolicitacao", DbType.DateTime, entidade.DataSolicitacao);
			_db.AddInParameter(command, "@justificativaProfessor", DbType.String, entidade.JustificativaProfessor);
			_db.AddInParameter(command, "@exportada", DbType.Int32, entidade.Exportada);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um TituloSolicitacao da base de dados.
        /// </summary>
        /// <param name="entidade">TituloSolicitacao a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(TituloSolicitacao entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM TituloSolicitacao ");
			sbSQL.Append("WHERE tituloSolicitacaoId=@tituloSolicitacaoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@tituloSolicitacaoId", DbType.Int32, entidade.TituloSolicitacaoId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um TituloSolicitacao.
		/// </summary>
        /// <param name="entidade">TituloSolicitacao a ser carregado (somente o identificador é necessário).</param>
		/// <returns>TituloSolicitacao</returns>
		public TituloSolicitacao Carregar(int tituloSolicitacaoId) {		
			TituloSolicitacao entidade = new TituloSolicitacao();
			entidade.TituloSolicitacaoId = tituloSolicitacaoId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um TituloSolicitacao.
		/// </summary>
        /// <param name="entidade">TituloSolicitacao a ser carregado (somente o identificador é necessário).</param>
		/// <returns>TituloSolicitacao</returns>
		public TituloSolicitacao Carregar(TituloSolicitacao entidade) {		
		
			TituloSolicitacao entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM TituloSolicitacao WHERE tituloSolicitacaoId=@tituloSolicitacaoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@tituloSolicitacaoId", DbType.Int32, entidade.TituloSolicitacaoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new TituloSolicitacao();
				PopulaTituloSolicitacao(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de TituloSolicitacao.
        /// </summary>
        /// <param name="entidade">TituloAvaliacao relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de TituloSolicitacao.</returns>
		public IEnumerable<TituloSolicitacao> Carregar(TituloAvaliacao entidade)
		{		
			List<TituloSolicitacao> entidadesRetorno = new List<TituloSolicitacao>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT TituloSolicitacao.* FROM TituloSolicitacao INNER JOIN TituloAvaliacao ON TituloSolicitacao.tituloSolicitacaoId=TituloAvaliacao.tituloSolicitacaoId WHERE TituloAvaliacao.tituloAvaliacaoId=@tituloAvaliacaoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@tituloAvaliacaoId", DbType.Int32, entidade.TituloAvaliacaoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                TituloSolicitacao entidadeRetorno = new TituloSolicitacao();
                PopulaTituloSolicitacao(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de TituloSolicitacao.
        /// </summary>
        /// <param name="entidade">Professor relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de TituloSolicitacao.</returns>
		public IEnumerable<TituloSolicitacao> Carregar(Professor entidade)
		{		
			List<TituloSolicitacao> entidadesRetorno = new List<TituloSolicitacao>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT TituloSolicitacao.* FROM TituloSolicitacao WHERE TituloSolicitacao.professorId=@professorId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@professorId", DbType.Int32, entidade.ProfessorId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                TituloSolicitacao entidadeRetorno = new TituloSolicitacao();
                PopulaTituloSolicitacao(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de TituloSolicitacao.
        /// </summary>
        /// <param name="entidade">Titulo relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de TituloSolicitacao.</returns>
		public IEnumerable<TituloSolicitacao> Carregar(Titulo entidade)
		{		
			List<TituloSolicitacao> entidadesRetorno = new List<TituloSolicitacao>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT TituloSolicitacao.* FROM TituloSolicitacao WHERE TituloSolicitacao.tituloId=@tituloId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@tituloId", DbType.Int32, entidade.TituloId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                TituloSolicitacao entidadeRetorno = new TituloSolicitacao();
                PopulaTituloSolicitacao(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de TituloSolicitacao.
        /// </summary>
        /// <param name="entidade">TituloSolicitacaoStatus relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de TituloSolicitacao.</returns>
		public IEnumerable<TituloSolicitacao> Carregar(TituloSolicitacaoStatus entidade)
		{		
			List<TituloSolicitacao> entidadesRetorno = new List<TituloSolicitacao>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT TituloSolicitacao.* FROM TituloSolicitacao WHERE TituloSolicitacao.tituloSolicitacaoStatusId=@tituloSolicitacaoStatusId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@tituloSolicitacaoStatusId", DbType.Int32, entidade.TituloSolicitacaoStatusId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                TituloSolicitacao entidadeRetorno = new TituloSolicitacao();
                PopulaTituloSolicitacao(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de TituloSolicitacao.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos TituloSolicitacao.</returns>
		public IEnumerable<TituloSolicitacao> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<TituloSolicitacao> entidadesRetorno = new List<TituloSolicitacao>();
			
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
				sbOrder.Append( " ORDER BY tituloSolicitacaoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM TituloSolicitacao");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM TituloSolicitacao WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM TituloSolicitacao ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT TituloSolicitacao.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM TituloSolicitacao ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT TituloSolicitacao.* FROM TituloSolicitacao ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                TituloSolicitacao entidadeRetorno = new TituloSolicitacao();
                PopulaTituloSolicitacao(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os TituloSolicitacao existentes na base de dados.
        /// </summary>
		public IEnumerable<TituloSolicitacao> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de TituloSolicitacao na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de TituloSolicitacao na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM TituloSolicitacao");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um TituloSolicitacao baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">TituloSolicitacao a ser populado(.</param>
		public static void PopulaTituloSolicitacao(IDataReader reader, TituloSolicitacao entidade) 
		{						
			if (reader["tituloSolicitacaoId"] != DBNull.Value)
				entidade.TituloSolicitacaoId = Convert.ToInt32(reader["tituloSolicitacaoId"].ToString());
			
			if (reader["dataSolicitacao"] != DBNull.Value)
				entidade.DataSolicitacao = Convert.ToDateTime(reader["dataSolicitacao"].ToString());
			
			if (reader["justificativaProfessor"] != DBNull.Value)
				entidade.JustificativaProfessor = reader["justificativaProfessor"].ToString();
			
			if (reader["exportada"] != DBNull.Value)
				entidade.Exportada = Convert.ToBoolean(reader["exportada"].ToString());
			
			if (reader["professorId"] != DBNull.Value) {
				entidade.Professor = new Professor();
				entidade.Professor.ProfessorId = Convert.ToInt32(reader["professorId"].ToString());
			}

			if (reader["tituloId"] != DBNull.Value) {
				entidade.Titulo = new Titulo();
				entidade.Titulo.TituloId = Convert.ToInt32(reader["tituloId"].ToString());
			}

			if (reader["tituloSolicitacaoStatusId"] != DBNull.Value) {
				entidade.TituloSolicitacaoStatus = new TituloSolicitacaoStatus();
				entidade.TituloSolicitacaoStatus.TituloSolicitacaoStatusId = Convert.ToInt32(reader["tituloSolicitacaoStatusId"].ToString());
			}


		}		
		
	}
}
		