using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using GrupoA.BusinessObject;

namespace GrupoA.DataAccess.ADO
{
    public partial class RevistaADO : ADOSuper, IRevistaDAL
    {
        /// <summary>
        /// Método que carrega um Revista.
        /// </summary>
        /// <param name="entidade">Revista a ser carregado (somente o identificador é necessário).</param>
        /// <returns>Revista</returns>
        public Revista CarregarRevistaPorIssn(Revista entidade)
        {
            Revista entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM Revista WHERE issn=@issn");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@issn", DbType.String, entidade.ISSN);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Revista();
                PopulaRevista(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qtdRegistros"></param>
        /// <returns></returns>
        public List<Revista> CarregarRevistasPatio(Int32 qtdRegistros)
        {
            List<Revista> entidadesRetorno = new List<Revista>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(String.Format("SELECT TOP {0} * ", qtdRegistros));
            sbSQL.Append("FROM Revista ");
            sbSQL.Append("WHERE revistaId in (2,3,4) ");
            sbSQL.Append("ORDER BY NEWID() ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Revista entidadeRetorno = new Revista();
                PopulaRevista(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revistas"></param>
        /// <returns></returns>
        public List<Revista> CarregarTodasExcetoRevistas(List<Revista> revistas)
        {
            List<Revista> entidadeRetorno = new List<Revista>();
            String ids = "";
            foreach (Revista revista in revistas)
            {
                ids += string.Concat(",", revista.RevistaId);
            }

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(string.Concat("SELECT * FROM Revista WHERE revistaId NOT IN (0", ids, ")"));

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Revista revista = new Revista();
                PopulaRevista(reader, revista);
                entidadeRetorno.Add(revista);
            }
            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="promocao"></param>
        /// <returns></returns>
        public List<Revista> CarregarTodosPorPromocao(Promocao promocao)
        {
            List<Revista> entidadeRetorno = new List<Revista>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(" SELECT Revista.* FROM PromocaoRevista ");
            sbSQL.Append(" INNER JOIN Promocao ON PromocaoRevista.promocaoId = Promocao.promocaoId ");
            sbSQL.Append(" INNER JOIN Revista ON Revista.revistaId = PromocaoRevista.revistaId ");
            sbSQL.Append(" WHERE PromocaoRevista.promocaoId = @PromocaoId ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@PromocaoId", DbType.Int32, promocao.PromocaoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Revista revista = new Revista();
                PopulaRevista(reader, revista);
                entidadeRetorno.Add(revista);
            }
            reader.Close();

            return entidadeRetorno;
        }
    }
}
