
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
	public partial class NoticiaADO : ADOSuper, INoticiaDAL {
	
	    /// <summary>
        /// Método que persiste um Noticia.
        /// </summary>
        /// <param name="entidade">Noticia contendo os dados a serem persistidos.</param>	
		public void Inserir(Noticia entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO Noticia ");
			sbSQL.Append(" (noticiaId, autor, dataPublicacao, categoriaNoticiaId, arquivoIdThumb) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@noticiaId, @autor, @dataPublicacao, @categoriaNoticiaId, @arquivoIdThumb) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@noticiaId", DbType.Int32, entidade.NoticiaId);

			if (entidade.Autor != null ) 
				_db.AddInParameter(command, "@autor", DbType.String, entidade.Autor);
			else
				_db.AddInParameter(command, "@autor", DbType.String, null);

			if (entidade.DataPublicacao != null && entidade.DataPublicacao != DateTime.MinValue ) 
				_db.AddInParameter(command, "@dataPublicacao", DbType.DateTime, entidade.DataPublicacao);
			else
				_db.AddInParameter(command, "@dataPublicacao", DbType.DateTime, null);

			_db.AddInParameter(command, "@categoriaNoticiaId", DbType.Int32, entidade.CategoriaNoticia.CategoriaNoticiaId);

			if (entidade.ArquivoThumb != null ) 
				_db.AddInParameter(command, "@arquivoIdThumb", DbType.Int32, entidade.ArquivoThumb.ArquivoId);
			else
				_db.AddInParameter(command, "@arquivoIdThumb", DbType.Int32, null);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um Noticia.
        /// </summary>
        /// <param name="entidade">Noticia contendo os dados a serem atualizados.</param>
		public void Atualizar(Noticia entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE Noticia SET ");
			sbSQL.Append(" autor=@autor, dataPublicacao=@dataPublicacao, categoriaNoticiaId=@categoriaNoticiaId, arquivoIdThumb=@arquivoIdThumb ");
			sbSQL.Append(" WHERE noticiaId=@noticiaId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@noticiaId", DbType.Int32, entidade.NoticiaId);
			if (entidade.Autor != null ) 
				_db.AddInParameter(command, "@autor", DbType.String, entidade.Autor);
			else
				_db.AddInParameter(command, "@autor", DbType.String, null);
			if (entidade.DataPublicacao != null && entidade.DataPublicacao != DateTime.MinValue ) 
				_db.AddInParameter(command, "@dataPublicacao", DbType.DateTime, entidade.DataPublicacao);
			else
				_db.AddInParameter(command, "@dataPublicacao", DbType.DateTime, null);
			_db.AddInParameter(command, "@categoriaNoticiaId", DbType.Int32, entidade.CategoriaNoticia.CategoriaNoticiaId);
			if (entidade.ArquivoThumb != null ) 
				_db.AddInParameter(command, "@arquivoIdThumb", DbType.Int32, entidade.ArquivoThumb.ArquivoId);
			else
				_db.AddInParameter(command, "@arquivoIdThumb", DbType.Int32, null);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um Noticia da base de dados.
        /// </summary>
        /// <param name="entidade">Noticia a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(Noticia entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM Noticia ");
			sbSQL.Append("WHERE noticiaId=@noticiaId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@noticiaId", DbType.Int32, entidade.NoticiaId);

								
			_db.ExecuteNonQuery(command);
		}
		

		/// <summary>
		/// Método que carrega um Noticia.
		/// </summary>
        /// <param name="entidade">Noticia a ser carregado (somente o identificador é necessário).</param>
		/// <returns>Noticia</returns>
		public Noticia Carregar(Noticia entidade) {		
		
			Noticia entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM Noticia WHERE noticiaId=@noticiaId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@noticiaId", DbType.Int32, entidade.NoticiaId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new Noticia();
				PopulaNoticia(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		
		
		/// <summary>
		/// Método que carrega um Noticia com suas dependências.
		/// </summary>
        /// <param name="entidade">Noticia a ser carregado (somente o identificador é necessário).</param>
		/// <returns>Noticia</returns>
		public Noticia CarregarComDependencias(Noticia entidade) {		
		
			Noticia entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append("SELECT Noticia.noticiaId, Noticia.autor, Noticia.dataPublicacao, Noticia.categoriaNoticiaId, Noticia.arquivoIdThumb");
			sbSQL.Append(", conteudoImprensaId, fonte, fonteUrl, ativo, dataExibicaoInicio, dataExibicaoFim, resumo, texto, destaque, titulo");
			sbSQL.Append(", conteudoId, conteudoTipoId, dataHoraCadastro");
			sbSQL.Append(" FROM Noticia");
			sbSQL.Append(" INNER JOIN ConteudoImprensa ON Noticia.noticiaId=ConteudoImprensa.conteudoImprensaId");
			sbSQL.Append(" INNER JOIN Conteudo ON ConteudoImprensa.conteudoImprensaId=Conteudo.conteudoId");
			sbSQL.Append(" WHERE Noticia.noticiaId=@noticiaId");
						
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@noticiaId", DbType.Int32, entidade.NoticiaId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new Noticia();
				PopulaNoticia(reader, entidadeRetorno);
				entidadeRetorno.ConteudoImprensa = new ConteudoImprensa();
				ConteudoImprensaADO.PopulaConteudoImprensa(reader, entidadeRetorno.ConteudoImprensa);
				entidadeRetorno.ConteudoImprensa.Conteudo = new Conteudo();
				ConteudoADO.PopulaConteudo(reader, entidadeRetorno.ConteudoImprensa.Conteudo);
			}
			reader.Close();
			
			return entidadeRetorno;
		}		


		/// <summary>
        /// Método que retorna uma coleção de Noticia.
        /// </summary>
        /// <param name="entidade">NoticiaImagem relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Noticia.</returns>
		public IEnumerable<Noticia> Carregar(NoticiaImagem entidade)
		{		
			List<Noticia> entidadesRetorno = new List<Noticia>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Noticia.* FROM Noticia INNER JOIN NoticiaImagem ON Noticia.noticiaId=NoticiaImagem.noticiaId WHERE NoticiaImagem.noticiaImagemId=@noticiaImagemId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@noticiaImagemId", DbType.Int32, entidade.NoticiaImagemId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Noticia entidadeRetorno = new Noticia();
                PopulaNoticia(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Noticia.
        /// </summary>
        /// <param name="entidade">Arquivo relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Noticia.</returns>
		public IEnumerable<Noticia> Carregar(Arquivo entidade)
		{		
			List<Noticia> entidadesRetorno = new List<Noticia>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Noticia.* FROM Noticia WHERE Noticia.arquivoId=@arquivoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.ArquivoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Noticia entidadeRetorno = new Noticia();
                PopulaNoticia(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Noticia.
        /// </summary>
        /// <param name="entidade">CategoriaNoticia relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Noticia.</returns>
		public IEnumerable<Noticia> Carregar(CategoriaNoticia entidade)
		{		
			List<Noticia> entidadesRetorno = new List<Noticia>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Noticia.* FROM Noticia WHERE Noticia.categoriaNoticiaId=@categoriaNoticiaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@categoriaNoticiaId", DbType.Int32, entidade.CategoriaNoticiaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Noticia entidadeRetorno = new Noticia();
                PopulaNoticia(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de Noticia.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos Noticia.</returns>
		public IEnumerable<Noticia> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<Noticia> entidadesRetorno = new List<Noticia>();
			
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
				sbOrder.Append( " ORDER BY noticiaId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM Noticia");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Noticia WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Noticia ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT Noticia.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM Noticia ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT Noticia.* FROM Noticia ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Noticia entidadeRetorno = new Noticia();
                PopulaNoticia(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os Noticia existentes na base de dados.
        /// </summary>
		public IEnumerable<Noticia> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de Noticia na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de Noticia na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM Noticia");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um Noticia baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Noticia a ser populado(.</param>
		public static void PopulaNoticia(IDataReader reader, Noticia entidade) 
		{						
			if (reader["autor"] != DBNull.Value)
				entidade.Autor = reader["autor"].ToString();
			
			if (reader["dataPublicacao"] != DBNull.Value)
				entidade.DataPublicacao = Convert.ToDateTime(reader["dataPublicacao"].ToString());
			
			if (reader["noticiaId"] != DBNull.Value) {
				entidade.NoticiaId = Convert.ToInt32(reader["noticiaId"].ToString());
			}

			if (reader["categoriaNoticiaId"] != DBNull.Value) {
				entidade.CategoriaNoticia = new CategoriaNoticia();
				entidade.CategoriaNoticia.CategoriaNoticiaId = Convert.ToInt32(reader["categoriaNoticiaId"].ToString());
			}

			if (reader["arquivoIdThumb"] != DBNull.Value) {
				entidade.ArquivoThumb = new Arquivo();
				entidade.ArquivoThumb.ArquivoId = Convert.ToInt32(reader["arquivoIdThumb"].ToString());
			}


		}		
		
	}
}
		