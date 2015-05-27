
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
	public partial class ProdutoTipoADO : ADOSuper, IProdutoTipoDAL {
	
	    /// <summary>
        /// Método que persiste um ProdutoTipo.
        /// </summary>
        /// <param name="entidade">ProdutoTipo contendo os dados a serem persistidos.</param>	
		public void Inserir(ProdutoTipo entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO ProdutoTipo ");
			sbSQL.Append(" (produtoTipoId, tipo) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@produtoTipoId, @tipo) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@produtoTipoId", DbType.Int32, entidade.ProdutoTipoId);

			_db.AddInParameter(command, "@tipo", DbType.String, entidade.Tipo);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um ProdutoTipo.
        /// </summary>
        /// <param name="entidade">ProdutoTipo contendo os dados a serem atualizados.</param>
		public void Atualizar(ProdutoTipo entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE ProdutoTipo SET ");
			sbSQL.Append(" tipo=@tipo ");
			sbSQL.Append(" WHERE produtoTipoId=@produtoTipoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@produtoTipoId", DbType.Int32, entidade.ProdutoTipoId);
			_db.AddInParameter(command, "@tipo", DbType.String, entidade.Tipo);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um ProdutoTipo da base de dados.
        /// </summary>
        /// <param name="entidade">ProdutoTipo a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(ProdutoTipo entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM ProdutoTipo ");
			sbSQL.Append("WHERE produtoTipoId=@produtoTipoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@produtoTipoId", DbType.Int32, entidade.ProdutoTipoId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um ProdutoTipo.
		/// </summary>
        /// <param name="entidade">ProdutoTipo a ser carregado (somente o identificador é necessário).</param>
		/// <returns>ProdutoTipo</returns>
		public ProdutoTipo Carregar(int produtoTipoId) {		
			ProdutoTipo entidade = new ProdutoTipo();
			entidade.ProdutoTipoId = produtoTipoId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um ProdutoTipo.
		/// </summary>
        /// <param name="entidade">ProdutoTipo a ser carregado (somente o identificador é necessário).</param>
		/// <returns>ProdutoTipo</returns>
		public ProdutoTipo Carregar(ProdutoTipo entidade) {		
		
			ProdutoTipo entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM ProdutoTipo WHERE produtoTipoId=@produtoTipoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@produtoTipoId", DbType.Int32, entidade.ProdutoTipoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new ProdutoTipo();
				PopulaProdutoTipo(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de ProdutoTipo.
        /// </summary>
        /// <param name="entidade">Produto relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de ProdutoTipo.</returns>
		public IEnumerable<ProdutoTipo> Carregar(Produto entidade)
		{		
			List<ProdutoTipo> entidadesRetorno = new List<ProdutoTipo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT ProdutoTipo.* FROM ProdutoTipo INNER JOIN Produto ON ProdutoTipo.produtoTipoId=Produto.produtoTipoId WHERE Produto.produtoId=@produtoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.ProdutoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                ProdutoTipo entidadeRetorno = new ProdutoTipo();
                PopulaProdutoTipo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de ProdutoTipo.
        /// </summary>
        /// <param name="entidade">Promocao relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de ProdutoTipo.</returns>
		public IEnumerable<ProdutoTipo> Carregar(Promocao entidade)
		{		
			List<ProdutoTipo> entidadesRetorno = new List<ProdutoTipo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT ProdutoTipo.* FROM ProdutoTipo INNER JOIN PromocaoProdutoTipo ON ProdutoTipo.produtoTipoId=PromocaoProdutoTipo.produtoTipoId WHERE PromocaoProdutoTipo.promocaoId=@promocaoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@promocaoId", DbType.Int32, entidade.PromocaoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                ProdutoTipo entidadeRetorno = new ProdutoTipo();
                PopulaProdutoTipo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de ProdutoTipo.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos ProdutoTipo.</returns>
		public IEnumerable<ProdutoTipo> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<ProdutoTipo> entidadesRetorno = new List<ProdutoTipo>();
			
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
				sbOrder.Append( " ORDER BY produtoTipoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM ProdutoTipo");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ProdutoTipo WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ProdutoTipo ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT ProdutoTipo.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM ProdutoTipo ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT ProdutoTipo.* FROM ProdutoTipo ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                ProdutoTipo entidadeRetorno = new ProdutoTipo();
                PopulaProdutoTipo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os ProdutoTipo existentes na base de dados.
        /// </summary>
		public IEnumerable<ProdutoTipo> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de ProdutoTipo na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de ProdutoTipo na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM ProdutoTipo");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um ProdutoTipo baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">ProdutoTipo a ser populado(.</param>
		public static void PopulaProdutoTipo(IDataReader reader, ProdutoTipo entidade) 
		{						
			if (reader["produtoTipoId"] != DBNull.Value)
				entidade.ProdutoTipoId = Convert.ToInt32(reader["produtoTipoId"].ToString());
			
			if (reader["tipo"] != DBNull.Value)
				entidade.Tipo = reader["tipo"].ToString();
			

		}		
		
	}
}
		