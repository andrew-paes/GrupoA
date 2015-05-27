
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
    public partial class TituloSolicitacaoStatusADO : ADOSuper, ITituloSolicitacaoStatusDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<TituloSolicitacaoStatus> CarregarTodosTituloSolicitacaoStatusParaLiberacao()
        {
            List<TituloSolicitacaoStatus> entidadesRetorno = new List<TituloSolicitacaoStatus>();

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            sbSQL.Append("SELECT TituloSolicitacaoStatus.* FROM TituloSolicitacaoStatus ");
            sbSQL.Append("WHERE tituloSolicitacaoStatusId <> 3");
            
            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                TituloSolicitacaoStatus entidadeRetorno = new TituloSolicitacaoStatus();
                PopulaTituloSolicitacaoStatus(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
        }
    }
}
