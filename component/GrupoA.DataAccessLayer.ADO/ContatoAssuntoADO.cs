
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
	public partial class ContatoAssuntoADO : ADOSuper, IContatoAssuntoDAL {
	
	    /// <summary>
        /// Método que persiste um ContatoAssunto.
        /// </summary>
        /// <param name="entidade">ContatoAssunto contendo os dados a serem persistidos.</param>	
		public void Inserir(ContatoAssunto entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO ContatoAssunto ");
			sbSQL.Append(" (contatoSetorId, nomeAssunto) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@contatoSetorId, @nomeAssunto) ");											

			sbSQL.Append(" ; SET @contatoAssuntoId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@contatoAssuntoId", DbType.Int32, 8);

			_db.AddInParameter(command, "@contatoSetorId", DbType.Int32, entidade.ContatoSetor.ContatoSetorId);

			if (entidade.NomeAssunto != null ) 
				_db.AddInParameter(command, "@nomeAssunto", DbType.String, entidade.NomeAssunto);
			else
				_db.AddInParameter(command, "@nomeAssunto", DbType.String, null);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.ContatoAssuntoId = Convert.ToInt32(_db.GetParameterValue(command, "@contatoAssuntoId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um ContatoAssunto.
        /// </summary>
        /// <param name="entidade">ContatoAssunto contendo os dados a serem atualizados.</param>
		public void Atualizar(ContatoAssunto entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE ContatoAssunto SET ");
			sbSQL.Append(" contatoSetorId=@contatoSetorId, nomeAssunto=@nomeAssunto ");
			sbSQL.Append(" WHERE contatoAssuntoId=@contatoAssuntoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@contatoAssuntoId", DbType.Int32, entidade.ContatoAssuntoId);
			_db.AddInParameter(command, "@contatoSetorId", DbType.Int32, entidade.ContatoSetor.ContatoSetorId);
			if (entidade.NomeAssunto != null ) 
				_db.AddInParameter(command, "@nomeAssunto", DbType.String, entidade.NomeAssunto);
			else
				_db.AddInParameter(command, "@nomeAssunto", DbType.String, null);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um ContatoAssunto da base de dados.
        /// </summary>
        /// <param name="entidade">ContatoAssunto a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(ContatoAssunto entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM ContatoAssunto ");
			sbSQL.Append("WHERE contatoAssuntoId=@contatoAssuntoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@contatoAssuntoId", DbType.Int32, entidade.ContatoAssuntoId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um ContatoAssunto.
		/// </summary>
        /// <param name="entidade">ContatoAssunto a ser carregado (somente o identificador é necessário).</param>
		/// <returns>ContatoAssunto</returns>
		public ContatoAssunto Carregar(int contatoAssuntoId) {		
			ContatoAssunto entidade = new ContatoAssunto();
			entidade.ContatoAssuntoId = contatoAssuntoId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um ContatoAssunto.
		/// </summary>
        /// <param name="entidade">ContatoAssunto a ser carregado (somente o identificador é necessário).</param>
		/// <returns>ContatoAssunto</returns>
		public ContatoAssunto Carregar(ContatoAssunto entidade) {		
		
			ContatoAssunto entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM ContatoAssunto WHERE contatoAssuntoId=@contatoAssuntoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@contatoAssuntoId", DbType.Int32, entidade.ContatoAssuntoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new ContatoAssunto();
				PopulaContatoAssunto(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de ContatoAssunto.
        /// </summary>
        /// <param name="entidade">ContatoResponsavel relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de ContatoAssunto.</returns>
		public IEnumerable<ContatoAssunto> Carregar(ContatoResponsavel entidade)
		{		
			List<ContatoAssunto> entidadesRetorno = new List<ContatoAssunto>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT ContatoAssunto.* FROM ContatoAssunto INNER JOIN ContatoResponsavel ON ContatoAssunto.contatoAssuntoId=ContatoResponsavel.contatoAssuntoId WHERE ContatoResponsavel.contatoResponsavelId=@contatoResponsavelId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@contatoResponsavelId", DbType.Int32, entidade.ContatoResponsavelId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                ContatoAssunto entidadeRetorno = new ContatoAssunto();
                PopulaContatoAssunto(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de ContatoAssunto.
        /// </summary>
        /// <param name="entidade">ContatoSetor relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de ContatoAssunto.</returns>
		public IEnumerable<ContatoAssunto> Carregar(ContatoSetor entidade)
		{		
			List<ContatoAssunto> entidadesRetorno = new List<ContatoAssunto>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT ContatoAssunto.* FROM ContatoAssunto WHERE ContatoAssunto.contatoSetorId=@contatoSetorId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@contatoSetorId", DbType.Int32, entidade.ContatoSetorId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                ContatoAssunto entidadeRetorno = new ContatoAssunto();
                PopulaContatoAssunto(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de ContatoAssunto.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos ContatoAssunto.</returns>
		public IEnumerable<ContatoAssunto> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<ContatoAssunto> entidadesRetorno = new List<ContatoAssunto>();
			
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
				sbOrder.Append( " ORDER BY contatoAssuntoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM ContatoAssunto");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ContatoAssunto WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ContatoAssunto ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT ContatoAssunto.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM ContatoAssunto ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT ContatoAssunto.* FROM ContatoAssunto ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                ContatoAssunto entidadeRetorno = new ContatoAssunto();
                PopulaContatoAssunto(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os ContatoAssunto existentes na base de dados.
        /// </summary>
		public IEnumerable<ContatoAssunto> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de ContatoAssunto na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de ContatoAssunto na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM ContatoAssunto");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um ContatoAssunto baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">ContatoAssunto a ser populado(.</param>
		public static void PopulaContatoAssunto(IDataReader reader, ContatoAssunto entidade) 
		{						
			if (reader["contatoAssuntoId"] != DBNull.Value)
				entidade.ContatoAssuntoId = Convert.ToInt32(reader["contatoAssuntoId"].ToString());
			
			if (reader["nomeAssunto"] != DBNull.Value)
				entidade.NomeAssunto = reader["nomeAssunto"].ToString();
			
			if (reader["contatoSetorId"] != DBNull.Value) {
				entidade.ContatoSetor = new ContatoSetor();
				entidade.ContatoSetor.ContatoSetorId = Convert.ToInt32(reader["contatoSetorId"].ToString());
			}


		}		
		
	}
}
		