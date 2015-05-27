
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
	public partial class ContatoResponsavelADO : ADOSuper, IContatoResponsavelDAL {
	
	    /// <summary>
        /// Método que persiste um ContatoResponsavel.
        /// </summary>
        /// <param name="entidade">ContatoResponsavel contendo os dados a serem persistidos.</param>	
		public void Inserir(ContatoResponsavel entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO ContatoResponsavel ");
			sbSQL.Append(" (contatoAssuntoId, nomeResponsavel, emailResonsavel) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@contatoAssuntoId, @nomeResponsavel, @emailResonsavel) ");											

			sbSQL.Append(" ; SET @contatoResponsavelId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@contatoResponsavelId", DbType.Int32, 8);

			_db.AddInParameter(command, "@contatoAssuntoId", DbType.Int32, entidade.ContatoAssunto.ContatoAssuntoId);

			_db.AddInParameter(command, "@nomeResponsavel", DbType.String, entidade.NomeResponsavel);

			_db.AddInParameter(command, "@emailResonsavel", DbType.String, entidade.EmailResonsavel);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.ContatoResponsavelId = Convert.ToInt32(_db.GetParameterValue(command, "@contatoResponsavelId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um ContatoResponsavel.
        /// </summary>
        /// <param name="entidade">ContatoResponsavel contendo os dados a serem atualizados.</param>
		public void Atualizar(ContatoResponsavel entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE ContatoResponsavel SET ");
			sbSQL.Append(" contatoAssuntoId=@contatoAssuntoId, nomeResponsavel=@nomeResponsavel, emailResonsavel=@emailResonsavel ");
			sbSQL.Append(" WHERE contatoResponsavelId=@contatoResponsavelId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@contatoResponsavelId", DbType.Int32, entidade.ContatoResponsavelId);
			_db.AddInParameter(command, "@contatoAssuntoId", DbType.Int32, entidade.ContatoAssunto.ContatoAssuntoId);
			_db.AddInParameter(command, "@nomeResponsavel", DbType.String, entidade.NomeResponsavel);
			_db.AddInParameter(command, "@emailResonsavel", DbType.String, entidade.EmailResonsavel);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um ContatoResponsavel da base de dados.
        /// </summary>
        /// <param name="entidade">ContatoResponsavel a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(ContatoResponsavel entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM ContatoResponsavel ");
			sbSQL.Append("WHERE contatoResponsavelId=@contatoResponsavelId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@contatoResponsavelId", DbType.Int32, entidade.ContatoResponsavelId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um ContatoResponsavel.
		/// </summary>
        /// <param name="entidade">ContatoResponsavel a ser carregado (somente o identificador é necessário).</param>
		/// <returns>ContatoResponsavel</returns>
		public ContatoResponsavel Carregar(int contatoResponsavelId) {		
			ContatoResponsavel entidade = new ContatoResponsavel();
			entidade.ContatoResponsavelId = contatoResponsavelId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um ContatoResponsavel.
		/// </summary>
        /// <param name="entidade">ContatoResponsavel a ser carregado (somente o identificador é necessário).</param>
		/// <returns>ContatoResponsavel</returns>
		public ContatoResponsavel Carregar(ContatoResponsavel entidade) {		
		
			ContatoResponsavel entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM ContatoResponsavel WHERE contatoResponsavelId=@contatoResponsavelId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@contatoResponsavelId", DbType.Int32, entidade.ContatoResponsavelId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new ContatoResponsavel();
				PopulaContatoResponsavel(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de ContatoResponsavel.
        /// </summary>
        /// <param name="entidade">ContatoAssunto relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de ContatoResponsavel.</returns>
		public IEnumerable<ContatoResponsavel> Carregar(ContatoAssunto entidade)
		{		
			List<ContatoResponsavel> entidadesRetorno = new List<ContatoResponsavel>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT ContatoResponsavel.* FROM ContatoResponsavel WHERE ContatoResponsavel.contatoAssuntoId=@contatoAssuntoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@contatoAssuntoId", DbType.Int32, entidade.ContatoAssuntoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                ContatoResponsavel entidadeRetorno = new ContatoResponsavel();
                PopulaContatoResponsavel(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de ContatoResponsavel.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos ContatoResponsavel.</returns>
		public IEnumerable<ContatoResponsavel> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<ContatoResponsavel> entidadesRetorno = new List<ContatoResponsavel>();
			
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
				sbOrder.Append( " ORDER BY contatoResponsavelId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM ContatoResponsavel");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ContatoResponsavel WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ContatoResponsavel ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT ContatoResponsavel.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM ContatoResponsavel ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT ContatoResponsavel.* FROM ContatoResponsavel ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                ContatoResponsavel entidadeRetorno = new ContatoResponsavel();
                PopulaContatoResponsavel(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os ContatoResponsavel existentes na base de dados.
        /// </summary>
		public IEnumerable<ContatoResponsavel> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de ContatoResponsavel na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de ContatoResponsavel na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM ContatoResponsavel");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um ContatoResponsavel baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">ContatoResponsavel a ser populado(.</param>
		public static void PopulaContatoResponsavel(IDataReader reader, ContatoResponsavel entidade) 
		{						
			if (reader["contatoResponsavelId"] != DBNull.Value)
				entidade.ContatoResponsavelId = Convert.ToInt32(reader["contatoResponsavelId"].ToString());
			
			if (reader["nomeResponsavel"] != DBNull.Value)
				entidade.NomeResponsavel = reader["nomeResponsavel"].ToString();
			
			if (reader["emailResonsavel"] != DBNull.Value)
				entidade.EmailResonsavel = reader["emailResonsavel"].ToString();
			
			if (reader["contatoAssuntoId"] != DBNull.Value) {
				entidade.ContatoAssunto = new ContatoAssunto();
				entidade.ContatoAssunto.ContatoAssuntoId = Convert.ToInt32(reader["contatoAssuntoId"].ToString());
			}


		}		
		
	}
}
		