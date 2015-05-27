
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
	public partial class TituloEletronicoAluguelADO : ADOSuper, ITituloEletronicoAluguelDAL {
	
	    /// <summary>
        /// Método que persiste um TituloEletronicoAluguel.
        /// </summary>
        /// <param name="entidade">TituloEletronicoAluguel contendo os dados a serem persistidos.</param>	
		public void Inserir(TituloEletronicoAluguel entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO TituloEletronicoAluguel ");
			sbSQL.Append(" (tituloEletronicoAluguelId, tituloEletronicoId, tempoAluguel) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@tituloEletronicoAluguelId, @tituloEletronicoId, @tempoAluguel) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@tituloEletronicoAluguelId", DbType.Int32, entidade.TituloEletronicoAluguelId);

			_db.AddInParameter(command, "@tituloEletronicoId", DbType.Int32, entidade.TituloEletronico.TituloEletronicoId);

			if (entidade.TempoAluguel != null ) 
				_db.AddInParameter(command, "@tempoAluguel", DbType.Int32, entidade.TempoAluguel);
			else
				_db.AddInParameter(command, "@tempoAluguel", DbType.Int32, null);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um TituloEletronicoAluguel.
        /// </summary>
        /// <param name="entidade">TituloEletronicoAluguel contendo os dados a serem atualizados.</param>
		public void Atualizar(TituloEletronicoAluguel entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE TituloEletronicoAluguel SET ");
			sbSQL.Append(" tituloEletronicoId=@tituloEletronicoId, tempoAluguel=@tempoAluguel ");
			sbSQL.Append(" WHERE tituloEletronicoAluguelId=@tituloEletronicoAluguelId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@tituloEletronicoAluguelId", DbType.Int32, entidade.TituloEletronicoAluguelId);
			_db.AddInParameter(command, "@tituloEletronicoId", DbType.Int32, entidade.TituloEletronico.TituloEletronicoId);
			if (entidade.TempoAluguel != null ) 
				_db.AddInParameter(command, "@tempoAluguel", DbType.Int32, entidade.TempoAluguel);
			else
				_db.AddInParameter(command, "@tempoAluguel", DbType.Int32, null);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um TituloEletronicoAluguel da base de dados.
        /// </summary>
        /// <param name="entidade">TituloEletronicoAluguel a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(TituloEletronicoAluguel entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM TituloEletronicoAluguel ");
			sbSQL.Append("WHERE tituloEletronicoAluguelId=@tituloEletronicoAluguelId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@tituloEletronicoAluguelId", DbType.Int32, entidade.TituloEletronicoAluguelId);

								
			_db.ExecuteNonQuery(command);
		}
		

		/// <summary>
		/// Método que carrega um TituloEletronicoAluguel.
		/// </summary>
        /// <param name="entidade">TituloEletronicoAluguel a ser carregado (somente o identificador é necessário).</param>
		/// <returns>TituloEletronicoAluguel</returns>
		public TituloEletronicoAluguel Carregar(TituloEletronicoAluguel entidade) {		
		
			TituloEletronicoAluguel entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM TituloEletronicoAluguel WHERE tituloEletronicoAluguelId=@tituloEletronicoAluguelId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@tituloEletronicoAluguelId", DbType.Int32, entidade.TituloEletronicoAluguelId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new TituloEletronicoAluguel();
				PopulaTituloEletronicoAluguel(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		
		
		/// <summary>
		/// Método que carrega um TituloEletronicoAluguel com suas dependências.
		/// </summary>
        /// <param name="entidade">TituloEletronicoAluguel a ser carregado (somente o identificador é necessário).</param>
		/// <returns>TituloEletronicoAluguel</returns>
		public TituloEletronicoAluguel CarregarComDependencias(TituloEletronicoAluguel entidade) {		
		
			TituloEletronicoAluguel entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append("SELECT TituloEletronicoAluguel.tituloEletronicoAluguelId, TituloEletronicoAluguel.tituloEletronicoId, TituloEletronicoAluguel.tempoAluguel");
			sbSQL.Append(", produtoId, produtoTipoId, disponivel, fabricanteId, valorUnitario, valorOferta, codigoEAN13, codigoProduto, exibirSite, homologado, nomeProduto, utilizaFrete, peso");
			sbSQL.Append(", conteudoId, conteudoTipoId, dataHoraCadastro");
			sbSQL.Append(" FROM TituloEletronicoAluguel");
			sbSQL.Append(" INNER JOIN Produto ON TituloEletronicoAluguel.tituloEletronicoAluguelId=Produto.produtoId");
			sbSQL.Append(" INNER JOIN Conteudo ON Produto.produtoId=Conteudo.conteudoId");
			sbSQL.Append(" WHERE TituloEletronicoAluguel.tituloEletronicoAluguelId=@tituloEletronicoAluguelId");
						
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@tituloEletronicoAluguelId", DbType.Int32, entidade.TituloEletronicoAluguelId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new TituloEletronicoAluguel();
				PopulaTituloEletronicoAluguel(reader, entidadeRetorno);
				entidadeRetorno.Produto = new Produto();
				ProdutoADO.PopulaProduto(reader, entidadeRetorno.Produto);
				entidadeRetorno.Produto.Conteudo = new Conteudo();
				ConteudoADO.PopulaConteudo(reader, entidadeRetorno.Produto.Conteudo);
			}
			reader.Close();
			
			return entidadeRetorno;
		}		


		/// <summary>
        /// Método que retorna uma coleção de TituloEletronicoAluguel.
        /// </summary>
        /// <param name="entidade">TituloEletronico relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de TituloEletronicoAluguel.</returns>
		public IEnumerable<TituloEletronicoAluguel> Carregar(TituloEletronico entidade)
		{		
			List<TituloEletronicoAluguel> entidadesRetorno = new List<TituloEletronicoAluguel>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT TituloEletronicoAluguel.* FROM TituloEletronicoAluguel WHERE TituloEletronicoAluguel.tituloEletronicoId=@tituloEletronicoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@tituloEletronicoId", DbType.Int32, entidade.TituloEletronicoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                TituloEletronicoAluguel entidadeRetorno = new TituloEletronicoAluguel();
                PopulaTituloEletronicoAluguel(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de TituloEletronicoAluguel.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos TituloEletronicoAluguel.</returns>
		public IEnumerable<TituloEletronicoAluguel> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<TituloEletronicoAluguel> entidadesRetorno = new List<TituloEletronicoAluguel>();
			
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
				sbOrder.Append( " ORDER BY tituloEletronicoAluguelId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM TituloEletronicoAluguel");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM TituloEletronicoAluguel WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM TituloEletronicoAluguel ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT TituloEletronicoAluguel.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM TituloEletronicoAluguel ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT TituloEletronicoAluguel.* FROM TituloEletronicoAluguel ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                TituloEletronicoAluguel entidadeRetorno = new TituloEletronicoAluguel();
                PopulaTituloEletronicoAluguel(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os TituloEletronicoAluguel existentes na base de dados.
        /// </summary>
		public IEnumerable<TituloEletronicoAluguel> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de TituloEletronicoAluguel na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de TituloEletronicoAluguel na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM TituloEletronicoAluguel");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um TituloEletronicoAluguel baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">TituloEletronicoAluguel a ser populado(.</param>
		public static void PopulaTituloEletronicoAluguel(IDataReader reader, TituloEletronicoAluguel entidade) 
		{						
			if (reader["tempoAluguel"] != DBNull.Value)
				entidade.TempoAluguel = Convert.ToInt32(reader["tempoAluguel"].ToString());
			
			if (reader["tituloEletronicoAluguelId"] != DBNull.Value) {
				entidade.TituloEletronicoAluguelId = Convert.ToInt32(reader["tituloEletronicoAluguelId"].ToString());
			}

			if (reader["tituloEletronicoId"] != DBNull.Value) {
				entidade.TituloEletronico = new TituloEletronico();
				entidade.TituloEletronico.TituloEletronicoId = Convert.ToInt32(reader["tituloEletronicoId"].ToString());
			}


		}		
		
	}
}
		