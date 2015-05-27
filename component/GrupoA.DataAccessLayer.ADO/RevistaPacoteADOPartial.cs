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
    public partial class RevistaPacoteADO : ADOSuper, IRevistaPacoteDAL
    {
        /// <summary>
        /// Método que persiste um RevistaPacoteBrinde.
        /// </summary>
        /// <param name="revistaPacoteBO"></param>
        /// <param name="produtoBO"></param>
        public void Inserir(RevistaPacote revistaPacoteBO, Produto produtoBO)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO RevistaPacoteBrinde ");
            sbSQL.Append(" (revistaPacoteId, produtoId) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@revistaPacoteId, @produtoId) ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@revistaPacoteId", DbType.Int32, revistaPacoteBO.RevistaPacoteId);
            _db.AddInParameter(command, "@produtoId", DbType.Int32, produtoBO.ProdutoId);
            // Executa a query.
            _db.ExecuteNonQuery(command);
        }

        public void Excluir(RevistaPacote revistaPacoteBO, Produto produtoBO)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM RevistaPacoteBrinde ");
            sbSQL.Append("WHERE revistaPacoteId=@revistaPacoteId AND produtoId=@produtoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaPacoteId", DbType.Int32, revistaPacoteBO.RevistaPacoteId);
            _db.AddInParameter(command, "@produtoId", DbType.Int32, produtoBO.ProdutoId);


            _db.ExecuteNonQuery(command);
        }

        public bool ProdutoBrinde(Produto produtoBO)
        {
            bool entidadeRetorno = false;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(@"SELECT
								COUNT(*) AS Total
							FROM 
								RevistaPacoteBrindeRegra
							WHERE
                                RevistaPacoteBrindeRegra.codigosProdutos IS NOT NULL
	                            AND RevistaPacoteBrindeRegra.codigosProdutos = @codigosProdutos
                            ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@codigosProdutos", DbType.String, produtoBO.CodigoProduto);
            IDataReader entidades = _db.ExecuteReader(command);

            if (entidades.Read())
            {
                if (entidades["Total"] != DBNull.Value)
                {
                    if (Convert.ToInt32(entidades["Total"].ToString()) > 0)
                    {
                        entidadeRetorno = true;
                    }
                }
            }

            entidades.Close();

            return entidadeRetorno;
        }

        public bool BrindeRevistaBMJ(Produto produtoBO)
        {
            bool entidadeRetorno = false;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(@"SELECT
								COUNT(*) AS Total
							FROM 
								RevistaPacoteBrinde
							WHERE
                                RevistaPacoteBrinde.revistaPacoteId = 1
								AND RevistaPacoteBrinde.produtoId = @produtoId
                            ");
            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@produtoId", DbType.Int32, produtoBO.ProdutoId);
            IDataReader entidades = _db.ExecuteReader(command);

            if (entidades.Read())
            {
                if (entidades["Total"] != DBNull.Value)
                {
                    if (Convert.ToInt32(entidades["Total"].ToString()) > 0)
                    {
                        entidadeRetorno = true;
                    }
                }
            }

            entidades.Close();

            return entidadeRetorno;
        }
    }
}