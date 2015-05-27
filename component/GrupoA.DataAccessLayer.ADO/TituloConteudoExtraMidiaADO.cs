
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
	public partial class TituloConteudoExtraMidiaADO : ADOSuper, ITituloConteudoExtraMidiaDAL {
	
	    /// <summary>
        /// Método que persiste um TituloConteudoExtraMidia.
        /// </summary>
        /// <param name="entidade">TituloConteudoExtraMidia contendo os dados a serem persistidos.</param>	
		public void Inserir(TituloConteudoExtraMidia entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO TituloConteudoExtraMidia ");
			sbSQL.Append(" (tituloConteudoExtraMidiaId, informacao) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@tituloConteudoExtraMidiaId, @informacao) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@tituloConteudoExtraMidiaId", DbType.Int32, entidade.TituloConteudoExtraMidiaId);

			_db.AddInParameter(command, "@informacao", DbType.String, entidade.Informacao);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um TituloConteudoExtraMidia.
        /// </summary>
        /// <param name="entidade">TituloConteudoExtraMidia contendo os dados a serem atualizados.</param>
		public void Atualizar(TituloConteudoExtraMidia entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE TituloConteudoExtraMidia SET ");
			sbSQL.Append(" informacao=@informacao ");
			sbSQL.Append(" WHERE tituloConteudoExtraMidiaId=@tituloConteudoExtraMidiaId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@tituloConteudoExtraMidiaId", DbType.Int32, entidade.TituloConteudoExtraMidiaId);
			_db.AddInParameter(command, "@informacao", DbType.String, entidade.Informacao);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um TituloConteudoExtraMidia da base de dados.
        /// </summary>
        /// <param name="entidade">TituloConteudoExtraMidia a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(TituloConteudoExtraMidia entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM TituloConteudoExtraMidia ");
			sbSQL.Append("WHERE tituloConteudoExtraMidiaId=@tituloConteudoExtraMidiaId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@tituloConteudoExtraMidiaId", DbType.Int32, entidade.TituloConteudoExtraMidiaId);

								
			_db.ExecuteNonQuery(command);
		}
		

		/// <summary>
		/// Método que carrega um TituloConteudoExtraMidia.
		/// </summary>
        /// <param name="entidade">TituloConteudoExtraMidia a ser carregado (somente o identificador é necessário).</param>
		/// <returns>TituloConteudoExtraMidia</returns>
		public TituloConteudoExtraMidia Carregar(TituloConteudoExtraMidia entidade) {		
		
			TituloConteudoExtraMidia entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM TituloConteudoExtraMidia WHERE tituloConteudoExtraMidiaId=@tituloConteudoExtraMidiaId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@tituloConteudoExtraMidiaId", DbType.Int32, entidade.TituloConteudoExtraMidiaId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new TituloConteudoExtraMidia();
				PopulaTituloConteudoExtraMidia(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		
		
		/// <summary>
		/// Método que carrega um TituloConteudoExtraMidia com suas dependências.
		/// </summary>
        /// <param name="entidade">TituloConteudoExtraMidia a ser carregado (somente o identificador é necessário).</param>
		/// <returns>TituloConteudoExtraMidia</returns>
		public TituloConteudoExtraMidia CarregarComDependencias(TituloConteudoExtraMidia entidade) {		
		
			TituloConteudoExtraMidia entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append("SELECT TituloConteudoExtraMidia.tituloConteudoExtraMidiaId, TituloConteudoExtraMidia.informacao");
			sbSQL.Append(", tituloId, subtituloLivro, numeroPaginas, edicao, dataLancamento, dataPublicacao, maisVendido, nomeTitulo, formato");
			sbSQL.Append(", conteudoId, conteudoTipoId, dataHoraCadastro");
			sbSQL.Append(" FROM TituloConteudoExtraMidia");
			sbSQL.Append(" INNER JOIN Titulo ON TituloConteudoExtraMidia.tituloConteudoExtraMidiaId=Titulo.tituloId");
			sbSQL.Append(" INNER JOIN Conteudo ON Titulo.tituloId=Conteudo.conteudoId");
			sbSQL.Append(" WHERE TituloConteudoExtraMidia.tituloConteudoExtraMidiaId=@tituloConteudoExtraMidiaId");
						
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@tituloConteudoExtraMidiaId", DbType.Int32, entidade.TituloConteudoExtraMidiaId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new TituloConteudoExtraMidia();
				PopulaTituloConteudoExtraMidia(reader, entidadeRetorno);
				entidadeRetorno.Titulo = new Titulo();
				TituloADO.PopulaTitulo(reader, entidadeRetorno.Titulo);
				entidadeRetorno.Titulo.Conteudo = new Conteudo();
				ConteudoADO.PopulaConteudo(reader, entidadeRetorno.Titulo.Conteudo);
			}
			reader.Close();
			
			return entidadeRetorno;
		}		

		
		
		/// <summary>
        /// Método que retorna uma coleção de TituloConteudoExtraMidia.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos TituloConteudoExtraMidia.</returns>
		public IEnumerable<TituloConteudoExtraMidia> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<TituloConteudoExtraMidia> entidadesRetorno = new List<TituloConteudoExtraMidia>();
			
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
				sbOrder.Append( " ORDER BY tituloConteudoExtraMidiaId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM TituloConteudoExtraMidia");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM TituloConteudoExtraMidia WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM TituloConteudoExtraMidia ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT TituloConteudoExtraMidia.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM TituloConteudoExtraMidia ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT TituloConteudoExtraMidia.* FROM TituloConteudoExtraMidia ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                TituloConteudoExtraMidia entidadeRetorno = new TituloConteudoExtraMidia();
                PopulaTituloConteudoExtraMidia(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os TituloConteudoExtraMidia existentes na base de dados.
        /// </summary>
		public IEnumerable<TituloConteudoExtraMidia> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de TituloConteudoExtraMidia na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de TituloConteudoExtraMidia na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM TituloConteudoExtraMidia");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um TituloConteudoExtraMidia baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">TituloConteudoExtraMidia a ser populado(.</param>
		public static void PopulaTituloConteudoExtraMidia(IDataReader reader, TituloConteudoExtraMidia entidade) 
		{						
			if (reader["informacao"] != DBNull.Value)
				entidade.Informacao = reader["informacao"].ToString();
			
			if (reader["tituloConteudoExtraMidiaId"] != DBNull.Value) {
				entidade.TituloConteudoExtraMidiaId = Convert.ToInt32(reader["tituloConteudoExtraMidiaId"].ToString());
			}


		}		
		
	}
}
		