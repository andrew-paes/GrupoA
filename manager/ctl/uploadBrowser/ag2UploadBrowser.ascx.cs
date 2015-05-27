using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ag2.Manager.Helper;

public partial class ctl_ag2UploadBrowser : System.Web.UI.UserControl
{
    private Ag2.Manager.DAL.IUploadBrowserDAL uploadBrowserADO = (Ag2.Manager.DAL.IUploadBrowserDAL)Util.GetADO("UploadBrowserADO", (System.Reflection.Assembly)System.Web.Compilation.BuildManager.CodeAssemblies[0]);

    protected void Page_Load(object sender, EventArgs e)
    {
        btnAcao.Click += new EventHandler(btnAcao_Click);
        gridArquivos.RowDataBound += new GridViewRowEventHandler(gridArquivos_RowDataBound);
        Ag2.Security.SecureQueryString sQuery = new Ag2.Security.SecureQueryString();

        sQuery["ctrl"] = pnlGrid.ClientID;
        sQuery["AllowedExtensions"] = AllowedExtensions;

        AdicionarUploadBrowser.NavigateUrl = string.Format("~/content/UploadBrowser.aspx?q={0}"
            , sQuery.ToString());
    }

    void btnAcao_Click(object sender, EventArgs e)
    {
        string[] ids = hdnIds.Value.Substring(0, hdnIds.Value.Length - 1).Split(',');

        var listaIds = (from l in ids select l).Distinct();

        List<Ag2.Manager.Entity.Arquivo> arquivos = new List<Ag2.Manager.Entity.Arquivo>();
        Ag2.Manager.Entity.Arquivo arquivo = null;

        foreach (var id in listaIds)
        {
            arquivo = new Ag2.Manager.Entity.Arquivo();
            arquivo.arquivoId = Convert.ToInt32(id);

            arquivos.Add(arquivo);
        }

        uploadBrowserADO.CarregarAquivos(arquivos);

        gridArquivos.DataSource = arquivos;
        gridArquivos.DataBind();
    }

    void gridArquivos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = false;
        e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Ag2.Manager.Entity.Arquivo arquivo = (Ag2.Manager.Entity.Arquivo)e.Row.DataItem;
            Image imgIco = (Image)e.Row.FindControl("imgIco");
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");

            lnkDelete.Attributes.Add("onClick", "if(confirm('Deseja excluir este registro?')){return true;}else{return false;}");

            if (arquivo.extensao.Equals("doc", StringComparison.OrdinalIgnoreCase) ||
            arquivo.extensao.Equals("docx", StringComparison.OrdinalIgnoreCase))
            {
                imgIco.ImageUrl = "~/img/icoDoc.png";
            }
            else if (arquivo.extensao.Equals("ppt", StringComparison.OrdinalIgnoreCase) ||
                 arquivo.extensao.Equals("pptx", StringComparison.OrdinalIgnoreCase))
            {
                imgIco.ImageUrl = "~/img/icoPpt.png";
            }
            else if (arquivo.extensao.Equals("xls", StringComparison.OrdinalIgnoreCase) ||
                     arquivo.extensao.Equals("xlsx", StringComparison.OrdinalIgnoreCase))
            {
                imgIco.ImageUrl = "~/img/icoXls.png";
            }
            else if (arquivo.extensao.Equals("pdf", StringComparison.OrdinalIgnoreCase))
            {
                imgIco.ImageUrl = "~/img/icoPdf.png";
            }
            else if (arquivo.extensao.Equals("txt", StringComparison.OrdinalIgnoreCase))
            {
                imgIco.ImageUrl = "~/img/icoTxt.png";
            }
            else if (arquivo.extensao.Equals("zip", StringComparison.OrdinalIgnoreCase))
            {
                imgIco.ImageUrl = "~/img/icoZip.png";
            }
            else if (arquivo.extensao.Equals("jpg", StringComparison.OrdinalIgnoreCase) ||
                     arquivo.extensao.Equals("gif", StringComparison.OrdinalIgnoreCase) ||
                     arquivo.extensao.Equals("png", StringComparison.OrdinalIgnoreCase))
            {
                imgIco.ImageUrl = "~/img/icoImg.png";
            }
            else
            {
                imgIco.ImageUrl = "~/img/icoOutros.png";
            }

            e.Row.Cells[3].Text = string.Concat((Convert.ToInt32(e.Row.Cells[3].Text) / 1024).ToString(), " kb ");

            lnkDelete.CommandArgument = arquivo.arquivoId.ToString();
        }
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;

        string[] ids = hdnIds.Value.Substring(0, hdnIds.Value.Length - 1).Split(',');

        var listaIds = (from l in ids select l).Distinct();

        List<Ag2.Manager.Entity.Arquivo> arquivos = new List<Ag2.Manager.Entity.Arquivo>();
        Ag2.Manager.Entity.Arquivo arquivo = null;

        foreach (var id in listaIds)
        {
            arquivo = new Ag2.Manager.Entity.Arquivo();
            arquivo.arquivoId = Convert.ToInt32(id);

            arquivos.Add(arquivo);
        }

        Ag2.Manager.Entity.Arquivo arTemp = arquivos.Find(delegate(Ag2.Manager.Entity.Arquivo ar)
        {
            return ar.arquivoId == Convert.ToInt32(btn.CommandArgument);
        });

        arquivos.Remove(arTemp);

        System.Text.StringBuilder sbHdn = new System.Text.StringBuilder();
        foreach (Ag2.Manager.Entity.Arquivo a in arquivos)
        {
            sbHdn.Append(a.arquivoId.ToString()).Append(",");
        }

        hdnIds.Value = sbHdn.ToString();

        uploadBrowserADO.CarregarAquivos(arquivos);

        gridArquivos.DataSource = arquivos;
        gridArquivos.DataBind();
    }

    public string AllowedExtensions { get; set; }
}
