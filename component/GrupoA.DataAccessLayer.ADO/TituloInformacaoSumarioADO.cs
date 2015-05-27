
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
	public partial class TituloInformacaoSumarioADO : ADOSuper, ITituloInformacaoSumarioDAL {
	
	    /// <summary>
        /// Método que persiste um TituloInformacaoSumario.
        /// </summary>
        /// <param name="entidade">TituloInformacaoSumario contendo os dados a serem persistidos.</param>	
		public void Inserir(TituloInformacaoSumario entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO TituloInformacaoSumario ");
			sbSQL.Append(" (tituloInformacaoSumarioId, arquivoIdSumario, textoSumario) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@tituloInformacaoSumarioId, @arquivoIdSumario, @textoSumario) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@tituloInformacaoSumarioId", DbType.Int32, entidade.TituloInformacaoSumarioId);

			if (entidade.ArquivoSumario != null ) 
				_db.AddInParameter(command, "@arquivoIdSumario", DbType.Int32, entidade.ArquivoSumario.ArquivoId);
			else
				_db.AddInParameter(command, "@arquivoIdSumario", DbType.Int32, null);

			if (entidade.TextoSumario != null ) 
				_db.AddInParameter(command, "@textoSumario", DbType.String, entidade.TextoSumario);
			else
				_db.AddInParameter(command, "@textoSumario", DbType.String, null);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um TituloInformacaoSumario.
        /// </summary>
        /// <param name="entidade">TituloInformacaoSumario contendo os dados a serem atualizados.</param>
		public void Atualizar(TituloInformacaoSumario entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE TituloInformacaoSumario SET ");
			sbSQL.Append(" arquivoIdSumario=@arquivoIdSumario, textoSumario=@textoSumario ");
			sbSQL.Append(" WHERE tituloInformacaoSumarioId=@tituloInformacaoSumarioId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@tituloInformacaoSumarioId", DbType.Int32, entidade.TituloInformacaoSumarioId);
			if (entidade.ArquivoSumario != null ) 
				_db.AddInParameter(command, "@arquivoIdSumario", DbType.Int32, entidade.ArquivoSumario.ArquivoId);
			else
				_db.AddInParameter(command, "@arquivoIdSumario", DbType.Int32, null);
			if (entidade.TextoSumario != null ) 
				_db.AddInParameter(command, "@textoSumario", DbType.String, entidade.TextoSumario);
			else
				_db.AddInParameter(command, "@textoSumario", DbType.String, null);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um TituloInformacaoSumario da base de dados.
        /// </summary>
        /// <param name="entidade">TituloInformacaoSumario a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(TituloInformacaoSumario entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM TituloInformacaoSumario ");
			sbSQL.Append("WHERE tituloInformacaoSumarioId=@tituloInformacaoSumarioId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@tituloInformacaoSumarioId", DbType.Int32, entidade.TituloInformacaoSumarioId);

								
			_db.ExecuteNonQuery(command);
		}
		

		/// <summary>
		/// Método que carrega um TituloInformacaoSumario.
		/// </summary>
        /// <param name="entidade">TituloInformacaoSumario a ser carregado (somente o identificador é necessário).</param>
		/// <returns>TituloInformacaoSumario</returns>
		public TituloInformacaoSumario Carregar(TituloInformacaoSumario entidade) {		
		
			TituloInformacaoSumario entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM TituloInformacaoSumario WHERE tituloInformacaoSumarioId=@tituloInformacaoSumarioId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@tituloInformacaoSumarioId", DbType.Int32, entidade.TituloInformacaoSumarioId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new TituloInformacaoSumario();
				PopulaTituloInformacaoSumario(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		
		
		/// <summary>
		/// Método que carrega um TituloInformacaoSumario com suas dependências.
		/// </summary>
        /// <param name="entidade">TituloInformacaoSumario a ser carregado (somente o identificador é necessário).</param>
		/// <returns>TituloInformacaoSumario</returns>
		public TituloInformacaoSumario CarregarComDependencias(TituloInformacaoSumario entidade) {		
		
			TituloInformacaoSumario entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append("SELECT TituloInformacaoSumario.tituloInformacaoSumarioId, TituloInformacaoSumario.arquivoIdSumario, TituloInformacaoSumario.textoSumario");
			sbSQL.Append(", tituloId, subtituloLivro, numeroPaginas, edicao, dataLancamento, dataPublicacao, maisVendido, nomeTitulo, formato");
			sbSQL.Append(", conteudoId, conteudoTipoId, dataHoraCadastro");
			sbSQL.Append(" FROM TituloInformacaoSumario");
			sbSQL.Append(" INNER JOIN Titulo ON TituloInformacaoSumario.tituloInformacaoSumarioId=Titulo.tituloId");
			sbSQL.Append(" INNER JOIN Conteudo ON Titulo.tituloId=Conteudo.conteudoId");
			sbSQL.Append(" WHERE TituloInformacaoSumario.tituloInformacaoSumarioId=@tituloInformacaoSumarioId");
						
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@tituloInformacaoSumarioId", DbType.Int32, entidade.TituloInformacaoSumarioId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new TituloInformacaoSumario();
				PopulaTituloInformacaoSumario(reader, entidadeRetorno);
				entidadeRetorno.Titulo = new Titulo();
				TituloADO.PopulaTitulo(reader, entidadeRetorno.Titulo);
				entidadeRetorno.Titulo.Conteudo = new Conteudo();
				ConteudoADO.PopulaConteudo(reader, entidadeRetorno.Titulo.Conteudo);
			}
			reader.Close();
			
			return entidadeRetorno;
		}		


		/// <summary>
        /// Método que retorna uma coleção de TituloInformacaoSumario.
        /// </summary>
        /// <param name="entidade">Arquivo relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de TituloInformacaoSumario.</returns>
		public IEnumerable<TituloInformacaoSumario> Carregar(Arquivo entidade)
		{		
			List<TituloInformacaoSumario> entidadesRetorno = new List<TituloInformacaoSumario>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT TituloInformacaoSumario.* FROM TituloInformacaoSumario WHERE TituloInformacaoSumario.arquivoId=@arquivoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.ArquivoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                TituloInformacaoSumario entidadeRetorno = new TituloInformacaoSumario();
                PopulaTituloInformacaoSumario(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de TituloInformacaoSumario.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos TituloInformacaoSumario.</returns>
		public IEnumerable<TituloInformacaoSumario> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<TituloInformacaoSumario> entidadesRetorno = new List<TituloInformacaoSumario>();
			
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
				sbOrder.Append( " ORDER BY tituloInformacaoSumarioId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM TituloInformacaoSumario");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM TituloInformacaoSumario WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM TituloInformacaoSumario ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT TituloInformacaoSumario.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM TituloInformacaoSumario ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT TituloInformacaoSumario.* FROM TituloInformacaoSumario ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                TituloInformacaoSumario entidadeRetorno = new TituloInformacaoSumario();
                PopulaTituloInformacaoSumario(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os TituloInformacaoSumario existentes na base de dados.
        /// </summary>
		public IEnumerable<TituloInformacaoSumario> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de TituloInformacaoSumario na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de TituloInformacaoSumario na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM TituloInformacaoSumario");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um TituloInformacaoSumario baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">TituloInformacaoSumario a ser populado(.</param>
		public static void PopulaTituloInformacaoSumario(IDataReader reader, TituloInformacaoSumario entidade) 
		{						
			if (reader["textoSumario"] != DBNull.Value)
				entidade.TextoSumario = reader["textoSumario"].ToString();
			
			if (reader["tituloInformacaoSumarioId"] != DBNull.Value) {
				entidade.TituloInformacaoSumarioId = Convert.ToInt32(reader["tituloInformacaoSumarioId"].ToString());
			}

			if (reader["arquivoIdSumario"] != DBNull.Value) {
				entidade.ArquivoSumario = new Arquivo();
				entidade.ArquivoSumario.ArquivoId = Convert.ToInt32(reader["arquivoIdSumario"].ToString());
			}


		}		
		
	}
}
		