
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
	public partial class CompraConjuntaPaginaADO : ADOSuper, ICompraConjuntaPaginaDAL
    {

        #region Métodos

        public CompraConjuntaPagina CarregarPorIdCompraConjuntaPagina(int CompraConjuntaPaginaId)
        {

            CompraConjuntaPagina entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT CompraConjuntaPagina.compraConjuntaPaginaId, ");
            sbSQL.Append("       CompraConjuntaPagina.pagina ");
            sbSQL.Append("FROM   CompraConjuntaPagina ");
            sbSQL.Append("WHERE  CompraConjuntaPaginaId=@CompraConjuntaPaginaId ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@CompraConjuntaPaginaId", DbType.Int32, CompraConjuntaPaginaId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new CompraConjuntaPagina();
                PopulaPagina(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }

        public static void PopulaPagina(IDataReader reader, CompraConjuntaPagina entidade)
        {
            if (reader["pagina"] != DBNull.Value)
            {
                entidade.Pagina = reader["pagina"].ToString();
            }

            if (reader["compraConjuntaPaginaId"] != DBNull.Value)
            {
                entidade.CompraConjuntaPaginaId = Convert.ToInt32(reader["compraConjuntaPaginaId"].ToString());
            }


        }		

        #endregion

    }
}
		