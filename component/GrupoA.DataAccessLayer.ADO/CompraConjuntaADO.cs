
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
	public partial class CompraConjuntaADO : ADOSuper, ICompraConjuntaDAL {
	
	    /// <summary>
        /// Método que persiste um CompraConjunta.
        /// </summary>
        /// <param name="entidade">CompraConjunta contendo os dados a serem persistidos.</param>	
		public void Inserir(CompraConjunta entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO CompraConjunta ");
			sbSQL.Append(" (produtoId, dataInicialCompra, dataFinalCompra, estoqueSeguranca, dataHoraFinalizacao, ativa, compraConjuntaStatusId, quantidadeLimite) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@produtoId, @dataInicialCompra, @dataFinalCompra, @estoqueSeguranca, @dataHoraFinalizacao, @ativa, @compraConjuntaStatusId, @quantidadeLimite) ");											

			sbSQL.Append(" ; SET @compraConjuntaId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@compraConjuntaId", DbType.Int32, 8);

			_db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.Produto.ProdutoId);

			_db.AddInParameter(command, "@dataInicialCompra", DbType.DateTime, entidade.DataInicialCompra);

			_db.AddInParameter(command, "@dataFinalCompra", DbType.DateTime, entidade.DataFinalCompra);

			_db.AddInParameter(command, "@estoqueSeguranca", DbType.Int32, entidade.EstoqueSeguranca);

			if (entidade.DataHoraFinalizacao != null && entidade.DataHoraFinalizacao != DateTime.MinValue ) 
				_db.AddInParameter(command, "@dataHoraFinalizacao", DbType.DateTime, entidade.DataHoraFinalizacao);
			else
				_db.AddInParameter(command, "@dataHoraFinalizacao", DbType.DateTime, null);

			_db.AddInParameter(command, "@ativa", DbType.Int32, entidade.Ativa);

			_db.AddInParameter(command, "@compraConjuntaStatusId", DbType.Int32, entidade.CompraConjuntaStatus.CompraConjuntaStatusId);

			if (entidade.QuantidadeLimite != null ) 
				_db.AddInParameter(command, "@quantidadeLimite", DbType.Int32, entidade.QuantidadeLimite);
			else
				_db.AddInParameter(command, "@quantidadeLimite", DbType.Int32, null);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.CompraConjuntaId = Convert.ToInt32(_db.GetParameterValue(command, "@compraConjuntaId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um CompraConjunta.
        /// </summary>
        /// <param name="entidade">CompraConjunta contendo os dados a serem atualizados.</param>
		public void Atualizar(CompraConjunta entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE CompraConjunta SET ");
			sbSQL.Append(" produtoId=@produtoId, dataInicialCompra=@dataInicialCompra, dataFinalCompra=@dataFinalCompra, estoqueSeguranca=@estoqueSeguranca, dataHoraFinalizacao=@dataHoraFinalizacao, ativa=@ativa, compraConjuntaStatusId=@compraConjuntaStatusId, quantidadeLimite=@quantidadeLimite ");
			sbSQL.Append(" WHERE compraConjuntaId=@compraConjuntaId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@compraConjuntaId", DbType.Int32, entidade.CompraConjuntaId);
			_db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.Produto.ProdutoId);
			_db.AddInParameter(command, "@dataInicialCompra", DbType.DateTime, entidade.DataInicialCompra);
			_db.AddInParameter(command, "@dataFinalCompra", DbType.DateTime, entidade.DataFinalCompra);
			_db.AddInParameter(command, "@estoqueSeguranca", DbType.Int32, entidade.EstoqueSeguranca);
			if (entidade.DataHoraFinalizacao != null && entidade.DataHoraFinalizacao != DateTime.MinValue ) 
				_db.AddInParameter(command, "@dataHoraFinalizacao", DbType.DateTime, entidade.DataHoraFinalizacao);
			else
				_db.AddInParameter(command, "@dataHoraFinalizacao", DbType.DateTime, null);
			_db.AddInParameter(command, "@ativa", DbType.Int32, entidade.Ativa);
			_db.AddInParameter(command, "@compraConjuntaStatusId", DbType.Int32, entidade.CompraConjuntaStatus.CompraConjuntaStatusId);
			if (entidade.QuantidadeLimite != null ) 
				_db.AddInParameter(command, "@quantidadeLimite", DbType.Int32, entidade.QuantidadeLimite);
			else
				_db.AddInParameter(command, "@quantidadeLimite", DbType.Int32, null);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um CompraConjunta da base de dados.
        /// </summary>
        /// <param name="entidade">CompraConjunta a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(CompraConjunta entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM CompraConjunta ");
			sbSQL.Append("WHERE compraConjuntaId=@compraConjuntaId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@compraConjuntaId", DbType.Int32, entidade.CompraConjuntaId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um CompraConjunta.
		/// </summary>
        /// <param name="entidade">CompraConjunta a ser carregado (somente o identificador é necessário).</param>
		/// <returns>CompraConjunta</returns>
		public CompraConjunta Carregar(int compraConjuntaId) {		
			CompraConjunta entidade = new CompraConjunta();
			entidade.CompraConjuntaId = compraConjuntaId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um CompraConjunta.
		/// </summary>
        /// <param name="entidade">CompraConjunta a ser carregado (somente o identificador é necessário).</param>
		/// <returns>CompraConjunta</returns>
		public CompraConjunta Carregar(CompraConjunta entidade) {		
		
			CompraConjunta entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM CompraConjunta WHERE compraConjuntaId=@compraConjuntaId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@compraConjuntaId", DbType.Int32, entidade.CompraConjuntaId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new CompraConjunta();
				PopulaCompraConjunta(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de CompraConjunta.
        /// </summary>
        /// <param name="entidade">CarrinhoItemCompraConjunta relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de CompraConjunta.</returns>
		public IEnumerable<CompraConjunta> Carregar(CarrinhoItemCompraConjunta entidade)
		{		
			List<CompraConjunta> entidadesRetorno = new List<CompraConjunta>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT CompraConjunta.* FROM CompraConjunta INNER JOIN CarrinhoItemCompraConjunta ON CompraConjunta.compraConjuntaId=CarrinhoItemCompraConjunta.compraConjuntaId WHERE CarrinhoItemCompraConjunta.carrinhoItemCompraConjuntaId=@carrinhoItemCompraConjuntaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@carrinhoItemCompraConjuntaId", DbType.Int32, entidade.CarrinhoItemCompraConjuntaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                CompraConjunta entidadeRetorno = new CompraConjunta();
                PopulaCompraConjunta(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de CompraConjunta.
        /// </summary>
        /// <param name="entidade">CompraConjuntaDesconto relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de CompraConjunta.</returns>
		public IEnumerable<CompraConjunta> Carregar(CompraConjuntaDesconto entidade)
		{		
			List<CompraConjunta> entidadesRetorno = new List<CompraConjunta>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT CompraConjunta.* FROM CompraConjunta INNER JOIN CompraConjuntaDesconto ON CompraConjunta.compraConjuntaId=CompraConjuntaDesconto.compraConjuntaId WHERE CompraConjuntaDesconto.compraConjuntaDescontoId=@compraConjuntaDescontoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@compraConjuntaDescontoId", DbType.Int32, entidade.CompraConjuntaDescontoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                CompraConjunta entidadeRetorno = new CompraConjunta();
                PopulaCompraConjunta(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de CompraConjunta.
        /// </summary>
        /// <param name="entidade">CompraConjuntaPagina relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de CompraConjunta.</returns>
		public IEnumerable<CompraConjunta> Carregar(CompraConjuntaPagina entidade)
		{		
			List<CompraConjunta> entidadesRetorno = new List<CompraConjunta>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT CompraConjunta.* FROM CompraConjunta INNER JOIN CompraConjuntaLocalizacao ON CompraConjunta.compraConjuntaId=CompraConjuntaLocalizacao.compraConjuntaId WHERE CompraConjuntaLocalizacao.compraConjuntaPaginaId=@compraConjuntaPaginaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@compraConjuntaPaginaId", DbType.Int32, entidade.CompraConjuntaPaginaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                CompraConjunta entidadeRetorno = new CompraConjunta();
                PopulaCompraConjunta(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de CompraConjunta.
        /// </summary>
        /// <param name="entidade">PedidoCompraConjunta relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de CompraConjunta.</returns>
		public IEnumerable<CompraConjunta> Carregar(PedidoCompraConjunta entidade)
		{		
			List<CompraConjunta> entidadesRetorno = new List<CompraConjunta>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT CompraConjunta.* FROM CompraConjunta INNER JOIN PedidoCompraConjunta ON CompraConjunta.compraConjuntaId=PedidoCompraConjunta.compraConjuntaId WHERE PedidoCompraConjunta.pedidoCompraConjuntaId=@pedidoCompraConjuntaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@pedidoCompraConjuntaId", DbType.Int32, entidade.PedidoCompraConjuntaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                CompraConjunta entidadeRetorno = new CompraConjunta();
                PopulaCompraConjunta(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de CompraConjunta.
        /// </summary>
        /// <param name="entidade">PedidoItemCompraConjunta relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de CompraConjunta.</returns>
		public IEnumerable<CompraConjunta> Carregar(PedidoItemCompraConjunta entidade)
		{		
			List<CompraConjunta> entidadesRetorno = new List<CompraConjunta>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT CompraConjunta.* FROM CompraConjunta INNER JOIN PedidoItemCompraConjunta ON CompraConjunta.compraConjuntaId=PedidoItemCompraConjunta.compraConjuntaId WHERE PedidoItemCompraConjunta.pedidoItemCompraConjuntaId=@pedidoItemCompraConjuntaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@pedidoItemCompraConjuntaId", DbType.Int32, entidade.PedidoItemCompraConjuntaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                CompraConjunta entidadeRetorno = new CompraConjunta();
                PopulaCompraConjunta(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de CompraConjunta.
        /// </summary>
        /// <param name="entidade">CompraConjuntaStatus relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de CompraConjunta.</returns>
		public IEnumerable<CompraConjunta> Carregar(CompraConjuntaStatus entidade)
		{		
			List<CompraConjunta> entidadesRetorno = new List<CompraConjunta>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT CompraConjunta.* FROM CompraConjunta WHERE CompraConjunta.compraConjuntaStatusId=@compraConjuntaStatusId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@compraConjuntaStatusId", DbType.Int32, entidade.CompraConjuntaStatusId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                CompraConjunta entidadeRetorno = new CompraConjunta();
                PopulaCompraConjunta(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de CompraConjunta.
        /// </summary>
        /// <param name="entidade">Produto relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de CompraConjunta.</returns>
		public IEnumerable<CompraConjunta> Carregar(Produto entidade)
		{		
			List<CompraConjunta> entidadesRetorno = new List<CompraConjunta>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT CompraConjunta.* FROM CompraConjunta WHERE CompraConjunta.produtoId=@produtoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.ProdutoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                CompraConjunta entidadeRetorno = new CompraConjunta();
                PopulaCompraConjunta(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de CompraConjunta.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos CompraConjunta.</returns>
		public IEnumerable<CompraConjunta> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<CompraConjunta> entidadesRetorno = new List<CompraConjunta>();
			
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
				sbOrder.Append( " ORDER BY compraConjuntaId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM CompraConjunta");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM CompraConjunta WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM CompraConjunta ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT CompraConjunta.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM CompraConjunta ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT CompraConjunta.* FROM CompraConjunta ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                CompraConjunta entidadeRetorno = new CompraConjunta();
                PopulaCompraConjunta(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os CompraConjunta existentes na base de dados.
        /// </summary>
		public IEnumerable<CompraConjunta> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de CompraConjunta na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de CompraConjunta na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM CompraConjunta");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um CompraConjunta baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">CompraConjunta a ser populado(.</param>
		public static void PopulaCompraConjunta(IDataReader reader, CompraConjunta entidade) 
		{						
			if (reader["compraConjuntaId"] != DBNull.Value)
				entidade.CompraConjuntaId = Convert.ToInt32(reader["compraConjuntaId"].ToString());
			
			if (reader["dataInicialCompra"] != DBNull.Value)
				entidade.DataInicialCompra = Convert.ToDateTime(reader["dataInicialCompra"].ToString());
			
			if (reader["dataFinalCompra"] != DBNull.Value)
				entidade.DataFinalCompra = Convert.ToDateTime(reader["dataFinalCompra"].ToString());
			
			if (reader["estoqueSeguranca"] != DBNull.Value)
				entidade.EstoqueSeguranca = Convert.ToInt32(reader["estoqueSeguranca"].ToString());
			
			if (reader["dataHoraFinalizacao"] != DBNull.Value)
				entidade.DataHoraFinalizacao = Convert.ToDateTime(reader["dataHoraFinalizacao"].ToString());
			
			if (reader["ativa"] != DBNull.Value)
				entidade.Ativa = Convert.ToBoolean(reader["ativa"].ToString());
			
			if (reader["quantidadeLimite"] != DBNull.Value)
				entidade.QuantidadeLimite = Convert.ToInt32(reader["quantidadeLimite"].ToString());
			
			if (reader["produtoId"] != DBNull.Value) {
				entidade.Produto = new Produto();
				entidade.Produto.ProdutoId = Convert.ToInt32(reader["produtoId"].ToString());
			}

			if (reader["compraConjuntaStatusId"] != DBNull.Value) {
				entidade.CompraConjuntaStatus = new CompraConjuntaStatus();
				entidade.CompraConjuntaStatus.CompraConjuntaStatusId = Convert.ToInt32(reader["compraConjuntaStatusId"].ToString());
			}


		}		
		
	}
}
		