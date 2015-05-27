
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
	public partial class ContatoSetorADO : ADOSuper, IContatoSetorDAL {
	
	    /// <summary>
        /// Método que persiste um ContatoSetor.
        /// </summary>
        /// <param name="entidade">ContatoSetor contendo os dados a serem persistidos.</param>	
		public void Inserir(ContatoSetor entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO ContatoSetor ");
			sbSQL.Append(" (nomeSetor) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@nomeSetor) ");											

			sbSQL.Append(" ; SET @contatoSetorId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@contatoSetorId", DbType.Int32, 8);

			_db.AddInParameter(command, "@nomeSetor", DbType.String, entidade.NomeSetor);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.ContatoSetorId = Convert.ToInt32(_db.GetParameterValue(command, "@contatoSetorId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um ContatoSetor.
        /// </summary>
        /// <param name="entidade">ContatoSetor contendo os dados a serem atualizados.</param>
		public void Atualizar(ContatoSetor entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE ContatoSetor SET ");
			sbSQL.Append(" nomeSetor=@nomeSetor ");
			sbSQL.Append(" WHERE contatoSetorId=@contatoSetorId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@contatoSetorId", DbType.Int32, entidade.ContatoSetorId);
			_db.AddInParameter(command, "@nomeSetor", DbType.String, entidade.NomeSetor);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um ContatoSetor da base de dados.
        /// </summary>
        /// <param name="entidade">ContatoSetor a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(ContatoSetor entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM ContatoSetor ");
			sbSQL.Append("WHERE contatoSetorId=@contatoSetorId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@contatoSetorId", DbType.Int32, entidade.ContatoSetorId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um ContatoSetor.
		/// </summary>
        /// <param name="entidade">ContatoSetor a ser carregado (somente o identificador é necessário).</param>
		/// <returns>ContatoSetor</returns>
		public ContatoSetor Carregar(int contatoSetorId) {		
			ContatoSetor entidade = new ContatoSetor();
			entidade.ContatoSetorId = contatoSetorId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um ContatoSetor.
		/// </summary>
        /// <param name="entidade">ContatoSetor a ser carregado (somente o identificador é necessário).</param>
		/// <returns>ContatoSetor</returns>
		public ContatoSetor Carregar(ContatoSetor entidade) {		
		
			ContatoSetor entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM ContatoSetor WHERE contatoSetorId=@contatoSetorId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@contatoSetorId", DbType.Int32, entidade.ContatoSetorId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new ContatoSetor();
				PopulaContatoSetor(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de ContatoSetor.
        /// </summary>
        /// <param name="entidade">ContatoAssunto relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de ContatoSetor.</returns>
		public IEnumerable<ContatoSetor> Carregar(ContatoAssunto entidade)
		{		
			List<ContatoSetor> entidadesRetorno = new List<ContatoSetor>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT ContatoSetor.* FROM ContatoSetor INNER JOIN ContatoAssunto ON ContatoSetor.contatoSetorId=ContatoAssunto.contatoSetorId WHERE ContatoAssunto.contatoAssuntoId=@contatoAssuntoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@contatoAssuntoId", DbType.Int32, entidade.ContatoAssuntoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                ContatoSetor entidadeRetorno = new ContatoSetor();
                PopulaContatoSetor(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de ContatoSetor.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos ContatoSetor.</returns>
		public IEnumerable<ContatoSetor> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<ContatoSetor> entidadesRetorno = new List<ContatoSetor>();
			
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
				sbOrder.Append( " ORDER BY contatoSetorId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM ContatoSetor");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ContatoSetor WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ContatoSetor ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT ContatoSetor.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM ContatoSetor ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT ContatoSetor.* FROM ContatoSetor ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                ContatoSetor entidadeRetorno = new ContatoSetor();
                PopulaContatoSetor(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os ContatoSetor existentes na base de dados.
        /// </summary>
		public IEnumerable<ContatoSetor> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de ContatoSetor na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de ContatoSetor na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM ContatoSetor");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um ContatoSetor baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">ContatoSetor a ser populado(.</param>
		public static void PopulaContatoSetor(IDataReader reader, ContatoSetor entidade) 
		{						
			if (reader["contatoSetorId"] != DBNull.Value)
				entidade.ContatoSetorId = Convert.ToInt32(reader["contatoSetorId"].ToString());
			
			if (reader["nomeSetor"] != DBNull.Value)
				entidade.NomeSetor = reader["nomeSetor"].ToString();
			

		}		
		
	}
}
		