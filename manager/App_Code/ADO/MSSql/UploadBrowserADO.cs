using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Ag2.Manager.DAL;
using Ag2.Manager.BusinessObject;
using Ag2.Manager.Helper;
using System.Web.Security;
using System.Data.Common;
using System.Data;

namespace Ag2.Manager.ADO.MSSql
{
    /// <summary>
    /// Summary description for UploadBrowser
    /// </summary>
    public class UploadBrowserADO : BaseADO, IUploadBrowserDAL
    {
        public UploadBrowserADO()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void CarregarAquivos(List<Ag2.Manager.Entity.Arquivo> arquivos)
        {
            if (arquivos.Count > 0)
            {
                System.Text.StringBuilder sbSQL = new System.Text.StringBuilder();

                sbSQL.Append(" SELECT * FROM dbo.Arquivo ");

                for (int i = 0; i < arquivos.Count; i++)
                {
                    if (sbSQL.ToString().IndexOf(" WHERE ") > -1)
                        sbSQL.Append(" OR ");
                    else
                        sbSQL.Append(" WHERE ");

                    sbSQL.AppendFormat(" arquivoId = @arquivoId{0} ", i.ToString());
                }

                DbCommand cmdArquivo = db.GetSqlStringCommand(sbSQL.ToString());

                for (int i = 0; i < arquivos.Count; i++)
                {
                    db.AddInParameter(cmdArquivo, string.Format("@arquivoId{0}", i.ToString()), DbType.Int32, arquivos[i].arquivoId);
                }

                DataTable dt = db.ExecuteDataSet(cmdArquivo).Tables[0];

                cmdArquivo.Connection.Close();

                arquivos.Clear();

                Ag2.Manager.Entity.Arquivo arquivo = null;

                foreach (DataRow dr in dt.Rows)
                {
                    arquivo = new Ag2.Manager.Entity.Arquivo();
                    PopulaArquivo(arquivo, dr);
                    arquivos.Add(arquivo);
                }
            }
        }

        public List<Ag2.Manager.Entity.Arquivo> BuscaArquivoTags(List<Ag2.Manager.Entity.Tag> tags)
        {
            if (tags.Count > 0)
            {
                System.Text.StringBuilder sbSQL = new System.Text.StringBuilder();

                sbSQL.Append(" SELECT ");
                sbSQL.Append(" [arquivoId] ");
                sbSQL.Append(" ,[nomeArquivo] ");
                sbSQL.Append(" ,[tamanho] ");
                sbSQL.Append(" ,[extensao] ");
                sbSQL.Append(" ,[nomeOriginal] ");
                sbSQL.Append(" ,[dataCriacao] ");
                sbSQL.Append(" ,[titulo] ");
                sbSQL.Append(" FROM ");
                sbSQL.Append(" [dbo].[vwArquivoTag] ");

                for (int i = 0; i < tags.Count; i++)
                {
                    if (sbSQL.ToString().IndexOf(" WHERE ") > -1)
                        sbSQL.Append(" OR ");
                    else
                        sbSQL.Append(" WHERE ");

                    sbSQL.AppendFormat(" nome = @nome{0} ", i.ToString());
                }

                DbCommand cmdArquivo = db.GetSqlStringCommand(sbSQL.ToString());

                for (int i = 0; i < tags.Count; i++)
                {
                    db.AddInParameter(cmdArquivo, string.Format("@nome{0}", i.ToString()), DbType.String, tags[i].nome);
                }

                DataTable dt = db.ExecuteDataSet(cmdArquivo).Tables[0];
                cmdArquivo.Connection.Close();

                List<Ag2.Manager.Entity.Arquivo> lista = new List<Ag2.Manager.Entity.Arquivo>();
                Ag2.Manager.Entity.Arquivo arquivo = null;

                foreach (DataRow dr in dt.Rows)
                {
                    arquivo = new Ag2.Manager.Entity.Arquivo();
                    PopulaArquivo(arquivo, dr);
                    lista.Add(arquivo);
                }

                return lista;
            }
            else
                return null;
        }

        public void RenomearArquivo(Ag2.Manager.Entity.Arquivo arquivoOriginal, Ag2.Manager.Entity.Arquivo arquivoNovo)
        {
            DbCommand cmdArquivo = db.GetStoredProcCommand("[ArquivoRename]");
            db.AddInParameter(cmdArquivo, "@nomeArquivoOriginal", DbType.String, arquivoOriginal.nomeArquivo);
            db.AddInParameter(cmdArquivo, "@nomeArquivoNovo", DbType.String, arquivoNovo.nomeArquivo);

            db.ExecuteDataSet(cmdArquivo);
        }

        public Ag2.Manager.Entity.Arquivo CarregarAquivoByName(Ag2.Manager.Entity.Arquivo arquivo)
        {
            DbCommand cmdArquivo = db.GetStoredProcCommand("[ArquivoSelectByName]");
            db.AddInParameter(cmdArquivo, "@nomeArquivo", DbType.String, arquivo.nomeArquivo);

            DataTable dt = db.ExecuteDataSet(cmdArquivo).Tables[0];
            cmdArquivo.Connection.Close();

            if (dt.Rows.Count > 0)
            {
                PopulaArquivo(arquivo, dt.Rows[0]);
            }

            return arquivo;
        }

        public List<Ag2.Manager.Entity.Arquivo> CarregarAquivos(Ag2.Manager.Enumerator.tipoArquivoBrowser tipoArquivo)
        {
            DbCommand cmdArquivo = db.GetStoredProcCommand("[ArquivoSelect]");
            db.AddInParameter(cmdArquivo, "@tipoArquivo", DbType.String, tipoArquivo.ToString());

            DataTable dt = db.ExecuteDataSet(cmdArquivo).Tables[0];

            cmdArquivo.Connection.Close();

            List<Ag2.Manager.Entity.Arquivo> lista = new List<Ag2.Manager.Entity.Arquivo>();
            Ag2.Manager.Entity.Arquivo arquivo = null;

            foreach (DataRow dr in dt.Rows)
            {
                arquivo = new Ag2.Manager.Entity.Arquivo();
                PopulaArquivo(arquivo, dr);
                lista.Add(arquivo);
            }

            return lista;
        }

        private void PopulaArquivo(Ag2.Manager.Entity.Arquivo arquivo, DataRow dr)
        {
            arquivo.arquivoId = Convert.ToInt32(dr["arquivoId"]);
            arquivo.extensao = dr["extensao"].ToString();
            arquivo.nomeArquivo = dr["nomeArquivo"].ToString();
            arquivo.nomeOriginal = dr["nomeOriginal"].ToString();
            arquivo.titulo = dr["titulo"].ToString();
            arquivo.tamanho = Convert.ToInt64(dr["tamanho"]);
            arquivo.dataCriacao = Convert.ToDateTime(dr["dataCriacao"]);
        }

        public Ag2.Manager.Entity.Arquivo Save(Ag2.Manager.Entity.Arquivo arquivo)
        {
            DbCommand cmdArquivo = db.GetStoredProcCommand("[ArquivoInsert]");
            db.AddOutParameter(cmdArquivo, "@arquivoId", DbType.Int32, 0);
            db.AddInParameter(cmdArquivo, "@nomeArquivo", DbType.String, arquivo.nomeArquivo);
            db.AddInParameter(cmdArquivo, "@tamanho", DbType.Int64, arquivo.tamanho);
            db.AddInParameter(cmdArquivo, "@extensao", DbType.String, arquivo.extensao.Replace(".", string.Empty));
            db.AddInParameter(cmdArquivo, "@nomeOriginal", DbType.String, arquivo.nomeOriginal);
            db.AddInParameter(cmdArquivo, "@dataCriacao", DbType.DateTime, DateTime.Now);
            db.AddInParameter(cmdArquivo, "@titulo", DbType.String, arquivo.titulo);

            db.ExecuteNonQuery(cmdArquivo);

            arquivo.arquivoId = Convert.ToInt32(db.GetParameterValue(cmdArquivo, "@arquivoId"));

            cmdArquivo.Connection.Close();

            return arquivo;
        }

        public void Delete(Ag2.Manager.Entity.Arquivo arquivo)
        {
            DbConnection conn = db.CreateConnection();
            conn.Open();

            //INICIA UMA TRANSAÇÂO
            DbTransaction transaction = conn.BeginTransaction();
            DbCommand cmdArquivo = null;
            
            try
            {
                cmdArquivo = db.GetStoredProcCommand("[ArquivoBrowserDelete]");
                db.AddInParameter(cmdArquivo, "@nomeArquivo", DbType.String, arquivo.nomeArquivo);
                db.ExecuteNonQuery(cmdArquivo, transaction);

                //FINALIZA A TRANSAÇAO
                transaction.Commit();
            }
            catch (System.Exception ex)
            {
                //CASO DE ERRO DESFAZ TUDO
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                //FECHA CONEXAO COM A BASE
                conn.Close();
            }
        }

        public void SaveArquivoTags(Ag2.Manager.Entity.Arquivo arquivo, List<Ag2.Manager.Entity.Tag> tags)
        {
            DbConnection conn = db.CreateConnection();
            conn.Open();

            //INICIA UMA TRANSAÇÂO
            DbTransaction transaction = conn.BeginTransaction();
            DbCommand cmdArquivo = null;

            try
            {
                cmdArquivo = db.GetStoredProcCommand("[arquivoTagInsert]");
                db.AddInParameter(cmdArquivo, "@arquivoId", DbType.Int64, arquivo.arquivoId);
                db.AddInParameter(cmdArquivo, "@tagId", DbType.Int32, 0);

                foreach (Ag2.Manager.Entity.Tag tag in tags)
                {
                    db.SetParameterValue(cmdArquivo, "@tagId", tag.tagId);
                    db.ExecuteNonQuery(cmdArquivo, transaction);
                }

                //FINALIZA A TRANSAÇAO
                transaction.Commit();
            }
            catch (System.Exception ex)
            {
                //CASO DE ERRO DESFAZ TUDO
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                //FECHA CONEXAO COM A BASE
                conn.Close();
            }

        }
    }
}
