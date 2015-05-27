
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
	public partial class EventoAlertaADO : ADOSuper, IEventoAlertaDAL {
	
	    /// <summary>
        /// Método que persiste um EventoAlerta.
        /// </summary>
        /// <param name="entidade">EventoAlerta contendo os dados a serem persistidos.</param>	
		public void Inserir(EventoAlerta entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO EventoAlerta ");
			sbSQL.Append(" (usuarioId, eventoId, dias, ativo, dataHoraEncaminhamento, dataHoraCancelamento) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@usuarioId, @eventoId, @dias, @ativo, @dataHoraEncaminhamento, @dataHoraCancelamento) ");											

			sbSQL.Append(" ; SET @eventoAlertaId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@eventoAlertaId", DbType.Int32, 8);

			_db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.Usuario.UsuarioId);

			_db.AddInParameter(command, "@eventoId", DbType.Int32, entidade.Evento.EventoId);

			_db.AddInParameter(command, "@dias", DbType.Int32, entidade.Dias);

			_db.AddInParameter(command, "@ativo", DbType.Int32, entidade.Ativo);

			if (entidade.DataHoraEncaminhamento != null && entidade.DataHoraEncaminhamento != DateTime.MinValue ) 
				_db.AddInParameter(command, "@dataHoraEncaminhamento", DbType.DateTime, entidade.DataHoraEncaminhamento);
			else
				_db.AddInParameter(command, "@dataHoraEncaminhamento", DbType.DateTime, null);

			if (entidade.DataHoraCancelamento != null && entidade.DataHoraCancelamento != DateTime.MinValue ) 
				_db.AddInParameter(command, "@dataHoraCancelamento", DbType.DateTime, entidade.DataHoraCancelamento);
			else
				_db.AddInParameter(command, "@dataHoraCancelamento", DbType.DateTime, null);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.EventoAlertaId = Convert.ToInt32(_db.GetParameterValue(command, "@eventoAlertaId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um EventoAlerta.
        /// </summary>
        /// <param name="entidade">EventoAlerta contendo os dados a serem atualizados.</param>
		public void Atualizar(EventoAlerta entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE EventoAlerta SET ");
			sbSQL.Append(" usuarioId=@usuarioId, eventoId=@eventoId, dias=@dias, ativo=@ativo, dataHoraEncaminhamento=@dataHoraEncaminhamento, dataHoraCancelamento=@dataHoraCancelamento ");
			sbSQL.Append(" WHERE eventoAlertaId=@eventoAlertaId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@eventoAlertaId", DbType.Int32, entidade.EventoAlertaId);
			_db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.Usuario.UsuarioId);
			_db.AddInParameter(command, "@eventoId", DbType.Int32, entidade.Evento.EventoId);
			_db.AddInParameter(command, "@dias", DbType.Int32, entidade.Dias);
			_db.AddInParameter(command, "@ativo", DbType.Int32, entidade.Ativo);
			if (entidade.DataHoraEncaminhamento != null && entidade.DataHoraEncaminhamento != DateTime.MinValue ) 
				_db.AddInParameter(command, "@dataHoraEncaminhamento", DbType.DateTime, entidade.DataHoraEncaminhamento);
			else
				_db.AddInParameter(command, "@dataHoraEncaminhamento", DbType.DateTime, null);
			if (entidade.DataHoraCancelamento != null && entidade.DataHoraCancelamento != DateTime.MinValue ) 
				_db.AddInParameter(command, "@dataHoraCancelamento", DbType.DateTime, entidade.DataHoraCancelamento);
			else
				_db.AddInParameter(command, "@dataHoraCancelamento", DbType.DateTime, null);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um EventoAlerta da base de dados.
        /// </summary>
        /// <param name="entidade">EventoAlerta a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(EventoAlerta entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM EventoAlerta ");
			sbSQL.Append("WHERE eventoAlertaId=@eventoAlertaId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@eventoAlertaId", DbType.Int32, entidade.EventoAlertaId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um EventoAlerta.
		/// </summary>
        /// <param name="entidade">EventoAlerta a ser carregado (somente o identificador é necessário).</param>
		/// <returns>EventoAlerta</returns>
		public EventoAlerta Carregar(int eventoAlertaId) {		
			EventoAlerta entidade = new EventoAlerta();
			entidade.EventoAlertaId = eventoAlertaId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um EventoAlerta.
		/// </summary>
        /// <param name="entidade">EventoAlerta a ser carregado (somente o identificador é necessário).</param>
		/// <returns>EventoAlerta</returns>
		public EventoAlerta Carregar(EventoAlerta entidade) {		
		
			EventoAlerta entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM EventoAlerta WHERE eventoAlertaId=@eventoAlertaId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@eventoAlertaId", DbType.Int32, entidade.EventoAlertaId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new EventoAlerta();
				PopulaEventoAlerta(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de EventoAlerta.
        /// </summary>
        /// <param name="entidade">Evento relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de EventoAlerta.</returns>
		public IEnumerable<EventoAlerta> Carregar(Evento entidade)
		{		
			List<EventoAlerta> entidadesRetorno = new List<EventoAlerta>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT EventoAlerta.* FROM EventoAlerta WHERE EventoAlerta.eventoId=@eventoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@eventoId", DbType.Int32, entidade.EventoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                EventoAlerta entidadeRetorno = new EventoAlerta();
                PopulaEventoAlerta(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de EventoAlerta.
        /// </summary>
        /// <param name="entidade">Usuario relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de EventoAlerta.</returns>
		public IEnumerable<EventoAlerta> Carregar(Usuario entidade)
		{		
			List<EventoAlerta> entidadesRetorno = new List<EventoAlerta>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT EventoAlerta.* FROM EventoAlerta WHERE EventoAlerta.usuarioId=@usuarioId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.UsuarioId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                EventoAlerta entidadeRetorno = new EventoAlerta();
                PopulaEventoAlerta(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de EventoAlerta.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos EventoAlerta.</returns>
		public IEnumerable<EventoAlerta> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<EventoAlerta> entidadesRetorno = new List<EventoAlerta>();
			
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
				sbOrder.Append( " ORDER BY eventoAlertaId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM EventoAlerta");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM EventoAlerta WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM EventoAlerta ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT EventoAlerta.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM EventoAlerta ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT EventoAlerta.* FROM EventoAlerta ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                EventoAlerta entidadeRetorno = new EventoAlerta();
                PopulaEventoAlerta(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os EventoAlerta existentes na base de dados.
        /// </summary>
		public IEnumerable<EventoAlerta> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de EventoAlerta na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de EventoAlerta na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM EventoAlerta");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um EventoAlerta baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">EventoAlerta a ser populado(.</param>
		public static void PopulaEventoAlerta(IDataReader reader, EventoAlerta entidade) 
		{						
			if (reader["eventoAlertaId"] != DBNull.Value)
				entidade.EventoAlertaId = Convert.ToInt32(reader["eventoAlertaId"].ToString());
			
			if (reader["dias"] != DBNull.Value)
				entidade.Dias = Convert.ToInt32(reader["dias"].ToString());
			
			if (reader["ativo"] != DBNull.Value)
				entidade.Ativo = Convert.ToBoolean(reader["ativo"].ToString());
			
			if (reader["dataHoraEncaminhamento"] != DBNull.Value)
				entidade.DataHoraEncaminhamento = Convert.ToDateTime(reader["dataHoraEncaminhamento"].ToString());
			
			if (reader["dataHoraCancelamento"] != DBNull.Value)
				entidade.DataHoraCancelamento = Convert.ToDateTime(reader["dataHoraCancelamento"].ToString());
			
			if (reader["usuarioId"] != DBNull.Value) {
				entidade.Usuario = new Usuario();
				entidade.Usuario.UsuarioId = Convert.ToInt32(reader["usuarioId"].ToString());
			}

			if (reader["eventoId"] != DBNull.Value) {
				entidade.Evento = new Evento();
				entidade.Evento.EventoId = Convert.ToInt32(reader["eventoId"].ToString());
			}


		}		
		
	}
}
		