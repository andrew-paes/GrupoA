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
    public partial class PromocaoCupomPedidoADO : ADOSuper, IPromocaoCupomPedidoDAL
    {
        /// <summary>
        /// Método que retorna uma coleção de PromocaoCupomPedido.
        /// </summary>
        /// <param name="entidade">Pedido relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de PromocaoCupomPedido.</returns>
        public List<PromocaoCupomPedido> CarregarPromocaoCupomPedidoPorPromocao(Promocao promocao)
        {
            List<PromocaoCupomPedido> entidadesRetorno = new List<PromocaoCupomPedido>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT PromocaoCupom.codigoCupom, ");
            sbSQL.Append("    PromocaoCupom.codigoAmigavel, ");
            sbSQL.Append("    PromocaoCupom.promocaoCupomId, ");
	        sbSQL.Append("    Pedido.pedidoId, ");
	        sbSQL.Append("    Usuario.nomeUsuario, ");
	        sbSQL.Append("    Usuario.usuarioId ");
            sbSQL.Append("FROM PromocaoCupom ");
            sbSQL.Append("LEFT JOIN PromocaoCupomPedido ");
	        sbSQL.Append("    ON PromocaoCupom.promocaoCupomId = PromocaoCupomPedido.promocaoCupomId ");
            sbSQL.Append("LEFT JOIN Pedido ");
	        sbSQL.Append("    ON PromocaoCupomPedido.pedidoId = Pedido.pedidoId ");
            sbSQL.Append("LEFT JOIN Usuario ");
	        sbSQL.Append("    ON Pedido.usuarioId = Usuario.usuarioId ");
            sbSQL.Append("WHERE PromocaoCupom.promocaoId = @promocaoId ");
            sbSQL.Append("ORDER BY Pedido.pedidoId ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@promocaoId", DbType.Int32, promocao.PromocaoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                PromocaoCupomPedido entidadeRetorno = new PromocaoCupomPedido();
                PopulaPromocaoCupomPedidoComDependencia(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna popula um PromocaoCupomPedido baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">PromocaoCupomPedido a ser populado(.</param>
        public static void PopulaPromocaoCupomPedidoComDependencia(IDataReader reader, PromocaoCupomPedido entidade)
        {
            if (reader["promocaoCupomId"] != DBNull.Value)
            {
                if (entidade.PromocaoCupom == null) entidade.PromocaoCupom = new PromocaoCupom();
                entidade.PromocaoCupom.PromocaoCupomId = Convert.ToInt32(reader["promocaoCupomId"].ToString());
            }

            if (reader["codigoCupom"] != DBNull.Value)
            {
                if (entidade.PromocaoCupom == null) entidade.PromocaoCupom = new PromocaoCupom();
                entidade.PromocaoCupom.CodigoCupom = new Guid(reader["codigoCupom"].ToString());
            }

            if (reader["codigoAmigavel"] != DBNull.Value)
            {
                if (entidade.PromocaoCupom == null) entidade.PromocaoCupom = new PromocaoCupom();
                entidade.PromocaoCupom.CodigoAmigavel = reader["codigoAmigavel"].ToString();
            }

            if (reader["pedidoId"] != DBNull.Value)
            {
                if (entidade.Pedido == null) entidade.Pedido = new Pedido();
                entidade.Pedido.PedidoId = Convert.ToInt32(reader["pedidoId"].ToString());
            }

            if (reader["usuarioId"] != DBNull.Value)
            {
                if (entidade.Pedido == null) entidade.Pedido = new Pedido();
                if (entidade.Pedido.Usuario == null) entidade.Pedido.Usuario = new Usuario();
                entidade.Pedido.Usuario.UsuarioId = Convert.ToInt32(reader["usuarioId"].ToString());
            }

            if (reader["nomeUsuario"] != DBNull.Value)
            {
                if (entidade.Pedido == null) entidade.Pedido = new Pedido();
                if (entidade.Pedido.Usuario == null) entidade.Pedido.Usuario = new Usuario();
                entidade.Pedido.Usuario.NomeUsuario = reader["nomeUsuario"].ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="codigoCupom"></param>
        /// <returns></returns>
        public Int32 TotalRegistrosPorCodigoCupom(String codigoCupom)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) AS Total ");
            sbSQL.Append("FROM PromocaoCupomPedido ");
            sbSQL.Append("INNER JOIN PromocaoCupom ");
	        sbSQL.Append("    ON PromocaoCupomPedido.promocaoCupomId = PromocaoCupom.promocaoCupomId ");
            sbSQL.Append("WHERE PromocaoCupom.codigoCupom = @codigoCupom ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@codigoCupom", DbType.Guid, new Guid(codigoCupom));
            // Executa a query.

            int resultado = (int)_db.ExecuteScalar(command);


            return resultado;
        }
    }
}
