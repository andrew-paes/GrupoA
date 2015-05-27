
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
    public partial class PedidoEnderecoADO : ADOSuper, IPedidoEnderecoDAL
    {

        /// <summary>
        /// Método que persiste um PedidoEndereco.
        /// </summary>
        /// <param name="entidade">PedidoEndereco contendo os dados a serem persistidos.</param>	
        public void Inserir(PedidoEndereco entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO PedidoEndereco ");
            sbSQL.Append(" (pedidoId, enderecoTipoId, municipioId, bairro, cep, complemento, logradouro, numero) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@pedidoId, @enderecoTipoId, @municipioId, @bairro, @cep, @complemento, @logradouro, @numero) ");

            sbSQL.Append(" ; SET @pedidoEnderecoId = SCOPE_IDENTITY(); ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddOutParameter(command, "@pedidoEnderecoId", DbType.Int32, 8);

            _db.AddInParameter(command, "@pedidoId", DbType.Int32, entidade.Pedido.PedidoId);

            _db.AddInParameter(command, "@enderecoTipoId", DbType.Int32, entidade.EnderecoTipo.EnderecoTipoId);

            _db.AddInParameter(command, "@municipioId", DbType.Int32, entidade.Municipio.MunicipioId);

            _db.AddInParameter(command, "@bairro", DbType.String, entidade.Bairro);

            _db.AddInParameter(command, "@cep", DbType.String, entidade.Cep);

            if (entidade.Complemento != null)
                _db.AddInParameter(command, "@complemento", DbType.String, entidade.Complemento);
            else
                _db.AddInParameter(command, "@complemento", DbType.String, null);

            _db.AddInParameter(command, "@logradouro", DbType.String, entidade.Logradouro);

            _db.AddInParameter(command, "@numero", DbType.String, entidade.Numero);


            // Executa a query.
            _db.ExecuteNonQuery(command);

            entidade.PedidoEnderecoId = Convert.ToInt32(_db.GetParameterValue(command, "@pedidoEnderecoId"));

        }

        /// <summary>
        /// Método que atualiza os dados de um PedidoEndereco.
        /// </summary>
        /// <param name="entidade">PedidoEndereco contendo os dados a serem atualizados.</param>
        public void Atualizar(PedidoEndereco entidade)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE PedidoEndereco SET ");
            sbSQL.Append(" pedidoId=@pedidoId, enderecoTipoId=@enderecoTipoId, municipioId=@municipioId, bairro=@bairro, cep=@cep, complemento=@complemento, logradouro=@logradouro, numero=@numero ");
            sbSQL.Append(" WHERE pedidoEnderecoId=@pedidoEnderecoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@pedidoEnderecoId", DbType.Int32, entidade.PedidoEnderecoId);
            _db.AddInParameter(command, "@pedidoId", DbType.Int32, entidade.Pedido.PedidoId);
            _db.AddInParameter(command, "@enderecoTipoId", DbType.Int32, entidade.EnderecoTipo.EnderecoTipoId);
            _db.AddInParameter(command, "@municipioId", DbType.Int32, entidade.Municipio.MunicipioId);
            _db.AddInParameter(command, "@bairro", DbType.String, entidade.Bairro);
            _db.AddInParameter(command, "@cep", DbType.String, entidade.Cep);
            if (entidade.Complemento != null)
                _db.AddInParameter(command, "@complemento", DbType.String, entidade.Complemento);
            else
                _db.AddInParameter(command, "@complemento", DbType.String, null);
            _db.AddInParameter(command, "@logradouro", DbType.String, entidade.Logradouro);
            _db.AddInParameter(command, "@numero", DbType.String, entidade.Numero);

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que remove um PedidoEndereco da base de dados.
        /// </summary>
        /// <param name="entidade">PedidoEndereco a ser excluído (somente o identificador é necessário).</param>		
        public void Excluir(PedidoEndereco entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM PedidoEndereco ");
            sbSQL.Append("WHERE pedidoEnderecoId=@pedidoEnderecoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@pedidoEnderecoId", DbType.Int32, entidade.PedidoEnderecoId);


            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que carrega um PedidoEndereco.
        /// </summary>
        /// <param name="entidade">PedidoEndereco a ser carregado (somente o identificador é necessário).</param>
        /// <returns>PedidoEndereco</returns>
        public PedidoEndereco Carregar(int pedidoEnderecoId)
        {
            PedidoEndereco entidade = new PedidoEndereco();
            entidade.PedidoEnderecoId = pedidoEnderecoId;
            return Carregar(entidade);

        }


        /// <summary>
        /// Método que carrega um PedidoEndereco.
        /// </summary>
        /// <param name="entidade">PedidoEndereco a ser carregado (somente o identificador é necessário).</param>
        /// <returns>PedidoEndereco</returns>
        public PedidoEndereco Carregar(PedidoEndereco entidade)
        {

            PedidoEndereco entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM PedidoEndereco WHERE pedidoEnderecoId=@pedidoEnderecoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@pedidoEnderecoId", DbType.Int32, entidade.PedidoEnderecoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new PedidoEndereco();
                PopulaPedidoEndereco(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }



        /// <summary>
        /// Método que retorna uma coleção de PedidoEndereco.
        /// </summary>
        /// <param name="entidade">EnderecoTipo relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de PedidoEndereco.</returns>
        public IEnumerable<PedidoEndereco> Carregar(EnderecoTipo entidade)
        {
            List<PedidoEndereco> entidadesRetorno = new List<PedidoEndereco>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT PedidoEndereco.* FROM PedidoEndereco WHERE PedidoEndereco.enderecoTipoId=@enderecoTipoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@enderecoTipoId", DbType.Int32, entidade.EnderecoTipoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                PedidoEndereco entidadeRetorno = new PedidoEndereco();
                PopulaPedidoEndereco(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de PedidoEndereco.
        /// </summary>
        /// <param name="entidade">Municipio relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de PedidoEndereco.</returns>
        public IEnumerable<PedidoEndereco> Carregar(Municipio entidade)
        {
            List<PedidoEndereco> entidadesRetorno = new List<PedidoEndereco>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT PedidoEndereco.* FROM PedidoEndereco WHERE PedidoEndereco.municipioId=@municipioId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@municipioId", DbType.Int32, entidade.MunicipioId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                PedidoEndereco entidadeRetorno = new PedidoEndereco();
                PopulaPedidoEndereco(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna um PedidoEndereco.
        /// </summary>
        /// <param name="entidade">Pedido relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna um PedidoEndereco.</returns>
        public PedidoEndereco Carregar(Pedido entidade)
        {
            PedidoEndereco entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT PedidoEndereco.* FROM PedidoEndereco WHERE PedidoEndereco.pedidoId=@pedidoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@pedidoId", DbType.Int32, entidade.PedidoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new PedidoEndereco();
                PopulaPedidoEndereco(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;

        }


        /// <summary>
        /// Método que retorna uma coleção de PedidoEndereco.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos PedidoEndereco.</returns>
        public IEnumerable<PedidoEndereco> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {

            List<PedidoEndereco> entidadesRetorno = new List<PedidoEndereco>();

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
                sbOrder.Append(" ORDER BY pedidoEnderecoId");
            }


            if (registrosPagina > 0)
            {

                //sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM PedidoEndereco");
                //if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM PedidoEndereco WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
                //} else {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM PedidoEndereco ORDER BY " + orderBy + ")");				
                //}	
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT PedidoEndereco.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM PedidoEndereco ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSQL.Append("SELECT PedidoEndereco.* FROM PedidoEndereco ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                PedidoEndereco entidadeRetorno = new PedidoEndereco();
                PopulaPedidoEndereco(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna todas os PedidoEndereco existentes na base de dados.
        /// </summary>
        public IEnumerable<PedidoEndereco> CarregarTodos()
        {
            return CarregarTodos(0, 0, null, null, null);
        }

        /// <summary>
        /// Método que retorna o total de PedidoEndereco na base de dados.
        /// </summary>
        /// <returns></returns>
        public int TotalRegistros()
        {
            return TotalRegistros(null);
        }

        /// <summary>
        /// Método que retorna o total de PedidoEndereco na base de dados, aceita filtro.
        /// </summary>
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
        /// <returns></returns>
        public int TotalRegistros(IFilterHelper filtro)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) AS Total FROM PedidoEndereco");

            if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Executa a query.

            int resultado = (int)_db.ExecuteScalar(command);


            return resultado;
        }

        /// <summary>
        /// Método que retorna popula um PedidoEndereco baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">PedidoEndereco a ser populado(.</param>
        public static void PopulaPedidoEndereco(IDataReader reader, PedidoEndereco entidade)
        {
            if (reader["pedidoEnderecoId"] != DBNull.Value)
                entidade.PedidoEnderecoId = Convert.ToInt32(reader["pedidoEnderecoId"].ToString());

            if (reader["bairro"] != DBNull.Value)
                entidade.Bairro = reader["bairro"].ToString();

            if (reader["cep"] != DBNull.Value)
                entidade.Cep = reader["cep"].ToString();

            if (reader["complemento"] != DBNull.Value)
                entidade.Complemento = reader["complemento"].ToString();

            if (reader["logradouro"] != DBNull.Value)
                entidade.Logradouro = reader["logradouro"].ToString();

            if (reader["numero"] != DBNull.Value)
                entidade.Numero = reader["numero"].ToString();

            if (reader["pedidoId"] != DBNull.Value)
            {
                entidade.Pedido = new Pedido();
                entidade.Pedido.PedidoId = Convert.ToInt32(reader["pedidoId"].ToString());
            }

            if (reader["enderecoTipoId"] != DBNull.Value)
            {
                entidade.EnderecoTipo = new EnderecoTipo();
                entidade.EnderecoTipo.EnderecoTipoId = Convert.ToInt32(reader["enderecoTipoId"].ToString());
            }

            if (reader["municipioId"] != DBNull.Value)
            {
                entidade.Municipio = new Municipio();
                entidade.Municipio.MunicipioId = Convert.ToInt32(reader["municipioId"].ToString());
            }


        }

    }
}
