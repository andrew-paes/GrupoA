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
    public partial class TituloInformacaoSumarioADO : ADOSuper, ITituloInformacaoSumarioDAL
    {
        /// <summary>
        /// Método que carrega um TituloInformacaoSumario.
        /// </summary>
        /// <param name="entidade">TituloInformacaoSumario a ser carregado (somente o identificador é necessário).</param>
        /// <returns>TituloInformacaoSumario</returns>
        public TituloInformacaoSumario Carregar(Titulo entidade)
        {

            TituloInformacaoSumario entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM TituloInformacaoSumario WHERE tituloInformacaoSumarioId=@tituloInformacaoSumarioId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@tituloInformacaoSumarioId", DbType.Int32, entidade.TituloId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new TituloInformacaoSumario();
                PopulaTituloInformacaoSumario(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }
    }
}
