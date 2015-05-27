
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
	public partial class TituloInformacaoComentarioEspecialistaADO : ADOSuper, ITituloInformacaoComentarioEspecialistaDAL {
	
	    /// <summary>
        /// Método que persiste um TituloInformacaoComentarioEspecialista.
        /// </summary>
        /// <param name="entidade">TituloInformacaoComentarioEspecialista contendo os dados a serem persistidos.</param>	
		public void Inserir(TituloInformacaoComentarioEspecialista entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO TituloInformacaoComentarioEspecialista ");
			sbSQL.Append(" (tituloInformacaoComentarioEspecialistaId, textoComentario, tituloComentario, urlMidia, arquivoIdAudio, arquivoIdImagem, destaqueAreaConhecimento, nomeEspecialista, especialidade, comentarioFormatoId, resumoComentario) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@tituloInformacaoComentarioEspecialistaId, @textoComentario, @tituloComentario, @urlMidia, @arquivoIdAudio, @arquivoIdImagem, @destaqueAreaConhecimento, @nomeEspecialista, @especialidade, @comentarioFormatoId, @resumoComentario) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@tituloInformacaoComentarioEspecialistaId", DbType.Int32, entidade.TituloInformacaoComentarioEspecialistaId);

			if (entidade.TextoComentario != null ) 
				_db.AddInParameter(command, "@textoComentario", DbType.String, entidade.TextoComentario);
			else
				_db.AddInParameter(command, "@textoComentario", DbType.String, null);

			if (entidade.TituloComentario != null ) 
				_db.AddInParameter(command, "@tituloComentario", DbType.String, entidade.TituloComentario);
			else
				_db.AddInParameter(command, "@tituloComentario", DbType.String, null);

			if (entidade.UrlMidia != null ) 
				_db.AddInParameter(command, "@urlMidia", DbType.String, entidade.UrlMidia);
			else
				_db.AddInParameter(command, "@urlMidia", DbType.String, null);

			if (entidade.ArquivoAudio != null ) 
				_db.AddInParameter(command, "@arquivoIdAudio", DbType.Int32, entidade.ArquivoAudio.ArquivoId);
			else
				_db.AddInParameter(command, "@arquivoIdAudio", DbType.Int32, null);

			if (entidade.ArquivoImagem != null ) 
				_db.AddInParameter(command, "@arquivoIdImagem", DbType.Int32, entidade.ArquivoImagem.ArquivoId);
			else
				_db.AddInParameter(command, "@arquivoIdImagem", DbType.Int32, null);

			_db.AddInParameter(command, "@destaqueAreaConhecimento", DbType.Int32, entidade.DestaqueAreaConhecimento);

			if (entidade.NomeEspecialista != null ) 
				_db.AddInParameter(command, "@nomeEspecialista", DbType.String, entidade.NomeEspecialista);
			else
				_db.AddInParameter(command, "@nomeEspecialista", DbType.String, null);

			if (entidade.Especialidade != null ) 
				_db.AddInParameter(command, "@especialidade", DbType.String, entidade.Especialidade);
			else
				_db.AddInParameter(command, "@especialidade", DbType.String, null);

			if (entidade.ComentarioFormato != null ) 
				_db.AddInParameter(command, "@comentarioFormatoId", DbType.Int32, entidade.ComentarioFormato.ComentarioFormatoId);
			else
				_db.AddInParameter(command, "@comentarioFormatoId", DbType.Int32, null);

			if (entidade.ResumoComentario != null ) 
				_db.AddInParameter(command, "@resumoComentario", DbType.String, entidade.ResumoComentario);
			else
				_db.AddInParameter(command, "@resumoComentario", DbType.String, null);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um TituloInformacaoComentarioEspecialista.
        /// </summary>
        /// <param name="entidade">TituloInformacaoComentarioEspecialista contendo os dados a serem atualizados.</param>
		public void Atualizar(TituloInformacaoComentarioEspecialista entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE TituloInformacaoComentarioEspecialista SET ");
			sbSQL.Append(" textoComentario=@textoComentario, tituloComentario=@tituloComentario, urlMidia=@urlMidia, arquivoIdAudio=@arquivoIdAudio, arquivoIdImagem=@arquivoIdImagem, destaqueAreaConhecimento=@destaqueAreaConhecimento, nomeEspecialista=@nomeEspecialista, especialidade=@especialidade, comentarioFormatoId=@comentarioFormatoId, resumoComentario=@resumoComentario ");
			sbSQL.Append(" WHERE tituloInformacaoComentarioEspecialistaId=@tituloInformacaoComentarioEspecialistaId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@tituloInformacaoComentarioEspecialistaId", DbType.Int32, entidade.TituloInformacaoComentarioEspecialistaId);
			if (entidade.TextoComentario != null ) 
				_db.AddInParameter(command, "@textoComentario", DbType.String, entidade.TextoComentario);
			else
				_db.AddInParameter(command, "@textoComentario", DbType.String, null);
			if (entidade.TituloComentario != null ) 
				_db.AddInParameter(command, "@tituloComentario", DbType.String, entidade.TituloComentario);
			else
				_db.AddInParameter(command, "@tituloComentario", DbType.String, null);
			if (entidade.UrlMidia != null ) 
				_db.AddInParameter(command, "@urlMidia", DbType.String, entidade.UrlMidia);
			else
				_db.AddInParameter(command, "@urlMidia", DbType.String, null);
			if (entidade.ArquivoAudio != null ) 
				_db.AddInParameter(command, "@arquivoIdAudio", DbType.Int32, entidade.ArquivoAudio.ArquivoId);
			else
				_db.AddInParameter(command, "@arquivoIdAudio", DbType.Int32, null);
			if (entidade.ArquivoImagem != null ) 
				_db.AddInParameter(command, "@arquivoIdImagem", DbType.Int32, entidade.ArquivoImagem.ArquivoId);
			else
				_db.AddInParameter(command, "@arquivoIdImagem", DbType.Int32, null);
			_db.AddInParameter(command, "@destaqueAreaConhecimento", DbType.Int32, entidade.DestaqueAreaConhecimento);
			if (entidade.NomeEspecialista != null ) 
				_db.AddInParameter(command, "@nomeEspecialista", DbType.String, entidade.NomeEspecialista);
			else
				_db.AddInParameter(command, "@nomeEspecialista", DbType.String, null);
			if (entidade.Especialidade != null ) 
				_db.AddInParameter(command, "@especialidade", DbType.String, entidade.Especialidade);
			else
				_db.AddInParameter(command, "@especialidade", DbType.String, null);
			if (entidade.ComentarioFormato != null ) 
				_db.AddInParameter(command, "@comentarioFormatoId", DbType.Int32, entidade.ComentarioFormato.ComentarioFormatoId);
			else
				_db.AddInParameter(command, "@comentarioFormatoId", DbType.Int32, null);
			if (entidade.ResumoComentario != null ) 
				_db.AddInParameter(command, "@resumoComentario", DbType.String, entidade.ResumoComentario);
			else
				_db.AddInParameter(command, "@resumoComentario", DbType.String, null);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um TituloInformacaoComentarioEspecialista da base de dados.
        /// </summary>
        /// <param name="entidade">TituloInformacaoComentarioEspecialista a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(TituloInformacaoComentarioEspecialista entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM TituloInformacaoComentarioEspecialista ");
			sbSQL.Append("WHERE tituloInformacaoComentarioEspecialistaId=@tituloInformacaoComentarioEspecialistaId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@tituloInformacaoComentarioEspecialistaId", DbType.Int32, entidade.TituloInformacaoComentarioEspecialistaId);

								
			_db.ExecuteNonQuery(command);
		}
		

		/// <summary>
		/// Método que carrega um TituloInformacaoComentarioEspecialista.
		/// </summary>
        /// <param name="entidade">TituloInformacaoComentarioEspecialista a ser carregado (somente o identificador é necessário).</param>
		/// <returns>TituloInformacaoComentarioEspecialista</returns>
		public TituloInformacaoComentarioEspecialista Carregar(TituloInformacaoComentarioEspecialista entidade) {		
		
			TituloInformacaoComentarioEspecialista entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM TituloInformacaoComentarioEspecialista WHERE tituloInformacaoComentarioEspecialistaId=@tituloInformacaoComentarioEspecialistaId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@tituloInformacaoComentarioEspecialistaId", DbType.Int32, entidade.TituloInformacaoComentarioEspecialistaId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new TituloInformacaoComentarioEspecialista();
				PopulaTituloInformacaoComentarioEspecialista(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		
		
		/// <summary>
		/// Método que carrega um TituloInformacaoComentarioEspecialista com suas dependências.
		/// </summary>
        /// <param name="entidade">TituloInformacaoComentarioEspecialista a ser carregado (somente o identificador é necessário).</param>
		/// <returns>TituloInformacaoComentarioEspecialista</returns>
		public TituloInformacaoComentarioEspecialista CarregarComDependencias(TituloInformacaoComentarioEspecialista entidade) {		
		
			TituloInformacaoComentarioEspecialista entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append("SELECT TituloInformacaoComentarioEspecialista.tituloInformacaoComentarioEspecialistaId, TituloInformacaoComentarioEspecialista.textoComentario, TituloInformacaoComentarioEspecialista.tituloComentario, TituloInformacaoComentarioEspecialista.urlMidia, TituloInformacaoComentarioEspecialista.arquivoIdAudio, TituloInformacaoComentarioEspecialista.arquivoIdImagem, TituloInformacaoComentarioEspecialista.destaqueAreaConhecimento, TituloInformacaoComentarioEspecialista.nomeEspecialista, TituloInformacaoComentarioEspecialista.especialidade, TituloInformacaoComentarioEspecialista.comentarioFormatoId, TituloInformacaoComentarioEspecialista.resumoComentario");
			sbSQL.Append(", tituloId, subtituloLivro, numeroPaginas, edicao, dataLancamento, dataPublicacao, maisVendido, nomeTitulo, formato");
			sbSQL.Append(", conteudoId, conteudoTipoId, dataHoraCadastro");
			sbSQL.Append(" FROM TituloInformacaoComentarioEspecialista");
			sbSQL.Append(" INNER JOIN Titulo ON TituloInformacaoComentarioEspecialista.tituloInformacaoComentarioEspecialistaId=Titulo.tituloId");
			sbSQL.Append(" INNER JOIN Conteudo ON Titulo.tituloId=Conteudo.conteudoId");
			sbSQL.Append(" WHERE TituloInformacaoComentarioEspecialista.tituloInformacaoComentarioEspecialistaId=@tituloInformacaoComentarioEspecialistaId");
						
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@tituloInformacaoComentarioEspecialistaId", DbType.Int32, entidade.TituloInformacaoComentarioEspecialistaId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new TituloInformacaoComentarioEspecialista();
				PopulaTituloInformacaoComentarioEspecialista(reader, entidadeRetorno);
				entidadeRetorno.Titulo = new Titulo();
				TituloADO.PopulaTitulo(reader, entidadeRetorno.Titulo);
				entidadeRetorno.Titulo.Conteudo = new Conteudo();
				ConteudoADO.PopulaConteudo(reader, entidadeRetorno.Titulo.Conteudo);
			}
			reader.Close();
			
			return entidadeRetorno;
		}		


		/// <summary>
        /// Método que retorna uma coleção de TituloInformacaoComentarioEspecialista.
        /// </summary>
        /// <param name="entidade">TituloInformacaoComentarioEspecialistaCategoria relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de TituloInformacaoComentarioEspecialista.</returns>
		public IEnumerable<TituloInformacaoComentarioEspecialista> Carregar(TituloInformacaoComentarioEspecialistaCategoria entidade)
		{		
			List<TituloInformacaoComentarioEspecialista> entidadesRetorno = new List<TituloInformacaoComentarioEspecialista>();
			
			StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT TituloInformacaoComentarioEspecialista.* FROM TituloInformacaoComentarioEspecialista INNER JOIN TituloInformacaoComentarioEspecialistaCategoria ON TituloInformacaoComentarioEspecialista.tituloInformacaoComentarioEspecialistaId=TituloInformacaoComentarioEspecialistaCategoria.tituloInformacaoComentarioEspecialistaId WHERE TituloInformacaoComentarioEspecialistaCategoria.tituloInformacaoComentarioEspecialistaCategoriaId = @tituloInformacaoComentarioEspecialistaCategoriaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@tituloInformacaoComentarioEspecialistaCategoriaId", DbType.Int32, entidade.TituloInformacaoComentarioEspecialistaCategoriaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                TituloInformacaoComentarioEspecialista entidadeRetorno = new TituloInformacaoComentarioEspecialista();
                PopulaTituloInformacaoComentarioEspecialista(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de TituloInformacaoComentarioEspecialista.
        /// </summary>
        /// <param name="entidade">Arquivo relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de TituloInformacaoComentarioEspecialista.</returns>
		public IEnumerable<TituloInformacaoComentarioEspecialista> Carregar(Arquivo entidade)
		{		
			List<TituloInformacaoComentarioEspecialista> entidadesRetorno = new List<TituloInformacaoComentarioEspecialista>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT TituloInformacaoComentarioEspecialista.* FROM TituloInformacaoComentarioEspecialista WHERE TituloInformacaoComentarioEspecialista.arquivoId=@arquivoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.ArquivoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                TituloInformacaoComentarioEspecialista entidadeRetorno = new TituloInformacaoComentarioEspecialista();
                PopulaTituloInformacaoComentarioEspecialista(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de TituloInformacaoComentarioEspecialista.
        /// </summary>
        /// <param name="entidade">ComentarioFormato relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de TituloInformacaoComentarioEspecialista.</returns>
		public IEnumerable<TituloInformacaoComentarioEspecialista> Carregar(ComentarioFormato entidade)
		{		
			List<TituloInformacaoComentarioEspecialista> entidadesRetorno = new List<TituloInformacaoComentarioEspecialista>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT TituloInformacaoComentarioEspecialista.* FROM TituloInformacaoComentarioEspecialista WHERE TituloInformacaoComentarioEspecialista.comentarioFormatoId=@comentarioFormatoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@comentarioFormatoId", DbType.Int32, entidade.ComentarioFormatoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                TituloInformacaoComentarioEspecialista entidadeRetorno = new TituloInformacaoComentarioEspecialista();
                PopulaTituloInformacaoComentarioEspecialista(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de TituloInformacaoComentarioEspecialista.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos TituloInformacaoComentarioEspecialista.</returns>
		public IEnumerable<TituloInformacaoComentarioEspecialista> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<TituloInformacaoComentarioEspecialista> entidadesRetorno = new List<TituloInformacaoComentarioEspecialista>();
			
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
				sbOrder.Append( " ORDER BY tituloInformacaoComentarioEspecialistaId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM TituloInformacaoComentarioEspecialista");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM TituloInformacaoComentarioEspecialista WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM TituloInformacaoComentarioEspecialista ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT TituloInformacaoComentarioEspecialista.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM TituloInformacaoComentarioEspecialista ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT TituloInformacaoComentarioEspecialista.* FROM TituloInformacaoComentarioEspecialista ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                TituloInformacaoComentarioEspecialista entidadeRetorno = new TituloInformacaoComentarioEspecialista();
                PopulaTituloInformacaoComentarioEspecialista(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os TituloInformacaoComentarioEspecialista existentes na base de dados.
        /// </summary>
		public IEnumerable<TituloInformacaoComentarioEspecialista> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de TituloInformacaoComentarioEspecialista na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de TituloInformacaoComentarioEspecialista na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM TituloInformacaoComentarioEspecialista");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um TituloInformacaoComentarioEspecialista baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">TituloInformacaoComentarioEspecialista a ser populado(.</param>
		public static void PopulaTituloInformacaoComentarioEspecialista(IDataReader reader, TituloInformacaoComentarioEspecialista entidade) 
		{						
			if (reader["textoComentario"] != DBNull.Value)
				entidade.TextoComentario = reader["textoComentario"].ToString();
			
			if (reader["tituloComentario"] != DBNull.Value)
				entidade.TituloComentario = reader["tituloComentario"].ToString();
			
			if (reader["urlMidia"] != DBNull.Value)
				entidade.UrlMidia = reader["urlMidia"].ToString();
			
			if (reader["destaqueAreaConhecimento"] != DBNull.Value)
				entidade.DestaqueAreaConhecimento = Convert.ToBoolean(reader["destaqueAreaConhecimento"].ToString());
			
			if (reader["nomeEspecialista"] != DBNull.Value)
				entidade.NomeEspecialista = reader["nomeEspecialista"].ToString();
			
			if (reader["especialidade"] != DBNull.Value)
				entidade.Especialidade = reader["especialidade"].ToString();
			
			if (reader["resumoComentario"] != DBNull.Value)
				entidade.ResumoComentario = reader["resumoComentario"].ToString();
			
			if (reader["tituloInformacaoComentarioEspecialistaId"] != DBNull.Value) {
				entidade.TituloInformacaoComentarioEspecialistaId = Convert.ToInt32(reader["tituloInformacaoComentarioEspecialistaId"].ToString());
			}

			if (reader["arquivoIdAudio"] != DBNull.Value) {
				entidade.ArquivoAudio = new Arquivo();
				entidade.ArquivoAudio.ArquivoId = Convert.ToInt32(reader["arquivoIdAudio"].ToString());
			}

			if (reader["arquivoIdImagem"] != DBNull.Value) {
				entidade.ArquivoImagem = new Arquivo();
				entidade.ArquivoImagem.ArquivoId = Convert.ToInt32(reader["arquivoIdImagem"].ToString());
			}

			if (reader["comentarioFormatoId"] != DBNull.Value) {
				entidade.ComentarioFormato = new ComentarioFormato();
				entidade.ComentarioFormato.ComentarioFormatoId = Convert.ToInt32(reader["comentarioFormatoId"].ToString());
			}


		}		
		
	}
}
		