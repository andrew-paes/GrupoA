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
    public partial class TituloConteudoExtraUrlADO : ADOSuper, ITituloConteudoExtraUrlDAL
    {
        /// <summary>
        /// Método que carrega um TituloConteudoExtraUrl.
        /// </summary>
        /// <param name="entidade">TituloConteudoExtraUrl a ser carregado (somente o identificador é necessário).</param>
        /// <returns>TituloConteudoExtraUrl</returns>
        public TituloConteudoExtraUrl Carregar(Titulo entidade)
        {

            TituloConteudoExtraUrl entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM TituloConteudoExtraUrl WHERE tituloConteudoExtraUrlId=@tituloConteudoExtraUrlId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@tituloConteudoExtraUrlId", DbType.Int32, entidade.TituloId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new TituloConteudoExtraUrl();
                PopulaTituloConteudoExtraUrl(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }
    }
}