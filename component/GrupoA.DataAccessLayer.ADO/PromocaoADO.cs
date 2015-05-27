
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
	public partial class PromocaoADO : ADOSuper, IPromocaoDAL {
	
	    /// <summary>
        /// Método que persiste um Promocao.
        /// </summary>
        /// <param name="entidade">Promocao contendo os dados a serem persistidos.</param>	
		public void Inserir(Promocao entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO Promocao ");
			sbSQL.Append(" (nomePromocao, codigoPromocao, dataHoraInicio, dataHoraFim, aplicaAutomaticamente, promocaoTipoId, ativa, descricaoPromocao, numeroMaximoCupom, origemSistema) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@nomePromocao, @codigoPromocao, @dataHoraInicio, @dataHoraFim, @aplicaAutomaticamente, @promocaoTipoId, @ativa, @descricaoPromocao, @numeroMaximoCupom, @origemSistema) ");											

			sbSQL.Append(" ; SET @promocaoId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@promocaoId", DbType.Int32, 8);

			_db.AddInParameter(command, "@nomePromocao", DbType.String, entidade.NomePromocao);

			if (entidade.CodigoPromocao != null ) 
				_db.AddInParameter(command, "@codigoPromocao", DbType.String, entidade.CodigoPromocao);
			else
				_db.AddInParameter(command, "@codigoPromocao", DbType.String, null);

			_db.AddInParameter(command, "@dataHoraInicio", DbType.DateTime, entidade.DataHoraInicio);

			_db.AddInParameter(command, "@dataHoraFim", DbType.DateTime, entidade.DataHoraFim);

			_db.AddInParameter(command, "@aplicaAutomaticamente", DbType.Int32, entidade.AplicaAutomaticamente);

			_db.AddInParameter(command, "@promocaoTipoId", DbType.Int32, entidade.PromocaoTipo.PromocaoTipoId);

			_db.AddInParameter(command, "@ativa", DbType.Int32, entidade.Ativa);

			if (entidade.DescricaoPromocao != null ) 
				_db.AddInParameter(command, "@descricaoPromocao", DbType.String, entidade.DescricaoPromocao);
			else
				_db.AddInParameter(command, "@descricaoPromocao", DbType.String, null);

			if (entidade.NumeroMaximoCupom != null ) 
				_db.AddInParameter(command, "@numeroMaximoCupom", DbType.Int32, entidade.NumeroMaximoCupom);
			else
				_db.AddInParameter(command, "@numeroMaximoCupom", DbType.Int32, null);

			_db.AddInParameter(command, "@origemSistema", DbType.Int32, entidade.OrigemSistema);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.PromocaoId = Convert.ToInt32(_db.GetParameterValue(command, "@promocaoId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um Promocao.
        /// </summary>
        /// <param name="entidade">Promocao contendo os dados a serem atualizados.</param>
		public void Atualizar(Promocao entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE Promocao SET ");
			sbSQL.Append(" nomePromocao=@nomePromocao, codigoPromocao=@codigoPromocao, dataHoraInicio=@dataHoraInicio, dataHoraFim=@dataHoraFim, aplicaAutomaticamente=@aplicaAutomaticamente, promocaoTipoId=@promocaoTipoId, ativa=@ativa, descricaoPromocao=@descricaoPromocao, numeroMaximoCupom=@numeroMaximoCupom, origemSistema=@origemSistema ");
			sbSQL.Append(" WHERE promocaoId=@promocaoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@promocaoId", DbType.Int32, entidade.PromocaoId);
			_db.AddInParameter(command, "@nomePromocao", DbType.String, entidade.NomePromocao);
			if (entidade.CodigoPromocao != null ) 
				_db.AddInParameter(command, "@codigoPromocao", DbType.String, entidade.CodigoPromocao);
			else
				_db.AddInParameter(command, "@codigoPromocao", DbType.String, null);
			_db.AddInParameter(command, "@dataHoraInicio", DbType.DateTime, entidade.DataHoraInicio);
			_db.AddInParameter(command, "@dataHoraFim", DbType.DateTime, entidade.DataHoraFim);
			_db.AddInParameter(command, "@aplicaAutomaticamente", DbType.Int32, entidade.AplicaAutomaticamente);
			_db.AddInParameter(command, "@promocaoTipoId", DbType.Int32, entidade.PromocaoTipo.PromocaoTipoId);
			_db.AddInParameter(command, "@ativa", DbType.Int32, entidade.Ativa);
			if (entidade.DescricaoPromocao != null ) 
				_db.AddInParameter(command, "@descricaoPromocao", DbType.String, entidade.DescricaoPromocao);
			else
				_db.AddInParameter(command, "@descricaoPromocao", DbType.String, null);
			if (entidade.NumeroMaximoCupom != null ) 
				_db.AddInParameter(command, "@numeroMaximoCupom", DbType.Int32, entidade.NumeroMaximoCupom);
			else
				_db.AddInParameter(command, "@numeroMaximoCupom", DbType.Int32, null);
			_db.AddInParameter(command, "@origemSistema", DbType.Int32, entidade.OrigemSistema);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um Promocao da base de dados.
        /// </summary>
        /// <param name="entidade">Promocao a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(Promocao entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM Promocao ");
			sbSQL.Append("WHERE promocaoId=@promocaoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@promocaoId", DbType.Int32, entidade.PromocaoId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um Promocao.
		/// </summary>
        /// <param name="entidade">Promocao a ser carregado (somente o identificador é necessário).</param>
		/// <returns>Promocao</returns>
		public Promocao Carregar(int promocaoId) {		
			Promocao entidade = new Promocao();
			entidade.PromocaoId = promocaoId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um Promocao.
		/// </summary>
        /// <param name="entidade">Promocao a ser carregado (somente o identificador é necessário).</param>
		/// <returns>Promocao</returns>
		public Promocao Carregar(Promocao entidade) {		
		
			Promocao entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM Promocao WHERE promocaoId=@promocaoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@promocaoId", DbType.Int32, entidade.PromocaoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new Promocao();
				PopulaPromocao(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de Promocao.
        /// </summary>
        /// <param name="entidade">PedidoItemPromocao relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Promocao.</returns>
		public IEnumerable<Promocao> Carregar(PedidoItemPromocao entidade)
		{		
			List<Promocao> entidadesRetorno = new List<Promocao>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Promocao.* FROM Promocao INNER JOIN PedidoItemPromocao ON Promocao.promocaoId=PedidoItemPromocao.promocaoId WHERE PedidoItemPromocao.pedidoItemPromocaoId=@pedidoItemPromocaoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@pedidoItemPromocaoId", DbType.Int32, entidade.PedidoItemPromocaoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Promocao entidadeRetorno = new Promocao();
                PopulaPromocao(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Promocao.
        /// </summary>
        /// <param name="entidade">Pedido relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Promocao.</returns>
		public IEnumerable<Promocao> Carregar(Pedido entidade)
		{		
			List<Promocao> entidadesRetorno = new List<Promocao>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Promocao.* FROM Promocao INNER JOIN PedidoPromocaoCarrinho ON Promocao.promocaoId=PedidoPromocaoCarrinho.promocaoId WHERE PedidoPromocaoCarrinho.pedidoId=@pedidoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@pedidoId", DbType.Int32, entidade.PedidoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Promocao entidadeRetorno = new Promocao();
                PopulaPromocao(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Promocao.
        /// </summary>
        /// <param name="entidade">Categoria relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Promocao.</returns>
		public IEnumerable<Promocao> Carregar(Categoria entidade)
		{		
			List<Promocao> entidadesRetorno = new List<Promocao>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Promocao.* FROM Promocao INNER JOIN PromocaoCategoria ON Promocao.promocaoId=PromocaoCategoria.promocaoId WHERE PromocaoCategoria.categoriaId=@categoriaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@categoriaId", DbType.Int32, entidade.CategoriaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Promocao entidadeRetorno = new Promocao();
                PopulaPromocao(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Promocao.
        /// </summary>
        /// <param name="entidade">PromocaoCupom relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Promocao.</returns>
		public IEnumerable<Promocao> Carregar(PromocaoCupom entidade)
		{		
			List<Promocao> entidadesRetorno = new List<Promocao>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Promocao.* FROM Promocao INNER JOIN PromocaoCupom ON Promocao.promocaoId=PromocaoCupom.promocaoId WHERE PromocaoCupom.promocaoCupomId=@promocaoCupomId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@promocaoCupomId", DbType.Int32, entidade.PromocaoCupomId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Promocao entidadeRetorno = new Promocao();
                PopulaPromocao(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Promocao.
        /// </summary>
        /// <param name="entidade">PromocaoFaixa relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Promocao.</returns>
		public IEnumerable<Promocao> Carregar(PromocaoFaixa entidade)
		{		
			List<Promocao> entidadesRetorno = new List<Promocao>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Promocao.* FROM Promocao INNER JOIN PromocaoFaixa ON Promocao.promocaoId=PromocaoFaixa.promocaoId WHERE PromocaoFaixa.promocaoFaixaId=@promocaoFaixaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@promocaoFaixaId", DbType.Int32, entidade.PromocaoFaixaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Promocao entidadeRetorno = new Promocao();
                PopulaPromocao(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Promocao.
        /// </summary>
        /// <param name="entidade">Perfil relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Promocao.</returns>
		public IEnumerable<Promocao> Carregar(Perfil entidade)
		{		
			List<Promocao> entidadesRetorno = new List<Promocao>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Promocao.* FROM Promocao INNER JOIN PromocaoPerfil ON Promocao.promocaoId=PromocaoPerfil.promocaoId WHERE PromocaoPerfil.perfilId=@perfilId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@perfilId", DbType.Int32, entidade.PerfilId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Promocao entidadeRetorno = new Promocao();
                PopulaPromocao(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Promocao.
        /// </summary>
        /// <param name="entidade">Produto relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Promocao.</returns>
		public IEnumerable<Promocao> Carregar(Produto entidade)
		{		
			List<Promocao> entidadesRetorno = new List<Promocao>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Promocao.* FROM Promocao INNER JOIN PromocaoProduto ON Promocao.promocaoId=PromocaoProduto.promocaoId WHERE PromocaoProduto.produtoId=@produtoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.ProdutoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Promocao entidadeRetorno = new Promocao();
                PopulaPromocao(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Promocao.
        /// </summary>
        /// <param name="entidade">ProdutoTipo relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Promocao.</returns>
		public IEnumerable<Promocao> Carregar(ProdutoTipo entidade)
		{		
			List<Promocao> entidadesRetorno = new List<Promocao>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Promocao.* FROM Promocao INNER JOIN PromocaoProdutoTipo ON Promocao.promocaoId=PromocaoProdutoTipo.promocaoId WHERE PromocaoProdutoTipo.produtoTipoId=@produtoTipoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@produtoTipoId", DbType.Int32, entidade.ProdutoTipoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Promocao entidadeRetorno = new Promocao();
                PopulaPromocao(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Promocao.
        /// </summary>
        /// <param name="entidade">Revista relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Promocao.</returns>
		public IEnumerable<Promocao> Carregar(Revista entidade)
		{		
			List<Promocao> entidadesRetorno = new List<Promocao>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Promocao.* FROM Promocao INNER JOIN PromocaoRevista ON Promocao.promocaoId=PromocaoRevista.promocaoId WHERE PromocaoRevista.revistaId=@revistaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@revistaId", DbType.Int32, entidade.RevistaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Promocao entidadeRetorno = new Promocao();
                PopulaPromocao(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Promocao.
        /// </summary>
        /// <param name="entidade">Usuario relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Promocao.</returns>
		public IEnumerable<Promocao> Carregar(Usuario entidade)
		{		
			List<Promocao> entidadesRetorno = new List<Promocao>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Promocao.* FROM Promocao INNER JOIN PromocaoUsuario ON Promocao.promocaoId=PromocaoUsuario.promocaoId WHERE PromocaoUsuario.usuarioId=@usuarioId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.UsuarioId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Promocao entidadeRetorno = new Promocao();
                PopulaPromocao(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Promocao.
        /// </summary>
        /// <param name="entidade">PromocaoTipo relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Promocao.</returns>
		public IEnumerable<Promocao> Carregar(PromocaoTipo entidade)
		{		
			List<Promocao> entidadesRetorno = new List<Promocao>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Promocao.* FROM Promocao WHERE Promocao.promocaoTipoId=@promocaoTipoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@promocaoTipoId", DbType.Int32, entidade.PromocaoTipoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Promocao entidadeRetorno = new Promocao();
                PopulaPromocao(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de Promocao.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos Promocao.</returns>
		public IEnumerable<Promocao> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<Promocao> entidadesRetorno = new List<Promocao>();
			
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
				sbOrder.Append( " ORDER BY promocaoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM Promocao");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Promocao WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Promocao ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT Promocao.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM Promocao ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT Promocao.* FROM Promocao ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Promocao entidadeRetorno = new Promocao();
                PopulaPromocao(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os Promocao existentes na base de dados.
        /// </summary>
		public IEnumerable<Promocao> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de Promocao na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de Promocao na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM Promocao");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um Promocao baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Promocao a ser populado(.</param>
		public static void PopulaPromocao(IDataReader reader, Promocao entidade) 
		{						
			if (reader["promocaoId"] != DBNull.Value)
				entidade.PromocaoId = Convert.ToInt32(reader["promocaoId"].ToString());
			
			if (reader["nomePromocao"] != DBNull.Value)
				entidade.NomePromocao = reader["nomePromocao"].ToString();
			
			if (reader["codigoPromocao"] != DBNull.Value)
				entidade.CodigoPromocao = reader["codigoPromocao"].ToString();
			
			if (reader["dataHoraInicio"] != DBNull.Value)
				entidade.DataHoraInicio = Convert.ToDateTime(reader["dataHoraInicio"].ToString());
			
			if (reader["dataHoraFim"] != DBNull.Value)
				entidade.DataHoraFim = Convert.ToDateTime(reader["dataHoraFim"].ToString());
			
			if (reader["aplicaAutomaticamente"] != DBNull.Value)
				entidade.AplicaAutomaticamente = Convert.ToBoolean(reader["aplicaAutomaticamente"].ToString());
			
			if (reader["ativa"] != DBNull.Value)
				entidade.Ativa = Convert.ToBoolean(reader["ativa"].ToString());
			
			if (reader["descricaoPromocao"] != DBNull.Value)
				entidade.DescricaoPromocao = reader["descricaoPromocao"].ToString();
			
			if (reader["numeroMaximoCupom"] != DBNull.Value)
				entidade.NumeroMaximoCupom = Convert.ToInt32(reader["numeroMaximoCupom"].ToString());
			
			if (reader["origemSistema"] != DBNull.Value)
				entidade.OrigemSistema = Convert.ToBoolean(reader["origemSistema"].ToString());
			
			if (reader["promocaoTipoId"] != DBNull.Value) {
				entidade.PromocaoTipo = new PromocaoTipo();
				entidade.PromocaoTipo.PromocaoTipoId = Convert.ToInt32(reader["promocaoTipoId"].ToString());
			}


		}		
		
	}
}
		