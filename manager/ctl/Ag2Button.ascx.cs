using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ctl_Ag2Button : System.Web.UI.UserControl
{
    public enum enunTipoLayout
    {
        BRANCO, //DEFAULT
        LARANJA
    }

    public event EventHandler Click;
    private Dictionary<string, string> _attributes = new Dictionary<string, string>();
    private string _text = string.Empty;
    private string _cssClass = "btnManager";
    private string _rel = string.Empty;
    private enunTipoLayout _tipoLayout;
    private bool _executeEventPostback = true;

    protected void Page_Load(object sender, EventArgs e)
    {
        Control btnAcao = null;
        Panel pnlContent = new Panel();
        Panel pnlLeft = new Panel();
        Panel pnlCenter = new Panel();
        Panel pnlRigth = new Panel();

        if (_executeEventPostback)
        {
            btnAcao = new LinkButton();
            ((LinkButton)btnAcao).Click += new EventHandler(btnAcao_Click);
        }
        else
        {
            btnAcao = new HyperLink();
        }

        foreach (KeyValuePair<string, string> item in _attributes)
        {
            if (_executeEventPostback)
            {
                ((LinkButton)btnAcao).Attributes.Add(item.Key, item.Value);
            }
            else
            {
                ((HyperLink)btnAcao).Attributes.Add(item.Key, item.Value);
            }
        }

        if (_tipoLayout == enunTipoLayout.BRANCO)
        {
            pnlContent.Attributes.Add("style", "border: 0px solid #ccc; float: left;");
            pnlLeft.Attributes.Add("style", "border: 0px solid red; background-image: url(" + ResolveUrl("~/img/btnLeftBorder.gif") + ");width: 23px; height: 22px; float: left; background-repeat: no-repeat;");
            pnlCenter.Attributes.Add("style", "border: 0px solid blue; float: left; background-image: url(" + ResolveUrl("~/img/btnBgCenter.gif") + ");background-repeat: repeat-x; height: 22px; padding-top: 3px;");
            pnlCenter.Controls.Add(new LiteralControl(_text));
            pnlRigth.Attributes.Add("style", "border: 0px solid black; background-image: url(" + ResolveUrl("~/img/btnRightBorder.gif") + ");width: 10px; height: 22px; float: left; background-repeat: no-repeat;");
        }
        else if (_tipoLayout == enunTipoLayout.LARANJA)
        {
            pnlContent.Attributes.Add("style", "border: 0px solid #ccc; float: left;");
            pnlLeft.Attributes.Add("style", "border: 0px solid red; background-image: url(" + ResolveUrl("~/img/btnLeftBorderOrange.png") + ");width: 23px; height: 22px; float: left; background-repeat: no-repeat;");
            pnlCenter.Attributes.Add("style", "border: 0px solid blue; float: left; background-image: url(" + ResolveUrl("~/img/btnBgCenterOrange.png") + ");background-repeat: repeat-x; height: 22px; padding-top: 3px; color: #fff;");
            pnlCenter.Controls.Add(new LiteralControl(_text));
            pnlRigth.Attributes.Add("style", "border: 0px solid black; background-image: url(" + ResolveUrl("~/img/btnRightBorderOrange.png") + ");width: 10px; height: 22px; float: left; background-repeat: no-repeat;");
        }

        pnlContent.Controls.Add(pnlLeft);
        pnlContent.Controls.Add(pnlCenter);
        pnlContent.Controls.Add(pnlRigth);

        btnAcao.Controls.Add(pnlContent);

        if (_executeEventPostback)
        {
            ((LinkButton)btnAcao).CssClass = _cssClass;
            ((LinkButton)btnAcao).Attributes.Add("rel", _rel);

        }
        else
        {
            ((HyperLink)btnAcao).CssClass = _cssClass;
            ((HyperLink)btnAcao).Attributes.Add("rel", _rel);
        }


        cphBotao.Controls.Add(btnAcao);
    }

    void btnAcao_Click(object sender, EventArgs e)
    {
        if (Click != null)
        {
            Click(sender, e);
        }
    }

    public string Text
    {
        get { return _text; }
        set { _text = value; }
    }

    public string CssClass
    {
        get { return _cssClass; }
        set { _cssClass = value; }
    }

    public string Rel
    {
        get { return _rel; }
        set { _rel = value; }
    }

    public Dictionary<string, string> Attributes
    {
        get { return _attributes; }
        set { _attributes = value; }
    }

    public enunTipoLayout TipoLayout
    {
        get { return _tipoLayout; }
        set { _tipoLayout = value; }
    }

    public bool ExecuteEventPostback
    {
        get { return _executeEventPostback; }
        set { _executeEventPostback = value; }
    }

}
