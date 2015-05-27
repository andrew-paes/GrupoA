
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
	public partial class ProgramaAtualizacaoPaginaADO : ADOSuper, IProgramaAtualizacaoPaginaDAL {
	
	    /// <summary>
        /// Método que persiste um ProgramaAtualizacaoPagina.
        /// </summary>
        /// <param name="entidade">ProgramaAtualizacaoPagina contendo os dados a serem persistidos.</param>	
		public void Inserir(ProgramaAtualizacaoPagina entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO ProgramaAtualizacaoPagina ");
			sbSQL.Append(" (programaAtualizacaoPaginaId, pagina) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@programaAtualizacaoPaginaId, @pagina) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@programaAtualizacaoPaginaId", DbType.Int32, entidade.ProgramaAtualizacaoPaginaId);

			_db.AddInParameter(command, "@pagina", DbType.String, entidade.Pagina);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um ProgramaAtualizacaoPagina.
        /// </summary>
        /// <param name="entidade">ProgramaAtualizacaoPagina contendo os dados a serem atualizados.</param>
		public void Atualizar(ProgramaAtualizacaoPagina entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE ProgramaAtualizacaoPagina SET ");
			sbSQL.Append(" pagina=@pagina ");
			sbSQL.Append(" WHERE programaAtualizacaoPaginaId=@programaAtualizacaoPaginaId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@programaAtualizacaoPaginaId", DbType.Int32, entidade.ProgramaAtualizacaoPaginaId);
			_db.AddInParameter(command, "@pagina", DbType.String, entidade.Pagina);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um ProgramaAtualizacaoPagina da base de dados.
        /// </summary>
        /// <param name="entidade">ProgramaAtualizacaoPagina a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(ProgramaAtualizacaoPagina entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM ProgramaAtualizacaoPagina ");
			sbSQL.Append("WHERE programaAtualizacaoPaginaId=@programaAtualizacaoPaginaId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@programaAtualizacaoPaginaId", DbType.Int32, entidade.ProgramaAtualizacaoPaginaId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um ProgramaAtualizacaoPagina.
		/// </summary>
        /// <param name="entidade">ProgramaAtualizacaoPagina a ser carregado (somente o identificador é necessário).</param>
		/// <returns>ProgramaAtualizacaoPagina</returns>
		public ProgramaAtualizacaoPagina Carregar(int programaAtualizacaoPaginaId) {		
			ProgramaAtualizacaoPagina entidade = new ProgramaAtualizacaoPagina();
			entidade.ProgramaAtualizacaoPaginaId = programaAtualizacaoPaginaId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um ProgramaAtualizacaoPagina.
		/// </summary>
        /// <param name="entidade">ProgramaAtualizacaoPagina a ser carregado (somente o identificador é necessário).</param>
		/// <returns>ProgramaAtualizacaoPagina</returns>
		public ProgramaAtualizacaoPagina Carregar(ProgramaAtualizacaoPagina entidade) {		
		
			ProgramaAtualizacaoPagina entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM ProgramaAtualizacaoPagina WHERE programaAtualizacaoPaginaId=@programaAtualizacaoPaginaId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@programaAtualizacaoPaginaId", DbType.Int32, entidade.ProgramaAtualizacaoPaginaId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new ProgramaAtualizacaoPagina();
				PopulaProgramaAtualizacaoPagina(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de ProgramaAtualizacaoPagina.
        /// </summary>
        /// <param name="entidade">ProgramaAtualizacaoChamada relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de ProgramaAtualizacaoPagina.</returns>
		public IEnumerable<ProgramaAtualizacaoPagina> Carregar(ProgramaAtualizacaoChamada entidade)
		{		
			List<ProgramaAtualizacaoPagina> entidadesRetorno = new List<ProgramaAtualizacaoPagina>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT ProgramaAtualizacaoPagina.* FROM ProgramaAtualizacaoPagina INNER JOIN ProgramaAtualizacaoChamadaLocalizacao ON ProgramaAtualizacaoPagina.programaAtualizacaoPaginaId=ProgramaAtualizacaoChamadaLocalizacao.programaAtualizacaoPaginaId WHERE ProgramaAtualizacaoChamadaLocalizacao.programaAtualizacaoChamadaId=@programaAtualizacaoChamadaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@programaAtualizacaoChamadaId", DbType.Int32, entidade.ProgramaAtualizacaoChamadaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                ProgramaAtualizacaoPagina entidadeRetorno = new ProgramaAtualizacaoPagina();
                PopulaProgramaAtualizacaoPagina(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de ProgramaAtualizacaoPagina.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos ProgramaAtualizacaoPagina.</returns>
		public IEnumerable<ProgramaAtualizacaoPagina> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<ProgramaAtualizacaoPagina> entidadesRetorno = new List<ProgramaAtualizacaoPagina>();
			
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
				sbOrder.Append( " ORDER BY programaAtualizacaoPaginaId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM ProgramaAtualizacaoPagina");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ProgramaAtualizacaoPagina WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ProgramaAtualizacaoPagina ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT ProgramaAtualizacaoPagina.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM ProgramaAtualizacaoPagina ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT ProgramaAtualizacaoPagina.* FROM ProgramaAtualizacaoPagina ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                ProgramaAtualizacaoPagina entidadeRetorno = new ProgramaAtualizacaoPagina();
                PopulaProgramaAtualizacaoPagina(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os ProgramaAtualizacaoPagina existentes na base de dados.
        /// </summary>
		public IEnumerable<ProgramaAtualizacaoPagina> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de ProgramaAtualizacaoPagina na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de ProgramaAtualizacaoPagina na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM ProgramaAtualizacaoPagina");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um ProgramaAtualizacaoPagina baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">ProgramaAtualizacaoPagina a ser populado(.</param>
		public static void PopulaProgramaAtualizacaoPagina(IDataReader reader, ProgramaAtualizacaoPagina entidade) 
		{						
			if (reader["programaAtualizacaoPaginaId"] != DBNull.Value)
				entidade.ProgramaAtualizacaoPaginaId = Convert.ToInt32(reader["programaAtualizacaoPaginaId"].ToString());
			
			if (reader["pagina"] != DBNull.Value)
				entidade.Pagina = reader["pagina"].ToString();
			

		}		
		
	}
}
		