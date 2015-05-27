
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
	public partial class EventoImagemADO : ADOSuper, IEventoImagemDAL {
	
	    /// <summary>
        /// Método que persiste um EventoImagem.
        /// </summary>
        /// <param name="entidade">EventoImagem contendo os dados a serem persistidos.</param>	
		public void Inserir(EventoImagem entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO EventoImagem ");
			sbSQL.Append(" (eventoId, arquivoId, ordemApresentacao) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@eventoId, @arquivoId, @ordemApresentacao) ");											

			sbSQL.Append(" ; SET @eventoImagemId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@eventoImagemId", DbType.Int32, 8);

			_db.AddInParameter(command, "@eventoId", DbType.Int32, entidade.Evento.EventoId);

			_db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.Arquivo.ArquivoId);

			_db.AddInParameter(command, "@ordemApresentacao", DbType.Int32, entidade.OrdemApresentacao);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.EventoImagemId = Convert.ToInt32(_db.GetParameterValue(command, "@eventoImagemId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um EventoImagem.
        /// </summary>
        /// <param name="entidade">EventoImagem contendo os dados a serem atualizados.</param>
		public void Atualizar(EventoImagem entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE EventoImagem SET ");
			sbSQL.Append(" eventoId=@eventoId, arquivoId=@arquivoId, ordemApresentacao=@ordemApresentacao ");
			sbSQL.Append(" WHERE eventoImagemId=@eventoImagemId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@eventoImagemId", DbType.Int32, entidade.EventoImagemId);
			_db.AddInParameter(command, "@eventoId", DbType.Int32, entidade.Evento.EventoId);
			_db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.Arquivo.ArquivoId);
			_db.AddInParameter(command, "@ordemApresentacao", DbType.Int32, entidade.OrdemApresentacao);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um EventoImagem da base de dados.
        /// </summary>
        /// <param name="entidade">EventoImagem a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(EventoImagem entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM EventoImagem ");
			sbSQL.Append("WHERE eventoImagemId=@eventoImagemId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@eventoImagemId", DbType.Int32, entidade.EventoImagemId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um EventoImagem.
		/// </summary>
        /// <param name="entidade">EventoImagem a ser carregado (somente o identificador é necessário).</param>
		/// <returns>EventoImagem</returns>
		public EventoImagem Carregar(int eventoImagemId) {		
			EventoImagem entidade = new EventoImagem();
			entidade.EventoImagemId = eventoImagemId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um EventoImagem.
		/// </summary>
        /// <param name="entidade">EventoImagem a ser carregado (somente o identificador é necessário).</param>
		/// <returns>EventoImagem</returns>
		public EventoImagem Carregar(EventoImagem entidade) {		
		
			EventoImagem entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM EventoImagem WHERE eventoImagemId=@eventoImagemId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@eventoImagemId", DbType.Int32, entidade.EventoImagemId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new EventoImagem();
				PopulaEventoImagem(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de EventoImagem.
        /// </summary>
        /// <param name="entidade">Arquivo relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de EventoImagem.</returns>
		public IEnumerable<EventoImagem> Carregar(Arquivo entidade)
		{		
			List<EventoImagem> entidadesRetorno = new List<EventoImagem>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT EventoImagem.* FROM EventoImagem WHERE EventoImagem.arquivoId=@arquivoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.ArquivoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                EventoImagem entidadeRetorno = new EventoImagem();
                PopulaEventoImagem(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de EventoImagem.
        /// </summary>
        /// <param name="entidade">Evento relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de EventoImagem.</returns>
		public IEnumerable<EventoImagem> Carregar(Evento entidade)
		{		
			List<EventoImagem> entidadesRetorno = new List<EventoImagem>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT EventoImagem.* FROM EventoImagem WHERE EventoImagem.eventoId=@eventoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@eventoId", DbType.Int32, entidade.EventoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                EventoImagem entidadeRetorno = new EventoImagem();
                PopulaEventoImagem(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de EventoImagem.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos EventoImagem.</returns>
		public IEnumerable<EventoImagem> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<EventoImagem> entidadesRetorno = new List<EventoImagem>();
			
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
				sbOrder.Append( " ORDER BY eventoImagemId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM EventoImagem");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM EventoImagem WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM EventoImagem ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT EventoImagem.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM EventoImagem ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT EventoImagem.* FROM EventoImagem ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                EventoImagem entidadeRetorno = new EventoImagem();
                PopulaEventoImagem(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os EventoImagem existentes na base de dados.
        /// </summary>
		public IEnumerable<EventoImagem> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de EventoImagem na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de EventoImagem na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM EventoImagem");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um EventoImagem baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">EventoImagem a ser populado(.</param>
		public static void PopulaEventoImagem(IDataReader reader, EventoImagem entidade) 
		{						
			if (reader["eventoImagemId"] != DBNull.Value)
				entidade.EventoImagemId = Convert.ToInt32(reader["eventoImagemId"].ToString());
			
			if (reader["ordemApresentacao"] != DBNull.Value)
				entidade.OrdemApresentacao = Convert.ToInt32(reader["ordemApresentacao"].ToString());
			
			if (reader["eventoId"] != DBNull.Value) {
				entidade.Evento = new Evento();
				entidade.Evento.EventoId = Convert.ToInt32(reader["eventoId"].ToString());
			}

			if (reader["arquivoId"] != DBNull.Value) {
				entidade.Arquivo = new Arquivo();
				entidade.Arquivo.ArquivoId = Convert.ToInt32(reader["arquivoId"].ToString());
			}


		}		
		
	}
}
		