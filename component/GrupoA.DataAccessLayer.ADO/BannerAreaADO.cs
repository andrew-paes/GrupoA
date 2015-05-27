
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
	public partial class BannerAreaADO : ADOSuper, IBannerAreaDAL {
	
	    /// <summary>
        /// Método que persiste um BannerArea.
        /// </summary>
        /// <param name="entidade">BannerArea contendo os dados a serem persistidos.</param>	
		public void Inserir(BannerArea entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO BannerArea ");
			sbSQL.Append(" (bannerAreaId, area, dimensao) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@bannerAreaId, @area, @dimensao) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@bannerAreaId", DbType.Int32, entidade.BannerAreaId);

			_db.AddInParameter(command, "@area", DbType.String, entidade.Area);

			_db.AddInParameter(command, "@dimensao", DbType.String, entidade.Dimensao);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um BannerArea.
        /// </summary>
        /// <param name="entidade">BannerArea contendo os dados a serem atualizados.</param>
		public void Atualizar(BannerArea entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE BannerArea SET ");
			sbSQL.Append(" area=@area, dimensao=@dimensao ");
			sbSQL.Append(" WHERE bannerAreaId=@bannerAreaId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@bannerAreaId", DbType.Int32, entidade.BannerAreaId);
			_db.AddInParameter(command, "@area", DbType.String, entidade.Area);
			_db.AddInParameter(command, "@dimensao", DbType.String, entidade.Dimensao);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um BannerArea da base de dados.
        /// </summary>
        /// <param name="entidade">BannerArea a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(BannerArea entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM BannerArea ");
			sbSQL.Append("WHERE bannerAreaId=@bannerAreaId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@bannerAreaId", DbType.Int32, entidade.BannerAreaId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um BannerArea.
		/// </summary>
        /// <param name="entidade">BannerArea a ser carregado (somente o identificador é necessário).</param>
		/// <returns>BannerArea</returns>
		public BannerArea Carregar(int bannerAreaId) {		
			BannerArea entidade = new BannerArea();
			entidade.BannerAreaId = bannerAreaId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um BannerArea.
		/// </summary>
        /// <param name="entidade">BannerArea a ser carregado (somente o identificador é necessário).</param>
		/// <returns>BannerArea</returns>
		public BannerArea Carregar(BannerArea entidade) {		
		
			BannerArea entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM BannerArea WHERE bannerAreaId=@bannerAreaId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@bannerAreaId", DbType.Int32, entidade.BannerAreaId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new BannerArea();
				PopulaBannerArea(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de BannerArea.
        /// </summary>
        /// <param name="entidade">Banner relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de BannerArea.</returns>
		public IEnumerable<BannerArea> Carregar(Banner entidade)
		{		
			List<BannerArea> entidadesRetorno = new List<BannerArea>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT BannerArea.* FROM BannerArea INNER JOIN BannerLocalizacao ON BannerArea.bannerAreaId=BannerLocalizacao.bannerAreaId WHERE BannerLocalizacao.bannerId=@bannerId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@bannerId", DbType.Int32, entidade.BannerId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                BannerArea entidadeRetorno = new BannerArea();
                PopulaBannerArea(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de BannerArea.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos BannerArea.</returns>
		public IEnumerable<BannerArea> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<BannerArea> entidadesRetorno = new List<BannerArea>();
			
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
				sbOrder.Append( " ORDER BY bannerAreaId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM BannerArea");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM BannerArea WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM BannerArea ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT BannerArea.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM BannerArea ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT BannerArea.* FROM BannerArea ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                BannerArea entidadeRetorno = new BannerArea();
                PopulaBannerArea(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os BannerArea existentes na base de dados.
        /// </summary>
		public IEnumerable<BannerArea> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de BannerArea na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de BannerArea na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM BannerArea");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um BannerArea baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">BannerArea a ser populado(.</param>
		public static void PopulaBannerArea(IDataReader reader, BannerArea entidade) 
		{						
			if (reader["bannerAreaId"] != DBNull.Value)
				entidade.BannerAreaId = Convert.ToInt32(reader["bannerAreaId"].ToString());
			
			if (reader["area"] != DBNull.Value)
				entidade.Area = reader["area"].ToString();
			
			if (reader["dimensao"] != DBNull.Value)
				entidade.Dimensao = reader["dimensao"].ToString();
			

		}		
		
	}
}
		