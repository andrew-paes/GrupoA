
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
	public partial class RevistaAssinaturaADO : ADOSuper, IRevistaAssinaturaDAL {
	
	    /// <summary>
        /// Método que persiste um RevistaAssinatura.
        /// </summary>
        /// <param name="entidade">RevistaAssinatura contendo os dados a serem persistidos.</param>	
		public void Inserir(RevistaAssinatura entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO RevistaAssinatura ");
			sbSQL.Append(" (revistaAssinaturaId, revistaId, numeroExemplares, descricaoAssinatura) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@revistaAssinaturaId, @revistaId, @numeroExemplares, @descricaoAssinatura) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@revistaAssinaturaId", DbType.Int32, entidade.RevistaAssinaturaId);

			_db.AddInParameter(command, "@revistaId", DbType.Int32, entidade.Revista.RevistaId);

			if (entidade.NumeroExemplares != null ) 
				_db.AddInParameter(command, "@numeroExemplares", DbType.Int32, entidade.NumeroExemplares);
			else
				_db.AddInParameter(command, "@numeroExemplares", DbType.Int32, null);

			if (entidade.DescricaoAssinatura != null ) 
				_db.AddInParameter(command, "@descricaoAssinatura", DbType.String, entidade.DescricaoAssinatura);
			else
				_db.AddInParameter(command, "@descricaoAssinatura", DbType.String, null);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um RevistaAssinatura.
        /// </summary>
        /// <param name="entidade">RevistaAssinatura contendo os dados a serem atualizados.</param>
		public void Atualizar(RevistaAssinatura entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE RevistaAssinatura SET ");
			sbSQL.Append(" revistaId=@revistaId, numeroExemplares=@numeroExemplares, descricaoAssinatura=@descricaoAssinatura ");
			sbSQL.Append(" WHERE revistaAssinaturaId=@revistaAssinaturaId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@revistaAssinaturaId", DbType.Int32, entidade.RevistaAssinaturaId);
			_db.AddInParameter(command, "@revistaId", DbType.Int32, entidade.Revista.RevistaId);
			if (entidade.NumeroExemplares != null ) 
				_db.AddInParameter(command, "@numeroExemplares", DbType.Int32, entidade.NumeroExemplares);
			else
				_db.AddInParameter(command, "@numeroExemplares", DbType.Int32, null);
			if (entidade.DescricaoAssinatura != null ) 
				_db.AddInParameter(command, "@descricaoAssinatura", DbType.String, entidade.DescricaoAssinatura);
			else
				_db.AddInParameter(command, "@descricaoAssinatura", DbType.String, null);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um RevistaAssinatura da base de dados.
        /// </summary>
        /// <param name="entidade">RevistaAssinatura a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(RevistaAssinatura entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM RevistaAssinatura ");
			sbSQL.Append("WHERE revistaAssinaturaId=@revistaAssinaturaId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@revistaAssinaturaId", DbType.Int32, entidade.RevistaAssinaturaId);

								
			_db.ExecuteNonQuery(command);
		}
		

		/// <summary>
		/// Método que carrega um RevistaAssinatura.
		/// </summary>
        /// <param name="entidade">RevistaAssinatura a ser carregado (somente o identificador é necessário).</param>
		/// <returns>RevistaAssinatura</returns>
		public RevistaAssinatura Carregar(RevistaAssinatura entidade) {		
		
			RevistaAssinatura entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM RevistaAssinatura WHERE revistaAssinaturaId=@revistaAssinaturaId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@revistaAssinaturaId", DbType.Int32, entidade.RevistaAssinaturaId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new RevistaAssinatura();
				PopulaRevistaAssinatura(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		
		
		/// <summary>
		/// Método que carrega um RevistaAssinatura com suas dependências.
		/// </summary>
        /// <param name="entidade">RevistaAssinatura a ser carregado (somente o identificador é necessário).</param>
		/// <returns>RevistaAssinatura</returns>
		public RevistaAssinatura CarregarComDependencias(RevistaAssinatura entidade) {		
		
			RevistaAssinatura entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append("SELECT RevistaAssinatura.revistaAssinaturaId, RevistaAssinatura.revistaId, RevistaAssinatura.numeroExemplares, RevistaAssinatura.descricaoAssinatura");
			sbSQL.Append(", produtoId, produtoTipoId, disponivel, fabricanteId, valorUnitario, valorOferta, codigoEAN13, codigoProduto, exibirSite, nomeProduto, utilizaFrete, peso, homologado");
			sbSQL.Append(", conteudoId, conteudoTipoId, dataHoraCadastro");
			sbSQL.Append(" FROM RevistaAssinatura");
			sbSQL.Append(" INNER JOIN Produto ON RevistaAssinatura.revistaAssinaturaId=Produto.produtoId");
			sbSQL.Append(" INNER JOIN Conteudo ON Produto.produtoId=Conteudo.conteudoId");
			sbSQL.Append(" WHERE RevistaAssinatura.revistaAssinaturaId=@revistaAssinaturaId");
						
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@revistaAssinaturaId", DbType.Int32, entidade.RevistaAssinaturaId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new RevistaAssinatura();
				PopulaRevistaAssinatura(reader, entidadeRetorno);
				entidadeRetorno.Produto = new Produto();
				ProdutoADO.PopulaProduto(reader, entidadeRetorno.Produto);
				entidadeRetorno.Produto.Conteudo = new Conteudo();
				ConteudoADO.PopulaConteudo(reader, entidadeRetorno.Produto.Conteudo);
			}
			reader.Close();
			
			return entidadeRetorno;
		}		


		/// <summary>
        /// Método que retorna uma coleção de RevistaAssinatura.
        /// </summary>
        /// <param name="entidade">Revista relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de RevistaAssinatura.</returns>
		public IEnumerable<RevistaAssinatura> Carregar(Revista entidade)
		{		
			List<RevistaAssinatura> entidadesRetorno = new List<RevistaAssinatura>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT RevistaAssinatura.* FROM RevistaAssinatura WHERE RevistaAssinatura.revistaId=@revistaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@revistaId", DbType.Int32, entidade.RevistaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                RevistaAssinatura entidadeRetorno = new RevistaAssinatura();
                PopulaRevistaAssinatura(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de RevistaAssinatura.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos RevistaAssinatura.</returns>
		public IEnumerable<RevistaAssinatura> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<RevistaAssinatura> entidadesRetorno = new List<RevistaAssinatura>();
			
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
				sbOrder.Append( " ORDER BY revistaAssinaturaId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM RevistaAssinatura");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM RevistaAssinatura WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM RevistaAssinatura ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT RevistaAssinatura.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM RevistaAssinatura ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT RevistaAssinatura.* FROM RevistaAssinatura ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                RevistaAssinatura entidadeRetorno = new RevistaAssinatura();
                PopulaRevistaAssinatura(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os RevistaAssinatura existentes na base de dados.
        /// </summary>
		public IEnumerable<RevistaAssinatura> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de RevistaAssinatura na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de RevistaAssinatura na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM RevistaAssinatura");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um RevistaAssinatura baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">RevistaAssinatura a ser populado(.</param>
		public static void PopulaRevistaAssinatura(IDataReader reader, RevistaAssinatura entidade) 
		{						
			if (reader["numeroExemplares"] != DBNull.Value)
				entidade.NumeroExemplares = Convert.ToInt32(reader["numeroExemplares"].ToString());
			
			if (reader["descricaoAssinatura"] != DBNull.Value)
				entidade.DescricaoAssinatura = reader["descricaoAssinatura"].ToString();
			
			if (reader["revistaAssinaturaId"] != DBNull.Value) {
				entidade.RevistaAssinaturaId = Convert.ToInt32(reader["revistaAssinaturaId"].ToString());
			}

			if (reader["revistaId"] != DBNull.Value) {
				entidade.Revista = new Revista();
				entidade.Revista.RevistaId = Convert.ToInt32(reader["revistaId"].ToString());
			}


		}		
		
	}
}
		