
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
    public partial class OfertaADO : ADOSuper, IOfertaDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Oferta> CarregarOfertasAplicaveis()
        {
            List<Oferta> entidadesRetorno = new List<Oferta>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT *  ");
            sbSQL.Append("FROM Oferta ");
            sbSQL.Append("WHERE GETDATE() >= dataHoraInicio ");
            sbSQL.Append("AND GETDATE() <= dataHoraTermino ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Oferta entidadeRetorno = new Oferta();
                PopulaOferta(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oferta"></param>
        /// <param name="categorias"></param>
        /// <param name="produtos"></param>
        /// <returns></returns>
        public Int32 ValidarOferta(Oferta oferta, List<Categoria> categorias, List<Produto> produtos)
        {
            StringBuilder sbSQL = new StringBuilder();
            StringBuilder sbSQLWhere = new StringBuilder();

            sbSQL.Append("SELECT COUNT(Oferta.ofertaId) AS Qtd ");
            sbSQL.Append("FROM Oferta ");

            if (oferta.OfertaTipo.OfertaTipoId == 1)
            {
                sbSQL.Append("INNER JOIN OfertaProduto ");
                sbSQL.Append("    ON Oferta.ofertaId = OfertaProduto.ofertaId ");
                sbSQL.Append("LEFT JOIN ProdutoCategoria ");
                sbSQL.Append("    ON OfertaProduto.produtoId = ProdutoCategoria.produtoId ");
                sbSQL.Append("LEFT JOIN OfertaCategoria ");
                sbSQL.Append("    ON ProdutoCategoria.categoriaId = OfertaCategoria.categoriaId ");
                sbSQL.Append("WHERE OfertaProduto.produtoId IN (");

                for (int i = 0; i < produtos.Count; i++)
                {
                    sbSQLWhere.Append(String.Concat("@produtoId", i, ","));
                }

                sbSQL.Append(sbSQLWhere.ToString(0, sbSQLWhere.Length - 1));
                sbSQL.Append(") AND ");
            }
            else if (oferta.OfertaTipo.OfertaTipoId == 2)
            {
                sbSQL.Append("WHERE ");
            }
            else if (oferta.OfertaTipo.OfertaTipoId == 3)
            {
                sbSQL.Append("INNER JOIN OfertaCategoria ");
	            sbSQL.Append("    ON Oferta.ofertaId = OfertaCategoria.ofertaId ");
                sbSQL.Append("LEFT JOIN ProdutoCategoria ");
	            sbSQL.Append("    ON OfertaCategoria.categoriaId = ProdutoCategoria.categoriaId ");
                sbSQL.Append("LEFT JOIN OfertaProduto ");
	            sbSQL.Append("    ON ProdutoCategoria.produtoId = OfertaProduto.produtoId ");
                sbSQL.Append("WHERE OfertaCategoria.categoriaId IN (");

                for (int i = 0; i < categorias.Count; i++)
                {
                    sbSQLWhere.Append(String.Concat("@categoriaId", i, ","));
                }

                sbSQL.Append(sbSQLWhere.ToString(0, sbSQLWhere.Length - 1));
                sbSQL.Append(") AND ");
            }

            sbSQL.Append("    ((Oferta.dataHoraInicio <= @dataHoraInicio AND Oferta.dataHoraTermino >= @dataHoraInicio) ");
            sbSQL.Append("    OR (Oferta.dataHoraInicio <= @dataHoraTermino AND Oferta.dataHoraTermino >= @dataHoraTermino)) ");

            if (oferta.OfertaId > 0)
            {
                sbSQL.Append("    AND Oferta.ofertaId <> @ofertaId ");
            }

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@dataHoraInicio", DbType.DateTime, oferta.DataHoraInicio);
            _db.AddInParameter(command, "@dataHoraTermino", DbType.DateTime, oferta.DataHoraTermino);

            if (oferta.OfertaTipo.OfertaTipoId == 1)
            {
                for (int i = 0; i < produtos.Count; i++)
                {
                    _db.AddInParameter(command, "@produtoId" + i, DbType.Int32, produtos[i].ProdutoId);
                }
            }
            else if (oferta.OfertaTipo.OfertaTipoId == 3)
            {
                for (int i = 0; i < categorias.Count; i++)
                {
                    _db.AddInParameter(command, "@categoriaId" + i, DbType.Int32, categorias[i].CategoriaId);
                }
            }

            if (oferta.OfertaId > 0)
            {
                _db.AddInParameter(command, "@ofertaId", DbType.Int32, oferta.OfertaId);
            }

            int resultado = (int)_db.ExecuteScalar(command);

            return resultado;
        }
    }
}
