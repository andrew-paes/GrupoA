
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
	public partial class ProdutoFormatoADO : ADOSuper, IProdutoFormatoDAL {
	
	    /// <summary>
        /// Método que persiste um ProdutoFormato.
        /// </summary>
        /// <param name="entidade">ProdutoFormato contendo os dados a serem persistidos.</param>	
		public void Inserir(ProdutoFormato entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO ProdutoFormato ");
			sbSQL.Append(" (produtoId, formato) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@produtoId, @formato) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.ProdutoId);

			if (entidade.Formato != null ) 
				_db.AddInParameter(command, "@formato", DbType.String, entidade.Formato);
			else
				_db.AddInParameter(command, "@formato", DbType.String, null);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um ProdutoFormato.
        /// </summary>
        /// <param name="entidade">ProdutoFormato contendo os dados a serem atualizados.</param>
		public void Atualizar(ProdutoFormato entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE ProdutoFormato SET ");
			sbSQL.Append(" formato=@formato ");
			sbSQL.Append(" WHERE produtoId=@produtoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.ProdutoId);
			if (entidade.Formato != null ) 
				_db.AddInParameter(command, "@formato", DbType.String, entidade.Formato);
			else
				_db.AddInParameter(command, "@formato", DbType.String, null);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um ProdutoFormato da base de dados.
        /// </summary>
        /// <param name="entidade">ProdutoFormato a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(ProdutoFormato entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM ProdutoFormato ");
			sbSQL.Append("WHERE produtoId=@produtoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.ProdutoId);

								
			_db.ExecuteNonQuery(command);
		}
		

		/// <summary>
		/// Método que carrega um ProdutoFormato.
		/// </summary>
        /// <param name="entidade">ProdutoFormato a ser carregado (somente o identificador é necessário).</param>
		/// <returns>ProdutoFormato</returns>
		public ProdutoFormato Carregar(ProdutoFormato entidade) {		
		
			ProdutoFormato entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM ProdutoFormato WHERE produtoId=@produtoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.ProdutoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new ProdutoFormato();
				PopulaProdutoFormato(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		
		
		/// <summary>
		/// Método que carrega um ProdutoFormato com suas dependências.
		/// </summary>
        /// <param name="entidade">ProdutoFormato a ser carregado (somente o identificador é necessário).</param>
		/// <returns>ProdutoFormato</returns>
		public ProdutoFormato CarregarComDependencias(ProdutoFormato entidade) {		
		
			ProdutoFormato entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append("SELECT ProdutoFormato.produtoId, ProdutoFormato.formato");
			sbSQL.Append(", produtoTipoId, disponivel, fabricanteId, valorUnitario, valorOferta, codigoEAN13, codigoProduto, exibirSite, nomeProduto, utilizaFrete, peso, homologado");
			sbSQL.Append(", conteudoId, conteudoTipoId, dataHoraCadastro");
			sbSQL.Append(" FROM ProdutoFormato");
			sbSQL.Append(" INNER JOIN Produto ON ProdutoFormato.produtoId=Produto.produtoId");
			sbSQL.Append(" INNER JOIN Conteudo ON Produto.produtoId=Conteudo.conteudoId");
			sbSQL.Append(" WHERE ProdutoFormato.produtoId=@produtoId");
						
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.ProdutoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new ProdutoFormato();
				PopulaProdutoFormato(reader, entidadeRetorno);
				entidadeRetorno.Produto = new Produto();
				ProdutoADO.PopulaProduto(reader, entidadeRetorno.Produto);
				entidadeRetorno.Produto.Conteudo = new Conteudo();
				ConteudoADO.PopulaConteudo(reader, entidadeRetorno.Produto.Conteudo);
			}
			reader.Close();
			
			return entidadeRetorno;
		}		

		
		
		/// <summary>
        /// Método que retorna uma coleção de ProdutoFormato.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos ProdutoFormato.</returns>
		public IEnumerable<ProdutoFormato> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<ProdutoFormato> entidadesRetorno = new List<ProdutoFormato>();
			
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
				sbOrder.Append( " ORDER BY produtoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM ProdutoFormato");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ProdutoFormato WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ProdutoFormato ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT ProdutoFormato.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM ProdutoFormato ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT ProdutoFormato.* FROM ProdutoFormato ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                ProdutoFormato entidadeRetorno = new ProdutoFormato();
                PopulaProdutoFormato(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os ProdutoFormato existentes na base de dados.
        /// </summary>
		public IEnumerable<ProdutoFormato> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de ProdutoFormato na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de ProdutoFormato na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM ProdutoFormato");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um ProdutoFormato baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">ProdutoFormato a ser populado(.</param>
		public static void PopulaProdutoFormato(IDataReader reader, ProdutoFormato entidade) 
		{						
			if (reader["formato"] != DBNull.Value)
				entidade.Formato = reader["formato"].ToString();
			
			if (reader["produtoId"] != DBNull.Value) {
				entidade.ProdutoId = Convert.ToInt32(reader["produtoId"].ToString());
			}


		}		
		
	}
}
		