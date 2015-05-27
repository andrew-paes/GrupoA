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
    public partial class TituloConteudoExtraMidiaADO : ADOSuper, ITituloConteudoExtraMidiaDAL
    {
        /// <summary>
        /// Método que carrega um TituloConteudoExtraMidia.
        /// </summary>
        /// <param name="entidade">TituloConteudoExtraMidia a ser carregado (somente o identificador é necessário).</param>
        /// <returns>TituloConteudoExtraMidia</returns>
        public TituloConteudoExtraMidia Carregar(Titulo entidade)
        {

            TituloConteudoExtraMidia entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM TituloConteudoExtraMidia WHERE tituloConteudoExtraMidiaId=@tituloConteudoExtraMidiaId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@tituloConteudoExtraMidiaId", DbType.Int32, entidade.TituloId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new TituloConteudoExtraMidia();
                PopulaTituloConteudoExtraMidia(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }
    }
}
