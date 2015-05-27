using System;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Reflection;
using Ag2.Manager.Module;
using Ag2.Manager.Module.Info;
using Ag2.Manager.BusinessObject;
using Ag2.Manager.WebUI;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

public partial class content_List : ManagerPage
{
    private string tipoForm = string.Empty;
    private string selectType = string.Empty;
    private string controlIdCallModal = string.Empty;
    private string _moduleName;
    private bool _filterData;
    private bool _showFilterBox;
    private ManagerModule activeModule;
    private Database db = DatabaseFactory.CreateDatabase();
    private Ag2.Manager.Helper.CurrentSessions cs = new Ag2.Manager.Helper.CurrentSessions();
    private OptionsController optionscontroller = new OptionsController();
    private Ag2.Security.SecureQueryString sq = null;
    private Ag2.Security.SecureQueryString sqGrid = null;
    private Ag2.Manager.Entity.ag2mngMenu menu = null;

    protected void Page_Init(object sender, EventArgs e)
    {
        //anula a sessao de ordenação de colunas sempre que entra a primeira vez na página
        if (!IsPostBack)
            SessaoOrdenacao = null;

        //monta caminho do modulo
        _moduleName = Ag2.Manager.Helper.Util.GetQueryString("md").ToString();

        menu = Ag2.Manager.Helper.Util.GetModulePermissions(_moduleName);
                
        sq = new Ag2.Security.SecureQueryString();
        sq["md"] = _moduleName;

        lnkNewRegister.NavigateUrl = string.Concat("edit.aspx?q=", sq.ToString());

        #region "SE FOR MODAL DESABILITA ALGUNS ITENS DE LAYOUT"

        if (!string.IsNullOrEmpty(Ag2.Manager.Helper.Util.GetQueryString("t")))
        {
            tipoForm = Ag2.Manager.Helper.Util.GetQueryString("t").ToString();

            if (tipoForm.Equals("modal", StringComparison.OrdinalIgnoreCase))
            {

                if (!string.IsNullOrEmpty(Ag2.Manager.Helper.Util.GetQueryString("control")))
                {
                    controlIdCallModal = Ag2.Manager.Helper.Util.GetQueryString("control").ToString();
                }

                Page.Master.FindControl("phConteudoTR1").Visible = false;
                Page.Master.FindControl("phConteudoTD1").Visible = false;

                System.Text.StringBuilder sbScript = new System.Text.StringBuilder();
                Control ctrl = Page.Master.FindControl("cphScripts");

                HtmlGenericControl script = new HtmlGenericControl("script");
                script.Attributes.Add("type", "text/javascript");

                sbScript.Append("$(document).ready(function() {").AppendLine();

                sbScript.Append("$('.conteudo').css('border','1px solid #ccc');").AppendLine();
                sbScript.Append("$('#trBorda TD.borda2').hide();").AppendLine();

                if (!string.IsNullOrEmpty(Ag2.Manager.Helper.Util.GetQueryString("ts")))
                {
                    selectType = Ag2.Manager.Helper.Util.GetQueryString("ts").ToString();

                    switch (selectType)
                    {
                        case "simgle":

                            //CHECKBOX ALL
                            sbScript.Append(" $('.chkAll').hide() ").AppendLine();

                            //CHECKBOX DAS LINHAS DE REGISTROS
                            sbScript.Append(" $('.chkListagem').click(function(){ ").AppendLine();
                            sbScript.Append("   if($('.chkListagem INPUT:checked').length > 1) { ").AppendLine();
                            sbScript.Append("       MessageBox('Você não pode selecionar mais de um registro.'); return false; ").AppendLine();
                            sbScript.Append("   } ").AppendLine();
                            sbScript.Append(" }); ").AppendLine();

                            break;

                        //A SELECAO MULTIPLA NÃO POSSUI IMPLEMENTACAO
                        case "multiple":
                            break;

                        default:
                            break;
                    }

                    //CLICK DO BOTÃO DE SALVAR E FECHAR A MODAL
                    sbScript.Append(" $('.btnSalvarRegistrosModal').click(function(){ ").AppendLine();

                    sbScript.Append("       var strIds = ''; ").AppendLine();

                    sbScript.Append("       $('.chkListagem INPUT:checked').each(function(){ ").AppendLine();
                    sbScript.Append("           if(strIds.length > 0){ strIds += '|'; } ").AppendLine();
                    sbScript.Append("           strIds += $(this).parent('SPAN').next().val();  ").AppendLine();
                    sbScript.Append("       }); ").AppendLine();

                    sbScript.Append("       if(strIds.length == 0){ MessageBox('Selecione pelo menos um registro'); } ").AppendLine();
                    sbScript.Append("       else{ window.parent.ClickSubForm(strIds, '").Append(controlIdCallModal).Append("'); window.parent.$.fancybox.close(); } ").AppendLine();

                    sbScript.Append("       return false; ").AppendLine();

                    sbScript.Append(" }); ").AppendLine();

                }

                sbScript.Append("});").AppendLine();

                //Escreve o bloco de script referente a configurações da modal
                script.Controls.Add(new LiteralControl(sbScript.ToString()));
                ctrl.Controls.Add(script);

                phSalvarRegistrosModal.Visible = true;
                btnSalvarRegistrosModal.Rel = controlIdCallModal;

                sq = new Ag2.Security.SecureQueryString();

                sq["t"] = tipoForm;
                sq["md"] = _moduleName;
                sq["ts"] = selectType;
                sq["control"] = controlIdCallModal;

                //AJUSTA O LINK DE NOVO REGISTRO PARA MANDAR A VARIAVEL DA MODAL
                lnkNewRegister.NavigateUrl = string.Format("edit.aspx?q={0}", sq.ToString());
            }

        }
        else
        {
            //Apaga todas as sessões referente ao controle subform
            List<string> keysRemove = new List<string>();
            foreach (string key in Session.Keys)
            {
                if (key.IndexOf("subform", StringComparison.OrdinalIgnoreCase) > -1)
                    keysRemove.Add(key);
            }

            keysRemove.ForEach(delegate(string key)
            {

                Session.Remove(key);

            });
        }

        #endregion

        //Instancia gerenciador de modulos
        activeModule = new ManagerModule();

        //carrega modulo
        activeModule.Load(_moduleName, (Assembly)System.Web.Compilation.BuildManager.CodeAssemblies[0]);

        if (activeModule.ModuleStructure.HasListforms)
        {
            activeModule.CarregaListform();
            return;
        }

        //verifica se o modulo em questão possui filtros 
        if (activeModule.ModuleStructure.HasFilters)
        {

            //monta filtros          
            for (int i = 0; i < activeModule.ModuleStructure.Filters.Count; i++)
            {
                //adiciona titulo
                HtmlGenericControl divFilter = new HtmlGenericControl("div");
                divFilter.Attributes.Add("class", "filter");
                divFilter.InnerHtml = string.Concat(activeModule.ModuleStructure.Filters[i].Label, "<br />");

                //cria controle
                Control filterControl = CreateFilterControl(activeModule.ModuleStructure.Filters[i]);
                if (filterControl != null)
                    divFilter.Controls.Add(filterControl);

                //adiciona controle
                boxFilter.Controls.Add(divFilter);
            }

            //configura botão de filtro
            btnFilter.OnClientClick = string.Format("filterBox('{0}')", IsFilterData.ClientID);
        }
        else
        {
            //remove botão de filtro
            showFilter.Visible = false;
        }

        if (activeModule.ModuleStructure.Options.Count > 0)
        {
            cmbOptions.DataSource = activeModule.ModuleStructure.Options;
            cmbOptions.DataValueField = "QuerySection";
            cmbOptions.DataTextField = "Name";
            cmbOptions.DataBind();
            cmbOptions.Items.Insert(0, new ListItem("Selecione...", ""));
            phOptions.Visible = true;
        }
        else
        {
            phOptions.Visible = false;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Ag2.Manager.Helper.CurrentSessions cs = new Ag2.Manager.Helper.CurrentSessions();
        cs.ActiveManagerModule = activeModule;

        if (activeModule.ModuleStructure.HasListforms)
        {
            return;
        }

        btnExcluir.Click += new EventHandler(btnExcluir_Click);
        btnExcluir.Attributes.Add("OnClick", "return confirm('Deseja realmente apagar este(s) registro(s)?');");

        //verifica se o box de filtro está sendo exibido
        _showFilterBox = IsShwoFilterBox.Value.Equals("S");
        //tableFilter.Style["display"] = _showFilterBox ? "block" : "none";


        //verifica se estra filtrando dados
        _filterData = IsFilterData.Value.Equals("S");

        //verifica configuração de filtros
        if (_filterData)
        {
            //popula filtros com valores
            LoadFilters();

            //verifica se tem dados filtrados
            if (!IsDataFiltered())
            {
                tableFilter.Style["display"] = "none";
                IsFilterData.Value = "N";
                showFilter.ImageUrl = "~/img/btn_aplicar_filtro.gif";
                IsShwoFilterBox.Value = "N";
            }
            else
            {
                tableFilter.Style["display"] = "block";
                showFilter.ImageUrl = "~/img/btn_aplicar_filtro_des.gif";
                IsShwoFilterBox.Value = "S";
            }
        }

        //Popula o grid
        BindGrid(sender);

    }

    protected void BindGrid(object sender)
    {
        //carrega dados para o datagrid
        DataSet ds = activeModule.GetData();
        DataTable moduleData = ds.Tables[0];

        if (activeModule.ModuleStructure.Multilanguage)
        {
            //LISTA DE IDIOMAS
            managerContent.Idiomas = ds.Tables[1];
        }

        if (moduleData.Rows.Count > 0)
        {
            List<Int32> registers = new List<Int32>();
            foreach (DataRow dr in moduleData.Rows)
            {
                registers.Add(Convert.ToInt32(dr[0].ToString()));
            }
            cs.RegisterNavigator = registers;
        }
        else
        {
            cs.RegisterNavigator = null;
        }

        //SETA A CLASSE DO MODULO CORRENTE
        managerContent.ManagerModule = activeModule;

        //se nao for PostBack configura GridView
        if (!IsPostBack)
        {
            //Configura titulo do modulo            
            lblModuleTitle.Text = activeModule.ModuleStructure.Title;

            //configura datagrid
            ConfigureList(managerContent, activeModule.ModuleStructure);

            //configura colunas do datagrid
            managerContent.CreateListColumns();
        }

        pagingBottom.RowsCount = moduleData.Rows.Count;

        if (!SessaoOrdenacao.DataFieldName.Equals(string.Empty))
            moduleData.DefaultView.Sort = string.Format("{0} {1}", SessaoOrdenacao.DataFieldName, SessaoOrdenacao.Direction.ToString());

        managerContent.DataSource = moduleData;
        managerContent.DataBind();

        //efetua configuração de permissões do formulario
        //ManagerMenuItem menuItem = new ManagerMenu().SelectedItem;
        ConfigurePagePermission();

    }

    void btnExcluir_Click(object sender, EventArgs e)
    {
        List<string> registerDelete = new List<string>();

        foreach (GridViewRow row in managerContent.Rows)
        {
            if (((CheckBox)row.FindControl("chkSelect")).Checked)
                registerDelete.Add(managerContent.DataKeys[row.RowIndex].Value.ToString());
        }

        if (registerDelete.Count > 0)
        {
            ICollection<DeleteRegisterInfo> deleteInfo = activeModule.DeleteData(registerDelete);
            //verifica se tem
            if (deleteInfo.Count > 0)
            {
                IEnumerator<DeleteRegisterInfo> IEn = deleteInfo.GetEnumerator();
                IEn.MoveNext();
                //Page.RegisterStartupScript("scrStatusMessage", "<script>setStatusMessage(\"" + IEn.Current.ErrorMessage + "\",\"true\");</script>");
                Ag2.Manager.Helper.Util.ShowMessage(IEn.Current.ErrorMessage);
            }

            DataTable moduleData = activeModule.GetData().Tables[0];
            managerContent.DataSource = moduleData;
            managerContent.DataBind();
            pagingBottom.RowsCount = moduleData.Rows.Count;
            pagingBottom.showNavigationButtons();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }

    protected Control CreateFilterControl(ManagerModuleField filter)
    {
        Control filterControl = null;
        switch (filter.Type)
        {
            case ManagerModuleFieldType.Text:
                filterControl = new TextBox();
                ((TextBox)filterControl).ID = string.Concat("filter", filter.Name);

                if (!((ManagerModuleFieldText)filter).InputMask.Equals(string.Empty))
                    ((TextBox)filterControl).CssClass = string.Format("frmborder {0}", ((ManagerModuleFieldText)filter).InputMask);
                else
                    ((TextBox)filterControl).CssClass = "frmborder";

                ((TextBox)filterControl).Width = 160;
                break;

            case ManagerModuleFieldType.Upload:
                filterControl = new TextBox();
                ((TextBox)filterControl).ID = string.Concat("filter", filter.Name);
                ((TextBox)filterControl).CssClass = "frmborder";
                ((TextBox)filterControl).Width = 160;
                break;

            case ManagerModuleFieldType.ListBox:
                filterControl = new ListBox();
                ((ListBox)filterControl).ID = string.Concat("filter", filter.Name);
                ((ListBox)filterControl).CssClass = "frmborder";
                ((ListBox)filterControl).SelectionMode = ListSelectionMode.Single;
                ((ListBox)filterControl).Rows = 1;
                ((ListBox)filterControl).DataTextField = ((ManagerModuleFieldListBox)filter).DataTextField;
                ((ListBox)filterControl).DataValueField = ((ManagerModuleFieldListBox)filter).DataValueField;

                if (((ManagerModuleFieldListBox)filter).TriggerField == null)
                {
                    if (!String.IsNullOrEmpty(((ManagerModuleFieldListBox)filter).DataSource))
                    {
                        ((ListBox)filterControl).DataSource = activeModule.GetComboData((ManagerModuleFieldListBox)filter);
                        ((ListBox)filterControl).DataBind();
                    }
                }
                ((ListBox)filterControl).Items.Insert(0, new ListItem(":: Todos ::", ""));

                if (!((ManagerModuleFieldListBox)filter).FilterListBox.Equals(""))
                {
                    ((ListBox)filterControl).AutoPostBack = true;
                    ((ListBox)filterControl).SelectedIndexChanged += new EventHandler(this.ListBoxFilterEvent2);
                }
                break;

            case ManagerModuleFieldType.CheckBox:
                filterControl = new ListBox();
                ((ListBox)filterControl).ID = string.Concat("filter", filter.Name);
                ((ListBox)filterControl).CssClass = "frmborder";
                ((ListBox)filterControl).SelectionMode = ListSelectionMode.Single;
                ((ListBox)filterControl).Rows = 1;
                ((ListBox)filterControl).Items.Add(new ListItem(":: todos ::", ""));

                if (_moduleName == "pessoa" )
                {
                    if (_moduleName == "pessoa" && filter.Name == "tipoPessoa")
                    {
                        ((ListBox)filterControl).Items.Add(new ListItem("PF", "0"));
                        ((ListBox)filterControl).Items.Add(new ListItem("PJ", "1"));
                        ((ListBox)filterControl).Items[0].Selected = true;
                    }
                    else
                    {
                        ((ListBox)filterControl).Items.Add(new ListItem("Sim", "1"));
                        ((ListBox)filterControl).Items.Add(new ListItem("Não", "0"));
                    }
                }
                else if (_moduleName == "solicitacaoTituloAvalicao")
                {
                    if (filter.Name == "statusProfessor")
                    {
                        ((ListBox)filterControl).Items.Add(new ListItem("Avaliado", "1"));
                        ((ListBox)filterControl).Items.Add(new ListItem("Pendente", "0"));
                    }
                    else
                    {
                        ((ListBox)filterControl).Items.Add(new ListItem("Sim", "1"));
                        ((ListBox)filterControl).Items.Add(new ListItem("Não", "0"));
                    }
                }
                else
                {
                    ((ListBox)filterControl).Items.Add(new ListItem("Sim", "1"));
                    ((ListBox)filterControl).Items.Add(new ListItem("Não", "0"));
                }
                break;

            case ManagerModuleFieldType.Date:
                filterControl = new DateField();
                ((DateField)filterControl).ID = string.Concat("filter", filter.Name);
				((DateField)filterControl).CssClass = "data dateField frmborder";
                ((DateField)filterControl).Attributes.Add("style", "margin-right: 3px;");
                ((DateField)filterControl).Width = 70;
                break;

        }


        return filterControl;
    }

    protected void ConfigureList(GridViewEditable grid, ManagerModuleStructure module)
    {
        grid.AllowSorting = module.AllowPaging;
        grid.AllowPaging = module.AllowPaging;

        //Escode linha de paginacao do GridView
        grid.PagerSettings.Visible = false;

        //Define a quantidade de registros por pagina
        grid.PageSize = module.PageSize;
    }

    protected void managerContent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        #region "Header Grid"
        if (e.Row.RowType == DataControlRowType.Header)
        {
            ManagerModuleStructure _moduleStructure = activeModule.ModuleStructure;
            Panel pnlHeader = null;
            Panel pnlTitulo = null;
            Panel pnlSetaOrdenacao = null;
            LinkButton lnkOrdenacao = null;
            Image imgSeta = null;

            for (int i = 0; i < e.Row.Cells.Count - 1; i++)
            {

                if (e.Row.Cells[i + 1].Text.Equals("Traduções", StringComparison.OrdinalIgnoreCase))
                    continue;

                pnlHeader = new Panel();
                pnlTitulo = new Panel();
                pnlSetaOrdenacao = new Panel();
                lnkOrdenacao = new LinkButton();

                pnlHeader.CssClass = "divGridHeader";
                pnlTitulo.CssClass = "divGridTitulo";
                pnlSetaOrdenacao.CssClass = "divSetaOrdenacao";
                lnkOrdenacao.CssClass = "lnkOrdenacao";

                pnlTitulo.Controls.Add(new LiteralControl(e.Row.Cells[i + 1].Text));

                imgSeta = new Image();

                if (SessaoOrdenacao.Direction == Ag2.Manager.Entity.OrdenacaoColunas.DirectionOrder.ASC)
                {
                    imgSeta.ImageUrl = "~/img/blt_seta_down.gif";
                }
                else
                {
                    imgSeta.ImageUrl = "~/img/blt_seta_up.gif";
                }

                lnkOrdenacao.CausesValidation = false;
                lnkOrdenacao.ToolTip = "Ordenar coluna";
                lnkOrdenacao.Controls.Add(imgSeta);

                foreach (ManagerModuleField field in _moduleStructure.Fields)
                {
                    if (field.Label.Equals(Server.HtmlDecode(e.Row.Cells[i + 1].Text)))
                    {
                        if (!field.Sort)
                        {
                            lnkOrdenacao.Visible = false;
                            break;
                        }

                        lnkOrdenacao.CommandArgument = field.ListFieldName;
                        break;
                    }
                }

                lnkOrdenacao.Click += new EventHandler(lnkOrdenacao_Click);

                pnlSetaOrdenacao.Controls.Add(lnkOrdenacao);

                pnlHeader.Controls.Add(pnlTitulo);
                pnlHeader.Controls.Add(pnlSetaOrdenacao);

                e.Row.Cells[i + 1].Controls.Add(pnlHeader);

            }
        }
        #endregion

        #region "Linhas Grid"
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyName = ((System.Web.UI.WebControls.GridView)(sender)).DataKeyNames[0];
            string strKey = ((System.Data.DataRowView)(e.Row.DataItem)).Row[strKeyName].ToString();

            if (tipoForm.Equals("modal", StringComparison.OrdinalIgnoreCase))
            {
                //USADO SOMENTE QUANDO É MODAL PARA PEGAR OS IDs DOS REGISTROS AO FECHAR A JANELA MODAL
                HiddenField hdnId = (HiddenField)e.Row.FindControl("hdnId");
                hdnId.Value = strKey;
            }

            try
            {
                if (_moduleName.ToLower().Equals("usuario"))
                {
                    if (e.Row.Cells[1].Text == cs.User.name.ToString())
                    {
                        ((CheckBox)e.Row.Cells[0].Controls[1]).Visible = false;
                    }
                }
                if (_moduleName.ToLower().Equals("userperfil"))
                {
                    if (cs.User.perfis.Exists(delegate(Ag2.Manager.Entity.ag2mngPerfil p) { return p.name.Equals(Server.HtmlDecode(e.Row.Cells[1].Text)); }))
                    {
                        ((CheckBox)e.Row.Cells[0].Controls[1]).Visible = false;
                    }
                }

                if (Session["colunaImagem"] != null)
                {
                    int cell = Convert.ToInt32(Session["colunaImagem"].ToString());
                    e.Row.Cells[cell].Text = "<img class=\"preview\" width=\"" + ((ManagerModuleFieldImagePreview)Session["dadosImagem"]).WidthThumb + "\" src=\"" + Session["caminhoImagem"].ToString() + e.Row.Cells[cell].Text + "\" />";
                }

            }
            catch (Exception)
            {
                //throw;
            }

            e.Row.Attributes.Add("onMouseOver", "this.style.backgroundColor='#FFCE9D';this.style.cursor='pointer';");
            e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor=''");

            for (int i = 1; i < e.Row.Cells.Count; i++)
            {
                if (managerContent.EditIndex != e.Row.RowIndex)
                {
                    string redirect = string.Empty;
                    sqGrid = new Ag2.Security.SecureQueryString();
                    sqGrid["md"] = _moduleName;
                    sqGrid["id"] = strKey;

                    redirect = string.Format("~/content/edit.aspx?q={0}", sqGrid.ToString());

                    if (activeModule.ModuleStructure.Multilanguage)
                    {
                        if (tipoForm.Equals("modal", StringComparison.OrdinalIgnoreCase))
                        {
                            sqGrid["t"] = tipoForm;
                            sqGrid["ts"] = selectType;
                            sqGrid["control"] = controlIdCallModal;
                            sqGrid["lg"] = cs.CurrentIdioma.IdiomaId.ToString();
                            redirect = string.Format("~/content/edit.aspx?q={0}", sqGrid.ToString());
                        }
                        else
                        {
                            sqGrid["lg"] = cs.CurrentIdioma.IdiomaId.ToString();
                            redirect = string.Format("~/content/edit.aspx?q={0}", sqGrid.ToString());
                        }

                        if (!i.Equals(e.Row.Cells.Count - 1))
                            e.Row.Cells[i].Attributes.Add("onclick", string.Format("window.location.href='{0}'", ResolveUrl(redirect)));
                        else
                        {
                            e.Row.Cells[i].Attributes.Add("style", "cursor: default;");
                        }

                    }
                    else
                    {
                        e.Row.Cells[i].Attributes.Add("onclick", string.Format("window.location.href='{0}'", ResolveUrl(redirect)));
                    }
                }
            }
        }
        #endregion
    }

    void lnkOrdenacao_Click(object sender, EventArgs e)
    {
        LinkButton btnOrdenacao = (LinkButton)sender;

        //AJUSTA SOMENTE A SESSAO PARA DEPOIS MONTAR O GRID COM BASE NO VALOR SETADO AQUI
        if (SessaoOrdenacao.Direction == Ag2.Manager.Entity.OrdenacaoColunas.DirectionOrder.ASC)
        {
            SessaoOrdenacao.Direction = Ag2.Manager.Entity.OrdenacaoColunas.DirectionOrder.DESC;
        }
        else
        {
            SessaoOrdenacao.Direction = Ag2.Manager.Entity.OrdenacaoColunas.DirectionOrder.ASC;
        }

        SessaoOrdenacao.DataFieldName = btnOrdenacao.CommandArgument;

        //MONTA O GRID
        BindGrid(sender);
    }

    protected void btnAction_Click(object sender, ImageClickEventArgs e)
    {

        //List<string> registerDelete = new List<string>();

        //foreach (GridViewRow row in managerContent.Rows)
        //{
        //    if (((CheckBox)row.FindControl("chkSelect")).Checked)
        //        registerDelete.Add(managerContent.DataKeys[row.RowIndex].Value.ToString());
        //}

        //if (registerDelete.Count > 0)
        //{
        //    ICollection<DeleteRegisterInfo> deleteInfo = activeModule.DeleteData(registerDelete);
        //    //verifica se tem
        //    if (deleteInfo.Count > 0)
        //    {
        //        IEnumerator<DeleteRegisterInfo> IEn = deleteInfo.GetEnumerator();
        //        IEn.MoveNext();
        //        //Page.RegisterStartupScript("scrStatusMessage", "<script>setStatusMessage(\"" + IEn.Current.ErrorMessage + "\",\"true\");</script>");
        //        Ag2.Manager.Helper.Util.ShowMessage(IEn.Current.ErrorMessage);
        //    }

        //    DataTable moduleData = activeModule.GetData().Tables[0];
        //    managerContent.DataSource = moduleData;
        //    managerContent.DataBind();
        //    pagingBottom.RowsCount = moduleData.Rows.Count;
        //    pagingBottom.showNavigationButtons();
        //}

    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        managerContent.DataBind();
        pagingBottom.showNavigationButtons();
    }

    protected void btnExecOptions_Click(object sender, EventArgs e)
    {
        if (cmbOptions.SelectedIndex > 0)
        {
            foreach (Query query in activeModule.ModuleStructure.Queries)
            {
                if (cmbOptions.SelectedValue.Equals(query.Section))
                {
                    optionscontroller.Export(query, _moduleName, cmbOptions.SelectedItem);
                    break;
                }
            }
        }
    }

    protected void LoadFilters()
    {
        foreach (ManagerModuleField field in activeModule.ModuleStructure.Filters)
        {
            //procura pelo controle
            Control filter = boxFilter.FindControl(string.Concat("filter", field.Name));

            //verifica se achou o controle
            if (filter != null)
            {
                //verifica tipo do filtro
                switch (field.Type)
                {
                    case ManagerModuleFieldType.Text:
                        field.FilterValue = filter != null ? ((TextBox)filter).Text : "";
                        break;

                    case ManagerModuleFieldType.Date:
                        field.FilterValue = filter != null ? ((DateField)filter).Text : "";
                        break;

                    case ManagerModuleFieldType.CheckBox:
                    case ManagerModuleFieldType.ListBox:
                        field.FilterValue = filter != null ? ((ListBox)filter).SelectedValue : "";
                        break;
                }
            }
        }

        cs.CurrentFilters = activeModule.ModuleStructure.Filters;
    }

    public bool IsDataFiltered()
    {
        bool dataFiltered = false;

        foreach (ManagerModuleField filter in activeModule.ModuleStructure.Filters)
        {
            if (!filter.FilterValue.Equals(""))
            {
                dataFiltered = true;
                break;
            }
        }

        return dataFiltered;
    }

    public void ListBoxFilterEvent2(object sender, EventArgs e)
    {

        //pega ID do combo que disparou o evento
        string senderName = ((ListBox)sender).ID.Substring(6);


        //procura combo na coleção
        int fieldIndex = activeModule.FindFieldIndexByName(senderName);

        //verifica se o objeto foi encontrado na coleção
        if (fieldIndex >= 0)
        {
            //pega valor selecionado no combo
            string selectedValue = ((ListBox)sender).SelectedValue;

            //verifica que objeto ele estará filtrando
            string targetName = ((ManagerModuleFieldListBox)activeModule.ModuleStructure.Fields[fieldIndex]).FilterListBox;

            //garente que a propriedade foi preenchida
            if (!targetName.Equals(""))
            {

                //pega campo que será utilizado para filtro da consulta
                string filterField = ((ManagerModuleFieldListBox)activeModule.ModuleStructure.Fields[fieldIndex]).FilterByField;

                //procura na coleção campo que será alvo do filtro
                fieldIndex = activeModule.FindFieldIndexByName(targetName);

                //
                ListBox targetListBox = (ListBox)boxFilter.FindControl("filter" + targetName);  //activeModule.FindControlByID("filter"+targetName);

                if (targetListBox != null)
                {
                    targetListBox.DataSource = activeModule.GetComboData((ManagerModuleFieldListBox)activeModule.ModuleStructure.Fields[fieldIndex], filterField, selectedValue);
                    targetListBox.DataBind();
                    targetListBox.Items.Insert(0, (new ListItem(":: Todos ::", "")));
                }
            }
        }

    }

    public void ConfigurePagePermission()
    {
		if (menu != null)
		{
			if (!menu.fullControl && !menu.canDelete && menu.canInsert && !menu.canRead && !menu.canUpdate)
			{
				Response.Redirect(lnkNewRegister.NavigateUrl, true);
			}

			lnkNewRegister.Visible = false;

			//permissão para deletar
			btnExcluir.Visible = (menu.fullControl || menu.canDelete);
			divSetaExcluir.Visible = btnExcluir.Visible;

			//verifica se tem permissão de escrita
			lnkNewRegister.Visible = (menu.fullControl || menu.canInsert);

			//verifica se tem permissão para exclusão
			managerContent.Columns[0].Visible = (menu.fullControl || menu.canDelete);
		}
		else
		{
			Response.Redirect("~/Default.aspx");
		}
    }

    public Ag2.Manager.Entity.OrdenacaoColunas SessaoOrdenacao
    {
        get
        {
            Ag2.Manager.Entity.OrdenacaoColunas oc = null;
            if (Session["_sessaoOrdenacao"] == null)
            {
                oc = new Ag2.Manager.Entity.OrdenacaoColunas();
                Session["_sessaoOrdenacao"] = oc;
            }

            oc = (Ag2.Manager.Entity.OrdenacaoColunas)Session["_sessaoOrdenacao"];

            return oc;
        }
        set
        {
            Session["_sessaoOrdenacao"] = value;
        }
    }
}


