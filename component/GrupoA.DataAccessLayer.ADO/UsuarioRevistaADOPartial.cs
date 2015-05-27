using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using GrupoA.BusinessObject;
using GrupoA.FilterHelper;

namespace GrupoA.DataAccess.ADO
{
    public partial class UsuarioRevistaADO : ADOSuper, IUsuarioRevistaDAL
    {
        /// <summary>
        /// Método que retorna uma coleção de UsuarioRevista.
        /// </summary>
        /// <param name="entidade">Usuario relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de UsuarioRevista.</returns>
        public List<UsuarioRevista> CarregarAssinaturasValidasPorUsuario(Usuario entidade)
        {
            List<UsuarioRevista> entidadesRetorno = new List<UsuarioRevista>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(@"SELECT *
                          FROM UsuarioRevista
                          WHERE usuarioId = @usuarioId
	                          AND dataInicioAssinatura <= GETDATE()
  	                          AND dataFimAssinatura >= GETDATE()");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.UsuarioId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                UsuarioRevista entidadeRetorno = new UsuarioRevista();
                PopulaUsuarioRevista(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }
    }
}
