
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
	public partial class CompraConjuntaPaginaADO : ADOSuper, ICompraConjuntaPaginaDAL {
	
	    /// <summary>
        /// Método que persiste um CompraConjuntaPagina.
        /// </summary>
        /// <param name="entidade">CompraConjuntaPagina contendo os dados a serem persistidos.</param>	
		public void Inserir(CompraConjuntaPagina entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO CompraConjuntaPagina ");
			sbSQL.Append(" (compraConjuntaPaginaId, pagina) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@compraConjuntaPaginaId, @pagina) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@compraConjuntaPaginaId", DbType.Int32, entidade.CompraConjuntaPaginaId);

			_db.AddInParameter(command, "@pagina", DbType.String, entidade.Pagina);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um CompraConjuntaPagina.
        /// </summary>
        /// <param name="entidade">CompraConjuntaPagina contendo os dados a serem atualizados.</param>
		public void Atualizar(CompraConjuntaPagina entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE CompraConjuntaPagina SET ");
			sbSQL.Append(" pagina=@pagina ");
			sbSQL.Append(" WHERE compraConjuntaPaginaId=@compraConjuntaPaginaId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@compraConjuntaPaginaId", DbType.Int32, entidade.CompraConjuntaPaginaId);
			_db.AddInParameter(command, "@pagina", DbType.String, entidade.Pagina);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um CompraConjuntaPagina da base de dados.
        /// </summary>
        /// <param name="entidade">CompraConjuntaPagina a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(CompraConjuntaPagina entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM CompraConjuntaPagina ");
			sbSQL.Append("WHERE compraConjuntaPaginaId=@compraConjuntaPaginaId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@compraConjuntaPaginaId", DbType.Int32, entidade.CompraConjuntaPaginaId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um CompraConjuntaPagina.
		/// </summary>
        /// <param name="entidade">CompraConjuntaPagina a ser carregado (somente o identificador é necessário).</param>
		/// <returns>CompraConjuntaPagina</returns>
		public CompraConjuntaPagina Carregar(int compraConjuntaPaginaId) {		
			CompraConjuntaPagina entidade = new CompraConjuntaPagina();
			entidade.CompraConjuntaPaginaId = compraConjuntaPaginaId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um CompraConjuntaPagina.
		/// </summary>
        /// <param name="entidade">CompraConjuntaPagina a ser carregado (somente o identificador é necessário).</param>
		/// <returns>CompraConjuntaPagina</returns>
		public CompraConjuntaPagina Carregar(CompraConjuntaPagina entidade) {		
		
			CompraConjuntaPagina entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM CompraConjuntaPagina WHERE compraConjuntaPaginaId=@compraConjuntaPaginaId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@compraConjuntaPaginaId", DbType.Int32, entidade.CompraConjuntaPaginaId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new CompraConjuntaPagina();
				PopulaCompraConjuntaPagina(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de CompraConjuntaPagina.
        /// </summary>
        /// <param name="entidade">CompraConjunta relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de CompraConjuntaPagina.</returns>
		public IEnumerable<CompraConjuntaPagina> Carregar(CompraConjunta entidade)
		{		
			List<CompraConjuntaPagina> entidadesRetorno = new List<CompraConjuntaPagina>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT CompraConjuntaPagina.* FROM CompraConjuntaPagina INNER JOIN CompraConjuntaLocalizacao ON CompraConjuntaPagina.compraConjuntaPaginaId=CompraConjuntaLocalizacao.compraConjuntaPaginaId WHERE CompraConjuntaLocalizacao.compraConjuntaId=@compraConjuntaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@compraConjuntaId", DbType.Int32, entidade.CompraConjuntaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                CompraConjuntaPagina entidadeRetorno = new CompraConjuntaPagina();
                PopulaCompraConjuntaPagina(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de CompraConjuntaPagina.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos CompraConjuntaPagina.</returns>
		public IEnumerable<CompraConjuntaPagina> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<CompraConjuntaPagina> entidadesRetorno = new List<CompraConjuntaPagina>();
			
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
				sbOrder.Append( " ORDER BY compraConjuntaPaginaId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM CompraConjuntaPagina");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM CompraConjuntaPagina WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM CompraConjuntaPagina ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT CompraConjuntaPagina.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM CompraConjuntaPagina ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT CompraConjuntaPagina.* FROM CompraConjuntaPagina ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                CompraConjuntaPagina entidadeRetorno = new CompraConjuntaPagina();
                PopulaCompraConjuntaPagina(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os CompraConjuntaPagina existentes na base de dados.
        /// </summary>
		public IEnumerable<CompraConjuntaPagina> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de CompraConjuntaPagina na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de CompraConjuntaPagina na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM CompraConjuntaPagina");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um CompraConjuntaPagina baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">CompraConjuntaPagina a ser populado(.</param>
		public static void PopulaCompraConjuntaPagina(IDataReader reader, CompraConjuntaPagina entidade) 
		{						
			if (reader["compraConjuntaPaginaId"] != DBNull.Value)
				entidade.CompraConjuntaPaginaId = Convert.ToInt32(reader["compraConjuntaPaginaId"].ToString());
			
			if (reader["pagina"] != DBNull.Value)
				entidade.Pagina = reader["pagina"].ToString();
			

		}		
		
	}
}
		