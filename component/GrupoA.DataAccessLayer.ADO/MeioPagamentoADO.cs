
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
    public partial class MeioPagamentoADO : ADOSuper, IMeioPagamentoDAL
    {

        /// <summary>
        /// Método que persiste um MeioPagamento.
        /// </summary>
        /// <param name="entidade">MeioPagamento contendo os dados a serem persistidos.</param>	
        public void Inserir(MeioPagamento entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO MeioPagamento ");
            sbSQL.Append(" (meioPagamentoId, nome, ativo, codigoLegado, codigoGateway) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@meioPagamentoId, @nome, @ativo, @codigoLegado, @codigoGateway) ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@meioPagamentoId", DbType.Int32, entidade.MeioPagamentoId);

            _db.AddInParameter(command, "@nome", DbType.String, entidade.Nome);

            _db.AddInParameter(command, "@ativo", DbType.Int32, entidade.Ativo);

            if (entidade.CodigoLegado != null)
                _db.AddInParameter(command, "@codigoLegado", DbType.String, entidade.CodigoLegado);
            else
                _db.AddInParameter(command, "@codigoLegado", DbType.String, null);

            if (entidade.CodigoGateway != null)
                _db.AddInParameter(command, "@codigoGateway", DbType.String, entidade.CodigoGateway);
            else
                _db.AddInParameter(command, "@codigoGateway", DbType.String, null);


            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que atualiza os dados de um MeioPagamento.
        /// </summary>
        /// <param name="entidade">MeioPagamento contendo os dados a serem atualizados.</param>
        public void Atualizar(MeioPagamento entidade)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE MeioPagamento SET ");
            sbSQL.Append(" nome=@nome, ativo=@ativo, codigoLegado=@codigoLegado, codigoGateway=@codigoGateway ");
            sbSQL.Append(" WHERE meioPagamentoId=@meioPagamentoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@meioPagamentoId", DbType.Int32, entidade.MeioPagamentoId);
            _db.AddInParameter(command, "@nome", DbType.String, entidade.Nome);
            _db.AddInParameter(command, "@ativo", DbType.Int32, entidade.Ativo);
            if (entidade.CodigoLegado != null)
                _db.AddInParameter(command, "@codigoLegado", DbType.String, entidade.CodigoLegado);
            else
                _db.AddInParameter(command, "@codigoLegado", DbType.String, null);

            if (entidade.CodigoGateway != null)
                _db.AddInParameter(command, "@codigoGateway", DbType.String, entidade.CodigoGateway);
            else
                _db.AddInParameter(command, "@codigoGateway", DbType.String, null);

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que remove um MeioPagamento da base de dados.
        /// </summary>
        /// <param name="entidade">MeioPagamento a ser excluído (somente o identificador é necessário).</param>		
        public void Excluir(MeioPagamento entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM MeioPagamento ");
            sbSQL.Append("WHERE meioPagamentoId=@meioPagamentoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@meioPagamentoId", DbType.Int32, entidade.MeioPagamentoId);


            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que carrega um MeioPagamento.
        /// </summary>
        /// <param name="entidade">MeioPagamento a ser carregado (somente o identificador é necessário).</param>
        /// <returns>MeioPagamento</returns>
        public MeioPagamento Carregar(int meioPagamentoId)
        {
            MeioPagamento entidade = new MeioPagamento();
            entidade.MeioPagamentoId = meioPagamentoId;
            return Carregar(entidade);

        }


        /// <summary>
        /// Método que carrega um MeioPagamento.
        /// </summary>
        /// <param name="entidade">MeioPagamento a ser carregado (somente o identificador é necessário).</param>
        /// <returns>MeioPagamento</returns>
        public MeioPagamento Carregar(MeioPagamento entidade)
        {

            MeioPagamento entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM MeioPagamento WHERE meioPagamentoId=@meioPagamentoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@meioPagamentoId", DbType.Int32, entidade.MeioPagamentoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new MeioPagamento();
                PopulaMeioPagamento(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }



        /// <summary>
        /// Método que retorna uma coleção de MeioPagamento.
        /// </summary>
        /// <param name="entidade">MeioPagamentoFaixa relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de MeioPagamento.</returns>
        public IEnumerable<MeioPagamento> Carregar(MeioPagamentoFaixa entidade)
        {
            List<MeioPagamento> entidadesRetorno = new List<MeioPagamento>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT MeioPagamento.* FROM MeioPagamento INNER JOIN MeioPagamentoFaixa ON MeioPagamento.meioPagamentoId=MeioPagamentoFaixa.meioPagamentoId WHERE MeioPagamentoFaixa.meioPagamentoFaixaId=@meioPagamentoFaixaId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@meioPagamentoFaixaId", DbType.Int32, entidade.MeioPagamentoFaixaId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                MeioPagamento entidadeRetorno = new MeioPagamento();
                PopulaMeioPagamento(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de MeioPagamento.
        /// </summary>
        /// <param name="entidade">Pagamento relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de MeioPagamento.</returns>
        public IEnumerable<MeioPagamento> Carregar(Pagamento entidade)
        {
            List<MeioPagamento> entidadesRetorno = new List<MeioPagamento>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT MeioPagamento.* FROM MeioPagamento INNER JOIN Pagamento ON MeioPagamento.meioPagamentoId=Pagamento.meioPagamentoId WHERE Pagamento.pagamentoId=@pagamentoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@pagamentoId", DbType.Int32, entidade.PagamentoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                MeioPagamento entidadeRetorno = new MeioPagamento();
                PopulaMeioPagamento(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }


        /// <summary>
        /// Método que retorna uma coleção de MeioPagamento.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos MeioPagamento.</returns>
        public IEnumerable<MeioPagamento> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {

            List<MeioPagamento> entidadesRetorno = new List<MeioPagamento>();

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
                sbOrder.Append(" ORDER BY meioPagamentoId");
            }


            if (registrosPagina > 0)
            {

                //sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM MeioPagamento");
                //if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM MeioPagamento WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
                //} else {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM MeioPagamento ORDER BY " + orderBy + ")");				
                //}	
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT MeioPagamento.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM MeioPagamento ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSQL.Append("SELECT MeioPagamento.* FROM MeioPagamento ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                MeioPagamento entidadeRetorno = new MeioPagamento();
                PopulaMeioPagamento(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna todas os MeioPagamento existentes na base de dados.
        /// </summary>
        public IEnumerable<MeioPagamento> CarregarTodos()
        {
            return CarregarTodos(0, 0, null, null, null);
        }

        /// <summary>
        /// Método que retorna o total de MeioPagamento na base de dados.
        /// </summary>
        /// <returns></returns>
        public int TotalRegistros()
        {
            return TotalRegistros(null);
        }

        /// <summary>
        /// Método que retorna o total de MeioPagamento na base de dados, aceita filtro.
        /// </summary>
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
        /// <returns></returns>
        public int TotalRegistros(IFilterHelper filtro)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) AS Total FROM MeioPagamento");

            if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Executa a query.

            int resultado = (int)_db.ExecuteScalar(command);


            return resultado;
        }

        /// <summary>
        /// Método que retorna popula um MeioPagamento baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">MeioPagamento a ser populado(.</param>
        public static void PopulaMeioPagamento(IDataReader reader, MeioPagamento entidade)
        {
            if (reader["meioPagamentoId"] != DBNull.Value)
                entidade.MeioPagamentoId = Convert.ToInt32(reader["meioPagamentoId"].ToString());

            if (reader["nome"] != DBNull.Value)
                entidade.Nome = reader["nome"].ToString();

            if (reader["ativo"] != DBNull.Value)
                entidade.Ativo = Convert.ToBoolean(reader["ativo"].ToString());

            if (reader["codigoLegado"] != DBNull.Value)
                entidade.CodigoLegado = reader["codigoLegado"].ToString();

            if (reader["codigoGateway"] != DBNull.Value)
                entidade.CodigoGateway = reader["codigoGateway"].ToString();
        }

    }
}
