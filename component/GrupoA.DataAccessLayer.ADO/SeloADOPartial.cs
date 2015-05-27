
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
    public partial class SeloADO : ADOSuper, ISeloDAL
    {
        public bool SeloProdutoRelacionado(Selo seloBO, Produto produtoBO)
        {
            bool entidadeRetorno = false;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(@"SELECT
								COUNT(*) AS Total
							FROM 
								ProdutoSelo
							WHERE
                                ProdutoSelo.seloId = @seloId
								AND ProdutoSelo.produtoId = @produtoId
                            ");
            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@seloId", DbType.Int32, seloBO.SeloId);
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