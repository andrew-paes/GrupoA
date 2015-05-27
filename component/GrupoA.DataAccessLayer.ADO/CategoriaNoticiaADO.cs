
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
	public partial class CategoriaNoticiaADO : ADOSuper, ICategoriaNoticiaDAL {
	
	    /// <summary>
        /// Método que persiste um CategoriaNoticia.
        /// </summary>
        /// <param name="entidade">CategoriaNoticia contendo os dados a serem persistidos.</param>	
		public void Inserir(CategoriaNoticia entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO CategoriaNoticia ");
			sbSQL.Append(" (nomeCategoriaNoticia) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@nomeCategoriaNoticia) ");											

			sbSQL.Append(" ; SET @categoriaNoticiaId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@categoriaNoticiaId", DbType.Int32, 8);

			_db.AddInParameter(command, "@nomeCategoriaNoticia", DbType.String, entidade.NomeCategoriaNoticia);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.CategoriaNoticiaId = Convert.ToInt32(_db.GetParameterValue(command, "@categoriaNoticiaId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um CategoriaNoticia.
        /// </summary>
        /// <param name="entidade">CategoriaNoticia contendo os dados a serem atualizados.</param>
		public void Atualizar(CategoriaNoticia entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE CategoriaNoticia SET ");
			sbSQL.Append(" nomeCategoriaNoticia=@nomeCategoriaNoticia ");
			sbSQL.Append(" WHERE categoriaNoticiaId=@categoriaNoticiaId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@categoriaNoticiaId", DbType.Int32, entidade.CategoriaNoticiaId);
			_db.AddInParameter(command, "@nomeCategoriaNoticia", DbType.String, entidade.NomeCategoriaNoticia);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um CategoriaNoticia da base de dados.
        /// </summary>
        /// <param name="entidade">CategoriaNoticia a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(CategoriaNoticia entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM CategoriaNoticia ");
			sbSQL.Append("WHERE categoriaNoticiaId=@categoriaNoticiaId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@categoriaNoticiaId", DbType.Int32, entidade.CategoriaNoticiaId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um CategoriaNoticia.
		/// </summary>
        /// <param name="entidade">CategoriaNoticia a ser carregado (somente o identificador é necessário).</param>
		/// <returns>CategoriaNoticia</returns>
		public CategoriaNoticia Carregar(int categoriaNoticiaId) {		
			CategoriaNoticia entidade = new CategoriaNoticia();
			entidade.CategoriaNoticiaId = categoriaNoticiaId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um CategoriaNoticia.
		/// </summary>
        /// <param name="entidade">CategoriaNoticia a ser carregado (somente o identificador é necessário).</param>
		/// <returns>CategoriaNoticia</returns>
		public CategoriaNoticia Carregar(CategoriaNoticia entidade) {		
		
			CategoriaNoticia entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM CategoriaNoticia WHERE categoriaNoticiaId=@categoriaNoticiaId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@categoriaNoticiaId", DbType.Int32, entidade.CategoriaNoticiaId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new CategoriaNoticia();
				PopulaCategoriaNoticia(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de CategoriaNoticia.
        /// </summary>
        /// <param name="entidade">Noticia relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de CategoriaNoticia.</returns>
		public IEnumerable<CategoriaNoticia> Carregar(Noticia entidade)
		{		
			List<CategoriaNoticia> entidadesRetorno = new List<CategoriaNoticia>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT CategoriaNoticia.* FROM CategoriaNoticia INNER JOIN Noticia ON CategoriaNoticia.categoriaNoticiaId=Noticia.categoriaNoticiaId WHERE Noticia.noticiaId=@noticiaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@noticiaId", DbType.Int32, entidade.NoticiaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                CategoriaNoticia entidadeRetorno = new CategoriaNoticia();
                PopulaCategoriaNoticia(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de CategoriaNoticia.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos CategoriaNoticia.</returns>
		public IEnumerable<CategoriaNoticia> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<CategoriaNoticia> entidadesRetorno = new List<CategoriaNoticia>();
			
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
				sbOrder.Append( " ORDER BY categoriaNoticiaId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM CategoriaNoticia");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM CategoriaNoticia WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM CategoriaNoticia ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT CategoriaNoticia.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM CategoriaNoticia ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT CategoriaNoticia.* FROM CategoriaNoticia ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                CategoriaNoticia entidadeRetorno = new CategoriaNoticia();
                PopulaCategoriaNoticia(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os CategoriaNoticia existentes na base de dados.
        /// </summary>
		public IEnumerable<CategoriaNoticia> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de CategoriaNoticia na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de CategoriaNoticia na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM CategoriaNoticia");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um CategoriaNoticia baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">CategoriaNoticia a ser populado(.</param>
		public static void PopulaCategoriaNoticia(IDataReader reader, CategoriaNoticia entidade) 
		{						
			if (reader["categoriaNoticiaId"] != DBNull.Value)
				entidade.CategoriaNoticiaId = Convert.ToInt32(reader["categoriaNoticiaId"].ToString());
			
			if (reader["nomeCategoriaNoticia"] != DBNull.Value)
				entidade.NomeCategoriaNoticia = reader["nomeCategoriaNoticia"].ToString();
			

		}		
		
	}
}
		