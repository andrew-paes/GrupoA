
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
    public partial class PedidoItemADO : ADOSuper, IPedidoItemDAL
    {


        /// <summary>
        /// Carrega Itens de um determinado pedido
        /// </summary>
        /// <param name="entidade">Objeto Pedido</param>
        /// <returns></returns>
        public List<PedidoItem> CarregarItensDoPedido(Pedido entidade)
        {
            List<PedidoItem> entidadesRetorno = new List<PedidoItem>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT PedidoItem.* FROM PedidoItem WHERE PedidoItem.pedidoId=@pedidoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@pedidoId", DbType.Int32, entidade.PedidoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                PedidoItem entidadeRetorno = new PedidoItem();
                PopulaPedidoItem(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }

            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pedidoBO"></param>
        /// <param name="produtoBO"></param>
        /// <returns></returns>
        public PedidoItem Carregar(Pedido pedidoBO, Produto produtoBO)
        {
            PedidoItem entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM PedidoItem WHERE pedidoId = @pedidoId AND produtoId = @produtoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@pedidoId", DbType.Int32, pedidoBO.PedidoId);
            _db.AddInParameter(command, "@produtoId", DbType.Int32, produtoBO.ProdutoId);
            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new PedidoItem();
                PopulaPedidoItem(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }
    }
}