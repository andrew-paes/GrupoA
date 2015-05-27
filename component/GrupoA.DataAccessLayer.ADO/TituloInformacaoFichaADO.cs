
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
	public partial class TituloInformacaoFichaADO : ADOSuper, ITituloInformacaoFichaDAL {
	
	    /// <summary>
        /// Método que persiste um TituloInformacaoFicha.
        /// </summary>
        /// <param name="entidade">TituloInformacaoFicha contendo os dados a serem persistidos.</param>	
		public void Inserir(TituloInformacaoFicha entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO TituloInformacaoFicha ");
			sbSQL.Append(" (tituloInformacaoFichaId, textoFichaTecnica) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@tituloInformacaoFichaId, @textoFichaTecnica) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@tituloInformacaoFichaId", DbType.Int32, entidade.TituloInformacaoFichaId);

			_db.AddInParameter(command, "@textoFichaTecnica", DbType.String, entidade.TextoFichaTecnica);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um TituloInformacaoFicha.
        /// </summary>
        /// <param name="entidade">TituloInformacaoFicha contendo os dados a serem atualizados.</param>
		public void Atualizar(TituloInformacaoFicha entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE TituloInformacaoFicha SET ");
			sbSQL.Append(" textoFichaTecnica=@textoFichaTecnica ");
			sbSQL.Append(" WHERE tituloInformacaoFichaId=@tituloInformacaoFichaId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@tituloInformacaoFichaId", DbType.Int32, entidade.TituloInformacaoFichaId);
			_db.AddInParameter(command, "@textoFichaTecnica", DbType.String, entidade.TextoFichaTecnica);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um TituloInformacaoFicha da base de dados.
        /// </summary>
        /// <param name="entidade">TituloInformacaoFicha a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(TituloInformacaoFicha entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM TituloInformacaoFicha ");
			sbSQL.Append("WHERE tituloInformacaoFichaId=@tituloInformacaoFichaId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@tituloInformacaoFichaId", DbType.Int32, entidade.TituloInformacaoFichaId);

								
			_db.ExecuteNonQuery(command);
		}
		

		/// <summary>
		/// Método que carrega um TituloInformacaoFicha.
		/// </summary>
        /// <param name="entidade">TituloInformacaoFicha a ser carregado (somente o identificador é necessário).</param>
		/// <returns>TituloInformacaoFicha</returns>
		public TituloInformacaoFicha Carregar(TituloInformacaoFicha entidade) {		
		
			TituloInformacaoFicha entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM TituloInformacaoFicha WHERE tituloInformacaoFichaId=@tituloInformacaoFichaId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@tituloInformacaoFichaId", DbType.Int32, entidade.TituloInformacaoFichaId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new TituloInformacaoFicha();
				PopulaTituloInformacaoFicha(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		
		
		/// <summary>
		/// Método que carrega um TituloInformacaoFicha com suas dependências.
		/// </summary>
        /// <param name="entidade">TituloInformacaoFicha a ser carregado (somente o identificador é necessário).</param>
		/// <returns>TituloInformacaoFicha</returns>
		public TituloInformacaoFicha CarregarComDependencias(TituloInformacaoFicha entidade) {		
		
			TituloInformacaoFicha entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append("SELECT TituloInformacaoFicha.tituloInformacaoFichaId, TituloInformacaoFicha.textoFichaTecnica");
			sbSQL.Append(", tituloId, subtituloLivro, numeroPaginas, edicao, dataLancamento, dataPublicacao, maisVendido, nomeTitulo, formato");
			sbSQL.Append(", conteudoId, conteudoTipoId, dataHoraCadastro");
			sbSQL.Append(" FROM TituloInformacaoFicha");
			sbSQL.Append(" INNER JOIN Titulo ON TituloInformacaoFicha.tituloInformacaoFichaId=Titulo.tituloId");
			sbSQL.Append(" INNER JOIN Conteudo ON Titulo.tituloId=Conteudo.conteudoId");
			sbSQL.Append(" WHERE TituloInformacaoFicha.tituloInformacaoFichaId=@tituloInformacaoFichaId");
						
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@tituloInformacaoFichaId", DbType.Int32, entidade.TituloInformacaoFichaId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new TituloInformacaoFicha();
				PopulaTituloInformacaoFicha(reader, entidadeRetorno);
				entidadeRetorno.Titulo = new Titulo();
				TituloADO.PopulaTitulo(reader, entidadeRetorno.Titulo);
				entidadeRetorno.Titulo.Conteudo = new Conteudo();
				ConteudoADO.PopulaConteudo(reader, entidadeRetorno.Titulo.Conteudo);
			}
			reader.Close();
			
			return entidadeRetorno;
		}		

		
		
		/// <summary>
        /// Método que retorna uma coleção de TituloInformacaoFicha.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos TituloInformacaoFicha.</returns>
		public IEnumerable<TituloInformacaoFicha> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<TituloInformacaoFicha> entidadesRetorno = new List<TituloInformacaoFicha>();
			
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
				sbOrder.Append( " ORDER BY tituloInformacaoFichaId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM TituloInformacaoFicha");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM TituloInformacaoFicha WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM TituloInformacaoFicha ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT TituloInformacaoFicha.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM TituloInformacaoFicha ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT TituloInformacaoFicha.* FROM TituloInformacaoFicha ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                TituloInformacaoFicha entidadeRetorno = new TituloInformacaoFicha();
                PopulaTituloInformacaoFicha(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os TituloInformacaoFicha existentes na base de dados.
        /// </summary>
		public IEnumerable<TituloInformacaoFicha> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de TituloInformacaoFicha na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de TituloInformacaoFicha na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM TituloInformacaoFicha");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um TituloInformacaoFicha baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">TituloInformacaoFicha a ser populado(.</param>
		public static void PopulaTituloInformacaoFicha(IDataReader reader, TituloInformacaoFicha entidade) 
		{						
			if (reader["textoFichaTecnica"] != DBNull.Value)
				entidade.TextoFichaTecnica = reader["textoFichaTecnica"].ToString();
			
			if (reader["tituloInformacaoFichaId"] != DBNull.Value) {
				entidade.TituloInformacaoFichaId = Convert.ToInt32(reader["tituloInformacaoFichaId"].ToString());
			}


		}		
		
	}
}
		