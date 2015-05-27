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
        #region Métodos de MeioPagamento
        /// <summary>
        /// Método que carrega um Produto com suas dependências.
        /// </summary>
        /// <param name="entidade">Produto a ser carregado (somente o identificador é necessário).</param>
        /// <returns>Produto</returns>
        public MeioPagamento CarregarComDependencias(MeioPagamento entidade)
        {

            MeioPagamento entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("SELECT meioPagamento.* ");
            sbSQL.Append("FROM meioPagamento ");

            sbSQL.Append("WHERE meioPagamento.meioPagamentoId=@meioPagamentoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@meioPagamentoId", DbType.Int32, entidade.MeioPagamentoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new MeioPagamento();
                PopulaMeioPagamentoComDependencias(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }

        public List<MeioPagamento> CarregarMeiosPagamentoMaioresQueValor(double valor)
        {
            List<MeioPagamento> meiosPagamento = new List<MeioPagamento>();

            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("SELECT meioPagamento.* ");
            sbSQL.Append("FROM meioPagamento ");
            sbSQL.Append("WHERE ativo = 1 ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            //_db.AddInParameter(command, "@meioPagamentoId", DbType.Int32, meioPagamento.MeioPagamentoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                MeioPagamento meioPagamento = new MeioPagamento();
                PopulaMeioPagamentoComDependencias(reader, meioPagamento, valor);
                meiosPagamento.Add(meioPagamento);
            }
            reader.Close();

            return meiosPagamento;
        }

        public static void PopulaMeioPagamentoComDependencias(IDataReader reader, MeioPagamento entidade)
        {
            PopulaMeioPagamentoComDependencias(reader, entidade, 0);
        }
        /// <summary>
        /// Método que retorna popula um MeioPagamento baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">MeioPagamento a ser populado(.</param>
        public static void PopulaMeioPagamentoComDependencias(IDataReader reader, MeioPagamento entidade, double valorMinimo)
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

            //Carrega Lista de MeioPagamentos
            IMeioPagamentoFaixaDAL meioPagamentoFaixasADO = new MeioPagamentoFaixaADO();
            if (valorMinimo == 0)
            {
                //meioPagamentoFaixasADO = new MeioPagamentoFaixaADO();
                var meioPagamentoFaixaFH = new MeioPagamentoFaixaFH() { MeioPagamentoId = entidade.MeioPagamentoId.ToString() };
                entidade.MeioPagamentoFaixas = (List<MeioPagamentoFaixa>)meioPagamentoFaixasADO.CarregarTodos(0, 0, null, null, meioPagamentoFaixaFH);
            }
            else
            {
                entidade.MeioPagamentoFaixas = (List<MeioPagamentoFaixa>)meioPagamentoFaixasADO.CarregarMeiosPagamentoFaixaMaioresQueValor(entidade, valorMinimo);
            }
        }
        #endregion
    }
}