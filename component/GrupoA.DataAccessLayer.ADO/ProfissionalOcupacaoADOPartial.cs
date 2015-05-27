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
    public partial class ProfissionalOcupacaoADO : ADOSuper, IProfissionalOcupacaoDAL
    {
        public ProfissionalOcupacao CarregarPorCodigoOcupacao(ProfissionalOcupacao entidade)
        {
            ProfissionalOcupacao entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM ProfissionalOcupacao WHERE codigoOcupacao=@codigoOcupacao");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@codigoOcupacao", DbType.String, entidade.CodigoOcupacao);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new ProfissionalOcupacao();
                PopulaProfissionalOcupacao(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }
    }
}