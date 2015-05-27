using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using CKEditor.NET;
using CKFinder.Settings;

public partial class ctl_ag2CkEditor : System.Web.UI.UserControl
{
    private Ag2.Manager.Enumerator.CkEditorToolBar _toolBar = Ag2.Manager.Enumerator.CkEditorToolBar.Basic;

    protected void Page_Load(object sender, EventArgs e)
    {
        txtCKEditor.Toolbar = this.ToolBar.ToString();

        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "../ckfinder/";
        _FileBrowser.SetupCKEditor(txtCKEditor);

        //ContentPlaceHolder cph = (ContentPlaceHolder)Page.Master.FindControl("cphScripts");

        //HtmlGenericControl script = new HtmlGenericControl("script");
        //script.Attributes.Add("type", "text/javascript");
        //script.Attributes.Add("src", "../js/ckeditor/ckeditor.js");
        //script.ID = "scriptCkEditor";

        //Control ctrlScript = cph.FindControl("scriptCkEditor");

        //if (ctrlScript == null)
        //    cph.Controls.Add(script);

        //script = new HtmlGenericControl("script");
        //script.Attributes.Add("type", "text/javascript");
        //script.Attributes.Add("src", "../js/ckeditor/adapters/jquery.js");
        //script.ID = "scriptCkEditorAdapter";

        //ctrlScript = cph.FindControl("scriptCkEditorAdapter");

        //if (ctrlScript == null)
        //    cph.Controls.Add(script);

        ////ScriptManager.RegisterStartupScript(Page, Page.GetType(), this.ClientID, "$(document).ready(function() {SetEditorCKEditor('" + txtCKEditor.ClientID + "','" + ToolBar.ToString() + "');});", true);

        ////if (lblLabel.Text.Length.Equals(0))
        ////    lblLabel.Visible = false;

        ////cvCKEditor.Attributes.Add("textBoxValidate", this.txtCKEditor.ClientID);
    }

    public Ag2.Manager.Enumerator.CkEditorToolBar ToolBar
    {
        get { return _toolBar; }
        set { _toolBar = value; }
    }

    public string Text
    {
        get
        {
            return this.txtCKEditor.Text;
        }
        set
        {
            string conteudo = value;
            //string navegador = Request.ServerVariables["HTTP_USER_AGENT"].ToString();

            ////RETIRA OS RETURN NO HTML
            //if (navegador.IndexOf("chrome", StringComparison.OrdinalIgnoreCase) > -1)
            //    conteudo = Server.HtmlEncode(conteudo.Replace(Convert.ToChar(10).ToString(), string.Empty).Replace(Convert.ToChar(13).ToString(), string.Empty));
            //else
            //    conteudo = conteudo.Replace(Convert.ToChar(10).ToString(), string.Empty).Replace(Convert.ToChar(13).ToString(), string.Empty);

            this.txtCKEditor.Text = conteudo;
        }
    }

    //public string AjaxText
    //{
    //    get { return this.txtCKEditor.Text; }
    //    set
    //    {

    //        //SQL_Latin1_General_CP1_CI_AS
    //        string conteudo = value;
    //        string navegador = Request.ServerVariables["HTTP_USER_AGENT"].ToString();

    //        //RESOLVE BUG DO RENDER HTML NO CHROME
    //        if (navegador.IndexOf("chrome", StringComparison.OrdinalIgnoreCase) > -1)
    //            conteudo = Server.HtmlEncode(conteudo.Replace(Convert.ToChar(10).ToString(), string.Empty).Replace(Convert.ToChar(13).ToString(), string.Empty));
    //        else
    //            conteudo = conteudo.Replace(Convert.ToChar(10).ToString(), string.Empty).Replace(Convert.ToChar(13).ToString(), string.Empty);

    //        Ag2.Manager.Helper.WriteScriptOnPage wsop = new Ag2.Manager.Helper.WriteScriptOnPage();
    //        wsop.Add("$('#" + this.txtCKEditor.ClientID + "').val('" + conteudo + "');");
    //        wsop.Bind();

    //        //SOMENTE PARA MOSTRAR NO DEBUG
    //        this.txtCKEditor.Text = conteudo;
    //    }
    //}

    //public string Label
    //{
    //    get { return this.lblLabel.Text; }
    //    set { this.lblLabel.Text = value; }
    //}
}
