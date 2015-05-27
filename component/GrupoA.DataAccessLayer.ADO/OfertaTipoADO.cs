
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
	public partial class OfertaTipoADO : ADOSuper, IOfertaTipoDAL {
	
	    /// <summary>
        /// Método que persiste um OfertaTipo.
        /// </summary>
        /// <param name="entidade">OfertaTipo contendo os dados a serem persistidos.</param>	
		public void Inserir(OfertaTipo entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO OfertaTipo ");
			sbSQL.Append(" (tipoOferta) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@tipoOferta) ");											

			sbSQL.Append(" ; SET @ofertaTipoId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@ofertaTipoId", DbType.Int32, 8);

			_db.AddInParameter(command, "@tipoOferta", DbType.String, entidade.TipoOferta);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.OfertaTipoId = Convert.ToInt32(_db.GetParameterValue(command, "@ofertaTipoId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um OfertaTipo.
        /// </summary>
        /// <param name="entidade">OfertaTipo contendo os dados a serem atualizados.</param>
		public void Atualizar(OfertaTipo entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE OfertaTipo SET ");
			sbSQL.Append(" tipoOferta=@tipoOferta ");
			sbSQL.Append(" WHERE ofertaTipoId=@ofertaTipoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@ofertaTipoId", DbType.Int32, entidade.OfertaTipoId);
			_db.AddInParameter(command, "@tipoOferta", DbType.String, entidade.TipoOferta);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um OfertaTipo da base de dados.
        /// </summary>
        /// <param name="entidade">OfertaTipo a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(OfertaTipo entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM OfertaTipo ");
			sbSQL.Append("WHERE ofertaTipoId=@ofertaTipoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@ofertaTipoId", DbType.Int32, entidade.OfertaTipoId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um OfertaTipo.
		/// </summary>
        /// <param name="entidade">OfertaTipo a ser carregado (somente o identificador é necessário).</param>
		/// <returns>OfertaTipo</returns>
		public OfertaTipo Carregar(int ofertaTipoId) {		
			OfertaTipo entidade = new OfertaTipo();
			entidade.OfertaTipoId = ofertaTipoId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um OfertaTipo.
		/// </summary>
        /// <param name="entidade">OfertaTipo a ser carregado (somente o identificador é necessário).</param>
		/// <returns>OfertaTipo</returns>
		public OfertaTipo Carregar(OfertaTipo entidade) {		
		
			OfertaTipo entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM OfertaTipo WHERE ofertaTipoId=@ofertaTipoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@ofertaTipoId", DbType.Int32, entidade.OfertaTipoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new OfertaTipo();
				PopulaOfertaTipo(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de OfertaTipo.
        /// </summary>
        /// <param name="entidade">Oferta relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de OfertaTipo.</returns>
		public IEnumerable<OfertaTipo> Carregar(Oferta entidade)
		{		
			List<OfertaTipo> entidadesRetorno = new List<OfertaTipo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT OfertaTipo.* FROM OfertaTipo INNER JOIN Oferta ON OfertaTipo.ofertaTipoId=Oferta.ofertaTipoId WHERE Oferta.ofertaId=@ofertaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@ofertaId", DbType.Int32, entidade.OfertaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                OfertaTipo entidadeRetorno = new OfertaTipo();
                PopulaOfertaTipo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de OfertaTipo.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos OfertaTipo.</returns>
		public IEnumerable<OfertaTipo> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<OfertaTipo> entidadesRetorno = new List<OfertaTipo>();
			
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
				sbOrder.Append( " ORDER BY ofertaTipoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM OfertaTipo");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM OfertaTipo WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM OfertaTipo ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT OfertaTipo.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM OfertaTipo ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT OfertaTipo.* FROM OfertaTipo ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                OfertaTipo entidadeRetorno = new OfertaTipo();
                PopulaOfertaTipo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os OfertaTipo existentes na base de dados.
        /// </summary>
		public IEnumerable<OfertaTipo> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de OfertaTipo na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de OfertaTipo na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM OfertaTipo");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um OfertaTipo baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">OfertaTipo a ser populado(.</param>
		public static void PopulaOfertaTipo(IDataReader reader, OfertaTipo entidade) 
		{						
			if (reader["ofertaTipoId"] != DBNull.Value)
				entidade.OfertaTipoId = Convert.ToInt32(reader["ofertaTipoId"].ToString());
			
			if (reader["tipoOferta"] != DBNull.Value)
				entidade.TipoOferta = reader["tipoOferta"].ToString();
			

		}		
		
	}
}
		