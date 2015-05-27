using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ag2.Manager.Helper;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;
using System.Configuration;
using System.IO;
using GrupoA.GlobalResources;

public partial class content_module_banner_banner : SmartUserControl
{
    #region Propriedades

    private BannerBLL _bannerBll;
    private BannerBLL BannerBll
    {
        get { return _bannerBll ?? (_bannerBll = new BannerBLL()); }
    }

    private ArquivoBLL _arquivoBll;
    private ArquivoBLL ArquivoBll
    {
        get { return _arquivoBll ?? (_arquivoBll = new ArquivoBLL()); }
    }



    #endregion

    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        ListFiles1.BindList += new EventHandler<ArquivoEventArgs>(ListFiles1_BindList);
        ListFiles1.DeleteItem += new EventHandler<ArquivoEventArgs>(ListFiles1_DeleteItem);

        if (IdRegistro > 0)
        {
            if (!IsPostBack)
            {
                this.hddBannerId.Value = IdRegistro.ToString();
                this.MontaChecklistPagina();
                this.LoadForm();
            }

            ListFiles1.RegistroId = Util.GetRequestId();
        }
    }

    protected void btnExecute_Click(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = false;
        SaveOrUpdate();
    }

    #endregion

    #region Métodos

    private void LoadForm()
    {
        if (IdRegistro > 0)
        {
            var banner = BannerBll.CarregarBanner(new Banner() { BannerId = (int)IdRegistro });
            banner.BannerAreas = BannerBll.CarregarAreasDoBanner(new Banner() { BannerId = (int)IdRegistro });
            hddBannerId.Value = banner.BannerId.ToString();

            if (banner.Arquivo != null)
            {
                var arquivo = ArquivoBll.CarregarArquivo(new Arquivo() { ArquivoId = banner.Arquivo.ArquivoId });
                hddBannerArquivoId.Value = arquivo.ArquivoId.ToString();
                hddBannerArquivoNome.Value = arquivo.NomeArquivo.ToString();
                ListFiles1.CarregaArquivo(arquivo.ArquivoId);
            }

            this.txtNomeBanner.Text = banner.NomeBanner;
            this.txtFonteUrl.Text = banner.Url;
            chkAtivo.Checked = banner.Ativo;
            chkTargetBlank.Checked = banner.TargetBlank;
            txtTempoExibicao.Text = banner.TempoExibicao.ToString();

            if (banner.DataExibicaoInicio != null)
            {
                txtDataExibicaoInicio.Text = banner.DataExibicaoInicio.Value.ToString("dd/MM/yyyy");
            }

            if (banner.DataExibicaoFim != null)
            {
                txtDataExibicaoFim.Text = banner.DataExibicaoFim.Value.ToString("dd/MM/yyyy");
            }

            lblArquivoBanner.Visible = true;

            foreach (BannerArea bannerItem in banner.BannerAreas)
            {
                for (int i = 0; i < cblBannerArea.Items.Count; i++)
                {
                    if (cblBannerArea.Items[i].Value.Equals(bannerItem.BannerAreaId.ToString()))
                    {
                        cblBannerArea.Items[i].Selected = true;
                    }
                }
            }

            if (Util.GetQueryString("origem") == "insert")
            {
                Util.ShowInsertMessage();
            }
        }
    }

    void ListFiles1_DeleteItem(object sender, ArquivoEventArgs e)
    {
        var arquivo = ArquivoBll.CarregarArquivo(new Arquivo() { ArquivoId = Convert.ToInt32(((ImageButton)sender).CommandArgument) });

        Banner banner = new Banner(Convert.ToInt32(hddBannerId.Value));

        banner = new BannerBLL().CarregarBanner(banner);
        banner.Arquivo = null;

        new BannerBLL().AtualizarBanner(banner);

        ArquivoBll.ExcluirArquivo(arquivo);


        // TODO: Verificar se isso está realmente funcionando
        //string strFilePath = Server.MapPath(string.Concat(GrupoA_Resource.baseUrlUpload, "/", ListFiles1.TargetFolder, "/"));
        string strFilePath = string.Concat(Server.MapPath(GrupoA_Resource.baseUrlUpload), "imagensBanner\\");
        this.ExcluirArquivosFisicamente(arquivo, strFilePath);

        hddBannerArquivoId.Value = "0";
        ListFiles1.DataBind();
    }

    private void ExcluirArquivosFisicamente(Arquivo arquivo, string strFilePath)
    {
        if (File.Exists(strFilePath + arquivo.NomeArquivo))
        {
            File.Delete(strFilePath + arquivo.NomeArquivo);
        }
    }

    void ListFiles1_BindList(object sender, ArquivoEventArgs e)
    {
        var arquivoId = Convert.ToInt32(hddBannerArquivoId.Value);

        if (!e.ArquivoId.ToString().Equals(hddBannerArquivoId.Value))
        {
            hddBannerArquivoIdDelete.Value = hddBannerArquivoId.Value;
        }

        hddBannerArquivoId.Value = e.ArquivoId.ToString();

        SaveOrUpdate();
    }

    protected void SaveOrUpdate()
    {
        if (Page.IsValid)
        {
            var banner = new Banner();
            banner.BannerId = Convert.ToInt32(hddBannerId.Value);
            banner.NomeBanner = this.txtNomeBanner.Text.Trim();
            banner.Url = this.txtFonteUrl.Text.Trim();
            banner.Ativo = chkAtivo.Checked;
            banner.TargetBlank = chkTargetBlank.Checked;
            banner.TempoExibicao = Convert.ToInt32(txtTempoExibicao.Text.Trim());
            if (!string.IsNullOrEmpty(txtDataExibicaoInicio.Text))
            {
                banner.DataExibicaoInicio = Convert.ToDateTime(txtDataExibicaoInicio.Text);
            }
            else
            {
                banner.DataExibicaoInicio = null;
            }

            if (!string.IsNullOrEmpty(txtDataExibicaoFim.Text))
            {
                banner.DataExibicaoFim = Convert.ToDateTime(txtDataExibicaoFim.Text);
            }
            else
            {
                banner.DataExibicaoFim = null;
            }

            List<BannerArea> bannerArea = new List<BannerArea>(); // Popula Páginas referentes a Enquete

            foreach (ListItem li in cblBannerArea.Items)
            {
                if (li.Selected)
                {
                    BannerArea ep = new BannerArea();
                    ep.BannerAreaId = Convert.ToInt32(li.Value);

                    bannerArea.Add(ep);
                }
            }

            banner.BannerAreas = bannerArea;

            if (Convert.ToInt32(hddBannerArquivoId.Value) > 0)
            {
                banner.Arquivo = new Arquivo() { ArquivoId = Convert.ToInt32(hddBannerArquivoId.Value) };
            }

            if (banner.BannerId > 0)
            {
                new BannerBLL().AtualizarBanner(banner);

                if (banner.Arquivo != null)
                {
                    hddBannerArquivoId.Value = banner.Arquivo.ArquivoId.ToString();
                    hddBannerArquivoNome.Value = banner.Arquivo.NomeArquivo;
                }

                Util.ShowUpdateMessage();
            }
            else
            {
                banner = new BannerBLL().InserirNovoBanner(banner);
                hddBannerId.Value = banner.BannerId.ToString();

                Ag2.Security.SecureQueryString sqGrid = new Ag2.Security.SecureQueryString();
                sqGrid["md"] = Util.GetQueryString("md");
                sqGrid["id"] = banner.BannerId.ToString();
                sqGrid["origem"] = "insert";

                Response.Redirect(string.Format("~/content/edit.aspx?q={0}", sqGrid.ToString()));
            }

            if (hddBannerArquivoIdDelete.Value != "0") //Deleta arquivo anterior
            {
                var arquivoDel = new ArquivoBLL().CarregarArquivo(new Arquivo() { ArquivoId = Convert.ToInt32(hddBannerArquivoIdDelete.Value) });
                if (arquivoDel != null)
                {
                    string pathFileDelete = string.Concat(Server.MapPath(GrupoA_Resource.baseUrlUpload), ListFiles1.TargetFolder, arquivoDel.NomeArquivo);
                    FileInfo infoDelete = new FileInfo(pathFileDelete);

                    if (infoDelete.Exists)
                    {
                        infoDelete.Delete();
                        new ArquivoBLL().ExcluirArquivo(arquivoDel);
                    }
                }
            }
        }
    }

    private void CarregarBannerArea()
    {
        //ddlBannerArea.DataSource = new BannerBLL().CarregarBanners();
        //ddlBannerArea.DataTextField = "Area";
        //ddlBannerArea.DataValueField = "BannerAreaId";
        //ddlBannerArea.DataBind();
        //if (IdRegistro == 0)
        //ddlBannerArea.Items.Insert(0, ":: Selecione ::");
    }

    #endregion

    #region Validacoes

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void cvValidarDatasPublicacao_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (string.IsNullOrEmpty(txtDataExibicaoInicio.Text) && string.IsNullOrEmpty(txtDataExibicaoFim.Text))
        {
            args.IsValid = true; return;
        }

        try
        {
            DateTime dtIni = DateTime.Parse(txtDataExibicaoInicio.Text);
            DateTime dtFim = DateTime.Parse(txtDataExibicaoFim.Text);

            if (DateTime.Compare(dtFim, dtIni) > 0)
            {
                args.IsValid = true;
            }
            else
            {
                cvValidarDatasPublicacao.ErrorMessage = "Data final deve ser maior que data inicial.";
                args.IsValid = false;
            }
        }
        catch
        {
            cvValidarDatasPublicacao.ErrorMessage = "Data incorreta!";
            args.IsValid = false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    protected void MontaChecklistPagina()
    {
        this.lblBannerArea.Visible = true;
        var areas = BannerBll.CarregarBannersArea().ToList().OrderBy(b => b.Area).ToList();
        areas.ForEach(s => s.Area = String.Concat(" ", s.Area, " - ", s.Dimensao));
        cblBannerArea.DataSource = areas;
        cblBannerArea.DataValueField = "bannerAreaId";
        cblBannerArea.DataTextField = "area";
        cblBannerArea.DataBind();
    }

    #endregion
}