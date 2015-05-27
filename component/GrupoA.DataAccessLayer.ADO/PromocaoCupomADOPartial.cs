
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
    public partial class PromocaoCupomADO : ADOSuper, IPromocaoCupomDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="promocao"></param>
        public void Inserir(Promocao promocao)
        {
            DbCommand command;

            command = _db.GetStoredProcCommand("GerarCuponsPromocao");

            _db.AddInParameter(command, "@promocaoId", DbType.Int32, promocao.PromocaoId);
            _db.AddInParameter(command, "@reutilizavel", DbType.Boolean, promocao.Reutilizavel);
            _db.AddInParameter(command, "@qtdCupons", DbType.Int32, promocao.NumeroMaximoCupomDif);

            // Executa a query.
            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public PromocaoCupom CarregarPorPromocao(Promocao entidade)
        {
            PromocaoCupom entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT TOP 1 PromocaoCupom.* FROM PromocaoCupom WHERE PromocaoCupom.promocaoId=@promocaoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@promocaoId", DbType.Int32, entidade.PromocaoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new PromocaoCupom();
                PopulaPromocaoCupom(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }

        public PromocaoCupom CarregarPorCodigoCupom(String codigoCupom)
        {
            PromocaoCupom entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT PromocaoCupom.* FROM PromocaoCupom WHERE PromocaoCupom.codigoCupom = @codigoCupom");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@codigoCupom", DbType.Guid, new Guid(codigoCupom));

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new PromocaoCupom();
                PopulaPromocaoCupom(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }

        public PromocaoCupom CarregarPorCodigoAmigavel(Int32? promocaoCupomId, String codigoAmigavel)
        {
            PromocaoCupom entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT PromocaoCupom.* FROM PromocaoCupom WHERE ");
            
            if (promocaoCupomId != null)
            {
                sbSQL.Append("PromocaoCupom.promocaoCupomId <> @promocaoCupomId AND  ");
            }

            sbSQL.Append("PromocaoCupom.codigoAmigavel = @codigoAmigavel");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            if (promocaoCupomId != null)
            {
                _db.AddInParameter(command, "@promocaoCupomId", DbType.Int32, promocaoCupomId);
            }
            _db.AddInParameter(command, "@codigoAmigavel", DbType.String, codigoAmigavel);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new PromocaoCupom();
                PopulaPromocaoCupom(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }
    }
}
