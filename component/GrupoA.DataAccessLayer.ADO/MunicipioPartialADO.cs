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
    public partial class MunicipioADO : ADOSuper, IMunicipioDAL
    {
        public List<Municipio> CarregarTodasCidades(int regiaoId)
        {
            List<Municipio> entidadesRetorno = new List<Municipio>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM Municipio WHERE regiaoId=@regiaoId ORDER BY nomeMunicipio ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@regiaoId", DbType.Int32, regiaoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Municipio entidadeRetorno = new Municipio();
                MunicipioADO.PopulaMunicipio(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }

            reader.Close();

            return entidadesRetorno;
        }

        public Municipio CarregarPorCodigoIbge(Municipio entidade)
        {
            Municipio entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM Municipio WHERE codigoIbge=@codigoIbge");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@codigoIbge", DbType.Int32, entidade.CodigoIbge);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Municipio();
                PopulaMunicipio(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }
    }
}