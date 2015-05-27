
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
	public partial class ProdutoImagemTipoADO : ADOSuper, IProdutoImagemTipoDAL {
	
	    /// <summary>
        /// Método que persiste um ProdutoImagemTipo.
        /// </summary>
        /// <param name="entidade">ProdutoImagemTipo contendo os dados a serem persistidos.</param>	
		public void Inserir(ProdutoImagemTipo entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO ProdutoImagemTipo ");
			sbSQL.Append(" (produtoImagemTipoId, tipoImagem) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@produtoImagemTipoId, @tipoImagem) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@produtoImagemTipoId", DbType.Int32, entidade.ProdutoImagemTipoId);

			_db.AddInParameter(command, "@tipoImagem", DbType.String, entidade.TipoImagem);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um ProdutoImagemTipo.
        /// </summary>
        /// <param name="entidade">ProdutoImagemTipo contendo os dados a serem atualizados.</param>
		public void Atualizar(ProdutoImagemTipo entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE ProdutoImagemTipo SET ");
			sbSQL.Append(" tipoImagem=@tipoImagem ");
			sbSQL.Append(" WHERE produtoImagemTipoId=@produtoImagemTipoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@produtoImagemTipoId", DbType.Int32, entidade.ProdutoImagemTipoId);
			_db.AddInParameter(command, "@tipoImagem", DbType.String, entidade.TipoImagem);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um ProdutoImagemTipo da base de dados.
        /// </summary>
        /// <param name="entidade">ProdutoImagemTipo a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(ProdutoImagemTipo entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM ProdutoImagemTipo ");
			sbSQL.Append("WHERE produtoImagemTipoId=@produtoImagemTipoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@produtoImagemTipoId", DbType.Int32, entidade.ProdutoImagemTipoId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um ProdutoImagemTipo.
		/// </summary>
        /// <param name="entidade">ProdutoImagemTipo a ser carregado (somente o identificador é necessário).</param>
		/// <returns>ProdutoImagemTipo</returns>
		public ProdutoImagemTipo Carregar(int produtoImagemTipoId) {		
			ProdutoImagemTipo entidade = new ProdutoImagemTipo();
			entidade.ProdutoImagemTipoId = produtoImagemTipoId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um ProdutoImagemTipo.
		/// </summary>
        /// <param name="entidade">ProdutoImagemTipo a ser carregado (somente o identificador é necessário).</param>
		/// <returns>ProdutoImagemTipo</returns>
		public ProdutoImagemTipo Carregar(ProdutoImagemTipo entidade) {		
		
			ProdutoImagemTipo entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM ProdutoImagemTipo WHERE produtoImagemTipoId=@produtoImagemTipoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@produtoImagemTipoId", DbType.Int32, entidade.ProdutoImagemTipoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new ProdutoImagemTipo();
				PopulaProdutoImagemTipo(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de ProdutoImagemTipo.
        /// </summary>
        /// <param name="entidade">ProdutoImagem relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de ProdutoImagemTipo.</returns>
		public IEnumerable<ProdutoImagemTipo> Carregar(ProdutoImagem entidade)
		{		
			List<ProdutoImagemTipo> entidadesRetorno = new List<ProdutoImagemTipo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT ProdutoImagemTipo.* FROM ProdutoImagemTipo INNER JOIN ProdutoImagem ON ProdutoImagemTipo.produtoImagemTipoId=ProdutoImagem.produtoImagemTipoId WHERE ProdutoImagem.produtoImagemId=@produtoImagemId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@produtoImagemId", DbType.Int32, entidade.ProdutoImagemId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                ProdutoImagemTipo entidadeRetorno = new ProdutoImagemTipo();
                PopulaProdutoImagemTipo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de ProdutoImagemTipo.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos ProdutoImagemTipo.</returns>
		public IEnumerable<ProdutoImagemTipo> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<ProdutoImagemTipo> entidadesRetorno = new List<ProdutoImagemTipo>();
			
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
				sbOrder.Append( " ORDER BY produtoImagemTipoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM ProdutoImagemTipo");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ProdutoImagemTipo WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ProdutoImagemTipo ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT ProdutoImagemTipo.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM ProdutoImagemTipo ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT ProdutoImagemTipo.* FROM ProdutoImagemTipo ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                ProdutoImagemTipo entidadeRetorno = new ProdutoImagemTipo();
                PopulaProdutoImagemTipo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os ProdutoImagemTipo existentes na base de dados.
        /// </summary>
		public IEnumerable<ProdutoImagemTipo> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de ProdutoImagemTipo na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de ProdutoImagemTipo na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM ProdutoImagemTipo");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um ProdutoImagemTipo baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">ProdutoImagemTipo a ser populado(.</param>
		public static void PopulaProdutoImagemTipo(IDataReader reader, ProdutoImagemTipo entidade) 
		{						
			if (reader["produtoImagemTipoId"] != DBNull.Value)
				entidade.ProdutoImagemTipoId = Convert.ToInt32(reader["produtoImagemTipoId"].ToString());
			
			if (reader["tipoImagem"] != DBNull.Value)
				entidade.TipoImagem = reader["tipoImagem"].ToString();
			

		}		
		
	}
}
		