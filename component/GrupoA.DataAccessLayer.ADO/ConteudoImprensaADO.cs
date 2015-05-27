
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
	public partial class ConteudoImprensaADO : ADOSuper, IConteudoImprensaDAL {
	
	    /// <summary>
        /// Método que persiste um ConteudoImprensa.
        /// </summary>
        /// <param name="entidade">ConteudoImprensa contendo os dados a serem persistidos.</param>	
		public void Inserir(ConteudoImprensa entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO ConteudoImprensa ");
			sbSQL.Append(" (conteudoImprensaId, fonte, fonteUrl, ativo, dataExibicaoInicio, dataExibicaoFim, resumo, texto, destaque, titulo) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@conteudoImprensaId, @fonte, @fonteUrl, @ativo, @dataExibicaoInicio, @dataExibicaoFim, @resumo, @texto, @destaque, @titulo) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@conteudoImprensaId", DbType.Int32, entidade.ConteudoImprensaId);

			if (entidade.Fonte != null ) 
				_db.AddInParameter(command, "@fonte", DbType.String, entidade.Fonte);
			else
				_db.AddInParameter(command, "@fonte", DbType.String, null);

			if (entidade.FonteUrl != null ) 
				_db.AddInParameter(command, "@fonteUrl", DbType.String, entidade.FonteUrl);
			else
				_db.AddInParameter(command, "@fonteUrl", DbType.String, null);

			_db.AddInParameter(command, "@ativo", DbType.Int32, entidade.Ativo);

			if (entidade.DataExibicaoInicio != null && entidade.DataExibicaoInicio != DateTime.MinValue ) 
				_db.AddInParameter(command, "@dataExibicaoInicio", DbType.DateTime, entidade.DataExibicaoInicio);
			else
				_db.AddInParameter(command, "@dataExibicaoInicio", DbType.DateTime, null);

			if (entidade.DataExibicaoFim != null && entidade.DataExibicaoFim != DateTime.MinValue ) 
				_db.AddInParameter(command, "@dataExibicaoFim", DbType.DateTime, entidade.DataExibicaoFim);
			else
				_db.AddInParameter(command, "@dataExibicaoFim", DbType.DateTime, null);

			if (entidade.Resumo != null ) 
				_db.AddInParameter(command, "@resumo", DbType.String, entidade.Resumo);
			else
				_db.AddInParameter(command, "@resumo", DbType.String, null);

			_db.AddInParameter(command, "@texto", DbType.String, entidade.Texto);

			_db.AddInParameter(command, "@destaque", DbType.Int32, entidade.Destaque);

			if (entidade.Titulo != null ) 
				_db.AddInParameter(command, "@titulo", DbType.String, entidade.Titulo);
			else
				_db.AddInParameter(command, "@titulo", DbType.String, null);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um ConteudoImprensa.
        /// </summary>
        /// <param name="entidade">ConteudoImprensa contendo os dados a serem atualizados.</param>
		public void Atualizar(ConteudoImprensa entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE ConteudoImprensa SET ");
			sbSQL.Append(" fonte=@fonte, fonteUrl=@fonteUrl, ativo=@ativo, dataExibicaoInicio=@dataExibicaoInicio, dataExibicaoFim=@dataExibicaoFim, resumo=@resumo, texto=@texto, destaque=@destaque, titulo=@titulo ");
			sbSQL.Append(" WHERE conteudoImprensaId=@conteudoImprensaId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@conteudoImprensaId", DbType.Int32, entidade.ConteudoImprensaId);
			if (entidade.Fonte != null ) 
				_db.AddInParameter(command, "@fonte", DbType.String, entidade.Fonte);
			else
				_db.AddInParameter(command, "@fonte", DbType.String, null);
			if (entidade.FonteUrl != null ) 
				_db.AddInParameter(command, "@fonteUrl", DbType.String, entidade.FonteUrl);
			else
				_db.AddInParameter(command, "@fonteUrl", DbType.String, null);
			_db.AddInParameter(command, "@ativo", DbType.Int32, entidade.Ativo);
			if (entidade.DataExibicaoInicio != null && entidade.DataExibicaoInicio != DateTime.MinValue ) 
				_db.AddInParameter(command, "@dataExibicaoInicio", DbType.DateTime, entidade.DataExibicaoInicio);
			else
				_db.AddInParameter(command, "@dataExibicaoInicio", DbType.DateTime, null);
			if (entidade.DataExibicaoFim != null && entidade.DataExibicaoFim != DateTime.MinValue ) 
				_db.AddInParameter(command, "@dataExibicaoFim", DbType.DateTime, entidade.DataExibicaoFim);
			else
				_db.AddInParameter(command, "@dataExibicaoFim", DbType.DateTime, null);
			if (entidade.Resumo != null ) 
				_db.AddInParameter(command, "@resumo", DbType.String, entidade.Resumo);
			else
				_db.AddInParameter(command, "@resumo", DbType.String, null);
			_db.AddInParameter(command, "@texto", DbType.String, entidade.Texto);
			_db.AddInParameter(command, "@destaque", DbType.Int32, entidade.Destaque);
			if (entidade.Titulo != null ) 
				_db.AddInParameter(command, "@titulo", DbType.String, entidade.Titulo);
			else
				_db.AddInParameter(command, "@titulo", DbType.String, null);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um ConteudoImprensa da base de dados.
        /// </summary>
        /// <param name="entidade">ConteudoImprensa a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(ConteudoImprensa entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM ConteudoImprensa ");
			sbSQL.Append("WHERE conteudoImprensaId=@conteudoImprensaId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@conteudoImprensaId", DbType.Int32, entidade.ConteudoImprensaId);

								
			_db.ExecuteNonQuery(command);
		}
		

		/// <summary>
		/// Método que carrega um ConteudoImprensa.
		/// </summary>
        /// <param name="entidade">ConteudoImprensa a ser carregado (somente o identificador é necessário).</param>
		/// <returns>ConteudoImprensa</returns>
		public ConteudoImprensa Carregar(ConteudoImprensa entidade) {		
		
			ConteudoImprensa entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM ConteudoImprensa WHERE conteudoImprensaId=@conteudoImprensaId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@conteudoImprensaId", DbType.Int32, entidade.ConteudoImprensaId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new ConteudoImprensa();
				PopulaConteudoImprensa(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		
		
		/// <summary>
		/// Método que carrega um ConteudoImprensa com suas dependências.
		/// </summary>
        /// <param name="entidade">ConteudoImprensa a ser carregado (somente o identificador é necessário).</param>
		/// <returns>ConteudoImprensa</returns>
		public ConteudoImprensa CarregarComDependencias(ConteudoImprensa entidade) {		
		
			ConteudoImprensa entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append("SELECT ConteudoImprensa.conteudoImprensaId, ConteudoImprensa.fonte, ConteudoImprensa.fonteUrl, ConteudoImprensa.ativo, ConteudoImprensa.dataExibicaoInicio, ConteudoImprensa.dataExibicaoFim, ConteudoImprensa.resumo, ConteudoImprensa.texto, ConteudoImprensa.destaque, ConteudoImprensa.titulo");
			sbSQL.Append(", conteudoId, conteudoTipoId, dataHoraCadastro");
			sbSQL.Append(" FROM ConteudoImprensa");
			sbSQL.Append(" INNER JOIN Conteudo ON ConteudoImprensa.conteudoImprensaId=Conteudo.conteudoId");
			sbSQL.Append(" WHERE ConteudoImprensa.conteudoImprensaId=@conteudoImprensaId");
						
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@conteudoImprensaId", DbType.Int32, entidade.ConteudoImprensaId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new ConteudoImprensa();
				PopulaConteudoImprensa(reader, entidadeRetorno);
				entidadeRetorno.Conteudo = new Conteudo();
				ConteudoADO.PopulaConteudo(reader, entidadeRetorno.Conteudo);
			}
			reader.Close();
			
			return entidadeRetorno;
		}		

		
		
		/// <summary>
        /// Método que retorna uma coleção de ConteudoImprensa.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos ConteudoImprensa.</returns>
		public IEnumerable<ConteudoImprensa> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<ConteudoImprensa> entidadesRetorno = new List<ConteudoImprensa>();
			
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
				sbOrder.Append( " ORDER BY conteudoImprensaId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM ConteudoImprensa");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ConteudoImprensa WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ConteudoImprensa ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT ConteudoImprensa.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM ConteudoImprensa ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT ConteudoImprensa.* FROM ConteudoImprensa ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                ConteudoImprensa entidadeRetorno = new ConteudoImprensa();
                PopulaConteudoImprensa(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os ConteudoImprensa existentes na base de dados.
        /// </summary>
		public IEnumerable<ConteudoImprensa> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de ConteudoImprensa na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de ConteudoImprensa na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM ConteudoImprensa");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um ConteudoImprensa baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">ConteudoImprensa a ser populado(.</param>
		public static void PopulaConteudoImprensa(IDataReader reader, ConteudoImprensa entidade) 
		{						
			if (reader["fonte"] != DBNull.Value)
				entidade.Fonte = reader["fonte"].ToString();
			
			if (reader["fonteUrl"] != DBNull.Value)
				entidade.FonteUrl = reader["fonteUrl"].ToString();
			
			if (reader["ativo"] != DBNull.Value)
				entidade.Ativo = Convert.ToBoolean(reader["ativo"].ToString());
			
			if (reader["dataExibicaoInicio"] != DBNull.Value)
				entidade.DataExibicaoInicio = Convert.ToDateTime(reader["dataExibicaoInicio"].ToString());
			
			if (reader["dataExibicaoFim"] != DBNull.Value)
				entidade.DataExibicaoFim = Convert.ToDateTime(reader["dataExibicaoFim"].ToString());
			
			if (reader["resumo"] != DBNull.Value)
				entidade.Resumo = reader["resumo"].ToString();
			
			if (reader["texto"] != DBNull.Value)
				entidade.Texto = reader["texto"].ToString();
			
			if (reader["destaque"] != DBNull.Value)
				entidade.Destaque = Convert.ToBoolean(reader["destaque"].ToString());
			
			if (reader["titulo"] != DBNull.Value)
				entidade.Titulo = reader["titulo"].ToString();
			
			if (reader["conteudoImprensaId"] != DBNull.Value) {
				entidade.ConteudoImprensaId = Convert.ToInt32(reader["conteudoImprensaId"].ToString());
			}


		}		
		
	}
}
		