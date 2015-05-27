using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using GrupoA.BusinessLogicalLayer;
using Ag2.Manager.Helper;
using GrupoA.GlobalResources;
using GrupoA.BusinessObject;

public partial class ctl_ag2ListFiles : System.Web.UI.UserControl
{
    public event EventHandler<ArquivoEventArgs> BindList;
    public event EventHandler<ArquivoEventArgs> DeleteItem;
    public enum enumTipoArquivo { ALL, IMAGE, DOCUMENT, PDF, DOC, XLS, PPT, ZIP, TXT, AUDIO };
    private enumTipoArquivo _tipoArquivos;
    private int _registroId = 0;
    private Int64 _maxFileLength = 300;
    private bool _editable = false;
    private int _arquivoId = 0;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (_registroId.Equals(0) && _arquivoId.Equals(0))
        {
            this.Visible = false;
            return;
        }

        hdnRegistroId.Value = _registroId.ToString();

        ContentPlaceHolder cphScript = Page.Master.FindControl("cphScript") as ContentPlaceHolder;

        HtmlGenericControl script = new HtmlGenericControl("script");
        script.Attributes.Add("type", "text/javascript");
        script.Attributes.Add("src", ResolveUrl("~/js/ag2/ag2ListFile.js"));
        cphScript.Controls.Add(script);

        lnkhidden.Click += new EventHandler(lnkhidden_Click);
        lnkhidden.Attributes.Add("style", "display: none;");

        hdnTipoArquivo.Value = _tipoArquivos.ToString();
        hdnTargetFolder.Value = this.TargetFolder;
        hdnMaxFileLength.Value = this._maxFileLength.ToString();

        hdnModuleName.Value = Ag2.Manager.Helper.Util.GetQueryString("md");
        hdnScriptModal.Value = this.ScriptModal;
    }

    void rptListagem_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        ItemArquivo itemArquivo = (ItemArquivo)e.Item.DataItem;
        HtmlGenericControl divThumb = (HtmlGenericControl)e.Item.FindControl("divThumb");
        Image imgThumb = (Image)e.Item.FindControl("imgThumb");
        ImageButton imgDelete = (ImageButton)e.Item.FindControl("imgDelete");
        ImageButton imgDownload = (ImageButton)e.Item.FindControl("imgDownload");
        Literal ltrArquivoNome = (Literal)e.Item.FindControl("ltrArquivoNome");
        Literal ltrTamanhoArquivo = (Literal)e.Item.FindControl("ltrTamanhoArquivo");
        PlaceHolder phEdit = (PlaceHolder)e.Item.FindControl("phEdit");
        ImageButton imgEdit = (ImageButton)e.Item.FindControl("imgEdit");

        if (Path.GetExtension(itemArquivo.CaminhoArquivo).Equals(".doc", StringComparison.OrdinalIgnoreCase)
            || Path.GetExtension(itemArquivo.CaminhoArquivo).Equals(".docx", StringComparison.OrdinalIgnoreCase))
        {
            divThumb.Attributes.Add("class", "divItemThumbDOC");
        }
        else if (Path.GetExtension(itemArquivo.CaminhoArquivo).Equals(".ppt", StringComparison.OrdinalIgnoreCase)
            || Path.GetExtension(itemArquivo.CaminhoArquivo).Equals(".pptx", StringComparison.OrdinalIgnoreCase))
        {
            divThumb.Attributes.Add("class", "divItemThumbPPT");
        }
        else if (Path.GetExtension(itemArquivo.CaminhoArquivo).Equals(".xls", StringComparison.OrdinalIgnoreCase)
            || Path.GetExtension(itemArquivo.CaminhoArquivo).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
        {
            divThumb.Attributes.Add("class", "divItemThumbXLS");
        }
        else if (Path.GetExtension(itemArquivo.CaminhoArquivo).Equals(".pdf", StringComparison.OrdinalIgnoreCase))
        {
            divThumb.Attributes.Add("class", "divItemThumbPDF");
        }
        else if (Path.GetExtension(itemArquivo.CaminhoArquivo).Equals(".zip", StringComparison.OrdinalIgnoreCase))
        {
            divThumb.Attributes.Add("class", "divItemThumbZIP");
        }
        else if (Path.GetExtension(itemArquivo.CaminhoArquivo).Equals(".txt", StringComparison.OrdinalIgnoreCase))
        {
            divThumb.Attributes.Add("class", "divItemThumbTXT");
        }
        else if (Path.GetExtension(itemArquivo.CaminhoArquivo).Equals(".mp3", StringComparison.OrdinalIgnoreCase))
        {
            divThumb.Attributes.Add("class", "divItemThumbMP3");
        }
        else if (Path.GetExtension(itemArquivo.CaminhoArquivo).Equals(".gif", StringComparison.OrdinalIgnoreCase)
            || Path.GetExtension(itemArquivo.CaminhoArquivo).Equals(".png", StringComparison.OrdinalIgnoreCase)
            || Path.GetExtension(itemArquivo.CaminhoArquivo).Equals(".jpg", StringComparison.OrdinalIgnoreCase))
        {
            divThumb.Attributes.Add("class", "divItemThumbIMG");
            imgThumb.Visible = true;
            imgThumb.ImageUrl = string.Format("~/Thumb.aspx?file={0}&w={1}&h={2}", itemArquivo.CaminhoArquivo, "122", "90");
        }

        ltrArquivoNome.Text = Path.GetFileName(itemArquivo.CaminhoArquivo);
        ltrTamanhoArquivo.Text = itemArquivo.TamanhoArquivo;
        imgDelete.CommandArgument = itemArquivo.ArquivoId.ToString();
        imgDelete.Attributes.Add("OnClick", "if(confirm('Deseja apagar este registro?')){return true;}else{return false;}");

        imgDownload.CommandArgument = itemArquivo.CaminhoArquivo;

        if (_editable)
        {
            phEdit.Visible = true;
            imgEdit.Attributes.Add("Onclick", string.Format("EditarRegistro({0}, '{1}'); return false;", itemArquivo.ArquivoId.ToString(), lnkInserirArquivo.ClientID));
        }
        else
            phEdit.Visible = false;


    }

    void lnkhidden_Click(object sender, EventArgs e)
    {
        ArquivoEventArgs arquivoEventArgs = new ArquivoEventArgs();
        arquivoEventArgs.ArquivoId = Convert.ToInt32(hdnArquivoId.Value);

        if (BindList != null)
        {
            if (MultiFile && BindList != null)
            {
                BindList(sender, arquivoEventArgs);
            }
            else
            {
                this.CarregaArquivo(arquivoEventArgs.ArquivoId);
                BindList(sender, arquivoEventArgs);
            }
        }
    }

    public void CarregaArquivo(int arquivoId)
    {
        List<ItemArquivo> arquivos = new List<ItemArquivo>();
        ItemArquivo itemArquivo = new ItemArquivo();

        var arquivo = new ArquivoBLL().CarregarArquivo(new GrupoA.BusinessObject.Arquivo() { ArquivoId = arquivoId });

        if (arquivo != null)
        {
            itemArquivo.ArquivoId = arquivo.ArquivoId;
            string pathFile = string.Concat(GrupoA_Resource.baseUrlUpload, this.TargetFolder, arquivo.NomeArquivo);
            itemArquivo.CaminhoArquivo = pathFile;
            itemArquivo.TamanhoArquivo = arquivo.TamanhoArquivo.ToString();
            arquivos.Add(itemArquivo);
            this.DataSource = arquivos;
        }
        this.DataBind();
    }

    public void CarregaArquivo(Produto produto)
    {
        List<ItemArquivo> arquivos = new List<ItemArquivo>();

        List<Arquivo> lArquivo = new ArquivoBLL().CarregarArquivo(new Produto() { ProdutoId = produto.ProdutoId });

        if (lArquivo.Count() > 0)
        {
            foreach (Arquivo arquivo in lArquivo)
            {
                ItemArquivo itemArquivo = new ItemArquivo();
                itemArquivo.ArquivoId = arquivo.ArquivoId;
                string pathFile = string.Concat(GrupoA_Resource.baseUrlUpload, this.TargetFolder, arquivo.NomeArquivo);
                itemArquivo.CaminhoArquivo = pathFile;
                itemArquivo.TamanhoArquivo = arquivo.TamanhoArquivo.ToString();
                arquivos.Add(itemArquivo);
            }

            this.DataSource = arquivos;
        }

        this.DataBind();
    }

    protected void imgDelete_Click(object sender, ImageClickEventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = true;

        if (DeleteItem != null)
        {
            ArquivoEventArgs arquivoEventArgs = new ArquivoEventArgs();
            arquivoEventArgs.ArquivoId = Convert.ToInt32(((ImageButton)sender).CommandArgument);

            DeleteItem(sender, arquivoEventArgs);
        }
    }

    protected void imgDownload_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgDownload = (ImageButton)sender;
        Ag2.Manager.Helper.Util.DownloadFile(Server.MapPath(imgDownload.CommandArgument), Path.GetFileName(imgDownload.CommandArgument), true);
    }

    public override void DataBind()
    {
        rptListagem.ItemDataBound += new RepeaterItemEventHandler(rptListagem_ItemDataBound);
        rptListagem.DataSource = this.DataSource;
        rptListagem.DataBind();
    }

    #region "PROPRIEDADES"

    public List<ItemArquivo> DataSource { get; set; }

    public string TargetFolder { get; set; }

    public bool MultiFile { get; set; }

    public bool Editable
    {
        get { return this._editable; }
        set { this._editable = value; }
    }

    public Int64 MaxFileLength
    {
        get { return this._maxFileLength; }
        set { this._maxFileLength = value; }
    }

    public int RegistroId
    {
        get { return this._registroId; }
        set { this._registroId = value; }
    }

    public int ArquivoId
    {
        get { return this._arquivoId; }
        set
        {
            this._arquivoId = value;
            this.CarregaArquivo(value);
        }
    }

    public enumTipoArquivo TipoArquivo
    {
        get { return this._tipoArquivos; }
        set { this._tipoArquivos = value; }
    }

    public string ScriptModal { get; set; }

    #endregion
}

public class ItemArquivo
{
    public ItemArquivo()
    {
        //CONSTRUTOR
    }

    public int ArquivoId { get; set; }
    public string CaminhoArquivo { get; set; }
    public string TamanhoArquivo { get; set; }
}

public class ArquivoEventArgs : EventArgs
{
    public ArquivoEventArgs()
    {
        //CONSTRUTOR
    }

    public int ArquivoId { get; set; }
}

