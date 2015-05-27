
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
	public partial class OfertaADO : ADOSuper, IOfertaDAL {
	
	    /// <summary>
        /// Método que persiste um Oferta.
        /// </summary>
        /// <param name="entidade">Oferta contendo os dados a serem persistidos.</param>	
		public void Inserir(Oferta entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO Oferta ");
			sbSQL.Append(" (ofertaTipoId, percentual, precoOferta, nomeOferta, dataHoraInicio, dataHoraTermino) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@ofertaTipoId, @percentual, @precoOferta, @nomeOferta, @dataHoraInicio, @dataHoraTermino) ");											

			sbSQL.Append(" ; SET @ofertaId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@ofertaId", DbType.Int32, 8);

			_db.AddInParameter(command, "@ofertaTipoId", DbType.Int32, entidade.OfertaTipo.OfertaTipoId);

			if (entidade.Percentual != null ) 
				_db.AddInParameter(command, "@percentual", DbType.Decimal, entidade.Percentual);
			else
				_db.AddInParameter(command, "@percentual", DbType.Decimal, null);

			if (entidade.PrecoOferta != null ) 
				_db.AddInParameter(command, "@precoOferta", DbType.Decimal, entidade.PrecoOferta);
			else
				_db.AddInParameter(command, "@precoOferta", DbType.Decimal, null);

			if (entidade.NomeOferta != null ) 
				_db.AddInParameter(command, "@nomeOferta", DbType.String, entidade.NomeOferta);
			else
				_db.AddInParameter(command, "@nomeOferta", DbType.String, null);

			_db.AddInParameter(command, "@dataHoraInicio", DbType.DateTime, entidade.DataHoraInicio);

			_db.AddInParameter(command, "@dataHoraTermino", DbType.DateTime, entidade.DataHoraTermino);
		
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.OfertaId = Convert.ToInt32(_db.GetParameterValue(command, "@ofertaId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um Oferta.
        /// </summary>
        /// <param name="entidade">Oferta contendo os dados a serem atualizados.</param>
		public void Atualizar(Oferta entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE Oferta SET ");
			sbSQL.Append(" ofertaTipoId=@ofertaTipoId, percentual=@percentual, precoOferta=@precoOferta, nomeOferta=@nomeOferta, dataHoraInicio=@dataHoraInicio, dataHoraTermino=@dataHoraTermino ");
			sbSQL.Append(" WHERE ofertaId=@ofertaId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@ofertaId", DbType.Int32, entidade.OfertaId);
			_db.AddInParameter(command, "@ofertaTipoId", DbType.Int32, entidade.OfertaTipo.OfertaTipoId);
			if (entidade.Percentual != null ) 
				_db.AddInParameter(command, "@percentual", DbType.Decimal, entidade.Percentual);
			else
				_db.AddInParameter(command, "@percentual", DbType.Decimal, null);
			if (entidade.PrecoOferta != null ) 
				_db.AddInParameter(command, "@precoOferta", DbType.Decimal, entidade.PrecoOferta);
			else
				_db.AddInParameter(command, "@precoOferta", DbType.Decimal, null);
			if (entidade.NomeOferta != null ) 
				_db.AddInParameter(command, "@nomeOferta", DbType.String, entidade.NomeOferta);
			else
				_db.AddInParameter(command, "@nomeOferta", DbType.String, null);
			_db.AddInParameter(command, "@dataHoraInicio", DbType.DateTime, entidade.DataHoraInicio);
			_db.AddInParameter(command, "@dataHoraTermino", DbType.DateTime, entidade.DataHoraTermino);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um Oferta da base de dados.
        /// </summary>
        /// <param name="entidade">Oferta a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(Oferta entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM Oferta ");
			sbSQL.Append("WHERE ofertaId=@ofertaId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@ofertaId", DbType.Int32, entidade.OfertaId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um Oferta.
		/// </summary>
        /// <param name="entidade">Oferta a ser carregado (somente o identificador é necessário).</param>
		/// <returns>Oferta</returns>
		public Oferta Carregar(int ofertaId) {		
			Oferta entidade = new Oferta();
			entidade.OfertaId = ofertaId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um Oferta.
		/// </summary>
        /// <param name="entidade">Oferta a ser carregado (somente o identificador é necessário).</param>
		/// <returns>Oferta</returns>
		public Oferta Carregar(Oferta entidade) {		
		
			Oferta entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM Oferta WHERE ofertaId=@ofertaId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@ofertaId", DbType.Int32, entidade.OfertaId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new Oferta();
				PopulaOferta(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de Oferta.
        /// </summary>
        /// <param name="entidade">OfertaCategoria relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Oferta.</returns>
		public IEnumerable<Oferta> Carregar(OfertaCategoria entidade)
		{		
			List<Oferta> entidadesRetorno = new List<Oferta>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Oferta.* FROM Oferta INNER JOIN OfertaCategoria ON Oferta.ofertaId=OfertaCategoria.ofertaId WHERE OfertaCategoria.ofertaCategoriaId=@ofertaCategoriaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@ofertaCategoriaId", DbType.Int32, entidade.OfertaCategoriaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Oferta entidadeRetorno = new Oferta();
                PopulaOferta(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Oferta.
        /// </summary>
        /// <param name="entidade">OfertaProduto relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Oferta.</returns>
		public IEnumerable<Oferta> Carregar(OfertaProduto entidade)
		{		
			List<Oferta> entidadesRetorno = new List<Oferta>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Oferta.* FROM Oferta INNER JOIN OfertaProduto ON Oferta.ofertaId=OfertaProduto.ofertaId WHERE OfertaProduto.ofertaProdutoId=@ofertaProdutoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@ofertaProdutoId", DbType.Int32, entidade.OfertaProdutoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Oferta entidadeRetorno = new Oferta();
                PopulaOferta(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Oferta.
        /// </summary>
        /// <param name="entidade">OfertaTipo relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Oferta.</returns>
		public IEnumerable<Oferta> Carregar(OfertaTipo entidade)
		{		
			List<Oferta> entidadesRetorno = new List<Oferta>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Oferta.* FROM Oferta WHERE Oferta.ofertaTipoId=@ofertaTipoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@ofertaTipoId", DbType.Int32, entidade.OfertaTipoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Oferta entidadeRetorno = new Oferta();
                PopulaOferta(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de Oferta.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos Oferta.</returns>
		public IEnumerable<Oferta> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<Oferta> entidadesRetorno = new List<Oferta>();
			
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
				sbOrder.Append( " ORDER BY ofertaId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM Oferta");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Oferta WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Oferta ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT Oferta.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM Oferta ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT Oferta.* FROM Oferta ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Oferta entidadeRetorno = new Oferta();
                PopulaOferta(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os Oferta existentes na base de dados.
        /// </summary>
		public IEnumerable<Oferta> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de Oferta na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de Oferta na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM Oferta");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um Oferta baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Oferta a ser populado(.</param>
		public static void PopulaOferta(IDataReader reader, Oferta entidade) 
		{						
			if (reader["ofertaId"] != DBNull.Value)
				entidade.OfertaId = Convert.ToInt32(reader["ofertaId"].ToString());
			
			if (reader["percentual"] != DBNull.Value)
				entidade.Percentual = Convert.ToDecimal(reader["percentual"].ToString());
			
			if (reader["precoOferta"] != DBNull.Value)
				entidade.PrecoOferta = Convert.ToDecimal(reader["precoOferta"].ToString());
			
			if (reader["nomeOferta"] != DBNull.Value)
				entidade.NomeOferta = reader["nomeOferta"].ToString();
			
			if (reader["dataHoraInicio"] != DBNull.Value)
				entidade.DataHoraInicio = Convert.ToDateTime(reader["dataHoraInicio"].ToString());
			
			if (reader["dataHoraTermino"] != DBNull.Value)
				entidade.DataHoraTermino = Convert.ToDateTime(reader["dataHoraTermino"].ToString());
			
			if (reader["ofertaTipoId"] != DBNull.Value) {
				entidade.OfertaTipo = new OfertaTipo();
				entidade.OfertaTipo.OfertaTipoId = Convert.ToInt32(reader["ofertaTipoId"].ToString());
			}


		}		
		
	}
}
		