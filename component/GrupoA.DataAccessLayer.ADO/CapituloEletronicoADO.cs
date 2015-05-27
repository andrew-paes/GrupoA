using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using GrupoA.BusinessObject;
using GrupoA.FilterHelper;

namespace GrupoA.DataAccess.ADO
{
	public partial class CapituloEletronicoADO : ADOSuper, ICapituloEletronicoDAL {
	
	    /// <summary>
        /// Método que persiste um CapituloEletronico.
        /// </summary>
        /// <param name="entidade">CapituloEletronico contendo os dados a serem persistidos.</param>	
		public void Inserir(CapituloEletronico entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO CapituloEletronico ");
			sbSQL.Append(" (capituloEletronicoId, tituloEletronicoId, capituloId) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@capituloEletronicoId, @tituloEletronicoId, @capituloId) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@capituloEletronicoId", DbType.Int32, entidade.CapituloEletronicoId);

			_db.AddInParameter(command, "@tituloEletronicoId", DbType.Int32, entidade.TituloEletronico.TituloEletronicoId);

			_db.AddInParameter(command, "@capituloId", DbType.Int32, entidade.Capitulo.CapituloId);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um CapituloEletronico.
        /// </summary>
        /// <param name="entidade">CapituloEletronico contendo os dados a serem atualizados.</param>
		public void Atualizar(CapituloEletronico entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE CapituloEletronico SET ");
			sbSQL.Append(" tituloEletronicoId=@tituloEletronicoId, capituloId=@capituloId ");
			sbSQL.Append(" WHERE capituloEletronicoId=@capituloEletronicoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@capituloEletronicoId", DbType.Int32, entidade.CapituloEletronicoId);
			_db.AddInParameter(command, "@tituloEletronicoId", DbType.Int32, entidade.TituloEletronico.TituloEletronicoId);
			_db.AddInParameter(command, "@capituloId", DbType.Int32, entidade.Capitulo.CapituloId);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um CapituloEletronico da base de dados.
        /// </summary>
        /// <param name="entidade">CapituloEletronico a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(CapituloEletronico entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM CapituloEletronico ");
			sbSQL.Append("WHERE capituloEletronicoId=@capituloEletronicoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@capituloEletronicoId", DbType.Int32, entidade.CapituloEletronicoId);

								
			_db.ExecuteNonQuery(command);
		}
		

		/// <summary>
		/// Método que carrega um CapituloEletronico.
		/// </summary>
        /// <param name="entidade">CapituloEletronico a ser carregado (somente o identificador é necessário).</param>
		/// <returns>CapituloEletronico</returns>
		public CapituloEletronico Carregar(CapituloEletronico entidade) {		
		
			CapituloEletronico entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM CapituloEletronico WHERE capituloEletronicoId=@capituloEletronicoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@capituloEletronicoId", DbType.Int32, entidade.CapituloEletronicoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new CapituloEletronico();
				PopulaCapituloEletronico(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		
		
		/// <summary>
		/// Método que carrega um CapituloEletronico com suas dependências.
		/// </summary>
        /// <param name="entidade">CapituloEletronico a ser carregado (somente o identificador é necessário).</param>
		/// <returns>CapituloEletronico</returns>
		public CapituloEletronico CarregarComDependencias(CapituloEletronico entidade) {		
		
			CapituloEletronico entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append("SELECT CapituloEletronico.capituloEletronicoId, CapituloEletronico.tituloEletronicoId, CapituloEletronico.capituloId");
			sbSQL.Append(", produtoId, produtoTipoId, disponivel, fabricanteId, valorUnitario, valorOferta, codigoEAN13, codigoProduto, exibirSite, nomeProduto, utilizaFrete, peso, homologado");
			sbSQL.Append(", conteudoId, conteudoTipoId, dataHoraCadastro");
			sbSQL.Append(" FROM CapituloEletronico");
			sbSQL.Append(" INNER JOIN Produto ON CapituloEletronico.capituloEletronicoId=Produto.produtoId");
			sbSQL.Append(" INNER JOIN Conteudo ON Produto.produtoId=Conteudo.conteudoId");
			sbSQL.Append(" WHERE CapituloEletronico.capituloEletronicoId=@capituloEletronicoId");
						
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@capituloEletronicoId", DbType.Int32, entidade.CapituloEletronicoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new CapituloEletronico();
				PopulaCapituloEletronico(reader, entidadeRetorno);
				entidadeRetorno.Produto = new Produto();
				ProdutoADO.PopulaProduto(reader, entidadeRetorno.Produto);
				entidadeRetorno.Produto.Conteudo = new Conteudo();
				ConteudoADO.PopulaConteudo(reader, entidadeRetorno.Produto.Conteudo);
			}
			reader.Close();
			
			return entidadeRetorno;
		}		


		/// <summary>
        /// Método que retorna um CapituloEletronico.
        /// </summary>
        /// <param name="entidade">Capitulo relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna um CapituloEletronico.</returns>
		public CapituloEletronico Carregar(Capitulo entidade)
		{		
			CapituloEletronico entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT CapituloEletronico.* FROM CapituloEletronico WHERE CapituloEletronico.capituloId=@capituloId");
		
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@capituloId", DbType.Int32, entidade.CapituloId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            if (reader.Read())
            {
                entidadeRetorno = new CapituloEletronico();
                PopulaCapituloEletronico(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de CapituloEletronico.
        /// </summary>
        /// <param name="entidade">TituloEletronico relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de CapituloEletronico.</returns>
		public IEnumerable<CapituloEletronico> Carregar(TituloEletronico entidade)
		{		
			List<CapituloEletronico> entidadesRetorno = new List<CapituloEletronico>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT CapituloEletronico.* FROM CapituloEletronico WHERE CapituloEletronico.tituloEletronicoId=@tituloEletronicoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@tituloEletronicoId", DbType.Int32, entidade.TituloEletronicoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                CapituloEletronico entidadeRetorno = new CapituloEletronico();
                PopulaCapituloEletronico(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de CapituloEletronico.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos CapituloEletronico.</returns>
		public IEnumerable<CapituloEletronico> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<CapituloEletronico> entidadesRetorno = new List<CapituloEletronico>();
			
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
				sbOrder.Append( " ORDER BY capituloEletronicoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM CapituloEletronico");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM CapituloEletronico WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM CapituloEletronico ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT CapituloEletronico.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM CapituloEletronico ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT CapituloEletronico.* FROM CapituloEletronico ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                CapituloEletronico entidadeRetorno = new CapituloEletronico();
                PopulaCapituloEletronico(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os CapituloEletronico existentes na base de dados.
        /// </summary>
		public IEnumerable<CapituloEletronico> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de CapituloEletronico na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de CapituloEletronico na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM CapituloEletronico");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um CapituloEletronico baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">CapituloEletronico a ser populado(.</param>
		public static void PopulaCapituloEletronico(IDataReader reader, CapituloEletronico entidade) 
		{						
			if (reader["capituloEletronicoId"] != DBNull.Value) {
				entidade.CapituloEletronicoId = Convert.ToInt32(reader["capituloEletronicoId"].ToString());
			}

			if (reader["tituloEletronicoId"] != DBNull.Value) {
				entidade.TituloEletronico = new TituloEletronico();
				entidade.TituloEletronico.TituloEletronicoId = Convert.ToInt32(reader["tituloEletronicoId"].ToString());
			}

			if (reader["capituloId"] != DBNull.Value) {
				entidade.Capitulo = new Capitulo();
				entidade.Capitulo.CapituloId = Convert.ToInt32(reader["capituloId"].ToString());
			}


		}		
		
	}
}
		