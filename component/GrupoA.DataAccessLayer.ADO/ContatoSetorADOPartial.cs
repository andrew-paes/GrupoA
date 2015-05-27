
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
	public partial class ContatoSetorADO : ADOSuper, IContatoSetorDAL
    {

        #region Métodos

        /// <summary>
        /// Método que Verifica se o Nome do Setor é Duplicado na tabela ContatoSetor
        /// </summary>
        public bool VerificaNomeSetorDuplicado(int contatoSetorId, string nomeSetor)
        {
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Registros FROM ContatoSetor ");
            if (contatoSetorId != 0)
            {
                sbSQL.Append("WHERE contatoSetorId != @contatoSetorId AND UPPER(nomeSetor) = @nomeSetor ");
            }
            else
            {
                sbSQL.Append("WHERE UPPER(nomeSetor) = @nomeSetor ");
            }

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@contatoSetorId", DbType.Int32, contatoSetorId);
            _db.AddInParameter(command, "@nomeSetor", DbType.String, nomeSetor);
								
			// Executa a query.

            int registros = ((int)_db.ExecuteScalar(command));

            if (registros > 0)
            {
                return false;
            }

			
			return true;			

        }

        #endregion

    }
}
		