
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
    public partial class UsuarioControleADO : ADOSuper, IUsuarioControleDAL
    {

        /// <summary>
        /// Método que atualiza os dados de um UsuarioControle.
        /// </summary>
        /// <param name="entidade">UsuarioControle contendo os dados a serem atualizados.</param>
        public void AtualizarStatusSincronizacao(UsuarioControle usuarioControle)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE UsuarioControle SET ");
            sbSQL.Append(" realizarSincronizacao=@realizarSincronizacao ");
            if (!usuarioControle.RealizarSincronizacao)
            {
                sbSQL.Append(" ,dataHoraUltimaSincronia=@dataHoraUltimaSincronia ");
            }
            sbSQL.Append(" WHERE usuarioId=@usuarioId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@usuarioId", DbType.Int32, usuarioControle.UsuarioId);
            _db.AddInParameter(command, "@realizarSincronizacao", DbType.Int32, usuarioControle.RealizarSincronizacao);
            if (!usuarioControle.RealizarSincronizacao)
            {
                _db.AddInParameter(command, "@dataHoraUltimaSincronia", DbType.DateTime, usuarioControle.DataHoraUltimaSincronia);
            }

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        public void AtualizarSomenteCamposRecebidos(UsuarioControle usuarioControle)
        {
            bool camposJaInseridos = false;
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.

            sbSQL.Append(" UPDATE UsuarioControle SET ");
            if (usuarioControle.DataHoraUltimaSincronia != null)
            {
                sbSQL.Append(string.Concat((camposJaInseridos ? "," : string.Empty), "dataHoraUltimaSincronia=@dataHoraUltimaSincronia "));
                camposJaInseridos = true;
            }
            if (usuarioControle.RealizarSincronizacao != null)
            {
                sbSQL.Append(string.Concat((camposJaInseridos ? "," : string.Empty), "realizarSincronizacao=@realizarSincronizacao "));
                camposJaInseridos = true;
            }
            if (usuarioControle.CustomerId != null)
            {
                sbSQL.Append(string.Concat((camposJaInseridos ? "," : string.Empty), "customerId=@customerId "));
                camposJaInseridos = true;
            }
            if (usuarioControle.ProspectId != null)
            {
                sbSQL.Append(string.Concat((camposJaInseridos ? "," : string.Empty), "prospectId=@prospectId "));
                camposJaInseridos = true;
            }
            sbSQL.Append(" WHERE usuarioId=@usuarioId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@usuarioId", DbType.Int32, usuarioControle.UsuarioId);
            if (usuarioControle.DataHoraUltimaSincronia != null)
                _db.AddInParameter(command, "@dataHoraUltimaSincronia", DbType.DateTime, usuarioControle.DataHoraUltimaSincronia);
            if (usuarioControle.RealizarSincronizacao != null)
                _db.AddInParameter(command, "@realizarSincronizacao", DbType.Int32, usuarioControle.RealizarSincronizacao);
            if (usuarioControle.CustomerId != null)
                _db.AddInParameter(command, "@customerId", DbType.String, usuarioControle.CustomerId);
            if (usuarioControle.ProspectId != null)
                _db.AddInParameter(command, "@prospectId", DbType.String, usuarioControle.ProspectId);

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }
    }
}
