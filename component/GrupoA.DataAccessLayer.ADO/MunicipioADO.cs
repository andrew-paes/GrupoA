
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
    public partial class MunicipioADO : ADOSuper, IMunicipioDAL
    {

        /// <summary>
        /// Método que persiste um Municipio.
        /// </summary>
        /// <param name="entidade">Municipio contendo os dados a serem persistidos.</param>	
        public void Inserir(Municipio entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO Municipio ");
            sbSQL.Append(" (municipioId, nomeMunicipio, regiaoId, codigoIbge) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@municipioId, @nomeMunicipio, @regiaoId, @codigoIbge) ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@municipioId", DbType.Int32, entidade.MunicipioId);

            _db.AddInParameter(command, "@nomeMunicipio", DbType.String, entidade.NomeMunicipio);

            _db.AddInParameter(command, "@regiaoId", DbType.Int32, entidade.Regiao.RegiaoId);

            if (entidade.CodigoIbge != null)
                _db.AddInParameter(command, "@codigoIbge", DbType.Int32, entidade.CodigoIbge);
            else
                _db.AddInParameter(command, "@codigoIbge", DbType.Int32, null);


            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que atualiza os dados de um Municipio.
        /// </summary>
        /// <param name="entidade">Municipio contendo os dados a serem atualizados.</param>
        public void Atualizar(Municipio entidade)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE Municipio SET ");
            sbSQL.Append(" nomeMunicipio=@nomeMunicipio, regiaoId=@regiaoId, codigoIbge=@codigoIbge ");
            sbSQL.Append(" WHERE municipioId=@municipioId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@municipioId", DbType.Int32, entidade.MunicipioId);
            _db.AddInParameter(command, "@nomeMunicipio", DbType.String, entidade.NomeMunicipio);
            _db.AddInParameter(command, "@regiaoId", DbType.Int32, entidade.Regiao.RegiaoId);
            if (entidade.CodigoIbge != null)
                _db.AddInParameter(command, "@codigoIbge", DbType.Int32, entidade.CodigoIbge);
            else
                _db.AddInParameter(command, "@codigoIbge", DbType.Int32, null);

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que remove um Municipio da base de dados.
        /// </summary>
        /// <param name="entidade">Municipio a ser excluído (somente o identificador é necessário).</param>		
        public void Excluir(Municipio entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM Municipio ");
            sbSQL.Append("WHERE municipioId=@municipioId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@municipioId", DbType.Int32, entidade.MunicipioId);


            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que carrega um Municipio.
        /// </summary>
        /// <param name="entidade">Municipio a ser carregado (somente o identificador é necessário).</param>
        /// <returns>Municipio</returns>
        public Municipio Carregar(int municipioId)
        {
            Municipio entidade = new Municipio();
            entidade.MunicipioId = municipioId;
            return Carregar(entidade);

        }


        /// <summary>
        /// Método que carrega um Municipio.
        /// </summary>
        /// <param name="entidade">Municipio a ser carregado (somente o identificador é necessário).</param>
        /// <returns>Municipio</returns>
        public Municipio Carregar(Municipio entidade)
        {

            Municipio entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM Municipio WHERE municipioId=@municipioId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@municipioId", DbType.Int32, entidade.MunicipioId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Municipio();
                PopulaMunicipio(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }



        /// <summary>
        /// Método que retorna uma coleção de Municipio.
        /// </summary>
        /// <param name="entidade">Endereco relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Municipio.</returns>
        public IEnumerable<Municipio> Carregar(Endereco entidade)
        {
            List<Municipio> entidadesRetorno = new List<Municipio>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Municipio.* FROM Municipio INNER JOIN Endereco ON Municipio.municipioId=Endereco.municipioId WHERE Endereco.enderecoId=@enderecoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@enderecoId", DbType.Int32, entidade.EnderecoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Municipio entidadeRetorno = new Municipio();
                PopulaMunicipio(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de Municipio.
        /// </summary>
        /// <param name="entidade">PedidoEndereco relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Municipio.</returns>
        public IEnumerable<Municipio> Carregar(PedidoEndereco entidade)
        {
            List<Municipio> entidadesRetorno = new List<Municipio>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Municipio.* FROM Municipio INNER JOIN PedidoEndereco ON Municipio.municipioId=PedidoEndereco.municipioId WHERE PedidoEndereco.pedidoEnderecoId=@pedidoEnderecoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@pedidoEnderecoId", DbType.Int32, entidade.PedidoEnderecoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Municipio entidadeRetorno = new Municipio();
                PopulaMunicipio(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de Municipio.
        /// </summary>
        /// <param name="entidade">Regiao relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Municipio.</returns>
        public IEnumerable<Municipio> Carregar(Regiao entidade)
        {
            List<Municipio> entidadesRetorno = new List<Municipio>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Municipio.* FROM Municipio WHERE Municipio.regiaoId=@regiaoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@regiaoId", DbType.Int32, entidade.RegiaoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Municipio entidadeRetorno = new Municipio();
                PopulaMunicipio(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }


        /// <summary>
        /// Método que retorna uma coleção de Municipio.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos Municipio.</returns>
        public IEnumerable<Municipio> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {

            List<Municipio> entidadesRetorno = new List<Municipio>();

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
                sbOrder.Append(" ORDER BY municipioId");
            }


            if (registrosPagina > 0)
            {

                //sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM Municipio");
                //if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Municipio WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
                //} else {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Municipio ORDER BY " + orderBy + ")");				
                //}	
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT Municipio.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM Municipio ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSQL.Append("SELECT Municipio.* FROM Municipio ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Municipio entidadeRetorno = new Municipio();
                PopulaMunicipio(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna todas os Municipio existentes na base de dados.
        /// </summary>
        public IEnumerable<Municipio> CarregarTodos()
        {
            return CarregarTodos(0, 0, null, null, null);
        }

        /// <summary>
        /// Método que retorna o total de Municipio na base de dados.
        /// </summary>
        /// <returns></returns>
        public int TotalRegistros()
        {
            return TotalRegistros(null);
        }

        /// <summary>
        /// Método que retorna o total de Municipio na base de dados, aceita filtro.
        /// </summary>
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
        /// <returns></returns>
        public int TotalRegistros(IFilterHelper filtro)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) AS Total FROM Municipio");

            if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Executa a query.

            int resultado = (int)_db.ExecuteScalar(command);


            return resultado;
        }

        /// <summary>
        /// Método que retorna popula um Municipio baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Municipio a ser populado(.</param>
        public static void PopulaMunicipio(IDataReader reader, Municipio entidade)
        {
            if (reader["municipioId"] != DBNull.Value)
                entidade.MunicipioId = Convert.ToInt32(reader["municipioId"].ToString());

            if (reader["nomeMunicipio"] != DBNull.Value)
                entidade.NomeMunicipio = reader["nomeMunicipio"].ToString();

            if (reader["codigoIbge"] != DBNull.Value)
                entidade.CodigoIbge = Convert.ToInt32(reader["codigoIbge"].ToString());

            if (reader["regiaoId"] != DBNull.Value)
            {
                entidade.Regiao = new Regiao();
                entidade.Regiao.RegiaoId = Convert.ToInt32(reader["regiaoId"].ToString());
            }


        }

    }
}
