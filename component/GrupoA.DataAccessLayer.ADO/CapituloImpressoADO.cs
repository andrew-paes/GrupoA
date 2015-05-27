using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using GrupoA.BusinessObject;
using GrupoA.FilterHelper;

namespace GrupoA.DataAccess.ADO
{
	public partial class CapituloImpressoADO : ADOSuper, ICapituloImpressoDAL {
	
	    /// <summary>
        /// Método que persiste um CapituloImpresso.
        /// </summary>
        /// <param name="entidade">CapituloImpresso contendo os dados a serem persistidos.</param>	
		public void Inserir(CapituloImpresso entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO CapituloImpresso ");
			sbSQL.Append(" (capituloImpressoId, capituloId, tituloImpressoId) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@capituloImpressoId, @capituloId, @tituloImpressoId) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@capituloImpressoId", DbType.Int32, entidade.CapituloImpressoId);

			_db.AddInParameter(command, "@capituloId", DbType.Int32, entidade.Capitulo.CapituloId);

			_db.AddInParameter(command, "@tituloImpressoId", DbType.Int32, entidade.TituloImpresso.TituloImpressoId);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um CapituloImpresso.
        /// </summary>
        /// <param name="entidade">CapituloImpresso contendo os dados a serem atualizados.</param>
		public void Atualizar(CapituloImpresso entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE CapituloImpresso SET ");
			sbSQL.Append(" capituloId=@capituloId, tituloImpressoId=@tituloImpressoId ");
			sbSQL.Append(" WHERE capituloImpressoId=@capituloImpressoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@capituloImpressoId", DbType.Int32, entidade.CapituloImpressoId);
			_db.AddInParameter(command, "@capituloId", DbType.Int32, entidade.Capitulo.CapituloId);
			_db.AddInParameter(command, "@tituloImpressoId", DbType.Int32, entidade.TituloImpresso.TituloImpressoId);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um CapituloImpresso da base de dados.
        /// </summary>
        /// <param name="entidade">CapituloImpresso a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(CapituloImpresso entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM CapituloImpresso ");
			sbSQL.Append("WHERE capituloImpressoId=@capituloImpressoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@capituloImpressoId", DbType.Int32, entidade.CapituloImpressoId);

								
			_db.ExecuteNonQuery(command);
		}
		

		/// <summary>
		/// Método que carrega um CapituloImpresso.
		/// </summary>
        /// <param name="entidade">CapituloImpresso a ser carregado (somente o identificador é necessário).</param>
		/// <returns>CapituloImpresso</returns>
		public CapituloImpresso Carregar(CapituloImpresso entidade) {		
		
			CapituloImpresso entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM CapituloImpresso WHERE capituloImpressoId=@capituloImpressoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@capituloImpressoId", DbType.Int32, entidade.CapituloImpressoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new CapituloImpresso();
				PopulaCapituloImpresso(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		
		
		/// <summary>
		/// Método que carrega um CapituloImpresso com suas dependências.
		/// </summary>
        /// <param name="entidade">CapituloImpresso a ser carregado (somente o identificador é necessário).</param>
		/// <returns>CapituloImpresso</returns>
		public CapituloImpresso CarregarComDependencias(CapituloImpresso entidade) {		
		
			CapituloImpresso entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append("SELECT CapituloImpresso.capituloImpressoId, CapituloImpresso.capituloId, CapituloImpresso.tituloImpressoId");
			sbSQL.Append(", produtoId, produtoTipoId, disponivel, fabricanteId, valorUnitario, valorOferta, codigoEAN13, codigoProduto, exibirSite, nomeProduto, utilizaFrete, peso, homologado");
			sbSQL.Append(", conteudoId, conteudoTipoId, dataHoraCadastro");
			sbSQL.Append(" FROM CapituloImpresso");
			sbSQL.Append(" INNER JOIN Produto ON CapituloImpresso.capituloImpressoId=Produto.produtoId");
			sbSQL.Append(" INNER JOIN Conteudo ON Produto.produtoId=Conteudo.conteudoId");
			sbSQL.Append(" WHERE CapituloImpresso.capituloImpressoId=@capituloImpressoId");
						
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@capituloImpressoId", DbType.Int32, entidade.CapituloImpressoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new CapituloImpresso();
				PopulaCapituloImpresso(reader, entidadeRetorno);
				entidadeRetorno.Produto = new Produto();
				ProdutoADO.PopulaProduto(reader, entidadeRetorno.Produto);
				entidadeRetorno.Produto.Conteudo = new Conteudo();
				ConteudoADO.PopulaConteudo(reader, entidadeRetorno.Produto.Conteudo);
			}
			reader.Close();
			
			return entidadeRetorno;
		}		


		/// <summary>
        /// Método que retorna um CapituloImpresso.
        /// </summary>
        /// <param name="entidade">Capitulo relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna um CapituloImpresso.</returns>
		public CapituloImpresso Carregar(Capitulo entidade)
		{		
			CapituloImpresso entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT CapituloImpresso.* FROM CapituloImpresso WHERE CapituloImpresso.capituloId=@capituloId");
		
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@capituloId", DbType.Int32, entidade.CapituloId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            if (reader.Read())
            {
                entidadeRetorno = new CapituloImpresso();
                PopulaCapituloImpresso(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de CapituloImpresso.
        /// </summary>
        /// <param name="entidade">TituloImpresso relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de CapituloImpresso.</returns>
		public IEnumerable<CapituloImpresso> Carregar(TituloImpresso entidade)
		{		
			List<CapituloImpresso> entidadesRetorno = new List<CapituloImpresso>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT CapituloImpresso.* FROM CapituloImpresso WHERE CapituloImpresso.tituloImpressoId=@tituloImpressoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@tituloImpressoId", DbType.Int32, entidade.TituloImpressoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                CapituloImpresso entidadeRetorno = new CapituloImpresso();
                PopulaCapituloImpresso(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de CapituloImpresso.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos CapituloImpresso.</returns>
		public IEnumerable<CapituloImpresso> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<CapituloImpresso> entidadesRetorno = new List<CapituloImpresso>();
			
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
				sbOrder.Append( " ORDER BY capituloImpressoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM CapituloImpresso");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM CapituloImpresso WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM CapituloImpresso ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT CapituloImpresso.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM CapituloImpresso ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT CapituloImpresso.* FROM CapituloImpresso ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                CapituloImpresso entidadeRetorno = new CapituloImpresso();
                PopulaCapituloImpresso(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os CapituloImpresso existentes na base de dados.
        /// </summary>
		public IEnumerable<CapituloImpresso> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de CapituloImpresso na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de CapituloImpresso na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM CapituloImpresso");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um CapituloImpresso baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">CapituloImpresso a ser populado(.</param>
		public static void PopulaCapituloImpresso(IDataReader reader, CapituloImpresso entidade) 
		{						
			if (reader["capituloImpressoId"] != DBNull.Value) {
				entidade.CapituloImpressoId = Convert.ToInt32(reader["capituloImpressoId"].ToString());
			}

			if (reader["capituloId"] != DBNull.Value) {
				entidade.Capitulo = new Capitulo();
				entidade.Capitulo.CapituloId = Convert.ToInt32(reader["capituloId"].ToString());
			}

			if (reader["tituloImpressoId"] != DBNull.Value) {
				entidade.TituloImpresso = new TituloImpresso();
				entidade.TituloImpresso.TituloImpressoId = Convert.ToInt32(reader["tituloImpressoId"].ToString());
			}


		}		
		
	}
}
		