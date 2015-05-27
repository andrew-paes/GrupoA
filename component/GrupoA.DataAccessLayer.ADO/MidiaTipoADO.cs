
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
	public partial class MidiaTipoADO : ADOSuper, IMidiaTipoDAL {
	
	    /// <summary>
        /// Método que persiste um MidiaTipo.
        /// </summary>
        /// <param name="entidade">MidiaTipo contendo os dados a serem persistidos.</param>	
		public void Inserir(MidiaTipo entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO MidiaTipo ");
			sbSQL.Append(" (midiaTipoId, tipoMidia) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@midiaTipoId, @tipoMidia) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@midiaTipoId", DbType.Int32, entidade.MidiaTipoId);

			_db.AddInParameter(command, "@tipoMidia", DbType.String, entidade.TipoMidia);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um MidiaTipo.
        /// </summary>
        /// <param name="entidade">MidiaTipo contendo os dados a serem atualizados.</param>
		public void Atualizar(MidiaTipo entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE MidiaTipo SET ");
			sbSQL.Append(" tipoMidia=@tipoMidia ");
			sbSQL.Append(" WHERE midiaTipoId=@midiaTipoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@midiaTipoId", DbType.Int32, entidade.MidiaTipoId);
			_db.AddInParameter(command, "@tipoMidia", DbType.String, entidade.TipoMidia);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um MidiaTipo da base de dados.
        /// </summary>
        /// <param name="entidade">MidiaTipo a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(MidiaTipo entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM MidiaTipo ");
			sbSQL.Append("WHERE midiaTipoId=@midiaTipoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@midiaTipoId", DbType.Int32, entidade.MidiaTipoId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um MidiaTipo.
		/// </summary>
        /// <param name="entidade">MidiaTipo a ser carregado (somente o identificador é necessário).</param>
		/// <returns>MidiaTipo</returns>
		public MidiaTipo Carregar(int midiaTipoId) {		
			MidiaTipo entidade = new MidiaTipo();
			entidade.MidiaTipoId = midiaTipoId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um MidiaTipo.
		/// </summary>
        /// <param name="entidade">MidiaTipo a ser carregado (somente o identificador é necessário).</param>
		/// <returns>MidiaTipo</returns>
		public MidiaTipo Carregar(MidiaTipo entidade) {		
		
			MidiaTipo entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM MidiaTipo WHERE midiaTipoId=@midiaTipoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@midiaTipoId", DbType.Int32, entidade.MidiaTipoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new MidiaTipo();
				PopulaMidiaTipo(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de MidiaTipo.
        /// </summary>
        /// <param name="entidade">Midia relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de MidiaTipo.</returns>
		public IEnumerable<MidiaTipo> Carregar(Midia entidade)
		{		
			List<MidiaTipo> entidadesRetorno = new List<MidiaTipo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT MidiaTipo.* FROM MidiaTipo INNER JOIN Midia ON MidiaTipo.midiaTipoId=Midia.midiaTipoId WHERE Midia.midiaId=@midiaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@midiaId", DbType.Int32, entidade.MidiaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                MidiaTipo entidadeRetorno = new MidiaTipo();
                PopulaMidiaTipo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de MidiaTipo.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos MidiaTipo.</returns>
		public IEnumerable<MidiaTipo> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<MidiaTipo> entidadesRetorno = new List<MidiaTipo>();
			
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
				sbOrder.Append( " ORDER BY midiaTipoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM MidiaTipo");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM MidiaTipo WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM MidiaTipo ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT MidiaTipo.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM MidiaTipo ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT MidiaTipo.* FROM MidiaTipo ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                MidiaTipo entidadeRetorno = new MidiaTipo();
                PopulaMidiaTipo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os MidiaTipo existentes na base de dados.
        /// </summary>
		public IEnumerable<MidiaTipo> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de MidiaTipo na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de MidiaTipo na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM MidiaTipo");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um MidiaTipo baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">MidiaTipo a ser populado(.</param>
		public static void PopulaMidiaTipo(IDataReader reader, MidiaTipo entidade) 
		{						
			if (reader["midiaTipoId"] != DBNull.Value)
				entidade.MidiaTipoId = Convert.ToInt32(reader["midiaTipoId"].ToString());
			
			if (reader["tipoMidia"] != DBNull.Value)
				entidade.TipoMidia = reader["tipoMidia"].ToString();
			

		}		
		
	}
}
		