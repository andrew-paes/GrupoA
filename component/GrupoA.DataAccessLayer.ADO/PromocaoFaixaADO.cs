
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
	public partial class PromocaoFaixaADO : ADOSuper, IPromocaoFaixaDAL {
	
	    /// <summary>
        /// Método que persiste um PromocaoFaixa.
        /// </summary>
        /// <param name="entidade">PromocaoFaixa contendo os dados a serem persistidos.</param>	
		public void Inserir(PromocaoFaixa entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO PromocaoFaixa ");
			sbSQL.Append(" (promocaoId, valorMinimo, percentualDesconto, valorDesconto, freteGratis) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@promocaoId, @valorMinimo, @percentualDesconto, @valorDesconto, @freteGratis) ");											

			sbSQL.Append(" ; SET @promocaoFaixaId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@promocaoFaixaId", DbType.Int32, 8);

			_db.AddInParameter(command, "@promocaoId", DbType.Int32, entidade.Promocao.PromocaoId);

			_db.AddInParameter(command, "@valorMinimo", DbType.Decimal, entidade.ValorMinimo);

			if (entidade.PercentualDesconto != null ) 
				_db.AddInParameter(command, "@percentualDesconto", DbType.Decimal, entidade.PercentualDesconto);
			else
				_db.AddInParameter(command, "@percentualDesconto", DbType.Decimal, null);

			if (entidade.ValorDesconto != null ) 
				_db.AddInParameter(command, "@valorDesconto", DbType.Decimal, entidade.ValorDesconto);
			else
				_db.AddInParameter(command, "@valorDesconto", DbType.Decimal, null);

			_db.AddInParameter(command, "@freteGratis", DbType.Int32, entidade.FreteGratis);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.PromocaoFaixaId = Convert.ToInt32(_db.GetParameterValue(command, "@promocaoFaixaId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um PromocaoFaixa.
        /// </summary>
        /// <param name="entidade">PromocaoFaixa contendo os dados a serem atualizados.</param>
		public void Atualizar(PromocaoFaixa entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE PromocaoFaixa SET ");
			sbSQL.Append(" promocaoId=@promocaoId, valorMinimo=@valorMinimo, percentualDesconto=@percentualDesconto, valorDesconto=@valorDesconto, freteGratis=@freteGratis ");
			sbSQL.Append(" WHERE promocaoFaixaId=@promocaoFaixaId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@promocaoFaixaId", DbType.Int32, entidade.PromocaoFaixaId);
			_db.AddInParameter(command, "@promocaoId", DbType.Int32, entidade.Promocao.PromocaoId);
			_db.AddInParameter(command, "@valorMinimo", DbType.Decimal, entidade.ValorMinimo);
			if (entidade.PercentualDesconto != null ) 
				_db.AddInParameter(command, "@percentualDesconto", DbType.Decimal, entidade.PercentualDesconto);
			else
				_db.AddInParameter(command, "@percentualDesconto", DbType.Decimal, null);
			if (entidade.ValorDesconto != null ) 
				_db.AddInParameter(command, "@valorDesconto", DbType.Decimal, entidade.ValorDesconto);
			else
				_db.AddInParameter(command, "@valorDesconto", DbType.Decimal, null);
			_db.AddInParameter(command, "@freteGratis", DbType.Int32, entidade.FreteGratis);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um PromocaoFaixa da base de dados.
        /// </summary>
        /// <param name="entidade">PromocaoFaixa a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(PromocaoFaixa entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM PromocaoFaixa ");
			sbSQL.Append("WHERE promocaoFaixaId=@promocaoFaixaId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@promocaoFaixaId", DbType.Int32, entidade.PromocaoFaixaId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um PromocaoFaixa.
		/// </summary>
        /// <param name="entidade">PromocaoFaixa a ser carregado (somente o identificador é necessário).</param>
		/// <returns>PromocaoFaixa</returns>
		public PromocaoFaixa Carregar(int promocaoFaixaId) {		
			PromocaoFaixa entidade = new PromocaoFaixa();
			entidade.PromocaoFaixaId = promocaoFaixaId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um PromocaoFaixa.
		/// </summary>
        /// <param name="entidade">PromocaoFaixa a ser carregado (somente o identificador é necessário).</param>
		/// <returns>PromocaoFaixa</returns>
		public PromocaoFaixa Carregar(PromocaoFaixa entidade) {		
		
			PromocaoFaixa entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM PromocaoFaixa WHERE promocaoFaixaId=@promocaoFaixaId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@promocaoFaixaId", DbType.Int32, entidade.PromocaoFaixaId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new PromocaoFaixa();
				PopulaPromocaoFaixa(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de PromocaoFaixa.
        /// </summary>
        /// <param name="entidade">Promocao relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de PromocaoFaixa.</returns>
		public IEnumerable<PromocaoFaixa> Carregar(Promocao entidade)
		{		
			List<PromocaoFaixa> entidadesRetorno = new List<PromocaoFaixa>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT PromocaoFaixa.* FROM PromocaoFaixa WHERE PromocaoFaixa.promocaoId=@promocaoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@promocaoId", DbType.Int32, entidade.PromocaoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                PromocaoFaixa entidadeRetorno = new PromocaoFaixa();
                PopulaPromocaoFaixa(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de PromocaoFaixa.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos PromocaoFaixa.</returns>
		public IEnumerable<PromocaoFaixa> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<PromocaoFaixa> entidadesRetorno = new List<PromocaoFaixa>();
			
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
				sbOrder.Append( " ORDER BY promocaoFaixaId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM PromocaoFaixa");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM PromocaoFaixa WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM PromocaoFaixa ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT PromocaoFaixa.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM PromocaoFaixa ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT PromocaoFaixa.* FROM PromocaoFaixa ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                PromocaoFaixa entidadeRetorno = new PromocaoFaixa();
                PopulaPromocaoFaixa(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os PromocaoFaixa existentes na base de dados.
        /// </summary>
		public IEnumerable<PromocaoFaixa> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de PromocaoFaixa na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de PromocaoFaixa na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM PromocaoFaixa");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um PromocaoFaixa baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">PromocaoFaixa a ser populado(.</param>
		public static void PopulaPromocaoFaixa(IDataReader reader, PromocaoFaixa entidade) 
		{						
			if (reader["promocaoFaixaId"] != DBNull.Value)
				entidade.PromocaoFaixaId = Convert.ToInt32(reader["promocaoFaixaId"].ToString());
			
			if (reader["valorMinimo"] != DBNull.Value)
				entidade.ValorMinimo = Convert.ToDecimal(reader["valorMinimo"].ToString());
			
			if (reader["percentualDesconto"] != DBNull.Value)
				entidade.PercentualDesconto = Convert.ToDecimal(reader["percentualDesconto"].ToString());
			
			if (reader["valorDesconto"] != DBNull.Value)
				entidade.ValorDesconto = Convert.ToDecimal(reader["valorDesconto"].ToString());
			
			if (reader["freteGratis"] != DBNull.Value)
				entidade.FreteGratis = Convert.ToBoolean(reader["freteGratis"].ToString());
			
			if (reader["promocaoId"] != DBNull.Value) {
				entidade.Promocao = new Promocao();
				entidade.Promocao.PromocaoId = Convert.ToInt32(reader["promocaoId"].ToString());
			}


		}		
		
	}
}
		