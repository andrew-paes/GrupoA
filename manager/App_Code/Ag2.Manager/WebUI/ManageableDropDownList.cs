using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Ag2.Manager.ADO.MSSql;
using Ag2.Manager.DAL;
using Ag2.Manager.Module;
using System.Reflection;

namespace Ag2.Manager.WebUI
{
    [Serializable]
    [ValidationProperty("Text")]
    [DefaultProperty("Text"), ToolboxData("<{0}:ManageableDropDownList runat=server></{0}:ManageableDropDownList>")]
    public class ManageableDropDownList : System.Web.UI.WebControls.WebControl, INamingContainer
    {
        private IManageableDropDownListADO manageabledropdownlistado = (IManageableDropDownListADO)Helper.Util.GetADO("ManageableDropDownListADO", System.Reflection.Assembly.GetExecutingAssembly());
        private UpdatePanel _updatePanel;
        private DropDownList _dropDownList;
        private TextBox _textBoxEdit;
        private ImageButton _imageButtonAdd;
        private ImageButton _imageButtonEdit;
        private ImageButton _imageButtonSave;
        private ImageButton _imageButtonDelete;
        private ImageButton _imageButtonCancel;
        private ImageButton _imageLoading;
        private Label _lblMsg;
        private string _primaryKey = string.Empty;
        private string _descriptionField = string.Empty;
        private string _titleModal = string.Empty;
        private string _tableName = string.Empty;

        public ManageableDropDownList()
        {
            _updatePanel = new UpdatePanel();
            _dropDownList = new DropDownList();
            _textBoxEdit = new TextBox();
            _imageButtonAdd = new ImageButton();
            _imageButtonEdit = new ImageButton();
            _imageButtonSave = new ImageButton();
            _imageButtonDelete = new ImageButton();
            _imageButtonCancel = new ImageButton();
            _imageLoading = new ImageButton();
            _lblMsg = new Label();
        }

        protected override void OnInit(EventArgs e)
        {
            LoadData();

            base.OnInit(e);
        }

        private void LoadData()
        {
            _dropDownList.Items.Clear();
            _dropDownList.Items.Insert(0, new ListItem("Selecione...", ""));

            ListItemCollection itens = manageabledropdownlistado.SelectAll(_tableName, this.DataValueField, this.DataTextField);

            foreach (ListItem item in itens)
            {
                _dropDownList.Items.Add(item);
            }
        }

        protected override void CreateChildControls()
        {
            _updatePanel.ID = string.Format("upp_{0}", this.ID);
            _updatePanel.RenderMode = UpdatePanelRenderMode.Inline;
            _updatePanel.UpdateMode = UpdatePanelUpdateMode.Conditional;

            _dropDownList.ID = string.Format("ddl_{0}", this.ID);
            _updatePanel.ContentTemplateContainer.Controls.Add(_dropDownList);

            _textBoxEdit.Visible = false;
            _textBoxEdit.Width = Unit.Pixel(200);
            _textBoxEdit.MaxLength = 50;
            _updatePanel.ContentTemplateContainer.Controls.Add(_textBoxEdit);

            _imageButtonAdd.Click += new ImageClickEventHandler(_imageButtonAdd_Click);
            _imageButtonAdd.CssClass = "icoAction";
            _imageButtonAdd.CausesValidation = false;
            _imageButtonAdd.AlternateText = "Adicionar";
            _imageButtonAdd.ImageUrl = "~/img/adicionar.jpg";
            _updatePanel.ContentTemplateContainer.Controls.Add(_imageButtonAdd);

            _imageButtonEdit.Click += new ImageClickEventHandler(_imageButtonEdit_Click);
            _imageButtonEdit.CssClass = "icoAction";
            _imageButtonEdit.CausesValidation = false;
            _imageButtonEdit.AlternateText = "Editar";
            _imageButtonEdit.ImageUrl = "~/img/editar.jpg";
            _imageButtonEdit.Visible = true;
            _updatePanel.ContentTemplateContainer.Controls.Add(_imageButtonEdit);

            _imageButtonSave.Click += new ImageClickEventHandler(_imageButtonSave_Click);
            _imageButtonSave.CssClass = "icoAction";
            _imageButtonSave.CausesValidation = false;
            _imageButtonSave.AlternateText = "Salvar";
            _imageButtonSave.ImageUrl = "~/img/salvar.jpg";
            _imageButtonSave.Visible = false;
            _updatePanel.ContentTemplateContainer.Controls.Add(_imageButtonSave);

            _imageButtonDelete.Click += new ImageClickEventHandler(_imageButtonDelete_Click);
            _imageButtonDelete.CssClass = "icoAction";
            _imageButtonDelete.CausesValidation = false;
            _imageButtonDelete.AlternateText = "Apagar";
            _imageButtonDelete.ImageUrl = "~/img/excluir.jpg";
            _imageButtonDelete.Visible = true;
            _updatePanel.ContentTemplateContainer.Controls.Add(_imageButtonDelete);

            _imageButtonCancel.Click += new ImageClickEventHandler(_imageButtonCancel_Click);
            _imageButtonCancel.CssClass = "icoAction";
            _imageButtonCancel.CausesValidation = false;
            _imageButtonCancel.AlternateText = "Cancelar";
            _imageButtonCancel.ImageUrl = "~/img/voltar.jpg";
            _imageButtonCancel.Visible = false;
            _updatePanel.ContentTemplateContainer.Controls.Add(_imageButtonCancel);

            _lblMsg.ForeColor = System.Drawing.ColorTranslator.FromHtml("Red");
            _lblMsg.Visible = false;
            _updatePanel.ContentTemplateContainer.Controls.Add(_lblMsg);

            this.Controls.Add(_updatePanel);

            this.ChildControlsCreated = true;
        }

        void _imageButtonCancel_Click(object sender, ImageClickEventArgs e)
        {
            _imageButtonSave.CommandArgument = string.Empty;
            _textBoxEdit.Text = string.Empty;
            _dropDownList.Visible = true;
            _textBoxEdit.Visible = false;
            _imageButtonEdit.Visible = true;
            _imageButtonAdd.Visible = true;
            _imageButtonSave.Visible = false;
            _imageButtonDelete.Visible = true;
            _imageButtonCancel.Visible = false;
        }

        void _imageButtonEdit_Click(object sender, ImageClickEventArgs e)
        {
            if (!_dropDownList.SelectedValue.Equals(string.Empty))
            {
                ListItem li = _dropDownList.SelectedItem;

                _textBoxEdit.Text = li.Text;
                _dropDownList.Visible = false;
                _textBoxEdit.Visible = true;
                _imageButtonEdit.Visible = false;
                _imageButtonAdd.Visible = false;
                _imageButtonSave.Visible = true;
                _imageButtonSave.CommandArgument = li.Value;
                _imageButtonDelete.Visible = false;
                _imageButtonCancel.Visible = true;
            }
        }

        void _imageButtonDelete_Click(object sender, ImageClickEventArgs e)
        {
            if (!_dropDownList.SelectedValue.Equals(string.Empty))
            {
                if (manageabledropdownlistado.Delete(_tableName, this.DataValueField, _dropDownList.SelectedValue))
                {
                    _dropDownList.Items.RemoveAt(_dropDownList.SelectedIndex);
                    _dropDownList.ClearSelection();

                    _textBoxEdit.Text = string.Empty;
                    _dropDownList.Visible = true;
                    _imageButtonAdd.Visible = true;
                    _imageButtonEdit.Visible = true;
                    _imageButtonSave.Visible = false;
                    _imageButtonDelete.Visible = true;
                    _imageButtonCancel.Visible = false;
                }
                else
                {
                    //_lblMsg.Visible = true;
                    //_lblMsg.Text = "Não foi possível apagar o registro pois ele esta sendo usado.";
                    Helper.Util.ShowMessage("Não foi possível apagar o registro pois ele esta sendo usado.");
                }
            }
        }

        void _imageButtonSave_Click(object sender, ImageClickEventArgs e)
        {
            if (_textBoxEdit.Text.Length > 0)
            {
                if (!_imageButtonSave.CommandArgument.Equals(string.Empty))
                {
                    foreach (ListItem item in _dropDownList.Items)
                    {
                        if (item.Value.Equals(_imageButtonSave.CommandArgument))
                        {
                            if (!manageabledropdownlistado.RegisterExist(_tableName, this.DataValueField, item.Value, this.DataTextField, _textBoxEdit.Text))
                            {
                                ListItem li = manageabledropdownlistado.Save(_tableName, this.DataValueField, this.DataTextField, Convert.ToInt32(item.Value), _textBoxEdit.Text);
                                break;
                            }
                            else
                            {
                                Helper.Util.ShowMessage("Já existe um registro com este nome");
                                return;
                            }
                        }
                    }

                    LoadData();
                    
                    _dropDownList.SelectedValue = _imageButtonSave.CommandArgument;
                    _textBoxEdit.Text = string.Empty;
                    _imageButtonSave.CommandArgument = string.Empty;
                }
                else
                {
                    if (!manageabledropdownlistado.RegisterExist(_tableName, null, null, this.DataTextField, _textBoxEdit.Text))
                    {
                        ListItem li = manageabledropdownlistado.Save(_tableName, this.DataValueField, this.DataTextField, null, _textBoxEdit.Text);

                        LoadData();

                        _dropDownList.SelectedValue = li.Value;
                    }
                    else
                    {
                        Helper.Util.ShowMessage("Já existe um registro com este nome");
                        return;
                    }
                }

                _dropDownList.Visible = true;
                _textBoxEdit.Visible = false;
                _imageButtonAdd.Visible = true;
                _imageButtonEdit.Visible = true;
                _imageButtonSave.Visible = false;
                _imageButtonDelete.Visible = true;
                _imageButtonCancel.Visible = false;
            }
        }

        void _imageButtonAdd_Click(object sender, ImageClickEventArgs e)
        {
            _textBoxEdit.Text = string.Empty;
            _dropDownList.Visible = false;
            _textBoxEdit.Visible = true;
            _imageButtonEdit.Visible = false;
            _imageButtonAdd.Visible = false;
            _imageButtonSave.Visible = true;
            _imageButtonDelete.Visible = false;
            _imageButtonCancel.Visible = true;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            this.RenderChildren(writer);
        }

        public override void DataBind()
        {
            _dropDownList.DataBind();
        }

        public void ClearSelection()
        {
            _dropDownList.ClearSelection();
        }

        public string DataTextField
        {
            get { return _dropDownList.DataTextField; }
            set { _dropDownList.DataTextField = value; }
        }

        public string DataValueField
        {
            get { return _dropDownList.DataValueField; }
            set { _dropDownList.DataValueField = value; }
        }

        public Object DataSource
        {
            get { return _dropDownList.DataSource; }
            set { _dropDownList.DataSource = value; }
        }

        public int SelectedIndex
        {
            get { return _dropDownList.SelectedIndex; }
            set { _dropDownList.SelectedIndex = value; }
        }

        public ListItem SelectedItem
        {
            get { return _dropDownList.SelectedItem; }
        }

        public string SelectedValue
        {
            get { return _dropDownList.SelectedValue; }
            set { _dropDownList.SelectedValue = value; }
        }
                
        public ListItemCollection Items
        {
            get { return _dropDownList.Items; }
        }

        public override string CssClass
        {
            get
            {
                return _dropDownList.CssClass; ;
            }
            set
            {
                _textBoxEdit.CssClass = value;
                _dropDownList.CssClass = value;
            }
        }

        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }
    }
}
