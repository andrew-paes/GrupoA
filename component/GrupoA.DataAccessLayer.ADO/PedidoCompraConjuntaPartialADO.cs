using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using GrupoA.BusinessObject;

namespace GrupoA.DataAccess.ADO
{
    public partial class PedidoCompraConjuntaADO : IPedidoCompraConjuntaDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="compraConjunta"></param>
        /// <returns></returns>
        public IEnumerable<PedidoCompraConjunta> CarregarTodosPorCompraConjunta(CompraConjunta compraConjunta)
        {
            List<PedidoCompraConjunta> entidadesRetorno = new List<PedidoCompraConjunta>();

            StringBuilder sbSql = new StringBuilder();

            sbSql.Append(@"SELECT
							PedidoCompraConjunta.*
						FROM 
							PedidoCompraConjunta
							--INNER JOIN CompraConjuntaDesconto ON CompraConjuntaDesconto.compraConjuntaDescontoId = PedidoCompraConjunta.compraConjuntaDescontoId
							INNER JOIN CompraConjunta ON CompraConjunta.compraConjuntaId = PedidoCompraConjunta.compraConjuntaId
						WHERE
							CompraConjunta.compraConjuntaId = @compraConjuntaId
                            AND fechamentoSincronizado = 0
                            ");

            DbCommand command = _db.GetSqlStringCommand(sbSql.ToString());

            _db.AddInParameter(command, "@compraConjuntaId", DbType.Int32, compraConjunta.CompraConjuntaId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                PedidoCompraConjunta entidadeRetorno = new PedidoCompraConjunta();
                PopulaPedidoCompraConjunta(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }

            reader.Close();

            return entidadesRetorno;
        }
    }
}
