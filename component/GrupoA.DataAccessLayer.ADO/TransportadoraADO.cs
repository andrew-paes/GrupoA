
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
	public partial class TransportadoraADO : ADOSuper, ITransportadoraDAL {
	
	    /// <summary>
        /// Método que persiste um Transportadora.
        /// </summary>
        /// <param name="entidade">Transportadora contendo os dados a serem persistidos.</param>	
		public void Inserir(Transportadora entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO Transportadora ");
			sbSQL.Append(" (nomeTransportadora) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@nomeTransportadora) ");											

			sbSQL.Append(" ; SET @transportadoraId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@transportadoraId", DbType.Int32, 8);

			_db.AddInParameter(command, "@nomeTransportadora", DbType.String, entidade.NomeTransportadora);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.TransportadoraId = Convert.ToInt32(_db.GetParameterValue(command, "@transportadoraId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um Transportadora.
        /// </summary>
        /// <param name="entidade">Transportadora contendo os dados a serem atualizados.</param>
		public void Atualizar(Transportadora entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE Transportadora SET ");
			sbSQL.Append(" nomeTransportadora=@nomeTransportadora ");
			sbSQL.Append(" WHERE transportadoraId=@transportadoraId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@transportadoraId", DbType.Int32, entidade.TransportadoraId);
			_db.AddInParameter(command, "@nomeTransportadora", DbType.String, entidade.NomeTransportadora);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um Transportadora da base de dados.
        /// </summary>
        /// <param name="entidade">Transportadora a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(Transportadora entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM Transportadora ");
			sbSQL.Append("WHERE transportadoraId=@transportadoraId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@transportadoraId", DbType.Int32, entidade.TransportadoraId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um Transportadora.
		/// </summary>
        /// <param name="entidade">Transportadora a ser carregado (somente o identificador é necessário).</param>
		/// <returns>Transportadora</returns>
		public Transportadora Carregar(int transportadoraId) {		
			Transportadora entidade = new Transportadora();
			entidade.TransportadoraId = transportadoraId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um Transportadora.
		/// </summary>
        /// <param name="entidade">Transportadora a ser carregado (somente o identificador é necessário).</param>
		/// <returns>Transportadora</returns>
		public Transportadora Carregar(Transportadora entidade) {		
		
			Transportadora entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM Transportadora WHERE transportadoraId=@transportadoraId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@transportadoraId", DbType.Int32, entidade.TransportadoraId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new Transportadora();
				PopulaTransportadora(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de Transportadora.
        /// </summary>
        /// <param name="entidade">TransportadoraServico relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Transportadora.</returns>
		public IEnumerable<Transportadora> Carregar(TransportadoraServico entidade)
		{		
			List<Transportadora> entidadesRetorno = new List<Transportadora>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Transportadora.* FROM Transportadora INNER JOIN TransportadoraServico ON Transportadora.transportadoraId=TransportadoraServico.transportadoraId WHERE TransportadoraServico.transportadoraServicoId=@transportadoraServicoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@transportadoraServicoId", DbType.Int32, entidade.TransportadoraServicoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Transportadora entidadeRetorno = new Transportadora();
                PopulaTransportadora(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de Transportadora.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos Transportadora.</returns>
		public IEnumerable<Transportadora> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<Transportadora> entidadesRetorno = new List<Transportadora>();
			
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
				sbOrder.Append( " ORDER BY transportadoraId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM Transportadora");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Transportadora WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Transportadora ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT Transportadora.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM Transportadora ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT Transportadora.* FROM Transportadora ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Transportadora entidadeRetorno = new Transportadora();
                PopulaTransportadora(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os Transportadora existentes na base de dados.
        /// </summary>
		public IEnumerable<Transportadora> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de Transportadora na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de Transportadora na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM Transportadora");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um Transportadora baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Transportadora a ser populado(.</param>
		public static void PopulaTransportadora(IDataReader reader, Transportadora entidade) 
		{						
			if (reader["transportadoraId"] != DBNull.Value)
				entidade.TransportadoraId = Convert.ToInt32(reader["transportadoraId"].ToString());
			
			if (reader["nomeTransportadora"] != DBNull.Value)
				entidade.NomeTransportadora = reader["nomeTransportadora"].ToString();
			

		}		
		
	}
}
		