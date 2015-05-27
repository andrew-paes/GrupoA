using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using GrupoA.BusinessObject;
using GrupoA.FilterHelper;

namespace GrupoA.DataAccess.ADO
{
    public partial class PaginaPromocionalADO : ADOSuper, IPaginaPromocionalDAL
    {
        /// <summary>
        /// Método que carrega um PaginaPromocional.
        /// </summary>
        /// <param name="entidade">PaginaPromocional a ser carregado (somente o identificador é necessário).</param>
        /// <returns>PaginaPromocional</returns>
        public PaginaPromocional CarregarPorNomePagina(PaginaPromocional entidade)
        {
            PaginaPromocional entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM PaginaPromocional WHERE nomePagina = @nomePagina AND ativo = 1");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@nomePagina", DbType.String, entidade.NomePagina);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new PaginaPromocional();
                PopulaPaginaPromocional(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }
    }
}
