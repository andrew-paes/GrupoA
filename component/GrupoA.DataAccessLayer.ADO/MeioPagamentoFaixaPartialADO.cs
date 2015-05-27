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
    public partial class MeioPagamentoFaixaADO : ADOSuper, IMeioPagamentoFaixaDAL
    {
        #region Métodos de MeioPagamentoFaixa

        /// <summary>
        /// Método que remove da base de dados lista de MeioPagamentoFaixa relacionados com MeioPagamento.
        /// </summary>
        /// <param name="entidade">MeioPagamento que contém a lista de Faixas a ser excluído (somente o identificador é necessário).</param>		
        public void ExcluirRelacionado(MeioPagamento entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append(@"DELETE FROM 
									MeioPagamentoFaixa 
							WHERE 
									meioPagamentoId=@meioPagamentoId
						");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@meioPagamentoId", DbType.Int32, entidade.MeioPagamentoId);

            _db.ExecuteNonQuery(command);
        }

        public List<MeioPagamentoFaixa> CarregarMeiosPagamentoFaixaMaioresQueValor(MeioPagamento meioPagamento, double valorMinimo)
        {
            List<MeioPagamentoFaixa> meioPagamentoFaixas = new List<MeioPagamentoFaixa>();

            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("SELECT * FROM meiopagamentofaixa ");
            sbSQL.Append("WHERE valorMinimo <= @valorMinimo ");
            sbSQL.Append("AND meioPagamentoId = @meioPagamentoId ");
            sbSQL.Append(" ORDER BY numeroParcelas ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@meioPagamentoId", DbType.Int32, meioPagamento.MeioPagamentoId);
            _db.AddInParameter(command, "@valorMinimo", DbType.Double, valorMinimo);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                MeioPagamentoFaixa meioPagamentoFaixa = new MeioPagamentoFaixa();
                MeioPagamentoFaixaADO.PopulaMeioPagamentoFaixa(reader, meioPagamentoFaixa);
                meioPagamentoFaixas.Add(meioPagamentoFaixa);
            }
            reader.Close();

            return meioPagamentoFaixas;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public MeioPagamentoFaixa CarregarPorPagamento(Pagamento entidade)
        {
            MeioPagamentoFaixa entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM MeioPagamentoFaixa WHERE meioPagamentoId = @meioPagamentoId AND numeroParcelas = @numeroParcelas");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@meioPagamentoId", DbType.Int32, entidade.MeioPagamento.MeioPagamentoId);
            _db.AddInParameter(command, "@numeroParcelas", DbType.Int32, entidade.NumeroParcelas);
            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new MeioPagamentoFaixa();
                PopulaMeioPagamentoFaixa(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }

        #endregion
    }
}