using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

namespace Ag2.Manager.ADO.Oracle
{
    /// <summary>
    /// Summary description for IdiomaADO
    /// </summary>
    public class IdiomaADO : BaseADO, Ag2.Manager.DAL.IIdiomaDAL
    {
        public IdiomaADO()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Atualiza o idioma
        /// </summary>
        /// <param name="idioma"></param>
        public void IdiomaUpdate(Ag2.Manager.Entity.Idioma idioma)
        {
            DbCommand cmd = db.GetStoredProcCommand("[idiomaUpdate]");
            db.AddInParameter(cmd, "@idiomaId", DbType.Int32, idioma.idiomaId);
            db.AddInParameter(cmd, "@active", DbType.Boolean, idioma.active);
            db.ExecuteNonQuery(cmd);
            cmd.Connection.Close();
        }

        /// <summary>
        /// Carrega idioma
        /// </summary>
        /// <param name="idioma"></param>
        /// <returns></returns>
        public Ag2.Manager.BusinessObject.Idioma LoadIdioma(Ag2.Manager.BusinessObject.Idioma idioma)
        {
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append(" SELECT * FROM idioma WHERE idiomaId = :idiomaId ");

            DbCommand cmd = db.GetSqlStringCommand(sbSQL.ToString());
            db.AddInParameter(cmd, ":idiomaId", DbType.Int32, idioma.IdiomaId);

            IDataReader dr = db.ExecuteReader(cmd);

            try
            {
                if (dr.Read())
                {
                    idioma.IdiomaId = Convert.ToInt32(dr["IDIOMAID"]);
                    idioma.Active = Convert.ToBoolean(dr["ACTIVE"]);
                    idioma.Default = Convert.ToBoolean(dr["IDIOMA_DEFAULT"]);
                    idioma.Flag = dr["FLAG"].ToString();
                    idioma.Name = dr["NAME"].ToString();
                }
            }
            catch
            {
                //
            }
            finally
            {
                cmd.Connection.Close();
                dr.Close();
            }

            return idioma;
        }

        /// <summary>
        /// Retorna os idiomas ativos
        /// </summary>
        /// <returns></returns>
        public List<Ag2.Manager.BusinessObject.Idioma> GetActiveIdiomas()
        {            
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append(" SELECT * FROM idioma WHERE Active = 1 ORDER BY name ");

            DbCommand cmd = db.GetSqlStringCommand(sbSQL.ToString());

            IDataReader dr = db.ExecuteReader(cmd);

            List<Ag2.Manager.BusinessObject.Idioma> idiomas = new List<Ag2.Manager.BusinessObject.Idioma>();
            Ag2.Manager.BusinessObject.Idioma idioma = null;

            try
            {
                while (dr.Read())
                {
                    idioma = new Ag2.Manager.BusinessObject.Idioma();
                    idioma.IdiomaId = Convert.ToInt32(dr["IDIOMAID"]);
                    idioma.Active = Convert.ToBoolean(dr["ACTIVE"]);
                    idioma.Default = Convert.ToBoolean(dr["IDIOMA_DEFAULT"]);
                    idioma.Flag = dr["FLAG"].ToString();
                    idioma.Name = dr["NAME"].ToString();

                    idiomas.Add(idioma);
                }
            }
            catch
            {
                //
            }
            finally
            {
                cmd.Connection.Close();
                dr.Close();
            }

            return idiomas;
        }

        /// <summary>
        /// Retorna idioma default do manager
        /// </summary>
        /// <returns></returns>
        public Ag2.Manager.BusinessObject.Idioma GetIdiomaDefault()
        {
            Ag2.Manager.BusinessObject.Idioma idioma = null;
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append(" SELECT * FROM idioma WHERE idioma_Default = 1 ");

            DbCommand cmd = db.GetSqlStringCommand(sbSQL.ToString());

            IDataReader dr = db.ExecuteReader(cmd);

            try
            {
                if (dr.Read())
                {
                    idioma = new Ag2.Manager.BusinessObject.Idioma();
                    idioma.IdiomaId = Convert.ToInt32(dr["IdiomaId"]);
                    idioma.Active = Convert.ToBoolean(dr["Active"]);
                    idioma.Default = Convert.ToBoolean(dr["idioma_Default"]);
                    idioma.Flag = dr["Flag"].ToString();
                    idioma.Name = dr["Name"].ToString();
                }
            }
            catch
            {
                //
            }
            finally
            {
                cmd.Connection.Close();
                dr.Close();
            }

            return idioma;
        }

        /// <summary>
        /// Carrega todos do idiomas da base de dados
        /// </summary>
        /// <returns></returns>
        public List<Ag2.Manager.Entity.Idioma> GetIdiomas()
        {
            DbCommand cmdArquivo = db.GetStoredProcCommand("[IdiomaSelect]");

            DataTable dt = db.ExecuteDataSet(cmdArquivo).Tables[0];

            cmdArquivo.Connection.Close();

            List<Ag2.Manager.Entity.Idioma> lista = new List<Ag2.Manager.Entity.Idioma>();
            Ag2.Manager.Entity.Idioma idioma = null;

            foreach (DataRow dr in dt.Rows)
            {
                idioma = new Ag2.Manager.Entity.Idioma();
                PopulaIdioma(idioma, dr);
                lista.Add(idioma);
            }

            return lista;
        }

        private void PopulaIdioma(Ag2.Manager.Entity.Idioma idioma, DataRow dr)
        {
            idioma.idiomaId = Convert.ToInt32(dr["idiomaId"]);
            idioma.active = Convert.ToBoolean(dr["active"].ToString());
            idioma.Default = Convert.ToBoolean(dr["Default"].ToString());
            idioma.flag = dr["flag"].ToString();
            idioma.name = dr["name"].ToString();
        }
    }
}
