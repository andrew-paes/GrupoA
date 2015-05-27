
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
	public partial class TituloInformacaoSobreAutorADO : ADOSuper, ITituloInformacaoSobreAutorDAL {
	
	    /// <summary>
        /// Método que persiste um TituloInformacaoSobreAutor.
        /// </summary>
        /// <param name="entidade">TituloInformacaoSobreAutor contendo os dados a serem persistidos.</param>	
		public void Inserir(TituloInformacaoSobreAutor entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO TituloInformacaoSobreAutor ");
			sbSQL.Append(" (tituloInformacaoSobreAutorId, textoAutor, urlMidia, arquivoIdImagem) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@tituloInformacaoSobreAutorId, @textoAutor, @urlMidia, @arquivoIdImagem) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@tituloInformacaoSobreAutorId", DbType.Int32, entidade.TituloInformacaoSobreAutorId);

			if (entidade.TextoAutor != null ) 
				_db.AddInParameter(command, "@textoAutor", DbType.String, entidade.TextoAutor);
			else
				_db.AddInParameter(command, "@textoAutor", DbType.String, null);

			if (entidade.UrlMidia != null ) 
				_db.AddInParameter(command, "@urlMidia", DbType.String, entidade.UrlMidia);
			else
				_db.AddInParameter(command, "@urlMidia", DbType.String, null);

			if (entidade.ArquivoImagem != null ) 
				_db.AddInParameter(command, "@arquivoIdImagem", DbType.Int32, entidade.ArquivoImagem.ArquivoId);
			else
				_db.AddInParameter(command, "@arquivoIdImagem", DbType.Int32, null);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um TituloInformacaoSobreAutor.
        /// </summary>
        /// <param name="entidade">TituloInformacaoSobreAutor contendo os dados a serem atualizados.</param>
		public void Atualizar(TituloInformacaoSobreAutor entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE TituloInformacaoSobreAutor SET ");
			sbSQL.Append(" textoAutor=@textoAutor, urlMidia=@urlMidia, arquivoIdImagem=@arquivoIdImagem ");
			sbSQL.Append(" WHERE tituloInformacaoSobreAutorId=@tituloInformacaoSobreAutorId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@tituloInformacaoSobreAutorId", DbType.Int32, entidade.TituloInformacaoSobreAutorId);
			if (entidade.TextoAutor != null ) 
				_db.AddInParameter(command, "@textoAutor", DbType.String, entidade.TextoAutor);
			else
				_db.AddInParameter(command, "@textoAutor", DbType.String, null);
			if (entidade.UrlMidia != null ) 
				_db.AddInParameter(command, "@urlMidia", DbType.String, entidade.UrlMidia);
			else
				_db.AddInParameter(command, "@urlMidia", DbType.String, null);
			if (entidade.ArquivoImagem != null ) 
				_db.AddInParameter(command, "@arquivoIdImagem", DbType.Int32, entidade.ArquivoImagem.ArquivoId);
			else
				_db.AddInParameter(command, "@arquivoIdImagem", DbType.Int32, null);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um TituloInformacaoSobreAutor da base de dados.
        /// </summary>
        /// <param name="entidade">TituloInformacaoSobreAutor a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(TituloInformacaoSobreAutor entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM TituloInformacaoSobreAutor ");
			sbSQL.Append("WHERE tituloInformacaoSobreAutorId=@tituloInformacaoSobreAutorId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@tituloInformacaoSobreAutorId", DbType.Int32, entidade.TituloInformacaoSobreAutorId);

								
			_db.ExecuteNonQuery(command);
		}
		

		/// <summary>
		/// Método que carrega um TituloInformacaoSobreAutor.
		/// </summary>
        /// <param name="entidade">TituloInformacaoSobreAutor a ser carregado (somente o identificador é necessário).</param>
		/// <returns>TituloInformacaoSobreAutor</returns>
		public TituloInformacaoSobreAutor Carregar(TituloInformacaoSobreAutor entidade) {		
		
			TituloInformacaoSobreAutor entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM TituloInformacaoSobreAutor WHERE tituloInformacaoSobreAutorId=@tituloInformacaoSobreAutorId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@tituloInformacaoSobreAutorId", DbType.Int32, entidade.TituloInformacaoSobreAutorId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new TituloInformacaoSobreAutor();
				PopulaTituloInformacaoSobreAutor(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		
		
		/// <summary>
		/// Método que carrega um TituloInformacaoSobreAutor com suas dependências.
		/// </summary>
        /// <param name="entidade">TituloInformacaoSobreAutor a ser carregado (somente o identificador é necessário).</param>
		/// <returns>TituloInformacaoSobreAutor</returns>
		public TituloInformacaoSobreAutor CarregarComDependencias(TituloInformacaoSobreAutor entidade) {		
		
			TituloInformacaoSobreAutor entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append("SELECT TituloInformacaoSobreAutor.tituloInformacaoSobreAutorId, TituloInformacaoSobreAutor.textoAutor, TituloInformacaoSobreAutor.urlMidia, TituloInformacaoSobreAutor.arquivoIdImagem");
			sbSQL.Append(", tituloId, subtituloLivro, numeroPaginas, edicao, dataLancamento, dataPublicacao, maisVendido, nomeTitulo, formato");
			sbSQL.Append(", conteudoId, conteudoTipoId, dataHoraCadastro");
			sbSQL.Append(" FROM TituloInformacaoSobreAutor");
			sbSQL.Append(" INNER JOIN Titulo ON TituloInformacaoSobreAutor.tituloInformacaoSobreAutorId=Titulo.tituloId");
			sbSQL.Append(" INNER JOIN Conteudo ON Titulo.tituloId=Conteudo.conteudoId");
			sbSQL.Append(" WHERE TituloInformacaoSobreAutor.tituloInformacaoSobreAutorId=@tituloInformacaoSobreAutorId");
						
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@tituloInformacaoSobreAutorId", DbType.Int32, entidade.TituloInformacaoSobreAutorId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new TituloInformacaoSobreAutor();
				PopulaTituloInformacaoSobreAutor(reader, entidadeRetorno);
				entidadeRetorno.Titulo = new Titulo();
				TituloADO.PopulaTitulo(reader, entidadeRetorno.Titulo);
				entidadeRetorno.Titulo.Conteudo = new Conteudo();
				ConteudoADO.PopulaConteudo(reader, entidadeRetorno.Titulo.Conteudo);
			}
			reader.Close();
			
			return entidadeRetorno;
		}		


		/// <summary>
        /// Método que retorna uma coleção de TituloInformacaoSobreAutor.
        /// </summary>
        /// <param name="entidade">Arquivo relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de TituloInformacaoSobreAutor.</returns>
		public IEnumerable<TituloInformacaoSobreAutor> Carregar(Arquivo entidade)
		{		
			List<TituloInformacaoSobreAutor> entidadesRetorno = new List<TituloInformacaoSobreAutor>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT TituloInformacaoSobreAutor.* FROM TituloInformacaoSobreAutor WHERE TituloInformacaoSobreAutor.arquivoId=@arquivoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.ArquivoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                TituloInformacaoSobreAutor entidadeRetorno = new TituloInformacaoSobreAutor();
                PopulaTituloInformacaoSobreAutor(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de TituloInformacaoSobreAutor.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos TituloInformacaoSobreAutor.</returns>
		public IEnumerable<TituloInformacaoSobreAutor> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<TituloInformacaoSobreAutor> entidadesRetorno = new List<TituloInformacaoSobreAutor>();
			
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
				sbOrder.Append( " ORDER BY tituloInformacaoSobreAutorId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM TituloInformacaoSobreAutor");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM TituloInformacaoSobreAutor WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM TituloInformacaoSobreAutor ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT TituloInformacaoSobreAutor.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM TituloInformacaoSobreAutor ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT TituloInformacaoSobreAutor.* FROM TituloInformacaoSobreAutor ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                TituloInformacaoSobreAutor entidadeRetorno = new TituloInformacaoSobreAutor();
                PopulaTituloInformacaoSobreAutor(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os TituloInformacaoSobreAutor existentes na base de dados.
        /// </summary>
		public IEnumerable<TituloInformacaoSobreAutor> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de TituloInformacaoSobreAutor na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de TituloInformacaoSobreAutor na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM TituloInformacaoSobreAutor");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um TituloInformacaoSobreAutor baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">TituloInformacaoSobreAutor a ser populado(.</param>
		public static void PopulaTituloInformacaoSobreAutor(IDataReader reader, TituloInformacaoSobreAutor entidade) 
		{						
			if (reader["textoAutor"] != DBNull.Value)
				entidade.TextoAutor = reader["textoAutor"].ToString();
			
			if (reader["urlMidia"] != DBNull.Value)
				entidade.UrlMidia = reader["urlMidia"].ToString();
			
			if (reader["tituloInformacaoSobreAutorId"] != DBNull.Value) {
				entidade.TituloInformacaoSobreAutorId = Convert.ToInt32(reader["tituloInformacaoSobreAutorId"].ToString());
			}

			if (reader["arquivoIdImagem"] != DBNull.Value) {
				entidade.ArquivoImagem = new Arquivo();
				entidade.ArquivoImagem.ArquivoId = Convert.ToInt32(reader["arquivoIdImagem"].ToString());
			}


		}		
		
	}
}
		