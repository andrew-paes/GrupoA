using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using GrupoA.BusinessObject;
using GrupoA.FilterHelper;

namespace GrupoA.DataAccess.ADO
{
	public partial class PaginaPromocionalADO : ADOSuper, IPaginaPromocionalDAL {
	
	    /// <summary>
        /// Método que persiste um PaginaPromocional.
        /// </summary>
        /// <param name="entidade">PaginaPromocional contendo os dados a serem persistidos.</param>	
		public void Inserir(PaginaPromocional entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO PaginaPromocional ");
            sbSQL.Append(" (nomePagina, tituloPagina, subtituloPagina, resumo, linkMidia, arquivoId, larguraArquivo, alturaArquivo, ativo, targetBlank) ");
			sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@nomePagina, @tituloPagina, @subtituloPagina, @resumo, @linkMidia, @arquivoId, @larguraArquivo, @alturaArquivo, @ativo, @targetBlank) ");

            sbSQL.Append(" ; SET @paginaPromocionalId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddOutParameter(command, "@paginaPromocionalId", DbType.Int32, 8);

            _db.AddInParameter(command, "@nomePagina", DbType.String, entidade.NomePagina);

			_db.AddInParameter(command, "@tituloPagina", DbType.String, entidade.TituloPagina);

			if (entidade.SubtituloPagina != null ) 
				_db.AddInParameter(command, "@subtituloPagina", DbType.String, entidade.SubtituloPagina);
			else
				_db.AddInParameter(command, "@subtituloPagina", DbType.String, null);

			_db.AddInParameter(command, "@resumo", DbType.String, entidade.Resumo);

			if (entidade.LinkMidia != null ) 
				_db.AddInParameter(command, "@linkMidia", DbType.String, entidade.LinkMidia);
			else
				_db.AddInParameter(command, "@linkMidia", DbType.String, null);

			if (entidade.Arquivo != null ) 
				_db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.Arquivo.ArquivoId);
			else
				_db.AddInParameter(command, "@arquivoId", DbType.Int32, null);

            if (entidade.LarguraArquivo != null)
                _db.AddInParameter(command, "@larguraArquivo", DbType.Int32, entidade.LarguraArquivo);
            else
                _db.AddInParameter(command, "@larguraArquivo", DbType.Int32, null);

            if (entidade.AlturaArquivo != null)
                _db.AddInParameter(command, "@alturaArquivo", DbType.Int32, entidade.AlturaArquivo);
            else
                _db.AddInParameter(command, "@alturaArquivo", DbType.Int32, null);

            _db.AddInParameter(command, "@ativo", DbType.Boolean, entidade.Ativo);
            _db.AddInParameter(command, "@targetBlank", DbType.Boolean, entidade.TargetBlank);
						
			// Executa a query.
			_db.ExecuteNonQuery(command);

            entidade.PaginaPromocionalId = Convert.ToInt32(_db.GetParameterValue(command, "@paginaPromocionalId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um PaginaPromocional.
        /// </summary>
        /// <param name="entidade">PaginaPromocional contendo os dados a serem atualizados.</param>
		public void Atualizar(PaginaPromocional entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE PaginaPromocional SET ");
            sbSQL.Append(" nomePagina=@nomePagina, tituloPagina=@tituloPagina, subtituloPagina=@subtituloPagina, resumo=@resumo, linkMidia=@linkMidia, arquivoId=@arquivoId, larguraArquivo=@larguraArquivo, alturaArquivo=@alturaArquivo, ativo=@ativo, targetBlank=@targetBlank ");
			sbSQL.Append(" WHERE paginaPromocionalId=@paginaPromocionalId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@paginaPromocionalId", DbType.Int32, entidade.PaginaPromocionalId);
            _db.AddInParameter(command, "@nomePagina", DbType.String, entidade.NomePagina);
            _db.AddInParameter(command, "@tituloPagina", DbType.String, entidade.TituloPagina);
			if (entidade.SubtituloPagina != null ) 
				_db.AddInParameter(command, "@subtituloPagina", DbType.String, entidade.SubtituloPagina);
			else
				_db.AddInParameter(command, "@subtituloPagina", DbType.String, null);
			_db.AddInParameter(command, "@resumo", DbType.String, entidade.Resumo);
			if (entidade.LinkMidia != null ) 
				_db.AddInParameter(command, "@linkMidia", DbType.String, entidade.LinkMidia);
			else
				_db.AddInParameter(command, "@linkMidia", DbType.String, null);
			if (entidade.Arquivo != null ) 
				_db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.Arquivo.ArquivoId);
			else
				_db.AddInParameter(command, "@arquivoId", DbType.Int32, null);

            if (entidade.LarguraArquivo != null)
                _db.AddInParameter(command, "@larguraArquivo", DbType.Int32, entidade.LarguraArquivo);
            else
                _db.AddInParameter(command, "@larguraArquivo", DbType.Int32, null);

            if (entidade.AlturaArquivo != null)
                _db.AddInParameter(command, "@alturaArquivo", DbType.Int32, entidade.AlturaArquivo);
            else
                _db.AddInParameter(command, "@alturaArquivo", DbType.Int32, null);

            _db.AddInParameter(command, "@ativo", DbType.Boolean, entidade.Ativo);
            _db.AddInParameter(command, "@targetBlank", DbType.Boolean, entidade.TargetBlank);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um PaginaPromocional da base de dados.
        /// </summary>
        /// <param name="entidade">PaginaPromocional a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(PaginaPromocional entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM PaginaPromocional ");
			sbSQL.Append("WHERE paginaPromocionalId=@paginaPromocionalId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@paginaPromocionalId", DbType.Int32, entidade.PaginaPromocionalId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um PaginaPromocional.
		/// </summary>
        /// <param name="entidade">PaginaPromocional a ser carregado (somente o identificador é necessário).</param>
		/// <returns>PaginaPromocional</returns>
		public PaginaPromocional Carregar(int paginaPromocionalId) {		
			PaginaPromocional entidade = new PaginaPromocional();
			entidade.PaginaPromocionalId = paginaPromocionalId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um PaginaPromocional.
		/// </summary>
        /// <param name="entidade">PaginaPromocional a ser carregado (somente o identificador é necessário).</param>
		/// <returns>PaginaPromocional</returns>
		public PaginaPromocional Carregar(PaginaPromocional entidade) {		
		
			PaginaPromocional entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM PaginaPromocional WHERE paginaPromocionalId=@paginaPromocionalId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@paginaPromocionalId", DbType.Int32, entidade.PaginaPromocionalId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new PaginaPromocional();
				PopulaPaginaPromocional(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de PaginaPromocional.
        /// </summary>
        /// <param name="entidade">Arquivo relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de PaginaPromocional.</returns>
		public IEnumerable<PaginaPromocional> Carregar(Arquivo entidade)
		{		
			List<PaginaPromocional> entidadesRetorno = new List<PaginaPromocional>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT PaginaPromocional.* FROM PaginaPromocional WHERE PaginaPromocional.arquivoId=@arquivoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.ArquivoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                PaginaPromocional entidadeRetorno = new PaginaPromocional();
                PopulaPaginaPromocional(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de PaginaPromocional.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos PaginaPromocional.</returns>
		public IEnumerable<PaginaPromocional> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<PaginaPromocional> entidadesRetorno = new List<PaginaPromocional>();
			
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
				sbOrder.Append( " ORDER BY paginaPromocionalId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM PaginaPromocional");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM PaginaPromocional WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM PaginaPromocional ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT PaginaPromocional.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM PaginaPromocional ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT PaginaPromocional.* FROM PaginaPromocional ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                PaginaPromocional entidadeRetorno = new PaginaPromocional();
                PopulaPaginaPromocional(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os PaginaPromocional existentes na base de dados.
        /// </summary>
		public IEnumerable<PaginaPromocional> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de PaginaPromocional na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de PaginaPromocional na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM PaginaPromocional");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um PaginaPromocional baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">PaginaPromocional a ser populado(.</param>
		public static void PopulaPaginaPromocional(IDataReader reader, PaginaPromocional entidade) 
		{						
			if (reader["paginaPromocionalId"] != DBNull.Value)
				entidade.PaginaPromocionalId = Convert.ToInt32(reader["paginaPromocionalId"].ToString());

            if (reader["nomePagina"] != DBNull.Value)
                entidade.NomePagina = reader["nomePagina"].ToString();

			if (reader["tituloPagina"] != DBNull.Value)
				entidade.TituloPagina = reader["tituloPagina"].ToString();
			
			if (reader["subtituloPagina"] != DBNull.Value)
				entidade.SubtituloPagina = reader["subtituloPagina"].ToString();
			
			if (reader["resumo"] != DBNull.Value)
				entidade.Resumo = reader["resumo"].ToString();
			
			if (reader["linkMidia"] != DBNull.Value)
				entidade.LinkMidia = reader["linkMidia"].ToString();
			
			if (reader["arquivoId"] != DBNull.Value) {
				entidade.Arquivo = new Arquivo();
				entidade.Arquivo.ArquivoId = Convert.ToInt32(reader["arquivoId"].ToString());
			}

            if (reader["alturaArquivo"] != DBNull.Value)
                entidade.AlturaArquivo = Convert.ToInt32(reader["alturaArquivo"].ToString());

            if (reader["larguraArquivo"] != DBNull.Value)
                entidade.LarguraArquivo = Convert.ToInt32(reader["larguraArquivo"].ToString());

            if (reader["ativo"] != DBNull.Value)
                entidade.Ativo = Convert.ToBoolean(reader["ativo"].ToString());

            if (reader["targetBlank"] != DBNull.Value)
                entidade.TargetBlank = Convert.ToBoolean(reader["targetBlank"].ToString());
		}		
	}
}
		