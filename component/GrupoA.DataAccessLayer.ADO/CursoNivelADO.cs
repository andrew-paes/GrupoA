
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
	public partial class CursoNivelADO : ADOSuper, ICursoNivelDAL {
	
	    /// <summary>
        /// Método que persiste um CursoNivel.
        /// </summary>
        /// <param name="entidade">CursoNivel contendo os dados a serem persistidos.</param>	
		public void Inserir(CursoNivel entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO CursoNivel ");
			sbSQL.Append(" (cursoNivelId, nivel) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@cursoNivelId, @nivel) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@cursoNivelId", DbType.Int32, entidade.CursoNivelId);

			if (entidade.Nivel != null ) 
				_db.AddInParameter(command, "@nivel", DbType.String, entidade.Nivel);
			else
				_db.AddInParameter(command, "@nivel", DbType.String, null);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um CursoNivel.
        /// </summary>
        /// <param name="entidade">CursoNivel contendo os dados a serem atualizados.</param>
		public void Atualizar(CursoNivel entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE CursoNivel SET ");
			sbSQL.Append(" nivel=@nivel ");
			sbSQL.Append(" WHERE cursoNivelId=@cursoNivelId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@cursoNivelId", DbType.Int32, entidade.CursoNivelId);
			if (entidade.Nivel != null ) 
				_db.AddInParameter(command, "@nivel", DbType.String, entidade.Nivel);
			else
				_db.AddInParameter(command, "@nivel", DbType.String, null);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um CursoNivel da base de dados.
        /// </summary>
        /// <param name="entidade">CursoNivel a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(CursoNivel entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM CursoNivel ");
			sbSQL.Append("WHERE cursoNivelId=@cursoNivelId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@cursoNivelId", DbType.Int32, entidade.CursoNivelId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um CursoNivel.
		/// </summary>
        /// <param name="entidade">CursoNivel a ser carregado (somente o identificador é necessário).</param>
		/// <returns>CursoNivel</returns>
		public CursoNivel Carregar(int cursoNivelId) {		
			CursoNivel entidade = new CursoNivel();
			entidade.CursoNivelId = cursoNivelId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um CursoNivel.
		/// </summary>
        /// <param name="entidade">CursoNivel a ser carregado (somente o identificador é necessário).</param>
		/// <returns>CursoNivel</returns>
		public CursoNivel Carregar(CursoNivel entidade) {		
		
			CursoNivel entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM CursoNivel WHERE cursoNivelId=@cursoNivelId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@cursoNivelId", DbType.Int32, entidade.CursoNivelId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new CursoNivel();
				PopulaCursoNivel(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de CursoNivel.
        /// </summary>
        /// <param name="entidade">ProfessorCurso relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de CursoNivel.</returns>
		public IEnumerable<CursoNivel> Carregar(ProfessorCurso entidade)
		{		
			List<CursoNivel> entidadesRetorno = new List<CursoNivel>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT CursoNivel.* FROM CursoNivel INNER JOIN ProfessorCurso ON CursoNivel.cursoNivelId=ProfessorCurso.cursoNivelId WHERE ProfessorCurso.professorCursoId=@professorCursoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@professorCursoId", DbType.Int32, entidade.ProfessorCursoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                CursoNivel entidadeRetorno = new CursoNivel();
                PopulaCursoNivel(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de CursoNivel.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos CursoNivel.</returns>
		public IEnumerable<CursoNivel> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<CursoNivel> entidadesRetorno = new List<CursoNivel>();
			
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
				sbOrder.Append( " ORDER BY cursoNivelId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM CursoNivel");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM CursoNivel WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM CursoNivel ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT CursoNivel.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM CursoNivel ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT CursoNivel.* FROM CursoNivel ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                CursoNivel entidadeRetorno = new CursoNivel();
                PopulaCursoNivel(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os CursoNivel existentes na base de dados.
        /// </summary>
		public IEnumerable<CursoNivel> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de CursoNivel na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de CursoNivel na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM CursoNivel");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um CursoNivel baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">CursoNivel a ser populado(.</param>
		public static void PopulaCursoNivel(IDataReader reader, CursoNivel entidade) 
		{						
			if (reader["cursoNivelId"] != DBNull.Value)
				entidade.CursoNivelId = Convert.ToInt32(reader["cursoNivelId"].ToString());
			
			if (reader["nivel"] != DBNull.Value)
				entidade.Nivel = reader["nivel"].ToString();
			

		}		
		
	}
}
		