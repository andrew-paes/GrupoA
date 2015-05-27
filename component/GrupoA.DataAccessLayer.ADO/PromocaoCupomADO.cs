
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
	public partial class PromocaoCupomADO : ADOSuper, IPromocaoCupomDAL {
	
	    /// <summary>
        /// Método que persiste um PromocaoCupom.
        /// </summary>
        /// <param name="entidade">PromocaoCupom contendo os dados a serem persistidos.</param>	
		public void Inserir(PromocaoCupom entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO PromocaoCupom ");
			sbSQL.Append(" (promocaoId, codigoCupom, reutilizavel, codigoAmigavel) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@promocaoId, @codigoCupom, @reutilizavel, @codigoAmigavel) ");											

			sbSQL.Append(" ; SET @promocaoCupomId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@promocaoCupomId", DbType.Int32, 8);

			if (entidade.Promocao != null ) 
				_db.AddInParameter(command, "@promocaoId", DbType.Int32, entidade.Promocao.PromocaoId);
			else
				_db.AddInParameter(command, "@promocaoId", DbType.Int32, null);

			_db.AddInParameter(command, "@codigoCupom", DbType.Guid, entidade.CodigoCupom);

			_db.AddInParameter(command, "@reutilizavel", DbType.Int32, entidade.Reutilizavel);

			if (entidade.CodigoAmigavel != null ) 
				_db.AddInParameter(command, "@codigoAmigavel", DbType.String, entidade.CodigoAmigavel);
			else
				_db.AddInParameter(command, "@codigoAmigavel", DbType.String, null);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.PromocaoCupomId = Convert.ToInt32(_db.GetParameterValue(command, "@promocaoCupomId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um PromocaoCupom.
        /// </summary>
        /// <param name="entidade">PromocaoCupom contendo os dados a serem atualizados.</param>
		public void Atualizar(PromocaoCupom entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE PromocaoCupom SET ");
			sbSQL.Append(" promocaoId=@promocaoId, codigoCupom=@codigoCupom, reutilizavel=@reutilizavel, codigoAmigavel=@codigoAmigavel ");
			sbSQL.Append(" WHERE promocaoCupomId=@promocaoCupomId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@promocaoCupomId", DbType.Int32, entidade.PromocaoCupomId);
			if (entidade.Promocao != null ) 
				_db.AddInParameter(command, "@promocaoId", DbType.Int32, entidade.Promocao.PromocaoId);
			else
				_db.AddInParameter(command, "@promocaoId", DbType.Int32, null);
			_db.AddInParameter(command, "@codigoCupom", DbType.Guid, entidade.CodigoCupom);
			_db.AddInParameter(command, "@reutilizavel", DbType.Int32, entidade.Reutilizavel);
			if (entidade.CodigoAmigavel != null ) 
				_db.AddInParameter(command, "@codigoAmigavel", DbType.String, entidade.CodigoAmigavel);
			else
				_db.AddInParameter(command, "@codigoAmigavel", DbType.String, null);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um PromocaoCupom da base de dados.
        /// </summary>
        /// <param name="entidade">PromocaoCupom a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(PromocaoCupom entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM PromocaoCupom ");
			sbSQL.Append("WHERE promocaoCupomId=@promocaoCupomId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@promocaoCupomId", DbType.Int32, entidade.PromocaoCupomId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um PromocaoCupom.
		/// </summary>
        /// <param name="entidade">PromocaoCupom a ser carregado (somente o identificador é necessário).</param>
		/// <returns>PromocaoCupom</returns>
		public PromocaoCupom Carregar(int promocaoCupomId) {		
			PromocaoCupom entidade = new PromocaoCupom();
			entidade.PromocaoCupomId = promocaoCupomId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um PromocaoCupom.
		/// </summary>
        /// <param name="entidade">PromocaoCupom a ser carregado (somente o identificador é necessário).</param>
		/// <returns>PromocaoCupom</returns>
		public PromocaoCupom Carregar(PromocaoCupom entidade) {		
		
			PromocaoCupom entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM PromocaoCupom WHERE promocaoCupomId=@promocaoCupomId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@promocaoCupomId", DbType.Int32, entidade.PromocaoCupomId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new PromocaoCupom();
				PopulaPromocaoCupom(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de PromocaoCupom.
        /// </summary>
        /// <param name="entidade">PromocaoCupomPedido relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de PromocaoCupom.</returns>
		public IEnumerable<PromocaoCupom> Carregar(PromocaoCupomPedido entidade)
		{		
			List<PromocaoCupom> entidadesRetorno = new List<PromocaoCupom>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT PromocaoCupom.* FROM PromocaoCupom INNER JOIN PromocaoCupomPedido ON PromocaoCupom.promocaoCupomId=PromocaoCupomPedido.promocaoCupomId WHERE PromocaoCupomPedido.promocaoCupomPedidoId=@promocaoCupomPedidoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@promocaoCupomPedidoId", DbType.Int32, entidade.PromocaoCupomPedidoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                PromocaoCupom entidadeRetorno = new PromocaoCupom();
                PopulaPromocaoCupom(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de PromocaoCupom.
        /// </summary>
        /// <param name="entidade">Promocao relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de PromocaoCupom.</returns>
		public IEnumerable<PromocaoCupom> Carregar(Promocao entidade)
		{		
			List<PromocaoCupom> entidadesRetorno = new List<PromocaoCupom>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT PromocaoCupom.* FROM PromocaoCupom WHERE PromocaoCupom.promocaoId=@promocaoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@promocaoId", DbType.Int32, entidade.PromocaoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                PromocaoCupom entidadeRetorno = new PromocaoCupom();
                PopulaPromocaoCupom(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de PromocaoCupom.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos PromocaoCupom.</returns>
		public IEnumerable<PromocaoCupom> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<PromocaoCupom> entidadesRetorno = new List<PromocaoCupom>();
			
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
				sbOrder.Append( " ORDER BY promocaoCupomId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM PromocaoCupom");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM PromocaoCupom WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM PromocaoCupom ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT PromocaoCupom.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM PromocaoCupom ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT PromocaoCupom.* FROM PromocaoCupom ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                PromocaoCupom entidadeRetorno = new PromocaoCupom();
                PopulaPromocaoCupom(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os PromocaoCupom existentes na base de dados.
        /// </summary>
		public IEnumerable<PromocaoCupom> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de PromocaoCupom na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de PromocaoCupom na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM PromocaoCupom");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um PromocaoCupom baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">PromocaoCupom a ser populado(.</param>
		public static void PopulaPromocaoCupom(IDataReader reader, PromocaoCupom entidade) 
		{						
			if (reader["promocaoCupomId"] != DBNull.Value)
				entidade.PromocaoCupomId = Convert.ToInt32(reader["promocaoCupomId"].ToString());
			
			if (reader["codigoCupom"] != DBNull.Value)
				entidade.CodigoCupom = new Guid(reader["codigoCupom"].ToString());
			
			if (reader["reutilizavel"] != DBNull.Value)
				entidade.Reutilizavel = Convert.ToBoolean(reader["reutilizavel"].ToString());
			
			if (reader["codigoAmigavel"] != DBNull.Value)
				entidade.CodigoAmigavel = reader["codigoAmigavel"].ToString();
			
			if (reader["promocaoId"] != DBNull.Value) {
				entidade.Promocao = new Promocao();
				entidade.Promocao.PromocaoId = Convert.ToInt32(reader["promocaoId"].ToString());
			}


		}		
		
	}
}
		