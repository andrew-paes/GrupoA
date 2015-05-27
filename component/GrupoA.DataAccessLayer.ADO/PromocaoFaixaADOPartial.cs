
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
    public partial class PromocaoFaixaADO : ADOSuper, IPromocaoFaixaDAL
    {
        /// <summary>
        /// Método que remove todas PromocaoFaixa da base de dados de uma promoção.
        /// </summary>
        /// <param name="entidade">Promocao a ser excluído (somente o identificador é necessário).</param>		
        public void ExcluirTodasFaixasDaPromocao(Promocao entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM PromocaoFaixa ");
            sbSQL.Append("WHERE promocaoId=@promocaoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@promocaoId", DbType.Int32, entidade.PromocaoId);


            _db.ExecuteNonQuery(command);
        }
    }
}
