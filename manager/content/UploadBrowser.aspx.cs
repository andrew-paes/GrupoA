using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using Ag2.Manager.Helper;
using System.Web.Services;
using System.Web.Script.Services;
using GrupoA.GlobalResources;

public partial class content_UploadBrowser : System.Web.UI.Page
{
    private string _uploadRoot = GrupoA_Resource.baseUrlUpload;
    private bool isCkEditor = false;
    private Ag2.Manager.DAL.IUploadBrowserDAL uploadBrowserADO = (Ag2.Manager.DAL.IUploadBrowserDAL)Util.GetADO("UploadBrowserADO", (System.Reflection.Assembly)System.Web.Compilation.BuildManager.CodeAssemblies[0]);
    private Ag2.Manager.DAL.ITagDAL tagADO = (Ag2.Manager.DAL.ITagDAL)Util.GetADO("TagADO", (System.Reflection.Assembly)System.Web.Compilation.BuildManager.CodeAssemblies[0]);
    private Ag2.Manager.Enumerator.tipoArquivoBrowser tipoArquivo = Ag2.Manager.Enumerator.tipoArquivoBrowser.TODOS;
    private Ag2.Security.SecureQueryString sQuery = null;
    private string ctrl = string.Empty;
    private string AllowedExtensions = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        btnFecharSalvar.Click += new ImageClickEventHandler(btnFecharSalvar_Click);
        btnUpload.Click += new ImageClickEventHandler(btnUpload_Click);
        rptArquivos.ItemDataBound += new RepeaterItemEventHandler(rptArquivos_ItemDataBound);
        rptArquivosLista.ItemDataBound += new RepeaterItemEventHandler(rptArquivosLista_ItemDataBound);
        btnBuscar.Click += new ImageClickEventHandler(btnBuscar_Click);

        lnkExecel.Click += new EventHandler(lnkMenu_Click);
        lnkImg.Click += new EventHandler(lnkMenu_Click);
        lnkOutros.Click += new EventHandler(lnkMenu_Click);
        lnkPdf.Click += new EventHandler(lnkMenu_Click);
        lnkPowerPoint.Click += new EventHandler(lnkMenu_Click);
        lnkTodos.Click += new EventHandler(lnkMenu_Click);
        lnkWord.Click += new EventHandler(lnkMenu_Click);
        lnkZip.Click += new EventHandler(lnkMenu_Click);

        rptArquivos.Visible = false;
        rptArquivosLista.Visible = false;

        if (!string.IsNullOrEmpty(Request.QueryString["CKEditor"]))
            isCkEditor = true;

        if (!isCkEditor && !string.IsNullOrEmpty(Request.QueryString["q"]))
        {
            sQuery = new Ag2.Security.SecureQueryString(Request.QueryString["q"].ToString());

            ctrl = sQuery["ctrl"].ToString();
            AllowedExtensions = sQuery["AllowedExtensions"].ToString();
        }

        if (!IsPostBack)
        {
            lnkMenu_Click(lnkImg, null);

            CarregaArquivos(null);
        }
    }

    void lnkMenu_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;

        lnkExecel.Font.Bold = false;
        lnkImg.Font.Bold = false;
        lnkOutros.Font.Bold = false;
        lnkPdf.Font.Bold = false;
        lnkPowerPoint.Font.Bold = false;
        lnkTodos.Font.Bold = false;
        lnkWord.Font.Bold = false;
        lnkZip.Font.Bold = false;

        lnkExecel.Attributes.Remove("style");
        lnkImg.Attributes.Remove("style");
        lnkOutros.Attributes.Remove("style");
        lnkPdf.Attributes.Remove("style");
        lnkPowerPoint.Attributes.Remove("style");
        lnkTodos.Attributes.Remove("style");
        lnkWord.Attributes.Remove("style");
        lnkZip.Attributes.Remove("style");

        lnk.Font.Bold = true;
        lnk.Attributes.Add("style", "padding-left: 8px;");

        this.TipoArquivoSelecionado = lnk.CommandArgument;

        CarregaArquivos(null);
    }

    void btnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        List<Ag2.Manager.Entity.Tag> tags = GetTags(txtPalavraChaveBusca.Text);
        List<Ag2.Manager.Entity.Arquivo> arquivos = uploadBrowserADO.BuscaArquivoTags(tags);

        CarregaArquivos(arquivos);
    }

    void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        CarregaArquivos(null);
    }

    void btnFecharSalvar_Click(object sender, ImageClickEventArgs e)
    {
        string url = string.Empty;

        List<Ag2.Manager.Entity.Arquivo> fileList = GetSelectedFiles();

        if (isCkEditor)
        {
            if (fileList.Count > 0)
            {
                url = this.ResolveUrl(string.Format("{0}{1}/{2}", _uploadRoot, "uploadBrowser", fileList[0].nomeArquivo));
                string funcNum = Request.QueryString["CKEditorFuncNum"].ToString();
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "ckEditorFunction", "window.opener.CKEDITOR.tools.callFunction(" + funcNum + ", '" + url + "', ''); window.close();", true);
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(ctrl))
            {
                System.Text.StringBuilder sbIds = new System.Text.StringBuilder();

                foreach (Ag2.Manager.Entity.Arquivo arquivo in fileList)
                {
                    sbIds.Append(arquivo.arquivoId).Append(",");
                }

                Ag2.Manager.Helper.WriteScriptOnPage wsop = new WriteScriptOnPage();
                wsop.Add(string.Format("window.parent.PopulaGridBrowser('{0}', '{1}');", ctrl, sbIds));
                wsop.Add("window.parent.$.fancybox.close();");
                wsop.Bind();
            }
        }
    }

    protected List<Ag2.Manager.Entity.Arquivo> GetSelectedFiles()
    {
        Ag2.Manager.Entity.Arquivo arquivo = null;
        List<Ag2.Manager.Entity.Arquivo> list = new List<Ag2.Manager.Entity.Arquivo>();

        foreach (RepeaterItem item in rptArquivos.Items)
        {
            CheckBox chkArquivo = (CheckBox)item.FindControl("chkArquivo");

            if (chkArquivo.Checked)
            {
                HiddenField hdnFileName = (HiddenField)item.FindControl("hdnFileName");
                arquivo = new Ag2.Manager.Entity.Arquivo();
                arquivo.nomeArquivo = Path.GetFileName(hdnFileName.Value);
                arquivo = uploadBrowserADO.CarregarAquivoByName(arquivo);

                list.Add(arquivo);
            }
        }

        foreach (RepeaterItem item in rptArquivosLista.Items)
        {
            CheckBox chkArquivo = (CheckBox)item.FindControl("chkArquivo");

            if (chkArquivo.Checked)
            {
                HiddenField hdnFileName = (HiddenField)item.FindControl("hdnFileName");
                arquivo = new Ag2.Manager.Entity.Arquivo();
                arquivo.nomeArquivo = Path.GetFileName(hdnFileName.Value);
                arquivo = uploadBrowserADO.CarregarAquivoByName(arquivo);

                list.Add(arquivo);
            }

        }

        return list;
    }

    void rptArquivosLista_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Ag2.Manager.Entity.Arquivo fileInfo = (Ag2.Manager.Entity.Arquivo)e.Item.DataItem;
        HtmlControl divIconFiles = (HtmlControl)e.Item.FindControl("divIconFiles");
        Literal ltrNomeArquivo = (Literal)e.Item.FindControl("ltrNomeArquivo");
        Literal ltrTamanho = (Literal)e.Item.FindControl("ltrTamanho");
        Literal ltrDataArquivo = (Literal)e.Item.FindControl("ltrDataArquivo");
        HiddenField hdnFileName = (HiddenField)e.Item.FindControl("hdnFileName");
        HiddenField hdnFilePath = (HiddenField)e.Item.FindControl("hdnFilePath");

        hdnFilePath.Value = string.Format("{0}{1}/", _uploadRoot, "uploadBrowser");
        hdnFileName.Value = fileInfo.nomeArquivo;

        CheckBox chkArquivo = (CheckBox)e.Item.FindControl("chkArquivo");

        if (isCkEditor)
            chkArquivo.CssClass = "ckEditorClass";

        if (fileInfo.extensao.Equals("doc", StringComparison.OrdinalIgnoreCase) ||
            fileInfo.extensao.Equals("docx", StringComparison.OrdinalIgnoreCase))
        {
            divIconFiles.Attributes.Add("class", "iconsFile icoDoc");
        }
        else if (fileInfo.extensao.Equals("ppt", StringComparison.OrdinalIgnoreCase) ||
                 fileInfo.extensao.Equals("pptx", StringComparison.OrdinalIgnoreCase))
        {
            divIconFiles.Attributes.Add("class", "iconsFile icoPpt");
        }
        else if (fileInfo.extensao.Equals("xls", StringComparison.OrdinalIgnoreCase) ||
                 fileInfo.extensao.Equals("xlsx", StringComparison.OrdinalIgnoreCase))
        {
            divIconFiles.Attributes.Add("class", "iconsFile icoXls");
        }
        else if (fileInfo.extensao.Equals("pdf", StringComparison.OrdinalIgnoreCase))
        {
            divIconFiles.Attributes.Add("class", "iconsFile icoPdf");
        }
        else if (fileInfo.extensao.Equals("txt", StringComparison.OrdinalIgnoreCase))
        {
            divIconFiles.Attributes.Add("class", "iconsFile icoTxt");
        }
        else if (fileInfo.extensao.Equals("zip", StringComparison.OrdinalIgnoreCase))
        {
            divIconFiles.Attributes.Add("class", "iconsFile icoZip");
        }
        else if (fileInfo.extensao.Equals("jpg", StringComparison.OrdinalIgnoreCase) ||
                 fileInfo.extensao.Equals("gif", StringComparison.OrdinalIgnoreCase) ||
                 fileInfo.extensao.Equals("png", StringComparison.OrdinalIgnoreCase))
        {
            divIconFiles.Attributes.Add("class", "iconsFile icoImg");
        }
        else
        {
            divIconFiles.Attributes.Add("class", "iconsFile icoOutros");
        }

        ltrNomeArquivo.Text = fileInfo.nomeArquivo;
        ltrTamanho.Text = string.Format("{0} kb", (fileInfo.tamanho / 1024).ToString());
        ltrDataArquivo.Text = fileInfo.dataCriacao.ToString("dd/MM/yyyy");

    }

    private void CarregaArquivos(List<Ag2.Manager.Entity.Arquivo> arquivos)
    {
        if (this.TipoArquivoSelecionado.Equals("excel"))
            tipoArquivo = Ag2.Manager.Enumerator.tipoArquivoBrowser.EXCEL;
        else if (this.TipoArquivoSelecionado.Equals("doc"))
            tipoArquivo = Ag2.Manager.Enumerator.tipoArquivoBrowser.DOC;
        else if (this.TipoArquivoSelecionado.Equals("images"))
            tipoArquivo = Ag2.Manager.Enumerator.tipoArquivoBrowser.IMAGES;
        else if (this.TipoArquivoSelecionado.Equals("outros"))
            tipoArquivo = Ag2.Manager.Enumerator.tipoArquivoBrowser.OUTROS;
        else if (this.TipoArquivoSelecionado.Equals("pdf"))
            tipoArquivo = Ag2.Manager.Enumerator.tipoArquivoBrowser.PDF;
        else if (this.TipoArquivoSelecionado.Equals("ppt"))
            tipoArquivo = Ag2.Manager.Enumerator.tipoArquivoBrowser.PPT;
        else if (this.TipoArquivoSelecionado.Equals("todos"))
            tipoArquivo = Ag2.Manager.Enumerator.tipoArquivoBrowser.TODOS;
        else if (this.TipoArquivoSelecionado.Equals("zip"))
            tipoArquivo = Ag2.Manager.Enumerator.tipoArquivoBrowser.ZIP;

        if (arquivos == null)
            arquivos = uploadBrowserADO.CarregarAquivos(tipoArquivo);

        string[] extensoespermitidas = AllowedExtensions.Split(',');
        List<Ag2.Manager.Entity.Arquivo> listaTemp = new List<Ag2.Manager.Entity.Arquivo>();

        //FILTRA EXTENSOES PERMITIDAS
        if (extensoespermitidas.Length >= 1 && !extensoespermitidas[0].Equals(string.Empty))
        {
            foreach (Ag2.Manager.Entity.Arquivo arq in arquivos)
            {
                if (permiteArquivo(arq))
                {
                    listaTemp.Add(arq);
                }
            }
        }
        else
            listaTemp = arquivos;

        if (tipoArquivo == Ag2.Manager.Enumerator.tipoArquivoBrowser.IMAGES)
        {
            rptArquivos.Visible = true;
            rptArquivos.DataSource = listaTemp.ToList();
            rptArquivos.DataBind();
        }
        else
        {
            rptArquivosLista.Visible = true;
            rptArquivosLista.DataSource = listaTemp.ToList();
            rptArquivosLista.DataBind();
        }
    }

    private bool permiteArquivo(Ag2.Manager.Entity.Arquivo arquivo)
    {
        string[] extensoespermitidas = AllowedExtensions.Split(',');

        if (extensoespermitidas.Length.Equals(1) && extensoespermitidas[0].Equals(string.Empty))
            return true;

        string extTemp = string.Empty;

        foreach (string ext in extensoespermitidas)
        {
            extTemp = ext.Replace("*", string.Empty).ToLower();

            if (Path.GetExtension(arquivo.nomeArquivo).ToLower().Equals(extTemp, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }

    void rptArquivos_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Ag2.Manager.Entity.Arquivo fileInfo = (Ag2.Manager.Entity.Arquivo)e.Item.DataItem;
        Image imgThumb = (Image)e.Item.FindControl("imgThumb");
        Literal ltrData = (Literal)e.Item.FindControl("ltrData");
        Literal ltrTamanho = (Literal)e.Item.FindControl("ltrTamanho");
        Literal ltrDimensao = (Literal)e.Item.FindControl("ltrDimensao");
        CheckBox chkArquivo = (CheckBox)e.Item.FindControl("chkArquivo");
        HiddenField hdnFileName = (HiddenField)e.Item.FindControl("hdnFileName");
        HiddenField hdnFilePath = (HiddenField)e.Item.FindControl("hdnFilePath");

        hdnFilePath.Value = ResolveUrl(string.Format("{0}{1}/", _uploadRoot, "uploadBrowser"));
        hdnFileName.Value = fileInfo.nomeArquivo;

        string fileUploadPath = string.Concat(hdnFilePath.Value, hdnFileName.Value);

        if (isCkEditor)
            chkArquivo.CssClass = "ckEditorClass";

        if (fileInfo.extensao.Equals("doc", StringComparison.OrdinalIgnoreCase) ||
            fileInfo.extensao.Equals("docx", StringComparison.OrdinalIgnoreCase))
        {
            imgThumb.ImageUrl = "~/img/icon_word.png";
        }
        else if (fileInfo.extensao.Equals("ppt", StringComparison.OrdinalIgnoreCase) ||
                 fileInfo.extensao.Equals("pptx", StringComparison.OrdinalIgnoreCase))
        {
            imgThumb.ImageUrl = "~/img/icon_powerPoint.png";
        }
        else if (fileInfo.extensao.Equals("xls", StringComparison.OrdinalIgnoreCase) ||
                 fileInfo.extensao.Equals("xlsx", StringComparison.OrdinalIgnoreCase))
        {
            imgThumb.ImageUrl = "~/img/icon_excel.png";
        }
        else if (fileInfo.extensao.Equals("pdf", StringComparison.OrdinalIgnoreCase))
        {
            imgThumb.ImageUrl = "~/img/icon_pdf.png";
        }
        else if (fileInfo.extensao.Equals("txt", StringComparison.OrdinalIgnoreCase))
        {
            imgThumb.ImageUrl = "~/img/icon_txt.png";
        }
        else if (fileInfo.extensao.Equals("zip", StringComparison.OrdinalIgnoreCase))
        {
            imgThumb.ImageUrl = "~/img/icon_zip.png";
        }
        else if (fileInfo.extensao.Equals("jpg", StringComparison.OrdinalIgnoreCase) ||
                 fileInfo.extensao.Equals("gif", StringComparison.OrdinalIgnoreCase) ||
                 fileInfo.extensao.Equals("png", StringComparison.OrdinalIgnoreCase))
        {
            imgThumb.ImageUrl = string.Format("~/thumb.aspx?file={0}&w={1}&h={2}", fileUploadPath, "122", "90");
        }

        ltrData.Text = fileInfo.dataCriacao.ToString("dd/MM/yyyy");
        ltrTamanho.Text = string.Format("{0} kb", (fileInfo.tamanho / 1024).ToString());

        System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath(fileUploadPath));
        ltrDimensao.Text = string.Format("{0} x {1}", image.Width, image.Height);
        image.Dispose();
    }

    void btnUpload_Click(object sender, ImageClickEventArgs e)
    {
        Ag2.Manager.Entity.Arquivo arquivo = new Ag2.Manager.Entity.Arquivo();


        arquivo.nomeArquivo = FileUpload1.FileName;

        if (string.Compare(".aspx", Path.GetExtension(FileUpload1.FileName), true) == 0 ||
                string.Compare(".asp", Path.GetExtension(FileUpload1.FileName), true) == 0 ||
                string.Compare(".bat", Path.GetExtension(FileUpload1.FileName), true) == 0 ||
                string.Compare(".php", Path.GetExtension(FileUpload1.FileName), true) == 0 ||
                string.Compare(".js", Path.GetExtension(FileUpload1.FileName), true) == 0 ||
                string.Compare(".css", Path.GetExtension(FileUpload1.FileName), true) == 0 ||
                string.Compare(".config", Path.GetExtension(FileUpload1.FileName), true) == 0 ||
                string.Compare(".ascx", Path.GetExtension(FileUpload1.FileName), true) == 0 ||
                string.Compare(".rescx", Path.GetExtension(FileUpload1.FileName), true) == 0 ||
                string.Compare(".sql", Path.GetExtension(FileUpload1.FileName), true) == 0 ||
                string.Compare(".exe", Path.GetExtension(FileUpload1.FileName), true) == 0 ||
                !FileUpload1.HasFile ||
                !permiteArquivo(arquivo))
        {
            Ag2.Manager.Helper.WriteScriptOnPage wsop = new WriteScriptOnPage();
            wsop.AddAlert("Arquivo inválido.");
            wsop.Bind();
            CarregaArquivos(null);
            return;
        }

        string fileUploadPath = string.Format("{0}{1}/{2}", _uploadRoot, "uploadBrowser", FileUpload1.FileName);
        string uploadPath = string.Format("{0}{1}", _uploadRoot, "uploadBrowser");
        string fileName = FileUpload1.FileName;

        if (File.Exists(Server.MapPath(fileUploadPath)))
        {
            string[] splitFileName = FileUpload1.FileName.Split('.');
            fileName = string.Concat(splitFileName[0], "_", DateTime.Now.ToString("yyyyMMddHHmmss"), ".", splitFileName[1]);
        }

        uploadPath = string.Concat(uploadPath, "/", fileName);

        FileUpload1.SaveAs(Server.MapPath(uploadPath));
        arquivo.extensao = Path.GetExtension(FileUpload1.FileName);
        arquivo.nomeArquivo = fileName;
        arquivo.nomeOriginal = FileUpload1.FileName;
        arquivo.tamanho = FileUpload1.FileContent.Length;
        arquivo.titulo = txtTitulo.Text;

        arquivo = uploadBrowserADO.Save(arquivo);

        List<Ag2.Manager.Entity.Tag> tags = GetTags(txtPalavraChave.Text);
        List<Ag2.Manager.Entity.Tag> tagsTemp = new List<Ag2.Manager.Entity.Tag>();

        foreach (Ag2.Manager.Entity.Tag tag in tags)
        {
            tagsTemp.Add(tagADO.Save(tag));
        }

        uploadBrowserADO.SaveArquivoTags(arquivo, tagsTemp);

        CarregaArquivos(null);

        txtTitulo.Text = string.Empty;
        txtPalavraChave.Text = string.Empty;

    }

    private List<Ag2.Manager.Entity.Tag> GetTags(string palavraChave)
    {
        string[] palavras = palavraChave.Split(' ');
        List<Ag2.Manager.Entity.Tag> tags = new List<Ag2.Manager.Entity.Tag>();
        Ag2.Manager.Entity.Tag tag = null;

        if (palavras.Length.Equals(1) && palavras[0].Equals(string.Empty))
            return tags;

        foreach (string palavra in palavras)
        {
            if (Ag2.Manager.Helper.Util.ExistInDicionary(palavra))
                continue;

            tag = new Ag2.Manager.Entity.Tag();
            tag.nome = palavra;
            tags.Add(tag);
        }

        return tags;
    }

    [WebMethod]
    [ScriptMethod]
    public static string DeleteFile(string filePath)
    {
        Ag2.Manager.DAL.IUploadBrowserDAL uploadBrowserADO = (Ag2.Manager.DAL.IUploadBrowserDAL)Util.GetADO("UploadBrowserADO", (System.Reflection.Assembly)System.Web.Compilation.BuildManager.CodeAssemblies[0]);

        string fileName = Path.GetFileName(filePath);

        Ag2.Manager.Entity.Arquivo arquivo = new Ag2.Manager.Entity.Arquivo();
        arquivo.nomeArquivo = fileName;

        try
        {
            uploadBrowserADO.Delete(arquivo);
            System.IO.File.Delete(HttpContext.Current.Server.MapPath(filePath));
        }
        catch (Exception ex)
        {
            if (ex.Message.IndexOf("FK_", StringComparison.OrdinalIgnoreCase) > -1)
                return "Este arquivo não pode ser apagado por que está sendo usado por outro cadastro.";
        }

        return "true";
    }

    [WebMethod]
    [ScriptMethod]
    public static string RenameFile(string filePathOriginal, string filePathNovo)
    {
        Ag2.Manager.DAL.IUploadBrowserDAL uploadBrowserADO = (Ag2.Manager.DAL.IUploadBrowserDAL)Util.GetADO("UploadBrowserADO", (System.Reflection.Assembly)System.Web.Compilation.BuildManager.CodeAssemblies[0]);

        string fileOriginal = System.IO.Path.GetFileName(filePathOriginal);
        string fileNovo = System.IO.Path.GetFileName(filePathNovo);
        string extOriginal = System.IO.Path.GetExtension(filePathOriginal);
        string extNovo = System.IO.Path.GetExtension(filePathNovo);

        if (!extOriginal.Equals(extNovo, StringComparison.OrdinalIgnoreCase))
        {
            return "Você não pode alterar a extensão do arquivo.";
        }

        string novoPathFile = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(filePathOriginal), filePathNovo);

        if (!System.IO.File.Exists(HttpContext.Current.Server.MapPath(novoPathFile)))
        {
            System.IO.File.Move(HttpContext.Current.Server.MapPath(filePathOriginal), HttpContext.Current.Server.MapPath(novoPathFile));

            Ag2.Manager.Entity.Arquivo arquivoOriginal = new Ag2.Manager.Entity.Arquivo();
            arquivoOriginal.nomeArquivo = fileOriginal;

            Ag2.Manager.Entity.Arquivo arquivoNovo = new Ag2.Manager.Entity.Arquivo();
            arquivoNovo.nomeArquivo = fileNovo;

            uploadBrowserADO.RenomearArquivo(arquivoOriginal, arquivoNovo);
        }
        else
        {
            return "Já existe um arquivo com este nome no sistema.";
        }

        return "true";
    }

    private string TipoArquivoSelecionado
    {
        get
        {
            if (ViewState["TipoArquivoSelecionado"] == null)
                ViewState["TipoArquivoSelecionado"] = string.Empty;
            return ViewState["TipoArquivoSelecionado"].ToString();
        }
        set
        {
            ViewState["TipoArquivoSelecionado"] = value;
        }
    }
}
