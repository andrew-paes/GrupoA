
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
	public partial class RevistaADO : ADOSuper, IRevistaDAL {
	
	    /// <summary>
        /// Método que persiste um Revista.
        /// </summary>
        /// <param name="entidade">Revista contendo os dados a serem persistidos.</param>	
		public void Inserir(Revista entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO Revista ");
			sbSQL.Append(" (revistaId, nomeRevista, periodicidade, descricaoRevista, publicoAlvo, ISSN) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@revistaId, @nomeRevista, @periodicidade, @descricaoRevista, @publicoAlvo, @ISSN) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@revistaId", DbType.Int32, entidade.RevistaId);

			_db.AddInParameter(command, "@nomeRevista", DbType.String, entidade.NomeRevista);

			_db.AddInParameter(command, "@periodicidade", DbType.Int32, entidade.Periodicidade);

			_db.AddInParameter(command, "@descricaoRevista", DbType.String, entidade.DescricaoRevista);

			if (entidade.PublicoAlvo != null ) 
				_db.AddInParameter(command, "@publicoAlvo", DbType.String, entidade.PublicoAlvo);
			else
				_db.AddInParameter(command, "@publicoAlvo", DbType.String, null);

			_db.AddInParameter(command, "@ISSN", DbType.String, entidade.ISSN);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um Revista.
        /// </summary>
        /// <param name="entidade">Revista contendo os dados a serem atualizados.</param>
		public void Atualizar(Revista entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE Revista SET ");
			sbSQL.Append(" nomeRevista=@nomeRevista, periodicidade=@periodicidade, descricaoRevista=@descricaoRevista, publicoAlvo=@publicoAlvo, ISSN=@ISSN ");
			sbSQL.Append(" WHERE revistaId=@revistaId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@revistaId", DbType.Int32, entidade.RevistaId);
			_db.AddInParameter(command, "@nomeRevista", DbType.String, entidade.NomeRevista);
			_db.AddInParameter(command, "@periodicidade", DbType.Int32, entidade.Periodicidade);
			_db.AddInParameter(command, "@descricaoRevista", DbType.String, entidade.DescricaoRevista);
			if (entidade.PublicoAlvo != null ) 
				_db.AddInParameter(command, "@publicoAlvo", DbType.String, entidade.PublicoAlvo);
			else
				_db.AddInParameter(command, "@publicoAlvo", DbType.String, null);
			_db.AddInParameter(command, "@ISSN", DbType.String, entidade.ISSN);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um Revista da base de dados.
        /// </summary>
        /// <param name="entidade">Revista a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(Revista entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM Revista ");
			sbSQL.Append("WHERE revistaId=@revistaId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@revistaId", DbType.Int32, entidade.RevistaId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um Revista.
		/// </summary>
        /// <param name="entidade">Revista a ser carregado (somente o identificador é necessário).</param>
		/// <returns>Revista</returns>
		public Revista Carregar(int revistaId) {		
			Revista entidade = new Revista();
			entidade.RevistaId = revistaId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um Revista.
		/// </summary>
        /// <param name="entidade">Revista a ser carregado (somente o identificador é necessário).</param>
		/// <returns>Revista</returns>
		public Revista Carregar(Revista entidade) {		
		
			Revista entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM Revista WHERE revistaId=@revistaId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@revistaId", DbType.Int32, entidade.RevistaId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new Revista();
				PopulaRevista(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de Revista.
        /// </summary>
        /// <param name="entidade">MidiaRevista relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Revista.</returns>
		public IEnumerable<Revista> Carregar(MidiaRevista entidade)
		{		
			List<Revista> entidadesRetorno = new List<Revista>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Revista.* FROM Revista INNER JOIN MidiaRevista ON Revista.revistaId=MidiaRevista.revistaId WHERE MidiaRevista.midiaRevistaId=@midiaRevistaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@midiaRevistaId", DbType.Int32, entidade.MidiaRevistaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Revista entidadeRetorno = new Revista();
                PopulaRevista(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Revista.
        /// </summary>
        /// <param name="entidade">Promocao relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Revista.</returns>
		public IEnumerable<Revista> Carregar(Promocao entidade)
		{		
			List<Revista> entidadesRetorno = new List<Revista>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Revista.* FROM Revista INNER JOIN PromocaoRevista ON Revista.revistaId=PromocaoRevista.revistaId WHERE PromocaoRevista.promocaoId=@promocaoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@promocaoId", DbType.Int32, entidade.PromocaoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Revista entidadeRetorno = new Revista();
                PopulaRevista(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Revista.
        /// </summary>
        /// <param name="entidade">Categoria relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Revista.</returns>
		public IEnumerable<Revista> Carregar(Categoria entidade)
		{		
			List<Revista> entidadesRetorno = new List<Revista>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Revista.* FROM Revista INNER JOIN RevistaAreaConhecimento ON Revista.revistaId=RevistaAreaConhecimento.revistaId WHERE RevistaAreaConhecimento.categoriaId=@categoriaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@categoriaId", DbType.Int32, entidade.CategoriaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Revista entidadeRetorno = new Revista();
                PopulaRevista(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Revista.
        /// </summary>
        /// <param name="entidade">RevistaAssinatura relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Revista.</returns>
		public IEnumerable<Revista> Carregar(RevistaAssinatura entidade)
		{		
			List<Revista> entidadesRetorno = new List<Revista>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Revista.* FROM Revista INNER JOIN RevistaAssinatura ON Revista.revistaId=RevistaAssinatura.revistaId WHERE RevistaAssinatura.revistaAssinaturaId=@revistaAssinaturaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@revistaAssinaturaId", DbType.Int32, entidade.RevistaAssinaturaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Revista entidadeRetorno = new Revista();
                PopulaRevista(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Revista.
        /// </summary>
        /// <param name="entidade">RevistaEdicao relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Revista.</returns>
		public IEnumerable<Revista> Carregar(RevistaEdicao entidade)
		{		
			List<Revista> entidadesRetorno = new List<Revista>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Revista.* FROM Revista INNER JOIN RevistaEdicao ON Revista.revistaId=RevistaEdicao.revistaId WHERE RevistaEdicao.revistaEdicaoId=@revistaEdicaoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@revistaEdicaoId", DbType.Int32, entidade.RevistaEdicaoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Revista entidadeRetorno = new Revista();
                PopulaRevista(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Revista.
        /// </summary>
        /// <param name="entidade">RevistaPagina relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Revista.</returns>
		public IEnumerable<Revista> Carregar(RevistaPagina entidade)
		{		
			List<Revista> entidadesRetorno = new List<Revista>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Revista.* FROM Revista INNER JOIN RevistaPagina ON Revista.revistaId=RevistaPagina.revistaId WHERE RevistaPagina.revistaPaginaId=@revistaPaginaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@revistaPaginaId", DbType.Int32, entidade.RevistaPaginaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Revista entidadeRetorno = new Revista();
                PopulaRevista(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Revista.
        /// </summary>
        /// <param name="entidade">RevistaSecao relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Revista.</returns>
		public IEnumerable<Revista> Carregar(RevistaSecao entidade)
		{		
			List<Revista> entidadesRetorno = new List<Revista>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Revista.* FROM Revista INNER JOIN RevistaSecao ON Revista.revistaId=RevistaSecao.revistaId WHERE RevistaSecao.revistaSecaoId=@revistaSecaoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@revistaSecaoId", DbType.Int32, entidade.RevistaSecaoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Revista entidadeRetorno = new Revista();
                PopulaRevista(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Revista.
        /// </summary>
        /// <param name="entidade">UsuarioRevista relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Revista.</returns>
		public IEnumerable<Revista> Carregar(UsuarioRevista entidade)
		{		
			List<Revista> entidadesRetorno = new List<Revista>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Revista.* FROM Revista INNER JOIN UsuarioRevista ON Revista.revistaId=UsuarioRevista.revistaId WHERE UsuarioRevista.usuarioRevistaId=@usuarioRevistaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@usuarioRevistaId", DbType.Int32, entidade.UsuarioRevistaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Revista entidadeRetorno = new Revista();
                PopulaRevista(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de Revista.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos Revista.</returns>
		public IEnumerable<Revista> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<Revista> entidadesRetorno = new List<Revista>();
			
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
				sbOrder.Append( " ORDER BY revistaId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM Revista");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Revista WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Revista ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT Revista.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM Revista ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT Revista.* FROM Revista ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Revista entidadeRetorno = new Revista();
                PopulaRevista(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os Revista existentes na base de dados.
        /// </summary>
		public IEnumerable<Revista> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de Revista na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de Revista na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM Revista");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um Revista baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Revista a ser populado(.</param>
		public static void PopulaRevista(IDataReader reader, Revista entidade) 
		{						
			if (reader["revistaId"] != DBNull.Value)
				entidade.RevistaId = Convert.ToInt32(reader["revistaId"].ToString());
			
			if (reader["nomeRevista"] != DBNull.Value)
				entidade.NomeRevista = reader["nomeRevista"].ToString();
			
			if (reader["periodicidade"] != DBNull.Value)
				entidade.Periodicidade = Convert.ToInt32(reader["periodicidade"].ToString());
			
			if (reader["descricaoRevista"] != DBNull.Value)
				entidade.DescricaoRevista = reader["descricaoRevista"].ToString();
			
			if (reader["publicoAlvo"] != DBNull.Value)
				entidade.PublicoAlvo = reader["publicoAlvo"].ToString();
			
			if (reader["ISSN"] != DBNull.Value)
				entidade.ISSN = reader["ISSN"].ToString();
			

		}		
		
	}
}
		