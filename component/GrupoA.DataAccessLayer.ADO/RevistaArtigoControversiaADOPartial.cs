using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using GrupoA.BusinessObject;
using GrupoA.FilterHelper;

namespace GrupoA.DataAccess.ADO
{
    public partial class RevistaArtigoControversiaADO : ADOSuper, IRevistaArtigoControversiaDAL
    {

        /// <summary>
        /// Método que carrega um RevistaArtigoControversia.
        /// </summary>
        /// <param name="entidade">RevistaArtigoControversia a ser carregado (somente o identificador é necessário).</param>
        /// <returns>RevistaArtigoControversia</returns>
        public RevistaArtigoControversia CarregarPorArtigoIdPosicionamento(RevistaArtigoControversia entidade)
        {
            RevistaArtigoControversia entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM RevistaArtigoControversia ");
            sbSQL.Append("WHERE revistaArtigoId = @revistaArtigoId ");
            sbSQL.Append("  AND posicionamento = @posicionamento ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaArtigoId", DbType.Int32, entidade.RevistaArtigoId);
            _db.AddInParameter(command, "@posicionamento", DbType.Int32, entidade.Posicionamento);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new RevistaArtigoControversia();
                PopulaRevistaArtigoControversia(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }

        public void ExcluirTodosPorRevistaArtigoId(Int32 revistaArtigoId)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM RevistaArtigoControversia ");
            sbSQL.Append("WHERE revistaArtigoId=@revistaArtigoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaArtigoId", DbType.Int32, revistaArtigoId);


            _db.ExecuteNonQuery(command);
        }

    }
}
