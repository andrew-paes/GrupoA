using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Ag2.Manager.DAL;
using Ag2.Manager.BusinessObject;
using Ag2.Manager.Helper;
using System.Web.Security;

namespace Ag2.Manager.ADO.MSSql
{
    /// <summary>
    /// Summary description for PerfilADO
    /// </summary>
    public class SecaoConteudoADO : BaseADO, ISecaoConteudo
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SecaoConteudoADO()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public Manager.Entity.Secao CarregarSecao(Manager.Entity.Secao secao)
        {
            DbCommand cmdSecao = db.GetStoredProcCommand("[SecaoCarregar]");
            db.AddInParameter(cmdSecao, "@idiomaId", DbType.Int32, new Manager.Helper.CurrentSessions().CurrentIdioma.IdiomaId);
            db.AddInParameter(cmdSecao, "@SecaoId", DbType.Int32, secao.SecaoId);

            DataTable dt = db.ExecuteDataSet(cmdSecao).Tables[0];
            cmdSecao.Connection.Close();

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                if (dr["AcessoRapido"] != DBNull.Value)
                    secao.AcessoRapido = Convert.ToBoolean(dr["AcessoRapido"]);
                if (dr["Ativo"] != DBNull.Value)
                    secao.Ativo = Convert.ToBoolean(dr["Ativo"]);
                if (dr["AvaliacaoSomaNotas"] != DBNull.Value)
                    secao.AvaliacaoSomaNotas = Convert.ToInt64(dr["AvaliacaoSomaNotas"]);
                if (dr["Avaliacoes"] != DBNull.Value)
                    secao.Avaliacoes = Convert.ToInt64(dr["Avaliacoes"]);
                if (dr["Avaliar"] != DBNull.Value)
                    secao.Avaliar = Convert.ToBoolean(dr["Avaliar"]);
                if (dr["Comentar"] != DBNull.Value)
                    secao.Comentar = Convert.ToBoolean(dr["Comentar"]);
                if (dr["Compartilhar"] != DBNull.Value)
                    secao.Compartilhar = Convert.ToBoolean(dr["Compartilhar"]);
                if (dr["EstadoPublicacao"] != DBNull.Value)
                    secao.EstadoPublicacao = dr["EstadoPublicacao"].ToString();
                if (dr["Excluido"] != DBNull.Value)
                    secao.Excluido = Convert.ToBoolean(dr["Excluido"]);
                if (dr["ExibeNoMenu"] != DBNull.Value)
                    secao.ExibeNoMenu = Convert.ToBoolean(dr["ExibeNoMenu"]);
                if (dr["HabilitaBoxRSS"] != DBNull.Value)
                    secao.HabilitaBoxRSS = Convert.ToBoolean(dr["HabilitaBoxRSS"]);
                if (dr["IdiomaId"] != DBNull.Value)
                    secao.IdiomaId = Convert.ToInt32(dr["IdiomaId"]);
                if (dr["ModeloId"] != DBNull.Value)
                    secao.ModeloId = Convert.ToInt32(dr["ModeloId"]);
                if (dr["Ordem"] != DBNull.Value)
                    secao.Ordem = Convert.ToInt32(dr["Ordem"]);
                if (dr["PalavraChave"] != DBNull.Value)
                    secao.PalavraChave = dr["PalavraChave"].ToString();
                if (dr["RedirecionaId"] != DBNull.Value)
                    secao.RedirecionaId = Convert.ToInt32(dr["RedirecionaId"]);
                if (dr["SecaoAutenticada"] != DBNull.Value)
                    secao.SecaoAutenticada = Convert.ToBoolean(dr["SecaoAutenticada"]);
                if (dr["SecaoId"] != DBNull.Value)
                    secao.SecaoId = Convert.ToInt32(dr["SecaoId"]);
                if (dr["SecaoIdPai"] != DBNull.Value)
                    secao.SecaoIdPai = Convert.ToInt32(dr["SecaoIdPai"]);
                if (dr["Texto"] != DBNull.Value)
                    secao.Texto = dr["Texto"].ToString();
                if (dr["TextoArquivos"] != DBNull.Value)
                    secao.TextoArquivos = dr["TextoArquivos"].ToString();
                if (dr["Titulo"] != DBNull.Value)
                    secao.Titulo = dr["Titulo"].ToString();
                if (dr["TituloArquivos"] != DBNull.Value)
                    secao.TituloArquivos = dr["TituloArquivos"].ToString();
                if (dr["TituloMenu"] != DBNull.Value)
                    secao.TituloMenu = dr["TituloMenu"].ToString();
            }

            return secao;
        }

        public void DesativarAtivarSecao(Manager.Entity.Secao secao)
        {
            DbCommand cmdSecao = db.GetStoredProcCommand("[SecaoAtivaDesativa]");
            db.AddInParameter(cmdSecao, "@secaoId", DbType.Int32, secao.SecaoId);
            db.AddInParameter(cmdSecao, "@ativo", DbType.Int32, secao.Ativo);

            db.ExecuteNonQuery(cmdSecao);
        }

        public Manager.Entity.Secao Salvar(Manager.Entity.Secao secao)
        {
            DbConnection conn = db.CreateConnection();
            conn.Open();

            //INICIA UMA TRANSAÇÂO
            DbTransaction transaction = conn.BeginTransaction();
            DbCommand cmdSecao = null;

            try
            {
                //VERIFICA QUAL A ULTIMA ORDEM INSERIDA NA BASE PARA INSERIR UMA NOVA ORDEM
                cmdSecao = db.GetStoredProcCommand("[VerificaUltimaOrdemSecao]");
                db.AddInParameter(cmdSecao, "@SecaoId", DbType.Int32, secao.SecaoId);
                db.AddInParameter(cmdSecao, "@secaoIdPai", DbType.Int32, secao.SecaoIdPai);

                //VERIFICA ULTIMA ORDEM E SOMA 1
                secao.Ordem = Convert.ToInt32(db.ExecuteScalar(cmdSecao, transaction)) + 1;

                //SALVA NA TABELA SECAO
                if (secao.SecaoId.Equals(0))
                {
                    cmdSecao = db.GetStoredProcCommand("[SecaoInsert]");
                    db.AddOutParameter(cmdSecao, "@secaoId", DbType.Int32, 0);
                }
                else
                {
                    cmdSecao = db.GetStoredProcCommand("[SecaoUpdate]");
                    db.AddInParameter(cmdSecao, "@secaoId", DbType.Int32, secao.SecaoId);
                }

                if (secao.SecaoIdPai > 0)
                    db.AddInParameter(cmdSecao, "@secaoIdPai", DbType.Int32, secao.SecaoIdPai);
                db.AddInParameter(cmdSecao, "@modeloId", DbType.Int32, secao.ModeloId);
                db.AddInParameter(cmdSecao, "@ordem", DbType.Int32, secao.Ordem);
                db.AddInParameter(cmdSecao, "@acessoRapido", DbType.Boolean, secao.AcessoRapido);
                db.AddInParameter(cmdSecao, "@habilitaBoxRSS", DbType.Boolean, secao.HabilitaBoxRSS);
                db.AddInParameter(cmdSecao, "@redirecionaId", DbType.Int32, secao.RedirecionaId);
                db.AddInParameter(cmdSecao, "@secaoAutenticada", DbType.Boolean, secao.SecaoAutenticada);
                db.AddInParameter(cmdSecao, "@exibeNoMenu", DbType.Boolean, secao.ExibeNoMenu);
                db.AddInParameter(cmdSecao, "@excluido", DbType.Boolean, false);

                db.ExecuteNonQuery(cmdSecao, transaction);

                if (secao.SecaoId.Equals(0))
                    secao.SecaoId = Convert.ToInt32(db.GetParameterValue(cmdSecao, "@secaoId"));

                //DELETE SECAO IDIOMA CORRESPONDENTE A SECAOID E IDIOMAID
                cmdSecao = db.GetStoredProcCommand("[Secao_IdiomaDelete]");
                db.AddInParameter(cmdSecao, "@idiomaId", DbType.Int32, secao.IdiomaId);
                db.AddInParameter(cmdSecao, "@secaoId", DbType.Int32, secao.SecaoId);
                db.ExecuteNonQuery(cmdSecao, transaction);

                //INSERE SECAO IDIOMA
                cmdSecao = db.GetStoredProcCommand("[Secao_IdiomaInsert]");
                db.AddInParameter(cmdSecao, "@idiomaId", DbType.Int32, secao.IdiomaId);
                db.AddInParameter(cmdSecao, "@secaoId", DbType.Int32, secao.SecaoId);
                db.AddInParameter(cmdSecao, "@titulo", DbType.String, secao.Titulo);
                db.AddInParameter(cmdSecao, "@tituloMenu", DbType.String, secao.TituloMenu);
                db.AddInParameter(cmdSecao, "@ativo", DbType.Boolean, secao.Ativo);
                db.AddInParameter(cmdSecao, "@palavraChave", DbType.String, secao.PalavraChave);
                db.AddInParameter(cmdSecao, "@texto", DbType.String, secao.Texto);
                db.AddInParameter(cmdSecao, "@tituloArquivos", DbType.String, secao.TituloArquivos);
                db.AddInParameter(cmdSecao, "@textoArquivos", DbType.String, secao.TextoArquivos);

                db.ExecuteNonQuery(cmdSecao, transaction);

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

            return secao;
        }

        public void DeletaSessao(Manager.Entity.Secao secao)
        {
            DbCommand cmdSecao = db.GetStoredProcCommand("[SecaoDelete]");
            db.AddInParameter(cmdSecao, "@secaoId", DbType.Int32, secao.SecaoId);
            //db.AddInParameter(cmdSecao, "@idiomaId", DbType.Int16, new Manager.Helper.CurrentSessions().CurrentIdioma.IdiomaId);

            db.ExecuteNonQuery(cmdSecao);
        }

        public void AlteraOrdem(Int32 secaoId, string direcao)
        {
            DbCommand cmdSecao = db.GetStoredProcCommand("[AlteraOrdemSecao]");
            db.AddInParameter(cmdSecao, "@secaoId", DbType.Int32, secaoId);
            db.AddInParameter(cmdSecao, "@direcao", DbType.String, direcao);
            db.ExecuteNonQuery(cmdSecao);
        }

        public List<Ag2.Manager.Entity.Modelo> CarregaModelos()
        {
            DbCommand cmdSecao = db.GetStoredProcCommand("[ModelosSelectAll]");

            DataTable dt = db.ExecuteDataSet(cmdSecao).Tables[0];
            List<Ag2.Manager.Entity.Modelo> modeloList = new List<Ag2.Manager.Entity.Modelo>();
            Ag2.Manager.Entity.Modelo m = null;

            foreach (DataRow dr in dt.Rows)
            {
                m = new Ag2.Manager.Entity.Modelo();
                m.ModeloId = Convert.ToInt32(dr["ModeloId"]);
                m.Nome = dr["Nome"].ToString();
                m.Arquivo = dr["Arquivo"].ToString();

                modeloList.Add(m);
            }

            return modeloList;
        }

        public List<Ag2.Manager.Entity.Secao> CarregaSecoes(Manager.Entity.Secao secao)
        {
            DbCommand cmdSecao = db.GetStoredProcCommand("[SecaoRecursivoNemTodas]");
            db.AddInParameter(cmdSecao, "@idiomaId", DbType.Int32, new Manager.Helper.CurrentSessions().CurrentIdioma.IdiomaId);
            db.AddInParameter(cmdSecao, "@idPai", DbType.Int32, secao.SecaoIdPai);

            DataTable dt = db.ExecuteDataSet(cmdSecao).Tables[0];
            List<Ag2.Manager.Entity.Secao> secaoList = new List<Ag2.Manager.Entity.Secao>();
            Ag2.Manager.Entity.Secao s = null;

            foreach (DataRow dr in dt.Rows)
            {
                s = new Ag2.Manager.Entity.Secao();
                s.SecaoId = Convert.ToInt32(dr["SecaoId"]);
                s.Titulo = dr["Titulo"].ToString();

                secaoList.Add(s);
            }

            return secaoList;
        }

        public List<Ag2.Manager.Entity.ItemSecao> CarregaSecoes()
        {
            DbCommand cmdSecao = db.GetStoredProcCommand("[SecaoConteudoCarregar]");
            db.AddInParameter(cmdSecao, "@idiomaId", DbType.Int16, new Ag2.Manager.Helper.CurrentSessions().CurrentIdioma.IdiomaId);

            DataTable dt = db.ExecuteDataSet(cmdSecao).Tables[0];

            List<Ag2.Manager.Entity.ItemSecao> itemSecaoList = new List<Ag2.Manager.Entity.ItemSecao>();
            itemSecaoList = Recurciva(dt, 0);

            return itemSecaoList;
        }

        private List<Ag2.Manager.Entity.ItemSecao> Recurciva(DataTable dt, int p)
        {
            dt.DefaultView.RowFilter = string.Format("ISNULL(secaoIdPai, 0) = {0}", p);
            List<Ag2.Manager.Entity.ItemSecao> itemSecaoList = new List<Ag2.Manager.Entity.ItemSecao>();
            Ag2.Manager.Entity.ItemSecao itemSecao = null;

            foreach (DataRowView item in dt.DefaultView)
            {
                itemSecao = new Ag2.Manager.Entity.ItemSecao();
                itemSecao.SecaoId = Convert.ToInt32(item["SecaoId"]);
                itemSecao.Descricao = item["titulo"].ToString();
                itemSecao.Ativo = Convert.ToBoolean(item["Ativo"]);

                itemSecao.ItensSecao = Recurciva(dt, itemSecao.SecaoId);

                itemSecaoList.Add(itemSecao);
            }

            return itemSecaoList;
        }


    }
}
