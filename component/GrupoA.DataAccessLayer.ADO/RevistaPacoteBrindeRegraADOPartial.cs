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
    public partial class RevistaPacoteBrindeRegraADO : ADOSuper, IRevistaPacoteBrindeRegraDAL
    {
        public RevistaPacoteBrindeRegra CarregarBMJ(List<Produto> produtoBOList)
        {
            RevistaPacoteBrindeRegra entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT TOP 1 * FROM RevistaPacoteBrindeRegra ");
            sbSQL.Append("WHERE RevistaPacoteBrindeRegra.codigosProdutos IS NOT NULL ");

            if (produtoBOList != null && produtoBOList.Count > 0)
            {
                foreach (Produto produtoBOTemp in produtoBOList)
                {
                    if (!String.IsNullOrEmpty(produtoBOTemp.CodigoProduto))
                    {
                        sbSQL.Append(String.Format("OR RevistaPacoteBrindeRegra.codigosProdutos LIKE '%{0}%' ", produtoBOTemp.CodigoProduto));
                    }
                }
            }

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new RevistaPacoteBrindeRegra();
                PopulaRevistaPacoteBrindeRegra(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }

        public RevistaPacoteBrindeRegra CarregarPatio(List<Produto> produtoBOList)
        {
            RevistaPacoteBrindeRegra entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT TOP 1 * FROM RevistaPacoteBrindeRegra ");
            sbSQL.Append("WHERE RevistaPacoteBrindeRegra.codigosProdutos IS NOT NULL ");

            if (produtoBOList != null && produtoBOList.Count > 0)
            {
                foreach (Produto produtoBOTemp in produtoBOList)
                {
                    if (!String.IsNullOrEmpty(produtoBOTemp.CodigoProduto))
                    {
                        sbSQL.Append(String.Format("AND RevistaPacoteBrindeRegra.codigosProdutos LIKE '%{0}%' ", produtoBOTemp.CodigoProduto));
                    }
                }
            }

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new RevistaPacoteBrindeRegra();
                PopulaRevistaPacoteBrindeRegra(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="produtoBO"></param>
        /// <returns></returns>
        public RevistaPacoteBrindeRegra Carregar(Produto produtoBO)
        {
            RevistaPacoteBrindeRegra entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT TOP 1 * FROM RevistaPacoteBrindeRegra ");
            sbSQL.Append("WHERE RevistaPacoteBrindeRegra.codigosProdutos IS NOT NULL ");

            if (!String.IsNullOrEmpty(produtoBO.CodigoProduto))
            {
                sbSQL.Append(String.Format("AND RevistaPacoteBrindeRegra.codigosProdutos LIKE '%{0}%' ", produtoBO.CodigoProduto));
            }

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new RevistaPacoteBrindeRegra();
                PopulaRevistaPacoteBrindeRegra(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }
    }
}
