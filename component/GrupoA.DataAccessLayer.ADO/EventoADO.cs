
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
	public partial class EventoADO : ADOSuper, IEventoDAL {
	
	    /// <summary>
        /// Método que persiste um Evento.
        /// </summary>
        /// <param name="entidade">Evento contendo os dados a serem persistidos.</param>	
		public void Inserir(Evento entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO Evento ");
			sbSQL.Append(" (eventoId, dataEventoInicio, dataEventoFim, local, arquivoIdThumb, exibeFormularioContato) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@eventoId, @dataEventoInicio, @dataEventoFim, @local, @arquivoIdThumb, @exibeFormularioContato) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@eventoId", DbType.Int32, entidade.EventoId);

			if (entidade.DataEventoInicio != null && entidade.DataEventoInicio != DateTime.MinValue ) 
				_db.AddInParameter(command, "@dataEventoInicio", DbType.DateTime, entidade.DataEventoInicio);
			else
				_db.AddInParameter(command, "@dataEventoInicio", DbType.DateTime, null);

			if (entidade.DataEventoFim != null && entidade.DataEventoFim != DateTime.MinValue ) 
				_db.AddInParameter(command, "@dataEventoFim", DbType.DateTime, entidade.DataEventoFim);
			else
				_db.AddInParameter(command, "@dataEventoFim", DbType.DateTime, null);

			if (entidade.Local != null ) 
				_db.AddInParameter(command, "@local", DbType.String, entidade.Local);
			else
				_db.AddInParameter(command, "@local", DbType.String, null);

			if (entidade.ArquivoThumb != null ) 
				_db.AddInParameter(command, "@arquivoIdThumb", DbType.Int32, entidade.ArquivoThumb.ArquivoId);
			else
				_db.AddInParameter(command, "@arquivoIdThumb", DbType.Int32, null);

			_db.AddInParameter(command, "@exibeFormularioContato", DbType.Int32, entidade.ExibeFormularioContato);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um Evento.
        /// </summary>
        /// <param name="entidade">Evento contendo os dados a serem atualizados.</param>
		public void Atualizar(Evento entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE Evento SET ");
			sbSQL.Append(" dataEventoInicio=@dataEventoInicio, dataEventoFim=@dataEventoFim, local=@local, arquivoIdThumb=@arquivoIdThumb, exibeFormularioContato=@exibeFormularioContato ");
			sbSQL.Append(" WHERE eventoId=@eventoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@eventoId", DbType.Int32, entidade.EventoId);
			if (entidade.DataEventoInicio != null && entidade.DataEventoInicio != DateTime.MinValue ) 
				_db.AddInParameter(command, "@dataEventoInicio", DbType.DateTime, entidade.DataEventoInicio);
			else
				_db.AddInParameter(command, "@dataEventoInicio", DbType.DateTime, null);
			if (entidade.DataEventoFim != null && entidade.DataEventoFim != DateTime.MinValue ) 
				_db.AddInParameter(command, "@dataEventoFim", DbType.DateTime, entidade.DataEventoFim);
			else
				_db.AddInParameter(command, "@dataEventoFim", DbType.DateTime, null);
			if (entidade.Local != null ) 
				_db.AddInParameter(command, "@local", DbType.String, entidade.Local);
			else
				_db.AddInParameter(command, "@local", DbType.String, null);
			if (entidade.ArquivoThumb != null ) 
				_db.AddInParameter(command, "@arquivoIdThumb", DbType.Int32, entidade.ArquivoThumb.ArquivoId);
			else
				_db.AddInParameter(command, "@arquivoIdThumb", DbType.Int32, null);
			_db.AddInParameter(command, "@exibeFormularioContato", DbType.Int32, entidade.ExibeFormularioContato);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um Evento da base de dados.
        /// </summary>
        /// <param name="entidade">Evento a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(Evento entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM Evento ");
			sbSQL.Append("WHERE eventoId=@eventoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@eventoId", DbType.Int32, entidade.EventoId);

								
			_db.ExecuteNonQuery(command);
		}
		

		/// <summary>
		/// Método que carrega um Evento.
		/// </summary>
        /// <param name="entidade">Evento a ser carregado (somente o identificador é necessário).</param>
		/// <returns>Evento</returns>
		public Evento Carregar(Evento entidade) {		
		
			Evento entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM Evento WHERE eventoId=@eventoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@eventoId", DbType.Int32, entidade.EventoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new Evento();
				PopulaEvento(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		
		
		/// <summary>
		/// Método que carrega um Evento com suas dependências.
		/// </summary>
        /// <param name="entidade">Evento a ser carregado (somente o identificador é necessário).</param>
		/// <returns>Evento</returns>
		public Evento CarregarComDependencias(Evento entidade) {		
		
			Evento entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append("SELECT Evento.eventoId, Evento.dataEventoInicio, Evento.dataEventoFim, Evento.local, Evento.arquivoIdThumb, Evento.exibeFormularioContato");
			sbSQL.Append(", conteudoImprensaId, fonte, fonteUrl, ativo, dataExibicaoInicio, dataExibicaoFim, resumo, texto, destaque, titulo");
			sbSQL.Append(", conteudoId, conteudoTipoId, dataHoraCadastro");
			sbSQL.Append(" FROM Evento");
			sbSQL.Append(" INNER JOIN ConteudoImprensa ON Evento.eventoId=ConteudoImprensa.conteudoImprensaId");
			sbSQL.Append(" INNER JOIN Conteudo ON ConteudoImprensa.conteudoImprensaId=Conteudo.conteudoId");
			sbSQL.Append(" WHERE Evento.eventoId=@eventoId");
						
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@eventoId", DbType.Int32, entidade.EventoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new Evento();
				PopulaEvento(reader, entidadeRetorno);
				entidadeRetorno.ConteudoImprensa = new ConteudoImprensa();
				ConteudoImprensaADO.PopulaConteudoImprensa(reader, entidadeRetorno.ConteudoImprensa);
				entidadeRetorno.ConteudoImprensa.Conteudo = new Conteudo();
				ConteudoADO.PopulaConteudo(reader, entidadeRetorno.ConteudoImprensa.Conteudo);
			}
			reader.Close();
			
			return entidadeRetorno;
		}		


		/// <summary>
        /// Método que retorna uma coleção de Evento.
        /// </summary>
        /// <param name="entidade">EventoAlerta relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Evento.</returns>
		public IEnumerable<Evento> Carregar(EventoAlerta entidade)
		{		
			List<Evento> entidadesRetorno = new List<Evento>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Evento.* FROM Evento INNER JOIN EventoAlerta ON Evento.eventoId=EventoAlerta.eventoId WHERE EventoAlerta.eventoAlertaId=@eventoAlertaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@eventoAlertaId", DbType.Int32, entidade.EventoAlertaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Evento entidadeRetorno = new Evento();
                PopulaEvento(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Evento.
        /// </summary>
        /// <param name="entidade">EventoImagem relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Evento.</returns>
		public IEnumerable<Evento> Carregar(EventoImagem entidade)
		{		
			List<Evento> entidadesRetorno = new List<Evento>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Evento.* FROM Evento INNER JOIN EventoImagem ON Evento.eventoId=EventoImagem.eventoId WHERE EventoImagem.eventoImagemId=@eventoImagemId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@eventoImagemId", DbType.Int32, entidade.EventoImagemId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Evento entidadeRetorno = new Evento();
                PopulaEvento(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Evento.
        /// </summary>
        /// <param name="entidade">Arquivo relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Evento.</returns>
		public IEnumerable<Evento> Carregar(Arquivo entidade)
		{		
			List<Evento> entidadesRetorno = new List<Evento>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Evento.* FROM Evento WHERE Evento.arquivoId=@arquivoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.ArquivoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Evento entidadeRetorno = new Evento();
                PopulaEvento(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de Evento.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos Evento.</returns>
		public IEnumerable<Evento> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<Evento> entidadesRetorno = new List<Evento>();
			
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
				sbOrder.Append( " ORDER BY eventoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM Evento");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Evento WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Evento ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT Evento.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM Evento ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT Evento.* FROM Evento ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Evento entidadeRetorno = new Evento();
                PopulaEvento(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os Evento existentes na base de dados.
        /// </summary>
		public IEnumerable<Evento> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de Evento na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de Evento na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM Evento");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um Evento baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Evento a ser populado(.</param>
		public static void PopulaEvento(IDataReader reader, Evento entidade) 
		{						
			if (reader["dataEventoInicio"] != DBNull.Value)
				entidade.DataEventoInicio = Convert.ToDateTime(reader["dataEventoInicio"].ToString());
			
			if (reader["dataEventoFim"] != DBNull.Value)
				entidade.DataEventoFim = Convert.ToDateTime(reader["dataEventoFim"].ToString());
			
			if (reader["local"] != DBNull.Value)
				entidade.Local = reader["local"].ToString();
			
			if (reader["exibeFormularioContato"] != DBNull.Value)
				entidade.ExibeFormularioContato = Convert.ToBoolean(reader["exibeFormularioContato"].ToString());
			
			if (reader["eventoId"] != DBNull.Value) {
				entidade.EventoId = Convert.ToInt32(reader["eventoId"].ToString());
			}

			if (reader["arquivoIdThumb"] != DBNull.Value) {
				entidade.ArquivoThumb = new Arquivo();
				entidade.ArquivoThumb.ArquivoId = Convert.ToInt32(reader["arquivoIdThumb"].ToString());
			}


		}		
		
	}
}
		