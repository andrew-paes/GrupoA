
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
	public partial class GraduacaoProfessorADO : ADOSuper, IGraduacaoProfessorDAL {
	
	    /// <summary>
        /// Método que persiste um GraduacaoProfessor.
        /// </summary>
        /// <param name="entidade">GraduacaoProfessor contendo os dados a serem persistidos.</param>	
		public void Inserir(GraduacaoProfessor entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO GraduacaoProfessor ");
			sbSQL.Append(" (graduacaoProfessorId, graduacao) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@graduacaoProfessorId, @graduacao) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@graduacaoProfessorId", DbType.Int32, entidade.GraduacaoProfessorId);

			_db.AddInParameter(command, "@graduacao", DbType.String, entidade.Graduacao);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um GraduacaoProfessor.
        /// </summary>
        /// <param name="entidade">GraduacaoProfessor contendo os dados a serem atualizados.</param>
		public void Atualizar(GraduacaoProfessor entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE GraduacaoProfessor SET ");
			sbSQL.Append(" graduacao=@graduacao ");
			sbSQL.Append(" WHERE graduacaoProfessorId=@graduacaoProfessorId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@graduacaoProfessorId", DbType.Int32, entidade.GraduacaoProfessorId);
			_db.AddInParameter(command, "@graduacao", DbType.String, entidade.Graduacao);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um GraduacaoProfessor da base de dados.
        /// </summary>
        /// <param name="entidade">GraduacaoProfessor a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(GraduacaoProfessor entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM GraduacaoProfessor ");
			sbSQL.Append("WHERE graduacaoProfessorId=@graduacaoProfessorId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@graduacaoProfessorId", DbType.Int32, entidade.GraduacaoProfessorId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um GraduacaoProfessor.
		/// </summary>
        /// <param name="entidade">GraduacaoProfessor a ser carregado (somente o identificador é necessário).</param>
		/// <returns>GraduacaoProfessor</returns>
		public GraduacaoProfessor Carregar(int graduacaoProfessorId) {		
			GraduacaoProfessor entidade = new GraduacaoProfessor();
			entidade.GraduacaoProfessorId = graduacaoProfessorId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um GraduacaoProfessor.
		/// </summary>
        /// <param name="entidade">GraduacaoProfessor a ser carregado (somente o identificador é necessário).</param>
		/// <returns>GraduacaoProfessor</returns>
		public GraduacaoProfessor Carregar(GraduacaoProfessor entidade) {		
		
			GraduacaoProfessor entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM GraduacaoProfessor WHERE graduacaoProfessorId=@graduacaoProfessorId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@graduacaoProfessorId", DbType.Int32, entidade.GraduacaoProfessorId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new GraduacaoProfessor();
				PopulaGraduacaoProfessor(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de GraduacaoProfessor.
        /// </summary>
        /// <param name="entidade">Professor relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de GraduacaoProfessor.</returns>
		public IEnumerable<GraduacaoProfessor> Carregar(Professor entidade)
		{		
			List<GraduacaoProfessor> entidadesRetorno = new List<GraduacaoProfessor>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT GraduacaoProfessor.* FROM GraduacaoProfessor INNER JOIN Professor ON GraduacaoProfessor.graduacaoProfessorId=Professor.graduacaoProfessorId WHERE Professor.professorId=@professorId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@professorId", DbType.Int32, entidade.ProfessorId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                GraduacaoProfessor entidadeRetorno = new GraduacaoProfessor();
                PopulaGraduacaoProfessor(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de GraduacaoProfessor.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos GraduacaoProfessor.</returns>
		public IEnumerable<GraduacaoProfessor> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<GraduacaoProfessor> entidadesRetorno = new List<GraduacaoProfessor>();
			
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
				sbOrder.Append( " ORDER BY graduacaoProfessorId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM GraduacaoProfessor");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM GraduacaoProfessor WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM GraduacaoProfessor ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT GraduacaoProfessor.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM GraduacaoProfessor ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT GraduacaoProfessor.* FROM GraduacaoProfessor ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                GraduacaoProfessor entidadeRetorno = new GraduacaoProfessor();
                PopulaGraduacaoProfessor(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os GraduacaoProfessor existentes na base de dados.
        /// </summary>
		public IEnumerable<GraduacaoProfessor> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de GraduacaoProfessor na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de GraduacaoProfessor na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM GraduacaoProfessor");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um GraduacaoProfessor baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">GraduacaoProfessor a ser populado(.</param>
		public static void PopulaGraduacaoProfessor(IDataReader reader, GraduacaoProfessor entidade) 
		{						
			if (reader["graduacaoProfessorId"] != DBNull.Value)
				entidade.GraduacaoProfessorId = Convert.ToInt32(reader["graduacaoProfessorId"].ToString());
			
			if (reader["graduacao"] != DBNull.Value)
				entidade.Graduacao = reader["graduacao"].ToString();
			

		}		
		
	}
}
		