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

namespace Ag2.Manager.ADO.Oracle
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
            throw new NotImplementedException();
        }

        public void DesativarAtivarSecao(Manager.Entity.Secao secao)
        {
            throw new NotImplementedException();
        }

        public Manager.Entity.Secao Salvar(Manager.Entity.Secao secao)
        {
            throw new NotImplementedException();
        }

        public List<Ag2.Manager.Entity.Modelo> CarregaModelos()
        {
            throw new NotImplementedException();
        }

        public List<Ag2.Manager.Entity.Secao> CarregaSecoes(Manager.Entity.Secao secao)
        {
            throw new NotImplementedException();
        }

        public void DeletaSessao(Manager.Entity.Secao secao)
        {
            throw new NotImplementedException();
        }

        public void AlteraOrdem(Int32 secaoId, string direcao)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Ag2.Manager.Entity.ItemSecao> CarregaSecoes()
        {
            DbCommand cmdSecao = db.GetStoredProcCommand("[SecaoConteudoCarregar]");
            db.AddInParameter(cmdSecao, "@idiomaId", DbType.Int16, 1);

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

                itemSecao.ItensSecao = Recurciva(dt, itemSecao.SecaoId);

                itemSecaoList.Add(itemSecao);
            }

            return itemSecaoList;
        }


    }
}
