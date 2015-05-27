
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
	public partial class ProfessorComprovanteDocenciaADO : ADOSuper, IProfessorComprovanteDocenciaDAL {
	
	    /// <summary>
        /// Método que persiste um ProfessorComprovanteDocencia.
        /// </summary>
        /// <param name="entidade">ProfessorComprovanteDocencia contendo os dados a serem persistidos.</param>	
		public void Inserir(ProfessorComprovanteDocencia entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO ProfessorComprovanteDocencia ");
			sbSQL.Append(" (professorId, arquivoId, instituicaoId) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@professorId, @arquivoId, @instituicaoId) ");											

			sbSQL.Append(" ; SET @professorComprovanteDocenciaId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@professorComprovanteDocenciaId", DbType.Int32, 8);

			_db.AddInParameter(command, "@professorId", DbType.Int32, entidade.Professor.ProfessorId);

			_db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.Arquivo.ArquivoId);

			_db.AddInParameter(command, "@instituicaoId", DbType.Int32, entidade.Instituicao.InstituicaoId);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.ProfessorComprovanteDocenciaId = Convert.ToInt32(_db.GetParameterValue(command, "@professorComprovanteDocenciaId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um ProfessorComprovanteDocencia.
        /// </summary>
        /// <param name="entidade">ProfessorComprovanteDocencia contendo os dados a serem atualizados.</param>
		public void Atualizar(ProfessorComprovanteDocencia entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE ProfessorComprovanteDocencia SET ");
			sbSQL.Append(" professorId=@professorId, arquivoId=@arquivoId, instituicaoId=@instituicaoId ");
			sbSQL.Append(" WHERE professorComprovanteDocenciaId=@professorComprovanteDocenciaId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@professorComprovanteDocenciaId", DbType.Int32, entidade.ProfessorComprovanteDocenciaId);
			_db.AddInParameter(command, "@professorId", DbType.Int32, entidade.Professor.ProfessorId);
			_db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.Arquivo.ArquivoId);
			_db.AddInParameter(command, "@instituicaoId", DbType.Int32, entidade.Instituicao.InstituicaoId);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um ProfessorComprovanteDocencia da base de dados.
        /// </summary>
        /// <param name="entidade">ProfessorComprovanteDocencia a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(ProfessorComprovanteDocencia entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM ProfessorComprovanteDocencia ");
			sbSQL.Append("WHERE professorComprovanteDocenciaId=@professorComprovanteDocenciaId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@professorComprovanteDocenciaId", DbType.Int32, entidade.ProfessorComprovanteDocenciaId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um ProfessorComprovanteDocencia.
		/// </summary>
        /// <param name="entidade">ProfessorComprovanteDocencia a ser carregado (somente o identificador é necessário).</param>
		/// <returns>ProfessorComprovanteDocencia</returns>
		public ProfessorComprovanteDocencia Carregar(int professorComprovanteDocenciaId) {		
			ProfessorComprovanteDocencia entidade = new ProfessorComprovanteDocencia();
			entidade.ProfessorComprovanteDocenciaId = professorComprovanteDocenciaId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um ProfessorComprovanteDocencia.
		/// </summary>
        /// <param name="entidade">ProfessorComprovanteDocencia a ser carregado (somente o identificador é necessário).</param>
		/// <returns>ProfessorComprovanteDocencia</returns>
		public ProfessorComprovanteDocencia Carregar(ProfessorComprovanteDocencia entidade) {		
		
			ProfessorComprovanteDocencia entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM ProfessorComprovanteDocencia WHERE professorComprovanteDocenciaId=@professorComprovanteDocenciaId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@professorComprovanteDocenciaId", DbType.Int32, entidade.ProfessorComprovanteDocenciaId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new ProfessorComprovanteDocencia();
				PopulaProfessorComprovanteDocencia(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de ProfessorComprovanteDocencia.
        /// </summary>
        /// <param name="entidade">Arquivo relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de ProfessorComprovanteDocencia.</returns>
		public IEnumerable<ProfessorComprovanteDocencia> Carregar(Arquivo entidade)
		{		
			List<ProfessorComprovanteDocencia> entidadesRetorno = new List<ProfessorComprovanteDocencia>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT ProfessorComprovanteDocencia.* FROM ProfessorComprovanteDocencia WHERE ProfessorComprovanteDocencia.arquivoId=@arquivoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.ArquivoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                ProfessorComprovanteDocencia entidadeRetorno = new ProfessorComprovanteDocencia();
                PopulaProfessorComprovanteDocencia(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de ProfessorComprovanteDocencia.
        /// </summary>
        /// <param name="entidade">Instituicao relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de ProfessorComprovanteDocencia.</returns>
		public IEnumerable<ProfessorComprovanteDocencia> Carregar(Instituicao entidade)
		{		
			List<ProfessorComprovanteDocencia> entidadesRetorno = new List<ProfessorComprovanteDocencia>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT ProfessorComprovanteDocencia.* FROM ProfessorComprovanteDocencia WHERE ProfessorComprovanteDocencia.instituicaoId=@instituicaoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@instituicaoId", DbType.Int32, entidade.InstituicaoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                ProfessorComprovanteDocencia entidadeRetorno = new ProfessorComprovanteDocencia();
                PopulaProfessorComprovanteDocencia(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de ProfessorComprovanteDocencia.
        /// </summary>
        /// <param name="entidade">Professor relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de ProfessorComprovanteDocencia.</returns>
		public IEnumerable<ProfessorComprovanteDocencia> Carregar(Professor entidade)
		{		
			List<ProfessorComprovanteDocencia> entidadesRetorno = new List<ProfessorComprovanteDocencia>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT ProfessorComprovanteDocencia.* FROM ProfessorComprovanteDocencia WHERE ProfessorComprovanteDocencia.professorId=@professorId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@professorId", DbType.Int32, entidade.ProfessorId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                ProfessorComprovanteDocencia entidadeRetorno = new ProfessorComprovanteDocencia();
                PopulaProfessorComprovanteDocencia(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de ProfessorComprovanteDocencia.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos ProfessorComprovanteDocencia.</returns>
		public IEnumerable<ProfessorComprovanteDocencia> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<ProfessorComprovanteDocencia> entidadesRetorno = new List<ProfessorComprovanteDocencia>();
			
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
				sbOrder.Append( " ORDER BY professorComprovanteDocenciaId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM ProfessorComprovanteDocencia");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ProfessorComprovanteDocencia WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ProfessorComprovanteDocencia ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT ProfessorComprovanteDocencia.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM ProfessorComprovanteDocencia ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT ProfessorComprovanteDocencia.* FROM ProfessorComprovanteDocencia ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                ProfessorComprovanteDocencia entidadeRetorno = new ProfessorComprovanteDocencia();
                PopulaProfessorComprovanteDocencia(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os ProfessorComprovanteDocencia existentes na base de dados.
        /// </summary>
		public IEnumerable<ProfessorComprovanteDocencia> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de ProfessorComprovanteDocencia na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de ProfessorComprovanteDocencia na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM ProfessorComprovanteDocencia");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um ProfessorComprovanteDocencia baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">ProfessorComprovanteDocencia a ser populado(.</param>
		public static void PopulaProfessorComprovanteDocencia(IDataReader reader, ProfessorComprovanteDocencia entidade) 
		{						
			if (reader["professorComprovanteDocenciaId"] != DBNull.Value)
				entidade.ProfessorComprovanteDocenciaId = Convert.ToInt32(reader["professorComprovanteDocenciaId"].ToString());
			
			if (reader["professorId"] != DBNull.Value) {
				entidade.Professor = new Professor();
				entidade.Professor.ProfessorId = Convert.ToInt32(reader["professorId"].ToString());
			}

			if (reader["arquivoId"] != DBNull.Value) {
				entidade.Arquivo = new Arquivo();
				entidade.Arquivo.ArquivoId = Convert.ToInt32(reader["arquivoId"].ToString());
			}

			if (reader["instituicaoId"] != DBNull.Value) {
				entidade.Instituicao = new Instituicao();
				entidade.Instituicao.InstituicaoId = Convert.ToInt32(reader["instituicaoId"].ToString());
			}


		}		
		
	}
}
		