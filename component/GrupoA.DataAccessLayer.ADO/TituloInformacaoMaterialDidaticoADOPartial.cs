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
    public partial class TituloInformacaoMaterialDidaticoADO : ADOSuper, ITituloInformacaoMaterialDidaticoDAL
    {
/// <summary>
        /// Método que carrega um TituloInformacaoMaterialDidatico.
        /// </summary>
        /// <param name="entidade">TituloInformacaoMaterialDidatico a ser carregado (somente o identificador é necessário).</param>
        /// <returns>TituloInformacaoMaterialDidatico</returns>
        public TituloInformacaoMaterialDidatico Carregar(Titulo entidade)
        {

            TituloInformacaoMaterialDidatico entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM TituloInformacaoMaterialDidatico WHERE tituloInformacaoMaterialDidaticoId=@tituloInformacaoMaterialDidaticoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@tituloInformacaoMaterialDidaticoId", DbType.Int32, entidade.TituloId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new TituloInformacaoMaterialDidatico();
                PopulaTituloInformacaoMaterialDidatico(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }
    }
}