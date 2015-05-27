using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GrupoA.GlobalResources;

public partial class content_ModalUploadFile : System.Web.UI.Page
{
    private string _idCtrl = string.Empty;
    private string _targetFolder = string.Empty;
    private string _allowedExtensions = string.Empty;
    private Int32 _maxFileLength = 100;

    protected void Page_Load(object sender, EventArgs e)
    {
        btnEnviar.Click += new EventHandler(btnEnviar_Click);
        btnEnviar.Attributes.Add("style", "display: none;");

        if (!string.IsNullOrEmpty(Request.QueryString["idCtrl"]))
        {
            _idCtrl = Request.QueryString["idCtrl"].ToString();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["t"]))
        {
            _targetFolder = Request.QueryString["t"].ToString();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["a"]))
        {
            _allowedExtensions = Request.QueryString["a"].ToString();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["m"]))
        {
            _maxFileLength = Convert.ToInt32(Request.QueryString["m"].ToString());
        }
    }
    protected void btnEnviar_Click(object sender, EventArgs e)
    {
        //string saveToFolder = Page.ResolveClientUrl(System.Configuration.ConfigurationManager.AppSettings["uploadRoot"]);
        string saveToFolder = Page.ResolveClientUrl(GrupoA_Resource.baseUrlUpload);
        string newFileName = string.Empty;
        string Path = string.Empty;
        int fileKb = Convert.ToInt32(FileUpload1.FileContent.Length) / 1024;
        string[] extensions = _allowedExtensions.Split(',');
        bool achouExtension = false;

        foreach (string ext in extensions)
        {
            string extFile = System.IO.Path.GetExtension(FileUpload1.FileName);
            string extAceita = System.IO.Path.GetExtension(ext);
            if (extFile.Equals(extAceita))
            {
                achouExtension = true;
                break;
            }
        }

        if (!achouExtension)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "modalUploads", "alert('Arquivo inválido.');", true);
            return;
        }

        if (fileKb > _maxFileLength)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "modalUploads", "alert('O arquivo acedeu o tamanho limite.');", true);
            return;
        }

        saveToFolder += _targetFolder;

        Path = Server.MapPath(saveToFolder);

        newFileName = string.Format("{0}_{1}", DateTime.Now.ToString("ddMMyyyyHHmmss"), FileUpload1.FileName).Replace(" ", "_");

        //cria pastas
        CreateDirectory(Path);

        string newName = newFileName;
        string extension = System.IO.Path.GetExtension(newName).Replace(".", "");

        if (string.Compare("aspx", extension, true) == 0 ||
            string.Compare("asp", extension, true) == 0 ||
            string.Compare("bat", extension, true) == 0 ||
            string.Compare("exe", extension, true) == 0)
        {
            return;
        }

        FileUpload1.SaveAs(string.Concat(Path, "\\", newName));

        Page.ClientScript.RegisterStartupScript(this.GetType(), "modalUploads", "$(document).ready(function() {parent.UpdateUploadFile('" + newName + "', '" + _idCtrl + "');});", true);
    }

    public void CreateDirectory(string path)
    {
        if (!System.IO.Directory.Exists(path))
        {
            System.IO.Directory.CreateDirectory(path);
        }
    }
}
