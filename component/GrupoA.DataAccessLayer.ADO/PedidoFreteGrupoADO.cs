
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
	public partial class PedidoFreteGrupoADO : ADOSuper, IPedidoFreteGrupoDAL {
	
	    /// <summary>
        /// Método que persiste um PedidoFreteGrupo.
        /// </summary>
        /// <param name="entidade">PedidoFreteGrupo contendo os dados a serem persistidos.</param>	
		public void Inserir(PedidoFreteGrupo entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO PedidoFreteGrupo ");
			sbSQL.Append(" (pedidoFreteGrupoId, nomeGrupo, PedidoFreteTipoId, cepInicial1, cepInicial2, cepFinal1, cepFinal2) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@pedidoFreteGrupoId, @nomeGrupo, @PedidoFreteTipoId, @cepInicial1, @cepInicial2, @cepFinal1, @cepFinal2) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@pedidoFreteGrupoId", DbType.Int32, entidade.PedidoFreteGrupoId);

			_db.AddInParameter(command, "@nomeGrupo", DbType.String, entidade.NomeGrupo);

			_db.AddInParameter(command, "@PedidoFreteTipoId", DbType.String, entidade.PedidoFreteTipo.PedidoFreteTipoId);

			_db.AddInParameter(command, "@cepInicial1", DbType.Int32, entidade.CepInicial1);

			_db.AddInParameter(command, "@cepInicial2", DbType.Int32, entidade.CepInicial2);

			_db.AddInParameter(command, "@cepFinal1", DbType.Int32, entidade.CepFinal1);

			_db.AddInParameter(command, "@cepFinal2", DbType.Int32, entidade.CepFinal2);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um PedidoFreteGrupo.
        /// </summary>
        /// <param name="entidade">PedidoFreteGrupo contendo os dados a serem atualizados.</param>
		public void Atualizar(PedidoFreteGrupo entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE PedidoFreteGrupo SET ");
			sbSQL.Append(" nomeGrupo=@nomeGrupo, PedidoFreteTipoId=@PedidoFreteTipoId, cepInicial1=@cepInicial1, cepInicial2=@cepInicial2, cepFinal1=@cepFinal1, cepFinal2=@cepFinal2 ");
			sbSQL.Append(" WHERE pedidoFreteGrupoId=@pedidoFreteGrupoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@pedidoFreteGrupoId", DbType.Int32, entidade.PedidoFreteGrupoId);
			_db.AddInParameter(command, "@nomeGrupo", DbType.String, entidade.NomeGrupo);
			_db.AddInParameter(command, "@PedidoFreteTipoId", DbType.String, entidade.PedidoFreteTipo.PedidoFreteTipoId);
			_db.AddInParameter(command, "@cepInicial1", DbType.Int32, entidade.CepInicial1);
			_db.AddInParameter(command, "@cepInicial2", DbType.Int32, entidade.CepInicial2);
			_db.AddInParameter(command, "@cepFinal1", DbType.Int32, entidade.CepFinal1);
			_db.AddInParameter(command, "@cepFinal2", DbType.Int32, entidade.CepFinal2);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um PedidoFreteGrupo da base de dados.
        /// </summary>
        /// <param name="entidade">PedidoFreteGrupo a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(PedidoFreteGrupo entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM PedidoFreteGrupo ");
			sbSQL.Append("WHERE pedidoFreteGrupoId=@pedidoFreteGrupoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@pedidoFreteGrupoId", DbType.Int32, entidade.PedidoFreteGrupoId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um PedidoFreteGrupo.
		/// </summary>
        /// <param name="entidade">PedidoFreteGrupo a ser carregado (somente o identificador é necessário).</param>
		/// <returns>PedidoFreteGrupo</returns>
		public PedidoFreteGrupo Carregar(int pedidoFreteGrupoId) {		
			PedidoFreteGrupo entidade = new PedidoFreteGrupo();
			entidade.PedidoFreteGrupoId = pedidoFreteGrupoId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um PedidoFreteGrupo.
		/// </summary>
        /// <param name="entidade">PedidoFreteGrupo a ser carregado (somente o identificador é necessário).</param>
		/// <returns>PedidoFreteGrupo</returns>
		public PedidoFreteGrupo Carregar(PedidoFreteGrupo entidade) {		
		
			PedidoFreteGrupo entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM PedidoFreteGrupo WHERE pedidoFreteGrupoId=@pedidoFreteGrupoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@pedidoFreteGrupoId", DbType.Int32, entidade.PedidoFreteGrupoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new PedidoFreteGrupo();
				PopulaPedidoFreteGrupo(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de PedidoFreteGrupo.
        /// </summary>
        /// <param name="entidade">PedidoFretePreco relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de PedidoFreteGrupo.</returns>
		public IEnumerable<PedidoFreteGrupo> Carregar(PedidoFretePreco entidade)
		{		
			List<PedidoFreteGrupo> entidadesRetorno = new List<PedidoFreteGrupo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT PedidoFreteGrupo.* FROM PedidoFreteGrupo INNER JOIN PedidoFretePreco ON PedidoFreteGrupo.pedidoFreteGrupoId=PedidoFretePreco.pedidoFreteGrupoId WHERE PedidoFretePreco.pedidoFretePrecoId=@pedidoFretePrecoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@pedidoFretePrecoId", DbType.Int32, entidade.PedidoFretePrecoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                PedidoFreteGrupo entidadeRetorno = new PedidoFreteGrupo();
                PopulaPedidoFreteGrupo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de PedidoFreteGrupo.
        /// </summary>
        /// <param name="entidade">PedidoFreteTipo relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de PedidoFreteGrupo.</returns>
		public IEnumerable<PedidoFreteGrupo> Carregar(PedidoFreteTipo entidade)
		{		
			List<PedidoFreteGrupo> entidadesRetorno = new List<PedidoFreteGrupo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT PedidoFreteGrupo.* FROM PedidoFreteGrupo WHERE PedidoFreteGrupo.PedidoFreteTipoId=@PedidoFreteTipoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@PedidoFreteTipoId", DbType.String, entidade.PedidoFreteTipoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                PedidoFreteGrupo entidadeRetorno = new PedidoFreteGrupo();
                PopulaPedidoFreteGrupo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de PedidoFreteGrupo.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos PedidoFreteGrupo.</returns>
		public IEnumerable<PedidoFreteGrupo> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<PedidoFreteGrupo> entidadesRetorno = new List<PedidoFreteGrupo>();
			
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
				sbOrder.Append( " ORDER BY pedidoFreteGrupoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM PedidoFreteGrupo");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM PedidoFreteGrupo WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM PedidoFreteGrupo ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT PedidoFreteGrupo.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM PedidoFreteGrupo ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT PedidoFreteGrupo.* FROM PedidoFreteGrupo ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                PedidoFreteGrupo entidadeRetorno = new PedidoFreteGrupo();
                PopulaPedidoFreteGrupo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os PedidoFreteGrupo existentes na base de dados.
        /// </summary>
		public IEnumerable<PedidoFreteGrupo> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de PedidoFreteGrupo na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de PedidoFreteGrupo na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM PedidoFreteGrupo");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um PedidoFreteGrupo baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">PedidoFreteGrupo a ser populado(.</param>
		public static void PopulaPedidoFreteGrupo(IDataReader reader, PedidoFreteGrupo entidade) 
		{						
			if (reader["pedidoFreteGrupoId"] != DBNull.Value)
				entidade.PedidoFreteGrupoId = Convert.ToInt32(reader["pedidoFreteGrupoId"].ToString());
			
			if (reader["nomeGrupo"] != DBNull.Value)
				entidade.NomeGrupo = reader["nomeGrupo"].ToString();
			
			if (reader["cepInicial1"] != DBNull.Value)
				entidade.CepInicial1 = Convert.ToInt32(reader["cepInicial1"].ToString());
			
			if (reader["cepInicial2"] != DBNull.Value)
				entidade.CepInicial2 = Convert.ToInt32(reader["cepInicial2"].ToString());
			
			if (reader["cepFinal1"] != DBNull.Value)
				entidade.CepFinal1 = Convert.ToInt32(reader["cepFinal1"].ToString());
			
			if (reader["cepFinal2"] != DBNull.Value)
				entidade.CepFinal2 = Convert.ToInt32(reader["cepFinal2"].ToString());
			
			if (reader["PedidoFreteTipoId"] != DBNull.Value) {
				entidade.PedidoFreteTipo = new PedidoFreteTipo();
				entidade.PedidoFreteTipo.PedidoFreteTipoId = reader["PedidoFreteTipoId"].ToString();
			}


		}		
		
	}
}
		