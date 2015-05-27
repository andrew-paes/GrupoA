
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
    public partial class ConfiguracaoADO : ADOSuper, IConfiguracaoDAL
    {

        /// <summary>
        /// Método que carrega um Configuracao completa.
        /// </summary>
        /// <param name="entidade">Configuracao a ser carregado filtrando por chave.</param>
        /// <returns>Configuracao</returns>
        public Configuracao CarregarCompleto(Configuracao entidade)
        {

            Configuracao entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT C.*, ");
            sbSQL.Append("CV.valor "); 
            sbSQL.Append("FROM Configuracao C ");
            sbSQL.Append("INNER JOIN ConfiguracaoValor CV ");
            sbSQL.Append("ON C.configuracaoId = CV.configuracaoId ");
            sbSQL.Append("WHERE chave=@chave");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@chave", DbType.String, entidade.Chave);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Configuracao();
                PopulaConfiguracaoCompleto(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// Método que retorna popula um Configuracao baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Configuracao a ser populado(.</param>
        public static void PopulaConfiguracaoCompleto(IDataReader reader, Configuracao entidade)
        {
            if (reader["configuracaoId"] != DBNull.Value)
                entidade.ConfiguracaoId = Convert.ToInt32(reader["configuracaoId"].ToString());

            if (reader["chave"] != DBNull.Value)
                entidade.Chave = reader["chave"].ToString();

            if (reader["descricaoConfiguracao"] != DBNull.Value)
                entidade.DescricaoConfiguracao = reader["descricaoConfiguracao"].ToString();

            if (reader["valor"] != DBNull.Value)
            {
                entidade.ConfiguracaoValor = new ConfiguracaoValor();
                entidade.ConfiguracaoValor.Valor = reader["valor"].ToString();
            }
        }

    }
}
