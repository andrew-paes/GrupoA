using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ctl_ag2UploadFile : System.Web.UI.UserControl
{
    private string _targetFolder = string.Empty;
    private string _allowedExtensions = string.Empty;
    private Int32 _maxFileLength = 100;

    protected void Page_Load(object sender, EventArgs e)
    {
        txtArquivo.Attributes.Add("OnKeyDown", "return false;");

        lnkUploadModal.NavigateUrl = string.Format("~/content/ModalUploadFile.aspx?idCtrl={0}&t={1}&a={2}&m={3}",
            lnkUploadModal.ClientID,
            this._targetFolder,
            this._allowedExtensions,
            this._maxFileLength.ToString());

        ltrTamanhoMaximo.Text = string.Format("{0} kb", _maxFileLength);
        ltrTipoArquivo.Text = _allowedExtensions;
    }

    public string FileName
    {
        get { return this.txtArquivo.Text; }
        set { txtArquivo.Text = value; }
    }

    public string TargetFolder
    {
        get { return this._targetFolder; }
        set { this._targetFolder = value; }
    }
    public string AllowedExtensions
    {
        get { return this._allowedExtensions; }
        set { this._allowedExtensions = value; }
    }
    public Int32 MaxFileLength
    {
        get { return this._maxFileLength; }
        set { this._maxFileLength = value; }
    }
    public string Label
    {
        get { return ltrLabel.Text; }
        set { ltrLabel.Text = value; }
    }
}
