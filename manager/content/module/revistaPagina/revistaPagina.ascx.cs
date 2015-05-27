using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ag2.Manager.Helper;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;
using System.Text.RegularExpressions;
using GrupoA.BusinessObject.Enumerator;

public partial class content_module_revista_pagina : SmartUserControl
{
    #region Eventos

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.CarregarRevista();

            if (IdRegistro > 0)
            {
                this.hdnRevistaPaginaId.Value = IdRegistro.ToString();
                this.LoadForm();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExecute_Click(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = false;
        this.SaveOrUpdate();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void cvValidarRevista_ServerValidate(object source, ServerValidateEventArgs args)
    {
        this.ValidarRevista(args);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void cvValidarTexto_ServerValidate(object source, ServerValidateEventArgs args)
    {
        this.ValidarTexto(args);
    }

    #endregion

    #region Métodos

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    protected void LoadForm()
    {
        if (IdRegistro > 0)
        {
            RevistaPagina revistaPagina = new RevistaPaginaBLL().Carregar(new RevistaPagina() { RevistaPaginaId = (Int32)IdRegistro });
            hdnRevistaPaginaId.Value = revistaPagina.RevistaPaginaId.ToString();
            txtTitulo.Text = revistaPagina.TituloPagina;
            txtTexto.Text = revistaPagina.TextoPagina;
            chkAtivo.Checked = revistaPagina.Ativo;
            chkExibirMenu.Checked = revistaPagina.ExibirMenu;
            txtOrdem.Text = revistaPagina.Ordem.ToString();
            ddlRevista.SelectedValue = revistaPagina.Revista.RevistaId.ToString();

            String caminhoRevista = null;

            if (revistaPagina.Revista.RevistaId == AreaDeRevista.Bmj.GetHashCode())
            {
                caminhoRevista = "revista-bmj/";
            }
            else
            {
                caminhoRevista = "revista-patio/";
            }

            String url = String.Concat(System.Configuration.ConfigurationManager.AppSettings["CaminhoSite"].ToString(), caminhoRevista, revistaPagina.NomePagina, ".aspx");
            pnlUrl.Visible = true;
            ltUrl.Text = url;
            hlVisualizar.NavigateUrl = url;

            if (Util.GetQueryString("origem") == "insert")
            {
                Util.ShowInsertMessage();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    protected void SaveOrUpdate()
    {
        if (Page.IsValid)
        {
            RevistaPagina revistaPagina = new RevistaPagina();

            revistaPagina.RevistaPaginaId = Convert.ToInt32(hdnRevistaPaginaId.Value);
            revistaPagina.TituloPagina = txtTitulo.Text;
            revistaPagina.TextoPagina = txtTexto.Text;
            revistaPagina.Ativo = chkAtivo.Checked;
            revistaPagina.ExibirMenu = chkExibirMenu.Checked;
            revistaPagina.Ordem = Convert.ToInt32(txtOrdem.Text);
            revistaPagina.NomePagina = this.FormataNomePagina(revistaPagina.TituloPagina);

            revistaPagina.Revista = new Revista() { RevistaId = Convert.ToInt32(this.ddlRevista.SelectedValue) };

            if (revistaPagina.RevistaPaginaId > 0)
            {
                new RevistaPaginaBLL().Atualizar(revistaPagina);
                Util.ShowUpdateMessage();

                String caminhoRevista = null;

                if (revistaPagina.Revista.RevistaId == AreaDeRevista.Bmj.GetHashCode())
                {
                    caminhoRevista = "revista-bmj/";
                }
                else
                {
                    caminhoRevista = "revista-patio/";
                }

                String url = String.Concat(System.Configuration.ConfigurationManager.AppSettings["CaminhoSite"].ToString(), caminhoRevista, revistaPagina.NomePagina, ".aspx");
                pnlUrl.Visible = true;
                ltUrl.Text = url;
                hlVisualizar.NavigateUrl = url;
            }
            else
            {
                new RevistaPaginaBLL().Inserir(revistaPagina);

                Ag2.Security.SecureQueryString sqGrid = new Ag2.Security.SecureQueryString();
                sqGrid["md"] = Util.GetQueryString("md");
                sqGrid["id"] = revistaPagina.RevistaPaginaId.ToString();
                sqGrid["origem"] = "insert";

                Response.Redirect(string.Format("~/content/edit.aspx?q={0}", sqGrid.ToString()));
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="nomePagina"></param>
    /// <returns></returns>
    private String FormataNomePagina(String nomePagina)
    {
        string str = RemoveAccent(nomePagina).ToLower();

        str = Regex.Replace(str, "-", " ");
        str = Regex.Replace(str, "/", " ");
        str = Regex.Replace(str, @"[^a-z0-9\s-]", ""); // invalid chars
        str = Regex.Replace(str, @"\s+", " ").Trim(); // convert multiple spaces into one space
        str = Regex.Replace(str, @"\s", "-"); // hyphens

        return str;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="txt"></param>
    /// <returns></returns>
    private string RemoveAccent(string txt)
    {
        byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);

        return System.Text.Encoding.ASCII.GetString(bytes);
    }

    /// <summary>
    /// 
    /// </summary>
    private void CarregarRevista()
    {
        this.ddlRevista.DataSource = new RevistaGrupoABLL().CarregarTodasRevistas();
        this.ddlRevista.DataTextField = "NomeRevista";
        this.ddlRevista.DataValueField = "RevistaId";
        this.ddlRevista.DataBind();
        this.ddlRevista.Items.Insert(0, ":: Selecione ::");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    private void ValidarRevista(ServerValidateEventArgs args)
    {
        if (ddlRevista.SelectedIndex == 0)
        {
            this.cvValidarRevista.ErrorMessage = "Campo Obrigatório";
            args.IsValid = false;
            return;
        }
        else
        {
            this.cvValidarRevista.ErrorMessage = "";
            args.IsValid = true;
            return;
        }
    }

    private void ValidarTexto(ServerValidateEventArgs args)
    {
        if (String.IsNullOrEmpty(txtTexto.Text.Trim()))
        {
            this.cvValidarTexto.ErrorMessage = "Campo Obrigatório";
            args.IsValid = false;
            return;
        }
        else
        {
            this.cvValidarTexto.ErrorMessage = "";
            args.IsValid = true;
            return;
        }
    }

    #endregion
}