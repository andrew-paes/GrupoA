using System;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ag2.Manager.Helper;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;

public partial class content_module_paginaPromocional_paginaPromocional : SmartUserControl
{
    #region Propriedades

    /// <summary>
    /// 
    /// </summary>
    private int _id
    {
        get
        {
            return Util.GetRequestId();
        }
    }

    #endregion

    #region Eventos

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        this.upArquivoImagem.BindList += new EventHandler<ArquivoEventArgs>(upArquivoImagem_BindList);
        this.upArquivoImagem.DeleteItem += new EventHandler<ArquivoEventArgs>(upArquivoImagem_DeleteItem);
        this.upArquivoImagem.RegistroId = Convert.ToInt32(_id);

        this.LoadForm(_id);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExecute_Click(object sender, ImageClickEventArgs e)
    {
        SaveOrUpdate();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void upArquivoImagem_BindList(object sender, ArquivoEventArgs e)
    {
        this.BindImage(e);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void upArquivoImagem_DeleteItem(object sender, ArquivoEventArgs e)
    {
        this.DeleteImage();
    }

    #endregion

    #region Métodos

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_id"></param>
    private void LoadForm(int _id)
    {
        if (!IsPostBack)
        {
            if (_id > 0)
            {
                this.pnlArquivo.Visible = true;

                PaginaPromocional paginaPromocionalBO = new PaginaPromocionalBLL().Carregar(new PaginaPromocional() { PaginaPromocionalId = _id });

                if (paginaPromocionalBO != null && paginaPromocionalBO.PaginaPromocionalId > 0)
                {
                    txtNomePagina.Text = paginaPromocionalBO.NomePagina;
                    txtTituloPagina.Text = paginaPromocionalBO.TituloPagina;
                    txtSubtituloPagina.Text = paginaPromocionalBO.SubtituloPagina;
                    HtmlTextoResumo.Text = paginaPromocionalBO.Resumo;
                    txtLinkURL.Text = paginaPromocionalBO.LinkMidia;
                    txtLarguraArquivo.Text = paginaPromocionalBO.LarguraArquivo != null ? paginaPromocionalBO.LarguraArquivo.Value.ToString() : String.Empty;
                    txtAlturaArquivo.Text = paginaPromocionalBO.AlturaArquivo != null ? paginaPromocionalBO.AlturaArquivo.Value.ToString() : String.Empty;
                    chkAtivo.Checked = paginaPromocionalBO.Ativo;
                    chkTargetBlank.Checked = paginaPromocionalBO.TargetBlank;

                    if (paginaPromocionalBO.Arquivo != null && paginaPromocionalBO.Arquivo.ArquivoId > 0)
                    {
                        this.hddArquivoId.Value = paginaPromocionalBO.Arquivo.ArquivoId.ToString();
                    }

                    String url = String.Concat(System.Configuration.ConfigurationManager.AppSettings["CaminhoSite"].ToString(), this.FormataNomePagina(paginaPromocionalBO.NomePagina));
                    pnlUrl.Visible = true;
                    ltUrl.Text = url;

                    hlVisualizar.NavigateUrl = url;

                    if (Util.GetQueryString("origem") == "insert")
                    {
                        Util.ShowInsertMessage();
                    }
                }
            }
        }

        if (!String.IsNullOrEmpty(this.hddArquivoId.Value) && Convert.ToInt32(this.hddArquivoId.Value) > 0)
        {
            this.upArquivoImagem.ArquivoId = Convert.ToInt32(this.hddArquivoId.Value);
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
    private void SaveOrUpdate()
    {
        Page.MaintainScrollPositionOnPostBack = false;

        if (Page.IsValid)
        {
            PaginaPromocional paginaPromocionalBO;

            if (_id > 0)
            {
                paginaPromocionalBO = new PaginaPromocionalBLL().Carregar(new PaginaPromocional(_id));
            }
            else
            {
                paginaPromocionalBO = new PaginaPromocional();
            }

            paginaPromocionalBO.NomePagina = txtNomePagina.Text;
            paginaPromocionalBO.TituloPagina = this.txtTituloPagina.Text;
            
            if (!String.IsNullOrEmpty(txtSubtituloPagina.Text))
            {
                paginaPromocionalBO.SubtituloPagina = txtSubtituloPagina.Text;
            }
            else
            {
                paginaPromocionalBO.SubtituloPagina = null;
            }

            paginaPromocionalBO.Resumo = this.HtmlTextoResumo.Text;
            paginaPromocionalBO.LinkMidia = this.txtLinkURL.Text;
            paginaPromocionalBO.Ativo = chkAtivo.Checked;
            paginaPromocionalBO.TargetBlank = chkTargetBlank.Checked;

            if (!String.IsNullOrEmpty(this.hddArquivoId.Value) && Convert.ToInt32(this.hddArquivoId.Value) > 0)
            {
                paginaPromocionalBO.Arquivo = new Arquivo();
                paginaPromocionalBO.Arquivo.ArquivoId = Convert.ToInt32(this.hddArquivoId.Value);
            }

            if (_id > 0)
            {
                paginaPromocionalBO.PaginaPromocionalId = _id;
                paginaPromocionalBO.LarguraArquivo = Convert.ToInt32(txtLarguraArquivo.Text);
                paginaPromocionalBO.AlturaArquivo = Convert.ToInt32(txtAlturaArquivo.Text);

                new PaginaPromocionalBLL().Atualizar(paginaPromocionalBO);

                String url = String.Concat(System.Configuration.ConfigurationManager.AppSettings["CaminhoSite"].ToString(), this.FormataNomePagina(paginaPromocionalBO.NomePagina));
                ltUrl.Text = url;

                hlVisualizar.NavigateUrl = url;

                Util.ShowUpdateMessage();
            }
            else
            {
                paginaPromocionalBO.LarguraArquivo = null;
                paginaPromocionalBO.AlturaArquivo = null;

                new PaginaPromocionalBLL().Inserir(paginaPromocionalBO);

                Ag2.Security.SecureQueryString sqGrid = new Ag2.Security.SecureQueryString();
                sqGrid["md"] = Util.GetQueryString("md");
                sqGrid["id"] = paginaPromocionalBO.PaginaPromocionalId.ToString();
                sqGrid["origem"] = "insert";

                Response.Redirect(string.Format("~/content/edit.aspx?q={0}", sqGrid.ToString()));
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    private void BindImage(ArquivoEventArgs e)
    {
        this.upArquivoImagem.ArquivoId = e.ArquivoId;
        this.upArquivoImagem.RegistroId = Convert.ToInt32(IdRegistro);

        //cria o relacionamento
        if (e.ArquivoId > 0)
        {
            Arquivo arquivoBO = new Arquivo();
            arquivoBO = new ArquivoBLL().CarregarArquivo(new Arquivo() { ArquivoId = this.upArquivoImagem.ArquivoId });

            if (arquivoBO != null && arquivoBO.ArquivoId > 0)
            {
                this.hddArquivoId.Value = arquivoBO.ArquivoId.ToString();

                PaginaPromocional paginaPromocionalBO = new PaginaPromocional();
                paginaPromocionalBO = new PaginaPromocionalBLL().Carregar(new PaginaPromocional() { PaginaPromocionalId = _id });
                paginaPromocionalBO.Arquivo = new Arquivo();
                paginaPromocionalBO.Arquivo = arquivoBO;

                new PaginaPromocionalBLL().Atualizar(paginaPromocionalBO);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void DeleteImage()
    {
        PaginaPromocional paginaPromocionalBO = new PaginaPromocional();
        paginaPromocionalBO = new PaginaPromocionalBLL().Carregar(new PaginaPromocional() { PaginaPromocionalId = _id }); ;
        paginaPromocionalBO.Arquivo = null;

        new PaginaPromocionalBLL().Atualizar(paginaPromocionalBO);

        // Remove da tabela Arquivo
        Arquivo arquivoBO = new Arquivo();
        arquivoBO = new ArquivoBLL().CarregarArquivo(new Arquivo() { ArquivoId = this.upArquivoImagem.ArquivoId });
        new ArquivoBLL().ExcluirArquivo(arquivoBO);

        //atualiza ListFile
        this.upArquivoImagem.RegistroId = _id;
        this.upArquivoImagem.DataSource = null;
        this.upArquivoImagem.DataBind();
    }

    #endregion

    #region Validações

    protected void cvValidarLargura_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (Convert.ToInt32(txtLarguraArquivo.Text) <= 0)
        {
            cvValidarLargura.ErrorMessage = "Largura deve ser maior que zero.";
            args.IsValid = false;
        }
        else
        {
            args.IsValid = true;
        }
    }

    protected void cvValidarAltura_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (Convert.ToInt32(txtAlturaArquivo.Text) <= 0)
        {
            cvValidarAltura.ErrorMessage = "Altura deve ser maior que zero.";
            args.IsValid = false;
        }
        else
        {
            args.IsValid = true;
        }
    }

    #endregion
}