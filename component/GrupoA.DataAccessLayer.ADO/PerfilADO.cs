
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
	public partial class PerfilADO : ADOSuper, IPerfilDAL {
	
	    /// <summary>
        /// Método que persiste um Perfil.
        /// </summary>
        /// <param name="entidade">Perfil contendo os dados a serem persistidos.</param>	
		public void Inserir(Perfil entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO Perfil ");
			sbSQL.Append(" (perfilId, perfilNome) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@perfilId, @perfilNome) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@perfilId", DbType.Int32, entidade.PerfilId);

			_db.AddInParameter(command, "@perfilNome", DbType.String, entidade.PerfilNome);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um Perfil.
        /// </summary>
        /// <param name="entidade">Perfil contendo os dados a serem atualizados.</param>
		public void Atualizar(Perfil entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE Perfil SET ");
			sbSQL.Append(" perfilNome=@perfilNome ");
			sbSQL.Append(" WHERE perfilId=@perfilId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@perfilId", DbType.Int32, entidade.PerfilId);
			_db.AddInParameter(command, "@perfilNome", DbType.String, entidade.PerfilNome);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um Perfil da base de dados.
        /// </summary>
        /// <param name="entidade">Perfil a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(Perfil entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM Perfil ");
			sbSQL.Append("WHERE perfilId=@perfilId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@perfilId", DbType.Int32, entidade.PerfilId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um Perfil.
		/// </summary>
        /// <param name="entidade">Perfil a ser carregado (somente o identificador é necessário).</param>
		/// <returns>Perfil</returns>
		public Perfil Carregar(int perfilId) {		
			Perfil entidade = new Perfil();
			entidade.PerfilId = perfilId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um Perfil.
		/// </summary>
        /// <param name="entidade">Perfil a ser carregado (somente o identificador é necessário).</param>
		/// <returns>Perfil</returns>
		public Perfil Carregar(Perfil entidade) {		
		
			Perfil entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM Perfil WHERE perfilId=@perfilId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@perfilId", DbType.Int32, entidade.PerfilId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new Perfil();
				PopulaPerfil(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de Perfil.
        /// </summary>
        /// <param name="entidade">Promocao relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Perfil.</returns>
		public IEnumerable<Perfil> Carregar(Promocao entidade)
		{		
			List<Perfil> entidadesRetorno = new List<Perfil>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Perfil.* FROM Perfil INNER JOIN PromocaoPerfil ON Perfil.perfilId=PromocaoPerfil.perfilId WHERE PromocaoPerfil.promocaoId=@promocaoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@promocaoId", DbType.Int32, entidade.PromocaoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Perfil entidadeRetorno = new Perfil();
                PopulaPerfil(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Perfil.
        /// </summary>
        /// <param name="entidade">Usuario relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Perfil.</returns>
		public IEnumerable<Perfil> Carregar(Usuario entidade)
		{		
			List<Perfil> entidadesRetorno = new List<Perfil>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Perfil.* FROM Perfil INNER JOIN UsuarioPerfil ON Perfil.perfilId=UsuarioPerfil.perfilId WHERE UsuarioPerfil.usuarioId=@usuarioId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.UsuarioId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Perfil entidadeRetorno = new Perfil();
                PopulaPerfil(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de Perfil.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos Perfil.</returns>
		public IEnumerable<Perfil> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<Perfil> entidadesRetorno = new List<Perfil>();
			
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
				sbOrder.Append( " ORDER BY perfilId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM Perfil");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Perfil WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Perfil ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT Perfil.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM Perfil ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT Perfil.* FROM Perfil ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Perfil entidadeRetorno = new Perfil();
                PopulaPerfil(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os Perfil existentes na base de dados.
        /// </summary>
		public IEnumerable<Perfil> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de Perfil na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de Perfil na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM Perfil");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um Perfil baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Perfil a ser populado(.</param>
		public static void PopulaPerfil(IDataReader reader, Perfil entidade) 
		{						
			if (reader["perfilId"] != DBNull.Value)
				entidade.PerfilId = Convert.ToInt32(reader["perfilId"].ToString());
			
			if (reader["perfilNome"] != DBNull.Value)
				entidade.PerfilNome = reader["perfilNome"].ToString();
			

		}		
		
	}
}
		