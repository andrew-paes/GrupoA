
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
	public partial class TelefoneADO : ADOSuper, ITelefoneDAL {
	
	    /// <summary>
        /// Método que persiste um Telefone.
        /// </summary>
        /// <param name="entidade">Telefone contendo os dados a serem persistidos.</param>	
		public void Inserir(Telefone entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO Telefone ");
			sbSQL.Append(" (numeroTelefone, dddTelefone, telefoneTipoId, usuarioId, ramal) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@numeroTelefone, @dddTelefone, @telefoneTipoId, @usuarioId, @ramal) ");											

			sbSQL.Append(" ; SET @telefoneId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@telefoneId", DbType.Int32, 8);

			_db.AddInParameter(command, "@numeroTelefone", DbType.String, entidade.NumeroTelefone);

			if (entidade.DddTelefone != null ) 
				_db.AddInParameter(command, "@dddTelefone", DbType.String, entidade.DddTelefone);
			else
				_db.AddInParameter(command, "@dddTelefone", DbType.String, null);

			_db.AddInParameter(command, "@telefoneTipoId", DbType.Int32, entidade.TelefoneTipo.TelefoneTipoId);

			_db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.Usuario.UsuarioId);

			if (entidade.Ramal != null ) 
				_db.AddInParameter(command, "@ramal", DbType.String, entidade.Ramal);
			else
				_db.AddInParameter(command, "@ramal", DbType.String, null);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.TelefoneId = Convert.ToInt32(_db.GetParameterValue(command, "@telefoneId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um Telefone.
        /// </summary>
        /// <param name="entidade">Telefone contendo os dados a serem atualizados.</param>
		public void Atualizar(Telefone entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE Telefone SET ");
			sbSQL.Append(" numeroTelefone=@numeroTelefone, dddTelefone=@dddTelefone, telefoneTipoId=@telefoneTipoId, usuarioId=@usuarioId, ramal=@ramal ");
			sbSQL.Append(" WHERE telefoneId=@telefoneId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@telefoneId", DbType.Int32, entidade.TelefoneId);
			_db.AddInParameter(command, "@numeroTelefone", DbType.String, entidade.NumeroTelefone);
			if (entidade.DddTelefone != null ) 
				_db.AddInParameter(command, "@dddTelefone", DbType.String, entidade.DddTelefone);
			else
				_db.AddInParameter(command, "@dddTelefone", DbType.String, null);
			_db.AddInParameter(command, "@telefoneTipoId", DbType.Int32, entidade.TelefoneTipo.TelefoneTipoId);
			_db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.Usuario.UsuarioId);
			if (entidade.Ramal != null ) 
				_db.AddInParameter(command, "@ramal", DbType.String, entidade.Ramal);
			else
				_db.AddInParameter(command, "@ramal", DbType.String, null);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um Telefone da base de dados.
        /// </summary>
        /// <param name="entidade">Telefone a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(Telefone entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM Telefone ");
			sbSQL.Append("WHERE telefoneId=@telefoneId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@telefoneId", DbType.Int32, entidade.TelefoneId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um Telefone.
		/// </summary>
        /// <param name="entidade">Telefone a ser carregado (somente o identificador é necessário).</param>
		/// <returns>Telefone</returns>
		public Telefone Carregar(int telefoneId) {		
			Telefone entidade = new Telefone();
			entidade.TelefoneId = telefoneId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um Telefone.
		/// </summary>
        /// <param name="entidade">Telefone a ser carregado (somente o identificador é necessário).</param>
		/// <returns>Telefone</returns>
		public Telefone Carregar(Telefone entidade) {		
		
			Telefone entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM Telefone WHERE telefoneId=@telefoneId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@telefoneId", DbType.Int32, entidade.TelefoneId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new Telefone();
				PopulaTelefone(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		
		
		/// <summary>
		/// Método que carrega um Telefone com suas dependências.
		/// </summary>
        /// <param name="entidade">Telefone a ser carregado (somente o identificador é necessário).</param>
		/// <returns>Telefone</returns>
		public Telefone CarregarComDependencias(Telefone entidade) {		
		
			Telefone entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append("SELECT Telefone.telefoneId, Telefone.numeroTelefone, Telefone.dddTelefone, Telefone.telefoneTipoId, Telefone.usuarioId, Telefone.ramal");
			sbSQL.Append(", professorInstituicaoId, instituicaoId, campus, departamento, professorId");
			sbSQL.Append(" FROM Telefone");
			sbSQL.Append(" LEFT JOIN ProfessorInstituicao ON Telefone.telefoneId=ProfessorInstituicao.professorInstituicaoId");
			sbSQL.Append(" WHERE Telefone.telefoneId=@telefoneId");
						
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@telefoneId", DbType.Int32, entidade.TelefoneId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new Telefone();
				PopulaTelefone(reader, entidadeRetorno);
				entidadeRetorno.ProfessorInstituicao = new ProfessorInstituicao();
				ProfessorInstituicaoADO.PopulaProfessorInstituicao(reader, entidadeRetorno.ProfessorInstituicao);
			}
			reader.Close();
			
			return entidadeRetorno;
		}		


		/// <summary>
        /// Método que retorna um Telefone.
        /// </summary>
        /// <param name="entidade">ProfessorInstituicao relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna um Telefone.</returns>
		public Telefone Carregar(ProfessorInstituicao entidade)
		{		
			Telefone entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Telefone.* FROM Telefone INNER JOIN ProfessorInstituicao ON Telefone.telefoneId=ProfessorInstituicao.telefoneId WHERE ProfessorInstituicao.professorInstituicaoId=@professorInstituicaoId");
		
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@professorInstituicaoId", DbType.Int32, entidade.ProfessorInstituicaoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            if (reader.Read())
            {
                entidadeRetorno = new Telefone();
                PopulaTelefone(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Telefone.
        /// </summary>
        /// <param name="entidade">TelefoneTipo relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Telefone.</returns>
		public IEnumerable<Telefone> Carregar(TelefoneTipo entidade)
		{		
			List<Telefone> entidadesRetorno = new List<Telefone>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Telefone.* FROM Telefone WHERE Telefone.telefoneTipoId=@telefoneTipoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@telefoneTipoId", DbType.Int32, entidade.TelefoneTipoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Telefone entidadeRetorno = new Telefone();
                PopulaTelefone(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Telefone.
        /// </summary>
        /// <param name="entidade">Usuario relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Telefone.</returns>
		public IEnumerable<Telefone> Carregar(Usuario entidade)
		{		
			List<Telefone> entidadesRetorno = new List<Telefone>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Telefone.* FROM Telefone WHERE Telefone.usuarioId=@usuarioId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.UsuarioId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Telefone entidadeRetorno = new Telefone();
                PopulaTelefone(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de Telefone.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos Telefone.</returns>
		public IEnumerable<Telefone> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<Telefone> entidadesRetorno = new List<Telefone>();
			
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
				sbOrder.Append( " ORDER BY telefoneId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM Telefone");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Telefone WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Telefone ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT Telefone.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM Telefone ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT Telefone.* FROM Telefone ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Telefone entidadeRetorno = new Telefone();
                PopulaTelefone(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os Telefone existentes na base de dados.
        /// </summary>
		public IEnumerable<Telefone> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de Telefone na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de Telefone na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM Telefone");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um Telefone baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Telefone a ser populado(.</param>
		public static void PopulaTelefone(IDataReader reader, Telefone entidade) 
		{						
			if (reader["telefoneId"] != DBNull.Value)
				entidade.TelefoneId = Convert.ToInt32(reader["telefoneId"].ToString());
			
			if (reader["numeroTelefone"] != DBNull.Value)
				entidade.NumeroTelefone = reader["numeroTelefone"].ToString();
			
			if (reader["dddTelefone"] != DBNull.Value)
				entidade.DddTelefone = reader["dddTelefone"].ToString();
			
			if (reader["ramal"] != DBNull.Value)
				entidade.Ramal = reader["ramal"].ToString();
			
			if (reader["telefoneTipoId"] != DBNull.Value) {
				entidade.TelefoneTipo = new TelefoneTipo();
				entidade.TelefoneTipo.TelefoneTipoId = Convert.ToInt32(reader["telefoneTipoId"].ToString());
			}

			if (reader["usuarioId"] != DBNull.Value) {
				entidade.Usuario = new Usuario();
				entidade.Usuario.UsuarioId = Convert.ToInt32(reader["usuarioId"].ToString());
			}


		}		
		
	}
}
		