
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
	public partial class FabricanteADO : ADOSuper, IFabricanteDAL {
	
	    /// <summary>
        /// Método que persiste um Fabricante.
        /// </summary>
        /// <param name="entidade">Fabricante contendo os dados a serem persistidos.</param>	
		public void Inserir(Fabricante entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO Fabricante ");
			sbSQL.Append(" (nomeFabricante) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@nomeFabricante) ");											

			sbSQL.Append(" ; SET @fabricanteId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@fabricanteId", DbType.Int32, 8);

			_db.AddInParameter(command, "@nomeFabricante", DbType.String, entidade.NomeFabricante);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.FabricanteId = Convert.ToInt32(_db.GetParameterValue(command, "@fabricanteId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um Fabricante.
        /// </summary>
        /// <param name="entidade">Fabricante contendo os dados a serem atualizados.</param>
		public void Atualizar(Fabricante entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE Fabricante SET ");
			sbSQL.Append(" nomeFabricante=@nomeFabricante ");
			sbSQL.Append(" WHERE fabricanteId=@fabricanteId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@fabricanteId", DbType.Int32, entidade.FabricanteId);
			_db.AddInParameter(command, "@nomeFabricante", DbType.String, entidade.NomeFabricante);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um Fabricante da base de dados.
        /// </summary>
        /// <param name="entidade">Fabricante a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(Fabricante entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM Fabricante ");
			sbSQL.Append("WHERE fabricanteId=@fabricanteId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@fabricanteId", DbType.Int32, entidade.FabricanteId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um Fabricante.
		/// </summary>
        /// <param name="entidade">Fabricante a ser carregado (somente o identificador é necessário).</param>
		/// <returns>Fabricante</returns>
		public Fabricante Carregar(int fabricanteId) {		
			Fabricante entidade = new Fabricante();
			entidade.FabricanteId = fabricanteId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um Fabricante.
		/// </summary>
        /// <param name="entidade">Fabricante a ser carregado (somente o identificador é necessário).</param>
		/// <returns>Fabricante</returns>
		public Fabricante Carregar(Fabricante entidade) {		
		
			Fabricante entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM Fabricante WHERE fabricanteId=@fabricanteId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@fabricanteId", DbType.Int32, entidade.FabricanteId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new Fabricante();
				PopulaFabricante(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de Fabricante.
        /// </summary>
        /// <param name="entidade">Produto relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Fabricante.</returns>
		public IEnumerable<Fabricante> Carregar(Produto entidade)
		{		
			List<Fabricante> entidadesRetorno = new List<Fabricante>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Fabricante.* FROM Fabricante INNER JOIN Produto ON Fabricante.fabricanteId=Produto.fabricanteId WHERE Produto.produtoId=@produtoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.ProdutoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Fabricante entidadeRetorno = new Fabricante();
                PopulaFabricante(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de Fabricante.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos Fabricante.</returns>
		public IEnumerable<Fabricante> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<Fabricante> entidadesRetorno = new List<Fabricante>();
			
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
				sbOrder.Append( " ORDER BY fabricanteId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM Fabricante");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Fabricante WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Fabricante ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT Fabricante.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM Fabricante ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT Fabricante.* FROM Fabricante ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Fabricante entidadeRetorno = new Fabricante();
                PopulaFabricante(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os Fabricante existentes na base de dados.
        /// </summary>
		public IEnumerable<Fabricante> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de Fabricante na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de Fabricante na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM Fabricante");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um Fabricante baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Fabricante a ser populado(.</param>
		public static void PopulaFabricante(IDataReader reader, Fabricante entidade) 
		{						
			if (reader["fabricanteId"] != DBNull.Value)
				entidade.FabricanteId = Convert.ToInt32(reader["fabricanteId"].ToString());
			
			if (reader["nomeFabricante"] != DBNull.Value)
				entidade.NomeFabricante = reader["nomeFabricante"].ToString();
			

		}		
		
	}
}
		