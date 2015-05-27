
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
	public partial class CarrinhoItemCompraConjuntaADO : ADOSuper, ICarrinhoItemCompraConjuntaDAL {

        /// <summary>
        /// Método que remove os carrinhos itens da base de dados.
        /// </summary>
        /// <param name="entidade">Carrinho a ser excluído.</param>		
        public void ExcluirPorCarrinho(Carrinho entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM CarrinhoItemCompraConjunta ");
            sbSQL.Append("WHERE CarrinhoItemCompraConjuntaId IN (SELECT cicc.CarrinhoItemCompraConjuntaId FROM CarrinhoItemCompraConjunta  cicc ");
            sbSQL.Append("                     JOIN CarrinhoItem ci ON ci.CarrinhoItemId = cicc.CarrinhoItemCompraConjuntaId ");
            sbSQL.Append("                                         AND ci.carrinhoId = @CarrinhoId )");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@CarrinhoId", DbType.Int32, entidade.CarrinhoId);


            _db.ExecuteNonQuery(command);
        }

        public List<CarrinhoItemCompraConjunta> CarregarPorCompraConjuntaIndisponivel(Carrinho carrinho)
        {
            List<CarrinhoItemCompraConjunta> carrinhoItensCompraConjunta = new List<CarrinhoItemCompraConjunta>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(" SELECT cicc.* FROM CompraConjunta cc ");
            sbSQL.Append(" JOIN Produto p ON p.produtoId = cc.produtoId ");
            sbSQL.Append(" JOIN CarrinhoItem ci ON ci.produtoId = p.produtoId AND ci.carrinhoId = @carrinhoId ");
            sbSQL.Append(" JOIN carrinhoitemcompraconjunta cicc ON cicc.carrinhoitemcompraconjuntaId = ci.CarrinhoItemId ");
            sbSQL.Append(" and cicc.compraconjuntaid = cc.compraconjuntaid ");
            sbSQL.Append(" WHERE cc.compraConjuntaStatusId <> 1 ");
            sbSQL.Append(" OR cc.ativa = 0 OR cc.dataHoraFinalizacao IS NOT NULL ");
            sbSQL.Append(" OR (cc.dataInicialCompra >= GETDATE() AND cc.dataFinalCompra <= GETDATE()) ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@carrinhoId", DbType.Int32, carrinho.CarrinhoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                CarrinhoItemCompraConjunta carrinhoItemCompraConjunta = new CarrinhoItemCompraConjunta();
                PopulaCarrinhoItemCompraConjunta(reader, carrinhoItemCompraConjunta);
                carrinhoItensCompraConjunta.Add(carrinhoItemCompraConjunta);
            }
            reader.Close();

            return carrinhoItensCompraConjunta;        
        }

        public string ExcluirMultiplos(List<CarrinhoItemCompraConjunta> carrinhoitensCompraConjunta)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM CarrinhoItemCompraConjunta ");
            sbSQL.Append("WHERE CarrinhoItemCompraConjuntaId in (@carrinhoItemCompraConjuntaIds)");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            string idsCarrinhoItemCompraConjunta = string.Empty;
            foreach (CarrinhoItemCompraConjunta item in carrinhoitensCompraConjunta)
            {
                if (idsCarrinhoItemCompraConjunta.Length > 0)
                {
                    idsCarrinhoItemCompraConjunta += ",";
                }
                idsCarrinhoItemCompraConjunta += item.CarrinhoItemCompraConjuntaId;
            }

            _db.AddInParameter(command, "@carrinhoItemCompraConjuntaIds", DbType.String, idsCarrinhoItemCompraConjunta);


            _db.ExecuteNonQuery(command);

            return idsCarrinhoItemCompraConjunta;
        }
	}
}
		