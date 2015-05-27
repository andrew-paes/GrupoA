
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
	public partial class CapituloADO : ADOSuper, ICapituloDAL {
	
	    /// <summary>
        /// Método que persiste um Capitulo.
        /// </summary>
        /// <param name="entidade">Capitulo contendo os dados a serem persistidos.</param>	
		public void Inserir(Capitulo entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO Capitulo ");
			sbSQL.Append(" (capituloId, nomeCapitulo, numeroPaginaCapitulo, resumoCapitulo, tituloId, codigoLegado) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@capituloId, @nomeCapitulo, @numeroPaginaCapitulo, @resumoCapitulo, @tituloId, @codigoLegado) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@capituloId", DbType.Int32, entidade.CapituloId);

			_db.AddInParameter(command, "@nomeCapitulo", DbType.String, entidade.NomeCapitulo);

			_db.AddInParameter(command, "@numeroPaginaCapitulo", DbType.Int32, entidade.NumeroPaginaCapitulo);

			if (entidade.ResumoCapitulo != null ) 
				_db.AddInParameter(command, "@resumoCapitulo", DbType.String, entidade.ResumoCapitulo);
			else
				_db.AddInParameter(command, "@resumoCapitulo", DbType.String, null);

			_db.AddInParameter(command, "@tituloId", DbType.Int32, entidade.Titulo.TituloId);

			if (entidade.CodigoLegado != null ) 
				_db.AddInParameter(command, "@codigoLegado", DbType.String, entidade.CodigoLegado);
			else
				_db.AddInParameter(command, "@codigoLegado", DbType.String, null);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um Capitulo.
        /// </summary>
        /// <param name="entidade">Capitulo contendo os dados a serem atualizados.</param>
		public void Atualizar(Capitulo entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE Capitulo SET ");
			sbSQL.Append(" nomeCapitulo=@nomeCapitulo, numeroPaginaCapitulo=@numeroPaginaCapitulo, resumoCapitulo=@resumoCapitulo, tituloId=@tituloId, codigoLegado=@codigoLegado ");
			sbSQL.Append(" WHERE capituloId=@capituloId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@capituloId", DbType.Int32, entidade.CapituloId);
			_db.AddInParameter(command, "@nomeCapitulo", DbType.String, entidade.NomeCapitulo);
			_db.AddInParameter(command, "@numeroPaginaCapitulo", DbType.Int32, entidade.NumeroPaginaCapitulo);
			if (entidade.ResumoCapitulo != null ) 
				_db.AddInParameter(command, "@resumoCapitulo", DbType.String, entidade.ResumoCapitulo);
			else
				_db.AddInParameter(command, "@resumoCapitulo", DbType.String, null);
			_db.AddInParameter(command, "@tituloId", DbType.Int32, entidade.Titulo.TituloId);
			if (entidade.CodigoLegado != null ) 
				_db.AddInParameter(command, "@codigoLegado", DbType.String, entidade.CodigoLegado);
			else
				_db.AddInParameter(command, "@codigoLegado", DbType.String, null);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um Capitulo da base de dados.
        /// </summary>
        /// <param name="entidade">Capitulo a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(Capitulo entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM Capitulo ");
			sbSQL.Append("WHERE capituloId=@capituloId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@capituloId", DbType.Int32, entidade.CapituloId);

								
			_db.ExecuteNonQuery(command);
		}
		

		/// <summary>
		/// Método que carrega um Capitulo.
		/// </summary>
        /// <param name="entidade">Capitulo a ser carregado (somente o identificador é necessário).</param>
		/// <returns>Capitulo</returns>
		public Capitulo Carregar(Capitulo entidade) {		
		
			Capitulo entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM Capitulo WHERE capituloId=@capituloId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@capituloId", DbType.Int32, entidade.CapituloId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new Capitulo();
				PopulaCapitulo(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		
		
		/// <summary>
		/// Método que carrega um Capitulo com suas dependências.
		/// </summary>
        /// <param name="entidade">Capitulo a ser carregado (somente o identificador é necessário).</param>
		/// <returns>Capitulo</returns>
		public Capitulo CarregarComDependencias(Capitulo entidade) {		
		
			Capitulo entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append("SELECT Capitulo.capituloId, Capitulo.nomeCapitulo, Capitulo.numeroPaginaCapitulo, Capitulo.resumoCapitulo, Capitulo.tituloId, Capitulo.codigoLegado");
			sbSQL.Append(", conteudoId, conteudoTipoId, dataHoraCadastro");
			sbSQL.Append(", capituloEletronicoId, tituloEletronicoId");
			sbSQL.Append(", capituloImpressoId, tituloImpressoId");
			sbSQL.Append(" FROM Capitulo");
			sbSQL.Append(" LEFT JOIN CapituloEletronico ON Capitulo.capituloId=CapituloEletronico.capituloEletronicoId");
			sbSQL.Append(" LEFT JOIN CapituloImpresso ON Capitulo.capituloId=CapituloImpresso.capituloImpressoId");
			sbSQL.Append(" INNER JOIN Conteudo ON Capitulo.capituloId=Conteudo.conteudoId");
			sbSQL.Append(" WHERE Capitulo.capituloId=@capituloId");
						
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@capituloId", DbType.Int32, entidade.CapituloId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new Capitulo();
				PopulaCapitulo(reader, entidadeRetorno);
				entidadeRetorno.CapituloEletronico = new CapituloEletronico();
				CapituloEletronicoADO.PopulaCapituloEletronico(reader, entidadeRetorno.CapituloEletronico);
				entidadeRetorno.CapituloImpresso = new CapituloImpresso();
				CapituloImpressoADO.PopulaCapituloImpresso(reader, entidadeRetorno.CapituloImpresso);
				entidadeRetorno.Conteudo = new Conteudo();
				ConteudoADO.PopulaConteudo(reader, entidadeRetorno.Conteudo);
			}
			reader.Close();
			
			return entidadeRetorno;
		}		


		/// <summary>
        /// Método que retorna uma coleção de Capitulo.
        /// </summary>
        /// <param name="entidade">Autor relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Capitulo.</returns>
		public IEnumerable<Capitulo> Carregar(Autor entidade)
		{		
			List<Capitulo> entidadesRetorno = new List<Capitulo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Capitulo.* FROM Capitulo INNER JOIN CapituloAutor ON Capitulo.capituloId=CapituloAutor.capituloId WHERE CapituloAutor.autorId=@autorId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@autorId", DbType.Int32, entidade.AutorId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Capitulo entidadeRetorno = new Capitulo();
                PopulaCapitulo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Capitulo.
        /// </summary>
        /// <param name="entidade">CapituloEletronico relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Capitulo.</returns>
		public IEnumerable<Capitulo> Carregar(CapituloEletronico entidade)
		{		
			List<Capitulo> entidadesRetorno = new List<Capitulo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Capitulo.* FROM Capitulo INNER JOIN CapituloEletronico ON Capitulo.capituloId=CapituloEletronico.capituloId WHERE CapituloEletronico.capituloEletronicoId=@capituloEletronicoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@capituloEletronicoId", DbType.Int32, entidade.CapituloEletronicoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Capitulo entidadeRetorno = new Capitulo();
                PopulaCapitulo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna um Capitulo.
        /// </summary>
        /// <param name="entidade">CapituloImpresso relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna um Capitulo.</returns>
		public Capitulo Carregar(CapituloImpresso entidade)
		{		
			Capitulo entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Capitulo.* FROM Capitulo INNER JOIN CapituloImpresso ON Capitulo.capituloId=CapituloImpresso.capituloId WHERE CapituloImpresso.capituloImpressoId=@capituloImpressoId");
		
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@capituloImpressoId", DbType.Int32, entidade.CapituloImpressoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            if (reader.Read())
            {
                entidadeRetorno = new Capitulo();
                PopulaCapitulo(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Capitulo.
        /// </summary>
        /// <param name="entidade">Titulo relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Capitulo.</returns>
		public IEnumerable<Capitulo> Carregar(Titulo entidade)
		{		
			List<Capitulo> entidadesRetorno = new List<Capitulo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Capitulo.* FROM Capitulo WHERE Capitulo.tituloId=@tituloId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@tituloId", DbType.Int32, entidade.TituloId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Capitulo entidadeRetorno = new Capitulo();
                PopulaCapitulo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de Capitulo.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos Capitulo.</returns>
		public IEnumerable<Capitulo> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<Capitulo> entidadesRetorno = new List<Capitulo>();
			
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
				sbOrder.Append( " ORDER BY capituloId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM Capitulo");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Capitulo WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Capitulo ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT Capitulo.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM Capitulo ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT Capitulo.* FROM Capitulo ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Capitulo entidadeRetorno = new Capitulo();
                PopulaCapitulo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os Capitulo existentes na base de dados.
        /// </summary>
		public IEnumerable<Capitulo> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de Capitulo na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de Capitulo na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM Capitulo");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um Capitulo baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Capitulo a ser populado(.</param>
		public static void PopulaCapitulo(IDataReader reader, Capitulo entidade) 
		{						
			if (reader["nomeCapitulo"] != DBNull.Value)
				entidade.NomeCapitulo = reader["nomeCapitulo"].ToString();
			
			if (reader["numeroPaginaCapitulo"] != DBNull.Value)
				entidade.NumeroPaginaCapitulo = Convert.ToInt32(reader["numeroPaginaCapitulo"].ToString());
			
			if (reader["resumoCapitulo"] != DBNull.Value)
				entidade.ResumoCapitulo = reader["resumoCapitulo"].ToString();
			
			if (reader["codigoLegado"] != DBNull.Value)
				entidade.CodigoLegado = reader["codigoLegado"].ToString();
			
			if (reader["capituloId"] != DBNull.Value) {
				entidade.CapituloId = Convert.ToInt32(reader["capituloId"].ToString());
			}

			if (reader["tituloId"] != DBNull.Value) {
				entidade.Titulo = new Titulo();
				entidade.Titulo.TituloId = Convert.ToInt32(reader["tituloId"].ToString());
			}


		}		
		
	}
}
		