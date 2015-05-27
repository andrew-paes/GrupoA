using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Ag2.Manager.Module;
using Ag2.Manager.Helper;
using Ag2.Manager.WebUI;
using System.Web.Services;
using System.Web.Script.Services;

public partial class content_edit : ManagerPage
{

    private ManagerModule _activeModule;        //instancia do modulo ativo
    private int _registerId;                    //ID do registro que será editado
    private string moduleName = string.Empty;
    private string tipoForm = string.Empty;
    private string selectType = string.Empty;
    private string controlIdCallModal = string.Empty;
    private Ag2.Security.SecureQueryString sq = null;

    protected void Page_Init(object o, EventArgs e)
    {
        //monta caminho do modulo
        moduleName = Ag2.Manager.Helper.Util.GetQueryString("md").ToString();

        VerificaPermissoes();

        sq = new Ag2.Security.SecureQueryString();
        sq["md"] = moduleName;

        //Monta Url da listagem
        lnkListagem.NavigateUrl = string.Concat("List.aspx?q=", sq.ToString());

        if (!string.IsNullOrEmpty(Ag2.Manager.Helper.Util.GetQueryString("t")))
        {
            tipoForm = Ag2.Manager.Helper.Util.GetQueryString("t").ToString();

            if (tipoForm.Equals("modal", StringComparison.OrdinalIgnoreCase))
            {
                Page.Master.FindControl("phConteudoTR1").Visible = false;
                Page.Master.FindControl("phConteudoTD1").Visible = false;

                System.Text.StringBuilder sbScript = new System.Text.StringBuilder();
                Control ctrl = Page.Master.FindControl("cphScripts");

                HtmlGenericControl script = new HtmlGenericControl("script");
                script.Attributes.Add("type", "text/javascript");

                sbScript.Append("$(document).ready(function() {").AppendLine();

                sbScript.Append("$('.conteudo').css('border','1px solid #ccc');").AppendLine();
                sbScript.Append("$('#trBorda TD.borda2').hide();").AppendLine();
                sbScript.Append("});").AppendLine();

                //Escreve o bloco de script referente a configurações da modal
                script.Controls.Add(new LiteralControl(sbScript.ToString()));
                ctrl.Controls.Add(script);

                if (!string.IsNullOrEmpty(Ag2.Manager.Helper.Util.GetQueryString("ts")))
                {
                    selectType = Ag2.Manager.Helper.Util.GetQueryString("ts").ToString();
                }
                if (!string.IsNullOrEmpty(Ag2.Manager.Helper.Util.GetQueryString("control")))
                {
                    controlIdCallModal = Ag2.Manager.Helper.Util.GetQueryString("control").ToString();
                }

                //Monta Url da listagem
                if (selectType.Length > 0 && controlIdCallModal.Length > 0)
                {
                    sq = new Ag2.Security.SecureQueryString();
                    sq["t"] = tipoForm;
                    sq["md"] = moduleName;
                    sq["ts"] = selectType;
                    sq["control"] = controlIdCallModal;
                    lnkListagem.NavigateUrl = string.Format("List.aspx?q={0}", sq.ToString());
                }
                else
                    lnkListagem.Visible = false;
            }
        }

        RegisterNavigator1.ModuleName = moduleName;

        //instantica novo modulo 
        _activeModule = new ManagerModule();

        //carrega modulo
        _activeModule.Load(moduleName, (System.Reflection.Assembly)System.Web.Compilation.BuildManager.CodeAssemblies[0]);

        //Configura titulo do modulo            
        lblModuleTitle.Text = _activeModule.ModuleStructure.Title;

        //cria formulario
        _activeModule.CreateForm();

        //carrega as definições do formulario
        _activeModule.ConfigureForm();
    }

    private void VerificaPermissoes()
    {
		lnkListagem.Visible = false;
		btnExecute.Visible = false;
		Ag2.Manager.Entity.ag2mngMenu menu = Util.GetModulePermissions(moduleName);

		if (menu != null)
		{
			lnkListagem.Visible = (menu.fullControl || menu.canUpdate || menu.canRead || menu.canDelete);
			btnExecute.Visible = (menu.fullControl || menu.canInsert || menu.canUpdate);
		}
		else
		{
			Response.Redirect("~/Default.aspx");
		}
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //captura ID que está sendo editado
            if (Util.GetRequestId().Equals(0))
            {
                _registerId = 0;
            }
            else
            {
                _registerId = Util.GetRequestId();
            }

            //salva ID no viewState
            ViewState["registerId"] = _registerId;

            //popula propriedade
            _activeModule.RegisterID = _registerId;

            //se é edição de registro carrega dados
            if (_registerId > 0)
            {
                _activeModule.FillForm();
            }
        }
        else
        {
            //resgata ID no viewState
            _registerId = (int)ViewState["registerId"];
        }

        //popula propriedade
        _activeModule.RegisterID = _registerId;


    }

    protected void btnExecute_Click(object sender, ImageClickEventArgs e)
    {

        //salva os dados do formulario     
        string msg = "";
        string fields = "";
        bool validForm = true;

        //verifica se existe valor no banco
        IList a = _activeModule.VerifyUniqueValues();

        if (a.Count > 0)
        {
            ((TextBox)fieldsHolder.Controls[0].FindControl(a[0].ToString())).BackColor = System.Drawing.Color.FromArgb(255, 153, 102);
            foreach (string campo in a)
            {
                if (!fields.Equals(""))
                {
                    fields = fields + ",";
                }
                fields = fields + campo;
            }

            msg = "O Cadastro não pode ser realizado pois já existem outros cadastros com os mesmo valores.<br />Os campos assinalados precisam ter seu valor alterado.";
            Ag2.Manager.Helper.Util.ShowMessage(msg);
            validForm = false;
            Util.ShowMessage(msg);
        }

        if (validForm)
        {
            if (_activeModule.SaveForm())
            {
                if (_registerId == 0)
                {
                    //limpa campos do formulario
                    _activeModule.ClearForm();

                    Util.ShowInsertMessage();
                }
                else
                {
                    Util.ShowUpdateMessage();
                }
            }
            else
            {
                Util.ShowMessage("Ocorreu um erro na gravação", Ag2.Manager.Enumerator.typeMessage.Erro);
            }
        }

    }

    [WebMethod]
    [ScriptMethod]
    public static string CarregaConteudoGrids(string controlId, string strIDs)
    {
        string[] Ids = strIDs.Split("|".ToCharArray());
        System.Text.StringBuilder sbHtml = new System.Text.StringBuilder();

        sbHtml.Append("<table border=\"0\" class=\"ag2SubForm\" cellpadding=\"0\" cellspacing=\"0\">");

        List<string> Headers = new List<string>();
        Headers.Add("Notícia");
        Headers.Add("Categoria");

        //IMPRIMI HEADER DA TABELA
        sbHtml.Append("<tr>").AppendLine();

        sbHtml.Append("<td class=\"tdHeader first\">").AppendLine();
        sbHtml.Append("<Input type=\"checkbox\" />").AppendLine();
        sbHtml.Append("</td>").AppendLine();

        foreach (string header in Headers)
        {
            sbHtml.Append("<td class=\"tdHeader\">").AppendLine();
            sbHtml.Append(header).AppendLine();
            sbHtml.Append("</td>").AppendLine();
        }

        sbHtml.Append("</tr>").AppendLine();

        //IMPRIMI LINHAS DA TABELA
        foreach (string id in Ids)
        {
            sbHtml.Append("<tr>").AppendLine();
            sbHtml.Append("<td class=\"tdId\">").AppendLine();
            sbHtml.Append("<Input class=\"chkItemSubForm\" type=\"checkbox\" />").AppendLine();
            sbHtml.Append("</td>").AppendLine();
            sbHtml.Append("<td class=\"tdItem\">").AppendLine();
            sbHtml.Append(id.ToString()).AppendLine();
            sbHtml.Append("</td>").AppendLine();
            sbHtml.Append("<td class=\"tdItem\">").AppendLine();
            sbHtml.Append(id.ToString()).AppendLine();
            sbHtml.Append("</td>").AppendLine();
            sbHtml.Append("</tr>").AppendLine();
        }

        sbHtml.Append("</table>");

        Ag2.Manager.Helper.CurrentSessions.AddContentControl(controlId, sbHtml.ToString());

        return sbHtml.ToString();
    }

}
