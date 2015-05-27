
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
	public partial class RevistaPaginaADO : ADOSuper, IRevistaPaginaDAL {
	
	    /// <summary>
        /// Método que persiste um RevistaPagina.
        /// </summary>
        /// <param name="entidade">RevistaPagina contendo os dados a serem persistidos.</param>	
		public void Inserir(RevistaPagina entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO RevistaPagina ");
			sbSQL.Append(" (revistaId, nomePagina, tituloPagina, textoPagina, ativo, ordem, exibirMenu) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@revistaId, @nomePagina, @tituloPagina, @textoPagina, @ativo, @ordem, @exibirMenu) ");											

			sbSQL.Append(" ; SET @revistaPaginaId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@revistaPaginaId", DbType.Int32, 8);

			_db.AddInParameter(command, "@revistaId", DbType.Int32, entidade.Revista.RevistaId);

			_db.AddInParameter(command, "@nomePagina", DbType.String, entidade.NomePagina);

			_db.AddInParameter(command, "@tituloPagina", DbType.String, entidade.TituloPagina);

			_db.AddInParameter(command, "@textoPagina", DbType.String, entidade.TextoPagina);

			_db.AddInParameter(command, "@ativo", DbType.Int32, entidade.Ativo);

			_db.AddInParameter(command, "@ordem", DbType.Int32, entidade.Ordem);

			_db.AddInParameter(command, "@exibirMenu", DbType.Int32, entidade.ExibirMenu);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.RevistaPaginaId = Convert.ToInt32(_db.GetParameterValue(command, "@revistaPaginaId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um RevistaPagina.
        /// </summary>
        /// <param name="entidade">RevistaPagina contendo os dados a serem atualizados.</param>
		public void Atualizar(RevistaPagina entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE RevistaPagina SET ");
			sbSQL.Append(" revistaId=@revistaId, nomePagina=@nomePagina, tituloPagina=@tituloPagina, textoPagina=@textoPagina, ativo=@ativo, ordem=@ordem, exibirMenu=@exibirMenu ");
			sbSQL.Append(" WHERE revistaPaginaId=@revistaPaginaId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@revistaPaginaId", DbType.Int32, entidade.RevistaPaginaId);
			_db.AddInParameter(command, "@revistaId", DbType.Int32, entidade.Revista.RevistaId);
			_db.AddInParameter(command, "@nomePagina", DbType.String, entidade.NomePagina);
			_db.AddInParameter(command, "@tituloPagina", DbType.String, entidade.TituloPagina);
			_db.AddInParameter(command, "@textoPagina", DbType.String, entidade.TextoPagina);
			_db.AddInParameter(command, "@ativo", DbType.Int32, entidade.Ativo);
			_db.AddInParameter(command, "@ordem", DbType.Int32, entidade.Ordem);
			_db.AddInParameter(command, "@exibirMenu", DbType.Int32, entidade.ExibirMenu);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um RevistaPagina da base de dados.
        /// </summary>
        /// <param name="entidade">RevistaPagina a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(RevistaPagina entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM RevistaPagina ");
			sbSQL.Append("WHERE revistaPaginaId=@revistaPaginaId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@revistaPaginaId", DbType.Int32, entidade.RevistaPaginaId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um RevistaPagina.
		/// </summary>
        /// <param name="entidade">RevistaPagina a ser carregado (somente o identificador é necessário).</param>
		/// <returns>RevistaPagina</returns>
		public RevistaPagina Carregar(int revistaPaginaId) {		
			RevistaPagina entidade = new RevistaPagina();
			entidade.RevistaPaginaId = revistaPaginaId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um RevistaPagina.
		/// </summary>
        /// <param name="entidade">RevistaPagina a ser carregado (somente o identificador é necessário).</param>
		/// <returns>RevistaPagina</returns>
		public RevistaPagina Carregar(RevistaPagina entidade) {		
		
			RevistaPagina entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM RevistaPagina WHERE revistaPaginaId=@revistaPaginaId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@revistaPaginaId", DbType.Int32, entidade.RevistaPaginaId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new RevistaPagina();
				PopulaRevistaPagina(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de RevistaPagina.
        /// </summary>
        /// <param name="entidade">Revista relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de RevistaPagina.</returns>
		public IEnumerable<RevistaPagina> Carregar(Revista entidade)
		{		
			List<RevistaPagina> entidadesRetorno = new List<RevistaPagina>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT RevistaPagina.* FROM RevistaPagina WHERE RevistaPagina.revistaId=@revistaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@revistaId", DbType.Int32, entidade.RevistaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                RevistaPagina entidadeRetorno = new RevistaPagina();
                PopulaRevistaPagina(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de RevistaPagina.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos RevistaPagina.</returns>
		public IEnumerable<RevistaPagina> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<RevistaPagina> entidadesRetorno = new List<RevistaPagina>();
			
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
				sbOrder.Append( " ORDER BY revistaPaginaId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM RevistaPagina");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM RevistaPagina WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM RevistaPagina ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT RevistaPagina.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM RevistaPagina ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT RevistaPagina.* FROM RevistaPagina ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                RevistaPagina entidadeRetorno = new RevistaPagina();
                PopulaRevistaPagina(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os RevistaPagina existentes na base de dados.
        /// </summary>
		public IEnumerable<RevistaPagina> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de RevistaPagina na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de RevistaPagina na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM RevistaPagina");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um RevistaPagina baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">RevistaPagina a ser populado(.</param>
		public static void PopulaRevistaPagina(IDataReader reader, RevistaPagina entidade) 
		{						
			if (reader["revistaPaginaId"] != DBNull.Value)
				entidade.RevistaPaginaId = Convert.ToInt32(reader["revistaPaginaId"].ToString());
			
			if (reader["nomePagina"] != DBNull.Value)
				entidade.NomePagina = reader["nomePagina"].ToString();
			
			if (reader["tituloPagina"] != DBNull.Value)
				entidade.TituloPagina = reader["tituloPagina"].ToString();
			
			if (reader["textoPagina"] != DBNull.Value)
				entidade.TextoPagina = reader["textoPagina"].ToString();
			
			if (reader["ativo"] != DBNull.Value)
				entidade.Ativo = Convert.ToBoolean(reader["ativo"].ToString());
			
			if (reader["ordem"] != DBNull.Value)
				entidade.Ordem = Convert.ToInt32(reader["ordem"].ToString());
			
			if (reader["exibirMenu"] != DBNull.Value)
				entidade.ExibirMenu = Convert.ToBoolean(reader["exibirMenu"].ToString());
			
			if (reader["revistaId"] != DBNull.Value) {
				entidade.Revista = new Revista();
				entidade.Revista.RevistaId = Convert.ToInt32(reader["revistaId"].ToString());
			}


		}		
		
	}
}
		