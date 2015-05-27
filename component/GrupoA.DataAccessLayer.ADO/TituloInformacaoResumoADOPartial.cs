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
    public partial class TituloInformacaoResumoADO : ADOSuper, ITituloInformacaoResumoDAL
    {
        /// <summary>
        /// Método que carrega um TituloInformacaoResumo.
        /// </summary>
        /// <param name="entidade">TituloInformacaoResumo a ser carregado (somente o identificador é necessário).</param>
        /// <returns>TituloInformacaoResumo</returns>
        public TituloInformacaoResumo Carregar(Titulo entidade)
        {
            TituloInformacaoResumo entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM TituloInformacaoResumo WHERE tituloInformacaoResumoId=@tituloInformacaoResumoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@tituloInformacaoResumoId", DbType.Int32, entidade.TituloId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new TituloInformacaoResumo();
                PopulaTituloInformacaoResumo(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }
    }
}