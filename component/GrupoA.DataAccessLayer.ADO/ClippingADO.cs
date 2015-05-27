
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
	public partial class ClippingADO : ADOSuper, IClippingDAL {
	
	    /// <summary>
        /// Método que persiste um Clipping.
        /// </summary>
        /// <param name="entidade">Clipping contendo os dados a serem persistidos.</param>	
		public void Inserir(Clipping entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO Clipping ");
			sbSQL.Append(" (clippingId, autor, dataPublicacao) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@clippingId, @autor, @dataPublicacao) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@clippingId", DbType.Int32, entidade.ClippingId);

			if (entidade.Autor != null ) 
				_db.AddInParameter(command, "@autor", DbType.String, entidade.Autor);
			else
				_db.AddInParameter(command, "@autor", DbType.String, null);

			if (entidade.DataPublicacao != null && entidade.DataPublicacao != DateTime.MinValue ) 
				_db.AddInParameter(command, "@dataPublicacao", DbType.DateTime, entidade.DataPublicacao);
			else
				_db.AddInParameter(command, "@dataPublicacao", DbType.DateTime, null);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um Clipping.
        /// </summary>
        /// <param name="entidade">Clipping contendo os dados a serem atualizados.</param>
		public void Atualizar(Clipping entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE Clipping SET ");
			sbSQL.Append(" autor=@autor, dataPublicacao=@dataPublicacao ");
			sbSQL.Append(" WHERE clippingId=@clippingId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@clippingId", DbType.Int32, entidade.ClippingId);
			if (entidade.Autor != null ) 
				_db.AddInParameter(command, "@autor", DbType.String, entidade.Autor);
			else
				_db.AddInParameter(command, "@autor", DbType.String, null);
			if (entidade.DataPublicacao != null && entidade.DataPublicacao != DateTime.MinValue ) 
				_db.AddInParameter(command, "@dataPublicacao", DbType.DateTime, entidade.DataPublicacao);
			else
				_db.AddInParameter(command, "@dataPublicacao", DbType.DateTime, null);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um Clipping da base de dados.
        /// </summary>
        /// <param name="entidade">Clipping a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(Clipping entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM Clipping ");
			sbSQL.Append("WHERE clippingId=@clippingId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@clippingId", DbType.Int32, entidade.ClippingId);

								
			_db.ExecuteNonQuery(command);
		}
		

		/// <summary>
		/// Método que carrega um Clipping.
		/// </summary>
        /// <param name="entidade">Clipping a ser carregado (somente o identificador é necessário).</param>
		/// <returns>Clipping</returns>
		public Clipping Carregar(Clipping entidade) {		
		
			Clipping entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM Clipping WHERE clippingId=@clippingId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@clippingId", DbType.Int32, entidade.ClippingId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new Clipping();
				PopulaClipping(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		
		
		/// <summary>
		/// Método que carrega um Clipping com suas dependências.
		/// </summary>
        /// <param name="entidade">Clipping a ser carregado (somente o identificador é necessário).</param>
		/// <returns>Clipping</returns>
		public Clipping CarregarComDependencias(Clipping entidade) {		
		
			Clipping entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append("SELECT Clipping.clippingId, Clipping.autor, Clipping.dataPublicacao");
			sbSQL.Append(", conteudoImprensaId, fonte, fonteUrl, ativo, dataExibicaoInicio, dataExibicaoFim, resumo, texto, destaque, titulo");
			sbSQL.Append(", conteudoId, conteudoTipoId, dataHoraCadastro");
			sbSQL.Append(" FROM Clipping");
			sbSQL.Append(" INNER JOIN ConteudoImprensa ON Clipping.clippingId=ConteudoImprensa.conteudoImprensaId");
			sbSQL.Append(" INNER JOIN Conteudo ON ConteudoImprensa.conteudoImprensaId=Conteudo.conteudoId");
			sbSQL.Append(" WHERE Clipping.clippingId=@clippingId");
						
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@clippingId", DbType.Int32, entidade.ClippingId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new Clipping();
				PopulaClipping(reader, entidadeRetorno);
				entidadeRetorno.ConteudoImprensa = new ConteudoImprensa();
				ConteudoImprensaADO.PopulaConteudoImprensa(reader, entidadeRetorno.ConteudoImprensa);
				entidadeRetorno.ConteudoImprensa.Conteudo = new Conteudo();
				ConteudoADO.PopulaConteudo(reader, entidadeRetorno.ConteudoImprensa.Conteudo);
			}
			reader.Close();
			
			return entidadeRetorno;
		}		


		/// <summary>
        /// Método que retorna uma coleção de Clipping.
        /// </summary>
        /// <param name="entidade">ClippingImagem relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Clipping.</returns>
		public IEnumerable<Clipping> Carregar(ClippingImagem entidade)
		{		
			List<Clipping> entidadesRetorno = new List<Clipping>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Clipping.* FROM Clipping INNER JOIN ClippingImagem ON Clipping.clippingId=ClippingImagem.clippingId WHERE ClippingImagem.clippingImagemId=@clippingImagemId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@clippingImagemId", DbType.Int32, entidade.ClippingImagemId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Clipping entidadeRetorno = new Clipping();
                PopulaClipping(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de Clipping.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos Clipping.</returns>
		public IEnumerable<Clipping> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<Clipping> entidadesRetorno = new List<Clipping>();
			
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
				sbOrder.Append( " ORDER BY clippingId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM Clipping");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Clipping WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Clipping ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT Clipping.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM Clipping ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT Clipping.* FROM Clipping ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Clipping entidadeRetorno = new Clipping();
                PopulaClipping(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os Clipping existentes na base de dados.
        /// </summary>
		public IEnumerable<Clipping> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de Clipping na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de Clipping na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM Clipping");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um Clipping baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Clipping a ser populado(.</param>
		public static void PopulaClipping(IDataReader reader, Clipping entidade) 
		{						
			if (reader["autor"] != DBNull.Value)
				entidade.Autor = reader["autor"].ToString();
			
			if (reader["dataPublicacao"] != DBNull.Value)
				entidade.DataPublicacao = Convert.ToDateTime(reader["dataPublicacao"].ToString());
			
			if (reader["clippingId"] != DBNull.Value) {
				entidade.ClippingId = Convert.ToInt32(reader["clippingId"].ToString());
			}


		}		
		
	}
}
		