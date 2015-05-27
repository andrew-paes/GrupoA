
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
	public partial class RevistaEdicaoADO : ADOSuper, IRevistaEdicaoDAL {
	
	    /// <summary>
        /// Método que persiste um RevistaEdicao.
        /// </summary>
        /// <param name="entidade">RevistaEdicao contendo os dados a serem persistidos.</param>	
		public void Inserir(RevistaEdicao entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO RevistaEdicao ");
			sbSQL.Append(" (revistaEdicaoId, revistaId, numeroEdicao, anoPublicacao, mesPublicacao, periodoPublicacao, anoEdicao, tituloEdicao, descricaoEdicao, ativo, numeroPaginas) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@revistaEdicaoId, @revistaId, @numeroEdicao, @anoPublicacao, @mesPublicacao, @periodoPublicacao, @anoEdicao, @tituloEdicao, @descricaoEdicao, @ativo, @numeroPaginas) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@revistaEdicaoId", DbType.Int32, entidade.RevistaEdicaoId);

			_db.AddInParameter(command, "@revistaId", DbType.Int32, entidade.Revista.RevistaId);

			if (entidade.NumeroEdicao != null ) 
				_db.AddInParameter(command, "@numeroEdicao", DbType.Int32, entidade.NumeroEdicao);
			else
				_db.AddInParameter(command, "@numeroEdicao", DbType.Int32, null);

			_db.AddInParameter(command, "@anoPublicacao", DbType.Int32, entidade.AnoPublicacao);

			_db.AddInParameter(command, "@mesPublicacao", DbType.Int32, entidade.MesPublicacao);

			_db.AddInParameter(command, "@periodoPublicacao", DbType.String, entidade.PeriodoPublicacao);

			if (entidade.AnoEdicao != null ) 
				_db.AddInParameter(command, "@anoEdicao", DbType.String, entidade.AnoEdicao);
			else
				_db.AddInParameter(command, "@anoEdicao", DbType.String, null);

			if (entidade.TituloEdicao != null ) 
				_db.AddInParameter(command, "@tituloEdicao", DbType.String, entidade.TituloEdicao);
			else
				_db.AddInParameter(command, "@tituloEdicao", DbType.String, null);

			if (entidade.DescricaoEdicao != null ) 
				_db.AddInParameter(command, "@descricaoEdicao", DbType.String, entidade.DescricaoEdicao);
			else
				_db.AddInParameter(command, "@descricaoEdicao", DbType.String, null);

			_db.AddInParameter(command, "@ativo", DbType.Int32, entidade.Ativo);

			if (entidade.NumeroPaginas != null ) 
				_db.AddInParameter(command, "@numeroPaginas", DbType.Int32, entidade.NumeroPaginas);
			else
				_db.AddInParameter(command, "@numeroPaginas", DbType.Int32, null);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um RevistaEdicao.
        /// </summary>
        /// <param name="entidade">RevistaEdicao contendo os dados a serem atualizados.</param>
		public void Atualizar(RevistaEdicao entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE RevistaEdicao SET ");
			sbSQL.Append(" revistaId=@revistaId, numeroEdicao=@numeroEdicao, anoPublicacao=@anoPublicacao, mesPublicacao=@mesPublicacao, periodoPublicacao=@periodoPublicacao, anoEdicao=@anoEdicao, tituloEdicao=@tituloEdicao, descricaoEdicao=@descricaoEdicao, ativo=@ativo, numeroPaginas=@numeroPaginas ");
			sbSQL.Append(" WHERE revistaEdicaoId=@revistaEdicaoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@revistaEdicaoId", DbType.Int32, entidade.RevistaEdicaoId);
			_db.AddInParameter(command, "@revistaId", DbType.Int32, entidade.Revista.RevistaId);
			if (entidade.NumeroEdicao != null ) 
				_db.AddInParameter(command, "@numeroEdicao", DbType.Int32, entidade.NumeroEdicao);
			else
				_db.AddInParameter(command, "@numeroEdicao", DbType.Int32, null);
			_db.AddInParameter(command, "@anoPublicacao", DbType.Int32, entidade.AnoPublicacao);
			_db.AddInParameter(command, "@mesPublicacao", DbType.Int32, entidade.MesPublicacao);
			_db.AddInParameter(command, "@periodoPublicacao", DbType.String, entidade.PeriodoPublicacao);
			if (entidade.AnoEdicao != null ) 
				_db.AddInParameter(command, "@anoEdicao", DbType.String, entidade.AnoEdicao);
			else
				_db.AddInParameter(command, "@anoEdicao", DbType.String, null);
			if (entidade.TituloEdicao != null ) 
				_db.AddInParameter(command, "@tituloEdicao", DbType.String, entidade.TituloEdicao);
			else
				_db.AddInParameter(command, "@tituloEdicao", DbType.String, null);
			if (entidade.DescricaoEdicao != null ) 
				_db.AddInParameter(command, "@descricaoEdicao", DbType.String, entidade.DescricaoEdicao);
			else
				_db.AddInParameter(command, "@descricaoEdicao", DbType.String, null);
			_db.AddInParameter(command, "@ativo", DbType.Int32, entidade.Ativo);
			if (entidade.NumeroPaginas != null ) 
				_db.AddInParameter(command, "@numeroPaginas", DbType.Int32, entidade.NumeroPaginas);
			else
				_db.AddInParameter(command, "@numeroPaginas", DbType.Int32, null);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um RevistaEdicao da base de dados.
        /// </summary>
        /// <param name="entidade">RevistaEdicao a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(RevistaEdicao entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM RevistaEdicao ");
			sbSQL.Append("WHERE revistaEdicaoId=@revistaEdicaoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@revistaEdicaoId", DbType.Int32, entidade.RevistaEdicaoId);

								
			_db.ExecuteNonQuery(command);
		}
		

		/// <summary>
		/// Método que carrega um RevistaEdicao.
		/// </summary>
        /// <param name="entidade">RevistaEdicao a ser carregado (somente o identificador é necessário).</param>
		/// <returns>RevistaEdicao</returns>
		public RevistaEdicao Carregar(RevistaEdicao entidade) {		
		
			RevistaEdicao entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM RevistaEdicao WHERE revistaEdicaoId=@revistaEdicaoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@revistaEdicaoId", DbType.Int32, entidade.RevistaEdicaoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new RevistaEdicao();
				PopulaRevistaEdicao(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		
		
		/// <summary>
		/// Método que carrega um RevistaEdicao com suas dependências.
		/// </summary>
        /// <param name="entidade">RevistaEdicao a ser carregado (somente o identificador é necessário).</param>
		/// <returns>RevistaEdicao</returns>
		public RevistaEdicao CarregarComDependencias(RevistaEdicao entidade) {		
		
			RevistaEdicao entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append("SELECT RevistaEdicao.revistaEdicaoId, RevistaEdicao.revistaId, RevistaEdicao.numeroEdicao, RevistaEdicao.anoPublicacao, RevistaEdicao.mesPublicacao, RevistaEdicao.periodoPublicacao, RevistaEdicao.anoEdicao, RevistaEdicao.tituloEdicao, RevistaEdicao.descricaoEdicao, RevistaEdicao.ativo, RevistaEdicao.numeroPaginas");
			sbSQL.Append(", produtoId, produtoTipoId, disponivel, fabricanteId, valorUnitario, valorOferta, codigoEAN13, codigoProduto, exibirSite, nomeProduto, utilizaFrete, peso, homologado");
			sbSQL.Append(", conteudoId, conteudoTipoId, dataHoraCadastro");
			sbSQL.Append(" FROM RevistaEdicao");
			sbSQL.Append(" INNER JOIN Produto ON RevistaEdicao.revistaEdicaoId=Produto.produtoId");
			sbSQL.Append(" INNER JOIN Conteudo ON Produto.produtoId=Conteudo.conteudoId");
			sbSQL.Append(" WHERE RevistaEdicao.revistaEdicaoId=@revistaEdicaoId");
						
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@revistaEdicaoId", DbType.Int32, entidade.RevistaEdicaoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new RevistaEdicao();
				PopulaRevistaEdicao(reader, entidadeRetorno);
				entidadeRetorno.Produto = new Produto();
				ProdutoADO.PopulaProduto(reader, entidadeRetorno.Produto);
				entidadeRetorno.Produto.Conteudo = new Conteudo();
				ConteudoADO.PopulaConteudo(reader, entidadeRetorno.Produto.Conteudo);
			}
			reader.Close();
			
			return entidadeRetorno;
		}		


		/// <summary>
        /// Método que retorna uma coleção de RevistaEdicao.
        /// </summary>
        /// <param name="entidade">RevistaArtigo relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de RevistaEdicao.</returns>
		public IEnumerable<RevistaEdicao> Carregar(RevistaArtigo entidade)
		{		
			List<RevistaEdicao> entidadesRetorno = new List<RevistaEdicao>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT RevistaEdicao.* FROM RevistaEdicao INNER JOIN RevistaArtigo ON RevistaEdicao.revistaEdicaoId=RevistaArtigo.revistaEdicaoId WHERE RevistaArtigo.revistaArtigoId=@revistaArtigoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@revistaArtigoId", DbType.Int32, entidade.RevistaArtigoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                RevistaEdicao entidadeRetorno = new RevistaEdicao();
                PopulaRevistaEdicao(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de RevistaEdicao.
        /// </summary>
        /// <param name="entidade">Revista relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de RevistaEdicao.</returns>
		public IEnumerable<RevistaEdicao> Carregar(Revista entidade)
		{		
			List<RevistaEdicao> entidadesRetorno = new List<RevistaEdicao>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT RevistaEdicao.* FROM RevistaEdicao WHERE RevistaEdicao.revistaId=@revistaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@revistaId", DbType.Int32, entidade.RevistaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                RevistaEdicao entidadeRetorno = new RevistaEdicao();
                PopulaRevistaEdicao(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de RevistaEdicao.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos RevistaEdicao.</returns>
		public IEnumerable<RevistaEdicao> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<RevistaEdicao> entidadesRetorno = new List<RevistaEdicao>();
			
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
				sbOrder.Append( " ORDER BY revistaEdicaoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM RevistaEdicao");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM RevistaEdicao WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM RevistaEdicao ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT RevistaEdicao.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM RevistaEdicao ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT RevistaEdicao.* FROM RevistaEdicao ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                RevistaEdicao entidadeRetorno = new RevistaEdicao();
                PopulaRevistaEdicao(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os RevistaEdicao existentes na base de dados.
        /// </summary>
		public IEnumerable<RevistaEdicao> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de RevistaEdicao na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de RevistaEdicao na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM RevistaEdicao");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um RevistaEdicao baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">RevistaEdicao a ser populado(.</param>
		public static void PopulaRevistaEdicao(IDataReader reader, RevistaEdicao entidade) 
		{						
			if (reader["numeroEdicao"] != DBNull.Value)
				entidade.NumeroEdicao = Convert.ToInt32(reader["numeroEdicao"].ToString());
			
			if (reader["anoPublicacao"] != DBNull.Value)
				entidade.AnoPublicacao = Convert.ToInt32(reader["anoPublicacao"].ToString());
			
			if (reader["mesPublicacao"] != DBNull.Value)
				entidade.MesPublicacao = Convert.ToInt32(reader["mesPublicacao"].ToString());
			
			if (reader["periodoPublicacao"] != DBNull.Value)
				entidade.PeriodoPublicacao = reader["periodoPublicacao"].ToString();
			
			if (reader["anoEdicao"] != DBNull.Value)
				entidade.AnoEdicao = reader["anoEdicao"].ToString();
			
			if (reader["tituloEdicao"] != DBNull.Value)
				entidade.TituloEdicao = reader["tituloEdicao"].ToString();
			
			if (reader["descricaoEdicao"] != DBNull.Value)
				entidade.DescricaoEdicao = reader["descricaoEdicao"].ToString();
			
			if (reader["ativo"] != DBNull.Value)
				entidade.Ativo = Convert.ToBoolean(reader["ativo"].ToString());
			
			if (reader["numeroPaginas"] != DBNull.Value)
				entidade.NumeroPaginas = Convert.ToInt32(reader["numeroPaginas"].ToString());
			
			if (reader["revistaEdicaoId"] != DBNull.Value) {
				entidade.RevistaEdicaoId = Convert.ToInt32(reader["revistaEdicaoId"].ToString());
			}

			if (reader["revistaId"] != DBNull.Value) {
				entidade.Revista = new Revista();
				entidade.Revista.RevistaId = Convert.ToInt32(reader["revistaId"].ToString());
			}


		}		
		
	}
}
		