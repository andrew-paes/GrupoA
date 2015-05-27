
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
    public partial class CarrinhoItemADO : ADOSuper, ICarrinhoItemDAL
    {

        /// <summary>
        /// Método que persiste um CarrinhoItem.
        /// </summary>
        /// <param name="entidade">CarrinhoItem contendo os dados a serem persistidos.</param>	
        public void IncrementarQuantidade(CarrinhoItem entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" UPDATE CarrinhoItem ");
            sbSQL.Append(" SET quantidade = quantidade + 1 ");
            sbSQL.Append(" WHERE ");
            sbSQL.Append(" carrinhoId = @carrinhoId AND produtoId = @produtoId ");
            if (entidade.CarrinhoItemCompraConjunta != null)
            {
                sbSQL.Append(" AND ((SELECT COUNT(CarrinhoItemCompraConjuntaId) FROM CarrinhoItemCompraConjunta Where CarrinhoItemCompraConjuntaId = CarrinhoItemId) > 0) ");
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@carrinhoId", DbType.Int32, entidade.Carrinho.CarrinhoId);

            _db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.Produto.ProdutoId);

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        public void AtualizarQuantidade(CarrinhoItem entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" UPDATE CarrinhoItem ");
            sbSQL.Append(" SET quantidade = @Quantidade ");
            sbSQL.Append(" WHERE ");
            sbSQL.Append(" carrinhoId = @carrinhoId AND produtoId = @produtoId ");
            if (entidade.CarrinhoItemCompraConjunta != null)
            {
                sbSQL.Append(" AND ((SELECT COUNT(CarrinhoItemCompraConjuntaId) FROM CarrinhoItemCompraConjunta Where CarrinhoItemCompraConjuntaId = CarrinhoItemId) > 0) ");
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@carrinhoId", DbType.Int32, entidade.Carrinho.CarrinhoId);

            _db.AddInParameter(command, "@Quantidade", DbType.Int32, entidade.Quantidade);

            _db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.Produto.ProdutoId);

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }


        /// <summary>
        /// Método que remove os carrinhos itens da base de dados.
        /// </summary>
        /// <param name="entidade">Carrinho a ser excluído.</param>		
        public void ExcluirPorCarrinho(Carrinho entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM CarrinhoItem ");
            sbSQL.Append("WHERE carrinhoId=@CarrinhoId");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@CarrinhoId", DbType.Int32, entidade.CarrinhoId);


            _db.ExecuteNonQuery(command);
        }

        public void ExcluirDesativadosPorCarrinho(Carrinho entidade, string itensExcluidosCompraConjunta)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM CarrinhoItem ");
            sbSQL.Append("WHERE carrinhoItemId IN ( ");
            if (itensExcluidosCompraConjunta.Length > 0)
            {
                sbSQL.AppendFormat("{0}) OR carrinhoItemId IN (", itensExcluidosCompraConjunta);
            }
            sbSQL.Append("SELECT ci.CarrinhoItemId FROM Produto p ");
            sbSQL.Append("JOIN CarrinhoItem ci ON ci.produtoId = p.produtoId AND ci.carrinhoId = @CarrinhoId WHERE (p.exibirSite = 0 OR p.homologado = 0)");
            sbSQL.Append(")");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@CarrinhoId", DbType.Int32, entidade.CarrinhoId);


            _db.ExecuteNonQuery(command);
        }

        public int TotalRegistrosComCompraConjunta(CarrinhoItem carrinhoItem)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) AS Total FROM CarrinhoItem");
            sbSQL.Append(" WHERE ");
            sbSQL.Append(" carrinhoId = @carrinhoId AND produtoId = @produtoId ");
            sbSQL.AppendFormat(" AND ((SELECT COUNT(CarrinhoItemCompraConjuntaId) FROM CarrinhoItemCompraConjunta Where CarrinhoItemCompraConjuntaId = CarrinhoItemId) {0} 0) ", (carrinhoItem.CarrinhoItemCompraConjunta != null ? ">" : "="));

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@produtoId", DbType.Int32, carrinhoItem.Produto.ProdutoId);
            _db.AddInParameter(command, "@carrinhoId", DbType.Int32, carrinhoItem.Carrinho.CarrinhoId);

            // Executa a query.

            int resultado = (int)_db.ExecuteScalar(command);


            return resultado;
        }
    }
}
