using System;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Text;

namespace Ag2.Manager.WebUI
{
    [Serializable]
    [ValidationProperty("FileName")]
    [DefaultProperty("Value"), ToolboxData("<{0}:UploadField runat=server></{0}:UploadField>")]
    public class UploadField : System.Web.UI.WebControls.WebControl, INamingContainer
    {

        #region PRIVATE VARIABLE
        private int _maxFileLength = 80;
        private string _allowedExtensions = "*.*";
        private bool _createUniqueName;
        private string _findButtonUrl, _changeButtonUrl, _deleteButtonUrl;
        private string _targetFolder;
        private TextBox _fileName;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public UploadField()
        {
            _createUniqueName = true;
            _allowedExtensions = "*.*";
            _fileName = new TextBox();
        }


        #region PROPERTIRES

        /// <summary>
        /// Image of button browsea file
        /// </summary>
        [UrlProperty("*.jpg;*.gif;*.png")]
        public string FindButtonUrl
        {
            set { _findButtonUrl = value; }
        }

        /// <summary>
        /// Image of button to replace a file
        /// </summary>
        [UrlProperty("*.jpg;*.gif;*.png")]
        public string ChangeButtonUrl
        {
            set { _changeButtonUrl = value; }
        }

        /// <summary>
        /// Image of button to delete a file
        /// </summary>
        [UrlProperty("*.jpg;*.gif;*.png")]
        public string DeleteButtonUrl
        {
            set { _deleteButtonUrl = value; }
        }


        /// <summary>
        /// Name of selected file
        /// </summary>
        public string FileName
        {
            set { _fileName.Text = value; ViewState["fileName"] = value; }
            get { return _fileName.Text; }
        }

        public string MaxFileLength
        {
            set { this._maxFileLength = value == "" ? 0 : Convert.ToInt32(value); }
            get { return Convert.ToString(this._maxFileLength); }
        }

        public bool CreateUniqueName
        {
            set { _createUniqueName = value; }
            get { return _createUniqueName; }
        }


        /// <summary>
        /// File extesions allowed in upload
        /// </summary>
        public string AllowedExtensions
        {
            set { _allowedExtensions = value; }
            get { return _allowedExtensions; }
        }

        public string TargetFolder
        {
            set { _targetFolder = value; }
            get { return _targetFolder; }
        }
        #endregion

        /// <summary>
        /// Component Initialization
        /// </summary>
        /// <param name="e">Event</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        /// <summary>
        /// Create Child Controls 
        /// </summary>        
        protected override void CreateChildControls()
        {
            //base.CreateChildControls();
            _fileName.ID = "fileName";
            _fileName.Attributes.Add("style", "display:none;");
            this.Controls.Add(_fileName);
            base.CreateChildControls();
            this.ChildControlsCreated = true;
        }

        /// <summary>
        /// Render Method
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            StringBuilder script = new StringBuilder();

            string id = string.Concat("divContainer", this.ID);

            script.AppendFormat("<div id=\"divUp{0}\">\n", id);
            script.Append("<script>\n");
            script.Append("var upload = new ag2.UploadApi();\n");
            script.AppendFormat("upload.id = \"{0}\";\n", this.ClientID);
            script.AppendFormat("upload.maxSize = {0};\n", _maxFileLength.ToString());
            script.Append("upload.maxWidth = 500;\n");
            script.Append("upload.maxHeight = 500;\n");

            if (_fileName.Text.Length > 0)
                script.AppendFormat("upload.fileName = \"{0}\";\n", _fileName.Text);
            else
                script.Append("upload.fileName = \"\";\n");

            script.AppendFormat("upload.baseUrlUpload = \"{0}\";\n", Ag2.Manager.Helper.ConfigurationManager.BaseUrlUpload.ToString());
            script.AppendFormat("upload.path = \"{0}\";\n", _targetFolder);
            script.AppendFormat("upload.addFilter(\"Arquivos ({0})\", \"{0}\");\n", _allowedExtensions);
            script.AppendFormat("upload.write(\"divUp{0}\");\n", id);
            script.Append("</script>\n");
            script.Append("</div>\n");

            base.RenderChildren(writer);

            LiteralControl lc = new LiteralControl(script.ToString());
            lc.RenderControl(writer);

        }
    }
}
