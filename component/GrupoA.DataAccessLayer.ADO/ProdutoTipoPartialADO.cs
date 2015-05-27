
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
	public partial class ProdutoTipoADO : ADOSuper, IProdutoTipoDAL
    {

        #region public List<ProdutoTipo> CarregarTodosExcetoProdutoTipos(List<ProdutoTipo> produtoTipos)
        /// <summary>
        /// Método que carrega os Tipos de Produto excluindo os tipos de produto recebidos por parâmetro.
        /// </summary>
        /// <param name="produtoTipos">Tipos de Produto que não devem ser buscados (somente o identificador é necessário).</param>
        /// <returns>Coleção de Tipos de Produto</returns>
        public List<ProdutoTipo> CarregarTodosExcetoProdutoTipos(List<ProdutoTipo> produtoTipos)
        {

            List<ProdutoTipo> entidadeRetorno = new List<ProdutoTipo>();
            String ids = "";
            foreach (ProdutoTipo _produtoTipo in produtoTipos)
            {
                ids += string.Concat(",", _produtoTipo.ProdutoTipoId);
            }

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(string.Concat("SELECT * FROM ProdutoTipo WHERE produtoTipoId NOT IN (0", ids, ")"));

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            //_db.AddInParameter(command, "@Ids", DbType.Int64, ids);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                ProdutoTipo _produtoTipo = new ProdutoTipo();
                PopulaProdutoTipo(reader, _produtoTipo);
                entidadeRetorno.Add(_produtoTipo);
            }
            reader.Close();

            return entidadeRetorno;
        }
        #endregion

        #region public List<ProdutoTipo> CarregarPorPromocao(Promocao promocao)
        /// <summary>
        /// Carrega uma coleção de Tipos de Produto por Promoção conforme o código identificador recebido.
        /// </summary>
        /// <param name="promocao">Objeto Promocao que deverá ser carregado os Tipos de Produto (somente o código identificador é utilizado).</param>
        /// <returns>Coleção de Tipos de Produto da Promoção</returns>
        public List<ProdutoTipo> CarregarPorPromocao(Promocao promocao)
        {
            List<ProdutoTipo> entidadeRetorno = new List<ProdutoTipo>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(" SELECT p.* FROM PromocaoProdutoTipo pp ");
            sbSQL.Append(" INNER JOIN ProdutoTipo p ON p.produtoTipoId = pp.produtoTipoId ");
            sbSQL.Append(" WHERE pp.promocaoId = @promocaoId ");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@promocaoId", DbType.Int32, promocao.PromocaoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                ProdutoTipo _produtoTipo = new ProdutoTipo();
                PopulaProdutoTipo(reader, _produtoTipo);
                entidadeRetorno.Add(_produtoTipo);
            }
            reader.Close();

            return entidadeRetorno;
        }
        #endregion

    }
}
		