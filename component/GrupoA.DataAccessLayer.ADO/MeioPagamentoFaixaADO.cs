
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
    public partial class MeioPagamentoFaixaADO : ADOSuper, IMeioPagamentoFaixaDAL
    {

        /// <summary>
        /// Método que persiste um MeioPagamentoFaixa.
        /// </summary>
        /// <param name="entidade">MeioPagamentoFaixa contendo os dados a serem persistidos.</param>	
        public void Inserir(MeioPagamentoFaixa entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO MeioPagamentoFaixa ");
            sbSQL.Append(" (meioPagamentoId, valorMinimo, numeroParcelas, codigoGatewayFaixa, codigoLegado, taxaJuros) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@meioPagamentoId, @valorMinimo, @numeroParcelas, @codigoGatewayFaixa, @codigoLegado, @taxaJuros) ");

            sbSQL.Append(" ; SET @meioPagamentoFaixaId = SCOPE_IDENTITY(); ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddOutParameter(command, "@meioPagamentoFaixaId", DbType.Int32, 8);

            _db.AddInParameter(command, "@meioPagamentoId", DbType.Int32, entidade.MeioPagamento.MeioPagamentoId);

            _db.AddInParameter(command, "@valorMinimo", DbType.Decimal, entidade.ValorMinimo);

            _db.AddInParameter(command, "@numeroParcelas", DbType.Int32, entidade.NumeroParcelas);

            if (entidade.CodigoGatewayFaixa != null)
                _db.AddInParameter(command, "@codigoGatewayFaixa", DbType.String, entidade.CodigoGatewayFaixa);
            else
                _db.AddInParameter(command, "@codigoGatewayFaixa", DbType.String, null);

            if (entidade.CodigoLegado != null)
                _db.AddInParameter(command, "@codigoLegado", DbType.String, entidade.CodigoLegado);
            else
                _db.AddInParameter(command, "@codigoLegado", DbType.String, null);

            _db.AddInParameter(command, "@taxaJuros", DbType.Decimal, entidade.TaxaJuros);


            // Executa a query.
            _db.ExecuteNonQuery(command);

            entidade.MeioPagamentoFaixaId = Convert.ToInt32(_db.GetParameterValue(command, "@meioPagamentoFaixaId"));

        }

        /// <summary>
        /// Método que atualiza os dados de um MeioPagamentoFaixa.
        /// </summary>
        /// <param name="entidade">MeioPagamentoFaixa contendo os dados a serem atualizados.</param>
        public void Atualizar(MeioPagamentoFaixa entidade)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE MeioPagamentoFaixa SET ");
            sbSQL.Append(" meioPagamentoId=@meioPagamentoId, valorMinimo=@valorMinimo, numeroParcelas=@numeroParcelas, codigoGatewayFaixa=@codigoGatewayFaixa, codigoLegado=@codigoLegado, taxaJuros=@taxaJuros ");
            sbSQL.Append(" WHERE meioPagamentoFaixaId=@meioPagamentoFaixaId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@meioPagamentoFaixaId", DbType.Int32, entidade.MeioPagamentoFaixaId);
            _db.AddInParameter(command, "@meioPagamentoId", DbType.Int32, entidade.MeioPagamento.MeioPagamentoId);
            _db.AddInParameter(command, "@valorMinimo", DbType.Decimal, entidade.ValorMinimo);
            _db.AddInParameter(command, "@numeroParcelas", DbType.Int32, entidade.NumeroParcelas);
            if (entidade.CodigoGatewayFaixa != null)
                _db.AddInParameter(command, "@codigoGatewayFaixa", DbType.String, entidade.CodigoGatewayFaixa);
            else
                _db.AddInParameter(command, "@codigoGatewayFaixa", DbType.String, null);
            if (entidade.CodigoLegado != null)
                _db.AddInParameter(command, "@codigoLegado", DbType.String, entidade.CodigoLegado);
            else
                _db.AddInParameter(command, "@codigoLegado", DbType.String, null);
            _db.AddInParameter(command, "@taxaJuros", DbType.Decimal, entidade.TaxaJuros);

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que remove um MeioPagamentoFaixa da base de dados.
        /// </summary>
        /// <param name="entidade">MeioPagamentoFaixa a ser excluído (somente o identificador é necessário).</param>		
        public void Excluir(MeioPagamentoFaixa entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM MeioPagamentoFaixa ");
            sbSQL.Append("WHERE meioPagamentoFaixaId=@meioPagamentoFaixaId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@meioPagamentoFaixaId", DbType.Int32, entidade.MeioPagamentoFaixaId);


            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que carrega um MeioPagamentoFaixa.
        /// </summary>
        /// <param name="entidade">MeioPagamentoFaixa a ser carregado (somente o identificador é necessário).</param>
        /// <returns>MeioPagamentoFaixa</returns>
        public MeioPagamentoFaixa Carregar(int meioPagamentoFaixaId)
        {
            MeioPagamentoFaixa entidade = new MeioPagamentoFaixa();
            entidade.MeioPagamentoFaixaId = meioPagamentoFaixaId;
            return Carregar(entidade);

        }


        /// <summary>
        /// Método que carrega um MeioPagamentoFaixa.
        /// </summary>
        /// <param name="entidade">MeioPagamentoFaixa a ser carregado (somente o identificador é necessário).</param>
        /// <returns>MeioPagamentoFaixa</returns>
        public MeioPagamentoFaixa Carregar(MeioPagamentoFaixa entidade)
        {

            MeioPagamentoFaixa entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM MeioPagamentoFaixa WHERE meioPagamentoFaixaId=@meioPagamentoFaixaId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@meioPagamentoFaixaId", DbType.Int32, entidade.MeioPagamentoFaixaId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new MeioPagamentoFaixa();
                PopulaMeioPagamentoFaixa(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }



        /// <summary>
        /// Método que retorna uma coleção de MeioPagamentoFaixa.
        /// </summary>
        /// <param name="entidade">MeioPagamento relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de MeioPagamentoFaixa.</returns>
        public IEnumerable<MeioPagamentoFaixa> Carregar(MeioPagamento entidade)
        {
            List<MeioPagamentoFaixa> entidadesRetorno = new List<MeioPagamentoFaixa>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT MeioPagamentoFaixa.* FROM MeioPagamentoFaixa WHERE MeioPagamentoFaixa.meioPagamentoId=@meioPagamentoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@meioPagamentoId", DbType.Int32, entidade.MeioPagamentoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                MeioPagamentoFaixa entidadeRetorno = new MeioPagamentoFaixa();
                PopulaMeioPagamentoFaixa(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }


        /// <summary>
        /// Método que retorna uma coleção de MeioPagamentoFaixa.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos MeioPagamentoFaixa.</returns>
        public IEnumerable<MeioPagamentoFaixa> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {

            List<MeioPagamentoFaixa> entidadesRetorno = new List<MeioPagamentoFaixa>();

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
                //sbOrder.Append(" ORDER BY meioPagamentoFaixaId");
                sbOrder.Append(" ORDER BY numeroParcelas");
            }


            if (registrosPagina > 0)
            {

                //sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM MeioPagamentoFaixa");
                //if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM MeioPagamentoFaixa WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
                //} else {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM MeioPagamentoFaixa ORDER BY " + orderBy + ")");				
                //}	
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT MeioPagamentoFaixa.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM MeioPagamentoFaixa ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSQL.Append("SELECT MeioPagamentoFaixa.* FROM MeioPagamentoFaixa ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                MeioPagamentoFaixa entidadeRetorno = new MeioPagamentoFaixa();
                PopulaMeioPagamentoFaixa(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna todas os MeioPagamentoFaixa existentes na base de dados.
        /// </summary>
        public IEnumerable<MeioPagamentoFaixa> CarregarTodos()
        {
            return CarregarTodos(0, 0, null, null, null);
        }

        /// <summary>
        /// Método que retorna o total de MeioPagamentoFaixa na base de dados.
        /// </summary>
        /// <returns></returns>
        public int TotalRegistros()
        {
            return TotalRegistros(null);
        }

        /// <summary>
        /// Método que retorna o total de MeioPagamentoFaixa na base de dados, aceita filtro.
        /// </summary>
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
        /// <returns></returns>
        public int TotalRegistros(IFilterHelper filtro)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) AS Total FROM MeioPagamentoFaixa");

            if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Executa a query.

            int resultado = (int)_db.ExecuteScalar(command);


            return resultado;
        }

        /// <summary>
        /// Método que retorna popula um MeioPagamentoFaixa baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">MeioPagamentoFaixa a ser populado(.</param>
        public static void PopulaMeioPagamentoFaixa(IDataReader reader, MeioPagamentoFaixa entidade)
        {
            if (reader["meioPagamentoFaixaId"] != DBNull.Value)
                entidade.MeioPagamentoFaixaId = Convert.ToInt32(reader["meioPagamentoFaixaId"].ToString());

            if (reader["valorMinimo"] != DBNull.Value)
                entidade.ValorMinimo = Convert.ToDecimal(reader["valorMinimo"].ToString());

            if (reader["numeroParcelas"] != DBNull.Value)
                entidade.NumeroParcelas = Convert.ToInt32(reader["numeroParcelas"].ToString());

            if (reader["codigoGatewayFaixa"] != DBNull.Value)
                entidade.CodigoGatewayFaixa = reader["codigoGatewayFaixa"].ToString();

            if (reader["codigoLegado"] != DBNull.Value)
                entidade.CodigoLegado = reader["codigoLegado"].ToString();

            if (reader["taxaJuros"] != DBNull.Value)
                entidade.TaxaJuros = Convert.ToDecimal(reader["taxaJuros"].ToString());

            if (reader["meioPagamentoId"] != DBNull.Value)
            {
                entidade.MeioPagamento = new MeioPagamento();
                entidade.MeioPagamento.MeioPagamentoId = Convert.ToInt32(reader["meioPagamentoId"].ToString());
            }


        }

    }
}
