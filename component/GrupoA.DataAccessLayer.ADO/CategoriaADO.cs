
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
	public partial class CategoriaADO : ADOSuper, ICategoriaDAL {
	
	    /// <summary>
        /// Método que persiste um Categoria.
        /// </summary>
        /// <param name="entidade">Categoria contendo os dados a serem persistidos.</param>	
		public void Inserir(Categoria entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO Categoria ");
			sbSQL.Append(" (nomeCategoria, categoriaIdPai, codigoCategoria) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@nomeCategoria, @categoriaIdPai, @codigoCategoria) ");											

			sbSQL.Append(" ; SET @categoriaId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@categoriaId", DbType.Int32, 8);

			_db.AddInParameter(command, "@nomeCategoria", DbType.String, entidade.NomeCategoria);

			if (entidade.CategoriaPai != null ) 
				_db.AddInParameter(command, "@categoriaIdPai", DbType.Int32, entidade.CategoriaPai.CategoriaId);
			else
				_db.AddInParameter(command, "@categoriaIdPai", DbType.Int32, null);

			if (entidade.CodigoCategoria != null ) 
				_db.AddInParameter(command, "@codigoCategoria", DbType.String, entidade.CodigoCategoria);
			else
				_db.AddInParameter(command, "@codigoCategoria", DbType.String, null);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.CategoriaId = Convert.ToInt32(_db.GetParameterValue(command, "@categoriaId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um Categoria.
        /// </summary>
        /// <param name="entidade">Categoria contendo os dados a serem atualizados.</param>
		public void Atualizar(Categoria entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE Categoria SET ");
			sbSQL.Append(" nomeCategoria=@nomeCategoria, categoriaIdPai=@categoriaIdPai, codigoCategoria=@codigoCategoria ");
			sbSQL.Append(" WHERE categoriaId=@categoriaId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@categoriaId", DbType.Int32, entidade.CategoriaId);
			_db.AddInParameter(command, "@nomeCategoria", DbType.String, entidade.NomeCategoria);
			if (entidade.CategoriaPai != null ) 
				_db.AddInParameter(command, "@categoriaIdPai", DbType.Int32, entidade.CategoriaPai.CategoriaId);
			else
				_db.AddInParameter(command, "@categoriaIdPai", DbType.Int32, null);
			if (entidade.CodigoCategoria != null ) 
				_db.AddInParameter(command, "@codigoCategoria", DbType.String, entidade.CodigoCategoria);
			else
				_db.AddInParameter(command, "@codigoCategoria", DbType.String, null);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um Categoria da base de dados.
        /// </summary>
        /// <param name="entidade">Categoria a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(Categoria entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM Categoria ");
			sbSQL.Append("WHERE categoriaId=@categoriaId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@categoriaId", DbType.Int32, entidade.CategoriaId);

								
			_db.ExecuteNonQuery(command);
		}
		

		/// <summary>
		/// Método que carrega um Categoria.
		/// </summary>
        /// <param name="entidade">Categoria a ser carregado (somente o identificador é necessário).</param>
		/// <returns>Categoria</returns>
		public Categoria Carregar(Categoria entidade) {		
		
			Categoria entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM Categoria WHERE categoriaId=@categoriaId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@categoriaId", DbType.Int32, entidade.CategoriaId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new Categoria();
				PopulaCategoria(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de Categoria.
        /// </summary>
        /// <param name="entidade">Conteudo relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Categoria.</returns>
		public IEnumerable<Categoria> Carregar(Conteudo entidade)
		{		
			List<Categoria> entidadesRetorno = new List<Categoria>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Categoria.* FROM Categoria INNER JOIN ConteudoAreaConhecimento ON Categoria.categoriaId=ConteudoAreaConhecimento.categoriaId WHERE ConteudoAreaConhecimento.conteudoId=@conteudoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@conteudoId", DbType.Int32, entidade.ConteudoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Categoria entidadeRetorno = new Categoria();
                PopulaCategoria(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Categoria.
        /// </summary>
        /// <param name="entidade">CursoPanamericano relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Categoria.</returns>
		public IEnumerable<Categoria> Carregar(CursoPanamericano entidade)
		{		
			List<Categoria> entidadesRetorno = new List<Categoria>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Categoria.* FROM Categoria INNER JOIN CursoPanamericanoCategoria ON Categoria.categoriaId=CursoPanamericanoCategoria.categoriaId WHERE CursoPanamericanoCategoria.cursoPanamericanoId=@cursoPanamericanoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@cursoPanamericanoId", DbType.Int32, entidade.CursoPanamericanoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Categoria entidadeRetorno = new Categoria();
                PopulaCategoria(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Categoria.
        /// </summary>
        /// <param name="entidade">Produto relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Categoria.</returns>
		public IEnumerable<Categoria> Carregar(Produto entidade)
		{		
			List<Categoria> entidadesRetorno = new List<Categoria>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Categoria.* FROM Categoria INNER JOIN ProdutoCategoria ON Categoria.categoriaId=ProdutoCategoria.categoriaId WHERE ProdutoCategoria.produtoId=@produtoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.ProdutoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Categoria entidadeRetorno = new Categoria();
                PopulaCategoria(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Categoria.
        /// </summary>
        /// <param name="entidade">Promocao relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Categoria.</returns>
		public IEnumerable<Categoria> Carregar(Promocao entidade)
		{		
			List<Categoria> entidadesRetorno = new List<Categoria>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Categoria.* FROM Categoria INNER JOIN PromocaoCategoria ON Categoria.categoriaId=PromocaoCategoria.categoriaId WHERE PromocaoCategoria.promocaoId=@promocaoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@promocaoId", DbType.Int32, entidade.PromocaoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Categoria entidadeRetorno = new Categoria();
                PopulaCategoria(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Categoria.
        /// </summary>
        /// <param name="entidade">Revista relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Categoria.</returns>
		public IEnumerable<Categoria> Carregar(Revista entidade)
		{		
			List<Categoria> entidadesRetorno = new List<Categoria>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Categoria.* FROM Categoria INNER JOIN RevistaAreaConhecimento ON Categoria.categoriaId=RevistaAreaConhecimento.categoriaId WHERE RevistaAreaConhecimento.revistaId=@revistaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@revistaId", DbType.Int32, entidade.RevistaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Categoria entidadeRetorno = new Categoria();
                PopulaCategoria(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Categoria.
        /// </summary>
        /// <param name="entidade">Usuario relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Categoria.</returns>
		public IEnumerable<Categoria> Carregar(Usuario entidade)
		{		
			List<Categoria> entidadesRetorno = new List<Categoria>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Categoria.* FROM Categoria INNER JOIN UsuarioInteresse ON Categoria.categoriaId=UsuarioInteresse.categoriaId WHERE UsuarioInteresse.usuarioId=@usuarioId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.UsuarioId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Categoria entidadeRetorno = new Categoria();
                PopulaCategoria(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de Categoria.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos Categoria.</returns>
		public IEnumerable<Categoria> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<Categoria> entidadesRetorno = new List<Categoria>();
			
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
				sbOrder.Append( " ORDER BY categoriaId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM Categoria");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Categoria WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Categoria ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT Categoria.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM Categoria ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT Categoria.* FROM Categoria ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Categoria entidadeRetorno = new Categoria();
                PopulaCategoria(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os Categoria existentes na base de dados.
        /// </summary>
		public IEnumerable<Categoria> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de Categoria na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de Categoria na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM Categoria");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um Categoria baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Categoria a ser populado(.</param>
		public static void PopulaCategoria(IDataReader reader, Categoria entidade) 
		{						
			if (reader["categoriaId"] != DBNull.Value)
				entidade.CategoriaId = Convert.ToInt32(reader["categoriaId"].ToString());
			
			if (reader["nomeCategoria"] != DBNull.Value)
				entidade.NomeCategoria = reader["nomeCategoria"].ToString();
			
			if (reader["codigoCategoria"] != DBNull.Value)
				entidade.CodigoCategoria = reader["codigoCategoria"].ToString();
			
			if (reader["categoriaIdPai"] != DBNull.Value) {
				entidade.CategoriaPai = new Categoria();
				entidade.CategoriaPai.CategoriaId = Convert.ToInt32(reader["categoriaIdPai"].ToString());
			}


		}		
		
	}
}
		