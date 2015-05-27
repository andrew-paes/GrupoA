
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
	public partial class EnderecoADO : ADOSuper, IEnderecoDAL
	{

		/// <summary>
		/// Método que persiste um Endereco.
		/// </summary>
		/// <param name="entidade">Endereco contendo os dados a serem persistidos.</param>	
		public void Inserir(Endereco entidade)
		{
			StringBuilder sbSQL = new StringBuilder();
			DbCommand command;

			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO Endereco ");
			sbSQL.Append(" (municipioId, enderecoTipoId, usuarioId, preferencialParaEntrega, logradouro, bairro, cep, complemento, numero, nomeEndereco) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@municipioId, @enderecoTipoId, @usuarioId, @preferencialParaEntrega, @logradouro, @bairro, @cep, @complemento, @numero, @nomeEndereco) ");

			sbSQL.Append(" ; SET @enderecoId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());

			_db.AddOutParameter(command, "@enderecoId", DbType.Int32, 8);

			_db.AddInParameter(command, "@municipioId", DbType.Int32, entidade.Municipio.MunicipioId);

			_db.AddInParameter(command, "@enderecoTipoId", DbType.Int32, entidade.EnderecoTipo.EnderecoTipoId);

			_db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.Usuario.UsuarioId);

			_db.AddInParameter(command, "@preferencialParaEntrega", DbType.Int32, entidade.PreferencialParaEntrega);

			_db.AddInParameter(command, "@logradouro", DbType.String, entidade.Logradouro);

			_db.AddInParameter(command, "@bairro", DbType.String, entidade.Bairro);

			_db.AddInParameter(command, "@cep", DbType.String, entidade.Cep);

			if (entidade.Complemento != null)
				_db.AddInParameter(command, "@complemento", DbType.String, entidade.Complemento);
			else
				_db.AddInParameter(command, "@complemento", DbType.String, null);

			_db.AddInParameter(command, "@numero", DbType.String, entidade.Numero);

			if (entidade.NomeEndereco != null)
				_db.AddInParameter(command, "@nomeEndereco", DbType.String, entidade.NomeEndereco);
			else
				_db.AddInParameter(command, "@nomeEndereco", DbType.String, null);


			// Executa a query.
			_db.ExecuteNonQuery(command);

			entidade.EnderecoId = Convert.ToInt32(_db.GetParameterValue(command, "@enderecoId"));

		}

		/// <summary>
		/// Método que atualiza os dados de um Endereco.
		/// </summary>
		/// <param name="entidade">Endereco contendo os dados a serem atualizados.</param>
		public void Atualizar(Endereco entidade)
		{

			StringBuilder sbSQL = new StringBuilder();
			DbCommand command;

			// Monta a string de atualização.
			sbSQL.Append(" UPDATE Endereco SET ");
			sbSQL.Append(" municipioId=@municipioId, enderecoTipoId=@enderecoTipoId, usuarioId=@usuarioId, preferencialParaEntrega=@preferencialParaEntrega, logradouro=@logradouro, bairro=@bairro, cep=@cep, complemento=@complemento, numero=@numero, nomeEndereco=@nomeEndereco ");
			sbSQL.Append(" WHERE enderecoId=@enderecoId ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());

			// Parâmetros
			_db.AddInParameter(command, "@enderecoId", DbType.Int32, entidade.EnderecoId);
			_db.AddInParameter(command, "@municipioId", DbType.Int32, entidade.Municipio.MunicipioId);
			_db.AddInParameter(command, "@enderecoTipoId", DbType.Int32, entidade.EnderecoTipo.EnderecoTipoId);
			_db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.Usuario.UsuarioId);
			_db.AddInParameter(command, "@preferencialParaEntrega", DbType.Int32, entidade.PreferencialParaEntrega);
			_db.AddInParameter(command, "@logradouro", DbType.String, entidade.Logradouro);
			_db.AddInParameter(command, "@bairro", DbType.String, entidade.Bairro);
			_db.AddInParameter(command, "@cep", DbType.String, entidade.Cep);
			if (entidade.Complemento != null)
				_db.AddInParameter(command, "@complemento", DbType.String, entidade.Complemento);
			else
				_db.AddInParameter(command, "@complemento", DbType.String, null);
			_db.AddInParameter(command, "@numero", DbType.String, entidade.Numero);
			if (entidade.NomeEndereco != null)
				_db.AddInParameter(command, "@nomeEndereco", DbType.String, entidade.NomeEndereco);
			else
				_db.AddInParameter(command, "@nomeEndereco", DbType.String, null);

			// Executa a query.
			_db.ExecuteNonQuery(command);

		}

		/// <summary>
		/// Método que remove um Endereco da base de dados.
		/// </summary>
		/// <param name="entidade">Endereco a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(Endereco entidade)
		{
			StringBuilder sbSQL = new StringBuilder();
			DbCommand command;

			sbSQL.Append("DELETE FROM Endereco ");
			sbSQL.Append("WHERE enderecoId=@enderecoId ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());

			_db.AddInParameter(command, "@enderecoId", DbType.Int32, entidade.EnderecoId);


			_db.ExecuteNonQuery(command);
		}

		/// <summary>
		/// Método que carrega um Endereco.
		/// </summary>
		/// <param name="entidade">Endereco a ser carregado (somente o identificador é necessário).</param>
		/// <returns>Endereco</returns>
		public Endereco Carregar(int enderecoId)
		{
			Endereco entidade = new Endereco();
			entidade.EnderecoId = enderecoId;
			return Carregar(entidade);

		}


		/// <summary>
		/// Método que carrega um Endereco.
		/// </summary>
		/// <param name="entidade">Endereco a ser carregado (somente o identificador é necessário).</param>
		/// <returns>Endereco</returns>
		public Endereco Carregar(Endereco entidade)
		{

			Endereco entidadeRetorno = null;

			StringBuilder sbSQL = new StringBuilder();

			sbSQL.Append("SELECT * FROM Endereco WHERE enderecoId=@enderecoId");

			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

			_db.AddInParameter(command, "@enderecoId", DbType.Int32, entidade.EnderecoId);

			IDataReader reader = _db.ExecuteReader(command);

			if (reader.Read())
			{
				entidadeRetorno = new Endereco();
				PopulaEndereco(reader, entidadeRetorno);
			}
			reader.Close();

			return entidadeRetorno;
		}



		/// <summary>
		/// Método que retorna uma coleção de Endereco.
		/// </summary>
		/// <param name="entidade">EnderecoTipo relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Endereco.</returns>
		public IEnumerable<Endereco> Carregar(EnderecoTipo entidade)
		{
			List<Endereco> entidadesRetorno = new List<Endereco>();

			StringBuilder sbSQL = new StringBuilder();

			sbSQL.Append("SELECT Endereco.* FROM Endereco WHERE Endereco.enderecoTipoId=@enderecoTipoId");


			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@enderecoTipoId", DbType.Int32, entidade.EnderecoTipoId);

			IDataReader reader = _db.ExecuteReader(command);

			while (reader.Read())
			{
				Endereco entidadeRetorno = new Endereco();
				PopulaEndereco(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
			}
			reader.Close();

			return entidadesRetorno;

		}

		/// <summary>
		/// Método que retorna uma coleção de Endereco.
		/// </summary>
		/// <param name="entidade">Municipio relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Endereco.</returns>
		public IEnumerable<Endereco> Carregar(Municipio entidade)
		{
			List<Endereco> entidadesRetorno = new List<Endereco>();

			StringBuilder sbSQL = new StringBuilder();

			sbSQL.Append("SELECT Endereco.* FROM Endereco WHERE Endereco.municipioId=@municipioId");


			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@municipioId", DbType.Int32, entidade.MunicipioId);

			IDataReader reader = _db.ExecuteReader(command);

			while (reader.Read())
			{
				Endereco entidadeRetorno = new Endereco();
				PopulaEndereco(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
			}
			reader.Close();

			return entidadesRetorno;

		}

		/// <summary>
		/// Método que retorna uma coleção de Endereco.
		/// </summary>
		/// <param name="entidade">Usuario relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Endereco.</returns>
		public IEnumerable<Endereco> Carregar(Usuario entidade)
		{
			List<Endereco> entidadesRetorno = new List<Endereco>();

			StringBuilder sbSQL = new StringBuilder();

			sbSQL.Append("SELECT Endereco.* FROM Endereco WHERE Endereco.usuarioId=@usuarioId");


			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.UsuarioId);

			IDataReader reader = _db.ExecuteReader(command);

			while (reader.Read())
			{
				Endereco entidadeRetorno = new Endereco();
				PopulaEndereco(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
			}
			reader.Close();

			return entidadesRetorno;

		}


		/// <summary>
		/// Método que retorna uma coleção de Endereco.
		/// </summary>
		/// <param name="registrosPagina">Número máximo de registros na página.</param>
		/// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
		/// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
		/// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos Endereco.</returns>
		public IEnumerable<Endereco> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
		{

			List<Endereco> entidadesRetorno = new List<Endereco>();

			StringBuilder sbSQL = new StringBuilder();
			StringBuilder sbWhere = new StringBuilder();
			StringBuilder sbOrder = new StringBuilder();
			DbCommand command;
			IDataReader reader;

			// Monta o "OrderBy"
			if (ordemColunas != null)
			{
				for (int i = 0; i < ordemColunas.Length; i++)
				{
					if (sbOrder.Length > 0) { sbOrder.Append(", "); }
					sbOrder.Append(ordemColunas[i] + " " + ordemSentidos[i]);
				}
				if (sbOrder.Length > 0) { sbOrder.Insert(0, " ORDER BY "); }
			}
			else
			{
				sbOrder.Append(" ORDER BY enderecoId");
			}


			if (registrosPagina > 0)
			{

				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM Endereco");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Endereco WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Endereco ORDER BY " + orderBy + ")");				
				//}	
				sbSQL.Append("SELECT * FROM ( ");
				sbSQL.Append("SELECT Endereco.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM Endereco ");
				if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

			}
			else
			{
				sbSQL.Append("SELECT Endereco.* FROM Endereco ");
				if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
				if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
			}

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);

			while (reader.Read())
			{
				Endereco entidadeRetorno = new Endereco();
				PopulaEndereco(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
			}
			reader.Close();

			return entidadesRetorno;

		}

		/// <summary>
		/// Método que retorna todas os Endereco existentes na base de dados.
		/// </summary>
		public IEnumerable<Endereco> CarregarTodos()
		{
			return CarregarTodos(0, 0, null, null, null);
		}

		/// <summary>
		/// Método que retorna o total de Endereco na base de dados.
		/// </summary>
		/// <returns></returns>
		public int TotalRegistros()
		{
			return TotalRegistros(null);
		}

		/// <summary>
		/// Método que retorna o total de Endereco na base de dados, aceita filtro.
		/// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro)
		{
			StringBuilder sbSQL = new StringBuilder();

			sbSQL.Append("SELECT COUNT(*) AS Total FROM Endereco");

			if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
				sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");

			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

			// Executa a query.

			int resultado = (int)_db.ExecuteScalar(command);


			return resultado;
		}

		/// <summary>
		/// Método que retorna popula um Endereco baseado nos dados de um DataReader.
		/// </summary>
		/// <param name="reader">IDataReader contendo os dados da consulta.</param>
		/// <param name="entidade">Endereco a ser populado(.</param>
		public static void PopulaEndereco(IDataReader reader, Endereco entidade)
		{
			if (reader["enderecoId"] != DBNull.Value)
				entidade.EnderecoId = Convert.ToInt32(reader["enderecoId"].ToString());

			if (reader["preferencialParaEntrega"] != DBNull.Value)
				entidade.PreferencialParaEntrega = Convert.ToBoolean(reader["preferencialParaEntrega"].ToString());

			if (reader["logradouro"] != DBNull.Value)
				entidade.Logradouro = reader["logradouro"].ToString();

			if (reader["bairro"] != DBNull.Value)
				entidade.Bairro = reader["bairro"].ToString();

			if (reader["cep"] != DBNull.Value)
				entidade.Cep = reader["cep"].ToString();

			if (reader["complemento"] != DBNull.Value)
				entidade.Complemento = reader["complemento"].ToString();

			if (reader["numero"] != DBNull.Value)
				entidade.Numero = reader["numero"].ToString();

			if (reader["nomeEndereco"] != DBNull.Value)
				entidade.NomeEndereco = reader["nomeEndereco"].ToString();

			if (reader["municipioId"] != DBNull.Value)
			{
				entidade.Municipio = new Municipio();
				entidade.Municipio.MunicipioId = Convert.ToInt32(reader["municipioId"].ToString());
			}

			if (reader["enderecoTipoId"] != DBNull.Value)
			{
				entidade.EnderecoTipo = new EnderecoTipo();
				entidade.EnderecoTipo.EnderecoTipoId = Convert.ToInt32(reader["enderecoTipoId"].ToString());
			}

			if (reader["usuarioId"] != DBNull.Value)
			{
				entidade.Usuario = new Usuario();
				entidade.Usuario.UsuarioId = Convert.ToInt32(reader["usuarioId"].ToString());
			}


		}

	}
}
