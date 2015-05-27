
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
	public partial class EnderecoTipoADO : ADOSuper, IEnderecoTipoDAL {
	
	    /// <summary>
        /// Método que persiste um EnderecoTipo.
        /// </summary>
        /// <param name="entidade">EnderecoTipo contendo os dados a serem persistidos.</param>	
		public void Inserir(EnderecoTipo entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO EnderecoTipo ");
			sbSQL.Append(" (enderecoTipoId, tipo) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@enderecoTipoId, @tipo) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@enderecoTipoId", DbType.Int32, entidade.EnderecoTipoId);

			_db.AddInParameter(command, "@tipo", DbType.String, entidade.Tipo);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um EnderecoTipo.
        /// </summary>
        /// <param name="entidade">EnderecoTipo contendo os dados a serem atualizados.</param>
		public void Atualizar(EnderecoTipo entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE EnderecoTipo SET ");
			sbSQL.Append(" tipo=@tipo ");
			sbSQL.Append(" WHERE enderecoTipoId=@enderecoTipoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@enderecoTipoId", DbType.Int32, entidade.EnderecoTipoId);
			_db.AddInParameter(command, "@tipo", DbType.String, entidade.Tipo);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um EnderecoTipo da base de dados.
        /// </summary>
        /// <param name="entidade">EnderecoTipo a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(EnderecoTipo entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM EnderecoTipo ");
			sbSQL.Append("WHERE enderecoTipoId=@enderecoTipoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@enderecoTipoId", DbType.Int32, entidade.EnderecoTipoId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um EnderecoTipo.
		/// </summary>
        /// <param name="entidade">EnderecoTipo a ser carregado (somente o identificador é necessário).</param>
		/// <returns>EnderecoTipo</returns>
		public EnderecoTipo Carregar(int enderecoTipoId) {		
			EnderecoTipo entidade = new EnderecoTipo();
			entidade.EnderecoTipoId = enderecoTipoId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um EnderecoTipo.
		/// </summary>
        /// <param name="entidade">EnderecoTipo a ser carregado (somente o identificador é necessário).</param>
		/// <returns>EnderecoTipo</returns>
		public EnderecoTipo Carregar(EnderecoTipo entidade) {		
		
			EnderecoTipo entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM EnderecoTipo WHERE enderecoTipoId=@enderecoTipoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@enderecoTipoId", DbType.Int32, entidade.EnderecoTipoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new EnderecoTipo();
				PopulaEnderecoTipo(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de EnderecoTipo.
        /// </summary>
        /// <param name="entidade">Endereco relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de EnderecoTipo.</returns>
		public IEnumerable<EnderecoTipo> Carregar(Endereco entidade)
		{		
			List<EnderecoTipo> entidadesRetorno = new List<EnderecoTipo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT EnderecoTipo.* FROM EnderecoTipo INNER JOIN Endereco ON EnderecoTipo.enderecoTipoId=Endereco.enderecoTipoId WHERE Endereco.enderecoId=@enderecoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@enderecoId", DbType.Int32, entidade.EnderecoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                EnderecoTipo entidadeRetorno = new EnderecoTipo();
                PopulaEnderecoTipo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de EnderecoTipo.
        /// </summary>
        /// <param name="entidade">PedidoEndereco relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de EnderecoTipo.</returns>
		public IEnumerable<EnderecoTipo> Carregar(PedidoEndereco entidade)
		{		
			List<EnderecoTipo> entidadesRetorno = new List<EnderecoTipo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT EnderecoTipo.* FROM EnderecoTipo INNER JOIN PedidoEndereco ON EnderecoTipo.enderecoTipoId=PedidoEndereco.enderecoTipoId WHERE PedidoEndereco.pedidoEnderecoId=@pedidoEnderecoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@pedidoEnderecoId", DbType.Int32, entidade.PedidoEnderecoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                EnderecoTipo entidadeRetorno = new EnderecoTipo();
                PopulaEnderecoTipo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de EnderecoTipo.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos EnderecoTipo.</returns>
		public IEnumerable<EnderecoTipo> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<EnderecoTipo> entidadesRetorno = new List<EnderecoTipo>();
			
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
				sbOrder.Append( " ORDER BY enderecoTipoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM EnderecoTipo");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM EnderecoTipo WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM EnderecoTipo ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT EnderecoTipo.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM EnderecoTipo ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT EnderecoTipo.* FROM EnderecoTipo ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                EnderecoTipo entidadeRetorno = new EnderecoTipo();
                PopulaEnderecoTipo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os EnderecoTipo existentes na base de dados.
        /// </summary>
		public IEnumerable<EnderecoTipo> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de EnderecoTipo na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de EnderecoTipo na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM EnderecoTipo");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um EnderecoTipo baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">EnderecoTipo a ser populado(.</param>
		public static void PopulaEnderecoTipo(IDataReader reader, EnderecoTipo entidade) 
		{						
			if (reader["enderecoTipoId"] != DBNull.Value)
				entidade.EnderecoTipoId = Convert.ToInt32(reader["enderecoTipoId"].ToString());
			
			if (reader["tipo"] != DBNull.Value)
				entidade.Tipo = reader["tipo"].ToString();
			

		}		
		
	}
}
		