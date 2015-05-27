using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CKEditor.NET;
using CKFinder.Settings;
//using FredCK.FCKeditorV2;


//System.Web.UI.WebControls.TextBox
public partial class ctl_ag2HtmlEditor : Ag2.Manager.DAL.IHtmlTextBox
{
    #region Atributos

    private string _text;
    private int _maxLength;
    private int _width = 600;
    private Ag2.Manager.Enumerator.CkEditorToolBar _toolBar = Ag2.Manager.Enumerator.CkEditorToolBar.Basic;

    #endregion

    #region Propriedates

    //public override string Text
    //{
    //    set
    //    {
    //        _text = value;
    //        FCKeditor1.Value = HttpUtility.HtmlDecode(_text);
    //    }
    //    get
    //    {
    //        _text = HttpUtility.HtmlEncode(FCKeditor1.Value);
    //        if (_text.Equals(string.Empty))
    //            _text = " ";
    //        return _text;
    //    }
    //}

    public override string Text
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

    public int MaxLength
    {
        set
        {
            _maxLength = value;
        }
    }

    public int Width
    {
        set
        {
            _width = value;
        }
    }

    public Ag2.Manager.Enumerator.CkEditorToolBar ToolBar
    {
        get { return _toolBar; }
        set { _toolBar = value; }
    }

    #endregion

    #region Métodos

    protected void Page_Load(object sender, EventArgs e)
    {
        
        txtCKEditor.Toolbar = ToolBar.ToString();
        txtCKEditor.Width = new Unit(_width);

        CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
        _FileBrowser.BasePath = "../js/ckfinder/";
        _FileBrowser.SetupCKEditor(txtCKEditor);
        
        //FCKeditor1.ToolbarSet = "MenuArtmed";
        ////FCKeditor1.EditorAreaCSS = "~/FCKeditor/hmv.css";
        //FCKeditor1.EditorAreaCSS = "../hmv.css";
        //FCKeditor1.Width = _width;
    }
    #endregion
}
