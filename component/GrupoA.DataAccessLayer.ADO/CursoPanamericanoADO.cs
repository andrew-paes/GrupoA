
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
	public partial class CursoPanamericanoADO : ADOSuper, ICursoPanamericanoDAL {
	
	    /// <summary>
        /// Método que persiste um CursoPanamericano.
        /// </summary>
        /// <param name="entidade">CursoPanamericano contendo os dados a serem persistidos.</param>	
		public void Inserir(CursoPanamericano entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO CursoPanamericano ");
			sbSQL.Append(" (titulo, subtitulo, descricao, arquivoIdImagem, urlLink, targetBlank) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@titulo, @subtitulo, @descricao, @arquivoIdImagem, @urlLink, @targetBlank) ");											

			sbSQL.Append(" ; SET @cursoPanamericanoId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@cursoPanamericanoId", DbType.Int32, 8);

			_db.AddInParameter(command, "@titulo", DbType.String, entidade.Titulo);

			_db.AddInParameter(command, "@subtitulo", DbType.String, entidade.Subtitulo);

			_db.AddInParameter(command, "@descricao", DbType.String, entidade.Descricao);

			if (entidade.ArquivoImagem != null ) 
				_db.AddInParameter(command, "@arquivoIdImagem", DbType.Int32, entidade.ArquivoImagem.ArquivoId);
			else
				_db.AddInParameter(command, "@arquivoIdImagem", DbType.Int32, null);

			if (entidade.UrlLink != null ) 
				_db.AddInParameter(command, "@urlLink", DbType.String, entidade.UrlLink);
			else
				_db.AddInParameter(command, "@urlLink", DbType.String, null);

			_db.AddInParameter(command, "@targetBlank", DbType.Int32, entidade.TargetBlank);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.CursoPanamericanoId = Convert.ToInt32(_db.GetParameterValue(command, "@cursoPanamericanoId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um CursoPanamericano.
        /// </summary>
        /// <param name="entidade">CursoPanamericano contendo os dados a serem atualizados.</param>
		public void Atualizar(CursoPanamericano entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE CursoPanamericano SET ");
			sbSQL.Append(" titulo=@titulo, subtitulo=@subtitulo, descricao=@descricao, arquivoIdImagem=@arquivoIdImagem, urlLink=@urlLink, targetBlank=@targetBlank ");
			sbSQL.Append(" WHERE cursoPanamericanoId=@cursoPanamericanoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@cursoPanamericanoId", DbType.Int32, entidade.CursoPanamericanoId);
			_db.AddInParameter(command, "@titulo", DbType.String, entidade.Titulo);
			_db.AddInParameter(command, "@subtitulo", DbType.String, entidade.Subtitulo);
			_db.AddInParameter(command, "@descricao", DbType.String, entidade.Descricao);
			if (entidade.ArquivoImagem != null ) 
				_db.AddInParameter(command, "@arquivoIdImagem", DbType.Int32, entidade.ArquivoImagem.ArquivoId);
			else
				_db.AddInParameter(command, "@arquivoIdImagem", DbType.Int32, null);
			if (entidade.UrlLink != null ) 
				_db.AddInParameter(command, "@urlLink", DbType.String, entidade.UrlLink);
			else
				_db.AddInParameter(command, "@urlLink", DbType.String, null);
			_db.AddInParameter(command, "@targetBlank", DbType.Int32, entidade.TargetBlank);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um CursoPanamericano da base de dados.
        /// </summary>
        /// <param name="entidade">CursoPanamericano a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(CursoPanamericano entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM CursoPanamericano ");
			sbSQL.Append("WHERE cursoPanamericanoId=@cursoPanamericanoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@cursoPanamericanoId", DbType.Int32, entidade.CursoPanamericanoId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um CursoPanamericano.
		/// </summary>
        /// <param name="entidade">CursoPanamericano a ser carregado (somente o identificador é necessário).</param>
		/// <returns>CursoPanamericano</returns>
		public CursoPanamericano Carregar(int cursoPanamericanoId) {		
			CursoPanamericano entidade = new CursoPanamericano();
			entidade.CursoPanamericanoId = cursoPanamericanoId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um CursoPanamericano.
		/// </summary>
        /// <param name="entidade">CursoPanamericano a ser carregado (somente o identificador é necessário).</param>
		/// <returns>CursoPanamericano</returns>
		public CursoPanamericano Carregar(CursoPanamericano entidade) {		
		
			CursoPanamericano entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM CursoPanamericano WHERE cursoPanamericanoId=@cursoPanamericanoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@cursoPanamericanoId", DbType.Int32, entidade.CursoPanamericanoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new CursoPanamericano();
				PopulaCursoPanamericano(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de CursoPanamericano.
        /// </summary>
        /// <param name="entidade">Categoria relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de CursoPanamericano.</returns>
		public IEnumerable<CursoPanamericano> Carregar(Categoria entidade)
		{		
			List<CursoPanamericano> entidadesRetorno = new List<CursoPanamericano>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT CursoPanamericano.* FROM CursoPanamericano INNER JOIN CursoPanamericanoCategoria ON CursoPanamericano.cursoPanamericanoId=CursoPanamericanoCategoria.cursoPanamericanoId WHERE CursoPanamericanoCategoria.categoriaId=@categoriaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@categoriaId", DbType.Int32, entidade.CategoriaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                CursoPanamericano entidadeRetorno = new CursoPanamericano();
                PopulaCursoPanamericano(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de CursoPanamericano.
        /// </summary>
        /// <param name="entidade">Arquivo relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de CursoPanamericano.</returns>
		public IEnumerable<CursoPanamericano> Carregar(Arquivo entidade)
		{		
			List<CursoPanamericano> entidadesRetorno = new List<CursoPanamericano>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT CursoPanamericano.* FROM CursoPanamericano WHERE CursoPanamericano.arquivoId=@arquivoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.ArquivoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                CursoPanamericano entidadeRetorno = new CursoPanamericano();
                PopulaCursoPanamericano(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de CursoPanamericano.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos CursoPanamericano.</returns>
		public IEnumerable<CursoPanamericano> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<CursoPanamericano> entidadesRetorno = new List<CursoPanamericano>();
			
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
				sbOrder.Append( " ORDER BY cursoPanamericanoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM CursoPanamericano");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM CursoPanamericano WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM CursoPanamericano ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT CursoPanamericano.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM CursoPanamericano ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT CursoPanamericano.* FROM CursoPanamericano ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                CursoPanamericano entidadeRetorno = new CursoPanamericano();
                PopulaCursoPanamericano(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os CursoPanamericano existentes na base de dados.
        /// </summary>
		public IEnumerable<CursoPanamericano> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de CursoPanamericano na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de CursoPanamericano na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM CursoPanamericano");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um CursoPanamericano baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">CursoPanamericano a ser populado(.</param>
		public static void PopulaCursoPanamericano(IDataReader reader, CursoPanamericano entidade) 
		{						
			if (reader["cursoPanamericanoId"] != DBNull.Value)
				entidade.CursoPanamericanoId = Convert.ToInt32(reader["cursoPanamericanoId"].ToString());
			
			if (reader["titulo"] != DBNull.Value)
				entidade.Titulo = reader["titulo"].ToString();
			
			if (reader["subtitulo"] != DBNull.Value)
				entidade.Subtitulo = reader["subtitulo"].ToString();
			
			if (reader["descricao"] != DBNull.Value)
				entidade.Descricao = reader["descricao"].ToString();
			
			if (reader["urlLink"] != DBNull.Value)
				entidade.UrlLink = reader["urlLink"].ToString();
			
			if (reader["targetBlank"] != DBNull.Value)
				entidade.TargetBlank = Convert.ToBoolean(reader["targetBlank"].ToString());
			
			if (reader["arquivoIdImagem"] != DBNull.Value) {
				entidade.ArquivoImagem = new Arquivo();
				entidade.ArquivoImagem.ArquivoId = Convert.ToInt32(reader["arquivoIdImagem"].ToString());
			}


		}		
		
	}
}
		