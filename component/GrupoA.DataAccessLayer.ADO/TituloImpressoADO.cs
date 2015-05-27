
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
	public partial class TituloImpressoADO : ADOSuper, ITituloImpressoDAL {
	
	    /// <summary>
        /// Método que persiste um TituloImpresso.
        /// </summary>
        /// <param name="entidade">TituloImpresso contendo os dados a serem persistidos.</param>	
		public void Inserir(TituloImpresso entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO TituloImpresso ");
			sbSQL.Append(" (tituloImpressoId, isbn10, isbn13, tituloId) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@tituloImpressoId, @isbn10, @isbn13, @tituloId) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@tituloImpressoId", DbType.Int32, entidade.TituloImpressoId);

			if (entidade.Isbn10 != null ) 
				_db.AddInParameter(command, "@isbn10", DbType.String, entidade.Isbn10);
			else
				_db.AddInParameter(command, "@isbn10", DbType.String, null);

			_db.AddInParameter(command, "@isbn13", DbType.String, entidade.Isbn13);

			_db.AddInParameter(command, "@tituloId", DbType.Int32, entidade.Titulo.TituloId);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um TituloImpresso.
        /// </summary>
        /// <param name="entidade">TituloImpresso contendo os dados a serem atualizados.</param>
		public void Atualizar(TituloImpresso entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE TituloImpresso SET ");
			sbSQL.Append(" isbn10=@isbn10, isbn13=@isbn13, tituloId=@tituloId ");
			sbSQL.Append(" WHERE tituloImpressoId=@tituloImpressoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@tituloImpressoId", DbType.Int32, entidade.TituloImpressoId);
			if (entidade.Isbn10 != null ) 
				_db.AddInParameter(command, "@isbn10", DbType.String, entidade.Isbn10);
			else
				_db.AddInParameter(command, "@isbn10", DbType.String, null);
			_db.AddInParameter(command, "@isbn13", DbType.String, entidade.Isbn13);
			_db.AddInParameter(command, "@tituloId", DbType.Int32, entidade.Titulo.TituloId);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um TituloImpresso da base de dados.
        /// </summary>
        /// <param name="entidade">TituloImpresso a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(TituloImpresso entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM TituloImpresso ");
			sbSQL.Append("WHERE tituloImpressoId=@tituloImpressoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@tituloImpressoId", DbType.Int32, entidade.TituloImpressoId);

								
			_db.ExecuteNonQuery(command);
		}
		

		/// <summary>
		/// Método que carrega um TituloImpresso.
		/// </summary>
        /// <param name="entidade">TituloImpresso a ser carregado (somente o identificador é necessário).</param>
		/// <returns>TituloImpresso</returns>
		public TituloImpresso Carregar(TituloImpresso entidade) {		
		
			TituloImpresso entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM TituloImpresso WHERE tituloImpressoId=@tituloImpressoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@tituloImpressoId", DbType.Int32, entidade.TituloImpressoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new TituloImpresso();
				PopulaTituloImpresso(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		
		
		/// <summary>
		/// Método que carrega um TituloImpresso com suas dependências.
		/// </summary>
        /// <param name="entidade">TituloImpresso a ser carregado (somente o identificador é necessário).</param>
		/// <returns>TituloImpresso</returns>
		public TituloImpresso CarregarComDependencias(TituloImpresso entidade) {		
		
			TituloImpresso entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append("SELECT TituloImpresso.tituloImpressoId, TituloImpresso.isbn10, TituloImpresso.isbn13, TituloImpresso.tituloId");
			sbSQL.Append(", produtoId, produtoTipoId, disponivel, fabricanteId, valorUnitario, valorOferta, codigoEAN13, codigoProduto, exibirSite, homologado, nomeProduto, utilizaFrete, peso");
			sbSQL.Append(", conteudoId, conteudoTipoId, dataHoraCadastro");
			sbSQL.Append(" FROM TituloImpresso");
			sbSQL.Append(" INNER JOIN Produto ON TituloImpresso.tituloImpressoId=Produto.produtoId");
			sbSQL.Append(" INNER JOIN Conteudo ON Produto.produtoId=Conteudo.conteudoId");
			sbSQL.Append(" WHERE TituloImpresso.tituloImpressoId=@tituloImpressoId");
						
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@tituloImpressoId", DbType.Int32, entidade.TituloImpressoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new TituloImpresso();
				PopulaTituloImpresso(reader, entidadeRetorno);
				entidadeRetorno.Produto = new Produto();
				ProdutoADO.PopulaProduto(reader, entidadeRetorno.Produto);
				entidadeRetorno.Produto.Conteudo = new Conteudo();
				ConteudoADO.PopulaConteudo(reader, entidadeRetorno.Produto.Conteudo);
			}
			reader.Close();
			
			return entidadeRetorno;
		}		


		/// <summary>
        /// Método que retorna uma coleção de TituloImpresso.
        /// </summary>
        /// <param name="entidade">CapituloImpresso relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de TituloImpresso.</returns>
		public IEnumerable<TituloImpresso> Carregar(CapituloImpresso entidade)
		{		
			List<TituloImpresso> entidadesRetorno = new List<TituloImpresso>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT TituloImpresso.* FROM TituloImpresso INNER JOIN CapituloImpresso ON TituloImpresso.tituloImpressoId=CapituloImpresso.tituloImpressoId WHERE CapituloImpresso.capituloImpressoId=@capituloImpressoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@capituloImpressoId", DbType.Int32, entidade.CapituloImpressoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                TituloImpresso entidadeRetorno = new TituloImpresso();
                PopulaTituloImpresso(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna um TituloImpresso.
        /// </summary>
        /// <param name="entidade">Titulo relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna um TituloImpresso.</returns>
		public TituloImpresso Carregar(Titulo entidade)
		{		
			TituloImpresso entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT TituloImpresso.* FROM TituloImpresso WHERE TituloImpresso.tituloId=@tituloId");
		
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@tituloId", DbType.Int32, entidade.TituloId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            if (reader.Read())
            {
                entidadeRetorno = new TituloImpresso();
                PopulaTituloImpresso(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de TituloImpresso.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos TituloImpresso.</returns>
		public IEnumerable<TituloImpresso> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<TituloImpresso> entidadesRetorno = new List<TituloImpresso>();
			
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
				sbOrder.Append( " ORDER BY tituloImpressoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM TituloImpresso");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM TituloImpresso WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM TituloImpresso ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT TituloImpresso.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM TituloImpresso ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT TituloImpresso.* FROM TituloImpresso ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                TituloImpresso entidadeRetorno = new TituloImpresso();
                PopulaTituloImpresso(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os TituloImpresso existentes na base de dados.
        /// </summary>
		public IEnumerable<TituloImpresso> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de TituloImpresso na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de TituloImpresso na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM TituloImpresso");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um TituloImpresso baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">TituloImpresso a ser populado(.</param>
		public static void PopulaTituloImpresso(IDataReader reader, TituloImpresso entidade) 
		{						
			if (reader["isbn10"] != DBNull.Value)
				entidade.Isbn10 = reader["isbn10"].ToString();
			
			if (reader["isbn13"] != DBNull.Value)
				entidade.Isbn13 = reader["isbn13"].ToString();
			
			if (reader["tituloImpressoId"] != DBNull.Value) {
				entidade.TituloImpressoId = Convert.ToInt32(reader["tituloImpressoId"].ToString());
			}

			if (reader["tituloId"] != DBNull.Value) {
				entidade.Titulo = new Titulo();
				entidade.Titulo.TituloId = Convert.ToInt32(reader["tituloId"].ToString());
			}


		}		
		
	}
}
		