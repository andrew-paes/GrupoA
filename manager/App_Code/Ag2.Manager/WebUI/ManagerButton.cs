using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;


namespace Ag2.Manager.WebUI
{
    [Serializable]
    [ValidationProperty("Text")]
    [DefaultProperty("Text"), ToolboxData("<{0}:ManagerButton runat=server></{0}:ManagerButton>")]
    public class ManagerButton : System.Web.UI.WebControls.WebControl, INamingContainer
    {
        private Image _image;
        private HyperLink _hyperLink;
        private string _dataSource = string.Empty;
        private string _primaryKey = string.Empty;
        private string _descriptionField = string.Empty;
        private string _titleModal = string.Empty;

        public ManagerButton()
        {
            _hyperLink = new HyperLink();
            _image = new ImageButton();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected override void CreateChildControls()
        {
            Ag2.Security.SecureQueryString sq = new Ag2.Security.SecureQueryString();

            sq["DataSource"] = DataSource;
            sq["PrimaryKey"] = PrimaryKey;
            sq["DescriptionField"] = DescriptionField;
            sq["Title"] = TitleModal;

            //CONFIGURA LINK PARA ABRIR MODAL
            _hyperLink.Attributes.Add("rel", "shadowbox;width=400;height=300");
            _hyperLink.ToolTip = _titleModal;
            _hyperLink.NavigateUrl = string.Format("~/content/register.aspx?v={0}", sq.ToString());

            _image.ImageUrl = "~/img/btn_cad.jpg";

            _hyperLink.Controls.Add(_image);

            this.Controls.Add(_hyperLink);
            this.ChildControlsCreated = true;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            this.RenderChildren(writer);
        }

        /// <summary>
        /// Seta imagem source para o botão de cadastro
        /// </summary>
        public string ImageUrl
        {
            get { return this._image.ImageUrl; }
            set { this._image.ImageUrl = value; }
        }

        /// <summary>
        /// Seta qual tabela da base de dados será administrada pelo cadastro
        /// </summary>
        public string DataSource
        {
            get { return _dataSource; }
            set { _dataSource = value; }
        }

        /// <summary>
        /// Seta qual é o campo chave do dataSource
        /// </summary>
        public string PrimaryKey
        {
            get { return _primaryKey; }
            set { _primaryKey = value; }
        }

        /// <summary>
        /// Seta qual campo do DataSource será inserida pelo cadastro
        /// </summary>
        public string DescriptionField
        {
            get { return _descriptionField; }
            set { _descriptionField = value; }
        }

        /// <summary>
        /// Seta titulo da modal
        /// </summary>
        public string TitleModal
        {
            get { return _titleModal; }
            set { _titleModal = value; }
        }
    }


}
