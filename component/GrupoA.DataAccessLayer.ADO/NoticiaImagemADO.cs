
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
	public partial class NoticiaImagemADO : ADOSuper, INoticiaImagemDAL {
	
	    /// <summary>
        /// Método que persiste um NoticiaImagem.
        /// </summary>
        /// <param name="entidade">NoticiaImagem contendo os dados a serem persistidos.</param>	
		public void Inserir(NoticiaImagem entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO NoticiaImagem ");
			sbSQL.Append(" (arquivoId, noticiaId, ordemApresentacao) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@arquivoId, @noticiaId, @ordemApresentacao) ");											

			sbSQL.Append(" ; SET @noticiaImagemId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@noticiaImagemId", DbType.Int32, 8);

			_db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.Arquivo.ArquivoId);

			_db.AddInParameter(command, "@noticiaId", DbType.Int32, entidade.Noticia.NoticiaId);

			_db.AddInParameter(command, "@ordemApresentacao", DbType.Int32, entidade.OrdemApresentacao);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.NoticiaImagemId = Convert.ToInt32(_db.GetParameterValue(command, "@noticiaImagemId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um NoticiaImagem.
        /// </summary>
        /// <param name="entidade">NoticiaImagem contendo os dados a serem atualizados.</param>
		public void Atualizar(NoticiaImagem entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE NoticiaImagem SET ");
			sbSQL.Append(" arquivoId=@arquivoId, noticiaId=@noticiaId, ordemApresentacao=@ordemApresentacao ");
			sbSQL.Append(" WHERE noticiaImagemId=@noticiaImagemId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@noticiaImagemId", DbType.Int32, entidade.NoticiaImagemId);
			_db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.Arquivo.ArquivoId);
			_db.AddInParameter(command, "@noticiaId", DbType.Int32, entidade.Noticia.NoticiaId);
			_db.AddInParameter(command, "@ordemApresentacao", DbType.Int32, entidade.OrdemApresentacao);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um NoticiaImagem da base de dados.
        /// </summary>
        /// <param name="entidade">NoticiaImagem a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(NoticiaImagem entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM NoticiaImagem ");
			sbSQL.Append("WHERE noticiaImagemId=@noticiaImagemId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@noticiaImagemId", DbType.Int32, entidade.NoticiaImagemId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um NoticiaImagem.
		/// </summary>
        /// <param name="entidade">NoticiaImagem a ser carregado (somente o identificador é necessário).</param>
		/// <returns>NoticiaImagem</returns>
		public NoticiaImagem Carregar(int noticiaImagemId) {		
			NoticiaImagem entidade = new NoticiaImagem();
			entidade.NoticiaImagemId = noticiaImagemId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um NoticiaImagem.
		/// </summary>
        /// <param name="entidade">NoticiaImagem a ser carregado (somente o identificador é necessário).</param>
		/// <returns>NoticiaImagem</returns>
		public NoticiaImagem Carregar(NoticiaImagem entidade) {		
		
			NoticiaImagem entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM NoticiaImagem WHERE noticiaImagemId=@noticiaImagemId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@noticiaImagemId", DbType.Int32, entidade.NoticiaImagemId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new NoticiaImagem();
				PopulaNoticiaImagem(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de NoticiaImagem.
        /// </summary>
        /// <param name="entidade">Arquivo relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de NoticiaImagem.</returns>
		public IEnumerable<NoticiaImagem> Carregar(Arquivo entidade)
		{		
			List<NoticiaImagem> entidadesRetorno = new List<NoticiaImagem>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT NoticiaImagem.* FROM NoticiaImagem WHERE NoticiaImagem.arquivoId=@arquivoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.ArquivoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                NoticiaImagem entidadeRetorno = new NoticiaImagem();
                PopulaNoticiaImagem(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de NoticiaImagem.
        /// </summary>
        /// <param name="entidade">Noticia relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de NoticiaImagem.</returns>
		public IEnumerable<NoticiaImagem> Carregar(Noticia entidade)
		{		
			List<NoticiaImagem> entidadesRetorno = new List<NoticiaImagem>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT NoticiaImagem.* FROM NoticiaImagem WHERE NoticiaImagem.noticiaId=@noticiaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@noticiaId", DbType.Int32, entidade.NoticiaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                NoticiaImagem entidadeRetorno = new NoticiaImagem();
                PopulaNoticiaImagem(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de NoticiaImagem.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos NoticiaImagem.</returns>
		public IEnumerable<NoticiaImagem> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<NoticiaImagem> entidadesRetorno = new List<NoticiaImagem>();
			
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
				sbOrder.Append( " ORDER BY noticiaImagemId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM NoticiaImagem");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM NoticiaImagem WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM NoticiaImagem ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT NoticiaImagem.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM NoticiaImagem ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT NoticiaImagem.* FROM NoticiaImagem ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                NoticiaImagem entidadeRetorno = new NoticiaImagem();
                PopulaNoticiaImagem(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os NoticiaImagem existentes na base de dados.
        /// </summary>
		public IEnumerable<NoticiaImagem> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de NoticiaImagem na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de NoticiaImagem na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM NoticiaImagem");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um NoticiaImagem baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">NoticiaImagem a ser populado(.</param>
		public static void PopulaNoticiaImagem(IDataReader reader, NoticiaImagem entidade) 
		{						
			if (reader["noticiaImagemId"] != DBNull.Value)
				entidade.NoticiaImagemId = Convert.ToInt32(reader["noticiaImagemId"].ToString());
			
			if (reader["ordemApresentacao"] != DBNull.Value)
				entidade.OrdemApresentacao = Convert.ToInt32(reader["ordemApresentacao"].ToString());
			
			if (reader["arquivoId"] != DBNull.Value) {
				entidade.Arquivo = new Arquivo();
				entidade.Arquivo.ArquivoId = Convert.ToInt32(reader["arquivoId"].ToString());
			}

			if (reader["noticiaId"] != DBNull.Value) {
				entidade.Noticia = new Noticia();
				entidade.Noticia.NoticiaId = Convert.ToInt32(reader["noticiaId"].ToString());
			}


		}		
		
	}
}
		