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
    public partial class OfertaCategoriaADO : ADOSuper, IOfertaCategoriaDAL
    {
        /// <summary>
        /// Método que remove todas OfertaCategoria da base de dados.
        /// </summary>
        /// <param name="entidade">Oferta a ser excluído (somente o identificador é necessário).</param>		
        public void ExcluirTodosPorOferta(Oferta oferta)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM OfertaCategoria ");
            sbSQL.Append("WHERE ofertaId=@ofertaId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@ofertaId", DbType.Int32, oferta.OfertaId);

            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oferta"></param>
        /// <returns></returns>
        public StringBuilder CarregarTodosPorOferta(Oferta oferta)
        {
            StringBuilder entidadesRetorno = new StringBuilder();

            StringBuilder sbSQL = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            sbSQL.Append("SELECT OfertaCategoria.categoriaId ");
            sbSQL.Append("FROM OfertaCategoria ");
            sbSQL.Append("WHERE OfertaCategoria.ofertaId = @ofertaId ");
            
            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@ofertaId", DbType.Int32, oferta.OfertaId);

            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                entidadesRetorno.Append(reader["categoriaId"].ToString());
                entidadesRetorno.Append(", ");
            }
            reader.Close();

            return entidadesRetorno;

        }
    }
}
