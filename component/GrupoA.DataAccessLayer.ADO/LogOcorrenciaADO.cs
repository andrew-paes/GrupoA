
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
	public partial class LogOcorrenciaADO : ADOSuper, ILogOcorrenciaDAL
	{

		/// <summary>
		/// Método que persiste um LogOcorrencia.
		/// </summary>
		/// <param name="entidade">LogOcorrencia contendo os dados a serem persistidos.</param>	
		public void Inserir(LogOcorrencia entidade)
		{
			StringBuilder sbSQL = new StringBuilder();
			DbCommand command;

			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO LogOcorrencia ");
			sbSQL.Append(" (logEventoId, dataHoraOcorrencia, usuarioId, dados) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@logEventoId, @dataHoraOcorrencia, @usuarioId, @dados) ");

			sbSQL.Append(" ; SET @logOcorrenciaId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());

			_db.AddOutParameter(command, "@logOcorrenciaId", DbType.Int32, 8);

			_db.AddInParameter(command, "@logEventoId", DbType.Int32, entidade.LogEvento.LogEventoId);

			_db.AddInParameter(command, "@dataHoraOcorrencia", DbType.DateTime, entidade.DataHoraOcorrencia);

			if (entidade.Usuario != null)
				_db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.Usuario.UsuarioId);
			else
				_db.AddInParameter(command, "@usuarioId", DbType.Int32, null);

			_db.AddInParameter(command, "@dados", DbType.Xml, entidade.Dados.ToString());


			// Executa a query.
			_db.ExecuteNonQuery(command);

			entidade.LogOcorrenciaId = Convert.ToInt32(_db.GetParameterValue(command, "@logOcorrenciaId"));

		}

		/// <summary>
		/// Método que atualiza os dados de um LogOcorrencia.
		/// </summary>
		/// <param name="entidade">LogOcorrencia contendo os dados a serem atualizados.</param>
		public void Atualizar(LogOcorrencia entidade)
		{

			StringBuilder sbSQL = new StringBuilder();
			DbCommand command;

			// Monta a string de atualização.
			sbSQL.Append(" UPDATE LogOcorrencia SET ");
			sbSQL.Append(" logEventoId=@logEventoId, dataHoraOcorrencia=@dataHoraOcorrencia, usuarioId=@usuarioId, dados=@dados ");
			sbSQL.Append(" WHERE logOcorrenciaId=@logOcorrenciaId ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());

			// Parâmetros
			_db.AddInParameter(command, "@logOcorrenciaId", DbType.Int32, entidade.LogOcorrenciaId);
			_db.AddInParameter(command, "@logEventoId", DbType.Int32, entidade.LogEvento.LogEventoId);
			_db.AddInParameter(command, "@dataHoraOcorrencia", DbType.DateTime, entidade.DataHoraOcorrencia);
			if (entidade.Usuario != null)
				_db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.Usuario.UsuarioId);
			else
				_db.AddInParameter(command, "@usuarioId", DbType.Int32, null);
			_db.AddInParameter(command, "@dados", DbType.Xml, entidade.Dados);

			// Executa a query.
			_db.ExecuteNonQuery(command);

		}

		/// <summary>
		/// Método que remove um LogOcorrencia da base de dados.
		/// </summary>
		/// <param name="entidade">LogOcorrencia a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(LogOcorrencia entidade)
		{
			StringBuilder sbSQL = new StringBuilder();
			DbCommand command;

			sbSQL.Append("DELETE FROM LogOcorrencia ");
			sbSQL.Append("WHERE logOcorrenciaId=@logOcorrenciaId ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());

			_db.AddInParameter(command, "@logOcorrenciaId", DbType.Int32, entidade.LogOcorrenciaId);


			_db.ExecuteNonQuery(command);
		}

		/// <summary>
		/// Método que carrega um LogOcorrencia.
		/// </summary>
		/// <param name="entidade">LogOcorrencia a ser carregado (somente o identificador é necessário).</param>
		/// <returns>LogOcorrencia</returns>
		public LogOcorrencia Carregar(int logOcorrenciaId)
		{
			LogOcorrencia entidade = new LogOcorrencia();
			entidade.LogOcorrenciaId = logOcorrenciaId;
			return Carregar(entidade);

		}


		/// <summary>
		/// Método que carrega um LogOcorrencia.
		/// </summary>
		/// <param name="entidade">LogOcorrencia a ser carregado (somente o identificador é necessário).</param>
		/// <returns>LogOcorrencia</returns>
		public LogOcorrencia Carregar(LogOcorrencia entidade)
		{

			LogOcorrencia entidadeRetorno = null;

			StringBuilder sbSQL = new StringBuilder();

			sbSQL.Append("SELECT * FROM LogOcorrencia WHERE logOcorrenciaId=@logOcorrenciaId");

			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

			_db.AddInParameter(command, "@logOcorrenciaId", DbType.Int32, entidade.LogOcorrenciaId);

			IDataReader reader = _db.ExecuteReader(command);

			if (reader.Read())
			{
				entidadeRetorno = new LogOcorrencia();
				PopulaLogOcorrencia(reader, entidadeRetorno);
			}
			reader.Close();

			return entidadeRetorno;
		}



		/// <summary>
		/// Método que retorna uma coleção de LogOcorrencia.
		/// </summary>
		/// <param name="entidade">LogEvento relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de LogOcorrencia.</returns>
		public IEnumerable<LogOcorrencia> Carregar(LogEvento entidade)
		{
			List<LogOcorrencia> entidadesRetorno = new List<LogOcorrencia>();

			StringBuilder sbSQL = new StringBuilder();

			sbSQL.Append("SELECT LogOcorrencia.* FROM LogOcorrencia WHERE LogOcorrencia.logEventoId=@logEventoId");


			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@logEventoId", DbType.Int32, entidade.LogEventoId);

			IDataReader reader = _db.ExecuteReader(command);

			while (reader.Read())
			{
				LogOcorrencia entidadeRetorno = new LogOcorrencia();
				PopulaLogOcorrencia(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
			}
			reader.Close();

			return entidadesRetorno;

		}

		/// <summary>
		/// Método que retorna uma coleção de LogOcorrencia.
		/// </summary>
		/// <param name="entidade">Usuario relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de LogOcorrencia.</returns>
		public IEnumerable<LogOcorrencia> Carregar(Usuario entidade)
		{
			List<LogOcorrencia> entidadesRetorno = new List<LogOcorrencia>();

			StringBuilder sbSQL = new StringBuilder();

			sbSQL.Append("SELECT LogOcorrencia.* FROM LogOcorrencia WHERE LogOcorrencia.usuarioId=@usuarioId");


			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.UsuarioId);

			IDataReader reader = _db.ExecuteReader(command);

			while (reader.Read())
			{
				LogOcorrencia entidadeRetorno = new LogOcorrencia();
				PopulaLogOcorrencia(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
			}
			reader.Close();

			return entidadesRetorno;

		}


		/// <summary>
		/// Método que retorna uma coleção de LogOcorrencia.
		/// </summary>
		/// <param name="registrosPagina">Número máximo de registros na página.</param>
		/// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
		/// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
		/// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos LogOcorrencia.</returns>
		public IEnumerable<LogOcorrencia> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
		{

			List<LogOcorrencia> entidadesRetorno = new List<LogOcorrencia>();

			StringBuilder sbSQL = new StringBuilder();
			StringBuilder sbWhere = new StringBuilder();
			StringBuilder sbOrder = new StringBuilder();
			DbCommand command;
			IDataReader reader;

			// Monta o "OrderBy"
			if (ordemColunas != null)
			{
				for (int i = 0; i < ordemColunas.Length; i++)
				{
					if (sbOrder.Length > 0) { sbOrder.Append(", "); }
					sbOrder.Append(ordemColunas[i] + " " + ordemSentidos[i]);
				}
				if (sbOrder.Length > 0) { sbOrder.Insert(0, " ORDER BY "); }
			}
			else
			{
				sbOrder.Append(" ORDER BY logOcorrenciaId");
			}


			if (registrosPagina > 0)
			{

				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM LogOcorrencia");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM LogOcorrencia WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM LogOcorrencia ORDER BY " + orderBy + ")");				
				//}	
				sbSQL.Append("SELECT * FROM ( ");
				sbSQL.Append("SELECT LogOcorrencia.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM LogOcorrencia ");
				if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

			}
			else
			{
				sbSQL.Append("SELECT LogOcorrencia.* FROM LogOcorrencia ");
				if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
				if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
			}

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);

			while (reader.Read())
			{
				LogOcorrencia entidadeRetorno = new LogOcorrencia();
				PopulaLogOcorrencia(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
			}
			reader.Close();

			return entidadesRetorno;

		}

		/// <summary>
		/// Método que retorna todas os LogOcorrencia existentes na base de dados.
		/// </summary>
		public IEnumerable<LogOcorrencia> CarregarTodos()
		{
			return CarregarTodos(0, 0, null, null, null);
		}

		/// <summary>
		/// Método que retorna o total de LogOcorrencia na base de dados.
		/// </summary>
		/// <returns></returns>
		public int TotalRegistros()
		{
			return TotalRegistros(null);
		}

		/// <summary>
		/// Método que retorna o total de LogOcorrencia na base de dados, aceita filtro.
		/// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro)
		{
			StringBuilder sbSQL = new StringBuilder();

			sbSQL.Append("SELECT COUNT(*) AS Total FROM LogOcorrencia");

			if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
				sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");

			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

			// Executa a query.

			int resultado = (int)_db.ExecuteScalar(command);


			return resultado;
		}

		/// <summary>
		/// Método que retorna popula um LogOcorrencia baseado nos dados de um DataReader.
		/// </summary>
		/// <param name="reader">IDataReader contendo os dados da consulta.</param>
		/// <param name="entidade">LogOcorrencia a ser populado(.</param>
		public static void PopulaLogOcorrencia(IDataReader reader, LogOcorrencia entidade)
		{
			if (reader["logOcorrenciaId"] != DBNull.Value)
				entidade.LogOcorrenciaId = Convert.ToInt32(reader["logOcorrenciaId"].ToString());

			if (reader["dataHoraOcorrencia"] != DBNull.Value)
				entidade.DataHoraOcorrencia = Convert.ToDateTime(reader["dataHoraOcorrencia"].ToString());

			if (reader["dados"] != DBNull.Value)
				if (!String.IsNullOrEmpty(reader["dados"].ToString()))
					entidade.Dados = XDocument.Parse(reader["dados"].ToString());

			if (reader["logEventoId"] != DBNull.Value)
			{
				entidade.LogEvento = new LogEvento();
				entidade.LogEvento.LogEventoId = Convert.ToInt32(reader["logEventoId"].ToString());
			}

			if (reader["usuarioId"] != DBNull.Value)
			{
				entidade.Usuario = new Usuario();
				entidade.Usuario.UsuarioId = Convert.ToInt32(reader["usuarioId"].ToString());
			}


		}

	}
}
