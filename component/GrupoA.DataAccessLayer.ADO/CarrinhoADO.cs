
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
	public partial class CarrinhoADO : ADOSuper, ICarrinhoDAL {
	
	    /// <summary>
        /// Método que persiste um Carrinho.
        /// </summary>
        /// <param name="entidade">Carrinho contendo os dados a serem persistidos.</param>	
		public void Inserir(Carrinho entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO Carrinho ");
			sbSQL.Append(" (usuarioId, dataHoraCriacao, carrinhoStatusId) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@usuarioId, @dataHoraCriacao, @carrinhoStatusId) ");											

			sbSQL.Append(" ; SET @carrinhoId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@carrinhoId", DbType.Int32, 8);

			_db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.Usuario.UsuarioId);

			_db.AddInParameter(command, "@dataHoraCriacao", DbType.DateTime, entidade.DataHoraCriacao);

			_db.AddInParameter(command, "@carrinhoStatusId", DbType.Int32, entidade.CarrinhoStatus.CarrinhoStatusId);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.CarrinhoId = Convert.ToInt32(_db.GetParameterValue(command, "@carrinhoId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um Carrinho.
        /// </summary>
        /// <param name="entidade">Carrinho contendo os dados a serem atualizados.</param>
		public void Atualizar(Carrinho entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE Carrinho SET ");
			sbSQL.Append(" usuarioId=@usuarioId, dataHoraCriacao=@dataHoraCriacao, carrinhoStatusId=@carrinhoStatusId ");
			sbSQL.Append(" WHERE carrinhoId=@carrinhoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@carrinhoId", DbType.Int32, entidade.CarrinhoId);
			_db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.Usuario.UsuarioId);
			_db.AddInParameter(command, "@dataHoraCriacao", DbType.DateTime, entidade.DataHoraCriacao);
			_db.AddInParameter(command, "@carrinhoStatusId", DbType.Int32, entidade.CarrinhoStatus.CarrinhoStatusId);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um Carrinho da base de dados.
        /// </summary>
        /// <param name="entidade">Carrinho a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(Carrinho entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM Carrinho ");
			sbSQL.Append("WHERE carrinhoId=@carrinhoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@carrinhoId", DbType.Int32, entidade.CarrinhoId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um Carrinho.
		/// </summary>
        /// <param name="entidade">Carrinho a ser carregado (somente o identificador é necessário).</param>
		/// <returns>Carrinho</returns>
		public Carrinho Carregar(int carrinhoId) {		
			Carrinho entidade = new Carrinho();
			entidade.CarrinhoId = carrinhoId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um Carrinho.
		/// </summary>
        /// <param name="entidade">Carrinho a ser carregado (somente o identificador é necessário).</param>
		/// <returns>Carrinho</returns>
		public Carrinho Carregar(Carrinho entidade) {		
		
			Carrinho entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM Carrinho WHERE carrinhoId=@carrinhoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@carrinhoId", DbType.Int32, entidade.CarrinhoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new Carrinho();
				PopulaCarrinho(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de Carrinho.
        /// </summary>
        /// <param name="entidade">CarrinhoItem relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Carrinho.</returns>
		public IEnumerable<Carrinho> Carregar(CarrinhoItem entidade)
		{		
			List<Carrinho> entidadesRetorno = new List<Carrinho>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Carrinho.* FROM Carrinho INNER JOIN CarrinhoItem ON Carrinho.carrinhoId=CarrinhoItem.carrinhoId WHERE CarrinhoItem.carrinhoItemId=@carrinhoItemId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@carrinhoItemId", DbType.Int32, entidade.CarrinhoItemId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Carrinho entidadeRetorno = new Carrinho();
                PopulaCarrinho(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Carrinho.
        /// </summary>
        /// <param name="entidade">Pedido relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Carrinho.</returns>
		public IEnumerable<Carrinho> Carregar(Pedido entidade)
		{		
			List<Carrinho> entidadesRetorno = new List<Carrinho>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Carrinho.* FROM Carrinho INNER JOIN Pedido ON Carrinho.carrinhoId=Pedido.carrinhoId WHERE Pedido.pedidoId=@pedidoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@pedidoId", DbType.Int32, entidade.PedidoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Carrinho entidadeRetorno = new Carrinho();
                PopulaCarrinho(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Carrinho.
        /// </summary>
        /// <param name="entidade">CarrinhoStatus relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Carrinho.</returns>
		public IEnumerable<Carrinho> Carregar(CarrinhoStatus entidade)
		{		
			List<Carrinho> entidadesRetorno = new List<Carrinho>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Carrinho.* FROM Carrinho WHERE Carrinho.carrinhoStatusId=@carrinhoStatusId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@carrinhoStatusId", DbType.Int32, entidade.CarrinhoStatusId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Carrinho entidadeRetorno = new Carrinho();
                PopulaCarrinho(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Carrinho.
        /// </summary>
        /// <param name="entidade">Usuario relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Carrinho.</returns>
		public IEnumerable<Carrinho> Carregar(Usuario entidade)
		{		
			List<Carrinho> entidadesRetorno = new List<Carrinho>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Carrinho.* FROM Carrinho WHERE Carrinho.usuarioId=@usuarioId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.UsuarioId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Carrinho entidadeRetorno = new Carrinho();
                PopulaCarrinho(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de Carrinho.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos Carrinho.</returns>
		public IEnumerable<Carrinho> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<Carrinho> entidadesRetorno = new List<Carrinho>();
			
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
				sbOrder.Append( " ORDER BY carrinhoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM Carrinho");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Carrinho WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Carrinho ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT Carrinho.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM Carrinho ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT Carrinho.* FROM Carrinho ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Carrinho entidadeRetorno = new Carrinho();
                PopulaCarrinho(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os Carrinho existentes na base de dados.
        /// </summary>
		public IEnumerable<Carrinho> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de Carrinho na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de Carrinho na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM Carrinho");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um Carrinho baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Carrinho a ser populado(.</param>
		public static void PopulaCarrinho(IDataReader reader, Carrinho entidade) 
		{						
			if (reader["carrinhoId"] != DBNull.Value)
				entidade.CarrinhoId = Convert.ToInt32(reader["carrinhoId"].ToString());
			
			if (reader["dataHoraCriacao"] != DBNull.Value)
				entidade.DataHoraCriacao = Convert.ToDateTime(reader["dataHoraCriacao"].ToString());
			
			if (reader["usuarioId"] != DBNull.Value) {
				entidade.Usuario = new Usuario();
				entidade.Usuario.UsuarioId = Convert.ToInt32(reader["usuarioId"].ToString());
			}

			if (reader["carrinhoStatusId"] != DBNull.Value) {
				entidade.CarrinhoStatus = new CarrinhoStatus();
				entidade.CarrinhoStatus.CarrinhoStatusId = Convert.ToInt32(reader["carrinhoStatusId"].ToString());
			}


		}		
		
	}
}
		