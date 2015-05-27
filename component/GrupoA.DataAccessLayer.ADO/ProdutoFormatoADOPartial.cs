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
    public partial class ProdutoFormatoADO : ADOSuper, IProdutoFormatoDAL
    {
        /// <summary>
        /// Método que carrega um ProdutoFormato.
        /// </summary>
        /// <param name="entidade">ProdutoFormato a ser carregado (somente o identificador é necessário).</param>
        /// <returns>ProdutoFormato</returns>
        public ProdutoFormato Carregar(string formato)
        {

            ProdutoFormato entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM ProdutoFormato WHERE formato = @formato");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@formato", DbType.Int32, formato);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new ProdutoFormato();
                PopulaProdutoFormato(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }
    }
}