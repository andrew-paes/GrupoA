
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
	public partial class EnqueteADO : ADOSuper, IEnqueteDAL {
	
	    /// <summary>
        /// Método que persiste um Enquete.
        /// </summary>
        /// <param name="entidade">Enquete contendo os dados a serem persistidos.</param>	
		public void Inserir(Enquete entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO Enquete ");
			sbSQL.Append(" (nomeEnquete, ativo, pergunta) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@nomeEnquete, @ativo, @pergunta) ");											

			sbSQL.Append(" ; SET @enqueteId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@enqueteId", DbType.Int32, 8);

			_db.AddInParameter(command, "@nomeEnquete", DbType.String, entidade.NomeEnquete);

			_db.AddInParameter(command, "@ativo", DbType.Int32, entidade.Ativo);

			_db.AddInParameter(command, "@pergunta", DbType.String, entidade.Pergunta);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.EnqueteId = Convert.ToInt32(_db.GetParameterValue(command, "@enqueteId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um Enquete.
        /// </summary>
        /// <param name="entidade">Enquete contendo os dados a serem atualizados.</param>
		public void Atualizar(Enquete entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE Enquete SET ");
			sbSQL.Append(" nomeEnquete=@nomeEnquete, ativo=@ativo, pergunta=@pergunta ");
			sbSQL.Append(" WHERE enqueteId=@enqueteId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@enqueteId", DbType.Int32, entidade.EnqueteId);
			_db.AddInParameter(command, "@nomeEnquete", DbType.String, entidade.NomeEnquete);
			_db.AddInParameter(command, "@ativo", DbType.Int32, entidade.Ativo);
			_db.AddInParameter(command, "@pergunta", DbType.String, entidade.Pergunta);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um Enquete da base de dados.
        /// </summary>
        /// <param name="entidade">Enquete a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(Enquete entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM Enquete ");
			sbSQL.Append("WHERE enqueteId=@enqueteId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@enqueteId", DbType.Int32, entidade.EnqueteId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um Enquete.
		/// </summary>
        /// <param name="entidade">Enquete a ser carregado (somente o identificador é necessário).</param>
		/// <returns>Enquete</returns>
		public Enquete Carregar(int enqueteId) {		
			Enquete entidade = new Enquete();
			entidade.EnqueteId = enqueteId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um Enquete.
		/// </summary>
        /// <param name="entidade">Enquete a ser carregado (somente o identificador é necessário).</param>
		/// <returns>Enquete</returns>
		public Enquete Carregar(Enquete entidade) {		
		
			Enquete entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM Enquete WHERE enqueteId=@enqueteId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@enqueteId", DbType.Int32, entidade.EnqueteId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new Enquete();
				PopulaEnquete(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de Enquete.
        /// </summary>
        /// <param name="entidade">EnquetePagina relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Enquete.</returns>
		public IEnumerable<Enquete> Carregar(EnquetePagina entidade)
		{		
			List<Enquete> entidadesRetorno = new List<Enquete>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Enquete.* FROM Enquete INNER JOIN EnqueteLocalizacao ON Enquete.enqueteId=EnqueteLocalizacao.enqueteId WHERE EnqueteLocalizacao.enquetePaginaId=@enquetePaginaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@enquetePaginaId", DbType.Int32, entidade.EnquetePaginaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Enquete entidadeRetorno = new Enquete();
                PopulaEnquete(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Enquete.
        /// </summary>
        /// <param name="entidade">EnqueteOpcao relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Enquete.</returns>
		public IEnumerable<Enquete> Carregar(EnqueteOpcao entidade)
		{		
			List<Enquete> entidadesRetorno = new List<Enquete>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Enquete.* FROM Enquete INNER JOIN EnqueteOpcao ON Enquete.enqueteId=EnqueteOpcao.enqueteId WHERE EnqueteOpcao.enqueteOpcaoId=@enqueteOpcaoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@enqueteOpcaoId", DbType.Int32, entidade.EnqueteOpcaoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Enquete entidadeRetorno = new Enquete();
                PopulaEnquete(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Enquete.
        /// </summary>
        /// <param name="entidade">Usuario relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Enquete.</returns>
		public IEnumerable<Enquete> Carregar(Usuario entidade)
		{		
			List<Enquete> entidadesRetorno = new List<Enquete>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Enquete.* FROM Enquete INNER JOIN EnqueteUsuario ON Enquete.enqueteId=EnqueteUsuario.enqueteId WHERE EnqueteUsuario.usuarioId=@usuarioId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.UsuarioId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Enquete entidadeRetorno = new Enquete();
                PopulaEnquete(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de Enquete.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos Enquete.</returns>
		public IEnumerable<Enquete> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<Enquete> entidadesRetorno = new List<Enquete>();
			
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
				sbOrder.Append( " ORDER BY enqueteId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM Enquete");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Enquete WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Enquete ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT Enquete.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM Enquete ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT Enquete.* FROM Enquete ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Enquete entidadeRetorno = new Enquete();
                PopulaEnquete(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os Enquete existentes na base de dados.
        /// </summary>
		public IEnumerable<Enquete> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de Enquete na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de Enquete na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM Enquete");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um Enquete baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Enquete a ser populado(.</param>
		public static void PopulaEnquete(IDataReader reader, Enquete entidade) 
		{						
			if (reader["enqueteId"] != DBNull.Value)
				entidade.EnqueteId = Convert.ToInt32(reader["enqueteId"].ToString());
			
			if (reader["nomeEnquete"] != DBNull.Value)
				entidade.NomeEnquete = reader["nomeEnquete"].ToString();
			
			if (reader["ativo"] != DBNull.Value)
				entidade.Ativo = Convert.ToBoolean(reader["ativo"].ToString());
			
			if (reader["pergunta"] != DBNull.Value)
				entidade.Pergunta = reader["pergunta"].ToString();
			

		}		
		
	}
}
		